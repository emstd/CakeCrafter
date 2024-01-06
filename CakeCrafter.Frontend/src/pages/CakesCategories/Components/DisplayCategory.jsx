import { useState } from "react";
import { Link, Form } from "react-router-dom";


function DisplayCategory( {category} ){

const [isEdit, setIsEdit] = useState(false);
    return(
    <div>
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
        
                            <input type='text' name="CategoryName"/>
                            <button type="submit">Save</button>
                            </Form>
                        ) 
                        : 
                        (
                            <Link to={`/categories/${category.id}`}>
                                <p>{category.name}</p>
                            </Link>
                        )
                    }

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
                        <button type="submit">Delete</button>
                    </Form>

                    <button type='button' onClick={() => setIsEdit(!isEdit)}>Edit</button>
        </div>
    );
}

export default DisplayCategory;
