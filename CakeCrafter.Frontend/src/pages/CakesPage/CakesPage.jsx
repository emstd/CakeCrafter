import './CakesPage.css'
import React, { useState, useEffect } from 'react';
import { useLoaderData, useParams, Link, Form } from "react-router-dom";
import { Box, Button, Image, Text, Card, Stack, CardBody, Heading, CardFooter, CardHeader, Flex, Divider } from '@chakra-ui/react';
import { ChevronLeftIcon, ChevronRightIcon, DeleteIcon, EditIcon, SmallAddIcon, TimeIcon } from '@chakra-ui/icons';
import { GetTastes } from './Components/CreateCakeCard';
import { LiaGrinTongueSquint } from "react-icons/lia";
import { MdOutlineScale } from "react-icons/md";

export async function GetCakes({ params, request }){
    let take = 5;

    const url = new URL(request.url);
    const skip = url.searchParams.get("skip");

    const response = await fetch(`http://localhost:5000/api/Cakes?categoryId=${params.categoryId}&skip=${skip ?? 0}&take=${take}`);
    const jsonResponse = await response.json();

  return jsonResponse;
}

export async function GetCakeById( {params} ){

    const response = await fetch(`http://localhost:5000/api/Cakes/${params.cakeId}`);
    const jsonResponse = await response.json();

  return jsonResponse;
}


export async function GetCategoryNameById(cakeId){
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

  const CakesOnPage = 5;
  let Pages = 0;
  let PagesArray = [];
  let startPage = -5;
  if (cakes.totalItems > 5)
  {
    Pages = Math.ceil(cakes.totalItems/CakesOnPage);
    PagesArray = Array.from({ length: Pages }, (_, index) => index + 1);
  }

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
                <Card
                key={item.id}
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

                  <Stack width='100%'>

                    <CardHeader>
                      <Flex justifyContent='space-around'>
                        <Heading flex='1' size='xl' textAlign='center'>{item.name}</Heading>
 
                        <Form
                          method="get"
                          action={`/categories/${categoryId}/cake/update/${item.id}`}
                        >
                          <Button size='sm' type="submit" mt='1vh'><EditIcon /></Button>
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
                            <Button size='sm' ml='2vh' mt='1vh' type="submit"><DeleteIcon color='red' /></Button>
                          </Form>
                      </Flex>
                    </CardHeader>

                    <Divider />

                    <CardBody>
                      <Text mt='1vh' fontSize='xl'>{item.description} Lorem ipsum dolor sit, amet consectetur adipisicing elit. Aliquam fugit dolore sit ipsa, ipsam facilis obcaecati sapiente cumque, porro illum temporibus et reiciendis esse odio optio hic architecto, rem culpa?</Text>
                    </CardBody>

                    <Divider />

                    <CardFooter>
                      <Flex justifyContent='space-around' w='100%' flexWrap='wrap'>
                        <Text>{categoryName}</Text>
                        <Flex alignItems='center'><LiaGrinTongueSquint style={{fontSize:'3vh'}}/><Text ml='0.5vw'>{tastes.length && tastes.find(taste => taste.id == item.tasteId).name}</Text></Flex>
                        <Text><TimeIcon mb='0.5vh' mr='0.5vw' />{item.cookTimeInMinutes} минут</Text>
                        <Text>Сложность: {item.level}</Text>
                        <Flex alignItems='center'><MdOutlineScale /><Text ml='0.5vw'>{item.weight}кг</Text></Flex>
                      </Flex>
                    </CardFooter>

                  </Stack>
                </Card>
                ))
            }
        <Box display='flex' justifyContent='space-around' width='30%' mt='10vh' ml='35%'>
          {
            PagesArray.length > 1 && 
            <Button as='a' href={`/categories/${categoryId}?skip=0&take=5`}><ChevronLeftIcon /></Button>
          }

          {
            PagesArray.length > 1 && 
            PagesArray.map((page) => 
              <Link to={`/categories/${categoryId}?skip=${(page-1)*5}&take=5`} key={page}><Button>{page}</Button></Link>
            )
          }

          {
            PagesArray.length > 1 && 
            <Button as='a' href={`/categories/${categoryId}?skip=${(PagesArray.length-1)*5}&take=5`}><ChevronRightIcon /></Button>
          }
        </Box>
      </Box>
    </>
);
}

export default CakesPage
