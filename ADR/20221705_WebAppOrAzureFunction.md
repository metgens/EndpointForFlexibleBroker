2022-05-17

# Does App should be developed as Azure Function or ASP.NET WebAPI

## Status

Accepted

## Context

- The aim of general task is to create an API service as a gateway to ingest **a lot of data fast** into some broker.
- Used by **external customers or our integrators** . 
- The service handles the call and forwards it to a broker.
- Non-functional requirement: **Handle large amounts of data** (a lot of small messages).
- The app will be using **.NET 6**
- optional: Containerization 

## Considered solutions

1. Azure Functions
- pros
  - scalable
  - high throughput
  - small, lightweight and fitted for small solutions
  - price for specific solutions may be much lower than in the case of web apps. 
- cons
  - force to use Azure Cloud
  - from experience, Azure functions in general have had more bugs then ASP.NET Web Apps
  - difficult to use other IoC containers such as Autofac
  - time of cold start
--- 
2. ASP.NET Web API 
- pros
  - can be containerized
  - easy to configure logging, authentication third-part IoC containers
  - easy to describe using Swagger 
- cons
  - price when hosting in cloud and high throughput need
 
## Decision

The team decided to use **2. ASP.NET Web API** because of:
- fear of being limited to the Azure cloud when choosing Azure Functions
- optional need of containerization the solution

## Consequences

Total price may be higher in comparison to AF.