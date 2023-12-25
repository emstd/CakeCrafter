import { Outlet } from 'react-router-dom';
import CategoriesComponent from '../../components/CategoriesComponent';

function MainPage() {

  return (
    <>
      <div>
            <CategoriesComponent />
      </div>
      <div>
        <Outlet />
      </div>
    </>
);
}

export default MainPage