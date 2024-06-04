/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : order_config

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:10:24
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for franchises_config
-- ----------------------------
DROP TABLE IF EXISTS `franchises_config`;
CREATE TABLE `franchises_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '押金所属分类（0 未设置 1：公司  2：门店）',
  `category` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '加盟费',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `pid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'chan',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for insurance_company
-- ----------------------------
DROP TABLE IF EXISTS `insurance_company`;
CREATE TABLE `insurance_company`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `company_id` bigint(0) NOT NULL DEFAULT 0,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '保险公司',
  `img` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for package_card_rule
-- ----------------------------
DROP TABLE IF EXISTS `package_card_rule`;
CREATE TABLE `package_card_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规则名称',
  `is_valid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效（0无效 1有效）',
  `use_rule_desc` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '使用规则描述',
  `valid_days` int(0) NOT NULL DEFAULT 0 COMMENT '有效时长天数',
  `earliest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最早使用日期',
  `latest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最晚使用日期',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for verification_rule
-- ----------------------------
DROP TABLE IF EXISTS `verification_rule`;
CREATE TABLE `verification_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规则名称',
  `is_valid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效（0无效 1有效）',
  `is_by_pid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按产品Id',
  `is_by_shop_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按门店类型',
  `shop_type` int(0) NOT NULL COMMENT '适用门店类型（位或运算结果）',
  `is_by_shop_id` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按门店Id',
  `use_rule_desc` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '使用规则描述',
  `valid_start_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '有效期开始类型（0未设置 1领取当天生效 2指定开始日期）',
  `valid_end_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '有效期结束类型（0未设置 1持续指定天数 2指定截止日期）',
  `valid_days` int(0) NOT NULL DEFAULT 0 COMMENT '有效时长天数',
  `earliest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最早使用日期',
  `latest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最晚使用日期',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 44 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券规则表（使用规则）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for verification_rule_pid
-- ----------------------------
DROP TABLE IF EXISTS `verification_rule_pid`;
CREATE TABLE `verification_rule_pid`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `pid` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 133 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券规则-产品Id' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for verification_rule_shop_id
-- ----------------------------
DROP TABLE IF EXISTS `verification_rule_shop_id`;
CREATE TABLE `verification_rule_shop_id`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 146 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券规则-门店Id' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
