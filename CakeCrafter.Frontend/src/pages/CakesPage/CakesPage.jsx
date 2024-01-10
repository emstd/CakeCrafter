import './CakesPage.css'
import { useLoaderData, useParams, Link, Form } from "react-router-dom";
import { Button } from '@chakra-ui/react';
import { ChevronLeftIcon } from '@chakra-ui/icons';

export async function GetCakes({ params }){

    const response = await fetch(`http://localhost:5000/api/Cakes?categoryId=${params.categoryId}&skip=0&take=5`);
    const jsonResponse = await response.json();

  return jsonResponse;
}

export async function GetCakeById( {params} ){

    const response = await fetch(`http://localhost:5000/api/Cakes/${params.cakeId}`);
    const jsonResponse = await response.json();

  return jsonResponse;
}

function CakesPage() {
  const cake = useLoaderData();
  const categoryId = useParams().categoryId;
  return (
    <>
      <Link to='/categories'><Button><ChevronLeftIcon />Назад</Button></Link>
      <div id='category-name'>
        
      </div>
      <div>
        <Link to={`/categories/${categoryId}/cake/create`}>Создать новую карточку</Link>
      </div>
      <div id='cakes-container'>
            {cake.items && cake.items.map(item => (
                <div id='cake-item' key={item.id}>

                    <div id='cake-photo'>
                      Фото
                    </div>

                    <div id='cake-name'>
                      {item.name}
                    </div>

                    <div id='cake-edit'>
                    <Form
                        method="get"
                        action={`/categories/${categoryId}/cake/update/${item.id}`}
                    >
                        <button type="submit">Ред.</button>
                    </Form>
                      <Form
                        method="post"
                        action={`/categories/${categoryId}/cake/delete/${item.id}`}
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
                        <button type="submit">Удалить</button>
                    </Form>
                    </div>

                    <div id='cake-desciption'>
                      <p>{item.description}</p>
                    </div>

                    <div id='cake-taste'>
                      <p>Вкус: {item.tasteId}</p>
                    </div>

                    <div id='cake-category'>
                      <p>Категория: {item.categoryId}</p>
                    </div>

                    <div id='cake-cook-time'>
                      <p>Время приготовления: {item.cookTimeInMinutes} минут</p>
                    </div>

                    <div id='cake-level'>
                      <p>Сложность: {item.level}</p>
                    </div>

                    <div id='cake-weight'>
                      <p>Вес: {item.weight}кг</p>
                    </div>

                </div>))
            }
      </div>
    </>
);
}

export default CakesPage
