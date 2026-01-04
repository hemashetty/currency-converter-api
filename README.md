Currency Converter API
A robust and scalable Currency Converter API built with ASP.NET Core, designed using Clean Architecture principles.
The API integrates with an external exchange rate provider and demonstrates production-ready practices such as security, resilience, rate limiting, and observability.
________________________________________
	Features
•	Retrieve latest exchange rates
•	Convert currencies between different pairs
•	Retrieve historical exchange rates with pagination
•	Blocked currency validation (TRY, PLN, THB, MXN)
•	JWT Authentication
•	Role-Based Access Control (RBAC)
•	API Rate Limiting (Throttling)
•	Resilience using Polly (Retry + Circuit Breaker)
•	Request logging with correlation ID
•	Clean, maintainable architecture
________________________________________
	Architecture
The solution follows Clean Architecture, separating concerns clearly:
CurrencyConverter.Api
 ├── Controllers
 ├── Middleware
 ├── Authentication
 └── Program.cs



CurrencyConverter.Application
 ├── Interfaces
 ├── Services
 ├── Factories
 ├── DTOs
 └── Exceptions

CurrencyConverter.Infrastructure
 └── Providers (External API integrations)

CurrencyConverter.Tests
 ├── Unit
 └── Integration
 
	Design Principles
•	Dependency Inversion (Application depends only on abstractions)
•	Infrastructure contains external integrations
•	Thin controllers, business logic in services
•	Framework-agnostic Application layer
________________________________________
	Security
•	JWT Bearer authentication
•	Tokens include:
o	clientId
o	role (Admin / User)
•	API endpoints are protected using role-based authorization
•	Swagger supports authenticated testing via Authorize 🔒
________________________________________

	Rate Limiting
•	Fixed-window rate limiting
•	10 requests per minute per client
•	Exceeding limit returns HTTP 429 (Too Many Requests)
•	Applied globally after authentication
________________________________________
	Resilience & Performance
•	HttpClient + Polly
o	Retry with exponential backoff
o	Circuit breaker to prevent cascading failures
•	In-memory caching for latest exchange rates
•	Protects the API from temporary external service failures
________________________________________
	Logging & Monitoring
Custom middleware logs the following for each request:
•	Client IP address
•	ClientId (from JWT token)
•	HTTP method and endpoint
•	Response status code
•	Response time (milliseconds)
•	Correlation ID (returned in response headers)
Example log:
GET /api/v1/convert | 200 | 120ms | ClientId=hema123 | CorrelationId=abc-xyz
________________________________________




	API Endpoints
Authentication
POST /api/auth/token
Request body:
{
  "clientId": "hema123",
  "role": "User"
}
________________________________________
Latest Exchange Rates
GET /api/v1/rates/latest?base=EUR
________________________________________
Currency Conversion
GET /api/v1/convert?from=EUR&to=USD&amount=10
________________________________________
Historical Exchange Rates (Paginated)
GET /api/v1/rates/historical
  ?base=EUR
  &from=2020-01-01
  &to=2020-01-10
  &page=1
  &pageSize=5
________________________________________
	Blocked Currencies
The following currencies are not supported:
•	TRY
•	PLN
•	THB
•	MXN
Requests involving these currencies return:
400 Bad Request
"Currency not supported"
________________________________________
	Running the Project
Prerequisites
•	.NET 8 SDK
•	Visual Studio 2022 professional
Steps
dotnet restore
dotnet build
dotnet run --project CurrencyConverter.Api
Swagger UI:
http://localhost:<port>/swagger
________________________________________
	Testing
•	Unit tests cover core business logic and validation rules using mocks
•	External API calls are mocked in unit tests
•	An integration test using WebApplicationFactory is included to demonstrate API-level testing
Note
Integration tests may require additional runtime configuration (testhost.deps.json) depending on the local environment.
In a production setup, this would typically be handled via CI pipelines or containerized test execution.
________________________________________


	Assumptions
•	External exchange rate provider availability
•	In-memory caching is sufficient for this exercise
•	Single exchange rate provider (designed to be extensible)
•	Stateless authentication using JWT
________________________________________
	Future Enhancements
•	Redis or distributed caching
•	Support for multiple exchange rate providers
•	OpenTelemetry tracing
•	Centralized logging (ELK / Seq)
•	Kubernetes deployment
•	CI/CD pipeline with automated tests and coverage reports
________________________________________
👤 Author
Hemadevi
Senior Backend Developer – C# / ASP.NET Core
Clean Architecture • Secure APIs • Resilient Systems

