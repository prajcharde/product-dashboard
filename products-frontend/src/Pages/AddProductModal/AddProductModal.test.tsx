
import { render, screen, waitFor } from '@testing-library/react';
import AddProductModal from './AddProductModal';
import * as categoriesService from '../../Services/CategoriesService';

jest.mock('../../Services/CategoriesService', () => ({
  GetCategoryList: jest.fn(),
}));
jest.mock('../../Services/ProductsService', () => ({
  AddProductData: jest.fn(),
}));

const mockCategories = [
  { id: 1, name: 'Category 1' },
  { id: 2, name: 'Category 2' },
];

test('renders when open', () => {
  (categoriesService.GetCategoryList as jest.Mock).mockResolvedValue(mockCategories);

  render(
    <AddProductModal
      open={true}
      onClose={jest.fn()}
      onSuccess={jest.fn()}
    />
  );

  expect(screen.getByText('Add New Product')).toBeInTheDocument();
});

test('fetches categories when opened', async () => {
  (categoriesService.GetCategoryList as jest.Mock).mockResolvedValue(mockCategories);

  render(
    <AddProductModal
      open={true}
      onClose={jest.fn()}
      onSuccess={jest.fn()}
    />
  );

  await waitFor(() => {
    expect(categoriesService.GetCategoryList).toHaveBeenCalled();
  });
});

test('Add button is disabled when form is invalid', () => {
  render(
    <AddProductModal
      open={true}
      onClose={jest.fn()}
      onSuccess={jest.fn()}
    />
  );
  expect(screen.getByText('Add')).toBeDisabled();
});

test('does not render when closed', () => {
  render(
    <AddProductModal
      open={false}
      onClose={jest.fn()}
      onSuccess={jest.fn()}
    />
  );
  expect(screen.queryByText('Add New Product')).not.toBeInTheDocument();
});

test('cancel button calls onClose', () => {
  const onCloseMock = jest.fn();
  render(
    <AddProductModal
      open={true}
      onClose={onCloseMock}
      onSuccess={jest.fn()}
    />
  );
  screen.getByText('Cancel').click();
  expect(onCloseMock).toHaveBeenCalled();
});

test('form fields are present', async () => {
  (categoriesService.GetCategoryList as jest.Mock).mockResolvedValue(mockCategories);
  render(
    <AddProductModal
      open={true}
      onClose={jest.fn()}
      onSuccess={jest.fn()}
    />
  );
  await waitFor(() => {
    expect(screen.getByLabelText('Name')).toBeInTheDocument();
    expect(screen.getByLabelText('Product Code')).toBeInTheDocument();
    expect(screen.getByLabelText('Category')).toBeInTheDocument();
    expect(screen.getByLabelText('Price')).toBeInTheDocument();
    expect(screen.getByLabelText('Stock Quantity')).toBeInTheDocument();
  });
});
