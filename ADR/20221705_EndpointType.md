2022-05-17

# Endpoint type for API service as a gateway to ingest a lot of data into broker

## Status

Accepted

## Context

- The aim of general task is to create an API service as a gateway to ingest **a lot of data fast** into some broker.
- Used by **external customers or our integrators** . 
- The service handles the call and forwards it to a broker.
- The destination **broker is specified as a field in the message**.
- Non-functional requirement: **Handle large amounts of data** (a lot of small messages).

---
- The **.NET** team will implement this feature.
- Solution will be hosted on Azure Cloud
- Size of a **single message** will be less then **1kB**. 
- From business point of view is important to **easily share API for external customers**.

## Considered solutions

1. RESTful API
- pros
  - simple to build and adapt
  - popular and uses HTTP protocol which is widely known among programmers
  - easy to test (for developers using for example webBrowser/Postman and also allows easily prepare load tests for example using software like JMeter)
  - we can easily expose the structure of API for external customers (Swagger)
  - human readable
  - easy API versioning in ASP.NET
  - performance is improved in .NET 6 version
- cons
  - json payload is not lightweight as in binary message format
  - the payload is larger, the performance is worse
--- 
2. GRPC
- pros
  - more efficient then REST API
    - because of using binary message format and HTTP/2.0
    - https://the-worst.dev/rest-vs-grpc-performance-benchmark-in-net-core-3-1/
  - we can share the communication description which is encapsulated in a 'proto' file 
    - developers can generate client code using this 'proto' file
- cons
  - needs a gRPC client to fetch data
    - more complicated for testing
  - client needs a *.proto file 
    - when we change the endpoint, **new version of this file need to be send to all of clients**
  - not as widely known among programmers 
    - harder to adapt this solution to new team members and external customers
  - not human readable
 
## Decision

The team decided to use solution **1. RESTful API on .NET 6** because of:
- easier adapt REST API to external customers
  - GRPC needs proto files sharing among clients, and it may be difficult for external clients. For REST there is no such need.
  - REST is widely known and easy to use in any programming language
- REST performance for small messages is quite good in comparison to GRPC
- Performance 
 

## Consequences

- The problem with performance may arise when the size of the massage gets bigger. But even then we may use payload compression.
- .NET 6 is quite fresh version (but production-ready with LTS). The consequence of this may be facing some bugs not found by Microsoft so far. 