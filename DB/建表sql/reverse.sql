/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : reverse

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:12:45
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for refund_order
-- ----------------------------
DROP TABLE IF EXISTS `refund_order`;
CREATE TABLE `refund_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reverse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '逆向申请ID',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '退款单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for return_order
-- ----------------------------
DROP TABLE IF EXISTS `return_order`;
CREATE TABLE `return_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reverse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '逆向申请ID',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '退货单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reverse_credential
-- ----------------------------
DROP TABLE IF EXISTS `reverse_credential`;
CREATE TABLE `reverse_credential`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reverse_id` bigint(0) NOT NULL COMMENT '逆向申请ID',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图片地址',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '逆向申请凭证' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reverse_order
-- ----------------------------
DROP TABLE IF EXISTS `reverse_order`;
CREATE TABLE `reverse_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reverse_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '逆向申请单号（RGRC/RGRT/RGRH*****）',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '原订单号',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户ID',
  `apply_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '申请类型（0未设置 1取消 2退款 3换货）',
  `apply_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '发起申请时间',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '逆向申请单状态（10已提交 20已确认 30已完成 40已取消）',
  `reason_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '原因ID',
  `reason` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '原因',
  `instruction` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '说明（选填）',
  `is_need_refund` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要退款（0否 1是）',
  `refund_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '退款金额',
  `refund_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '退款状态（0未退款 1已退款）',
  `refund_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '退款完成时间',
  `is_need_return` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要退货（0否 1是）',
  `return_status` tinyint(0) NOT NULL COMMENT '退货状态（0未退货 1已退货）',
  `return_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '退货完成时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 305 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '逆向申请单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reverse_product
-- ----------------------------
DROP TABLE IF EXISTS `reverse_product`;
CREATE TABLE `reverse_product`  (
  `id` bigint(0) NOT NULL COMMENT '主键',
  `reverse_order_id` bigint(0) NULL DEFAULT NULL COMMENT '逆向申请单ID',
  `reverse_order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '逆向申请单号',
  `order_product_id` bigint(0) NULL DEFAULT NULL COMMENT '原订单商品ID',
  `product_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '商品编号',
  `number` int(0) NULL DEFAULT NULL COMMENT '数量',
  `stock_type` tinyint(0) NULL DEFAULT NULL COMMENT '商品出入库类型（0未设置 1需退回入库的商品 2需换货出库的商品 3无需退换的商品）',
  `change_by_id` bigint(0) NULL DEFAULT NULL COMMENT '被哪个商品ID替换',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '逆向申请商品' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reverse_reason_config
-- ----------------------------
DROP TABLE IF EXISTS `reverse_reason_config`;
CREATE TABLE `reverse_reason_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `apply_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '申请类型（0未设置 1取消 2退款 3换货）',
  `reason` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
