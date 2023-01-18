# SalesProject
# Summary

API development will be made for order transactions. The infrastructure, tools and workflow to be used in the application going to be as follows.

# Tecnologies
  Framework: .Net 6
  Language: C#
  Database: Mssql
  ORM: Entity Framework – Code First Approach
  Memory Cache: .Net Memory Cache or Redis (started with .net memory cache, in future going to change to Redis)
  Message Queue: RabbitMQ
  Tools: AutoMapper, SeriLog

# Entity ve Models
  1.	Product Entity
      Id, Description, Category, Unit, UnitPrice, Status, CreateDate, UpdateDate 
  2.	ProductDto
      Id, Description,Category,Unit,UnitPrice
  3.	Order Entity
      Id, CustomerName, CustomerEmail, CustomerGSM, TotalAmount 
  4.	OrderDetail Entity
      Id, OrderId, ProductId, UnitPrice,
  5.	CreateOrderRequest
      CustomerName, CustomerEmail, CustomerGSM, List<ProductDetail>. ProductDetail object: ProductId,UnitPrice,Amount
  6.	ApiResponse
      Status (Success,Failed enum) , ResultMessage,ErrorCode,Data (GenericType)


# Api Methods
  1.	GetProducts
  If the category parameter is empty, all products will be listed, and if it is not empty, products belonging to the relevant category will be listed.
  When a request is made to the method, the memory cache will be checked first and if it is full, products will be given from here. 
  If the cache is empty, the records will be read from the database and the memory cache will be filled and the products will be listed. 
  List<ProductDto> will be returned in the Response Data field.
  2.	CreateOrder
  Http POST. It will take the CreateOrderRequest pattern as a parameter.By using the CreateOrderRequest object, insert operation will be made into the Order and OrderDetail tables. 
  It will be added to the SendMail queue in the RabitMq service in order to send the e-mail to the sales customer asynchronously.
  The Id information of the order record created in the Response Data field will be returned.



