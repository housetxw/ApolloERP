/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : wms

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:15:39
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for allot_product
-- ----------------------------
DROP TABLE IF EXISTS `allot_product`;
CREATE TABLE `allot_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `task_id` bigint(0) NOT NULL COMMENT '铺货单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2已审核 3已取消 4已发出 5已驳回)',
  `batch_id` bigint(0) NOT NULL COMMENT '批次(源批次)',
  `ref_batch_id` bigint(0) NOT NULL COMMENT '外联批次',
  `owner_id` bigint(0) NOT NULL COMMENT '货主',
  `stock_id` bigint(0) NOT NULL COMMENT '库存单号',
  `cost_price` decimal(18, 2) NOT NULL COMMENT '成本',
  `unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `is_deleted` tinyint(0) NOT NULL COMMENT '是否删除',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `reject_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '驳回人',
  `reject_reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '驳回理由',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1209 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for allot_task
-- ----------------------------
DROP TABLE IF EXISTS `allot_task`;
CREATE TABLE `allot_task`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `task_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '任务单号',
  `source_warehouse` bigint(0) NOT NULL COMMENT '源仓库编号',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '源类型',
  `source_warehouse_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '源仓库名称',
  `target_warehouse` bigint(0) NOT NULL COMMENT '目标仓库编号',
  `target_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '目标类型',
  `target_warehouse_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '目标仓库名称',
  `task_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2已审核 3已取消 4已发出 5已驳回)',
  `task_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '任务类型(1铺货 2移库 3调拨)',
  `operator` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '申请人',
  `operator_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '申请时间',
  `is_audit` tinyint(0) NOT NULL COMMENT '审核状态',
  `audit_user` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '审核人',
  `audit_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '审核时间',
  `audit_remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '审核说明',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 193 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for asn
-- ----------------------------
DROP TABLE IF EXISTS `asn`;
CREATE TABLE `asn`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `location` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `asn_no` bigint(0) NOT NULL DEFAULT 0 COMMENT '关联单号',
  `asn_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型(1采购单 2退货单)',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2关闭 3已取消 4部分收货 5已收货 6已签收 7已发货)',
  `sender_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '发货方编号',
  `sender` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发货方名称',
  `send_address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发货地址',
  `earliest_arrival_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最早到达时间',
  `latest_arrival_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最迟到达时间',
  `owner_Id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `channel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '渠道',
  `owner_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型',
  `owner_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主名称',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 210 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for asn_endreceipt_product
-- ----------------------------
DROP TABLE IF EXISTS `asn_endreceipt_product`;
CREATE TABLE `asn_endreceipt_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '备注',
  `asn_id` bigint(0) NOT NULL DEFAULT 0 COMMENT 'asn单号',
  `asn_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT 'asn产品单号',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '原因',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '差异类型(1漏发 2多发 3破损)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for asn_product
-- ----------------------------
DROP TABLE IF EXISTS `asn_product`;
CREATE TABLE `asn_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `asn_id` bigint(0) NOT NULL DEFAULT 0 COMMENT 'asn单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品单号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2关闭 3取消 4部分收货 5已收货)',
  `ref_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联单号(采购子单,退货子单)',
  `unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库单位',
  `tax_rate` int(0) NOT NULL DEFAULT 0 COMMENT '税率',
  `no_tax_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '无税成本',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 965 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for asn_receipt_product
-- ----------------------------
DROP TABLE IF EXISTS `asn_receipt_product`;
CREATE TABLE `asn_receipt_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `asn_id` bigint(0) NOT NULL DEFAULT 0 COMMENT 'asn单号',
  `asn_product_id` bigint(0) NOT NULL COMMENT 'asn产品单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `week_year` int(0) NOT NULL DEFAULT 0 COMMENT '周期',
  `is_defective` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否为不良品',
  `defective_reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '不良品原因',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `receipted_unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收货单位',
  `package_delivery_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '包裹单号',
  `package_weight` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '包裹重量',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 910 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for batch
