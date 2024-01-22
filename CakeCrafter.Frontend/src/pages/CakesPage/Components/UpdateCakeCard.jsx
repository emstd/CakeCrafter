import React, { useState, useEffect } from 'react';
import{ Box, 
        Button, 
        Text, 
        Input, 
        NumberInput,  
        NumberInputField, 
        NumberInputStepper, 
        NumberIncrementStepper, 
        NumberDecrementStepper, 
        Divider
        } 
    from "@chakra-ui/react";

import { useNavigate, Form, redirect, useLoaderData, useParams } from "react-router-dom";
import { GetCategoryNameById } from '../CakesPage';
import { Select } from '@chakra-ui/react';


export async function UpdateCake( {params, request} ){
    const formData = await request.formData();
    const updatedCake = Object.fromEntries(formData);

    const response = await fetch(`http://localhost:5000/api/cakes/${params.cakeId}`,
                                {
                                    method: 'PUT',
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify(updatedCake),
                                });
    return redirect(`/categories/${params.categoryId}`);
}

export async function GetTastes(){
    const response = await fetch(`http://localhost:5000/api/Tastes/`);
    const tastesJson = await response.json();
    return tastesJson;
}
async function GetCategories(){

    const response = await fetch("http://localhost:5000/api/categories");
    const jsonResponse = await response.json();

  return jsonResponse;
}


function UpdateCakeCard(){
    const cake = useLoaderData();
    const navigate = useNavigate();

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

    const [cakesCategories, setCakesCategories] = useState([]);
    useEffect(() => {
        async function fetchGetCategories() {
          const categoriesResponse = await GetCategories();
          setCakesCategories(categoriesResponse);
        }
        fetchGetCategories();
      }, []);    

    return(
        <Form method="post" id="create-cake-form">

        <Box display='flex' flexDirection='column' width='50%' mt='7vh' ml='10%'>

                <Box display='flex' justifyContent='space-between' alignItems='center'> 
                  <Text>Фотография: </Text>
                  
                  <Input
                        width='50%'
                            placeholder="Select Date and Time"
                            size="md"
                            p='0.8vh'
                            type="file"
                        />
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Название:</Text>
                    <Input
                        width='50%'
                        type="text"
                        name="name"
                        placeholder="Название"
                        defaultValue={cake.name}
                    />
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Описание:</Text>
                    <Input
                        size='lg'
                        width='50%'
                        type="text"
                        name="description"
                        placeholder="Описание"
                        defaultValue={cake.description}
                    />
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Вкус:</Text>
                    <Select
                            width='50%'
                            type="text"
                            name="tasteId"
                        >
                            {
                                tastes && tastes.map(taste => 
                                    (
                                        <option key={taste.id} value={taste.id} selected={taste.id===cake.tasteId}>{taste.name}</option>
                                    )
  
                                )
                            }

                    </Select>
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Категория:</Text>
                    <Select
                            width='50%'
                            type='text'
                            name='categoryId'
                        >
                            {
                                cakesCategories && cakesCategories.map(category =>
                                    (
                                        <option key={category.id} value={category.id} selected={category.id==categoryId}>{category.name}</option>
                                    )
                                )
                            }
                    </Select>
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Время приготовления, мин:</Text>
                    <NumberInput
                        defaultValue={cake.cookTimeInMinutes}
                        min={0}
                        max={10000}
                        step={15}
                        width='50%'
                        type="text"
                        name="cookTimeInMinutes"
                        placeholder="Время приготовления"
                    >
                        <NumberInputField />
                        <NumberInputStepper>
                            <NumberIncrementStepper />
                            <NumberDecrementStepper />
                        </NumberInputStepper>
                    </NumberInput>
                </Box>
                <Divider mt='1vh' />
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Сложность:</Text>
                    <Input
                        width='50%'
                        type="text"
                        name="level"
                        placeholder="Сложность"
                        defaultValue={cake.level}
                    />
                </Box>
                <Divider mt='1vh'/>
                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Вес, кг:</Text>

                    <NumberInput
                        defaultValue={cake.weight}
                        min={0.5}
                        max={20}
                        step={0.5}
                        width='50%'
                        type="text"
                        name="weight"
                        placeholder="Вес"
                    >
                        <NumberInputField />
                        <NumberInputStepper>
                            <NumberIncrementStepper />
                            <NumberDecrementStepper />
                        </NumberInputStepper>
                    </NumberInput>
                </Box>
                <Divider mt='1vh'/>
        </Box>

        <Box width='30%' display='flex' justifyContent='space-between' ml='20%' mt='10vh'>
            <Button bgColor='green' type="submit">Сохранить</Button>
            <Button bgColor='red'
                onClick={() => {
                        navigate(-1);
                    }
                }
            >   Отмена  </Button>
        </Box>

    </Form>
    );
}


export default UpdateCakeCard
