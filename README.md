# Product Management Dashboard

## Overview

**Product Management Dashboard** is a full-stack application for managing and displaying products. It features a RESTful API built with ASP.NET Core and a React frontend. The dashboard supports product creation, listing, filtering, and data visualization.

---

## Technologies Used

### Backend
- **ASP.NET Core Web API** (.NET 8)
- **Entity Framework Core**
- **SQLite** (Embedded database)

### Frontend
- **React** (with hooks)
- **Axios** (for API communication)
- **React Material UI** (for DataTable UI and Graphs)

---

## Features

### Backend API

- **Add a New Product**
  - `POST /api/products`
  - Adds a new product with details: Category, Name, Product Code, Price, Stock Quantity, and Date Added.

- **List All Products**
  - `GET /api/products`
  - Retrieves all products in the system.

- **List All Categories**
  - `GET /api/categories`
  - Retrieves all available categories.

### Frontend

- **DataTable View**
  - Displays all product details.
  - Sortable by all columns.
  - Filterable by Product Code and Name.

- **Graph View**
  - Bar chart showing total stock quantity per category.

---

## Setup Instructions

### Backend Setup

1. **Clone the repository:**
    ```
    git clone https://github.com/prajcharde/product-dashboard.git
    cd product-dashboard/ProductsWebAPI
    ```

2. **Setup the database and run migrations:**
    ```
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

3. **Run the API:**
    ```
    dotnet run
    ```

4. **API Swagger URL:**  
   `https://localhost:7196/swagger/index.html`

---

### Frontend Setup

1. **Navigate to frontend folder:**
    ```
    cd ../products-frontend
    ```

2. **Install dependencies:**
    ```
    npm install
    ```

3. **Run the frontend:**
    ```
    npm start
    ```

---

## Testing

**This project includes automated tests for both backend and frontend:**

- **Backend:** Uses xUnit and Moq to test API endpoints, business logic, and data validation.
- **Frontend:** Uses Jest and React Testing Library to test components, UI behavior, and API integration (mocked).

- **How to run:**
  - **Backend:**  
    ```
    cd ProductsWebAPI
    dotnet test
    ```
  - **Frontend:**  
    ```
    cd products-frontend
    npm test
    ```

---

## Design Decisions

- **SQLite:** Choosen for its simplicity and ease of local testing without external dependencies.
- **EF Core:** Used for database access to reduce boilerplate and leverage code-first migrations.
- **React + Axios:** Clean API interaction, and React’s component-based model keeps the UI modular.
- **Material UI/Table/Charts:** Fast development with built-in responsiveness and customizable UI.

---


*Last updated: June 26, 2025*
