import axios from "axios";
import { Product } from "../Interfaces/Product";
import config from "./Config";
import { AddProduct } from "../Interfaces/AddProduct";

export const GetProductList = async (): Promise<Product[]> => {
    try {
        const response = await axios.get<Product[]>(`${config.apiUrl}Products`);
        return response.data;
    } catch (error) {
        console.error('Error fetching data:', error);
        return [];
    }
};

export const AddProductData = async (product: AddProduct) : Promise<Product> =>{
    try{
        const response = await axios.post<Product>(`${config.apiUrl}Products`, product);
        return response.data;
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
}