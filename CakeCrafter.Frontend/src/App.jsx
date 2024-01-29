import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import CakesCategories from './pages/CakesCategories/CakesCategories';
import CreateCakeCard from './pages/CakesPage/Components/CreateCakeCard'
import UpdateCakeCard from './pages/CakesPage/Components/UpdateCakeCard';
import { APIClient } from './APIClient';
import Tastes from './pages/Tastes/Tastes';

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
        loader: api.GetCakes,
      },
      {
        path: "categories/:categoryId/cake/create",
        element: <CreateCakeCard />,
        action: api.CreateCake,
      },
      {
        path: "categories/:categoryId/cake/delete/:cakeId",
        action: api.DeleteCakeCard,
      },
      {
        path: "categories/:categoryId/cake/update/:cakeId",
        element: <UpdateCakeCard />,
        loader: api.GetCakeById,
        action: api.UpdateCake,
      },
      

      {
        path: "tastes",
        element: <Tastes />,
        loader: api.GetTastes,
      },
      {
        path: "tastes/update/:tasteId",
        action: api.UpdateTaste,
      },
      {
        path: "tastes/delete/:tasteId",
        action: api.DeleteTaste,
      },
      {
        path: "tastes/create",
        action: api.CreateTaste,
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
