import { NavLink } from "react-router-dom";

const navStyle = {
  backgroundColor: '#f0f0f0',
  padding: '10px',
  display: 'flex',
  justifyContent: 'space-around',
};

const linkStyle = {
  color: 'blue',
  textDecoration: 'none',
  fontSize: '16px',
};

const NavBar = () => (
  <nav style={navStyle}>
    <NavLink to={'/'} style={linkStyle}> Accueil </NavLink>
    <NavLink to={'/intervention/fetch_then'} style={linkStyle}> Récup interventions avec Fetch/then </NavLink>
    <NavLink to={'/intervention/fetch_async'} style={linkStyle}> Récup interventions avec Fetch/async </NavLink>
    <NavLink to={'/intervention/axios'} style={linkStyle}> Récup interventions avec axios </NavLink>
  </nav>
);

export default NavBar;
