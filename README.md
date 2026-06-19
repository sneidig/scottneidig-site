# scottneidig.com

My personal portfolio site — a full-stack web app built to demonstrate the stack
end to end, not just describe it.

## Stack

| Layer | Tech |
|-------|------|
| Frontend | React (Vite) |
| Backend | ASP.NET Core (.NET 10) Web API |
| Data | EF Core + SQL Server (with migrations) |
| Notable | Contact API with honeypot + rate limiting; image lightbox |

In production the ASP.NET Core app serves the built React frontend and the `/api`
endpoints from a single host.

## Project structure

```
client/   React frontend (Vite)
server/   ASP.NET Core Web API + EF Core data layer
```

## Running locally

**Prerequisites:** .NET 10 SDK, Node.js, and SQL Server (or LocalDB).

**Backend** (from `server/`):
```bash
dotnet run
```
On startup it applies EF Core migrations and seeds content. API runs on `http://localhost:5073`.

**Frontend** (from `client/`):
```bash
npm install
npm run dev
```
Vite runs on `http://localhost:5173` and proxies `/api` to the backend.

## Configuration

- Connection string: `ConnectionStrings:Default` (appsettings / env var)
- Contact email (optional): the `Smtp` section — set the password via user-secrets,
  never in source control.
