using GlobalRenov.Datas;
using GlobalRenov.DTO;
using GlobalRenov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalRenov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly DefaultContext _context;

        public InterventionController(DefaultContext context)
        {
            _context = context;
        }


        /**
         * Méthode qui permet de crée une intervention
         */
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InterventionDTO interventionDTO)
        {

            var intervention = new Intervention
            {
                Description = interventionDTO.Description,
                Address = interventionDTO.Address,
                PhoneNumber = interventionDTO.PhoneNumber,
                UserInterventions = interventionDTO.Users.Select(u => new UserIntervention { UserId = u.Id }).ToList()
            };

            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();
            return Ok("création avec succès");
        }

        /**
         * Méthode pour récupérer la liste des interventions avec les users associés
         */
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInterventions()
        {
            var interventions = _context.Interventions
                .Include(i => i.UserInterventions)
                    .ThenInclude(ui => ui.User)
                .ToList();

            var interventionDTO = interventions.Select(i => new InterventionDTO
            {
                Id = i.Id,
                Description = i.Description,
                Address = i.Address,
                PhoneNumber = i.PhoneNumber,

                Users = i.UserInterventions.Select(ui =>
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == ui.UserId);

                    return new UserDTO
                    {
                        Id = ui.UserId,
                        FirstName = user?.FirstName,
                        LastName = user?.LastName,
                    };
                }).ToList()
            }).ToList();

            return Ok(interventionDTO);
        }

        /**
         * Méthode pour afficher le détail d'une intervention
         */
        [HttpGet("{id}")]
        public IActionResult GetInterventionDetails(int id)
        {
            // Vérifie si l'intervention avec l'ID spécifié existe
            var intervention = _context.Interventions
                .Include(i => i.UserInterventions)
                    .ThenInclude(ui => ui.User)
                .FirstOrDefault(i => i.Id == id);

            if (intervention == null)
            {
                return NotFound($"Intervention avec ID {id} n'existe pas.");
            }

            // Construit le DTO pour les détails de l'intervention
            var interventionDTO = new InterventionDTO
            {
                Id = intervention.Id,
                Description = intervention.Description,
                Address = intervention.Address,
                PhoneNumber = intervention.PhoneNumber,

                Users = intervention.UserInterventions.Select(ui => new UserDTO
                {
                    Id = ui.UserId,
                    FirstName = ui.User?.FirstName,
                    LastName = ui.User?.LastName
                }).ToList()
            };

            return Ok(interventionDTO);
        }



    }
}
