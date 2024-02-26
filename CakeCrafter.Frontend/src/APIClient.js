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

    GetCategoryNameById = async(cakeId) =>
    {
        const response = await fetch(`http://localhost:5000/api/Categories/${cakeId}`);
        const responseJson = await response.json();
        const categoryName = responseJson.name;
        return categoryName;
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
    };

    UpdateCategory = async({ params, request }) =>
    {
        const formData = await request.formData();
        const newCategoryName = formData.get("CategoryName");
        const response = await fetch(`${this.URL}/api/categories/${params.categoryId}`,
                                    {
                                        method: 'PUT',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify({id: params.categoryId, name: newCategoryName}),
                                    });
        return redirect('/categories');
    };
    
    DeleteCategory = async({ params }) =>
    {
        const response = await fetch(`${this.URL}/api/categories/${params.categoryId}`,
                                    {
                                        method: 'DELETE',
                                    });
        return redirect('/categories');
    };


    /////***** Tastes *****/////

    GetTastes = async () =>
    {
        const response = await fetch(`${this.URL}/api/tastes/`);
        const tastesJson = await response.json();
        return tastesJson;
    };

    CreateTaste = async({ request }) =>
    {
        const formData = await request.formData();
        const newTaste = formData.get("NewTaste");
        const response = await fetch(`${this.URL}/api/tastes`,
                                    {
                                        method: 'POST',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify({name: newTaste}),
                                    });
        return redirect('/tastes');
    };

    UpdateTaste = async({ params, request }) =>
    {
        const formData = await request.formData();
        const newTasteName = formData.get("TasteName");
        const response = await fetch(`${this.URL}/api/tastes/${params.tasteId}`,
                                    {
                                        method: 'PUT',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify({id: params.tasteId, name: newTasteName}),
                                    });
        return redirect('/tastes');
    };

    DeleteTaste = async({ params }) =>
    {
        const response = await fetch(`${this.URL}/api/tastes/${params.tasteId}`,
                                    {
                                        method: 'DELETE',
                                    });
        return redirect('/tastes');
    };


    /////***** Cakes *****/////

    GetCakes = async({ params, request }) =>
    {
        let take = 5;
    
        const url = new URL(request.url);
        const skip = url.searchParams.get("skip");
    
        const response = await fetch(`${this.URL}/api/Cakes?categoryId=${params.categoryId}&skip=${skip ?? 0}&take=${take}`);
        const jsonResponse = await response.json();
    
        return jsonResponse;
    };

    GetCakeById = async( {params} ) =>
    {
        const response = await fetch(`http://localhost:5000/api/Cakes/${params.cakeId}`);
        const jsonResponse = await response.json();

        return jsonResponse;
    };

    CreateCake = async( {params, request} ) =>
    {
        const formData = await request.formData();
        const uploadOption = formData.get('uploadOption');
        let imageRequestId = null;
        
        uploadOption == 0 
                    ? imageRequestId = formData.get('imageIdFile') 
                    : imageRequestId = formData.get('imageIdUrl');
                        
        imageRequestId === "" 
                        ? imageRequestId = null 
                        : imageRequestId = imageRequestId;


        const newCake = { ...Object.fromEntries(formData), imageId: imageRequestId};
        const response = await fetch(`${this.URL}/api/cakes`,
                                    {
                                        method: 'POST',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify(newCake),
                                    });
        return redirect(`/categories/${params.categoryId}`);
    };

    UpdateCake = async( {params, request} ) =>
    {
        const formData = await request.formData();

        let defaultId = formData.get('defaultImageId');     //Получаем ID картинки, которая была, если не было, то null
        defaultId = defaultId === '' ? null : defaultId;    


        let imageRequestId = null;
        
        const uploadOption = formData.get('uploadOption');       //Получаем какой TAB был выбран (File или URL)

        let imageIdFile = formData.get('imageIdFile');              //Смотрим загрузил ли пользователь файл
        imageIdFile = imageIdFile === '' ? null : imageIdFile;  

        let imageIdUrl = formData.get('imageIdUrl');            //Смотрим загрузил ли пользователь картинку по URL
        imageIdUrl = imageIdUrl === '' ? null : imageIdUrl;   

        if(imageIdFile === null && imageIdUrl === null)           //Если пользователь не загрузил изображением ни файлом, ни по URL, то оставляем дефолтный ID картинки, которой был (возможно null если картинки и не было)
        {
            imageRequestId = defaultId;
        }
        else
        {
            if(uploadOption == 0)                               //Если был выбран TAB с файлом, то в запрос пишем ID файла изображения
            {
                imageRequestId = imageIdFile;
            }
            else                                            //В другом случае был выбран TAB с URL, тогда пишем в запрос ID URL-изображения
            {
                imageRequestId = imageIdUrl;
            }
        }

        const updatedCake = { ...Object.fromEntries(formData), imageId: imageRequestId };
        const response = await fetch(`${this.URL}/api/cakes/${params.cakeId}`,
                                    {
                                        method: 'PUT',
                                        headers: { "Content-Type": "application/json" },
                                        body: JSON.stringify(updatedCake),
                                    });
        return redirect(`/categories/${params.categoryId}`);
    };

    DeleteCakeCard = async( {params} ) =>
    {
        const response = await fetch(`${this.URL}/api/cakes/${params.cakeId}`,
                                    {
                                        method: 'DELETE',
                                    });

        return redirect(`/categories/${params.categoryId}`);
    };

    /////***** Image *****/////
    CreateFileImage = async(imageId) =>
    {
        const response = await fetch('http://localhost:5000/api/images/imageUrl', 
        {
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
            },
            body: JSON.stringify(imgURL)
        });
        const image = await response.json();
        return response;
    };



};