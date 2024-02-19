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
        const resultId = formData.get('imageResult');
        let imageId = formData.get('imageId');
        imageId = imageId === "" ? null : imageId;
        let imageURL = formData.get('imageURL');
        imageURL = imageURL === "" ? null : imageURL;

        let ImageRequestId = null;

        if(resultId == 0)
        {
            ImageRequestId = imageId;
        }
        else
        {
            ImageRequestId = imageURL;
        }
        
        console.log(resultId);
        console.log(ImageRequestId);

        const newCake = { ...Object.fromEntries(formData), imageId: ImageRequestId};
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

        const resultId = formData.get('imageResult');       //Получаем какой TAB был выбран (File или URL)

        let imageId = formData.get('imageId');              //Смотрим загрузил ли пользователь файл
        imageId = imageId === '' ? null : imageId;  

        let imageURL = formData.get('imageURL');            //Смотрим загрузил ли пользователь картинку по URL
        imageURL = imageURL === '' ? null : imageURL;   

        let ImageRequestId = null;                          //инициализация Id картинки, который будет записан в итоге в запрос

        if(imageId === null && imageURL === null)           //Если пользователь не загрузил изображением ни файлом, ни по URL, то оставляем дефолтный ID картинки, которой был (возможно null если картинки и не было)
        {
            ImageRequestId = defaultId;
        }
        else
        {
            if(resultId == 0)                               //Если был выбран TAB с файлом, то в запрос пишем ID файла изображения
            {
                ImageRequestId = imageId;
            }
            else                                            //В другом случае был выбран TAB с URL, тогда пишем в запрос ID URL-изображения
            {
                ImageRequestId = imageURL;
            }
        }

        const updatedCake = { ...Object.fromEntries(formData), imageId: ImageRequestId };
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
    CreateImage = async( {request} ) =>
    {
        const formData = await request.formData();
        const newImage = Object.fromEntries(formData);
        const response = await fetch(`${this.URL}/api/cakes/image`,
                                    {
                                        method: 'POST',
                                        body: newImage,
                                    });
        return response;
    };
};