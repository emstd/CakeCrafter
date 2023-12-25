import './App.css';
import MainPage from './pages/MainPage/MainPage';
import ErrorPage from "./pages/ErrorPage/error-page";
import CakesPage from "./pages/CakesPage/CakesPage";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { GetCategories } from './components/CategoriesComponent';

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    loader: GetCategories,
    children: [
      {
        path: "categories/:categoryId",
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