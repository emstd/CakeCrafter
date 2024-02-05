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
        Select,
        Divider,
        Tabs,
        TabList,
        Tab,
        TabPanels,
        TabPanel
        } 
    from "@chakra-ui/react";

import { useNavigate, Form, useParams, Link } from "react-router-dom";
import { APIClient } from '../../../APIClient';
import { AddIcon } from '@chakra-ui/icons';


function CreateCakeCard(){
    const api = new APIClient();

    const categoryId = useParams().categoryId;
    const navigate = useNavigate();

    const [tastes, setTastes] = useState([]);
    useEffect(() => {
      async function fetchGetTastes() {
        const tastesResponse = await api.GetTastes();
        setTastes(tastesResponse);
      }
      fetchGetTastes();
    }, []);

    const [cakesCategories, setCakesCategories] = useState([]);
    useEffect(() => {
        async function fetchGetCategories() {
          const categoriesResponse = await api.GetCategories();
          setCakesCategories(categoriesResponse);
        }
        fetchGetCategories();
      }, []);

    const [imageId, setImageId] = useState(null);
    const imageIdHandle = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('image', e.target.files[0]);
        const response = await fetch('http://localhost:5000/api/cakes/image', {
            method: 'POST',
            body: formData,
          });
        const image = await response.json();
        setImageId(image);
    }

    const [imageURL, setImageURL] = useState(null);
    const imageURLHandle = () => {
        console.log("changed")
    }

    return(
        <>
            <Form method='post' id='create-image'>
                <Box display='flex' justifyContent='space-between' width='50%' mt='5vh' ml='10%' alignItems='center'> 
                    <Text>Фотография: </Text>
                    <Tabs width='50%' isFitted>
                        <TabList>
                            <Tab>Файл</Tab>
                            <Tab>URL</Tab>
                        </TabList>

                        <TabPanels>
                            <TabPanel padding='0' pt='2vh'>
                                <Input
                                    name='image'
                                    width='100%'
                                    placeholder="Select Date and Time"
                                    size="md"
                                    p='0.8vh'
                                    type="file"
                                    form="create-image"
                                    onChange={imageIdHandle}
                                />  
                            </TabPanel>
                            <TabPanel padding='0' pt='2vh'>
                                <Input
                                    name='image'
                                    width='100%'
                                    placeholder="Select Date and Time"
                                    size="md"
                                    p='0.8vh'
                                    type="text"
                                    form="create-image"
                                    onChange={imageURLHandle}
                                />
                            </TabPanel>
                        </TabPanels>
                    </Tabs>        
                </Box>
            </Form>
            <Form method="post" id="create-cake-form">
                <Box display='flex' flexDirection='column' width='50%' ml='10%'>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>ImageId:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="imageId"
                            defaultValue={imageId}
                            readonly="readonly"
                        />
                    </Box>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>Название:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="name"
                            placeholder="Название"
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
                        />
                    </Box>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>Вкус:</Text>
                        <Box display='flex' width='50%' justifyContent='space-between'>
                            <Select
                                type="text"
                                name="tasteId"
                            >
                                {
                                    tastes && tastes.map(taste => 
                                        (
                                            <option key={taste.id} value={taste.id}>{taste.name}</option>
                                        )
                                    )
                                }
                            </Select>
                            <Link to='/tastes'><Button ml='1vw'><AddIcon boxSize={3} /></Button></Link>
                        </Box>
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
                            defaultValue={60}
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
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>Сложность:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="level"
                            placeholder="Сложность"
                        />
                    </Box>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>Вес, кг:</Text>
                        <NumberInput
                            defaultValue={1}
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
        </>);
}


export default CreateCakeCard
