import React from 'react';
import { Card, CardMedia, CardContent, CardActions, Typography, Button } from '@mui/material';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import type { IProduct } from '../../infrastructure/interfaces';


interface ProductCardProps {
  product: IProduct;
  onAddToCart?: (product: IProduct) => void;
}

export const ProductCard: React.FC<ProductCardProps> = ({ product, onAddToCart }) => {
  
  return (
    <Card
      sx={{
        maxWidth: 280,
        borderRadius: 3,
        boxShadow: '0 4px 20px rgba(0,0,0,0.1)',
        transition: 'transform 0.2s ease, box-shadow 0.2s ease',
        '&:hover': {
          transform: 'translateY(-4px)',
          boxShadow: '0 6px 25px rgba(0,0,0,0.15)',
        },
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'space-between',
      }}
    >
      <CardMedia
        component="img"
        height="180"
        image={product.image}
        alt={product.title}
        sx={{ objectFit: 'cover', borderTopLeftRadius: 12, borderTopRightRadius: 12 }}
      />

      <CardContent>
        <Typography gutterBottom variant="subtitle1" fontWeight={600}>
          {product.title}
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
          {product.description}
        </Typography>
        <Typography variant="h6" color="primary">
          {product.price.toLocaleString()} ₽
        </Typography>
      </CardContent>

      <CardActions sx={{ p: 2, pt: 0 }}>
        <Button
          fullWidth
          variant="contained"
          startIcon={<ShoppingCartIcon />}
          onClick={() => onAddToCart?.(product)}
          sx={{
            textTransform: 'none',
            borderRadius: 2,
            fontWeight: 600,
          }}
        >
          Добавить в корзину
        </Button>
      </CardActions>
    </Card>
  );
};
