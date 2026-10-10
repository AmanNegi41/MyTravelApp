FROM ://microsoft.com AS build

WORKDIR /src

# Copy everything from the repository root and restore dependencies
COPY . .
RUN dotnet restore

# Build and publish the web project
RUN dotnet publish "MyTravelApp.Web/MyTravelApp.Web.csproj" -c Release -o /app/publish

FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose standard web port for Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MyTravelApp.Web.dll"]
