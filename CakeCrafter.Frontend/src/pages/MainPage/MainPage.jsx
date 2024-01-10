import { Outlet, useNavigate, Link } from 'react-router-dom';
import {
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  IconButton,
  Box
} from '@chakra-ui/react'

import {
  HamburgerIcon,
  StarIcon
} from '@chakra-ui/icons'

function MainPage() {
  const navigate = useNavigate();
  return (
    <div id='main-page'>
      <Box>
        <Link to='/'><StarIcon />CCLogo<StarIcon /></Link>
      </Box>
      <Menu>
        <MenuButton
          as={IconButton}
          aria-label='Options'
          icon={<HamburgerIcon />}
          variant='outline'
        />
        <MenuList>
          <MenuItem onClick={() => navigate("/")}>
            Главная
          </MenuItem>
          <MenuItem onClick={() => navigate("/categories")}>
            Карточки товаров
          </MenuItem>
        </MenuList>
      </Menu>

      <div id='content'>
        <Outlet />
      </div>
    </div>
);
}


export default MainPage
