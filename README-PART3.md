# RaceDay — Part 3 MVC Front End

This Part 3 workspace adds an ASP.NET Core MVC front end to the supplied Part 2 API. MVC data operations are sent through `ApiClient`; MVC has no database context and does not duplicate database business rules. The API's session cookie is forwarded by the MVC server and the MVC session keeps the signed-in user ID, role and display name.

## Included
- Registration and login through `POST /api/auth/register` and `/api/auth/login`.
- Session-based role-aware navigation and protected Organiser / Participant actions.
- Organiser dashboard, event create/edit/delete, categories, enrolment list and result publishing.
- Participant event discovery, event details/category entry, personal enrolments, results and profile editing.
- Bootstrap-responsive RaceDay styling.
- Dockerfile for the MVC application.
- Updated solution and GitHub Actions build workflow.

## Important Part 2 integration gaps — resolve before claiming full marks
The uploaded Part 2 API currently exposes only `Name`, `Description`, `Location` and `EventDate` on events. Its `EventRequest` has no event distance or event type, and there is no event banner/profile-picture upload endpoint, no Azure Blob Storage service, and no stored image URL. The MVC project deliberately does not connect to Azure directly or write to the database. Therefore the following rubric items are **not yet complete** until the Part 2 API is extended:
1. Add `DistanceKm` and `EventType` to the event entity, request DTO, EF configuration/migration or SQL schema, and event read/write endpoints.
2. Add Azure.Storage.Blobs to the API; configure `AzureBlobStorage:ConnectionString` and container names using environment secrets; implement authenticated multipart upload endpoints for event banners and profile images; save returned blob URLs against the event/user; return those URLs in API DTOs.
3. Add a participant results endpoint such as `GET /api/users/me/results` (or keep MVC's current per-enrolment lookups and extend event/category/position total fields if the rubric requires "47th out of 312").
4. Extend category and enrolment API endpoints if you require status values beyond the API's current confirmed-on-create behaviour.
The MVC layer is ready to consume those endpoints once added. It intentionally does not fake Azure upload success or invent event fields.

## Requirements
- .NET 8 SDK
- SQL Server configured for the Part 2 API
- Docker Desktop (optional)

## Run locally
1. Run SQL setup exactly as described in `README.md` (Part 2); do not rerun the destructive seed script against a database with data you need.
2. Start the API from the repository root:
   `dotnet run --project src/RaceDay.Api/RaceDay.Api.csproj`
3. Confirm the API base URL. Set `Api:BaseUrl` in `src/RaceDay.Mvc/appsettings.json` to the API's HTTP base address (default template is `http://localhost:5000/`). If using HTTPS, use the correct local API URL and trust the development certificate.
4. In another terminal:
   `dotnet run --project src/RaceDay.Mvc/RaceDay.Mvc.csproj`
5. Browse to the MVC URL printed in the console. Register one Organiser and one Participant account to test both navigation experiences.

The API must be reachable from the MVC process. When using Docker on Windows/Mac, set `Api__BaseUrl=http://host.docker.internal:5000/` if the API is running on the host.

## Docker
Build:
```bash
docker build -t raceday-mvc:part3 .
```
Run (single command):
```bash
docker run --rm -p 8080:8080 -e Api__BaseUrl=http://host.docker.internal:5000/ raceday-mvc:part3
```
Open `http://localhost:8080`. On Linux, use `--add-host=host.docker.internal:host-gateway` if needed. If both API and MVC are in Docker, put them on the same Docker network and set `Api__BaseUrl` to the API service/container name. SQL Server remains configured for the API, not MVC.

## GitHub and CI/CD
The workflow `.github/workflows/raceday-api-ci.yml` restores and builds the solution (including MVC) and runs the Part 2 API tests. Push this Part 3 folder/project to your repository and confirm the Actions run is green. Do not fabricate commit history: make at least 20 meaningful commits from your own GitHub account as you implement, test, document and refine the remaining items.

Suggested commit sequence:
1. Add MVC project shell
2. Configure MVC services and session
3. Add API client
4. Implement API cookie forwarding
5. Add login view
6. Add registration view
7. Implement API registration
8. Implement API login/logout
9. Add role-aware layout
10. Add public event listing
11. Add organiser dashboard
12. Add event creation
13. Add event editing
14. Add event deletion
15. Add category management
16. Add organiser enrolment listing
17. Add results publishing
18. Add participant event details
19. Add participant enrolment
20. Add participant enrolment history
21. Add participant results
22. Add profile editing
23. Add responsive styling
24. Add Docker support
25. Extend CI build for MVC
26. Document deployment and test steps

## Evidence and video
Add your own actual screenshots under `docs/screenshots/` after running the application. Add the real unlisted YouTube URL here after recording; do not submit a placeholder as evidence. Demonstrate both roles, registration/login, organiser event/category/enrolment/results workflow, participant entry and results, API calls, and Docker startup. Explain the Azure API integration after completing the API gaps above.


## Important: run the MVC login page (not Swagger)

The Part 3 user interface is the **RaceDay.Mvc** project. The Part 2 API is a separate project and opens Swagger. In Visual Studio, right-click `src/RaceDay.Mvc/RaceDay.Mvc.csproj` and choose **Set as Startup Project**, then run it. The browser should open `https://localhost:7294/Auth/Login`.

The API must also be running in a second Visual Studio window/terminal: run `src/RaceDay.Api/RaceDay.Api.csproj` (HTTP `http://localhost:5194`, Swagger at `/swagger`). The MVC app calls that API. If Visual Studio starts only the API project, you will see Swagger/Part 2 rather than the MVC login screen.

From a terminal, use two terminals:

```bash
dotnet run --project src/RaceDay.Api/RaceDay.Api.csproj
```

```bash
dotnet run --project src/RaceDay.Mvc/RaceDay.Mvc.csproj
```

Then browse to `https://localhost:7294/Auth/Login` (or the MVC URL printed in the terminal).
