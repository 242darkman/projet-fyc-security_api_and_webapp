import React, { useEffect, useState } from 'react';

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
    return null;
  }

  return (
    <div>
      <h1>Intervention With Fetch and Async/await</h1>
    </div>
  );
}

export default InterventionWithFetchAsyncAwait;