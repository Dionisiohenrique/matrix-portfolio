# 🟢 Matrix Portfolio — Fullstack (ASP.NET Core + Angular + PostgreSQL)

A Matrix-themed developer portfolio. Public site shows your profile, projects and skills;
a JWT-protected admin console lets you manage everything and read contact messages.

**Stack:** .NET 10 Web API · EF Core · PostgreSQL (Npgsql) · JWT auth · Angular 20 (standalone, signals) · Netlify/Render/Neon free tiers.

## Structure

```
matrix-portfolio/
├── MatrixPortfolio.Api/      # ASP.NET Core API (EF Core + JWT)
├── matrix-portfolio-web/     # Angular frontend
├── netlify.toml              # Netlify config for the Angular build
└── render.yaml               # Render blueprint for the API
```

## Run locally

1. Start PostgreSQL (any instance on localhost:5432) and create the DB:
   ```bash
   docker run -d --name matrix-pg -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=matrix_portfolio -p 5432:5432 postgres:16-alpine
   ```
2. API:
   ```bash
   cd MatrixPortfolio.Api
   dotnet ef database update   # or just `dotnet run` — migrations apply at startup
   ASPNETCORE_URLS=http://localhost:5000 dotnet run
   ```
3. Frontend:
   ```bash
   cd matrix-portfolio-web
   npx ng serve --proxy-config proxy.conf.json
   # → http://localhost:4200  (API calls proxy to :5000)
   ```

Admin login: `admin` / `NeoIsAwake!2026` (change in appsettings.Development.json).

## Deploy for free (~30 min)

### 1. Database — Neon (free Postgres)
1. Sign up at https://neon.com → create project.
2. Copy the pooled connection string (ends with `sslmode=require`).

### 2. Backend — Render
1. Push this repo to GitHub.
2. https://render.com → **New → Blueprint**, point it at the repo (`render.yaml` included).
3. Set env vars when prompted:
   - `ConnectionStrings__Default` = Neon connection string
   - `Jwt__Key` = any random string ≥ 32 chars
   - `Admin__Username` / `Admin__Password` = your admin login
4. Note the API URL, e.g. `https://matrix-portfolio-api.onrender.com`.

Migrations + seed data apply automatically at startup.

### 3. Frontend — Netlify
1. https://app.netlify.com → **Add new site → Import from Git** → pick the repo.
3. Base directory: `matrix-portfolio-web`. Config comes from `netlify.toml`.
4. **Important:** edit `netlify.toml` and replace `REPLACE-WITH-YOUR-RENDER-URL` with your real Render URL — this proxies `/api/*` to your backend so there are no CORS issues.
5. Set env var in Render dashboard: `Frontend__Url` = `https://your-site.netlify.app` (this whitelists your site's CORS origin too).

## Notes

- The CORS policy allows localhost:4200 plus whatever you put in `Frontend__Url` (env: `Frontend__Url`) — set it to your Netlify URL.
- Admin credentials come from env vars; swap in ASP.NET Identity if you ever need multi-user.
- Render free tier sleeps after inactivity — first request takes ~30s ("the Oracle is thinking...").
