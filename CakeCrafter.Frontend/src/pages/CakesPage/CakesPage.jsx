import './CakesPage.css'
import React, { useState, useEffect } from 'react';
import { useLoaderData, useParams, Link, Form } from "react-router-dom";
import { Box, Button, Image, Text, Card, Stack, CardBody, Heading, CardFooter, CardHeader, Flex } from '@chakra-ui/react';
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
        width='80%'
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
        flexDirection='row'
        justifyContent='space-between'
        width='80%'
        flexWrap='wrap'
        mt='3vh'
      >
            {cakes.items && cakes.items.map(item => (
              <>
                <Card
                direction={{ base: 'column', sm: 'column', md: 'row' }}
                overflow='hidden'
                variant='outline'
                mt='3vh'
                width='100%'
              >
                 <Image
                    objectFit='cover'
                    maxW={{ base: '100%', sm: '100%', md: '30%' }}
                    src='https://o-tendencii.com/uploads/posts/2022-02/1645679812_20-o-tendencii-com-p-tort-na-svadbu-odnoyarusnii-kremovii-foto-20.jpg'
                    alt='Caffe Latte'
                  />
                  <Stack>
                    <Heading size='lg' textAlign='center'>{item.name}</Heading>
                    <Text textAlign='center' mt='5vh' fontSize='xl'>{item.description}</Text>

                    <CardBody>

                      <p>Вкус: {tastes.length && tastes.find(taste => taste.id == item.tasteId).name}</p>
                                  
                      <p>Категория: {item.categoryId}</p>
      
                      <p>Время приготовления: {item.cookTimeInMinutes} минут</p>
      
                      <p>Сложность: {item.level}</p>
      
                      <p>Вес: {item.weight}кг</p>

                    </CardBody>

                    <CardFooter>

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
