import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { CreateCategory, DeleteCategory, GetCategories } from './components/CategoriesComponent';
import { GetCakes } from './pages/CakesPage/CakesPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    loader: GetCategories,
    children: [
      {
        path: "categories/:categoryId",
        loader: GetCakes,
        element: <CakesPage />,
      },
    ]
  },
  {
    path: "/categories/create",
    action: CreateCategory,
  },
  {
    path: "categories/delete/:categoryId",
    action: DeleteCategory,
  }
]);

function App() {

  return (
    <RouterProvider router={router} />
);
}

export default App