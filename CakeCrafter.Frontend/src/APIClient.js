import { redirect } from "react-router-dom";

export class APIClient
{
    URL = "http://localhost:5000";


    /////***** Cakes Categories *****/////

    GetCategories = async () =>
    {
        const response = await fetch(`${this.URL}/api/categories`);
        const jsonResponse = await response.json();
        return jsonResponse;
    };

    CreateCategory = async({ request }) =>
    {
        const formData = await request.formData();
        const newCategory = formData.get("NewCategory");
        const response = await fetch(`${this.URL}/api/categories`,
                                    {
                                        method: 'POST',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify({name: newCategory}),
                                    });
        return redirect('/categories');
    }

    UpdateCategory = async({ params, request }) =>
    {
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
    
    DeleteCategory = async({ params }) =>
    {
        const response = await fetch(`http://localhost:5000/api/categories/${params.categoryId}`,
                                    {
                                        method: 'DELETE',
                                    });
        return redirect('/categories');
    }
};