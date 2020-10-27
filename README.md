I have implemented simple solution without too many abstraction layers.


If it was a much bigger enterprise level solution, I would have have structure my project based on Clean Architecture principles + using CQRS patters.


Adapters:
- Web.Api
- RestCountries.Integration

Ports:
 - Domain
     - Entities with private setters and following DDD principles
     - Commands + Command Handlers
     - Queries + Query Handlers
     - Events + Event Handlers
     - Specifications
     - Outbox for events

For CQRS implementations, I would have used: https://github.com/jbogard/MediatR
