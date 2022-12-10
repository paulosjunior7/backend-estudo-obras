dotnet-ef migrations add AddReferenceCompany -s ObrasAPI -p Obras.Data -c ObrasDBContext
## Criar Banco ##
dotnet-ef database update -s ObrasAPI -p Obras.Data -c ObrasDBContext
## Subir Imagem Docker ##
docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/azure-sql-edges