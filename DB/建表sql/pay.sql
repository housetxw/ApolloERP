/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : pay

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:10:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for merge_order
-- ----------------------------
DROP TABLE IF EXISTS `merge_order`;
CREATE TABLE `merge_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `merge_no` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '合并支付单号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 584 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for merge_pay
-- ----------------------------
DROP TABLE IF EXISTS `merge_pay`;
CREATE TABLE `merge_pay`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pay_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '支付单主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '被合并支付的内部单号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3817 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '合并支付表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pay
-- ----------------------------
DROP TABLE IF EXISTS `pay`;
CREATE TABLE `pay`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pay_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '支付单号',
  `inner_no_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '内部单号类型（0未设置 1订单号 2预约编号 3预约编号）',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '内部单号（根据内部单号类型决定传值）',
  `prepay_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '预支付交易会话标识',
  `trade_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '外部交易流水单号（微信或支付宝返回的）',
  `merchant_order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '外部商户单号（微信或支付宝返回的）',
  `platform_trade_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '外部平台交易流水单号（环迅返回的）',
  `method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付方式（0未设置 1在线支付 2到店付款）',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '实际支付渠道（0未设置 1微信支付 2支付宝 3银联支付）',
  `platform_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付平台类型（0未设置 1直连 2环迅）',
  `terminal_type` tinyint(0) NOT NULL COMMENT '支付终端类型（0未设置 1小程序 2管家App）',
  `scene_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付场景类型（0未设置 1主扫收款码支付 2H5支付 3App支付 4小程序和公众号支付 5被扫付款码支付',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付状态（0新建 1已调用等待回调中 2成功 3失败 4关闭 5取消）',
  `pay_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '支付时间',
  `app_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '应用ID（或小程序ID）',
  `seller_account` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '卖家收款账号（商户号或支付宝账号）',
  `buyer_account` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '买家付款账号（openid或支付宝账号）',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '交易金额（单位为元）',
  `currency` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'CNY' COMMENT '币种（默认CNY）',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '备注',
  `pay_info` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '支付唤起数据',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10394 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '支付表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pay_detail
-- ----------------------------
DROP TABLE IF EXISTS `pay_detail`;
CREATE TABLE `pay_detail`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pay_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '支付单ID',
  `service_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '调用服务名称（AlipayTradePay/WxpayUnifiedOrder/AlipayTradePayNotify等）',
  `api_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '调用接口名称（alipay.trade.pay/unifiedorder/alipaytradepaynotify等）',
  `req_ip` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '发起请求端IP',
  `req_url` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '请求地址',
  `notify_url` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '通知地址',
  `req_param` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '请求参数',
  `res_param` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '返回参数',
  `is_system_success` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否系统调用成功（0否 1是）',
  `system_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '系统级返回码',
  `system_msg` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '系统级返回消息',
  `is_business_success` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否业务处理成功（0否 1是）',
  `business_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '业务级返回码',
  `business_msg` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '业务级返回消息',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `支付单号`(`pay_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11933 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '支付明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pay_log
-- ----------------------------
DROP TABLE IF EXISTS `pay_log`;
CREATE TABLE `pay_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pay_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '支付单号',
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作标题',
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '支付日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pay_method
-- ----------------------------
DROP TABLE IF EXISTS `pay_method`;
CREATE TABLE `pay_method`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `icon_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '支付图标URl',
  `pay_methodtext` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '支付方式描述',
  `pay_methodcode` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付方式',
  `pay_scenetype` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付场景',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for refund
-- ----------------------------
DROP TABLE IF EXISTS `refund`;
CREATE TABLE `refund`  (
  `id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `order_pid` bigint(0) NOT NULL DEFAULT 0 COMMENT '单退订单商品Id',
  `refund_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '退款金额',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for refund_detail
-- ----------------------------
DROP TABLE IF EXISTS `refund_detail`;
CREATE TABLE `refund_detail`  (
  `id` bigint(0) NOT NULL,
  `refund_id` bigint(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for refund_log
-- ----------------------------
DROP TABLE IF EXISTS `refund_log`;
CREATE TABLE `refund_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `refund_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '支付单号',
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作标题',
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '支付日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wxpay_bank
-- ----------------------------
DROP TABLE IF EXISTS `wxpay_bank`;
CREATE TABLE `wxpay_bank`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行编码',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行名称',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '微信支付银行表' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
