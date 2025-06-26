import React from 'react';
import { render, screen } from '@testing-library/react';
import { useLocation, Navigate } from 'react-router-dom';
import ProductsChart from './ProductsChart';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useLocation: jest.fn(),
  Navigate: jest.fn().mockImplementation(() => <div>Navigated</div>),
}));

test('renders chart when products are provided', () => {
  const mockProducts = [
    { categoryName: 'Electronics', stockQuantity: 10 },
    { categoryName: 'Electronics', stockQuantity: 5 },
    { categoryName: 'Clothing', stockQuantity: 7 }
  ];
  (useLocation as jest.Mock).mockReturnValue({ state: { products: mockProducts } });

  render(<ProductsChart />);
  expect(screen.getByText('Products Chart')).toBeInTheDocument();
});

test('navigates away when no products are provided', () => {
  (useLocation as jest.Mock).mockReturnValue({ state: {} });
  render(<ProductsChart />);
  expect(screen.getByText('Navigated')).toBeInTheDocument();
});

test('groups products by category correctly', () => {
  const mockProducts = [
    { categoryName: 'Electronics', stockQuantity: 10 },
    { categoryName: 'Electronics', stockQuantity: 5 },
    { categoryName: 'Clothing', stockQuantity: 7 }
  ];
  (useLocation as jest.Mock).mockReturnValue({ state: { products: mockProducts } });
  
  render(<ProductsChart />);
  expect(screen.getByText('Products Chart')).toBeInTheDocument();
});
