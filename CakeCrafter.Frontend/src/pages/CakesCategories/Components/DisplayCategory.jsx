import { useState } from "react";
import { Link, Form } from "react-router-dom";
import { 
    DeleteIcon,
    EditIcon
} from "@chakra-ui/icons";
import { Box, Button, Text, Input } from "@chakra-ui/react";

function DisplayCategory( {category} ){

const [isEdit, setIsEdit] = useState(false);
    return(
        <Box display='flex' justifyContent='space-between' width='40%' mb='4vh'>
            {
                isEdit ? (
                    <Form
                        method="post"
                        action={`/categories/update/${category.id}`}
                        onSubmit={(event) => {
                                
                        setIsEdit(false);

                        if (
                            !confirm(
                                "Please confirm you want to update this record."
                            )
                        ) {
                            event.preventDefault();
                        }
                        }}
                    >
        
                        <Input
                            defaultValue={category.name}
                            fontSize='2xl' 
                            width='90%'
                            size='sm'
                            mt='0.5vh' 
                            type='text' 
                            name="CategoryName"
                        />

                        <Button mt='2vh' type="submit">Сохранить</Button>
                        <Button onClick={() => setIsEdit(false)} mt='2vh' ml='2vw'>Отмена</Button>
                            
                    </Form>
                ) 
                : 
                (
                    <Link to={`/categories/${category.id}`}>
                        <Text fontSize='2xl'>{category.name}</Text>
                    </Link>
                )
            }

            <Box display='flex' minWidth='40%' justifyContent='space-between'>
                <Button type='button' onClick={() => setIsEdit(!isEdit)}><EditIcon /></Button>

                <Form
                    method="post"
                    action={`/categories/delete/${category.id}`}
                    onSubmit={(event) => {
                    if (
                        !confirm(
                            "Please confirm you want to delete this record."
                        )
                    ) 
                    {
                        event.preventDefault();
                    }
                    }}
                >
                    <Button type="submit"><DeleteIcon color='red' /></Button>
                </Form>

            </Box>
        </Box>
    );
}

export default DisplayCategory;
