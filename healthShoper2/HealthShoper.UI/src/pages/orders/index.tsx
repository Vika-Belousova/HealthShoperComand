import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Paper,
  Button,
  CardMedia,
  Chip,
  Stack,
  Divider,
  Alert,
} from '@mui/material';
import { Link } from 'react-router-dom';
import ShoppingBagIcon from '@mui/icons-material/ShoppingBag';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import PendingIcon from '@mui/icons-material/Pending';

// Тип для заказа
interface OrderItem {
  id: string;
  title: string;
  price: number;
  quantity: number;
  image: string;
}

interface Order {
  id: string;
  number: string;
  date: string;
  status: 'pending' | 'processing' | 'shipped' | 'delivered' | 'cancelled';
  items: OrderItem[];
  total: number;
  customer: {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
  };
  shipping: {
    address: string;
    city: string;
    postalCode: string;
  };
}

export const OrdersPage: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);

  // Mock данные для заказов
  useEffect(() => {
    const mockOrders: Order[] = [
      {
        id: '1',
        number: 'ORD-123456',
        date: '2024-01-15',
        status: 'delivered',
        total: 25752,
        customer: {
          firstName: 'Иван',
          lastName: 'Петров',
          email: 'ivan@example.com',
          phone: '+7 (900) 123-45-67',
        },
        shipping: {
          address: 'ул. Примерная, д. 123, кв. 45',
          city: 'Москва',
          postalCode: '123456',
        },
        items: [
          {
            id: '1',
            title: 'Матрас-топпер FIBROTOP Classic (80x200x8 см)',
            price: 25752,
            quantity: 1,
            image: '/card/card1.jpg',
          },
        ],
      },
      {
        id: '2',
        number: 'ORD-123457',
        date: '2024-01-10',
        status: 'shipped',
        total: 35980,
        customer: {
          firstName: 'Мария',
          lastName: 'Сидорова',
          email: 'maria@example.com',
          phone: '+7 (900) 987-65-43',
        },
        shipping: {
          address: 'пр. Ленинградский, д. 456, кв. 12',
          city: 'Санкт-Петербург',
          postalCode: '654321',
        },
        items: [
          {
            id: '2',
            title: 'Ортопедическая подушка Memory Foam',
            price: 5990,
            quantity: 2,
            image: '/card/card2.jpg',
          },
          {
            id: '3',
            title: 'Массажер для шеи с прогревом',
            price: 12000,
            quantity: 1,
            image: '/card/card3.jpg',
          },
        ],
      },
    ];

    // Имитация загрузки данных
    setTimeout(() => {
      setOrders(mockOrders);
      setLoading(false);
    }, 1000);
  }, []);

  const getStatusInfo = (status: Order['status']) => {
    switch (status) {
      case 'pending':
        return {
          label: 'Ожидает обработки',
          color: 'warning' as const,
          icon: <PendingIcon />,
        };
      case 'processing':
        return {
          label: 'В обработке',
          color: 'info' as const,
          icon: <ShoppingBagIcon />,
        };
      case 'shipped':
        return {
          label: 'Отправлен',
          color: 'primary' as const,
          icon: <LocalShippingIcon />,
        };
      case 'delivered':
        return {
          label: 'Доставлен',
          color: 'success' as const,
          icon: <CheckCircleIcon />,
        };
      case 'cancelled':
        return {
          label: 'Отменен',
          color: 'error' as const,
          icon: <PendingIcon />,
        };
      default:
        return {
          label: 'Неизвестно',
          color: 'default' as const,
          icon: <PendingIcon />,
        };
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('ru-RU', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    });
  };

  if (loading) {
    return (
      <Box p={3}>
        <Typography variant="h4" mb={3}>
          Мои заказы
        </Typography>
        <Alert severity="info">Загрузка ваших заказов...</Alert>
      </Box>
    );
  }

  if (orders.length === 0) {
    return (
      <Box p={3} textAlign="center">
        <Typography variant="h4" mb={3}>
          Мои заказы
        </Typography>
        <Alert severity="info" sx={{ mb: 2 }}>
          У вас пока нет заказов
        </Alert>
        <Button variant="contained" component={Link} to="/" sx={{ textTransform: 'none' }}>
          Начать покупки
        </Button>
      </Box>
    );
  }

  return (
    <Box p={3}>
      <Typography variant="h4" mb={3}>
        Мои заказы
      </Typography>

      <Stack spacing={3}>
        {orders.map((order) => {
          const statusInfo = getStatusInfo(order.status);
          
          return (
            <Paper key={order.id} sx={{ p: 3 }}>
              {/* Заголовок заказа */}
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                <Box>
                  <Typography variant="h6" gutterBottom>
                    Заказ #{order.number}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    от {formatDate(order.date)}
                  </Typography>
                </Box>
                <Chip
                  icon={statusInfo.icon}
                  label={statusInfo.label}
                  color={statusInfo.color}
                  variant="outlined"
                />
              </Box>

              <Divider sx={{ my: 2 }} />

              {/* Товары в заказе */}
              <Stack spacing={2} mb={3}>
                {order.items.map((item) => (
                  <Box key={item.id} sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
                    <CardMedia
                      component="img"
                      image={item.image}
                      alt={item.title}
                      sx={{ width: 60, height: 60, objectFit: 'cover', borderRadius: 1 }}
                    />
                    <Box flex={1}>
                      <Typography variant="body1" fontWeight={600}>
                        {item.title}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {item.quantity} × {item.price.toLocaleString()} ₽
                      </Typography>
                    </Box>
                    <Typography variant="body1" fontWeight={600}>
                      {(item.price * item.quantity).toLocaleString()} ₽
                    </Typography>
                  </Box>
                ))}
              </Stack>

              {/* Информация о доставке */}
              <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 3, mb: 2 }}>
                <Box flex={1}>
                  <Typography variant="subtitle2" color="text.secondary" gutterBottom>
                    Адрес доставки
                  </Typography>
                  <Typography variant="body2">
                    {order.shipping.city}, {order.shipping.address}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Индекс: {order.shipping.postalCode}
                  </Typography>
                </Box>
                <Box flex={1}>
                  <Typography variant="subtitle2" color="text.secondary" gutterBottom>
                    Получатель
                  </Typography>
                  <Typography variant="body2">
                    {order.customer.firstName} {order.customer.lastName}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {order.customer.phone}
                  </Typography>
                </Box>
              </Box>

              <Divider sx={{ my: 2 }} />

              {/* Итог и действия */}
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Box>
                  <Typography variant="h6" color="primary">
                    Итого: {order.total.toLocaleString()} ₽
                  </Typography>
                </Box>
                <Stack direction="row" spacing={1}>
                  <Button variant="outlined" size="small">
                    Подробнее
                  </Button>
                  {order.status === 'delivered' && (
                    <Button variant="contained" size="small">
                      Оставить отзыв
                    </Button>
                  )}
                </Stack>
              </Box>
            </Paper>
          );
        })}
      </Stack>
    </Box>
  );
};