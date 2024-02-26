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
        Divider,
        Tabs,
        TabList,
        Tab,
        TabPanels,
        TabPanel
        } 
    from "@chakra-ui/react";

import { useNavigate, Form, useLoaderData, useParams, Link } from "react-router-dom";
import { Select } from '@chakra-ui/react';
import { APIClient } from '../../../APIClient';
import { AddIcon } from '@chakra-ui/icons';


function UpdateCakeCard(){
    const api = new APIClient();

    const cake = useLoaderData();
    const navigate = useNavigate();

    const categoryId = useParams().categoryId;

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

    
    const [uploadOption, setUploadOption] = useState(0);

    const [imageIdFile, setImageIdFile] = useState(null);
    const imageIdFileHandle = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('image', e.target.files[0]);
        const response = await fetch('http://localhost:5000/api/images/imageFile', {
            method: 'POST',
            body: formData,
          });
        const image = await response.json();
        setImageIdFile(image);
    }

    const [imageIdUrl, setImageIdUrl] = useState(null);
    const imageIdUrlHandle = async(e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        const imgURL = formData.get('imageUrl');
        const response = await fetch('http://localhost:5000/api/images/imageUrl', 
        {
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
            },
            body: JSON.stringify(imgURL)
        });
        const image = await response.json();
        setImageIdUrl(image);
    }

    return(
        <>
            <Box display='flex' justifyContent='space-between' width='50%' mt='5vh' ml='10%' alignItems='center'> 
                <Text>Фотография: </Text>
                <Tabs width='50%' isFitted onChange={(index) => setUploadOption(index)}>
                    <TabList>
                        <Tab>Файл</Tab>
                        <Tab>URL</Tab>
                    </TabList>
                    <TabPanels>
                        <TabPanel padding='0' pt='2vh'>
                            <Input
                                name='image'
                                width='100%'
                                placeholder="Select file"
                                size="md"
                                p='0.5vh'
                                type="file"
                                onChange={imageIdFileHandle}
                            />  
                        </TabPanel>
                        <TabPanel padding='0' pt='2vh'>
                            <Form 
                                method='post' 
                                action='imageUrl' 
                                onSubmit={imageIdUrlHandle}
                                id='create-img'
                            >
                                <Input
                                    name='imageUrl'
                                    width='100%'
                                    placeholder="Введите URL"
                                    size="md"
                                    p='0.8vh'
                                    type="text"
                                    form="create-img"
                                />
                                <Box display='flex' justifyContent='center' width='100%'>
                                    <Button type='submit' mt='2vh'>Загрузить</Button>
                                </Box>
                            </Form>
                        </TabPanel>
                    </TabPanels>
                </Tabs>                    
            </Box>

            <Form method="post" id="create-cake-form">
                <Box display='flex' flexDirection='column' width='50%' ml='10%'>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>ImageId:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="imageIdFile"
                            defaultValue={imageIdFile}
                            value={imageIdFile}
                            readOnly="readonly"
                        />
                    </Box>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>ImageURL:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="imageIdUrl"
                            defaultValue={imageIdUrl}
                            value={imageIdUrl}
                            readOnly="readonly"
                        />
                    </Box>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>uploadOption:</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="uploadOption"
                            Value={uploadOption}
                            readOnly="readonly"
                        />
                    </Box>
                    <Divider mt='1vh'/>
                    <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                        <Text>defaultImageId</Text>
                        <Input
                            width='50%'
                            type="text"
                            name="defaultImageId"
                            Value={cake.imageId}
                            readOnly="readonly"
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
                        <Box display='flex' width='50%' justifyContent='space-between'>
                            <Select
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
    </>
    );
}


export default UpdateCakeCard
