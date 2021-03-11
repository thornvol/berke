## Project

The project is to create a web API for a game collection software. Using this API users will be able to get, add, and remove games from their collection. In order to do all these functions users need to be authenticated and authorized to do specific action. Do not design any UI for this. Any database design should be done using code first approach and tables configured using Fluent API. Any configuration needed should be stored in appsettings.json.

Use the following technologies:
  * .NET 5
  * EF Core 5
  * MSSQL

Requirements:
1. Create a table for users in the DB. The table should hold the users' username, password, and whether the user is administrator or not.
  * Seed this table with two users. No need to hash passwords unless you want to.
  * Users:
    * Username: harry, Password: hedwig, IsAdmin: true
    * Username: hermione, Password: crookshanks, IsAdmin: false
2. Create the following tables for game collection:
  * Table: Genres, Columns: GenreId (PK), Name
  * Table: Publishers, Columns: PublisherId (PK), Name
  * Table: Games: GameId (PK), Name, ReleaseDate, Overview,
  * Table: UserGames: UserId (PK), GameId (PK)
  * The table Games has many-to-many relationship to Genres and one-to-many relationship to Publihers
  * The table UserGames has one-to-one relationship to Users and Games.
  * Seed these tables with few games, genres, and publishers. They can be either made up or real games.
  * All tables should have well defined relationships and proper constraints on columns
3. Create the following endpoints
  * /auth, POST, accepts username and password from form data
  * Authenticates a user and issues a JWT token. Set the token expiration to 1 year from now. Add any claims that you might need for authentication/authorization.
  * You can use this post to generate JWT token if you need: https://stackoverflow.com/questions/40281050/jwt-authentication-for-asp-net-web-api
  * /collection, GET
    * retrieves all games for the authenticated user
  * /collection/{gameId}, POST, gameId is the ID of a game. It is required and must be and integer.
    * Adds a game to the users' collection.
  * Handle non-existent game ID.
  * /collection/{gameId}, DELETE, gameId is the ID of a game. It is required and must be and integer.
    * Removes a game from users' collection.
    * Handle non-existent game ID.
  * /games, POST, Needs to take in data about a game. Requires user to be administrator.
    * Adds a game to the database.
4. Errors on the server side should be handled accordingly and returned to client with useful context.
5. Use repository and unit of work patterns for database access.
5. Time permitting add unit tests using XUnit. If you need to mock anything use Moq.

**All endpoints should return appropriate result codes for the action they are performing. The only action that is not authenticated using JWT is the /auth action. Everything else should return 401 or 403 if unauthenticated/unauthorized.**