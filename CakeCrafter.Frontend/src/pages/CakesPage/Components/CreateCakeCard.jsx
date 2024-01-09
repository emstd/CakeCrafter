import { useNavigate, useParams, Form, redirect } from "react-router-dom";


export async function CreateCake( {params, request} ){
    const formData = await request.formData();
    const newCake = Object.fromEntries(formData);
    const response = await fetch("http://localhost:5000/api/cakes",
                                {
                                    method: 'POST',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify(newCake),
                                });
    console.log(params.categoryId);
    return redirect(`/categories/${params.categoryId}`);
}


function CreateCakeCard(){
    const navigate = useNavigate();
    return(
        <Form method="post" id="create-cake-form">
            <div id='cake-item'>

                    <div id='cake-photo'>
                      Фото
                    </div>

                    <div id='cake-name'>
                    <input
                        type="text"
                        name="name"
                        placeholder="Название"
                    />
                    </div>

                    <div id='cake-desciption'>
                    <input
                        type="text"
                        name="description"
                        placeholder="Описание"
                    />
                    </div>

                    <div id='cake-taste'>
                    <input
                        type="text"
                        name="tasteId"
                        placeholder="Вкус"
                    />
                    </div>

                    <div id='cake-category'>
                    <input
                        type="text"
                        name="categoryId"
                        placeholder="Категория"
                    />
                    </div>

                    <div id='cake-cook-time'>
                    <input
                        type="text"
                        name="cookTimeInMinutes"
                        placeholder="Время приготовления"
                    />
                    </div>

                    <div id='cake-level'>
                    <input
                        type="text"
                        name="level"
                        placeholder="Сложность"
                    />
                    </div>

                    <div id='cake-weight'>
                    <input
                        type="text"
                        name="weight"
                        placeholder="Вес"
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


export default CreateCakeCard
