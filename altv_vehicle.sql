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

 Date: 05/06/2023 17:58:27
*/

CREATE DATABASE IF NOT EXISTS altv;
USE altv;

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
  `SpawnPositionX` float NULL DEFAULT 0,
  `SpawnPositionY` float NULL DEFAULT 0,
  `SpawnPositionZ` float NULL DEFAULT 0,
  `SpawnRotation` float NULL DEFAULT NULL,
  `StoragePositionX` float NULL DEFAULT NULL,
  `StoragePositionY` float NULL DEFAULT NULL,
  `StoragePositionZ` float NULL DEFAULT NULL,
  `BlipId` int NULL DEFAULT 0,
  `BlipColorId` int NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of garages
-- ----------------------------

-- ----------------------------
-- Table structure for garagestorages
-- ----------------------------
DROP TABLE IF EXISTS `garagestorages`;
CREATE TABLE `garagestorages`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `GarageId` int NULL DEFAULT NULL,
  `VehicleId` int NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of garagestorages
-- ----------------------------

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
INSERT INTO `playerpositions` VALUES ('244741995917082624', 0, 0, 75, 0, 0);
INSERT INTO `playerpositions` VALUES ('398114157276299264', 0, 0, 75, 0.940004, 0);

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
) ENGINE = InnoDB AUTO_INCREMENT = 1009 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of vehicles
-- ----------------------------

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
) ENGINE = InnoDB AUTO_INCREMENT = 1002 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of vehicletemplates
-- ----------------------------
INSERT INTO `vehicletemplates` VALUES (1000, 'Police', 2046537925, 60);
INSERT INTO `vehicletemplates` VALUES (1001, 'Police 2', 2667966721, 65);

SET FOREIGN_KEY_CHECKS = 1;
