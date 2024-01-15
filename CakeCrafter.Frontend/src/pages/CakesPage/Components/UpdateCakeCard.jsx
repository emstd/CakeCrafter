import { useNavigate, Form, redirect, useLoaderData } from "react-router-dom";
import{ Box, 
    Button, 
    Text, 
    Input, 
    NumberInput,  
    NumberInputField, 
    NumberInputStepper, 
    NumberIncrementStepper, 
    NumberDecrementStepper 
    } 
from "@chakra-ui/react";


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



function UpdateCakeCard(){
    const cake = useLoaderData();
    const navigate = useNavigate();
    return(
        <Form method="post" id="create-cake-form">

        <Box display='flex' flexDirection='column' width='50%' mt='7vh' ml='10%'>

                <Box display='flex' justifyContent='space-between' alignItems='center'> 
                  <Text>Фотография: </Text>
                  <Box width='100px' height='100px' border='1px solid white' borderRadius='20px' mr='10%'></Box>
                </Box>

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

                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Вкус:</Text>
                    <Input
                        width='50%'
                        type="text"
                        name="tasteId"
                        placeholder="Вкус"
                        defaultValue={cake.tasteId}
                    />
                </Box>

                <Box display='flex' justifyContent='space-between' mt='3vh' alignItems='center'>
                    <Text>Категория:</Text>
                    <Input
                        width='50%'
                        type="text"
                        name="categoryId"
                        placeholder="Категория"
                        defaultValue={cake.categoryId}
                    />
                </Box>

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
