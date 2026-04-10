# MzansiBuilds

A platform where developers can build in public, share progress, collaborate, and celebrate completed work.

Built for the Derivco Graduate Programme Code Skills Challenge, April 2026.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET MVC 5 (.NET Framework 4.7.2) |
| Language | C# |
| ORM | Entity Framework 6 (Code First) |
| Database | SQL Server Express |
| Authentication | ASP.NET Identity |
| Frontend | Bootstrap 5, Font Awesome 6 |

---

## Database Design

![ERD and User Flow](docs/MzansiBuild_UML.png)

Six tables: Developers, Projects, Comments, Milestones, CollaborationRequests, Celebrations.

---

## Setup

**Requirements:** Visual Studio 2022, SQL Server Express, SSMS

1. Clone the repo and open `MzansiBuilds.sln`
2. Confirm `Web.config` connection string points to `localhost\SQLEXPRESS`
3. Run `Update-Database` in the Package Manager Console
4. Press F5 to run

---

## Seed Data

To load sample data (5 developers, 8 projects, milestones, comments, collaboration requests):

1. Register these accounts through the app at `/Account/Register`:
   - dev.thabo@gmail.com
   - sara.chen@outlook.com
   - marcus.dev@gmail.com
   - priya.builds@gmail.com
   - luca.codes@outlook.com

2. Run `seed_data.sql` in SSMS against the `MzansiBuilds` database.

---

*Shaista Naicker, 2026*
