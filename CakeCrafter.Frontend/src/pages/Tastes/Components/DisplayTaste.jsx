import { Box, Button, Text, Input} from "@chakra-ui/react";
import { EditIcon, DeleteIcon } from "@chakra-ui/icons";
import { useState } from "react";
import { Form } from "react-router-dom";

export default function DisplayTaste( {taste} ){

  const [isEdit, setIsEdit] = useState(false);

  return(
    <Box display='flex' justifyContent='space-between' width='40%' mb='4vh'>
      {
        isEdit ?
        (
          <Form
            method="post"
            action={`/tastes/update/${taste.id}`}
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
              defaultValue={taste.name}
              fontSize='2xl' 
              width='90%'
              size='sm'
              mt='0.5vh' 
              type='text' 
              name="TasteName"
            />

            <Button mt='2vh' type="submit">Сохранить</Button>
            <Button onClick={() => setIsEdit(false)} mt='2vh' ml='2vw'>Отмена</Button>

          </Form>
        )
        :
        (
                <Text fontSize='2xl'>{taste.name}</Text>
        )
      }

      <Box display='flex' minWidth='40%' justifyContent='flex-end'>
        <Button mr='2vw' type='button' onClick={() => setIsEdit(!isEdit)}><EditIcon /></Button>

        <Form
          method="post"
          action={`/tastes/delete/${taste.id}`}
          onSubmit={(event) => {
          if (!confirm("Please confirm you want to delete this record.")) 
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