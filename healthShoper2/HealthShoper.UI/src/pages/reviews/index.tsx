import React, { useState } from 'react';
import {
  Box,
  Typography,
  Paper,
  Button,
  TextField,
  Rating,
  Avatar,
  Stack,
  Divider,
  Card,
  CardContent,
  Alert,
} from '@mui/material';
import { Star, StarBorder, ThumbUp, ThumbDown } from '@mui/icons-material';

interface Review {
  id: string;
  author: string;
  rating: number;
  date: string;
  text: string;
  likes: number;
  dislikes: number;
  product: string;
  verified: boolean;
}

export const ReviewsPage: React.FC = () => {
  const [reviews, setReviews] = useState<Review[]>([
    {
      id: '1',
      author: 'Анна Петрова',
      rating: 5,
      date: '2024-01-15',
      text: 'Отличный матрас-топпер! Спина перестала болеть уже после первой ночи. Очень довольна покупкой, рекомендую всем, кто страдает от болей в спине.',
      likes: 12,
      dislikes: 0,
      product: 'Матрас-топпер FIBROTOP Classic',
      verified: true,
    },
    {
      id: '2',
      author: 'Михаил Иванов',
      rating: 4,
      date: '2024-01-10',
      text: 'Хорошая ортопедическая подушка, но ожидал большего. Шея действительно меньше устает, но цена немного завышена.',
      likes: 8,
      dislikes: 2,
      product: 'Ортопедическая подушка Memory Foam',
      verified: true,
    },
    {
      id: '3',
      author: 'Екатерина Смирнова',
      rating: 5,
      date: '2024-01-08',
      text: 'Массажер просто спасает после рабочего дня! Удобно использовать, приятный прогрев. Муж тоже пользуется, теперь спорим за него)',
      likes: 15,
      dislikes: 1,
      product: 'Массажер для шеи с прогревом',
      verified: true,
    },
    {
      id: '4',
      author: 'Дмитрий Козлов',
      rating: 3,
      date: '2024-01-05',
      text: 'Массажный коврик не совсем оправдал ожидания. Колючий слишком, сложно расслабиться. Может, нужно привыкнуть.',
      likes: 3,
      dislikes: 5,
      product: 'Массажный коврик акупунктурный',
      verified: false,
    },
  ]);

  const [newReview, setNewReview] = useState({
    rating: 0,
    text: '',
    product: '',
  });

  const [showForm, setShowForm] = useState(false);

  // Исправленная функция - принимает оба параметра
  const handleRatingChange = (event: React.SyntheticEvent, value: number | null) => {
    setNewReview({ ...newReview, rating: value || 0 });
  };

  const handleTextChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    setNewReview({ ...newReview, text: event.target.value });
  };

  const handleProductChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewReview({ ...newReview, product: event.target.value });
  };

  const handleSubmitReview = () => {
    if (newReview.rating === 0 || !newReview.text.trim() || !newReview.product.trim()) {
      alert('Пожалуйста, заполните все поля и поставьте оценку');
      return;
    }

    const review: Review = {
      id: Date.now().toString(),
      author: 'Вы',
      rating: newReview.rating,
      date: new Date().toISOString().split('T')[0],
      text: newReview.text,
      likes: 0,
      dislikes: 0,
      product: newReview.product,
      verified: true,
    };

    setReviews([review, ...reviews]);
    setNewReview({ rating: 0, text: '', product: '' });
    setShowForm(false);
  };

  const handleLike = (id: string) => {
    setReviews(reviews.map(review => 
      review.id === id ? { ...review, likes: review.likes + 1 } : review
    ));
  };

  const handleDislike = (id: string) => {
    setReviews(reviews.map(review => 
      review.id === id ? { ...review, dislikes: review.dislikes + 1 } : review
    ));
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('ru-RU', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    });
  };

  const averageRating = reviews.reduce((acc, review) => acc + review.rating, 0) / reviews.length;

  return (
    <Box p={3}>
      <Typography variant="h4" mb={3}>
        Отзывы покупателей
      </Typography>

      {/* Статистика */}
      <Paper sx={{ p: 3, mb: 4, backgroundColor: 'grey.50' }}>
        <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, justifyContent: 'space-between', alignItems: 'center' }}>
          <Box textAlign="center" mb={{ xs: 2, sm: 0 }}>
            <Typography variant="h3" color="primary" fontWeight="bold">
              {averageRating.toFixed(1)}
            </Typography>
            <Rating value={averageRating} readOnly precision={0.1} />
            <Typography variant="body2" color="text.secondary">
              Средняя оценка
            </Typography>
          </Box>
          <Box textAlign="center" mb={{ xs: 2, sm: 0 }}>
            <Typography variant="h3" color="primary" fontWeight="bold">
              {reviews.length}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Всего отзывов
            </Typography>
          </Box>
          <Box textAlign="center">
            <Typography variant="h3" color="primary" fontWeight="bold">
              {reviews.filter(r => r.verified).length}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Проверенных отзывов
            </Typography>
          </Box>
        </Box>
      </Paper>

      {/* Кнопка добавления отзыва */}
      <Box textAlign="center" mb={4}>
        <Button 
          variant="contained" 
          size="large"
          onClick={() => setShowForm(!showForm)}
          sx={{ textTransform: 'none', borderRadius: 2 }}
        >
          {showForm ? 'Отменить' : 'Написать отзыв'}
        </Button>
      </Box>

      {/* Форма добавления отзыва */}
      {showForm && (
        <Paper sx={{ p: 3, mb: 4 }}>
          <Typography variant="h6" mb={2}>
            Оставить отзыв
          </Typography>
          <Stack spacing={3}>
            <TextField
              label="О каком товаре вы хотите оставить отзыв?"
              value={newReview.product}
              onChange={handleProductChange}
              fullWidth
              placeholder="Например: Матрас-топпер FIBROTOP Classic"
            />
            <Box>
              <Typography component="legend" mb={1}>
                Ваша оценка
              </Typography>
              <Rating
                value={newReview.rating}
                onChange={handleRatingChange}
                size="large"
                icon={<Star fontSize="inherit" />}
                emptyIcon={<StarBorder fontSize="inherit" />}
              />
            </Box>
            <TextField
              label="Текст отзыва"
              value={newReview.text}
              onChange={handleTextChange}
              multiline
              rows={4}
              fullWidth
              placeholder="Поделитесь вашим опытом использования товара..."
            />
            <Button 
              variant="contained" 
              onClick={handleSubmitReview}
              disabled={!newReview.rating || !newReview.text.trim() || !newReview.product.trim()}
              sx={{ textTransform: 'none' }}
            >
              Опубликовать отзыв
            </Button>
          </Stack>
        </Paper>
      )}

      {/* Список отзывов */}
      <Stack spacing={3}>
        {reviews.map((review) => (
          <Card key={review.id} variant="outlined">
            <CardContent sx={{ p: 3 }}>
              {/* Заголовок отзыва */}
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mb: 2 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
                  <Avatar sx={{ bgcolor: 'primary.main' }}>
                    {review.author.charAt(0)}
                  </Avatar>
                  <Box>
                    <Typography variant="subtitle1" fontWeight={600}>
                      {review.author}
                      {review.verified && (
                        <Typography 
                          component="span" 
                          variant="caption" 
                          color="success.main"
                          sx={{ ml: 1 }}
                        >
                          ✓ Проверенный покупатель
                        </Typography>
                      )}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {formatDate(review.date)}
                    </Typography>
                  </Box>
                </Box>
                <Rating value={review.rating} readOnly />
              </Box>

              {/* Товар */}
              <Typography variant="body2" color="primary" fontWeight={600} mb={1}>
                {review.product}
              </Typography>

              {/* Текст отзыва */}
              <Typography variant="body1" paragraph>
                {review.text}
              </Typography>

              <Divider sx={{ my: 2 }} />

              {/* Действия */}
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Box sx={{ display: 'flex', gap: 1 }}>
                  <Button 
                    startIcon={<ThumbUp />} 
                    size="small"
                    onClick={() => handleLike(review.id)}
                    sx={{ textTransform: 'none' }}
                  >
                    Полезно ({review.likes})
                  </Button>
                  <Button 
                    startIcon={<ThumbDown />} 
                    size="small"
                    onClick={() => handleDislike(review.id)}
                    sx={{ textTransform: 'none' }}
                  >
                    ({review.dislikes})
                  </Button>
                </Box>
                <Button variant="text" size="small" sx={{ textTransform: 'none' }}>
                  Ответить
                </Button>
              </Box>
            </CardContent>
          </Card>
        ))}
      </Stack>

      {/* Сообщение если нет отзывов */}
      {reviews.length === 0 && (
        <Alert severity="info" sx={{ mt: 3 }}>
          Пока нет отзывов. Будьте первым, кто оставит отзыв!
        </Alert>
      )}
    </Box>
  );
};

export default ReviewsPage;