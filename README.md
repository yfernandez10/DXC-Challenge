# DXC-Challenge
DXC Challenge for YeisonFS

This Project was created on Visual Studio 2015
The Data Base was created on SQL Server 2017 with SQL Server Management Studio (SSMS)
The Backup file for the DataBase was included on the repository.

**Data Base Backup **
To Execute the project, first restore the Data Base, because the project was created using the Entity Framework
So in order for the project to run you will need to restore the data base and them on the projects folders on Models
you can find the Entity Framework database called "People.edmx", after the restauration of the DB, please delete this file
and replace the file with a new one, on models do a right click and select "Add" -> "New Item" -> "Data" -> "ADO.NET Entity Data Model"
And connect to the SQL server and the restored Data Base, after that the project should run as usual.

**Postman Tests**
The Postman tests were included on the repository as well as a .json file exported by Postman, over there you can review the test for each 
User Story


