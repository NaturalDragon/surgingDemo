/*
 Navicat Premium Data Transfer

 Source Server         : mycentos
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : 112.74.59.197:3306
 Source Schema         : OrderInfo

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 02/09/2019 16:34:31
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for OrderDetail
-- ----------------------------
DROP TABLE IF EXISTS `OrderDetail`;
CREATE TABLE `OrderDetail`  (
  `Id` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `OrderId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '订单id',
  `GoodsId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '商品id',
  `Price` decimal(10, 0) NOT NULL COMMENT '单价',
  `Count` int(255) NOT NULL COMMENT '数量',
  `Money` decimal(10, 0) NOT NULL COMMENT '合计',
  `IsDelete` bit(1) NOT NULL,
  `CreateDate` datetime(0) NOT NULL,
  `CreateUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `ModifyUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for OrderInfo
-- ----------------------------
DROP TABLE IF EXISTS `OrderInfo`;
CREATE TABLE `OrderInfo`  (
  `Id` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `OrderNumber` varchar(128) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '订单号',
  `TotalMoney` decimal(10, 0) NOT NULL COMMENT '总金额',
  `UserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '下单用户',
  `ExpireTime` datetime(0) NOT NULL COMMENT '过期时间',
  `Status` int(255) NOT NULL COMMENT '订单状态',
  `IsDelete` bit(1) NOT NULL,
  `CreateDate` datetime(0) NOT NULL,
  `CreateUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `CreateUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `ModifyUserId` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyUserName` varchar(32) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ModifyDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
