/*
 Navicat Premium Data Transfer

 Source Server         : localhost_4406
 Source Server Type    : MySQL
 Source Server Version : 80033 (8.0.33)
 Source Host           : localhost:4406
 Source Schema         : altv_new

 Target Server Type    : MySQL
 Target Server Version : 80033 (8.0.33)
 File Encoding         : 65001

 Date: 02/06/2023 15:00:17
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
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for factionranks
-- ----------------------------
DROP TABLE IF EXISTS `factionranks`;
CREATE TABLE `factionranks`  (
  `FactionRankId` int NOT NULL AUTO_INCREMENT,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PermissionLevel` int NOT NULL DEFAULT 0,
  PRIMARY KEY (`FactionRankId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for factions
-- ----------------------------
DROP TABLE IF EXISTS `factions`;
CREATE TABLE `factions`  (
  `FactionId` int NOT NULL AUTO_INCREMENT,
  `FactionRankId` int NOT NULL,
  `FactionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`FactionId`) USING BTREE,
  INDEX `FactionRankId_FK`(`FactionRankId` ASC) USING BTREE,
  CONSTRAINT `FactionRankId_FK` FOREIGN KEY (`FactionRankId`) REFERENCES `factionranks` (`FactionRankId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB AUTO_INCREMENT = 1000 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
