# Order Management System

This project is an Order Management System built using a microservices architecture. It utilizes .NET Core for the backend services, Angular for the frontend, SQL Server for data storage, and RabbitMQ for inter-service communication. The system is designed to manage orders, products, and customers efficiently.

## Project Structure

```
order-management-system
├── backend
│   ├── OrderService
│   ├── ProductService
│   └── CustomerService
├── frontend
└── docker-compose.yml
```

### Backend Services

- **Order Service**: Manages operations related to orders.
- **Product Service**: Manages operations related to products.
- **Customer Service**: Manages operations related to customers.

Each service has its own database and communicates with others using RabbitMQ for asynchronous messaging.

### Frontend

The frontend is built using Angular and provides a user interface for interacting with the order management system. Users can view products, create orders, and view order details.

## Features

- **Microservices Architecture**: Each service is independently deployable and scalable.
- **Asynchronous Communication**: Services communicate via RabbitMQ, ensuring decoupled interactions.
- **Comprehensive Validations**: Validations are implemented on all endpoints to ensure data integrity.
- **Authentication and Authorization**: JWT is used for securing the microservices.
- **Logging**: Each service implements logging using Serilog or NLog.
- **Unit Testing**: Each service includes unit tests to ensure reliability and correctness.

## Getting Started

1. Clone the repository:
   ```
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```
   cd order-management-system
   ```

3. Run the application using Docker:
   ```
   docker-compose up
   ```

4. Access the frontend application at `http://localhost:4200`.

## Technologies Used

- **Backend**: .NET Core, C#
- **Frontend**: Angular
- **Database**: SQL Server
- **Messaging**: RabbitMQ
- **Testing**: xUnit
- **Logging**: Serilog/NLog

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for details.