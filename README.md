Order Management - Sales Analytics API
1. About
.NET 9 Web API
Reads sales data from CSV
Stores data in PostgreSQL
Provides analytics APIs
2. What it does
Load CSV data into database
Normalized tables (customer, product, order, etc.)
Get top selling products
Filter by category, region, date
Trigger data refresh via API
Maintain refresh logs
3. Prerequisites
.NET 9 SDK
PostgreSQL running
Database name: 
4. How to run
Clone repository
Create database
createdb 
Update connection string
Order.API/appsettings.json
Add CSV file
Order.API/data/sales_data.csv
Run API
cd Order.API
dotnet run
Open
http://localhost:5295
5. Database tables
customer - customer details
product - product info
product_category - category details
order - order data
order_item - order line items
refresh_log - import logs
6. Relationships
order -> customer
order_item -> order
order_item -> product
product -> product_category
7. API Endpoints
GET /api/analysis/top-products
GET /api/analysis/top-products/by-category
GET /api/analysis/top-products/by-region
POST /api/refresh/trigger
GET /api/refresh/logs
8. Example usage
Top products
/api/analysis/top-products?n=3&startDate=2023-01-01&endDate=2024-12-31
By category
/api/analysis/top-products/by-category?category=Electronics
Refresh data
POST /api/refresh/trigger
9. CSV format

Order ID
Product ID
Customer ID
Product Name
Category
Region
Date of Sale
Quantity Sold
Unit Price
Discount
Shipping Cost
Payment Method
Customer Name
Customer Email
Customer Address

10. Project structure
Order.API - controllers
Order.Model - entities
Order.Dto - DTOs
Order.Repository - DB and CSV logic
Order.Services - business logic
11. Notes
Ensure PostgreSQL is running
Verify CSV file path
Do not commit secrets