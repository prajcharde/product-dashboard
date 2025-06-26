import React, { useEffect, useState } from "react";
import { AddProduct } from "../../Interfaces/AddProduct";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, MenuItem, Stack, TextField } from "@mui/material";
import { GetCategoryList } from "../../Services/CategoriesService";
import { Category } from "../../Interfaces/Category";
import { AddProductData } from "../../Services/ProductsService";

interface AddProductModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

const AddProductModal: React.FunctionComponent<AddProductModalProps> = ({ open, onClose, onSuccess }) => {

    const [formData, setFormData] = useState<AddProduct>({
        name: '',
        productCode: '',
        categoryId: 0,
        price: 0,
        stockQuantity: 0
    });
    const [categories, setCategories] = useState<Category[]>([]);

    useEffect(() => {
        if (open) {
            const fetchData = async () => {
                try {
                    const data = await GetCategoryList();
                    setCategories(data);
                } catch (error) {
                    console.error('Error fetching category data:', error);
                }
            };
            fetchData();
        }
    }, [open]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: name === 'price' || name === 'stockQuantity'
                || name === 'categoryId' ? Number(value) : value,
        }));
    };

    const handleSubmit = async () => {
        try {
            await AddProductData(formData);
            alert("Product Added successfully");
            onSuccess();
            handleClose();
        } catch (error) {
            console.error(error);
            alert("Error in adding product");
        }
    };

    const handleClose = () => {
        onClose();
        setFormData({
            name: '',
            productCode: '',
            categoryId: 0,
            price: 0,
            stockQuantity: 0
        });
        setCategories([]);
    };

    const isFormValid = () => {
        return (
            formData.name.trim() !== '' &&
            formData.productCode.trim() !== '' &&
            formData.categoryId > 0 &&
            formData.price > 0 &&
            formData.stockQuantity > 0
        );
    };

    return (
        <>
            <Dialog open={open} onClose={onClose}>
                <DialogTitle>Add New Product</DialogTitle>
                <DialogContent>
                    <Stack spacing={2} sx={{ mt: 1, minWidth: 300 }}>
                        <TextField label="Name" name="name" value={formData.name} onChange={handleChange} fullWidth />
                        <TextField label="Product Code" name="productCode" value={formData.productCode} onChange={handleChange} fullWidth />
                        <TextField
                            select
                            label="Category"
                            name="categoryId"
                            value={formData.categoryId}
                            onChange={handleChange}
                            fullWidth
                        >
                            {categories.map((cat) => (
                                <MenuItem key={cat.id} value={cat.id}>{cat.name}</MenuItem>
                            ))}
                        </TextField>
                        <TextField label="Price" name="price" type="number" value={formData.price} onChange={handleChange} fullWidth />
                        <TextField label="Stock Quantity" name="stockQuantity" type="number" value={formData.stockQuantity} onChange={handleChange} fullWidth />
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button variant="contained"
                        onClick={handleSubmit} disabled={!isFormValid()}>Add</Button>
                </DialogActions>
            </Dialog>
        </>
    )
};
export default AddProductModal;