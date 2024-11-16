# Point of Sale SaaS

## Running project locally

To run the project locally you will need to install PostgreSQL and set database connection string in `appsettings.Development.json`:

```
{
  "ConnectionStrings": {
    "Database": "Host=localhost;Port=5432;Database=PointOfSale;Userid={YOUR_POSTGRES_USER};Password={YOUR_POSTGRES_PASSWORD}"
  }
}
```

## Running using Docker containers

To run the project using Docker containers you will need to build the API image by running the following command in root directory: `docker build -t pos-api .`

Then change database connection string in `appsettings.Development.json`:

```
{
  "ConnectionStrings": {
    "Database": "Host=pos.database;Port=5433;Database=PointOfSale;Userid=postgres;Password=postgres"
  }
}
```

And finally start the API by running
`docker compose up`

## Code formatting

Install [CSharpier](https://csharpier.com/) with the following command:

`dotnet tool install -g csharpier`

Format the code by running the following command:

`dotnet csharpier .`
