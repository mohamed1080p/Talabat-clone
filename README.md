#  Talabat Clone API

A robust backend API for an e-commerce platform inspired by Talabat, built with **ASP.NET Core** and **MSSQL**, following **Onion Architecture**. It provides full-featured product management, shopping cart (basket) with Redis, order processing, and JWT-based authentication with role-based authorization (Admin, SuperAdmin, User).

---

##  Features

### Authentication & Authorization
- ASP.NET Core Identity with custom roles (Admin, SuperAdmin, User)
- JWT token generation and validation
- Login, Register, and additional account management endpoints
- Seeded Admin and SuperAdmin users

### Product Management
- Products, Brands, and Types with relationships
- Full CRUD operations via REST endpoints
- Filtering, sorting, searching, and pagination
- Product images with URL resolution
- Redis caching for improved performance

### Shopping Cart (Basket)
- Redis-based basket persistence
- Add/remove items, update quantities
- Dedicated Basket controller and services

### Order Processing
- Order module with models, configurations, and services
- Create orders from basket, retrieve order history
- Order item picture resolver for DTO mapping
- Comprehensive Order endpoints

### Advanced Architecture & Patterns
- Repository & Unit of Work patterns for data access
- Specification pattern for dynamic queries (filtering, sorting, includes)
- Generic Repository with async support
- Service Layer (ServiceManager, ProductService, OrderService, etc.)
- AutoMapper for object-object mapping
- Custom exception handling middleware
- Pagination, sorting, filtering as reusable components

### Error Handling & Validation
- Global exception handler middleware
- Custom exceptions (e.g., `ProductNotFoundException`)
- Not Found endpoint handling
- Validation error responses

---

##  Technologies Used

| Technology | Purpose |
|---|---|
| .NET 8 – ASP.NET Core Web API | Core framework |
| Entity Framework Core | ORM for MSSQL |
| MSSQL | Primary database |
| Redis | Basket storage & caching |
| ASP.NET Core Identity | User management |
| JWT (JSON Web Tokens) | Authentication |
| AutoMapper | DTO/entity mapping |
| Swagger / OpenAPI | API documentation |
| Git | Version control |

---

##  Architecture & Design Patterns

The project follows **Onion Architecture**, where dependencies flow strictly inward — the Domain layer at the core has no external dependencies, and outer layers (Infrastructure, Presentation) depend on inner ones, never the reverse. This ensures a clean separation of concerns across all projects:

- **Domain (Core)** – The heart of the application. Contains business entities, contracts (interfaces), and exceptions. Has zero dependencies on any other layer or framework.
- **Service Layer (Core)** – Business logic (ProductService, OrderService, AuthService, BasketService). Depends only on Domain contracts, never on infrastructure details.
- **Infrastructure Layer** – Implements the contracts defined in the Domain. Handles data access via Generic Repository, Unit of Work, EF Core, Redis, and Identity.
- **Presentation Layer** – Handles HTTP requests/responses via Controllers and delegates work to the Service Layer.
- **Shared Layer** – Cross-cutting concerns such as DTOs, error models, and pagination helpers used across multiple layers.

Additional patterns applied throughout the project:
- **Specification Pattern** – Encapsulates dynamic query logic (filtering, sorting, includes)
- **Middleware** – Custom global exception handling
- **Caching** – Redis distributed cache for improved performance

This architecture ensures testability, maintainability, and scalability as the project grows.


---

## 📁 Project Structure

