# Online Shopping System


## Task
Design and implement a simple application simulating basic online shopping system, where users can browse products, add them to their shopping carts, and place orders. At the end of each month application sends an email to customers with a list of all of their transactions as a PDF. Design for accessibility, quality, high availability, fault tolerance and European scale.

### Requirements
1. Provide a high-level design for the application, 
2. Demonstrating how the components interact and communicate

### Instructions given
"Focus on showcasing your skills, style, knowledge of patterns, tools, and ways of thinking.
It is less important to deliver a feature complete application, but nevertheless it should run :)!"

## TL;DR
### How to get this running
Ensure that you have a docker runtime started and runnign on your computer.
I used Rancher Desktop with Mody (docker support)

1. build images:
    - OnlineShoppingSolution\API\Dockerfile
    - OnlineShoppingSolution\StoreService\Dockerfile
    - OnlineShoppingSolution\UserService\Dockerfile
2. run docker compose file
    - From the root of the repo, in your shell run: ´´´docker compose up´´´
    Now be patient, RabbitMQ take a while to start :)

The environmen is now running and you can see the Swagger api on: http://localhost:8900/swagger/index.html

## General approach
Time was not on my side, so I tried to showcase what I could with the time I had.
the User service was implemented using a TDD approach and the Git commits should show this.
I added only a minimal set of tests to services where I added tests.
I skipped tests on the REST API, as well as input validations by using FluentValidation and middleware.

I had intentions of added SpecFlow tests on the REST API as E2E testing, but time did not allow

## Application Design
### Libraries
- MassTransit
- Marten
- Swagger/ OpenAPI
- FluentAssertions
- NSubstitute
- EntityFramework Core
- NpgSQL

### Docker images
- MassTransit RabbitMQ
- Postgres SQL
- .NET SDK 6.0
- .NET ASPNET 6.0
- .NET Runtime 6.0

### Design patterns
Event Driven
CQRS (Not the full hardcore version, but keeping repo operations seperated in Commands and Queries)
Repostiory pattern

### Concerns
Below are concers listed that I found throughout the design.

#### Eventual consistency
as we are using events, aggregates will have eventual consistency, to mitigate this, I did a request / resposne pattern on MassTransit.
that said, if the session is materialized and new items are added to the session or items are removed, this will not show until the next time the session is aggregated.

### Runtime
Docker files with Docker Compose, a private network for all services.

### Microservices
#### User service
Manages users (profiles etc.)

User class definition:
- First Name
- Last Name
- Address
- City
- Postal code
- Country
- email address
- password hash


#### Product Catalog Service
List, Add, Update, Remove product as well as stock keeping

Procuct class defninition:
- Id
- SKU
- Name
- price

#### Shopping Cart Service
Manages users active shopping carts (state)
*I Implemented a small subset of operations, New sessions, adding, removing and getting the session, I did not implement checkout*
Shopping session (Cart) are implemented by using MassTransit for event bus and Marten for EventStore on PGSQL.
When a session is fetched, the service creates an event aggregate based on the session id and returns the current state of the cart.

#### Order Management Service
Handle orders: Place, Process, Cancel, Return, Ship as well as report generation
*This service I did not implement due to time constarins*
For this implementation, the scheduled report job I would have implemented with Hangfire or Quartz, as well I ITextSharp for PDF generation.

#### API Gateway
Presentation layer, OpenAPI 3 spec with SwaggerUI and OAuth2
*I skipped tests here, fully aware that it's not a good practice, this was due to time constrains*