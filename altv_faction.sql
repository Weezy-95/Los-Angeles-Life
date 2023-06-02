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
  PRIMARY KEY (`FactionRankId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1002 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factionranks
-- ----------------------------
INSERT INTO `factionranks` VALUES (1000, 'LSPD', 'Penis', 10);
INSERT INTO `factionranks` VALUES (1001, 'LSPD', 'Muschi', 9);

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
  PRIMARY KEY (`FactionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1001 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of factions
-- ----------------------------
INSERT INTO `factions` VALUES (1000, 'LSPD', 0, 0, 0, 669, 8, 555.88);

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
INSERT INTO `playerpositions` VALUES ('398114157276299264', 190.615, -68.4923, 68.5736, -1.88001, 0);

SET FOREIGN_KEY_CHECKS = 1;
