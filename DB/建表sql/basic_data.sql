/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : basic_data

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:08:32
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for app_version
-- ----------------------------
DROP TABLE IF EXISTS `app_version`;
CREATE TABLE `app_version`  (
  `code` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '版本号',
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '版本号名称',
  `info` varchar(3000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '版本信息',
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '版本URL',
  `config` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '版本配置',
  `time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '版本时间',
  `is_force` tinyint(1) NOT NULL DEFAULT 0 COMMENT '标识此版本，是否让灰度门店升级',
  `is_release` tinyint(1) NOT NULL DEFAULT 0 COMMENT '标识此版本，正式版是否发布',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 91 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for app_version_gray
-- ----------------------------
DROP TABLE IF EXISTS `app_version_gray`;
CREATE TABLE `app_version_gray`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `version_code` bigint(0) NOT NULL COMMENT '版本号',
  `shop_id` bigint(0) NOT NULL COMMENT '门店Id',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for log_operation
-- ----------------------------
DROP TABLE IF EXISTS `log_operation`;
CREATE TABLE `log_operation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `log_id` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志记录的业务主键id',
  `log_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'C：新增；R：查询；U：更新；D：删除',
  `biz_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `req_param` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '请求参数',
  `operated_before_content` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '操作前内容',
  `operated_after_content` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '操作后内容',
  `operator` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '操作人',
  `comment` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for region_china
-- ----------------------------
DROP TABLE IF EXISTS `region_china`;
CREATE TABLE `region_china`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '省市区表主键',
  `region_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'RG内部使用唯一标识； 一旦初始化，便不会更改；RegionId只有一位时(省、自治区、直辖市)，是 \'0\'；其他级别的行政单位RegionId是6位；',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '省市区名称全称',
  `parent_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'RG内部使用唯一标识，省、自治区、直辖市 此值为‘0’；',
  `country_code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '国家行政级编码；仅供参考，请不要用于任何系统的任何业务逻辑； (可能会有变化)',
  `initial` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '首字母 (大写)',
  `spell` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '省市区全称拼音 (全部小写，拼音间用空格隔离)',
  `level` tinyint(0) NOT NULL COMMENT '地区级别: 1-省、自治区、直辖市； 2-地级市、地区、自治州、盟；3-市辖区、县级市、县；',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_region_id`(`region_id`) USING BTREE,
  INDEX `idx_parent_id`(`parent_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3458 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
