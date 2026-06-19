# Deployment Checklist — scottneidig.com

Target: the same Windows/IIS host already running nopCommerce (ASP.NET Core + SQL Server).
Hosting model: **one app** — ASP.NET Core serves both the React site and the `/api`.

---

## Proven locally ✓
Running `ASPNETCORE_ENVIRONMENT=Production` with the React build in `wwwroot`, a single
server serves the website (`/`), the API (`/api/content`), and assets. `dotnet publish`
produces a clean package with the IIS `web.config` (AspNetCoreModuleV2, in-process).

---

## 1. Build the deployable package
From the repo root:
```powershell
./publish.ps1
```
This builds React → copies it into `server/wwwroot` → publishes to `server/publish/`.
**`server/publish/` is what you upload.**

## 2. Create the production database
- In the host control panel, create an empty SQL Server database named **`ScottNeidigSite`**
  (separate from the nopCommerce DB — do not touch that one).
- Note the SQL **login/password** the host gives you; it must be `db_owner` of this DB.

> Migrations + seed run automatically on first app start (`Database.Migrate()` + seed).
> No manual scripts needed — as long as the connection string is set and the SQL user
> can create tables. (Alternative: `dotnet ef migrations script -i -o deploy.sql` and run by hand.)

## 3. Set production configuration (NEVER commit secrets)
On the host, set these (env vars preferred — note the `__` separator):
```
ASPNETCORE_ENVIRONMENT = Production
ConnectionStrings__Default = Server=...;Database=ScottNeidigSite;User Id=...;Password=...;TrustServerCertificate=True
Smtp__Password = <gmail app password>     # only if you want the contact form to email
```
See `server/appsettings.Production.sample.json` for the shape. Prod uses **SQL auth**
(user/password), not the Windows-auth string used in dev.

## 4. Deploy to a STAGING path first (don't swap the live site yet)
- Create a temporary IIS site/app (e.g. `new.scottneidig.com` or a subfolder).
- App pool: **No Managed Code** (ASP.NET Core hosts via the ASP.NET Core Module).
- Upload `server/publish/*` there.
- Browse it: confirm the homepage, the gallery/lightbox, and submitting the contact form
  all work against the prod database. Check the `/logs` folder if it won't start.

## 5. Swap to live (with rollback ready)
1. **Back up** the current nopCommerce site files to a safe directory (your rollback).
2. Point the `scottneidig.com` root at the new app's folder (or copy `publish/*` into it).
3. Recycle the app pool / restart the site.
4. Verify live. If anything's wrong, repoint to the backup — instant rollback.

---

## Notes / caveats
- Exact clicks depend on the host's control panel (Plesk vs. custom). Concepts above hold;
  the UI may differ.
- The contact form saves to the DB even if SMTP isn't configured — nothing is lost.
- HTTPS is typically handled by the host; the app doesn't need to manage certs.
- First request after deploy may be slow (cold start + migration). Normal.
