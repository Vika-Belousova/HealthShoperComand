import { Route, Routes } from 'react-router-dom';
import HomePage from './pages/home';
import { CartPage } from './pages/cart';
import { routes } from './infrastructure/consts/routes';
import MainLayout from './layout/main';
import ArticlesPage from './pages/articles';
import ReviewsPage from './pages/reviews';
import PartnersPage from './pages/partners';
import ContactsPage from './pages/contacts';
import { LoginPage } from './pages/login';
import { RegisterPage } from './pages/register';
import { CheckoutPage } from './pages/checkout';
import { OrdersPage } from './pages/orders';
import { BackPainPage } from './pages/back-pain';

const withMainLayout = (Component: React.FC) => (
  <MainLayout>
    <Component />
  </MainLayout>
);

function App() {
  return (
    <Routes>
      <Route path={routes.home} element={withMainLayout(HomePage)} />
      <Route path={routes.cart} element={withMainLayout(CartPage)} />
      <Route path={routes.articles} element={withMainLayout(ArticlesPage)} />
      <Route path={routes.reviews} element={withMainLayout(ReviewsPage)} />
      <Route path={routes.partners} element={withMainLayout(PartnersPage)} />
      <Route path={routes.contacts} element={withMainLayout(ContactsPage)} />
      <Route path={routes.login} element={<LoginPage />} />
      <Route path={routes.register} element={<RegisterPage />} />
      <Route path="/checkout" element={withMainLayout(CheckoutPage)} />
      <Route path="/orders" element={withMainLayout(OrdersPage)} />
      <Route path="/back-pain" element={withMainLayout(BackPainPage)} />
    </Routes>
  );
}

export default App;