/*
 Navicat Premium Data Transfer

 Source Server         : mycentos
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : 112.74.59.197:3306
 Source Schema         : Organization

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 02/09/2019 16:34:49
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Roles
-- ----------------------------
DROP TABLE IF EXISTS `Roles`;
CREATE TABLE `Roles`  (
  `Id` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Name` varchar(64) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Level` int(255) NOT NULL,
  `IsDelete` bit(1) NOT NULL,
  `CreateUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateDate` datetime(0) NOT NULL,
  `ModifyUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Roles
-- ----------------------------
INSERT INTO `Roles` VALUES ('DD1AF84F-7570-4D1E-93F6-F67330689367', 'admin', 1, b'0', '00000000-0000-0000-0000-000000000000', 'admin', '2019-08-31 00:00:00', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for Users
-- ----------------------------
DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users`  (
  `Id` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Name` varchar(64) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `RoleId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `PhoneCode` varchar(16) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Password` varchar(128) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `IsDelete` bit(1) NOT NULL,
  `CreateUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateDate` datetime(0) NOT NULL,
  `ModifyUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Users
-- ----------------------------
INSERT INTO `Users` VALUES ('46B5F5DF-C6AC-43A1-86FF-DED6D530A7D1', 'admin', 'DD1AF84F-7570-4D1E-93F6-F67330689367', '13032988121', '123456', b'0', '00000000-0000-0000-0000-000000000000', 'dmin', '2019-08-31 00:00:00', NULL, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
