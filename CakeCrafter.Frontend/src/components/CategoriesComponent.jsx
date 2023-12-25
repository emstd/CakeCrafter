import { Link, useLoaderData } from 'react-router-dom';

export async function GetCategories(){
      const response = await fetch("http://localhost:5000/api/categories");
      const jsonResponse = await response.json();

    return jsonResponse;
}

function CategoriesComponent() {
    const categories = useLoaderData();
    return (
      <div>
            {categories.map(category => (
                  <div key={category.id}>
                    <Link to={`categories/${category.id}`}>
                      <p>{category.name}</p>
                    </Link>
                  </div>
            ))}
      </div>
);
}

export default CategoriesComponent