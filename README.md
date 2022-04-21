# Blackwood
A project for a personal library.

## Project structure

### Blackwood.Domain
Contains all domain logic.

### Blackwood.Services
An entry point for an external client.
Coordinate a work between domain classes and out-of-process dependencies.

### Blackwood.Infrastructure
Working with out-of-process dependencies.
This layer contains database repositories, ORM settings, SMTP-clients etc. 

### Blackwood.Web.Api
An API client with authorisation, endpoints and mapping. 

### Blackwood.Web.Client
An Angular project as UI.

### Blackwood.Tests.UnitTests
All tests for the domain logic.

### Blackwood.Tests.IntegrationTests
All tests for services.