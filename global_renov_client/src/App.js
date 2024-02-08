import './App.css';

import { Outlet, RouterProvider, createBrowserRouter } from 'react-router-dom';

import InterventionWithAxios from './component/interaction-api/InterventionWithAxios';
import InterventionWithFetchAsyncAwait from './component/interaction-api/InterventionWithFetchAsyncAwait';
import InterventionWithFetchThen from './component/interaction-api/InterventionWithFetchThen';
import NavBar from './component/NavBar';
import SingleIntervention from './component/SingleIntervention';

const router = createBrowserRouter([
  {
    path: '/',
    element: <div>
      <NavBar />
      <h1>Page d'Accueil</h1>
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
            path: 'fetch_then',
            element: <InterventionWithFetchThen />
          },
          {
            path: 'fetch_async',
            element: <InterventionWithFetchAsyncAwait />
          },
          {
            path: 'axios',
            element: <InterventionWithAxios />
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
