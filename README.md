# ProductAPI WebAPI Project

## Introduction
This project demonstrates how to create a WebAPI using ASP.NET with a `ProductController` implementing CRUD (Create, Read, Update, Delete) operations. The data is stored in a database using Entity Framework Code First approach.

## Requirements
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server (LocalDB or other)
- Postman (for testing the API)

## Project Setup

### Step 1: Create WebAPI Project
1. Open Visual Studio.
2. Create a new project.
3. Select **ASP.NET Web Application (.NET Framework)**.
4. Name the project `ProductAPI`.
5. Choose **Web API** template and click **Create**.

### Step 2: Install Entity Framework
1. Open **Package Manager Console** from Tools > NuGet Package Manager.
2. Run the command to install Entity Framework.

### Step 3: Add Product Model
1. Create a new folder named `Models`.
2. Add a `Product` class with properties: `ID`, `Name`, `Quantity`, `Price`, and `Description`.

### Step 4: Create ProductContext
1. Create a new folder named `Context`.
2. Add a `ProductContext` class to manage database access.

### Step 5: Initialize the Database
1. Add an `Initializer` class in the `Context` folder.
2. Define seed data to populate the database initially.

### Step 6: Configure Database Initializer
1. In `Global.asax`, set the database initializer to use your custom `Initializer`.

### Step 7: Add ProductController
1. Create a `ProductController` in the `Controllers` folder.
2. Implement methods for `GET`, `POST`, `PUT`, and `DELETE` operations.

### Step 8: Configure Routing
1. Ensure your `WebApiConfig` has default routes configured.

### Step 9: Test Endpoints in Postman
1. **Get All Products**: `GET http://localhost:[port]/api/Product`
2. **Get Product by ID**: `GET http://localhost:[port]/api/Product/{id}`
3. **Create Product**: `POST http://localhost:[port]/api/Product`
   - Use JSON body to define new product.
4. **Update Product**: `PUT http://localhost:[port]/api/Product/{id}`
   - Use JSON body to define updated product details.
5. **Delete Product**: `DELETE http://localhost:[port]/api/Product/{id}`
