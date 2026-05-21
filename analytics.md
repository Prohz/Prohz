# Project Analytics: Kopke-Prohz/Prohz

## Project Overview

This is a comprehensive web application platform for Prohz, appearing to be a contractor/homeowner service marketplace or property management system. The project follows a layered .NET architecture with separation of concerns across multiple projects.

## Solution Structure

The solution consists of the following projects:

### Core API Layer
- **KopkeHome-FMRS_API**: Main ASP.NET Core Web API project containing all controllers and API endpoints

### Business Logic Layer
- **KopkeHome-BusinessLayer**: Contains business logic and service implementations

### Data Access Layer
- **KopkeHome-DataAccessLayer**: Handles database operations and repository patterns

### Model Layer
- **KopkeHome-ModelLayer**: Contains data models, view models, and DTOs
  - DataModel: Entity classes for database tables
  - ViewModels: Classes used for API request/response payloads
  - MembershipBenifits: Membership-related models
  - PaymentAndSubscription: Payment and subscription-related models
  - DashboardViewModels: Models specific to dashboard functionality

### Utility Layer
- **KopkeHome-UtilityLayer**: Shared utilities, helpers, and cross-cutting concerns

### Supporting Projects
- **KopkeHome-FMRS**: ASP.NET MVC/Web application (likely the frontend)
- **KopkeHomeMobile-FMRS**: Mobile application components
- **KopkeHome-LogManager**: Custom logging implementation
- **DatabaseSeed**: Database initialization and seeding scripts

## Key Features Identified

### 1. User Management & Authentication
- **AccountController** handles:
  - User registration (homeowner/contractor)
  - OTP verification for email confirmation
  - Password reset/forgot password flows
  - User sign-in/sign-out with JWT token generation
  - User profile updates
  - Role-based access control (Admin, HomeOwner, Contractor, SubContractor, etc.)

### 2. Membership & Subscription System
- **MembershipController** manages:
  - Membership plans listing and retrieval
  - Custom membership plan requests
  - Membership-zipcode-category mappings
  - Custom plan details by user

- **PaymentController** handles:
  - Stripe payment processing and subscription management
  - Custom plan creation with dynamic pricing
  - Subscription upgrades/downgrades/cancellations
  - Payment success/failure handling via Stripe webhooks
  - Stripe customer and subscription management

### 3. Business & Contractor Management
- **BusinessProfileController** manages:
  - Contractor business profile creation and updates
  - Sub-contractor profile management
  - Business verification and documentation

### 4. Dashboard & Search Functionality
- **DashboardController** provides:
  - Contractor search and filtering
  - Contractor profile details
  - Categories and zipcode listing
  - Review/like system for contractors

### 5. Administrative Functions
- **AdminController** handles:
  - User management and listing
  - Membership plan request approval
  - Document verification for users
  - FAQ management
  - Promo video management
  - Legal file management
  - Category management
  - Sales person management
  - Admin dashboard statistics

### 6. Contact & Communication
- **ContactController** manages:
  - Contact form submissions
  - Email sending capabilities

## Technical Architecture

### API Design Patterns
- RESTful API conventions with attribute routing
- `[Route("[controller]/[action]")]` pattern for consistent endpoint naming
- ApiController attributes for automatic model validation
- Proper HTTP verb usage (GET, POST) based on operation type

### Security Features
- JWT-based authentication with token generation on sign-in
- Role-based authorization checks throughout controllers
- Password hashing and secure token generation for password resets
- Input validation via ModelState checks
- Protection against common web vulnerabilities

### Data Handling
- Repository pattern for data access abstraction
- Service layer interfaces for business logic
- ViewModel separation for API contracts
- Async/await patterns for scalable I/O operations
- Proper exception handling and logging

### Payment Processing
- Stripe integration for subscription billing
- Secure handling of payment information
- Webhook endpoints for payment status updates
- Custom pricing capabilities for flexible plans
- Invoice generation and management

## Database Entities (Inferred from Models)

Based on the model references, the system likely includes:

1. **User/Account Entities**:
   - User base information (name, email, phone, address)
   - Role-based differentiation (homeowner, contractor, admin)
   - Authentication credentials and tokens
   - Verification status (email, documents, OTP)

2. **Membership/Subscription Entities**:
   - Membership plans and pricing tiers
   - User subscription records
   - Payment transaction history
   - Stripe customer and subscription IDs

3. **Business Profile Entities**:
   - Contractor business information
   - Service categories and specializations
   - Geographic service areas (zipcodes, cities)
   - Verification documents and status

4. **Content Management Entities**:
   - FAQ entries
   - Promotional videos
   - Legal documents
   - System configuration settings

## Current Development Focus

Based on the backend-routes.md documentation and code examination, the current work appears to be focused on:

1. **API Endpoint Development**: Complete REST API implementation for all core functionalities
2. **Payment Integration**: Robust Stripe payment processing with subscription management
3. **User Experience**: Role-based workflows and redirection logic
4. **Administrative Tools**: Comprehensive admin panel for system management
5. **Mobile/Web Readiness**: Endpoints designed to support both web and mobile clients

## Dependencies & Technologies

- **Framework**: .NET 8.0 (ASP.NET Core)
- **ORM/Data Access**: Likely Entity Framework or similar (based on repository pattern)
- **Payment Processing**: Stripe API
- **Logging**: Serilog (based on references)
- **Dependency Injection**: Built-in ASP.NET Core DI
- **JSON Processing**: Newtonsoft.Json
- **Email Services**: Custom email service implementation
- **Validation**: Data annotations and FluentValidation (inferred)
- **Testing**: Not evident in current exploration but expected for production system

## Next Steps / Recommendations

1. **Complete Documentation**: Generate OpenAPI/Swagger documentation from controllers
2. **Frontend Development**: Develop corresponding MVC views or SPA frontend
3. **Mobile Integration**: Ensure API endpoints are mobile-client friendly
4. **Testing Strategy**: Implement unit and integration tests
5. **Performance Optimization**: Review database queries and caching strategies
6. **Security Audit**: Perform penetration testing and security review
7. **Deployment Preparation**: Configure CI/CD pipelines and environment-specific settings

## Project Status

Based on the code examination, this appears to be a **feature-complete API layer** with:
- All core business functionalities implemented
- Proper separation of concerns through layered architecture
- Production-ready error handling and logging
- Secure authentication and authorization mechanisms
- Robust payment processing integration
- Comprehensive administrative capabilities

The system is ready for frontend development, testing, and deployment phases.

---
*Analytics generated on: 2026-05-20*