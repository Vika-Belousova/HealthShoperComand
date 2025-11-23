import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import type { IProduct } from '../infrastructure/interfaces';
import api from '../api/axios';

interface CartItem extends IProduct {
  quantity: number;
}

interface CartState {
  items: CartItem[];
  getFromBasket: () => Promise<void>;
  addToCart: (product: IProduct) => Promise<void>;
  removeFromCart: (id: number) => void;
  removeQuantity: (id: number) => void;
  clearCart: () => void;
  totalPrice: () => number;
}

export const useCartStore = create<CartState>()(
  persist(
    (set, get) => ({
      items: [],
      getFromBasket: async () => {
        const response = await api.get('/api/bucket/GetItemFromBucket');

        // response.data — это список товаров
        const serverItems: CartItem[] = response.data.map((i) => ({
          id: i.id,
          title: i.title,
          description: i.description,
          image: i.id == 1 ? `/card/card1.jpg` : `/card/card1${i.id}.jpg`,
          price: i.price,
          quantity: i.count,
        }));

        set({ items: serverItems });
      },
      addToCart: async product => {
        await api.post('/api/bucket/AddInBucket', JSON.stringify(product.id), {
          headers: { 'Content-Type': 'application/json' },
        });

        // Обновить корзину после добавления товара
        await get().getFromBasket();
      },

      // ======================== REMOVE QUANTITY ========================
      removeQuantity: async id => {
        await api.delete(`/api/bucket/DeleteFromBucket/${id}`);

        await get().getFromBasket();
      },

      // ======================== REMOVE ITEM ========================
      removeFromCart: async id => {
        // Удаляем полностью (до 0)
        await api.post('/api/bucket/DeleteFromBucket', JSON.stringify(id), {
          headers: { 'Content-Type': 'application/json' },
        });

        await get().getFromBasket();
      },

      clearCart: () => set({ items: [] }),

      totalPrice: () => {
        return get().items.reduce((acc, item) => acc + item.price * item.quantity, 0);
      },
    }),
    {
      name: 'cart-storage',
    }
  )
);
