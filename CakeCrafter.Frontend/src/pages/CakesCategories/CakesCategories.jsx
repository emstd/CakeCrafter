import { useLoaderData, Form, redirect } from 'react-router-dom';
import DisplayCategory from './Components/DisplayCategory';

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

export async function UpdateCategory({ params, request }){ //Почему это работает, даже если написать { request, params }
    const formData = await request.formData();
    const newCategoryName = formData.get("CategoryName");
    const response = await fetch(`http://localhost:5000/api/categories/${params.categoryId}`,
                                {
                                    method: 'PUT',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify({id: params.categoryId, name: newCategoryName}),
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
                        <DisplayCategory key={category.id} category={category} />
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
