import React from 'react';
import { fireEvent, render, screen } from '@testing-library/react';
import * as productsService from '../../Services/ProductsService';
import ProductsTable from './ProductsTable';

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));

jest.mock('../../Services/ProductsService', () => ({
    GetProductList: jest.fn(),
}));

test('renders component and title', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    expect(screen.getByText('Product List')).toBeInTheDocument();
});

test('filter inputs are present', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    expect(screen.getByLabelText('Filter by Product Code')).toBeInTheDocument();
    expect(screen.getByLabelText('Filter by Name')).toBeInTheDocument();
});

test('fetches products on mount', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    expect(productsService.GetProductList).toHaveBeenCalled();
});

test('"Add Products" and "View Chart" buttons are present', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    expect(screen.getByText('Add Products')).toBeInTheDocument();
    expect(screen.getByText('View Chart')).toBeInTheDocument();
});


test('table headers are rendered with sortable fields', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    const sortableFields = ['Name', 'ProductCode', 'CategoryName', 'Price', 'StockQuantity', 'DateAdded'];
    sortableFields.forEach(field => {
        expect(screen.getByText(field)).toBeInTheDocument();
    });
});

test('table body is rendered', () => {
    (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
    render(<ProductsTable />);
    expect(screen.getByRole('table')).toBeInTheDocument();
});

test('Add Products button opens modal', async () => {
  (productsService.GetProductList as jest.Mock).mockResolvedValue([]);
  render(<ProductsTable />);
  const addButton = screen.getByText('Add Products');
  fireEvent.click(addButton);
  expect(screen.getByText('Add New Product')).toBeInTheDocument();
});
