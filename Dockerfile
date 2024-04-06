# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# Set the working directory
WORKDIR /src
# Copy the project file
COPY MyMicroservice.csproj .
# Restore the dependencies
RUN dotnet restore
# Copy the rest of the code
COPY . .
# Build and publish the application
RUN dotnet publish -c Release -o /publish
# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# Set the working directory
WORKDIR /publish
# Copy the published output from the build stage
COPY --from=build-env /publish .
# Set environment variables
ARG TENOR_API_KEY
ENV TENOR_API_KEY=$TENOR_API_KEY
# Expose the port
EXPOSE 8080
# Entrypoint
ENTRYPOINT ["dotnet", "MyMicroservice.dll"]