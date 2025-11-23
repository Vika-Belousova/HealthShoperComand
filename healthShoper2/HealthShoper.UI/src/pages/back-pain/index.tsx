import React from 'react';
import {
  Box,
  Typography,
  Paper,
  Stack,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Alert,
} from '@mui/material';
import { CheckCircle, LocalHospital, FitnessCenter,  Warning } from '@mui/icons-material';

export const BackPainPage: React.FC = () => {
  return (
    <Box p={3}>
      <Typography variant="h4" mb={3} fontWeight="bold">
        Болит спина, что делать?
      </Typography>

      {/* Введение */}
      <Paper sx={{ p: 3, mb: 4, backgroundColor: 'primary.50' }}>
        <Typography variant="h6" color="primary" mb={2}>
          Понимание причин боли в спине
        </Typography>
        <Typography variant="body1" paragraph>
          Боль в спине — одна из самых распространенных проблем современного человека. 
          Она может быть вызвана различными факторами: от неправильной осанки до серьезных заболеваний.
        </Typography>
      </Paper>

      {/* Основные причины */}
      <Paper sx={{ p: 3, mb: 4 }}>
        <Typography variant="h5" mb={3} color="primary">
          Основные причины боли в спине
        </Typography>
        
        <Stack spacing={2}>
          <Box display="flex" alignItems="flex-start" gap={2}>
            <Warning color="warning" />
            <Box>
              <Typography variant="h6" gutterBottom>
                Неправильная осанка
              </Typography>
              <Typography variant="body1">
                Длительное сидение в неправильном положении, сутулость, 
                неравномерная нагрузка на позвоночник.
              </Typography>
            </Box>
          </Box>

          <Box display="flex" alignItems="flex-start" gap={2}>
            <FitnessCenter color="secondary" />
            <Box>
              <Typography variant="h6" gutterBottom>
                Мышечное напряжение
              </Typography>
              <Typography variant="body1">
                Чрезмерные физические нагрузки, подъем тяжестей, 
                резкие движения без подготовки.
              </Typography>
            </Box>
          </Box>

          <Box display="flex" alignItems="flex-start" gap={2}>
            <LocalHospital color="error" />
            <Box>
              <Typography variant="h6" gutterBottom>
                Заболевания позвоночника
              </Typography>
              <Typography variant="body1">
                Остеохондроз, грыжи межпозвонковых дисков, радикулит, 
                артрит и другие патологии.
              </Typography>
            </Box>
          </Box>
        </Stack>
      </Paper>

      {/* Рекомендации */}
      <Paper sx={{ p: 3, mb: 4 }}>
        <Typography variant="h5" mb={3} color="primary">
          Что делать при острой боли в спине?
        </Typography>

        <List>
          <ListItem>
            <ListItemIcon>
              <CheckCircle color="success" />
            </ListItemIcon>
            <ListItemText 
              primary="Обеспечьте покой" 
              secondary="Избегайте резких движений и физических нагрузок" 
            />
          </ListItem>
          
          <ListItem>
            <ListItemIcon>
              <CheckCircle color="success" />
            </ListItemIcon>
            <ListItemText 
              primary="Примите удобное положение" 
              secondary="Лягте на жесткую поверхность, подложив под колени валик" 
            />
          </ListItem>
          
          <ListItem>
            <ListItemIcon>
              <CheckCircle color="success" />
            </ListItemIcon>
            <ListItemText 
              primary="Используйте местные средства" 
              secondary="Противовоспалительные мази и гели могут временно облегчить боль" 
            />
          </ListItem>
          
          <ListItem>
            <ListItemIcon>
              <CheckCircle color="success" />
            </ListItemIcon>
            <ListItemText 
              primary="Обратитесь к специалисту" 
              secondary="При сильной или длительной боли обязательно проконсультируйтесь с врачом" 
            />
          </ListItem>
        </List>
      </Paper>

      {/* Профилактика */}
      <Paper sx={{ p: 3, mb: 4 }}>
        <Typography variant="h5" mb={3} color="primary">
          Профилактика боли в спине
        </Typography>

        <Box display="grid" gridTemplateColumns={{ xs: '1fr', md: '1fr 1fr' }} gap={3}>
          <Box>
            <Typography variant="h6" gutterBottom color="secondary">
              🏃‍♂️ Регулярная активность
            </Typography>
            <Typography variant="body2">
              Ежедневная гимнастика, плавание, йога, пилатес
            </Typography>
          </Box>

          <Box>
            <Typography variant="h6" gutterBottom color="secondary">
              💺 Правильная осанка
            </Typography>
            <Typography variant="body2">
              Организация рабочего места, эргономичная мебель
            </Typography>
          </Box>

          <Box>
            <Typography variant="h6" gutterBottom color="secondary">
              🛌 Здоровый сон
            </Typography>
            <Typography variant="body2">
              Ортопедический матрас и подушка для правильного положения позвоночника
            </Typography>
          </Box>

          <Box>
            <Typography variant="h6" gutterBottom color="secondary">
              ⚖️ Контроль веса
            </Typography>
            <Typography variant="body2">
              Избыточный вес создает дополнительную нагрузку на позвоночник
            </Typography>
          </Box>
        </Box>
      </Paper>

      {/* Наши решения */}
      <Paper sx={{ p: 3, mb: 4, backgroundColor: 'success.50' }}>
        <Typography variant="h5" mb={2} color="success.main">
          Наши решения для здоровья вашей спины
        </Typography>
        
        <Stack spacing={2} mb={3}>
          <Box display="flex" alignItems="center" gap={2}>
            <CheckCircle color="success" />
            <Typography variant="body1">
              <strong>Ортопедические матрасы</strong> — правильная поддержка позвоночника во время сна
            </Typography>
          </Box>
          
          <Box display="flex" alignItems="center" gap={2}>
            <CheckCircle color="success" />
            <Typography variant="body1">
              <strong>Массажные кресла</strong> — снятие мышечного напряжения и улучшение кровообращения
            </Typography>
          </Box>
          
          <Box display="flex" alignItems="center" gap={2}>
            <CheckCircle color="success" />
            <Typography variant="body1">
              <strong>Ортопедические подушки</strong> — поддержка шейного отдела позвоночника
            </Typography>
          </Box>
          
          <Box display="flex" alignItems="center" gap={2}>
            <CheckCircle color="success" />
            <Typography variant="body1">
              <strong>Массажеры для спины</strong> — домашняя физиотерапия и расслабление мышц
            </Typography>
          </Box>
        </Stack>
      </Paper>

      {/* Важное предупреждение */}
      <Alert severity="warning" sx={{ mb: 2 }}>
        <Typography variant="subtitle2">
          Важно: Данная информация носит ознакомительный характер. 
          При сильной или продолжительной боли в спине обязательно обратитесь к врачу!
        </Typography>
      </Alert>
    </Box>
  );
};

export default BackPainPage;