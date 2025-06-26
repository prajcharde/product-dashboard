import React, { useEffect, useState } from 'react';
import { Product } from '../../Interfaces/Product';
import { GetProductList } from '../../Services/ProductsService';
import { Box, Button, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TableSortLabel, TextField, Typography } from '@mui/material';
import AddProductModal from '../AddProductModal/AddProductModal';
import { useNavigate } from 'react-router-dom';
type Order = 'asc' | 'desc';
const sortableFields: (keyof Product)[] = [
    'name',
    'productCode',
    'categoryName',
    'price',
    'stockQuantity',
    'dateAdded'
];

const ProductsTable: React.FunctionComponent = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [filtered, setFiltered] = useState<Product[]>([]);
    const [productCodeFilter, setProductCodeFilter] = useState('');
    const [nameFilter, setNameFilter] = useState('');
    const [orderBy, setOrderBy] = useState<keyof Product>('name');
    const [order, setOrder] = useState<Order>('asc');
    const [openModal, setOpenModal] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        fetchProducts();
    }, []);

    useEffect(() => {
        let updated = [...products];

        if (productCodeFilter) {
            updated = updated.filter(p =>
                p.productCode.toLowerCase().includes(productCodeFilter.toLowerCase())
            );
        }

        if (nameFilter) {
            updated = updated.filter(p =>
                p.name.toLowerCase().includes(nameFilter.toLowerCase())
            );
        }

        setFiltered(updated);
    }, [productCodeFilter, nameFilter, products]);

    const fetchProducts = async () => {
        try {
            const data = await GetProductList();
            setProducts(data);
            setFiltered(data);
        } catch (error) {
            console.error('Error fetching Product data:', error);
        }
    };

    const handleSort = (field: keyof Product) => {
        const isAsc = orderBy === field && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(field);
        setFiltered((prev) =>
            [...prev].sort((a, b) => {
                const aVal = a[field];
                const bVal = b[field];

                if (typeof aVal === 'string' && typeof bVal === 'string') {
                    return isAsc
                        ? aVal.localeCompare(bVal)
                        : bVal.localeCompare(aVal);
                }

                return isAsc
                    ? (aVal as number) - (bVal as number)
                    : (bVal as number) - (aVal as number);
            })
        );
    };

    const viewChart = () => {
        if (!products.length) {
            alert("No Data Available");
        } else
            navigate('/charts', { state: { products } });
    }

    return (
        <>
            <Typography color="secondary" variant="h3"> Product List</Typography>
            <Box>
                <Stack direction="row" justifyContent="flex-end" spacing={2} sx={{ mb: 2 }}>
                    <Button variant="contained" onClick={() => setOpenModal(true)}>Add Products</Button>
                    <Button variant="contained" onClick={viewChart}>View Chart</Button>
                </Stack>
                <Stack direction="row" spacing={2} sx={{ mb: 2 }}>
                    <TextField
                        label="Filter by Product Code"
                        size="small"
                        value={productCodeFilter}
                        onChange={(e) => setProductCodeFilter(e.target.value)}
                    />
                    <TextField
                        label="Filter by Name"
                        size="small"
                        value={nameFilter}
                        onChange={(e) => setNameFilter(e.target.value)}
                    />
                </Stack>

                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                {sortableFields.map((field) => (
                                    <TableCell key={field}>
                                        <TableSortLabel
                                            active={orderBy === field}
                                            direction={orderBy === field ? order : 'asc'}
                                            onClick={() => handleSort(field)}
                                        >
                                            {field.charAt(0).toUpperCase() + field.slice(1)}
                                        </TableSortLabel>
                                    </TableCell>
                                ))}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filtered.map((product, key) => (
                                <TableRow key={key}>
                                    <TableCell>{product.name}</TableCell>
                                    <TableCell>{product.productCode}</TableCell>
                                    <TableCell>{product.categoryName}</TableCell>
                                    <TableCell>{product.price}</TableCell>
                                    <TableCell>{product.stockQuantity}</TableCell>
                                    <TableCell>{new Date(product.dateAdded).toLocaleDateString('en-GB', {
                                        day: '2-digit',
                                        month: 'short',
                                        year: 'numeric',
                                        hour: '2-digit',
                                        minute: '2-digit'
                                    })}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <AddProductModal
                    open={openModal}
                    onClose={() => setOpenModal(false)}
                    onSuccess={fetchProducts}
                />
            </Box>
        </>
    );
}

export default ProductsTable;