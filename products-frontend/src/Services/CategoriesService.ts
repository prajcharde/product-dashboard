import axios from "axios";
import { Category } from "../Interfaces/Category";
import config from "./Config";

export const GetCategoryList = async (): Promise<Category[]> => {
  try {
    const response = await axios.get<Category[]>(`${config.apiUrl}Categories`);
    return response.data;
  } catch (error) {
    console.error('Error fetching data:', error);
    return [];
  }
};