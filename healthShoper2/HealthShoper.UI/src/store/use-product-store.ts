import { create } from 'zustand';
import { devtools, persist } from 'zustand/middleware';
import type { IProduct } from '../infrastructure/interfaces';
import api from '../api/axios';

interface ProductState {
  products: IProduct[];
  loading: boolean;
  error: string | null;
  fetchProducts: () => Promise<void>;
  getProductById: (id: number) => IProduct | undefined;
}

export const useProductStore = create<ProductState>()(
  devtools(
    persist(
      (set, get) => ({
        products: [],
        loading: false,
        error: null,
        fetchProducts: async () => {
          try {
            set({ loading: true, error: null });
            const response = await api.get('/api/item/getall');
            await new Promise(r => setTimeout(r, 800));

            const products: IProduct[] = response.data.map(item => ({
              id: item.id,
              title: item.name ?? '',
              description: item.description ?? '',
              price: item.priceWithDiscount == null ? item.price : item.priceWithDiscount,
              image: item.id == 1 ? `/card/card1.jpg` : `/card/card1${item.id}.jpg`,
            }));

            set({ products: products, loading: false });
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
          } catch (err) {
            set({ error: 'Ошибка загрузки товаров', loading: false });
          }
        },

        getProductById: (id: number) => {
          return get().products.find(p => p.id === id);
        },
      }),
      {
        name: 'product-storage',
      }
    )
  )
);
