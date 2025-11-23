import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Stack from '@mui/material/Stack';
import Badge from '@mui/material/Badge';
import Button from '@mui/material/Button';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import { useNavigate } from 'react-router-dom';
import { routes } from '../../infrastructure/consts/routes';
import { useCartStore } from '../../store/use-cart-store';
import { useAuthStore } from '../../store/use-auth-store';

const SubHeader: React.FC = () => {
  const navigate = useNavigate();
  const cartCount = useCartStore(state => state.items.length);
  const { user, isAuthenticated, logout } = useAuthStore();

  return (
    <AppBar
      position="static"
      elevation={0}
      sx={{
        background: 'linear-gradient(90deg, #5B8DEF 0%, #5BD2A1 100%)',
        color: '#fff',
      }}
    >
      <Toolbar sx={{ justifyContent: 'space-between', py: 0.5 }}>
        <Stack direction="row" alignItems="center" spacing={1}>
          <Typography variant="subtitle1" fontWeight={600}>
            КАТАЛОГ ТОВАРОВ
          </Typography>
        </Stack>
        <Stack direction="row" alignItems="center" spacing={3}>
          <Button
            color="inherit"
            sx={{ textTransform: 'none', color: 'white' }}
            onClick={() => (isAuthenticated ? logout() : navigate('/login'))}
            startIcon={<AccountCircleIcon />}
          >
            {isAuthenticated ? user?.name : 'Войти'}
          </Button>
          <Button
            color="inherit"
            startIcon={
              <Badge badgeContent={cartCount} color="error">
                <ShoppingCartIcon />
              </Badge>
            }
            endIcon={<ArrowDropDownIcon />}
            sx={{ textTransform: 'none', color: 'white' }}
            onClick={() => navigate(routes.cart)}
          >
            Корзина
          </Button>
        </Stack>
      </Toolbar>
    </AppBar>
  );
};

export default SubHeader;
