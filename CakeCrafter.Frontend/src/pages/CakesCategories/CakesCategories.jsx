import { Link, useLoaderData, Form, redirect } from 'react-router-dom';

export async function GetCategories(){

      const response = await fetch("http://localhost:5000/api/categories");
      const jsonResponse = await response.json();

    return jsonResponse;
}

export async function CreateCategory({ request }){
    const formData = await request.formData();
    const newCategory = formData.get("NewCategory");
    const response = await fetch("http://localhost:5000/api/categories",
                                {
                                    method: 'POST',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify({name: newCategory}),
                                });
    return redirect('/categories');
}

export async function DeleteCategory({ params }){
    const response = await fetch(`http://localhost:5000/api/categories/${params.categoryId}`,
                                {
                                    method: 'DELETE',
                                });
    return redirect('/categories');
}

function CakesCategories() {
    const categories = useLoaderData();

    return (
        <div>
            <div>
                    {categories.length ? (categories.map(category => (
                        <div key={category.id}>
                            <Link to={`/categories/${category.id}`}>
                            <p>{category.name}</p>
                            </Link>

                            <Form
                                    method="post"
                                    action={`/categories/delete/${category.id}`}
                                    onSubmit={(event) => {
                                    if (
                                    !confirm(
                                        "Please confirm you want to delete this record."
                                        )
                                    ) {
                                        event.preventDefault();
                                        }
                                    }}
                                    >
                                    <button type="submit">Delete</button>
                            </Form>

                        </div>
                    )))
                        : (
                            <p>Нет категорий</p>
                        )
                    }
            </div>
            <div>
                <Form method='POST' action='/categories/create'>
                        <input type='search' name='NewCategory'/>
                        <button type='submit'>Добавить</button>
                </Form>
            </div>
        </div>
);
}

export default CakesCategories
