import React, { useEffect } from 'react';
import {
  Box,
  Typography,
  IconButton,
  Button,
  Divider,
  Stack,
  Card,
  CardMedia,
  CardContent,
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import { Link, useNavigate } from 'react-router-dom';
import { useCartStore } from '../../store/use-cart-store';

export const CartPage: React.FC = () => {
  const navigate = useNavigate();
  const { items, getFromBasket, removeFromCart, removeQuantity, clearCart, addToCart, totalPrice } =
    useCartStore();

  const handleCheckout = () => {
    // Переход на страницу оформления заказа
    navigate('/checkout');
  };

  useEffect(() => {
    getFromBasket();
  },[]);

  return (
    <Box p={3}>
      <Typography variant="h4" mb={3}>
        Корзина
      </Typography>

      {items.length === 0 ? (
        <Box textAlign="center" mt={5}>
          <Typography variant="h6" color="text.secondary" mb={2}>
            Ваша корзина пуста
          </Typography>
          <Button variant="contained" component={Link} to="/" sx={{ textTransform: 'none' }}>
            Вернуться к покупкам
          </Button>
        </Box>
      ) : (
        <>
          <Stack spacing={2}>
            {items.map(item => (
              <Card key={item.id} sx={{ display: 'flex', alignItems: 'center', p: 1 }}>
                <CardMedia
                  component="img"
                  image={item.image}
                  alt={item.title}
                  sx={{ width: 100, height: 100, objectFit: 'cover', borderRadius: 2 }}
                />
                <CardContent sx={{ flex: 1 }}>
                  <Typography variant="subtitle1" fontWeight={600}>
                    {item.title}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {item.description}
                  </Typography>
                  <Typography variant="h6" color="primary" mt={1}>
                    {item.price.toLocaleString()} ₽
                  </Typography>
                </CardContent>
                <Stack direction="row" alignItems="center" spacing={1}>
                  <IconButton onClick={() => removeQuantity(item.id)}>
                    <RemoveIcon />
                  </IconButton>
                  <Typography>{item.quantity}</Typography>
                  <IconButton onClick={() => addToCart(item)}>
                    <AddIcon />
                  </IconButton>
                </Stack>
                <IconButton color="error" onClick={() => removeFromCart(item.id)}>
                  <DeleteIcon />
                </IconButton>
              </Card>
            ))}
          </Stack>

          <Divider sx={{ my: 3 }} />

          <Stack direction="row" justifyContent="space-between" alignItems="center">
            <Typography variant="h6">Итого: {totalPrice().toLocaleString()} ₽</Typography>
            <Stack direction="row" spacing={2}>
              <Button variant="outlined" color="error" onClick={clearCart}>
                Очистить
              </Button>
              <Button variant="contained" color="primary" onClick={handleCheckout}>
                Оформить заказ
              </Button>
            </Stack>
          </Stack>
        </>
      )}
    </Box>
  );
};
