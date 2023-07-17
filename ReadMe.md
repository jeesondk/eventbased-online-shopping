# Online Shopping System

## Task
Design and implement a simple application simulating basic online shopping system, where users can browse products, add them to their shopping carts, and place orders. At the end of each month application sends an email to customers with a list of all of their transactions as a PDF. Design for accessibility, quality, high availability, fault tolerance and European scale.

### Requirements
1. Provide a high-level design for the application, 
2. Demonstrating how the components interact and communicate

Focus on showcasing your skills, style, knowledge of patterns, tools, and ways of thinking.
It is less important to deliver a feature complete application, but nevertheless it should run :)

## Application Design

### Libraries
- MassTransit
- Marten
- FluentValidation
- Swagger/ OpenAPI

### Docker images
- MassTransit RabbitMQ
- Postgres SQL
- .NET SDK 6.0
- .NET ASPNET 6.0
- .NET Runtime 6.0

### Design patterns
Event Driven

### Concerns
Below are concers listed that I found throughout the design.

#### Eventual consistency
as we are using events, aggregates and projections will have eventual consistency

### Runtime
Docker files with Docker Compose

#### Docker Compose design

public endpoint for NGINX container (Reverse Proxy) -> proxying Internal Network

Internal network for Kafka and Zookeeper, Service.

Each Service should have a pgSql sidecar and only expose ports for I/O

### Microservices

#### User service
Manages users (profiles etc.)

User class definition:
- First Name
- Last Name
- Address
- City
- Zip code
- Country
- email address
- password hash


#### Product Catalog Service
List, Add, Update, Remove product as well as stock keeping

Procuct class defninition:
- SKU
- Name
- price

#### Shopping Cart Service
Manages users active shopping carts (state)

#### Order Management Service
Handle orders: Place, Process, Cancel, Return, Ship as well as report generation


#### API Gateway
Presentation layer, OpenAPI 3 spec with SwaggerUI and OAuth2