-- ----------------------------
DROP TABLE IF EXISTS `batch`;
CREATE TABLE `batch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `po_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '采购子单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `in_stock_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '入库时间',
  `week_year` char(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '周期',
  `in_stock_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库操作人',
  `vender_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '供应商编号',
  `vender_short_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商简称',
  `product_batch_id` int(0) NOT NULL DEFAULT 0 COMMENT '商品批次',
  `production_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '产品生产时间',
  `channel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '渠道',
  `stock_unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '库存单位',
  `owner_id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `owner_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型',
  `owner_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主名称',
  `tax_rate` int(0) NOT NULL DEFAULT 0 COMMENT '税率',
  `no_tax_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '无税成本',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 912 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for product_package_relation
-- ----------------------------
DROP TABLE IF EXISTS `product_package_relation`;
CREATE TABLE `product_package_relation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` bigint(0) NOT NULL COMMENT '出库单号',
  `transfer_product_id` bigint(0) NOT NULL COMMENT '出库产品单号',
  `package_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '包裹单号',
  `product_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL COMMENT '数量',
  `receive_num` int(0) NOT NULL COMMENT '已收数量',
  `batch_id` bigint(0) NOT NULL COMMENT '批次号',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建 已发出 已签收 已清点)',
  `is_deleted` tinyint(0) NOT NULL COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1063 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for replenish_product
-- ----------------------------
DROP TABLE IF EXISTS `replenish_product`;
CREATE TABLE `replenish_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `task_id` bigint(0) NOT NULL COMMENT '铺货单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2已审核 3已取消 4已发出 5已驳回)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `reject_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '驳回人',
  `reject_reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '驳回理由',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1149 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for replenish_task
-- ----------------------------
DROP TABLE IF EXISTS `replenish_task`;
CREATE TABLE `replenish_task`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `source_warehouse` bigint(0) NOT NULL COMMENT '源仓库编号',
  `source_warehouse_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '源仓库名称',
  `target_warehouse` bigint(0) NOT NULL COMMENT '目标仓库编号',
  `target_warehouse_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '目标仓库名称',
  `task_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '任务类型(1铺货 2移库 3调拨)',
  `task_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2已审核 3已取消 4已发出 5已驳回)',
  `send_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '发出时间',
  `reject_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '驳回人',
  `reject_reason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '驳回理由',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 217 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_occupy_mapping
-- ----------------------------
DROP TABLE IF EXISTS `shop_occupy_mapping`;
CREATE TABLE `shop_occupy_mapping`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '门店名称',
  `relation_shop_id` bigint(0) NOT NULL COMMENT '关联前置仓编号',
  `relation_shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '关联前置仓名称',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '状态',
  `level` int(0) NOT NULL COMMENT '优先级',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for storage_plan
-- ----------------------------
DROP TABLE IF EXISTS `storage_plan`;
CREATE TABLE `storage_plan`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `plan_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计划编号',
  `plan_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计划名称',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品来源(总部产品，外采产品)',
  `type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '盘点类型(指定产品 全盘)',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '执行状态(新建 盘点中 盘点完成 差异处理中)',
  `plan_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '计划时间',
  `actual_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '实际完成时间',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for storage_plan_product
