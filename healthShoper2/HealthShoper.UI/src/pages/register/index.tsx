import React, { useState } from 'react';
import { Box, TextField, Button, Typography, Paper, Link as MuiLink } from '@mui/material';
import { useNavigate, Link } from 'react-router-dom';
import { useAuthStore } from '../../store/use-auth-store';

export const RegisterPage: React.FC = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const register = useAuthStore((s) => s.register);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const success = await register(name, email, password);
    if (success) navigate('/');
  };

  return (
    <Box display="flex" justifyContent="center" alignItems="center" height="80vh">
      <Paper sx={{ p: 4, width: 360, borderRadius: 3 }}>
        <Typography variant="h5" mb={2}>
          Регистрация
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Имя"
            margin="normal"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            fullWidth
            label="Email"
            type="email"
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            fullWidth
            label="Пароль"
            type="password"
            margin="normal"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Button fullWidth type="submit" variant="contained" sx={{ mt: 2 }}>
            Зарегистрироваться
          </Button>
        </form>
        <Typography mt={2} textAlign="center" variant="body2">
          Уже есть аккаунт?{' '}
          <MuiLink component={Link} to="/login">
            Войти
          </MuiLink>
        </Typography>
      </Paper>
    </Box>
  );
};
