import { redirect } from "react-router-dom";

export default async function DeleteCakeCard( {params} ){
        const response = await fetch(`http://localhost:5000/api/cakes/${params.cakeId}`,
                                    {
                                        method: 'DELETE',
                                    });

        return redirect('/categories');
}
