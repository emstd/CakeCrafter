import { Outlet, Link } from 'react-router-dom';

function MainPage() {

  return (
    <>
      <div>
        <p>
             <Link to="/">Главная</Link>
        </p>
        <p>
             <Link to="/categories">Карточки товаров</Link>
        </p>
      </div>
      <div>
        <Outlet />
      </div>
    </>
);
}

export default MainPage