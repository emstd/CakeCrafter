import { Outlet, useNavigate, Link } from 'react-router-dom';
import {
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  IconButton,
  Button,
  Box,
  useColorMode
} from '@chakra-ui/react'

import {
  HamburgerIcon,
  StarIcon,
  MoonIcon,
  SunIcon
} from '@chakra-ui/icons'

function MainPage() {
  const navigate = useNavigate();
  const { colorMode, toggleColorMode } = useColorMode()
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

      <Button width={'40px'} onClick={toggleColorMode}>
        {colorMode === 'light' ? <MoonIcon /> : <SunIcon />}
      </Button>

      <div id='content'>
        <Outlet />
      </div>
    </div>
);
}


export default MainPage
