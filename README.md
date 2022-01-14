# s3716853-a1
Matthew Eletva's (s3716853) Web Development Technologies Assignment 1 2022 Summer
## Design Patterns
### Facade
The Facade pattern is when a larger system operated by many sub-systems is interfaced/interacted with a 'facade', a class which supplies a more limited set of functionality however it implements all the features the client should wish for. This can allows the codebase to be more easily used and understood, as the common functionalites have been abstracted out to the facade class. It also reduces how coupled the classes are with the further sub-systems, as the only dependency is the facade.

My implementation of the Facade class is my MCBABackend.Managers.DatabaseManager.

MCBABackend.Managers.DatabaseManager itself does very little other than manage the other MCBABackend.Managers classes. If multiple of the managers need to interact, it will facilitate that, however the specific database actions are left instead to the managers, and MCBABackend.Managers.DatabaseManager just implements the functionality which is expected out of the database through complex interactions with these other maagers.

The use of a facade allowed for good seperation of concern, the class dedicated to account database actions (MCBABackend.Managers.AccountManager) did not need to know or interact with the class which dealt with Transaction database actions (MCBABackend.Managers.TransactionManager) as that responsiblity is MCBABackend.Managers.DatabaseManager and theirs is to interact with the database. This also works for MCBABackend.Managers.DatabaseManager, as its only responsiblity was the interactions bewteen the Managers.

### Dependency Injection
Dependency Injection is the process of removing the responsibility of instanciating dependencies away from the class that will use it, and instead to an outer injector.

The purpose of this is it allows the dependencies for the object to be easily changed at runtime by passing in different dependencies which match the same interface, allowing more flexbility. It also helps achieve seperattion of concerns, as now the class which is 'injected' does not concern itself with dependency creation.

My code specifically implements it with the MCBABackend.Managers.DatabaseManager

`public static ICustomerManager _customerManager { private get; set; }`

`public static ILoginManager _loginManager { private get; set; }`

`public static IAccountManager _accountManager { private get; set; }`

`public static ITransactionManager _transactionManager { private get; set; }`

Each of these is a dependency references only by their interfaces, and are set in MCBAConsole.Program. So long as each of the interfaces are met, the underlying code can be easily modified. This could be useful for testing, as the manager classes are the ones which actually interact with the database, so a testing implementation of all of these managers could be made to test all other functionality without worrying about the database or network requests.
## Class Library
I made the backend for my application a class library (MCBABackend). By implemnting it this way, it is much easier to make new user interface (such as in Assignment 2) as the logic for the backend of the application and its database interactions can be re-used and extended if needed. It also helsp facilitate seperation of concerns, as the front-end and back-end are seperated.
