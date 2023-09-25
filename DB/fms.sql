/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : fms

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:09:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account_check
-- ----------------------------
DROP TABLE IF EXISTS `account_check`;
CREATE TABLE `account_check`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `account_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '对账单类型(风险单)',
  `install_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '安装时间',
  `pay_method` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '付款方式',
  `money_arrive_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '到账状态',
  `order_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单类型',
  `sale_total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售总价',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `service_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务费',
  `commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '手续费(前期是固定的)',
  `other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '其他费用',
  `settlement_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '结算价',
  `shop_check_result` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店对账状态（已核对 、核对异常）',
  `shop_check_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '门店核对时间',
  `shop_check_suggestion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '门店核对意见',
  `rg_check_result` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '总部对账状态（已核对）',
  `rg_check_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '总部核对时间',
  `rg_check_suggestion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '总部核对意见',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '对账状态(备用)',
  `withdraw_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '提现状态(未申请、已申请、申请成功)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '备注',
  `channel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单渠道',
  `telephone` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单用户手机号',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `company_id` bigint(0) NOT NULL COMMENT '公司id',
  `company_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司全称',
  `shop_install_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务安装费',
  `actual_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实付金额',
  `shop_distribution_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货成本（1. sum(PID.门店结算价*数量)\n',
  `shop_distribution_gross_profit` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货毛利（1. 订单实付金额 - 安装费 - 铺货成本 - 门店项目金额）',
  `shop_difference_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '差价（1. if 铺货毛利>=0 then 补差价=0，if 铺货毛利<0 then 补差价=-1*铺货毛利\n）',
  `shop_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005\n）',
  `shop_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续费\n)',
  `company_back_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量)\n）',
  `shop_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店其他费用',
  `shop_item_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店项目费',
  `shopOutProductCost` decimal(18, 2) NOT NULL COMMENT '门店外采成本',
  `company_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司扣手续费\n(1. （公司返现金额 - 公司扣其他）* 0.005\n)',
  `company_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司应结算金额\n(1. 公司返现金额 - 公司扣其他 - 公司扣手续费\n)',
  `company_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司其他费用',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `product_detail` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '产品明细',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22789 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for account_check_exception
-- ----------------------------
DROP TABLE IF EXISTS `account_check_exception`;
CREATE TABLE `account_check_exception`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `relation_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号(订单号,采购单号)',
  `relation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型(门店/总部)',
  `report_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '上报类型',
  `report_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '上报人',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `report_reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '上报原因',
  `report_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '上报时间',
  `status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建 已处理)',
  `suggestion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '处理意见',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `install_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '安装时间',
  `pay_method` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '付款方式',
  `money_arrive_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '到账状态',
  `order_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单类型',
  `settlement_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '结算价',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 85 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for account_check_log
-- ----------------------------
DROP TABLE IF EXISTS `account_check_log`;
CREATE TABLE `account_check_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `relation_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `relation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '关联类型',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `before_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作前值',
  `after_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作后值',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '备注',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '状态',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fms_dictionary
-- ----------------------------
DROP TABLE IF EXISTS `fms_dictionary`;
CREATE TABLE `fms_dictionary`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '类型',
  `dic_key` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT 'key',
  `dic_value` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT 'value',
  `is_delete` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_account_check
-- ----------------------------
DROP TABLE IF EXISTS `purchase_account_check`;
CREATE TABLE `purchase_account_check`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `settlement_staff` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算人员',
  `settlement_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算方式( 现金  、月结)',
  `purchase_no` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '采购单号',
  `purchase_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '采购产品Id',
  `purchase_date` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '采购日期',
  `product_category` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品类目',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品编码',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `specification` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '型号规格',
  `oe_number` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '原厂编号',
  `unit` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '单位',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '采购数量 ',
  `purchase_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购成本价格',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总成本',
  `settlement_amount` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '结算的金额',
  `shop_commission_fee` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算的费用',
  `shop_other_fee` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '其他费用',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '状态（已核对 、核对异常、未核对）',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4077 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_account_check_exception
-- ----------------------------
DROP TABLE IF EXISTS `purchase_account_check_exception`;
CREATE TABLE `purchase_account_check_exception`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `purchase_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '对账单Id',
  `relation_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '关联单号(订单号,采购单号)',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品编码',
  `report_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '上报人',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `report_reason` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '上报原因',
  `report_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '上报时间',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_settlement_batch
-- ----------------------------
DROP TABLE IF EXISTS `purchase_settlement_batch`;
CREATE TABLE `purchase_settlement_batch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算批次no',
  `settlement_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算方式(现金  、月结)',
  `settlement_year` int(0) NOT NULL DEFAULT 0 COMMENT '结算年份',
  `settlement_month` int(0) NOT NULL DEFAULT 0 COMMENT '结算月份',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态(0:未付款 1:付款失败 2：已付款)',
  `settlement_staff` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算人员',
  `apply_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '申请人',
  `apply_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '申请时间',
  `bill_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '本期对账金额',
  `bank_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行的名称',
  `bank_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行卡卡号',
  `top_remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '最新备注',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_settlement_detail
