import { useLoaderData, Form } from 'react-router-dom';
import DisplayCategory from './Components/DisplayCategory';
import { Input, Button, Menu, MenuButton, MenuList, MenuItem, Box } from '@chakra-ui/react';
import { AddIcon } from '@chakra-ui/icons';



function CakesCategories() {
    const categories = useLoaderData();

    return (
        <Box>
            <Box>
                    {categories.length ? (categories.map(category => (
                        <DisplayCategory key={category.id} category={category} />
                    )))
                        : (
                            <p>Нет категорий</p>
                        )
                    }
            </Box>
            <Box mt='6vh'>
                <Menu>
                    <MenuButton size='sm' as={Button} rightIcon={<AddIcon />}>
                        Добавить
                    </MenuButton>
                    <MenuList>
                        <Form method='POST' action='/categories/create'>
                            <Input size='md' placeholder='Название категории' name='NewCategory'/>
                            <MenuItem as={Button} type='submit' borderWidth='1px' borderColor='gray' mt='10px'>
                                Добавить
                            </MenuItem>
                        </Form>
                    </MenuList>
                </Menu>
            </Box>
        </Box>
);
}

export default CakesCategories
