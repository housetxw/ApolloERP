/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : hangfire

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:09:40
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for hangfire_AggregatedCounter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_AggregatedCounter`;
CREATE TABLE `hangfire_AggregatedCounter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` int(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_CounterAggregated_Key`(`Key`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_Counter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_Counter`;
CREATE TABLE `hangfire_Counter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` int(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Counter_Key`(`Key`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_DistributedLock
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_DistributedLock`;
CREATE TABLE `hangfire_DistributedLock`  (
  `Resource` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(0) NOT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_Hash
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_Hash`;
CREATE TABLE `hangfire_Hash`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Field` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Hash_Key_Field`(`Key`, `Field`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_Job
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_Job`;
CREATE TABLE `hangfire_Job`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `StateId` int(0) NULL DEFAULT NULL,
  `StateName` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `InvocationData` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Arguments` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Job_StateName`(`StateName`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_JobParameter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_JobParameter`;
CREATE TABLE `hangfire_JobParameter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_JobParameter_JobId_Name`(`JobId`, `Name`) USING BTREE,
  INDEX `FK_JobParameter_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_JobQueue
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_JobQueue`;
CREATE TABLE `hangfire_JobQueue`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Queue` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FetchedAt` datetime(0) NULL DEFAULT NULL,
  `FetchToken` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_JobQueue_QueueAndFetchedAt`(`Queue`, `FetchedAt`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_JobState
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_JobState`;
CREATE TABLE `hangfire_JobState`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_JobState_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_List
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_List`;
CREATE TABLE `hangfire_List`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_Server
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_Server`;
CREATE TABLE `hangfire_Server`  (
  `Id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastHeartbeat` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_Set
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_Set`;
CREATE TABLE `hangfire_Set`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Score` float NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Set_Key_Value`(`Key`, `Value`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_State
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_State`;
CREATE TABLE `hangfire_State`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_HangFire_State_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_aggregatedcounter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_aggregatedcounter`;
CREATE TABLE `hangfire_aggregatedcounter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` int(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_CounterAggregated_Key`(`Key`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_counter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_counter`;
CREATE TABLE `hangfire_counter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` int(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Counter_Key`(`Key`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_distributedlock
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_distributedlock`;
CREATE TABLE `hangfire_distributedlock`  (
  `Resource` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(0) NOT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_hash
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_hash`;
CREATE TABLE `hangfire_hash`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Field` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Hash_Key_Field`(`Key`, `Field`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_job
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_job`;
CREATE TABLE `hangfire_job`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `StateId` int(0) NULL DEFAULT NULL,
  `StateName` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `InvocationData` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Arguments` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Job_StateName`(`StateName`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_jobparameter
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_jobparameter`;
CREATE TABLE `hangfire_jobparameter`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_JobParameter_JobId_Name`(`JobId`, `Name`) USING BTREE,
  INDEX `FK_JobParameter_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_jobqueue
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_jobqueue`;
CREATE TABLE `hangfire_jobqueue`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Queue` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FetchedAt` datetime(0) NULL DEFAULT NULL,
  `FetchToken` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_JobQueue_QueueAndFetchedAt`(`Queue`, `FetchedAt`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_jobstate
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_jobstate`;
CREATE TABLE `hangfire_jobstate`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_JobState_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_list
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_list`;
CREATE TABLE `hangfire_list`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_server
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_server`;
CREATE TABLE `hangfire_server`  (
  `Id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastHeartbeat` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_set
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_set`;
CREATE TABLE `hangfire_set`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Score` float NOT NULL,
  `ExpireAt` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_Set_Key_Value`(`Key`, `Value`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hangfire_state
-- ----------------------------
DROP TABLE IF EXISTS `hangfire_state`;
CREATE TABLE `hangfire_state`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT,
  `JobId` int(0) NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_HangFire_State_Job`(`JobId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
