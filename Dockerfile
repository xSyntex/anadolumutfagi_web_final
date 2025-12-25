# Use the official .NET 10 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Web Programlama Proje.csproj", "./"]
RUN dotnet restore "Web Programlama Proje.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/."

# Build and publish the application
RUN dotnet build "Web Programlama Proje.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Web Programlama Proje.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 8080 (default for .NET 10)
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Configure the entry point
ENTRYPOINT ["dotnet", "Web Programlama Proje.dll"]
