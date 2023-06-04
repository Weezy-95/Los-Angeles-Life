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
INSERT INTO `playerpositions` VALUES ('244741995917082624',-7.0022,14.1495,71.0168,0,0),('398114157276299264',205.108,-49.7275,68.7252,0,0);
/*!40000 ALTER TABLE `playerpositions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-04 18:47:07
