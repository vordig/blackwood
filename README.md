# Blackwood
A project for a personal library.

## Project structure

### Blackwood.Domain
Contains all business logic. Domain objects, helper classes etc.

### Blackwood.Infrastructure
Working with out-of-process dependencies.
This layer contains database repositories, ORM settings, SMTP-clients etc.

### Blackwood.Services
An entry point for an external client.
Coordinate a work between domain classes and out-of-process dependencies.

### Blackwood.Api
An API client with authorisation, endpoints and mapping. 

### Blackwood.Client
An Angular project as UI.

### Blackwood.Tests.Unit
All tests for a domain logic.

### Blackwood.Tests.Integration
All tests for an entire API with Database.