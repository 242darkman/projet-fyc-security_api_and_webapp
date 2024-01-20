using GlobalRenov.Datas;
using GlobalRenov.DTO;
using GlobalRenov.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GlobalRenov.Controllers
{
    public class AuthenticationController : ControllerBase
    {

        private readonly DefaultContext _context;
        private readonly string _jwtKey;

        public AuthenticationController(DefaultContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtKey = configuration["AppSettings:JwtKey"];
        }

        /**
         * Méthode de création d'un compte pour l'utilisateur
         */
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO model)
        {
            // Vérifier si l'utilisateur existe déjà
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                return BadRequest("Un utilisateur avec cet email existe déjà.");
            }

            // Générer un sel unique pour chaque utilisateur (à stocker avec l'utilisateur dans la base de données)
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hacher le mot de passe en utilisant PBKDF2 avec 10000 itérations et un algorithme de hachage SHA-256
            string hashedPassword = HashPassword(model.Password, salt);

            // Créer un nouvel utilisateur
            var newUser = new User
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                Password = hashedPassword,
                UserRoles = new List<UserRole> { new UserRole { Role = "Users" } },
                Salt = salt,
            };

            // Si des rôles sont spécifiés dans le modèle, les ajouter à l'utilisateur
            if (model.Roles != null && model.Roles.Any())
            {
                newUser.UserRoles.AddRange(model.Roles.Select(role => new UserRole { Role = role }));
            }

            // Enregistrer l'utilisateur dans la base de données
            _context.Add(newUser);
            _context.SaveChanges();

            return Ok("Inscription réussie.");
        }

        /**
         * Méthode qui permet de haché le password
         */
        public static string HashPassword(string password, byte[] salt)
        {
            // Utiliser PBKDF2 avec 10000 itérations et un algorithme de hachage SHA-256
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashedPassword;
        }


        /**
         * Méthode pour se connecter en générant un token
         */
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            // vérifier l'existence de l'utilisateur
            var user = _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
            {
                var hashedPassword = HashPassword(model.Password, user.Salt);
                if (hashedPassword == user.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.Email)
                    };

                    // Ajouter les rôles à la liste de revendications
                    foreach (var userRole in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role));
                    }

                    var token = GenerateToken(_jwtKey, claims);

                    // rétourner la réponse en JSON
                    var response = new
                    {
                        Token = token
                    };

                    return Ok(response);
                }
                else
                {
                    return Unauthorized("Mot de passe incorrect.");
                }
            }
            else
            {
                return Unauthorized("Email incorrect.");
            }
        }

        /**
         * Méthode pour générer le token lors de la connexion
         */
        private static string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
