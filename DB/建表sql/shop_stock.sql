/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : shop_stock

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:14:14
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for occupy_item
-- ----------------------------
DROP TABLE IF EXISTS `occupy_item`;
CREATE TABLE `occupy_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `occupy_queue_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '占用queueId',
  `source_inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源单据Id',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `order_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单产品单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存id',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库id',
  `num` bigint(0) NOT NULL DEFAULT 0 COMMENT '占库数量',
  `batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '批次号',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联批次单号',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `occupy_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '占用类型(订单，退货的应该分开)',
  `own_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `stock_type` tinyint(0) NOT NULL COMMENT '库存类型(1良品 2不良品)',
  `is_valid` tinyint(0) NOT NULL COMMENT '是否有效(1有效-还在占用中 2无效-订单已发出/安装  3.占用释放)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 325782 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '占库明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for occupy_queue
-- ----------------------------
DROP TABLE IF EXISTS `occupy_queue`;
CREATE TABLE `occupy_queue`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `queue_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类型(Order,OrderMQ)',
  `source_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联的单号',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_processing` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '是否是处理中',
  `status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '占用状态(1新建,2未占用，3已占用，4已取消)',
  `pripority` smallint(0) NOT NULL DEFAULT 0 COMMENT '占库优先级',
  `rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '占库规则id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3799 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '占库通知' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for occupy_rule
-- ----------------------------
DROP TABLE IF EXISTS `occupy_rule`;
CREATE TABLE `occupy_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `is_deleted` tinyint(0) NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '占库规则' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for occupy_stock_log
-- ----------------------------
DROP TABLE IF EXISTS `occupy_stock_log`;
CREATE TABLE `occupy_stock_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `object_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `object_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '关联子单号',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类型',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `log_level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '日志级别(1Info  2Warning 3Error 4Critical)',
  `before_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改前值',
  `after_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改后值',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`, `log_level`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
