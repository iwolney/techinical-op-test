# CQRS - SQL SErver e MongoDB

Backend em .NET 10 para gestão de biblioteca comunitária.

## Tecnologias

- .NET 10
- SQL Server
- MongoDB
- RabbitMQ
- Entity Framework Core
- MassTransit
- MediatR
- CQRS
- DDD
- Arquitetura Hexagonal
- Docker
- xUnit

## Arquitetura

A solução segue arquitetura hexagonal com separação entre:

- Core Domain
- Core Application
- Adapters
- Ports

## Fluxo principal

```text
API
 ↓
Application
 ↓
Domain
 ↓
SQL Server
 ↓
Domain Events
 ↓
Integration Events
 ↓
RabbitMQ
 ↓
Worker
 ↓
MongoDB
 ↓
Queries



## Como executar

Requisitos : Docker-Desktop

- Na raíz da solução execute para subir o docker:

</> bash
docker compose up -d

- Aplicar migrations:
</> bash
dotnet ef database update --project ./src/adapters/TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase --startup-project ./src/ports/TechnicalTestOpea.Ports.OperationAPI

- Executar API:
</> bash
dotnet run --project ./src/ports/TechnicalTestOpea.Ports.OperationAPI

- Executar Worker/Consumers:
</> bash
dotnet run --project ./src/ports/TechnicalTestOpea.Ports.DataReplicationConsumer

# Principais endpoints

- Books
</> http

POST /api/books
GET /api/books
GET /api/books/{id}

- Loans
</> http

POST /api/loans
PUT /api/loans/{id}/return
GET /api/loans

- Health Check
</> http

GET /health


## Resumo das Regras de domínio

- Não criar livro sem título.
- Não criar livro sem autor.
- Não emprestar livro sem disponibilidade.
- Ao emprestar, reduzir quantidade disponível.
- Ao devolver, aumentar quantidade disponível.
- Não devolver empréstimo já devolvido.


## Observação sobre produção

Em ambiente produtivo, a publicação de eventos deveria utilizar o padrão Outbox para garantir consistência transacional entre SQL Server e RabbitMQ.

## Melhorias futuras

- Implementar Health Checks para SQL Server, MongoDB e RabbitMQ.
- Implementar Outbox Pattern para garantir consistência entre SQL Server e RabbitMQ.
- Adicionar autenticação/autorização.
