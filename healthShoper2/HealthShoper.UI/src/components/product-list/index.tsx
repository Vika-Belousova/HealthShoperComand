import { Grid, Box } from '@mui/material';
import { ProductCard } from '../product-card';
import { useCartStore } from '../../store/use-cart-store';
import type { IProduct } from '../../infrastructure/interfaces';
import { useProductStore } from '../../store/use-product-store';
import { useEffect } from 'react';

export const ProductList = () => {
  const addToCart = useCartStore(state => state.addToCart);
  const { fetchProducts, loading, products } = useProductStore();

  const handleAddToCart = (product: IProduct) => {
    addToCart(product);
  };

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  if (loading) return <div>Загрузка товаров...</div>;

  return (
    <Box p={3}>
      <Grid container spacing={3} justifyContent="center">
        {products.map(p => (
          <Grid key={p.id}>
            <ProductCard product={p} onAddToCart={handleAddToCart} />
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};
