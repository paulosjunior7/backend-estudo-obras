dotnet-ef migrations add AddReferenceCompany -s ObrasAPI -p Obras.Data -c ObrasDBContext
dotnet-ef database update -s ObrasAPI -p Obras.Data -c ObrasDBContext