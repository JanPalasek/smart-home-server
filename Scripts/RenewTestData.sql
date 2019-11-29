-- MySQL dump 10.13  Distrib 5.7.28, for Linux (x86_64)
--
-- Host: localhost    Database: SmartHome
-- ------------------------------------------------------
-- Server version	5.7.28-0ubuntu0.18.04.4

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
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
INSERT INTO `AspNetRoles` VALUES (1,'Admin','ADMIN','c1c0b684-517d-4409-bb07-72b23df0498c'),(2,'User','USER','49ed20ba-25d5-433c-be2e-af25d9b50009');
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
INSERT INTO `AspNetUserRoles` VALUES (3,1),(3,2),(4,2);
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES (3,'Admin','ADMIN','admin@janpalasek.com','ADMIN@JANPALASEK.COM',_binary '\0','AQAAAAEAACcQAAAAEEqs7FX6HkgwsEMGGLxsD/3Sa0OaPKXblxttwMM64JvTmdQxuUGv5VXwBndCVyh+8A==','OESB4BWEJCO3KVM77VOFY7FLSY56IGRV','b42a3ff3-1649-4409-bdcf-c8f853a58402',NULL,_binary '\0',_binary '\0',NULL,_binary '\0',0),(4,'User','USER','user@janpalasek.com','USER@JANPALASEK.COM',_binary '\0','AQAAAAEAACcQAAAAELghlHWCGXlODnXNTmp4Hzr/2NPFSvz3lRP2q3IzyxKZqIT5fTEQ+AHuJHa6Tz2S0Q==','XLMDUQC4YYW2ZGAMCERWZVWGJLJCWJFX','7374b2f6-1f7d-42ba-8eaa-409b8fd409ae',NULL,_binary '\0',_binary '\0',NULL,_binary '',0);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `BatteryMeasurement`
--

LOCK TABLES `BatteryMeasurement` WRITE;
/*!40000 ALTER TABLE `BatteryMeasurement` DISABLE KEYS */;
/*!40000 ALTER TABLE `BatteryMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `BatteryPowerSourceType`
--

LOCK TABLES `BatteryPowerSourceType` WRITE;
/*!40000 ALTER TABLE `BatteryPowerSourceType` DISABLE KEYS */;
INSERT INTO `BatteryPowerSourceType` VALUES (1,'2 AA',0,2,3),(2,'3 AA',2,3.3,3.9);
/*!40000 ALTER TABLE `BatteryPowerSourceType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `HumidityMeasurement`
--

LOCK TABLES `HumidityMeasurement` WRITE;
/*!40000 ALTER TABLE `HumidityMeasurement` DISABLE KEYS */;
/*!40000 ALTER TABLE `HumidityMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Place`
--

LOCK TABLES `Place` WRITE;
/*!40000 ALTER TABLE `Place` DISABLE KEYS */;
INSERT INTO `Place` VALUES (1,'Bathroom',_binary '',NULL),(2,'Living room',_binary '',NULL);
/*!40000 ALTER TABLE `Place` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Sensor`
--

LOCK TABLES `Sensor` WRITE;
/*!40000 ALTER TABLE `Sensor` DISABLE KEYS */;
INSERT INTO `Sensor` VALUES (1,'DS18B20 Temperature sensor 1',1,1,1,3),(2,'DHT11 hum and temp 1',2,2,2,3);
/*!40000 ALTER TABLE `Sensor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `SensorType`
--

LOCK TABLES `SensorType` WRITE;
/*!40000 ALTER TABLE `SensorType` DISABLE KEYS */;
INSERT INTO `SensorType` VALUES (1,'DS18B20','Temperature sensor DS1820 whose purpose is to measure temperature.'),(2,'DHT11','Humidity and temperature sensor');
/*!40000 ALTER TABLE `SensorType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `TemperatureMeasurement`
--

LOCK TABLES `TemperatureMeasurement` WRITE;
/*!40000 ALTER TABLE `TemperatureMeasurement` DISABLE KEYS */;
INSERT INTO `TemperatureMeasurement` VALUES (1,1,'2019-11-06 00:00:00.000000',2,23.4),(2,1,'2019-10-30 00:00:00.000000',2,23),(3,2,'2019-11-29 00:00:00.000000',2,23),(4,2,'2019-11-29 22:30:00.000000',1,30);
/*!40000 ALTER TABLE `TemperatureMeasurement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20191129191030_Initial','3.0.0');
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

-- Dump completed on 2019-11-29 22:45:42
