-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: altv
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accounts`
--
CREATE DATABASE IF NOT EXISTS altv;
USE altv;

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `PlayerID` int NOT NULL AUTO_INCREMENT,
  `DiscordID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PlayerName` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SocialClub` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AdminLevel` int NOT NULL DEFAULT '0',
  `IsWhitelisted` tinyint(1) NOT NULL DEFAULT '0',
  `FactionId` int DEFAULT NULL,
  PRIMARY KEY (`PlayerID`) USING BTREE,
  KEY `DiscordID` (`DiscordID`) USING BTREE,
  KEY `FactionId_FK` (`FactionId`) USING BTREE,
  CONSTRAINT `FactionId_FK` FOREIGN KEY (`FactionId`) REFERENCES `factions` (`FactionId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=1002 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES (1000,'244741995917082624','Kai_Mahone','306012812',5,0,NULL),(1001,'398114157276299264','Alex','309928410',4,0,NULL);
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `factionranks`
--

DROP TABLE IF EXISTS `factionranks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `factionranks` (
  `FactionRankId` int NOT NULL AUTO_INCREMENT,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionRankName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionRankPermission` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`FactionRankId`) USING BTREE,
  KEY `FactionName_FK` (`FactionName`) USING BTREE,
  CONSTRAINT `FactionName_FK` FOREIGN KEY (`FactionName`) REFERENCES `factions` (`FactionName`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=1015 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factionranks`
--

LOCK TABLES `factionranks` WRITE;
/*!40000 ALTER TABLE `factionranks` DISABLE KEYS */;
/*!40000 ALTER TABLE `factionranks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `factions`
--

DROP TABLE IF EXISTS `factions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `factions` (
  `FactionId` int NOT NULL AUTO_INCREMENT,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionLocationX` float NOT NULL,
  `FactionLocationY` float NOT NULL,
  `FactionLocationZ` float NOT NULL,
  `FactionBlipId` int NOT NULL,
  `FactionBlipColorId` int NOT NULL,
  `FactionMoney` float DEFAULT NULL,
  PRIMARY KEY (`FactionId`) USING BTREE,
  KEY `FactionName` (`FactionName`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1011 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factions`
--

LOCK TABLES `factions` WRITE;
/*!40000 ALTER TABLE `factions` DISABLE KEYS */;
INSERT INTO `factions` VALUES (1000,'LSPD',446.085,-987.151,43.686,60,0,555.88),(1008,'LSMC',314.254,-577.648,94.471,61,1,50000.5),(1009,'FIB',135.995,-749.248,258.15,564,0,50000.5),(1010,'ACLS',-336.145,-136.865,60.452,446,5,5000.66);
/*!40000 ALTER TABLE `factions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `garages`
--

DROP TABLE IF EXISTS `garages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `garages` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `LocationX` float DEFAULT '0',
  `LocationY` float DEFAULT '0',
  `LocationZ` float DEFAULT '0',
  `BlipId` int DEFAULT '0',
  `BlipColorId` int DEFAULT '0',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1001 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `garages`
--

LOCK TABLES `garages` WRITE;
/*!40000 ALTER TABLE `garages` DISABLE KEYS */;
INSERT INTO `garages` VALUES (1000,'Würfelpark Garage',213.824,-808.906,30.9985,50,0);
/*!40000 ALTER TABLE `garages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `garagespawnpositions`
--

DROP TABLE IF EXISTS `garagespawnpositions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `garagespawnpositions` (
  `SpawnPositionId` int NOT NULL AUTO_INCREMENT,
  `GarageId` int DEFAULT NULL,
  `PositionX` float DEFAULT NULL,
  `PositionY` float DEFAULT NULL,
  `PositionZ` float DEFAULT NULL,
  `Rotation` float DEFAULT NULL,
  PRIMARY KEY (`SpawnPositionId`) USING BTREE,
  KEY `GarageSpawnPositionId_FK` (`GarageId`) USING BTREE,
  CONSTRAINT `GarageSpawnPositionId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=1007 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `garagespawnpositions`
--

LOCK TABLES `garagespawnpositions` WRITE;
/*!40000 ALTER TABLE `garagespawnpositions` DISABLE KEYS */;
INSERT INTO `garagespawnpositions` VALUES (1006,1000,228.949,-802.76,30.386,2.77);
/*!40000 ALTER TABLE `garagespawnpositions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `garagestoragepositions`
--

DROP TABLE IF EXISTS `garagestoragepositions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `garagestoragepositions` (
  `StoragePositionId` int NOT NULL AUTO_INCREMENT,
  `GarageId` int DEFAULT NULL,
  `PositionX` float DEFAULT NULL,
  `PositionY` float DEFAULT NULL,
  `PositionZ` float DEFAULT NULL,
  `Rotation` float DEFAULT NULL,
  PRIMARY KEY (`StoragePositionId`) USING BTREE,
  KEY `GarageStoragePositionId_FK` (`GarageId`) USING BTREE,
  CONSTRAINT `GarageStoragePositionId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=1005 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `garagestoragepositions`
--

LOCK TABLES `garagestoragepositions` WRITE;
/*!40000 ALTER TABLE `garagestoragepositions` DISABLE KEYS */;
INSERT INTO `garagestoragepositions` VALUES (1000,1000,212.94,-796.694,30.386,-0.395);
/*!40000 ALTER TABLE `garagestoragepositions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `garagestorages`
--

DROP TABLE IF EXISTS `garagestorages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `garagestorages` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `GarageId` int DEFAULT NULL,
  `VehicleId` bigint DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `GarageStorageId_FK` (`GarageId`) USING BTREE,
  KEY `GarageVehicleId_FK` (`VehicleId`) USING BTREE,
  CONSTRAINT `GarageStorageId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `GarageVehicleId_FK` FOREIGN KEY (`VehicleId`) REFERENCES `vehicles` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=1000 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `garagestorages`
--

LOCK TABLES `garagestorages` WRITE;
/*!40000 ALTER TABLE `garagestorages` DISABLE KEYS */;
/*!40000 ALTER TABLE `garagestorages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `peds`
--

DROP TABLE IF EXISTS `peds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `peds` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `Hash` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PositionX` float NOT NULL,
  `PositionY` float NOT NULL,
  `PositionZ` float NOT NULL,
  `Rotation` float NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1005 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `peds`
--

LOCK TABLES `peds` WRITE;
/*!40000 ALTER TABLE `peds` DISABLE KEYS */;
INSERT INTO `peds` VALUES (1003,'WürfelparkGarage',4,'s_m_m_autoshop_02',213.784,-808.47,29.992,2.72);
/*!40000 ALTER TABLE `peds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playerfinances`
--

DROP TABLE IF EXISTS `playerfinances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playerfinances` (
  `PlayerFinanceId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cash` float DEFAULT '0',
  `Bank` float DEFAULT '0',
  PRIMARY KEY (`PlayerFinanceId`) USING BTREE,
  CONSTRAINT `PlayerFinanceId_FK` FOREIGN KEY (`PlayerFinanceId`) REFERENCES `accounts` (`DiscordID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerfinances`
--

LOCK TABLES `playerfinances` WRITE;
/*!40000 ALTER TABLE `playerfinances` DISABLE KEYS */;
INSERT INTO `playerfinances` VALUES ('244741995917082624',1500,0),('398114157276299264',1500,0);
/*!40000 ALTER TABLE `playerfinances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playerpositions`
--

DROP TABLE IF EXISTS `playerpositions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playerpositions` (
  `PositionId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PlayerPosX` float NOT NULL,
  `PlayerPosY` float NOT NULL,
  `PlayerPosZ` float NOT NULL,
  `PlayerRotation` float NOT NULL,
  `playerDimension` int NOT NULL,
  PRIMARY KEY (`PositionId`) USING BTREE,
  CONSTRAINT `PositionId_FK` FOREIGN KEY (`PositionId`) REFERENCES `accounts` (`DiscordID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerpositions`
--

LOCK TABLES `playerpositions` WRITE;
/*!40000 ALTER TABLE `playerpositions` DISABLE KEYS */;
INSERT INTO `playerpositions` VALUES ('244741995917082624',201.152,-839.473,30.1223,-1.48422,0),('398114157276299264',210.462,-794.308,30.9143,-0.494739,0);
/*!40000 ALTER TABLE `playerpositions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicles`
--

DROP TABLE IF EXISTS `vehicles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicles` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `SessionId` int DEFAULT NULL,
  `VehicleTemplateId` int DEFAULT NULL,
  `FactionId` int DEFAULT NULL,
  `GarageStorageId` int DEFAULT NULL,
  `Owner` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Fuel` float DEFAULT NULL,
  `Mileage` float DEFAULT NULL,
  `IsEngineHealthy` tinyint(1) DEFAULT '1',
  `IsLocked` tinyint(1) DEFAULT '1',
  `IsInGarage` tinyint(1) DEFAULT '0',
  `PositionX` float DEFAULT NULL,
  `PositionY` float DEFAULT NULL,
  `PositionZ` float DEFAULT NULL,
  `Rotation` float DEFAULT NULL,
  `Plate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1066 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicles`
--

LOCK TABLES `vehicles` WRITE;
/*!40000 ALTER TABLE `vehicles` DISABLE KEYS */;
/*!40000 ALTER TABLE `vehicles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicletemplates`
--

DROP TABLE IF EXISTS `vehicletemplates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicletemplates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ModelId` bigint DEFAULT NULL,
  `Fuel` float DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1050 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicletemplates`
--

LOCK TABLES `vehicletemplates` WRITE;
/*!40000 ALTER TABLE `vehicletemplates` DISABLE KEYS */;
INSERT INTO `vehicletemplates` VALUES (1000,'Police',2046537925,60),(1001,'Police 2',2667966721,65),(1002,'Sea Shark',3264692260,15),(1003,'Dinghy 3',509498602,40),(1004,'Mule',904750859,80),(1005,'Issi',3117103977,45),(1006,'Blista',3950024287,45),(1007,'Windsor 2',2364918497,60),(1008,'Bmx',1131912276,0),(1009,'Cruiser',448402357,0),(1010,'Scorcher',4108429845,0),(1011,'Trikebike',1127861609,0),(1012,'Fbi 2',2647026068,65),(1013,'Fbi',1127131465,65),(1014,'Ambulance',1171614426,80),(1015,'Firetruck',1938952078,80),(1016,'Police Prisoner Bus',2287941233,80),(1017,'Police 3',1912215274,65),(1018,'Police Bike',4260343491,35),(1019,'Police Bus',456714581,60),(1020,'Police Maverick',353883353,80),(1021,'Police Sherrif 2',1922257928,60),(1022,'Police Sherrif',2611638396,45),(1023,'Buzzard',745926877,80),(1024,'Frogger',744705981,80),(1025,'Havok',2310691317,60),(1026,'Maverick',2634305738,60),(1027,'Seasparrow',3568198617,50),(1028,'Flatbed',1353720154,60),(1029,'Akuma',1672195559,40),(1030,'Bati 2',3403504941,40),(1031,'Daemon',2006142190,45),(1032,'Faggio 2',55628203,15),(1033,'Sanchez',788045382,35),(1034,'Blade',3089165662,45),(1035,'Buccaneer',3612755468,45),(1036,'Ratloader',3627815886,65),(1037,'Caracara 2',2945871676,65),(1038,'Bus',3581397346,80),(1039,'Taxi',3338918751,35),(1040,'Coquette',108773431,45),(1041,'Jugular',4086055493,60),(1042,'Itali GTB',2246633323,60),(1043,'Itali GTO',3963499524,60),(1044,'Comet 6',2568944644,50),(1045,'Growler',1304459735,55),(1046,'Cypher',1755697647,60),(1047,'Tempesta',272929391,65),(1048,'Mower',1783355638,5),(1049,'Caddy',1147287684,5);
/*!40000 ALTER TABLE `vehicletemplates` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-11 11:57:22
