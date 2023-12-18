import { useState, useEffect } from 'react'
import './App.css'

function App() {
  const [cake, setCake] = useState(null)

  useEffect(() => {
    fetch("http://localhost:5239/api/cakes/1",{
      method: "GET"
    })
    .then(response => response.json())
    .then(data => {
      setCake(data);
    });
  }, []);
  return (
    <>
      <h2>Cake name:</h2>
      {cake && (
        <div>
          <p>id: {cake.id}</p>
          <p>name: {cake.name}</p>
          <p>description: {cake.description}</p>
          <p>taste id: {cake.tasteId}</p>
          <p>category Id: {cake.categoryId}</p>
          <p>cook Time In Minutes: {cake.cookTimeInMinutes}</p>
          <p>level: {cake.level}</p>
          <p>weight: {cake.weight}</p>
        </div>
      )}
    </>
  );
}

export default App
