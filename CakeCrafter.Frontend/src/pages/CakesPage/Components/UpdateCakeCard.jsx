import { useNavigate, Form, redirect, useLoaderData } from "react-router-dom";


export async function UpdateCake( {request, params} ){
    const formData = await request.formData();
    const updatedCake = Object.fromEntries(formData);
    updatedCake.id = params.cakeId;

    const response = await fetch(`http://localhost:5000/api/cakes/`,
                                {
                                    method: 'PUT',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify(updatedCake),
                                });
    return redirect('/categories');
}



function UpdateCakeCard(){
    const cake = useLoaderData();
    const navigate = useNavigate();
    return(
        <Form method="post" id="update-cake-form">
            <div id='cake-item'>

                    <div id='cake-photo'>
                      Фото
                    </div>


                    <div id='cake-name'>
                    <input
                        type="text"
                        name="name"
                        placeholder="Название"
                        defaultValue={cake.name}
                    />
                    </div>

                    <div id='cake-desciption'>
                    <input
                        type="text"
                        name="description"
                        placeholder="Описание"
                        defaultValue={cake.description}
                    />
                    </div>

                    <div id='cake-taste'>
                    <input
                        type="text"
                        name="tasteId"
                        placeholder="Вкус"
                        defaultValue={cake.tasteId}
                    />
                    </div>

                    <div id='cake-category'>
                    <input
                        type="text"
                        name="categoryId"
                        placeholder="Категория"
                        defaultValue={cake.categoryId}
                    />
                    </div>

                    <div id='cake-cook-time'>
                    <input
                        type="text"
                        name="cookTimeInMinutes"
                        placeholder="Время приготовления"
                        defaultValue={cake.cookTimeInMinutes}
                    />
                    </div>

                    <div id='cake-level'>
                    <input
                        type="text"
                        name="level"
                        placeholder="Сложность"
                        defaultValue={cake.level}
                    />
                    </div>

                    <div id='cake-weight'>
                    <input
                        type="text"
                        name="weight"
                        placeholder="Вес"
                        defaultValue={cake.weight}
                    />
                    </div>
            </div>


            <button type="submit">Сохранить</button>
            <button 
                type="button"
                onClick={() => {
                        navigate(-1);
                    }
                }
            >   Отмена  </button>
        </Form>
    );
}


export default UpdateCakeCard
