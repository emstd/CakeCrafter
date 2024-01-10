import { Outlet, Link } from 'react-router-dom';
import { useColorMode, Button } from '@chakra-ui/react';

function MainPage() {

  return (
    <div id='main-page'>
      <div id='nav-panel'>
      <nav>
            <ul>
              <Link to="/"><li>Главная</li></Link>
              <Link to="/categories"><li>Карточки товаров</li></Link>
            </ul>
        </nav>
      </div>
      <div id='content'>
        <Outlet />
      </div>
    </div>
);
}


export default MainPage
