import { useState, useEffect } from 'react';

function CakesPage() {
  const [cake, setCake] = useState([])

  const getApiData = async() => {
    const response = await fetch("http://localhost:5000/api/Cakes?categoryId=&skip=0&take=5").then(response => response.json());
    setCake(response);
  }

  useEffect(() => {
    getApiData();
  }, []);


  return (
    <div>
          {cake.items && cake.items.map(item => (
              <div key={item.id}>
                <p>id: {item.id}</p>
                <p>name: {item.name}</p>
                <p>description: {item.description}</p>
                <p>taste id: {item.tasteId}</p>
                <p>category Id: {item.categoryId}</p>
                <p>cook Time In Minutes: {item.cookTimeInMinutes}</p>
                <p>level: {item.level}</p>
                <p>weight: {item.weight}</p>
              </div>))
          }
    </div>
);
}

export default CakesPage