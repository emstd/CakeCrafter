import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { CreateCake } from './pages/CakesPage/Components/CreateCakeCard';
import { GetCakes, GetCakeById } from './pages/CakesPage/CakesPage';
import CakesCategories from './pages/CakesCategories/CakesCategories';
import CreateCakeCard from './pages/CakesPage/Components/CreateCakeCard'
import DeleteCakeCard from './pages/CakesPage/Components/DeleteCakeCard';
import UpdateCakeCard, { UpdateCake } from './pages/CakesPage/Components/UpdateCakeCard';
import { APIClient } from './APIClient';

const api = new APIClient();

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "categories",
        element: <CakesCategories />,
        loader: api.GetCategories,
      },
      {
        path: "categories/create",
        action: api.CreateCategory,
      },
      {
        path: "categories/delete/:categoryId",
        action: api.DeleteCategory,
      },
      {
        path: "categories/update/:categoryId",
        action: api.UpdateCategory,
      },


      {
        path: "categories/:categoryId",
        element: <CakesPage />,
        loader: GetCakes,
      },
      {
        path: "categories/:categoryId/cake/create",
        element: <CreateCakeCard />,
        action: CreateCake,
      },
      {
        path: "categories/:categoryId/cake/delete/:cakeId",
        action: DeleteCakeCard,
      },
      {
        path: "categories/:categoryId/cake/update/:cakeId",
        element: <UpdateCakeCard />,
        loader: GetCakeById,
        action: UpdateCake,
      },

    ],
  },
]);

function App() {

  return (
    <RouterProvider router={router} />
);
}

export default App
