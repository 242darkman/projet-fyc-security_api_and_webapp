import React, { useEffect, useState } from 'react';

import axios from 'axios';

function InterventionWithAxios() {
  const [interventions, setInterventions] = useState(null);

  useEffect(() => {
    const fetchInterventions = async () => {
      try {
        const response = await axios.get('http://localhost:5230/api/Intervention');
        setInterventions(response.data);
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
      <h1>Intervention With Axios</h1>
    </div>
  );
}

export default InterventionWithAxios;