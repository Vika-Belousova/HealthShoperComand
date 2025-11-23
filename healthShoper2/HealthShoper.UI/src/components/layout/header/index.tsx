import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Link from '@mui/material/Link';
import IconButton from '@mui/material/IconButton';
import Stack from '@mui/material/Stack';
import PhoneIcon from '@mui/icons-material/Phone';
import { routes } from '../../../infrastructure/consts/routes';

const navItems = [
  { label: 'СТАТЬИ', to: routes.articles },
  { label: 'ОТЗЫВЫ', to: routes.reviews },
  { label: 'ПАРТНЕРАМ', to: routes.partners },
  { label: 'КОНТАКТЫ', to: routes.contacts },
];

const AppHeader: React.FC = () => {
  return (
    <AppBar position="static" elevation={0} color="inherit">
      <Toolbar sx={{ justifyContent: 'space-between', py: 1 }}>
        <RouterLink to={routes.home}>
          <Stack direction="row" alignItems="center" spacing={1}>
            <Box component="img" src="/vite.svg" alt="HealthShoper" sx={{ height: 32 }} />
            <Stack alignItems="center">
              <Typography variant="h6" color="primary" sx={{ fontWeight: 600 }}>
                HealthShoper
              </Typography>
              <Typography variant="body2" color="text.secondary" sx={{ ml: 0.5 }}>
                товары для здоровья
              </Typography>
            </Stack>
          </Stack>
        </RouterLink>

        <Stack direction="row" alignItems="center" spacing={3}>
          <Button
            variant="contained"
            sx={{
              background: 'linear-gradient(90deg, #26A69A 0%, #66BB6A 100%)',
              borderRadius: '8px',
              textTransform: 'none',
              fontWeight: 600,
              px: 2.5,
              '&:hover': { opacity: 0.9 },
            }}
          >
            ХОЧУ ПОПРОБОВАТЬ
          </Button>

          <Stack direction="row" spacing={2}>
            {navItems.map(item => (
              <Link
                component={RouterLink}
                key={item.to}
                to={item.to}
                underline="none"
                color="text.primary"
                sx={{
                  fontSize: 14,
                  fontWeight: 500,
                  '&:hover': { color: 'primary.main' },
                }}
              >
                {item.label}
              </Link>
            ))}
          </Stack>
        </Stack>

        <Stack alignItems="center" spacing={1}>
          <Typography fontWeight={700} color="text.primary">
            +7 499 444-69-02
          </Typography>
          <Link href="#" underline="hover" color="primary.main" fontSize={14}>
            <IconButton color="primary" size="small">
              <PhoneIcon fontSize="small" />
            </IconButton>
            обратный звонок
          </Link>
        </Stack>
      </Toolbar>
    </AppBar>
  );
};

export default AppHeader;
