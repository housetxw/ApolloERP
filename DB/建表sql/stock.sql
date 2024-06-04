/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : stock

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:14:52
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for order_queue
-- ----------------------------
DROP TABLE IF EXISTS `order_queue`;
CREATE TABLE `order_queue`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `queue_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `queue_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '消息类型(接口调用，队列调用)',
  `queue_remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '消息备注',
  `queue_processing` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '处理状态',
  `queue_priority` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '消息优先级',
  `queue_status` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '消息状态(1新建 2未占用 3已占用 4已取消)',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 45 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_stock
-- ----------------------------
DROP TABLE IF EXISTS `order_stock`;
CREATE TABLE `order_stock`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '订单号',
  `order_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单产品单号',
  `stock_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '占用数量',
  `batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '入库批次',
  `occupy_type` tinyint(0) NOT NULL COMMENT '占用类型(1订单，2退货 调拨单)',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `owner_id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `product_batch_id` int(0) NOT NULL DEFAULT 0 COMMENT '商品批次编号',
  `is_valid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效(1有效-还在占用中 2无效-订单已发出)',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `stock_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '库存类型(1良品 2不良品)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_stock_log
-- ----------------------------
DROP TABLE IF EXISTS `order_stock_log`;
CREATE TABLE `order_stock_log`  (
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

-- ----------------------------
-- Table structure for release_stock_log
-- ----------------------------
DROP TABLE IF EXISTS `release_stock_log`;
CREATE TABLE `release_stock_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `order_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单产品号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '批次号',
  `owner_id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `stock_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '良品类型(1良品 2不良品)',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_location
-- ----------------------------
DROP TABLE IF EXISTS `stock_location`;
CREATE TABLE `stock_location`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_id` bigint(0) NOT NULL COMMENT '仓库编号',
  `location_type` int(0) NOT NULL COMMENT '类型(1 仓库 2门店 3在途 4客户)',
  `location` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '库存数量',
  `batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '入库批次',
  `cost_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本单价',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本总价',
  `week_year` char(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '周期',
  `owner_id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `stock_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '良品类型(1良品 2不良品)',
  `stock_unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '库存单位',
  `owner_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主名称',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 106 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_location_summary
-- ----------------------------
DROP TABLE IF EXISTS `stock_location_summary`;
CREATE TABLE `stock_location_summary`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `location_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓储类型(1仓库 2门店 3供应商)',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `total_stock_num` int(0) NOT NULL DEFAULT 0 COMMENT '总库存量',
  `total_available_num` int(0) NOT NULL DEFAULT 0 COMMENT '总库用量',
  `total_occupy_num` int(0) NOT NULL DEFAULT 0 COMMENT '总占用量',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_tranction
-- ----------------------------
DROP TABLE IF EXISTS `stock_tranction`;
CREATE TABLE `stock_tranction`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_object_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型(订单号,采购单号,退货单号)',
  `transfer_from` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移起点',
  `transfer_from_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '转移起点名称',
  `transfer_to` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移终点',
  `transfer_to_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '转移终点名称',
  `from_stock_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移库存起点',
  `to_stock_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移库存终点',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '转移数量',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `transfer_operator` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '转移操作人',
  `transfer_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '转移操作时间',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `remark` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 106 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
