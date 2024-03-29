import { useLoaderData, Form, useNavigate } from "react-router-dom";
import { Input, Button, Menu, MenuButton, MenuList, MenuItem, Box } from '@chakra-ui/react';
import { AddIcon, ChevronLeftIcon } from '@chakra-ui/icons';
import DisplayTaste from "./Components/DisplayTaste";

export default function Tastes(){
  const tastes = useLoaderData();
  const navigate = useNavigate();
  return(
    <Box>
      <Box>
        <Button onClick={() => {navigate(-1)}}><ChevronLeftIcon mt={'3px'} mr={'2px'} />Назад</Button>
      </Box>
      <Box mt='6vh'>
            {tastes.length ? (tastes.map(taste => (
                <DisplayTaste key={taste.id} taste={taste} />
            )))
                : (
                    <p>Нет вкусов</p>
                )
            }
      </Box>

      <Box mt='6vh'>
        <Menu>
          <MenuButton size='sm' as={Button} rightIcon={<AddIcon />}>
            Добавить
          </MenuButton>
          <MenuList>
            <Form method='POST' action='/tastes/create'>
              <Input size='md' placeholder='Название вкуса' name='NewTaste'/>
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