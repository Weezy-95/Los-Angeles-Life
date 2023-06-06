/*
 Navicat Premium Data Transfer

 Source Server         : localhost_4406
 Source Server Type    : MySQL
 Source Server Version : 80033 (8.0.33)
 Source Host           : localhost:4406
 Source Schema         : altv

 Target Server Type    : MySQL
 Target Server Version : 80033 (8.0.33)
 File Encoding         : 65001

 Date: 06/06/2023 19:24:51
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `PlayerID` int NOT NULL AUTO_INCREMENT,
  `DiscordID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PlayerName` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SocialClub` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AdminLevel` int NOT NULL DEFAULT 0,
  `IsWhitelisted` tinyint(1) NOT NULL DEFAULT 0,
  `FactionId` int NULL DEFAULT NULL,
  PRIMARY KEY (`PlayerID`) USING BTREE,
  INDEX `DiscordID`(`DiscordID` ASC) USING BTREE,
  INDEX `FactionId_FK`(`FactionId` ASC) USING BTREE,
  CONSTRAINT `FactionId_FK` FOREIGN KEY (`FactionId`) REFERENCES `factions` (`FactionId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1002 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES (1000, '244741995917082624', 'Kai_Mahone', '306012812', 5, 0, NULL);
INSERT INTO `accounts` VALUES (1001, '398114157276299264', 'Alex', '309928410', 4, 0, NULL);

-- ----------------------------
-- Table structure for factionranks
-- ----------------------------
DROP TABLE IF EXISTS `factionranks`;
CREATE TABLE `factionranks`  (
  `FactionRankId` int NOT NULL AUTO_INCREMENT,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionRankName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionRankPermission` int NOT NULL DEFAULT 0,
  PRIMARY KEY (`FactionRankId`) USING BTREE,
  INDEX `FactionName_FK`(`FactionName` ASC) USING BTREE,
  CONSTRAINT `FactionName_FK` FOREIGN KEY (`FactionName`) REFERENCES `factions` (`FactionName`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1015 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factionranks
-- ----------------------------

-- ----------------------------
-- Table structure for factions
-- ----------------------------
DROP TABLE IF EXISTS `factions`;
CREATE TABLE `factions`  (
  `FactionId` int NOT NULL AUTO_INCREMENT,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FactionLocationX` float NOT NULL,
  `FactionLocationY` float NOT NULL,
  `FactionLocationZ` float NOT NULL,
  `FactionBlipId` int NOT NULL,
  `FactionBlipColorId` int NOT NULL,
  `FactionMoney` float NULL DEFAULT NULL,
  PRIMARY KEY (`FactionId`) USING BTREE,
  INDEX `FactionName`(`FactionName` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1011 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factions
-- ----------------------------
INSERT INTO `factions` VALUES (1000, 'LSPD', 446.085, -987.151, 43.686, 60, 0, 555.88);
INSERT INTO `factions` VALUES (1008, 'LSMC', 314.254, -577.648, 94.471, 61, 1, 50000.5);
INSERT INTO `factions` VALUES (1009, 'FIB', 135.995, -749.248, 258.15, 564, 0, 50000.5);
INSERT INTO `factions` VALUES (1010, 'ACLS', -336.145, -136.865, 60.452, 446, 5, 5000.66);

-- ----------------------------
-- Table structure for garages
-- ----------------------------
DROP TABLE IF EXISTS `garages`;
CREATE TABLE `garages`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `LocationX` float NULL DEFAULT 0,
  `LocationY` float NULL DEFAULT 0,
  `LocationZ` float NULL DEFAULT 0,
  `BlipId` int NULL DEFAULT 0,
  `BlipColorId` int NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1001 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of garages
-- ----------------------------
INSERT INTO `garages` VALUES (1000, 'Garage 1', 213.824, -808.906, 30.9985, 225, 25);

-- ----------------------------
-- Table structure for garagespawnpositions
-- ----------------------------
DROP TABLE IF EXISTS `garagespawnpositions`;
CREATE TABLE `garagespawnpositions`  (
  `SpawnPositionId` int NOT NULL AUTO_INCREMENT,
  `GarageId` int NULL DEFAULT NULL,
  `PositionX` float NULL DEFAULT NULL,
  `PositionY` float NULL DEFAULT NULL,
  `PositionZ` float NULL DEFAULT NULL,
  `Rotation` float NULL DEFAULT NULL,
  PRIMARY KEY (`SpawnPositionId`) USING BTREE,
  INDEX `GarageSpawnPositionId_FK`(`GarageId` ASC) USING BTREE,
  CONSTRAINT `GarageSpawnPositionId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1006 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of garagespawnpositions
-- ----------------------------
INSERT INTO `garagespawnpositions` VALUES (1000, 1000, 214.154, -804.29, 30.3414, 1.1379);
INSERT INTO `garagespawnpositions` VALUES (1001, 1000, 216.198, -799.305, 30.3414, 1.1379);
INSERT INTO `garagespawnpositions` VALUES (1003, 1000, 217.873, -794.334, 30.3414, 1.1379);
INSERT INTO `garagespawnpositions` VALUES (1004, 1000, 220.022, -789.429, 30.3414, 1.1379);
INSERT INTO `garagespawnpositions` VALUES (1005, 1000, 221.855, -784.338, 30.3414, 1.1379);

-- ----------------------------
-- Table structure for garagestoragepositions
-- ----------------------------
DROP TABLE IF EXISTS `garagestoragepositions`;
CREATE TABLE `garagestoragepositions`  (
  `StoragePositionId` int NOT NULL AUTO_INCREMENT,
  `GarageId` int NULL DEFAULT NULL,
  `PositionX` float NULL DEFAULT NULL,
  `PositionY` float NULL DEFAULT NULL,
  `PositionZ` float NULL DEFAULT NULL,
  `Rotation` float NULL DEFAULT NULL,
  PRIMARY KEY (`StoragePositionId`) USING BTREE,
  INDEX `GarageStoragePositionId_FK`(`GarageId` ASC) USING BTREE,
  CONSTRAINT `GarageStoragePositionId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1005 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of garagestoragepositions
-- ----------------------------
INSERT INTO `garagestoragepositions` VALUES (1000, 1000, 234.989, -752.123, 30.392, -1.88001);
INSERT INTO `garagestoragepositions` VALUES (1001, 1000, 242.545, -756.58, 30.392, -1.92948);
INSERT INTO `garagestoragepositions` VALUES (1002, 1000, 236.519, -739.78, 30.392, 1.18737);
INSERT INTO `garagestoragepositions` VALUES (1003, 1000, 234.791, -751.49, 34.2, -1.88001);
INSERT INTO `garagestoragepositions` VALUES (1004, 1000, 258.462, -747.785, 34.2, -0.346317);

-- ----------------------------
-- Table structure for garagestorages
-- ----------------------------
DROP TABLE IF EXISTS `garagestorages`;
CREATE TABLE `garagestorages`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `GarageId` int NULL DEFAULT NULL,
  `VehicleId` bigint NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `GarageStorageId_FK`(`GarageId` ASC) USING BTREE,
  INDEX `GarageVehicleId_FK`(`VehicleId` ASC) USING BTREE,
  CONSTRAINT `GarageStorageId_FK` FOREIGN KEY (`GarageId`) REFERENCES `garages` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `GarageVehicleId_FK` FOREIGN KEY (`VehicleId`) REFERENCES `vehicles` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of garagestorages
-- ----------------------------

-- ----------------------------
-- Table structure for peds
-- ----------------------------
DROP TABLE IF EXISTS `peds`;
CREATE TABLE `peds`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` int NOT NULL,
  `Hash` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PositionX` float NOT NULL,
  `PositionY` float NOT NULL,
  `PositionZ` float NOT NULL,
  `Rotation` float NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1002 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of peds
-- ----------------------------
INSERT INTO `peds` VALUES (1000, 'Test1', 4, 'a_m_m_acult_01', 213.74, -789.46, 30.84, 0.09);
INSERT INTO `peds` VALUES (1001, 'Test2', 4, 'a_m_m_acult_01', 213.74, -789.46, 30.84, 0.09);

-- ----------------------------
-- Table structure for playerfinances
-- ----------------------------
DROP TABLE IF EXISTS `playerfinances`;
CREATE TABLE `playerfinances`  (
  `PlayerFinanceId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cash` float NULL DEFAULT 0,
  `Bank` float NULL DEFAULT 0,
  PRIMARY KEY (`PlayerFinanceId`) USING BTREE,
  CONSTRAINT `PlayerFinanceId_FK` FOREIGN KEY (`PlayerFinanceId`) REFERENCES `accounts` (`DiscordID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of playerfinances
-- ----------------------------
INSERT INTO `playerfinances` VALUES ('244741995917082624', 1500, 0);
INSERT INTO `playerfinances` VALUES ('398114157276299264', 1500, 0);

-- ----------------------------
-- Table structure for playerpositions
-- ----------------------------
DROP TABLE IF EXISTS `playerpositions`;
CREATE TABLE `playerpositions`  (
  `PositionId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PlayerPosX` float NOT NULL,
  `PlayerPosY` float NOT NULL,
  `PlayerPosZ` float NOT NULL,
  `PlayerRotation` float NOT NULL,
  `playerDimension` int NOT NULL,
  PRIMARY KEY (`PositionId`) USING BTREE,
  CONSTRAINT `PositionId_FK` FOREIGN KEY (`PositionId`) REFERENCES `accounts` (`DiscordID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of playerpositions
-- ----------------------------
INSERT INTO `playerpositions` VALUES ('244741995917082624', 207.138, -797.604, 30.9817, -0.989478, 0);
INSERT INTO `playerpositions` VALUES ('398114157276299264', 210.462, -794.308, 30.9143, -0.494739, 0);

-- ----------------------------
-- Table structure for vehicles
-- ----------------------------
DROP TABLE IF EXISTS `vehicles`;
CREATE TABLE `vehicles`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `SessionId` int NULL DEFAULT NULL,
  `VehicleTemplateId` int NULL DEFAULT NULL,
  `FactionId` int NULL DEFAULT NULL,
  `GarageStorageId` int NULL DEFAULT NULL,
  `Owner` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Fuel` float NULL DEFAULT NULL,
  `Mileage` float NULL DEFAULT NULL,
  `IsEngineHealthy` tinyint(1) NULL DEFAULT 1,
  `IsLocked` tinyint(1) NULL DEFAULT 1,
  `IsInGarage` tinyint(1) NULL DEFAULT 0,
  `PositionX` float NULL DEFAULT NULL,
  `PositionY` float NULL DEFAULT NULL,
  `PositionZ` float NULL DEFAULT NULL,
  `Rotation` float NULL DEFAULT NULL,
  `Plate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1011 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of vehicles
-- ----------------------------
INSERT INTO `vehicles` VALUES (1009, 0, 1000, 0, 0, '398114157276299264', 60, 0, 1, 1, 0, 1183.21, -594.673, 63.6198, 0.21935, 'PO PO 1337');
INSERT INTO `vehicles` VALUES (1010, 1, 1001, 0, 0, '398114157276299264', 65, 0, 1, 1, 0, 213.732, -802.022, 30.4594, -1.94285, 'PO PO 1337');

-- ----------------------------
-- Table structure for vehicletemplates
-- ----------------------------
DROP TABLE IF EXISTS `vehicletemplates`;
CREATE TABLE `vehicletemplates`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ModelId` bigint NULL DEFAULT NULL,
  `Fuel` float NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1002 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of vehicletemplates
-- ----------------------------
INSERT INTO `vehicletemplates` VALUES (1000, 'Police', 2046537925, 60);
INSERT INTO `vehicletemplates` VALUES (1001, 'Police 2', 2667966721, 65);

SET FOREIGN_KEY_CHECKS = 1;
