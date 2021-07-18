## Movies API

**About:** 
* Movies API has one API with four different actions against stored movie db.
* The application is set to launch swagger file by default.

**Details:**
* The soultion consists of API project and Unit tests project.
* The seed data is initialized on startup.
	* To make it easy all the primary keys are defined as integers. I would consider using GUIDs otherwise.
	* There are 20 movies defined (1...20).
	* There are 5 Users defined (1...5).
	* Available genres:
		* Drama
		* Comedy
		* Action
		* Romance
		* Adventure
		* History
		* Animation
		* Fiction
	Please use this data to interact with the API.
**Dependencies:**
* SQLite for the database.
* AutoMapper for mapping the API models to Db Models and vice versa.
* EF Core as ORM and LinqKit.Core to build predicates for querying.
* XUnit, Moq for Unit tests.

