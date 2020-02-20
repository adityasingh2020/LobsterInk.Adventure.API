# LobsterInk.Adventure.API
LobsterInk.Adventure.API

Download/clone the code

Navigate to \LobsterInk.Adventure.API\LobsterInk.Adventure.API folder and run 'dotnet run' command .   
open swagger UI @ https://localhost:5001/swagger/index.html
UnitTest : navigate to \LobsterInk.Adventure.API\LobsterInk.Adventure.Tests.Unit  folder and run 'dotnet test LobsterInk.Adventure.Tests.Unit.csproj'   


Run using Docker :

Pre-requisite : Install Docker ( https://docs.docker.com/install/ )

1- Download/clone the code & Navigate to folder ( root ) conatining Dockerfile

2- docker build -t adventureapi .

3- Dockerfile has a step to execute unit test cases and will be executed in above steps i.e. while creating docker image. 
   Test result will be displayed on console after executing the step 2

4- docker run -d -p 5001:80 --name oppapi opportunityapi

5- Open https://localhost:5001/index.html in browser

6- Execute ~adventureapi endpoint using swagger UI or execute ( curl -X GET "http2://localhost:50001/swagger" -H "accept: application/json" )
