# s3716853-a1
Madeline Eletva's (s3716853) Web Development Technologies Assignment 2 2022 Summer

Trello folder included as part of a assignment requirement and can be ignored
## Github URL
https://github.com/rmit-wdt-fs-2022/s3716853-a2
## Start Guide
Migrations are all done in MCBACustomerAPI
### Main App
MCBAWebApplication requires MCBACustomerAPI to be running for it to function, so run MCBACustomerAPI and then run MCBAWebApplication.

All these can be run either through Visual Studio, or by running 'dotnet run' within their respective folders.
### Admin
MCBAAdminWebApplication requires MCBAAdminAPI to be running for it to function, so run MCBAAdminAPI and then run MCBAAdminWebApplication.

All these can be run either through Visual Studio, or by running 'dotnet run' within their respective folders.
## User Logins (username - password)
### Users
12345678 - abc123 (has bank accounts 4100 and 4101)

38074569 - ilovermit2020 (has bank account 4200)

17963428 - youWill_n0tGuess-This! (has bank account 4300)
### Admin (only works on admin site)
admin - admin
## Ports
MCBAWebApplication - 7575

MCBACustomerAPI - 59823

MCBAAdminWebApplication - 26984

MCBAAdminAPI - 38999
## Admin Api
Every table in the database has the following actions:

GET - /[Table-Name] (Gets all)

GET - /[Table-Name]/{key} (Get with key = primary key)

PUT - /[Table-Name]/ (Update existing object, response body is the database model Jsonified)

POST - /[Table-Name]/ (Create new object, response body is the database model Jsonified)

DELETE - /[Table-Name]/{key} (delete with key = primary key)

### Update Customers
PUT - /Customer/Update

Expected Request Body

{

  "customerID": "string",

  "name": "string",

  "tfn": "string",

  "address": "string",

  "suburb": "string",

  "state": 0, <-this is an enum in the code, gets saved as int

  "postCode": "string",

  "mobile": "string"

}

### Transaction History
GET - /Transaction/Account/{accountNumber}?startDate=[startDate]?endDate=[endDate]

startDate and endDate get parsed with DateTime.Parse() (YYYY-MM-DD format works for DateTime.Parse() for example)

if not included then all transactions are returned
