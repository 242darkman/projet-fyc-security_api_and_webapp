import React, { useEffect, useState } from 'react';

import { Link } from 'react-router-dom';

function InterventionWithFetchAsyncAwait() {
  const [interventions, setInterventions] = useState(null);

  useEffect(() => {
    const fetchInterventions = async () => {
      try {
        const response = await fetch('http://localhost:5230/api/Intervention');
        const data = await response.json();
        setInterventions(data);
      } catch (error) {
        console.error("Erreur lors du fetching:", error);
      }
    };

    fetchInterventions();
    
  }, []);

  if (!interventions) {
    return <div>Chargement des données en cours...</div>
  }

  return (
    <div>
      <h1>Intervention With Fetch and Async/await</h1>

      <section style={{ overflowX: 'auto', marginTop: '20px' }}>
        <table style={{
              width: '100%',
              borderCollapse: 'separate',
              borderSpacing: '0',
              boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
            }}>
                <thead style={{ backgroundColor: '#007bff', color: 'white' }}>
                    <tr>
                        <th style={{ border: '1px solid #ddd', padding: '10px 15px', textAlign: 'left' }}>ID</th>
                        <th style={{ border: '1px solid #ddd', padding: '10px 15px', textAlign: 'left' }}>Description</th>
                        <th style={{ border: '1px solid #ddd', padding: '10px 15px', textAlign: 'left' }}>Adresse</th>
                        <th style={{ border: '1px solid #ddd', padding: '10px 15px', textAlign: 'left' }}>Téléphone</th>
                        <th style={{ border: '1px solid #ddd', padding: '10px 15px', textAlign: 'left' }}>Intervenant</th>
                    </tr>
                </thead>
                <tbody>
                    {interventions.map((intervention, index) => (
                        <tr key={intervention.id} style={{
                          backgroundColor: index % 2 === 0 ? '#f8f9fa' : 'white',
                          transition: 'background-color 0.3s',
                        }}>
                            <td style={{ border: '1px solid #ddd', padding: '8px' }}>
                              <Link to={`/intervention/${intervention.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                                {intervention.id}
                              </Link>
                            </td>
                            <td style={{ border: '1px solid #ddd', padding: '8px' }}>{intervention.description}</td>
                            <td style={{ border: '1px solid #ddd', padding: '8px' }}>{intervention.address}</td>
                            <td style={{ border: '1px solid #ddd', padding: '8px' }}>{intervention.phoneNumber}</td>
                            <td style={{ border: '1px solid #ddd', padding: '8px' }}>
                                {intervention.users.map(user => `${user.firstName} ${user.lastName}`).join(', ')}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
      </section>
    </div>
  );
}

export default InterventionWithFetchAsyncAwait;