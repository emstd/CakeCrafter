import { Link, useLoaderData, Form } from 'react-router-dom';

export async function GetCategories(){

      const response = await fetch("http://localhost:5000/api/categories");
      const jsonResponse = await response.json();

    return jsonResponse;
}

export async function CreateCategory(category){
    const response = await fetch("http://localhost:5000/api/categories",
                                {
                                    method: 'POST',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify({name: "string"}),
                                });
    return response;
}

function CategoriesComponent() {
    const categories = useLoaderData();

    return (
        <div>
            <div>
                    <p><Link to='/'>Назад </Link></p>
                    {categories.length ? (categories.map(category => (
                        <div key={category.id}>
                            <Link to={`categories/${category.id}`}>
                            <p>{category.name}</p>
                            </Link>
                        </div>
                    )))
                        : (
                            <p>No Categories</p>
                        )
                    }
            </div>
            <div>
                <Form
                    method='POST'
                    action='/categories'>

                        <input type='search' />
                        <button type='submit'>Добавить</button>

                </Form>
            </div>
        </div>
);
}

export default CategoriesComponent