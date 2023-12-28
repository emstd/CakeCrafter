import { Outlet, Link } from 'react-router-dom';

function MainPage() {

  return (
    <>
      <div>
      <nav>
            <ul>
              <Link to="/"><li>Главная</li></Link>
              <Link to="/categories"><li>Карточки товаров</li></Link>
            </ul>
        </nav>
      </div>
      <div>
        <Outlet />
      </div>
    </>
);
}

export default MainPage