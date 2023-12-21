import { useState, useEffect } from 'react';

function MainPage() {
  const [categories, setCategories] = useState([])

  const getApiData = async() => {
    const response = await fetch("http://localhost:5000/api/categories").then(response => response.json());
    setCategories(response);
  }

  useEffect(() => {
    getApiData();
  }, []);


  return (
    <div>
          {Object.keys(categories).map(categoryKey => (
                <div key={categoryKey}>
                    <p>{categories[categoryKey].name}</p>
                </div>
          ))}
    </div>
);
}

export default MainPage