# OptiBid
The Ultimate Solution for Securing the Best Deals on Top-Quality Products and Services.

# System architecture
This is a .NET-based system consisting of two GRPC services and an API gateway. The first GRPC service deals with account-related data and uses the MediatR library to process requests. It also sends notifications to a RabbitMQ message queue if any changes occur. The data is persisted using Entity Framework and MS SQL.

The second GRPC service processes auctions and is structured as a usual business logic structured project. It uses Entity Framework and Postgres SQL for persistence and also sends messages with changes to a RabbitMQ message queue.

Caching is implemented using a third-party library and is used by both GRPC services. The API gateway uses a GRPC client to communicate with the microservices and caches responses, which are deployed to a hybrid cache.

The system follows a usual business layered project architecture. The API gateway includes a SignalR hub that provides real-time streaming of changes from both GRPC services.

Overall, this system is designed to efficiently handle account-related data and auctions with real-time streaming capabilities, all while utilizing caching and following best practices for layered architecture.

In the next picture, you can take a look at this architecture

![System-Architecture](https://user-images.githubusercontent.com/36825550/219522934-380a1a77-a8c4-4c0e-a8ca-c7214d00bc7f.png)
