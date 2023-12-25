import { useLoaderData } from "react-router-dom";

export async function GetCakes({ params }){

    const response = await fetch(`http://localhost:5000/api/Cakes?categoryId=${params.categoryId}&skip=0&take=5`);
    const jsonResponse = await response.json();

  return jsonResponse;
}

function CakesPage() {
  const cake = useLoaderData();
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