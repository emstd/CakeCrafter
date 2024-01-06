import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { CreateCategory, DeleteCategory, GetCategories, UpdateCategory } from './pages/CakesCategories/CakesCategories';
import { CreateCake } from './pages/CakesPage/Components/CreateCakeCard';
import { GetCakes } from './pages/CakesPage/CakesPage';
import CakesCategories from './pages/CakesCategories/CakesCategories';
import CreateCakeCard from './pages/CakesPage/Components/CreateCakeCard'
import DeleteCakeCard from './pages/CakesPage/Components/DeleteCakeCard';

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "categories",
        element: <CakesCategories />,
        loader: GetCategories,
      },
      {
        path: "categories/:categoryId",
        element: <CakesPage />,
        loader: GetCakes,
      },
      {
        path: "categories/create",
        action: CreateCategory,
      },
      {
        path: "categories/delete/:categoryId",
        action: DeleteCategory,
      },
      {
        path: "categories/update/:categoryId",
        action: UpdateCategory,
      },

      {
        path: "categories/cake/create",
        element: <CreateCakeCard />,
        action: CreateCake,
      },
      {
        path: "categories/cake/delete/:cakeId",
        action: DeleteCakeCard,
      }
    ],
  },
]);

function App() {

  return (
    <RouterProvider router={router} />
);
}

export default App
