import React from 'react';
import { MainDrawer } from '../components/main-drawer';
import AppHeader from '../components/layout/header';

interface IProps {
  children: React.ReactNode;
}

const MainLayout: React.FC<IProps> = ({ children }) => {
  return (
    <MainDrawer>
      <AppHeader />
      {children}
    </MainDrawer>
  );
};

export default MainLayout;
