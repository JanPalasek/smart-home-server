-- MySQL dump 10.13  Distrib 5.7.27, for Linux (x86_64)
--
-- Host: localhost    Database: SmartHome
-- ------------------------------------------------------
-- Server version	5.7.27-0ubuntu0.18.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` bigint(20) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoles` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` bigint(20) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserRoles` (
  `UserId` bigint(20) NOT NULL,
  `RoleId` bigint(20) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserTokens` (
  `UserId` bigint(20) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUsers` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES (1,'Admin','ADMIN','admin@janpalasek.com','ADMIN@JANPALASEK.COM',_binary '\0','AQAAAAEAACcQAAAAEF19wTR6dzsUw4HKccl3FOI5dIJXeZ6MbN3Hajjl1tzV3gs7+0czrWkifYaft4BW8A==','YQSLUSWC7FRFW6X3IGOFMDOQ7JBECCES','f304cb0b-1d1c-4ebd-88c4-8bee095d66ad',NULL,_binary '\0',_binary '\0',NULL,_binary '',0);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BatteryMeasurement`
--

DROP TABLE IF EXISTS `BatteryMeasurement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BatteryMeasurement` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `MeasurementDateTime` datetime(6) NOT NULL,
  `PlaceId` bigint(20) NOT NULL,
  `Voltage` double NOT NULL,
  `BatteryPowerSourceTypeId` bigint(20) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_BatteryMeasurement_BatteryPowerSourceTypeId` (`BatteryPowerSourceTypeId`),
  KEY `IX_BatteryMeasurement_MeasurementDateTime` (`MeasurementDateTime`),
  KEY `IX_BatteryMeasurement_PlaceId` (`PlaceId`),
  KEY `IX_BatteryMeasurement_SensorId` (`SensorId`),
  CONSTRAINT `FK_BatteryMeasurement_BatteryPowerSourceType_BatteryPowerSource~` FOREIGN KEY (`BatteryPowerSourceTypeId`) REFERENCES `BatteryPowerSourceType` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BatteryMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_BatteryMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BatteryMeasurement`
--

LOCK TABLES `BatteryMeasurement` WRITE;
/*!40000 ALTER TABLE `BatteryMeasurement` DISABLE KEYS */;
/*!40000 ALTER TABLE `BatteryMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BatteryPowerSourceType`
--

DROP TABLE IF EXISTS `BatteryPowerSourceType`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BatteryPowerSourceType` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `BatteryType` int(11) NOT NULL,
  `MinimumVoltage` double NOT NULL,
  `MaximumVoltage` double NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BatteryPowerSourceType`
--

LOCK TABLES `BatteryPowerSourceType` WRITE;
/*!40000 ALTER TABLE `BatteryPowerSourceType` DISABLE KEYS */;
/*!40000 ALTER TABLE `BatteryPowerSourceType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `HumidityMeasurement`
--

DROP TABLE IF EXISTS `HumidityMeasurement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `HumidityMeasurement` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `MeasurementDateTime` datetime(6) NOT NULL,
  `PlaceId` bigint(20) NOT NULL,
  `Humidity` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_HumidityMeasurement_MeasurementDateTime` (`MeasurementDateTime`),
  KEY `IX_HumidityMeasurement_PlaceId` (`PlaceId`),
  KEY `IX_HumidityMeasurement_SensorId` (`SensorId`),
  CONSTRAINT `FK_HumidityMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_HumidityMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `HumidityMeasurement`
--

LOCK TABLES `HumidityMeasurement` WRITE;
/*!40000 ALTER TABLE `HumidityMeasurement` DISABLE KEYS */;
/*!40000 ALTER TABLE `HumidityMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Place`
--

DROP TABLE IF EXISTS `Place`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Place` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `IsInside` bit(1) NOT NULL,
  `Note` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Place_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Place`
--

LOCK TABLES `Place` WRITE;
/*!40000 ALTER TABLE `Place` DISABLE KEYS */;
INSERT INTO `Place` VALUES (1,'Place nam',_binary '\0','aaaa\r\n10f');
/*!40000 ALTER TABLE `Place` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Sensor`
--

DROP TABLE IF EXISTS `Sensor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Sensor` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorTypeId` bigint(20) NOT NULL,
  `BatteryPowerSourceTypeId` bigint(20) DEFAULT NULL,
  `PlaceId` bigint(20) DEFAULT NULL,
  `MinimumRequiredVoltage` double DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sensor_BatteryPowerSourceTypeId` (`BatteryPowerSourceTypeId`),
  KEY `IX_Sensor_PlaceId` (`PlaceId`),
  KEY `IX_Sensor_SensorTypeId` (`SensorTypeId`),
  CONSTRAINT `FK_Sensor_BatteryPowerSourceType_BatteryPowerSourceTypeId` FOREIGN KEY (`BatteryPowerSourceTypeId`) REFERENCES `BatteryPowerSourceType` (`Id`),
  CONSTRAINT `FK_Sensor_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`),
  CONSTRAINT `FK_Sensor_SensorType_SensorTypeId` FOREIGN KEY (`SensorTypeId`) REFERENCES `SensorType` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Sensor`
--

LOCK TABLES `Sensor` WRITE;
/*!40000 ALTER TABLE `Sensor` DISABLE KEYS */;
/*!40000 ALTER TABLE `Sensor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SensorType`
--

DROP TABLE IF EXISTS `SensorType`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `SensorType` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_SensorType_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SensorType`
--

LOCK TABLES `SensorType` WRITE;
/*!40000 ALTER TABLE `SensorType` DISABLE KEYS */;
/*!40000 ALTER TABLE `SensorType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TemperatureMeasurement`
--

DROP TABLE IF EXISTS `TemperatureMeasurement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `TemperatureMeasurement` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `SensorId` bigint(20) NOT NULL,
  `MeasurementDateTime` datetime(6) NOT NULL,
  `PlaceId` bigint(20) NOT NULL,
  `Temperature` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TemperatureMeasurement_MeasurementDateTime` (`MeasurementDateTime`),
  KEY `IX_TemperatureMeasurement_PlaceId` (`PlaceId`),
  KEY `IX_TemperatureMeasurement_SensorId` (`SensorId`),
  CONSTRAINT `FK_TemperatureMeasurement_Place_PlaceId` FOREIGN KEY (`PlaceId`) REFERENCES `Place` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_TemperatureMeasurement_Sensor_SensorId` FOREIGN KEY (`SensorId`) REFERENCES `Sensor` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TemperatureMeasurement`
--

LOCK TABLES `TemperatureMeasurement` WRITE;
/*!40000 ALTER TABLE `TemperatureMeasurement` DISABLE KEYS */;
/*!40000 ALTER TABLE `TemperatureMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20190905172418_Initial','3.0.0'),('20191112211036_Roles','3.0.0');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-12 22:24:03
