CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `AspNetRoles` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` longtext NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` bit NOT NULL,
    `PasswordHash` longtext NULL,
    `SecurityStamp` longtext NULL,
    `ConcurrencyStamp` longtext NULL,
    `PhoneNumber` longtext NULL,
    `PhoneNumberConfirmed` bit NOT NULL,
    `TwoFactorEnabled` bit NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` bit NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `BatteryPowerSourceType` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `BatteryType` int NOT NULL,
    `MinimumVoltage` double NOT NULL,
    `MaximumVoltage` double NOT NULL,
    CONSTRAINT `PK_BatteryPowerSourceType` PRIMARY KEY (`Id`)
);

CREATE TABLE `Place` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    `IsInside` bit NOT NULL,
    `Note` longtext NULL,
    CONSTRAINT `PK_Place` PRIMARY KEY (`Id`)
);

CREATE TABLE `SensorType` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    `Description` longtext NULL,
    CONSTRAINT `PK_SensorType` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` bigint NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` bigint NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) NOT NULL,
    `ProviderKey` varchar(255) NOT NULL,
    `ProviderDisplayName` longtext NULL,
    `UserId` bigint NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` bigint NOT NULL,
    `RoleId` bigint NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` bigint NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `Value` longtext NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Sensor` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `SensorTypeId` bigint NOT NULL,
    `BatteryPowerSourceTypeId` bigint NULL,
    `PlaceId` bigint NULL,
    `MinimumRequiredVoltage` double NULL,
    CONSTRAINT `PK_Sensor` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Sensor_BatteryPowerSourceType_BatteryPowerSourceTypeId` FOREIGN KEY (`BatteryPowerSourceTypeId`) REFERENCES `BatteryPowerSourceType` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Sensor_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Sensor_SensorType_SensorTypeId` FOREIGN KEY (`SensorTypeId`) REFERENCES `SensorType` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `BatteryMeasurement` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `SensorId` bigint NOT NULL,
    `MeasurementDateTime` datetime(6) NOT NULL,
    `PlaceId` bigint NOT NULL,
    `Voltage` double NOT NULL,
    `BatteryPowerSourceTypeId` bigint NOT NULL,
    CONSTRAINT `PK_BatteryMeasurement` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BatteryMeasurement_BatteryPowerSourceType_BatteryPowerSource~` FOREIGN KEY (`BatteryPowerSourceTypeId`) REFERENCES `BatteryPowerSourceType` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_BatteryMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_BatteryMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `HumidityMeasurement` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `SensorId` bigint NOT NULL,
    `MeasurementDateTime` datetime(6) NOT NULL,
    `PlaceId` bigint NOT NULL,
    `Humidity` double NOT NULL,
    CONSTRAINT `PK_HumidityMeasurement` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_HumidityMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_HumidityMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `TemperatureMeasurement` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `SensorId` bigint NOT NULL,
    `MeasurementDateTime` datetime(6) NOT NULL,
    `PlaceId` bigint NOT NULL,
    `Temperature` double NOT NULL,
    CONSTRAINT `PK_TemperatureMeasurement` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TemperatureMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TemperatureMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_BatteryMeasurement_BatteryPowerSourceTypeId` ON `BatteryMeasurement` (`BatteryPowerSourceTypeId`);

CREATE INDEX `IX_BatteryMeasurement_MeasurementDateTime` ON `BatteryMeasurement` (`MeasurementDateTime`);

CREATE INDEX `IX_BatteryMeasurement_PlaceId` ON `BatteryMeasurement` (`PlaceId`);

CREATE INDEX `IX_BatteryMeasurement_SensorId` ON `BatteryMeasurement` (`SensorId`);

CREATE INDEX `IX_HumidityMeasurement_MeasurementDateTime` ON `HumidityMeasurement` (`MeasurementDateTime`);

CREATE INDEX `IX_HumidityMeasurement_PlaceId` ON `HumidityMeasurement` (`PlaceId`);

CREATE INDEX `IX_HumidityMeasurement_SensorId` ON `HumidityMeasurement` (`SensorId`);

CREATE UNIQUE INDEX `IX_Place_Name` ON `Place` (`Name`);

CREATE INDEX `IX_Sensor_BatteryPowerSourceTypeId` ON `Sensor` (`BatteryPowerSourceTypeId`);

CREATE INDEX `IX_Sensor_PlaceId` ON `Sensor` (`PlaceId`);

CREATE INDEX `IX_Sensor_SensorTypeId` ON `Sensor` (`SensorTypeId`);

CREATE UNIQUE INDEX `IX_SensorType_Name` ON `SensorType` (`Name`);

CREATE INDEX `IX_TemperatureMeasurement_MeasurementDateTime` ON `TemperatureMeasurement` (`MeasurementDateTime`);

CREATE INDEX `IX_TemperatureMeasurement_PlaceId` ON `TemperatureMeasurement` (`PlaceId`);

CREATE INDEX `IX_TemperatureMeasurement_SensorId` ON `TemperatureMeasurement` (`SensorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190905172418_Initial', '3.0.0');

