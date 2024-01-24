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
    <Box display='flex' flexDirection='column' width='100%' alignItems='stretch'>

      <Box display='flex' alignItems='center' justifyContent='space-between' width='15%'>
            <Box>
              <Link to='/'><StarIcon />CCLogo<StarIcon /></Link>
            </Box>

            <Button width={'40px'} onClick={toggleColorMode} >
              {colorMode === 'light' ? <MoonIcon /> : <SunIcon />}
            </Button>

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
                <MenuItem onClick={() => navigate("/tastes")}>
                  Вкусы
                </MenuItem>
              </MenuList>
            </Menu>
      </Box>


      <Box ml='20%' width='80%'>
        <Outlet />
      </Box>

    </Box>
);
}


export default MainPage
