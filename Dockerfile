# Inherit from the dotnet sdk version 6 base image.
# Make this image a multi-stage build, and name the first stage build-env.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set working directory to /App
WORKDIR /App

# Copy contents to the base directory
COPY . ./

# Restore the dotnet project using the "dotnet restore" command.
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out -r linux-x64

# Build runtime image, make this the last build step.
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set working directory to /App same as previous build stage
WORKDIR /App

# Copy results from the first build stage into the working directory out folder.
COPY --from=build-env /App/out .

# Install curl
RUN apt-get update && apt-get install -y curl

# Expose port 80
EXPOSE 80

# Use the dll file as an entrypoint using dotnet.
ENTRYPOINT ["dotnet", "AwsTodoApp.dll"]