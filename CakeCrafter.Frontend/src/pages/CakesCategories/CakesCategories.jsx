import { useLoaderData, Form, redirect } from 'react-router-dom';
import DisplayCategory from './Components/DisplayCategory';
import { Input, Button, Menu, MenuButton, MenuList, MenuItem } from '@chakra-ui/react';
import { AddIcon } from '@chakra-ui/icons';

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

export async function UpdateCategory({ params, request }){
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
            <Menu>
                <MenuButton size='sm' as={Button} rightIcon={<AddIcon />}>
                    Добавить
                </MenuButton>
                <MenuList>
                    <Form method='POST' action='/categories/create'>
                        <Input size='md' placeholder='Название категории' name='NewCategory'/>
                        <MenuItem mt='10px'>
                            <Button width='100%' size='sm' type='submit'>Добавить</Button>
                        </MenuItem>
                    </Form>
                </MenuList>
            </Menu>

                {/* <Form method='POST' action='/categories/create'>
                    <Button size='sm' type='submit'>Добавить</Button>
                    <Input size='sm' placeholder='Название категории' name='NewCategory'/>
                </Form> */}
            </div>
        </div>
);
}

export default CakesCategories
