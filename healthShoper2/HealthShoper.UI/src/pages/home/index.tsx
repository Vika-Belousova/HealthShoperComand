import React from 'react';
import SubHeader from '../../components/subheader';
import { MainContent } from '../../components/main-content';
import { ProductList } from '../../components/product-list';

const HomePage: React.FC = () => {
  return (
    <>
      <SubHeader />
      <MainContent />
      <ProductList />
    </>
  );
};

export default HomePage;
