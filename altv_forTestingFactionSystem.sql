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

 Date: 03/06/2023 16:52:13
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
) ENGINE = InnoDB AUTO_INCREMENT = 1013 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factionranks
-- ----------------------------
INSERT INTO `factionranks` VALUES (1000, 'LSPD', 'Penis', 10);
INSERT INTO `factionranks` VALUES (1001, 'LSPD', 'Muschi', 9);
INSERT INTO `factionranks` VALUES (1002, 'LSPD', 'Beides', 8);
INSERT INTO `factionranks` VALUES (1003, 'Test1', 'Hoden', 4);
INSERT INTO `factionranks` VALUES (1005, 'Test2', 'Hand', 5);
INSERT INTO `factionranks` VALUES (1012, 'Test1', 'Dieter Peter', 999);

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
) ENGINE = InnoDB AUTO_INCREMENT = 1006 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factions
-- ----------------------------
INSERT INTO `factions` VALUES (1000, 'LSPD', 1, 2, 3, 669, 8, 555.88);
INSERT INTO `factions` VALUES (1004, 'Test1', 4, 5, 6, 44, 5, 45);
INSERT INTO `factions` VALUES (1005, 'Test2', 8, 9, 10, 55, 77, 448);

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
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

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
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of playerpositions
-- ----------------------------
INSERT INTO `playerpositions` VALUES ('244741995917082624', 740.4, 1293.77, 360.294, 1.83053, 0);
INSERT INTO `playerpositions` VALUES ('398114157276299264', 205.108, -49.7275, 68.7252, 0, 0);

SET FOREIGN_KEY_CHECKS = 1;
