# Relational database

This repository uses Docker Compose to run PostgreSQL locally.

## Prerequisites

- Docker Desktop (Windows)
- Docker Compose v2 (bundled with Docker Desktop)
- .NET 10 SDK (as required by the solution)

## Quick start (PostgreSQL)

1. Create a local `.env` file in the repository root (next to the `.sln`):

   ```bash
   copy .env.example .env
   ```

2. Open `.env` and set:

    - `POSTGRES_PASSWORD` (required)

3. Start PostgreSQL:

   ```bash
   docker compose up -d
   ```

4. Check status:

   ```bash
   docker compose ps
   ```

5. Stop containers:

   ```bash
   docker compose down
   ```

### Reset database data (delete the volume)

> This will remove all local database data.

## Connection details

- Host: `localhost`
- Port: `5434`
- Database: value of `POSTGRES_DB` (default: `reservation_service_db`)
- User: value of `POSTGRES_USER` (default: `postgres`)
- Password: value of `POSTGRES_PASSWORD` (from `.env`)

## Why `.env.example` exists

The real `.env` file is intentionally **not committed** (it may contain secrets like passwords).
`.env.example` is a committed, safe template that documents the required environment variables and makes onboarding quick:

- you can copy it to `.env`
- fill in local values
- run `docker compose up -d` without editing `docker-compose.yml`