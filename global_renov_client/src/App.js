import './App.css';
import { createBrowserRouter, Link, Outlet, RouterProvider } from 'react-router-dom';
import SingleIntervention from './component/SingleIntervention';

const router = createBrowserRouter([
  {
    path: '/',
    element: <div>
      <nav>
        <Link to={'/'}> Accueil </Link>
        <Link to={'/intervention'}> Intervention </Link>
        <Link to={'/intervention/242'}> Detail intervention </Link>
      </nav>
      <main>
        <Outlet />
      </main>
    </div>,
    children: [
      {
        path: '/intervention',
        element: 
          <main>
            <Outlet />
          </main>
        ,
        children: [
          {
            path: '',
            element: <div>Intervention</div>
          },
          {
        path: ':id',
        element: <SingleIntervention />
      }
        ]
      },
    ],
  },
  {
    path: '/intervention/:id',
    element: <SingleIntervention />
  }
]);



function App() {
  return (
    <RouterProvider router={router}/>
  );
}

export default App;
