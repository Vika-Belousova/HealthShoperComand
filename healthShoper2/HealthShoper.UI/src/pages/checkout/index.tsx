import React, { useState } from 'react';
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  Stepper,
  Step,
  StepLabel,
  CardMedia,
  Divider,
  FormControlLabel,
  Checkbox,
  Alert,
  Stack,
} from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import { useCartStore } from '../../store/use-cart-store';
import ArrowBackIcon from '@mui/icons-material/ArrowBack'; // Добавлен импорт

const steps = ['Контактные данные', 'Доставка', 'Подтверждение'];

export const CheckoutPage: React.FC = () => {
  const navigate = useNavigate();
  const { items, totalPrice, clearCart } = useCartStore();
  const [activeStep, setActiveStep] = useState(0);
  const [orderCompleted, setOrderCompleted] = useState(false);
  const [orderNumber, setOrderNumber] = useState('');

  // Форма данных
  const [formData, setFormData] = useState({
    // Контактные данные
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    
    // Адрес доставки
    address: '',
    city: '',
    postalCode: '',
    apartment: '',
    
    // Комментарий
    comment: '',
    
    // Соглашения
    agreeToTerms: false,
    subscribeToNews: false,
  });

  const handleInputChange = (field: string) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [field]: event.target.value,
    });
  };

  const handleCheckboxChange = (field: string) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [field]: event.target.checked,
    });
  };

  const handleNext = () => {
    setActiveStep((prevStep) => prevStep + 1);
  };

  const handleBack = () => {
    setActiveStep((prevStep) => prevStep - 1);
  };

  const handleSubmitOrder = () => {
    // Генерация номера заказа
    const newOrderNumber = `ORD-${Date.now()}`;
    setOrderNumber(newOrderNumber);
    setOrderCompleted(true);
    
    // Очистка корзины
    clearCart();
  };

  if (items.length === 0 && !orderCompleted) {
    return (
      <Box p={3} textAlign="center">
        <Typography variant="h4" mb={3}>
          Оформление заказа
        </Typography>
        <Alert severity="info" sx={{ mb: 2 }}>
          Ваша корзина пуста
        </Alert>
        <Button variant="contained" component={Link} to="/" sx={{ textTransform: 'none' }}>
          Вернуться к покупкам
        </Button>
      </Box>
    );
  }

  if (orderCompleted) {
    return (
      <Box p={3} textAlign="center">
        <Typography variant="h4" mb={3} color="success.main">
          Заказ оформлен!
        </Typography>
        <Alert severity="success" sx={{ mb: 2 }}>
          Ваш заказ #{orderNumber} успешно оформлен
        </Alert>
        <Typography variant="body1" mb={2}>
          Мы свяжемся с вами в ближайшее время для подтверждения заказа.
        </Typography>
        <Button 
          variant="contained" 
          onClick={() => navigate('/')}
          sx={{ textTransform: 'none', mr: 2 }}
        >
          Вернуться в магазин
        </Button>
        <Button 
          variant="outlined" 
          component={Link} 
          to="/orders"
          sx={{ textTransform: 'none' }}
        >
          Мои заказы
        </Button>
      </Box>
    );
  }

  return (
    <Box p={3}>
      {/* Добавлена кнопка назад в корзину */}
      <Button 
        startIcon={<ArrowBackIcon />}
        component={Link}
        to="/cart"
        sx={{ mb: 2, textTransform: 'none' }}
      >
        Вернуться в корзину
      </Button>

      <Typography variant="h4" mb={3}>
        Оформление заказа
      </Typography>

      <Stepper activeStep={activeStep} sx={{ mb: 4 }}>
        {steps.map((label) => (
          <Step key={label}>
            <StepLabel>{label}</StepLabel>
          </Step>
        ))}
      </Stepper>

      {/* Остальной код без изменений */}
      <Box sx={{ display: 'flex', flexDirection: { xs: 'column', md: 'row' }, gap: 4 }}>
        {/* Основная форма */}
        <Box sx={{ flex: 8 }}>
          <Paper sx={{ p: 3 }}>
            {activeStep === 0 && (
              <Box>
                <Typography variant="h6" mb={3}>
                  Контактные данные
                </Typography>
                <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, gap: 2, mb: 2 }}>
                  <TextField
                    fullWidth
                    label="Имя"
                    value={formData.firstName}
                    onChange={handleInputChange('firstName')}
                    required
                  />
                  <TextField
                    fullWidth
                    label="Фамилия"
                    value={formData.lastName}
                    onChange={handleInputChange('lastName')}
                    required
                  />
                </Box>
                <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, gap: 2 }}>
                  <TextField
                    fullWidth
                    label="Email"
                    type="email"
                    value={formData.email}
                    onChange={handleInputChange('email')}
                    required
                  />
                  <TextField
                    fullWidth
                    label="Телефон"
                    type="tel"
                    value={formData.phone}
                    onChange={handleInputChange('phone')}
                    required
                  />
                </Box>
              </Box>
            )}

            {activeStep === 1 && (
              <Box>
                <Typography variant="h6" mb={3}>
                  Адрес доставки
                </Typography>
                <Stack spacing={2}>
                  <TextField
                    fullWidth
                    label="Город"
                    value={formData.city}
                    onChange={handleInputChange('city')}
                    required
                  />
                  <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, gap: 2 }}>
                    <TextField
                      fullWidth
                      label="Адрес"
                      value={formData.address}
                      onChange={handleInputChange('address')}
                      required
                    />
                    <TextField
                      fullWidth
                      label="Квартира/Офис"
                      value={formData.apartment}
                      onChange={handleInputChange('apartment')}
                    />
                  </Box>
                  <TextField
                    fullWidth
                    label="Индекс"
                    value={formData.postalCode}
                    onChange={handleInputChange('postalCode')}
                  />
                  <TextField
                    fullWidth
                    label="Комментарий к заказу"
                    multiline
                    rows={3}
                    value={formData.comment}
                    onChange={handleInputChange('comment')}
                    placeholder="Например, код домофона, этаж и т.д."
                  />
                </Stack>
              </Box>
            )}

            {activeStep === 2 && (
              <Box>
                <Typography variant="h6" mb={3}>
                  Подтверждение заказа
                </Typography>
                
                <Box mb={3}>
                  <Typography variant="subtitle1" fontWeight={600} mb={1}>
                    Контактные данные
                  </Typography>
                  <Typography>
                    {formData.firstName} {formData.lastName}
                  </Typography>
                  <Typography>{formData.email}</Typography>
                  <Typography>{formData.phone}</Typography>
                </Box>

                <Box mb={3}>
                  <Typography variant="subtitle1" fontWeight={600} mb={1}>
                    Адрес доставки
                  </Typography>
                  <Typography>
                    {formData.city}, {formData.address}
                    {formData.apartment && `, кв. ${formData.apartment}`}
                  </Typography>
                  {formData.comment && (
                    <Typography color="text.secondary">
                      Комментарий: {formData.comment}
                    </Typography>
                  )}
                </Box>

                <FormControlLabel
                  control={
                    <Checkbox
                      checked={formData.agreeToTerms}
                      onChange={handleCheckboxChange('agreeToTerms')}
                      required
                    />
                  }
                  label="Я соглашаюсь с условиями покупки и возврата"
                />
                
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={formData.subscribeToNews}
                      onChange={handleCheckboxChange('subscribeToNews')}
                    />
                  }
                  label="Подписаться на рассылку новостей и акций"
                />
              </Box>
            )}

            <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 3 }}>
              <Button
                disabled={activeStep === 0}
                onClick={handleBack}
                sx={{ textTransform: 'none' }}
              >
                Назад
              </Button>
              <Button
                variant="contained"
                onClick={activeStep === steps.length - 1 ? handleSubmitOrder : handleNext}
                disabled={activeStep === steps.length - 1 && !formData.agreeToTerms}
                sx={{ textTransform: 'none' }}
              >
                {activeStep === steps.length - 1 ? 'Подтвердить заказ' : 'Далее'}
              </Button>
            </Box>
          </Paper>
        </Box>

        {/* Боковая панель с заказом */}
        <Box sx={{ flex: 4 }}>
          <Paper sx={{ p: 3, position: 'sticky', top: 20 }}>
            <Typography variant="h6" mb={2}>
              Ваш заказ
            </Typography>
            
            <Stack spacing={2} mb={3}>
              {items.map((item) => (
                <Box key={item.id} display="flex" gap={2}>
                  <CardMedia
                    component="img"
                    image={item.image}
                    alt={item.title}
                    sx={{ width: 60, height: 60, objectFit: 'cover', borderRadius: 1 }}
                  />
                  <Box flex={1}>
                    <Typography variant="body2" fontWeight={600}>
                      {item.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {item.quantity} × {item.price.toLocaleString()} ₽
                    </Typography>
                  </Box>
                </Box>
              ))}
            </Stack>

            <Divider sx={{ my: 2 }} />

            <Box display="flex" justifyContent="space-between" mb={1}>
              <Typography>Товары:</Typography>
              <Typography>{totalPrice().toLocaleString()} ₽</Typography>
            </Box>
            
            <Box display="flex" justifyContent="space-between" mb={1}>
              <Typography>Доставка:</Typography>
              <Typography color="success.main">Бесплатно</Typography>
            </Box>
            
            <Divider sx={{ my: 2 }} />
            
            <Box display="flex" justifyContent="space-between">
              <Typography variant="h6">Итого:</Typography>
              <Typography variant="h6" color="primary">
                {totalPrice().toLocaleString()} ₽
              </Typography>
            </Box>
          </Paper>
        </Box>
      </Box>
    </Box>
  );
};