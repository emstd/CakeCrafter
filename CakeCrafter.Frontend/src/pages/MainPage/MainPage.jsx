import { useState, useEffect } from 'react';
import { Link, Outlet } from 'react-router-dom';

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
    <>
      <div>
            {Object.keys(categories).map(categoryKey => (
                  <div key={categoryKey}>
                    <Link to="categories/1">
                      <p>{categories[categoryKey].name}</p>
                    </Link>
                  </div>
            ))}
      </div>
      <div>
        <Outlet />
      </div>
    </>
);
}

export default MainPage