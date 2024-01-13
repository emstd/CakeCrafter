import './CakesPage.css'
import React, { useState, useEffect } from 'react';
import { useLoaderData, useParams, Link, Form } from "react-router-dom";
import { Box, Button, Image, Text, Card, Stack, CardBody, Heading, CardFooter } from '@chakra-ui/react';
import { ChevronLeftIcon, DeleteIcon, EditIcon, SmallAddIcon } from '@chakra-ui/icons';
import { GetTastes } from './Components/CreateCakeCard';

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


async function GetCategoryNameById(cakeId){
  const response = await fetch(`http://localhost:5000/api/Categories/${cakeId}`);
  const responseJson = await response.json();
  const categoryName = responseJson.name;
  return categoryName;
}




function CakesPage() {
  const cakes = useLoaderData();
  const categoryId = useParams().categoryId;

  const [categoryName, setCategoryName] = useState('');
  useEffect(() => {
    async function fetchCategoryName() {
      const name = await GetCategoryNameById(categoryId);
      setCategoryName(name);
    }
    fetchCategoryName();
  }, []);

  const [tastes, setTastes] = useState([]);
  useEffect(() => {
    async function fetchGetTastes() {
      const tastesResponse = await GetTastes();
      setTastes(tastesResponse);
    }
    fetchGetTastes();
  }, []);

  return (
    <>
      <Box
        display='flex'
        justifyContent='space-between'
        width='65%'
        alignItems='center'
      >
        <Link to='/categories'><Button><ChevronLeftIcon mt={'3px'} mr={'2px'}/>Назад</Button></Link>

        <Box>
          <Text fontSize='3xl'>{categoryName ? categoryName : ' '}</Text>
        </Box>
        <Box>
          <Link to={`/categories/${categoryId}/cake/create`}><Button><SmallAddIcon mt='0.5vh' />Создать карту</Button></Link>
        </Box>

      </Box>

      <Box
        display='flex'
        flexDirection='column'
        justifyContent='space-between'
        width='65%'
        mt='3vh'
      >
            {cakes.items && cakes.items.map(item => (
              <>
                <Card
                direction={{ base: 'column', sm: 'row' }}
                overflow='hidden'
                variant='outline'
              >
                <Image
                  objectFit='cover'
                  maxW={{ base: '100%', sm: '200px' }}
                  src='https://mykaleidoscope.ru/uploads/posts/2022-11/1669403152_mykaleidoscope-ru-p-tort-s-yagodami-vkontakte-4.jpg'
                  alt='Caffe Latte'
                />
  
                <Stack>
                  <CardBody>
                    <Heading size='md'>The perfect latte</Heading>
  
                    <Box                                                                  
                    display='flex'
                    justifyContent='space-between'
                    >                                                     
                
                        
                      
  
                      <Box width='50%' alignSelf='center'>
                        <Text fontSize='4xl' textAlign='center'>{item.name}</Text>
                      </Box>
  
                      <Box display='flex' mt='1vh' mr='1vh'>
                        <Form
                          method="get"
                          action={`/categories/${categoryId}/cake/update/${item.id}`}
                        >
                          <Button size='sm' type="submit"><EditIcon /></Button>
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
                          <Button size='sm' ml='2vh' type="submit"><DeleteIcon color='red' /></Button>
                        </Form>
                      </Box>
                    </Box>
                                                                                                {/* 2 строка */}
                    <Box mt='2vh' width='100%' textAlign='center'>                                                             
                      <Text fontSize='xl'>{item.description}</Text>
                    </Box>
                                                                                                {/* 3 строка */}
                    <Box
                    mt='4vh'
                    display='flex'
                    justifyContent='space-around'
                    flexWrap='wrap'
                    >    
                      <p>Вкус: {tastes.length && tastes.find(taste => taste.id == item.tasteId).name}</p>
                              
                      <p>Категория: {item.categoryId}</p>
  
                      <p>Время приготовления: {item.cookTimeInMinutes} минут</p>
  
                      <p>Сложность: {item.level}</p>
  
                      <p>Вес: {item.weight}кг</p>
  
                    </Box>
  
                  
                  </CardBody>
  
                  <CardFooter>
                    <Button variant='solid' colorScheme='blue'>
                      Buy Latte
                    </Button>
                  </CardFooter>
                </Stack>
              </Card>

            </>
                ))
            }
      </Box>
    </>
);
}

export default CakesPage
