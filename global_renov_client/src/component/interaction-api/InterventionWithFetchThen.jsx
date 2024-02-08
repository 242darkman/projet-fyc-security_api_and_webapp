import React, { useEffect, useState } from 'react';

function InterventionWithFetchThen() {
  const [interventions, setInterventions] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5230/api/Intervention')
      .then((response) => response.json())
      .then((data) => setInterventions(data)) 
      .catch((error) => console.error("Erreur lors du fetching:", error));
  }, []);

  return (
    <div>
      <h1> Intervention With Fetch Then </h1>
    </div>
  );
}

export default InterventionWithFetchThen;