-- ----------------------------
DROP TABLE IF EXISTS `storage_plan_product`;
CREATE TABLE `storage_plan_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `plan_id` bigint(0) NOT NULL COMMENT '计划编号',
  `source_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源编号(重盘的关联产品单号)',
  `product_id` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `product_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品分类',
  `unit` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `system_num` int(0) NOT NULL DEFAULT 0 COMMENT '系统数量',
  `storage_num` int(0) NOT NULL DEFAULT 0 COMMENT '盘点数量',
  `diff_num` int(0) NOT NULL DEFAULT 0 COMMENT '差异数量',
  `operate_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '盘点人',
  `operate_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '盘点时间',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `deal_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '处理人',
  `deal_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '处理方式',
  `deal_remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '处理说明',
  `deal_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '处理时间',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 150 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for warehouse_config
-- ----------------------------
DROP TABLE IF EXISTS `warehouse_config`;
CREATE TABLE `warehouse_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `warehouse_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库类型(轮胎 保养 车品)',
  `contacter` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓管联系人',
  `mobile` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '电话',
  `telephone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机',
  `province_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省',
  `city_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市',
  `district_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区',
  `province_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省名称',
  `city_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市名称',
  `district_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区名称',
  `address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '详细地址',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(正常 待上线 待检查)',
  `warehouse_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库编码',
  `function_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库功能类型',
  `is_active` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否激活',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '备注',
  `region_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区域名称(华北区 华中 华南 华东)',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `company_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '公司id',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for warehouse_transfer_exception
-- ----------------------------
DROP TABLE IF EXISTS `warehouse_transfer_exception`;
CREATE TABLE `warehouse_transfer_exception`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `transfer_product_id` bigint(0) NOT NULL COMMENT '出库产品单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2已取消 3已审核 4已驳回)',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `plan_send_num` int(0) NOT NULL DEFAULT 9 COMMENT '计划发出数量',
  `actual_receive_num` int(0) NOT NULL DEFAULT 9 COMMENT '时间收到数量',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for warehouse_transfer_package
-- ----------------------------
DROP TABLE IF EXISTS `warehouse_transfer_package`;
CREATE TABLE `warehouse_transfer_package`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库类型(1订单 2移库)',
  `delivery_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配送类型(快递 自配)',
  `delivery_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '快递单号',
  `delivery_company` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '快递公司',
  `delivery_fee` decimal(18, 2) NOT NULL COMMENT '快递费用',
  `delivery_weight` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '快递重量',
  `delivery_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '快递电话',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建 已发出 已签收)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 178 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for warehouse_transfer_product
-- ----------------------------
DROP TABLE IF EXISTS `warehouse_transfer_product`;
CREATE TABLE `warehouse_transfer_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移单号',
  `transfer_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '转移子单号(补货子单号 订单产品单号)',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `stock_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存单号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `receive_num` int(0) NOT NULL DEFAULT 0 COMMENT '已收货数量',
  `transfer_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '移库费用',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '成本',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '单价',
  `week_year` char(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '周期',
  `vender_short_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商简称',
  `vender_id` bigint(0) NOT NULL COMMENT '供应商编号',
  `batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '入库批次',
  `owner_id` int(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `owner_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主名称',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1249 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for warehouse_transfer_task
-- ----------------------------
DROP TABLE IF EXISTS `warehouse_transfer_task`;
CREATE TABLE `warehouse_transfer_task`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `source_warehouse` bigint(0) NOT NULL DEFAULT 0 COMMENT '源仓库编号',
  `source_warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '源仓库名称',
  `target_warehouse` bigint(0) NOT NULL DEFAULT 0 COMMENT '目标仓编号',
  `target_warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '目标仓名称',
  `transfer_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '转移单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '转移类型',
  `task_status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '任务状态(1等待发出 2任务接收 3已发出 4已送达 5部分收货 6已收货 7已取消 8揽件失败)',
  `delivery_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配送类型(1快递 2自配)',
  `expect_send_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '期望发货时间',
  `expect_arrive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '期望送达时间',
  `accept_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货物配送人',
  `accept_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '货物配送接收时间',
  `check_state` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对账状态',
  `delivery_line_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '干线编号',
  `delivery_line` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '干线名称',
  `send_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '发出时间',
  `arrive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '到达时间',
  `stock_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '货物类型(1良品 2不良品)',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 284 CHARACTER SET = ascii COLLATE = ascii_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wms_log
-- ----------------------------
DROP TABLE IF EXISTS `wms_log`;
CREATE TABLE `wms_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `object_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '关联单号',
  `object_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型',
  `log_level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '日志级别(1Info  2Warning 3Error 4Critical)',
  `before_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改前值',
  `after_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改后值',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