-- ----------------------------
DROP TABLE IF EXISTS `purchase_settlement_detail`;
CREATE TABLE `purchase_settlement_detail`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `settlement_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '结算批次Id',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '结算批次no',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '门店名称',
  `settlement_staff` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算人员',
  `settlement_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算方式( 现金  、月结)',
  `purchase_no` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '采购单号',
  `purchase_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '采购产品Id',
  `purchase_date` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '采购日期',
  `product_category` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品类目',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品编码',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `specification` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '型号规格',
  `oe_number` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '原厂编号',
  `unit` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '单位',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '采购数量 ',
  `purchase_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购成本价格',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总成本',
  `settlement_amount` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '结算的金额',
  `shop_commission_fee` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算的费用',
  `shop_other_fee` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '其他费用',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 35 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_settlement_pay_review
-- ----------------------------
DROP TABLE IF EXISTS `purchase_settlement_pay_review`;
CREATE TABLE `purchase_settlement_pay_review`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '门店名称',
  `location_id` bigint(0) NULL DEFAULT 0 COMMENT '门店ID',
  `settlement_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '结算批次id',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算批次no',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态(0:未付款 1：已付款 2：付款失败)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '结果',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reconciliation_fee
-- ----------------------------
DROP TABLE IF EXISTS `reconciliation_fee`;
CREATE TABLE `reconciliation_fee`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道',
  `order_type` tinyint(0) NOT NULL COMMENT '订单类型',
  `produce_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '产生类型',
  `account_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '对账单类型',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `shop_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `company_id` bigint(0) NOT NULL COMMENT '公司id',
  `company_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '公司全称',
  `sale_total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '订单总价',
  `shop_install_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务安装费',
  `actual_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实付金额',
  `shop_distribution_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货成本（1. sum(PID.门店结算价*数量)\n',
  `shop_distribution_gross_profit` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货毛利（1. 订单实付金额 - 安装费 - 铺货成本 - 门店项目金额）',
  `shop_difference_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '差价（1. if 铺货毛利>=0 then 补差价=0，if 铺货毛利<0 then 补差价=-1*铺货毛利\n）',
  `shop_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005\n）',
  `shop_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续费\n)',
  `company_back_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量)\n）',
  `shop_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店其他费用',
  `shop_item_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店项目费',
  `shop_item_cost` decimal(18, 2) NOT NULL COMMENT '门店外采成本',
  `company_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司扣手续费\n(1. （公司返现金额 - 公司扣其他）* 0.005\n)',
  `company_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司应结算金额\n(1. 公司返现金额 - 公司扣其他 - 公司扣手续费\n)',
  `company_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司其他费用',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `product_detail` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '产品明细',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19073 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for settlement_batch
-- ----------------------------
DROP TABLE IF EXISTS `settlement_batch`;
CREATE TABLE `settlement_batch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算批次no',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态(0:未付款 1:付款失败 2：已付款)',
  `check_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '对账人',
  `apply_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '申请人',
  `apply_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '申请时间',
  `bill_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '本期对账金额',
  `bank_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行的名称',
  `bank_branch` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行的支行',
  `bank_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行账户名',
  `bank_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '银行卡卡号',
  `top_remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '最新备注',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `province_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '省',
  `city_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '市',
  `district_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '区',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '省名称',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '市名称',
  `address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '详细地址',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '区县名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 45 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for settlement_batch_detail
-- ----------------------------
DROP TABLE IF EXISTS `settlement_batch_detail`;
CREATE TABLE `settlement_batch_detail`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `settlement_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '结算批次Id',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算批次no',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `account_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '对账单类型(风险单)',
  `install_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '安装时间',
  `order_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单类型',
  `sale_total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售总价',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `service_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务费',
  `commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '手续费(前期是固定的)',
  `other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '其他费用',
  `settlement_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '结算价',
  `pay_method` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '付款方式',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `shop_install_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务安装费',
  `actual_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实付金额',
  `shop_distribution_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货成本（1. sum(PID.门店结算价*数量)\n',
  `shop_distribution_gross_profit` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '铺货毛利（1. 订单实付金额 - 安装费 - 铺货成本 - 门店项目金额）',
  `shop_difference_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '差价（1. if 铺货毛利>=0 then 补差价=0，if 铺货毛利<0 then 补差价=-1*铺货毛利\n）',
  `shop_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '手续费（1. （安装费+门店项目金额+铺货毛利+补差价-扣其他）* 0.005\n）',
  `shop_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算金额(1. 安装费+门店项目金额+铺货毛利+补差价 - 扣其他 - 扣手续费\n)',
  `company_back_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司返现金额（1. 订单中所有铺货实物产品的sum(PID.返现价*数量)\n）',
  `shop_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店其他费用',
  `shop_item_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店项目费',
  `company_commission_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司扣手续费\n(1. （公司返现金额 - 公司扣其他）* 0.005\n)',
  `company_settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司应结算金额\n(1. 公司返现金额 - 公司扣其他 - 公司扣手续费\n)',
  `company_other_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '公司其他费用',
  `company_id` bigint(0) NOT NULL COMMENT '公司id',
  `company_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司全称',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1685 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for settlement_pay_review
-- ----------------------------
DROP TABLE IF EXISTS `settlement_pay_review`;
CREATE TABLE `settlement_pay_review`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '门店名称',
  `shop_id` bigint(0) NULL DEFAULT 0 COMMENT '门店ID',
  `settlement_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '结算批次id',
  `settlement_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '结算批次no',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态(0:未付款 1：已付款 2：付款失败)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '结果',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
