
import React from 'react';
import { BarChart } from '@mui/x-charts/BarChart';
import { Navigate, useLocation } from 'react-router-dom';
import { Product } from '../../Interfaces/Product';
import { Typography } from '@mui/material';

const ProductsChart: React.FunctionComponent = () => {
    const grouped: { [key: string]: number } = {};

    const location = useLocation();
    const products = location.state?.products ?? [];

    if (!products.length) {
        alert('No Data Available');
        return <Navigate to="/" />;
    }

    products.forEach((p: Product) => {
        grouped[p.categoryName] = (grouped[p.categoryName] || 0) + p.stockQuantity;
    });

    const xAxisLabels = Object.keys(grouped);
    const seriesData = Object.values(grouped);

    return (
        <>
            <Typography color="secondary" variant="h3"> Products Chart</Typography>
            <BarChart
                xAxis={[{ scaleType: 'band', data: xAxisLabels }]}
                series={[{ label: 'Total Stock', data: seriesData }]}
                width={600}
                height={400}
            />
        </>
    );
};

export default ProductsChart;