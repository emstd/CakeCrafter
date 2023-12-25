import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { CreateCategory, GetCategories } from './components/CategoriesComponent';
import { GetCakes } from './pages/CakesPage/CakesPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    loader: GetCategories,
    action: CreateCategory,
    children: [
      {
        path: "categories/:categoryId",
        loader: GetCakes,
        element: <CakesPage />,
      }
    ]
  },
]);

function App() {

  return (
    <RouterProvider router={router} />
);
}

export default App