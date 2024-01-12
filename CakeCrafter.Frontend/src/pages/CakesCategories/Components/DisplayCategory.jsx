import { useState } from "react";
import { Link, Form } from "react-router-dom";
import { 
    DeleteIcon,
    EditIcon
} from "@chakra-ui/icons";
import { Box, Flex, Input } from "@chakra-ui/react";

function DisplayCategory( {category} ){

const [isEdit, setIsEdit] = useState(false);
    return(
        <Box display='flex'>
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
        
                            <Input size='xs' type='text' name="CategoryName"/>
                            <button type="submit">Save</button>
                            </Form>
                        ) 
                        : 
                        (
                            <Link to={`/categories/${category.id}`}>
                                {category.name}
                            </Link>
                        )
                    }

                    <button type='button' onClick={() => setIsEdit(!isEdit)}><EditIcon /></button>

                    <Form
                        method="post"
                        action={`/categories/delete/${category.id}`}
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
                        <button type="submit"><DeleteIcon /></button>
                    </Form>
        </Box>
    );
}

export default DisplayCategory;
