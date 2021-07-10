# SmartHome Server
Server that is supposed to handle everything what smart home requires. It communicates with sensors and displaying aggregated results. Additionally it also provides API for performing file backups.

[![Build Status](https://dev.azure.com/janpalasek/smart-home-server/_apis/build/status/JanPalasek.smart-home-server?branchName=master)](https://dev.azure.com/janpalasek/smart-home-server/_build/latest?definitionId=2&branchName=master)

## Requirements
- .NET Core 3.1
- MySQL database

## How to install
1. Clone the project and change working directory into smart-home-server.
    ```bash
    git clone https://github.com/JanPalasek/smart-home-server
   # for linux
    cd smart-home-server
    ```
2. Create SmartHome database and user with privileges.
    ```
   # run mysql command line
   sudo mysql
    ```
   
   ```mysql
   CREATE USER 'HomeUser' IDENTIFIED BY 'noPass1234';
   GRANT ALL PRIVILEGES ON *.* TO 'HomeUser';
   CREATE DATABASE SmartHome;
   ```
3. Create tables and push basic test data
    ```
   # create tables
    mysql --user="HomeUser" --database="SmartHome" -p"noPass1234" < "Scripts/RenewDb.sql"
    # insert test data
    mysql --user="HomeUser" --database="SmartHome" -p"noPass1234" < "Scripts/RenewTestData.sql"
    ```
4. Restore all nugget packages
    ```
    cd SmartHome.Web
    dotnet restore
    ```
5. Download all required npm packages
   ```
   # expected directory: SmartHome.Web
   npm install
   ```
6. Create configuration files from defaults, for example from *appsettings.development.json.defaults* create a valid *appsettings.development.json* replacing some placeholders.
   
 ## How to run
 Go to *SmartHome.Web* directory and run
```
dotnet build
dotnet run
```

## How to run rests
Go to project root directory and run
```
dotnet test
```