# _Pierre Online Bakery_

#### _Week 5 C# ASP.NET MCV w/ MySQL Project for Epicodus, October 23rd, 2020_

#### By _**Mike Manchee**_

## Description

Pierre is back again, This time around, he wants a web site were customers can log in and create an order of tasty treats. He wants to be able to have his customers mix and match treats in all sorts of flavors. Pierre can add new items but the customers can only build a basket.

Admin -
User: admin@OnlineBakery.local
Pass: NotSecure123!!

<!-- Brainstorming
View a list of Treats
View a list of Flavors
details for each Treat
details for each Flavor
list of all different types of Treats
list of all different types of Flavors
CRUD Treats
CRUD Flavors
Login for Users

********** Further **************
add Customer baskets
add Admin Access
 -->
### Specs
| Spec | Input | Output |
| :-------------     | :------------- | :------------- |
|  1.  Create Treats, Flavors Classes | ... | ... |
|  2.  Build Treats Controllers for Index, Create, Details, Delete, Edit | ... | ... |
|  3.  Build Flavors Controllers for Index, Create, Details, Delete, Edit | ... | ... |
|  4.  Build Home Controllers and View for Index | ... | ... |
|  5.  Build Treats Views for Index, Create, Details, Delete, Edit | ... | ... |
|  6.  Build Flavors Views for Index, Create, Details, Delete, Edit | ... | ... |
|  7.  Create User Login | ... | ... |
|  8.  Create a Basket for Users to add Items to | ... | ... |
|  9.  Create Admin Access for Pierre | ... | ... |
|  10.  Add CSS and Styling | ... | ... |


## Setup/Installation Requirements

### Project from GitHub
* Download option
  * Download files from GitHub repository by click Code and Download Zip
  * Extract files into a single directory 
  * Run GitBASH in directory
  * Type "dotnet restore" to get bin and obj files
  * Type "dotnet run" in GitBash to run the program
  * Add database per the instructions below.
  * Have fun with Online Bakery <!-- TITLE HERE -->

* Cloning options
  * For cloning please use the following GitHub [tutorial](https://docs.github.com/en/enterprise/2.16/user/github/creating-cloning-and-archiving-repositories/cloning-a-repository)
  * Place files into a single directory 
  * Run GitBASH in directory
  * Type "dotnet restore" to get bin and obj files
  * Type "dotnet run" in GitBash to run the program
  * Add database per the instructions below.
  * Have fun with Online Bakery <!-- TITLE HERE -->

### Database Setup
* Go to appsettings.json and change the password if needed.

* Setup with SQL migrations
  * In the terminal, navigate to the project folder
  * Type "dotnet ef migrations add Initial" and wait for migration file to be built
  * Type "dotnet ef database update" and wait for build confirmation


## Known Bugs

No Known Bugs

## Technologies Used
Project Specifics
* Many to Many Database relationships
* Authentication with Identity

Main Programs
* MySQL
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
  * [Entity](https://docs.microsoft.com/en-us/ef/core/)
  * [Razor](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-3.1)
  * [MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-3.1)
* CSS
  * [Bootstrap](https://getbootstrap.com/docs/4.5/getting-started/introduction/)
  * [Font Awesome](https://www.w3schools.com/icons/fontawesome_icons_intro.asp)


### Other Links
[Mike's GitHub](https://github.com/mmanchee)
[LinkedIn](https://www.linkedin.com/in/mikemanchee/)

### License

Copyright (c) 2020 **_{Mike Manchee}_**
Licensed under MIT