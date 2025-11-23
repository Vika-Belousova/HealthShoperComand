/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { type ReactNode } from 'react';
import {
  Box,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Chip,
  Typography,
} from '@mui/material';
import { Link } from 'react-router-dom';

interface IProps {
  children: ReactNode;
}

const menuItems = [
  { text: 'Все товары', link: '/' },
  { text: 'Когда болит спина', chip: 'ХИТ', chipColor: 'error', link: '/back-pain' },
  { text: 'Когда болит шея', link: '/neck-pain' },
  { text: 'Для здорового сна', link: '/sleep' },
  { text: 'Массажное оборудование', link: '/massage-equipment' },
  { text: 'Товары со скидкой', chip: 'СКИДКА', chipColor: 'success', link: '/discounts' },
  { text: 'Отзывы', chip: 'НОВОЕ', chipColor: 'info', link: '/reviews' },
  { text: 'Болит спина, что делать?', link: '/back-pain' }, 
  { text: 'Где можно попробовать?', link: '/try' },
];

export const MainDrawer: React.FC<IProps> = ({ children }) => {
  const drawerWidth = 230;

  return (
    <Box display="flex" mt={2}>
      <Drawer variant="persistent" anchor="left" open>
        <List>
          {menuItems.map(({ text, chip, chipColor, link }) => (
            <ListItem key={text} disablePadding>
              <ListItemButton component={Link} to={link || '#'}>
                <ListItemText
                  primary={
                    <Box display="flex" alignItems="center" justifyContent="space-between">
                      <Typography variant="body1">{text}</Typography>
                      {chip && <Chip label={chip} color={chipColor as any} size="small" />}
                    </Box>
                  }
                />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Drawer>

      <Box
        sx={{
          flexGrow: 1,
          ml: `${drawerWidth}px`,
          transition: 'margin-left 0.3s ease',
        }}
      >
        {children}
      </Box>
    </Box>
  );
};