```
📦 E-Commerce.Web (Solution - 7 Projects)
│
├── 📦 Core
│   ├── 📦 Domain
│   │   ├── Contracts/
│   │   ├── Exceptions/
│   │   └── Models/
│   │
│   └── 📦 Services
│       ├── MappingProfiles/
│       ├── Specifications/
│       ├── ApplicationServicesRegistration.cs
│       ├── AssemblyReference.cs
│       ├── AuthenticationService.cs
│       ├── BasketService.cs
│       ├── CacheService.cs
│       ├── OrderService.cs
│       ├── ProductService.cs
│       ├── ServiceManager.cs
│       ├── ServiceManagerWithFactoryDelegate.cs
│       └── ServicesAbstraction.cs
│
├── 📦 Infrastructure
│   └── 📦 Persistence
│       ├── Data/
│       ├── DataSeedFiles/
│       ├── Identity/
│       ├── Repositories/
│       ├── DataSeeding.cs
│       ├── InfrastructureServicesRegisteration.cs
│       └── SpecificationEvaluator.cs
│
├── 📦 Presentation
│   └── Controllers/
│       ├── ApiBaseController.cs
│       ├── AuthenticationController.cs
│       ├── BasketController.cs
│       ├── OrdersController.cs
│       └── ProductsController.cs
│
├── 📦 E-Commerce.Web
│   ├── CustomMiddleWares/
│   ├── Extensions/
│   ├── Factories/
│   ├── wwwroot/
│   ├── appsettings.json
│   └── Program.cs
│
└── 📦 Shared
    ├── DataTransferedObjects/
    │   ├── BasketModuleDTOs/
    │   ├── IdentityDTOs/
    │   ├── OrderDTOs/
    │   └── ProductModuleDTOs/
    ├── ErrorModels/
    ├── PaginatedResult.cs
    ├── ProductQueryParams.cs
    └── ProductSortingOptions.cs
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (or LocalDB)
- Redis (local or cloud instance like Redis Labs)

### Setup Instructions

**1. Clone the repository**
```bash
git clone https://github.com/mohamed1080p/Talabat-clone
cd talabat-clone-api
```

**2. Configure the database & Redis**

Update `appsettings.json` with your connection strings:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TalabatDB;Trusted_Connection=True;",
    "Redis": "localhost:6379"
  },
  "JWT": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "DurationInDays": 30
  }
}
```

**3. Apply database migrations**
```bash
dotnet ef database update
```
This will create the MSSQL database and seed initial data (products, brands, types, admin users).

**4. Run Redis (if using locally)**
```bash
redis-server
```

**5. Run the application**
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or `http://localhost:5000`).  
Swagger UI can be accessed at `/swagger`.

---

## ⚙️ Configuration

Key settings in `appsettings.json`:

| Section | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | MSSQL connection string |
| `ConnectionStrings:Redis` | Redis connection string (host:port) |
| `JWT` | JWT parameters (Key, Issuer, Audience, Duration) |
| `Logging` | Log levels |
| `AllowedHosts` | CORS / host restrictions |

---

## 📡 API Endpoints

### Account (`/api/account`)

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/login` | User login | Anonymous |
| POST | `/register` | User registration | Anonymous |
| GET | `/currentuser` | Get current logged-in user | Authenticated |

### Products (`/api/products`)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/` | Get all products (with filter/sort/pagination) |
| GET | `/{id}` | Get product by ID |
| GET | `/brands` | Get all product brands |
| GET | `/types` | Get all product types |

### Basket (`/api/basket`)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/{id}` | Get basket by ID |
| POST | `/` | Create or update basket |
| DELETE | `/{id}` | Delete basket |

### Orders (`/api/orders`)

| Method | Endpoint | Description |
|---|---|---|
| POST | `/` | Create order from basket |
| GET | `/` | Get orders for current user |
| GET | `/{id}` | Get order by ID for current user |

---

## 🌱 Seeding

The project includes automatic seeding on first run:

- **Products, Brands, Types** – Sample data for demonstration
- **Identity Roles** – "Admin", "SuperAdmin", "User"
- **Admin/SuperAdmin Users** – Default credentials seeded via `DataSeeding.cs`

To modify seeding, see `Infrastructure/Persistence/DataSeedFiles/`.

---

## 🤝 Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

Distributed under the MIT License.

---

<div align="center">Built with 💙 by Mohamed Bahaa</div>
