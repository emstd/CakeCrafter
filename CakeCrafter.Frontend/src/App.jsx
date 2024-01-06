import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { CreateCategory, DeleteCategory, GetCategories, UpdateCategory } from './pages/CakesCategories/CakesCategories';
import { GetCakes } from './pages/CakesPage/CakesPage';
import CakesCategories from './pages/CakesCategories/CakesCategories';

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
        loader: GetCakes,
        element: <CakesPage />,
      },
      {
        path: "/categories/create",
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
    ],
  },
]);

function App() {

  return (
    <RouterProvider router={router} />
);
}

export default App
