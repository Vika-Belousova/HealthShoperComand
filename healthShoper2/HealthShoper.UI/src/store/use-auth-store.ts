import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import api from '../api/axios';

interface User {
  email: string;
  name: string;
}

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<boolean>;
  register: (name: string, email: string, password: string) => Promise<boolean>;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      isAuthenticated: false,

      login: async (email, password) => {
        if (email && password) {
          const response = await api.post("/api/auth/logIn", {
            email,
            password,
          });
          localStorage.setItem("token", response.data.accessToken)

          const aboutMe = await api.get("/api/client/me");
          
          set({ user: { email: aboutMe.data.email, name: aboutMe.data.firstName }, isAuthenticated: true });
          return true;
        }
        return false;
      },

      register: async (name, email, password) => {
        if (name && email && password) {
          console.log(name);
          const response = await api.post("/api/auth/registration", {
            firstName: name,
            email,
            password,
          });

          localStorage.setItem("token", response.data.accessToken)

          const aboutMe = await api.get("/api/client/me");
          
          set({ user: { email: aboutMe.data.email, name: aboutMe.data.firstName }, isAuthenticated: true });
          return true;
        }
        console.log("TEST");
        
        return false;
      },

      logout: () => {
        localStorage.removeItem("token")
        set({ user: null, isAuthenticated: false })
      },
    }),
    {
      name: 'auth-storage',
    }
  )
);
