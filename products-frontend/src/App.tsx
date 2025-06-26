import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import ProductsTable from './Pages/ProductsTable/ProductsTable';
import ProductsChart from './Pages/ProductsChart/ProductsChart';


function App() {
  return (
    <div className='App'>
      <Router>
          <Routes>
              <Route path="/" element={<ProductsTable />} />
              <Route path="/charts" element={<ProductsChart />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
