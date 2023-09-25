/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : shop_wms

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:14:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for allot_product
-- ----------------------------
DROP TABLE IF EXISTS `allot_product`;
CREATE TABLE `allot_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `task_id` bigint(0) NOT NULL COMMENT '调拨单号',
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
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for allot_task
-- ----------------------------
DROP TABLE IF EXISTS `allot_task`;
CREATE TABLE `allot_task`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `task_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '任务单号',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
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
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for full_flow_log
-- ----------------------------
DROP TABLE IF EXISTS `full_flow_log`;
CREATE TABLE `full_flow_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `source_inventory_id` bigint(0) NULL DEFAULT 0 COMMENT '来源单id',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '来源单号',
  `flow_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '流水类型',
  `flow_type_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '流水类型文本',
  `flow_step` bigint(0) NULL DEFAULT 0 COMMENT '流水步骤',
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `is_deleted` tinyint(0) NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '库存全局流水' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for home_care_order
-- ----------------------------
DROP TABLE IF EXISTS `home_care_order`;
CREATE TABLE `home_care_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `home_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '上门领取产品编号',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建/已使用/作废)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for home_care_product
-- ----------------------------
DROP TABLE IF EXISTS `home_care_product`;
CREATE TABLE `home_care_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `record_id` bigint(0) NOT NULL COMMENT '记录单号',
  `product_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产名称',
  `category_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品类型',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '领取数量',
  `install_num` int(0) NOT NULL DEFAULT 0 COMMENT '安装数量',
  `receive_num` int(0) NOT NULL DEFAULT 0 COMMENT '应收数量',
  `actual_num` int(0) NOT NULL DEFAULT 0 COMMENT '实收数量',
  `exception_num` int(0) NOT NULL DEFAULT 0 COMMENT '货损数量',
  `less_num` int(0) NOT NULL DEFAULT 0 COMMENT '缺少数量',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '上门养车领取产品' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for home_care_record
-- ----------------------------
DROP TABLE IF EXISTS `home_care_record`;
CREATE TABLE `home_care_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `tech_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师编号',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师名称',
  `receive_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取人',
  `receive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '领取时间',
  `category_num` int(0) NOT NULL DEFAULT 0 COMMENT '品类数',
  `sum_product_num` int(0) NOT NULL DEFAULT 0 COMMENT '单品数',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建/部分退货/已退货)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for home_return_product
-- ----------------------------
DROP TABLE IF EXISTS `home_return_product`;
CREATE TABLE `home_return_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `record_id` bigint(0) NOT NULL COMMENT '记录单号',
  `product_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产名称',
  `category_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品类型',
  `actual_num` int(0) NOT NULL DEFAULT 0 COMMENT '实收数量',
  `exception_num` int(0) NOT NULL DEFAULT 0 COMMENT '货损数量',
  `less_num` int(0) NOT NULL DEFAULT 0 COMMENT '缺少数量',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for home_return_record
-- ----------------------------
DROP TABLE IF EXISTS `home_return_record`;
CREATE TABLE `home_return_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `tech_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师编号',
  `receive_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取人',
  `receive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '领取时间',
  `category_num` int(0) NOT NULL DEFAULT 0 COMMENT '品类数',
  `sum_product_num` int(0) NOT NULL DEFAULT 0 COMMENT '单品数',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory
-- ----------------------------
DROP TABLE IF EXISTS `inventory`;
CREATE TABLE `inventory`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库Id',
  `location_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源类型',
  `brand_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的Code',
  `brand_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的名称',
  `business_category` bigint(0) NOT NULL DEFAULT 0 COMMENT '业务分类',
  `business_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '业务分类编码',
  `business_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '业务分类名称',
  `first_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类Code',
  `first_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类名称',
  `second_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类Code',
  `second_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类名称',
  `third_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类Code',
  `third_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类名称',
  `four_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类Code',
  `four_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类名称',
  `five_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类code',
  `five_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类名称',
  `total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存总数量',
  `total_cost` decimal(18, 2) NOT NULL COMMENT '总库存金额(每个批次的金额累加)',
  `can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '可用库存',
  `occupy_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '占用库存',
  `no_sale_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '不可销售库存（损坏的库存）',
  `lock_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '锁定的库存（活动/促销）',
  `invented_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '虚拟库存（仓库中没有实物库存，实物库存来源于供应商）',
  `allocation_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '调配库存',
  `allocationing_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '调配中库存',
  `safe_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '安全库存设置',
  `enough_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '充足库存设置的数量',
  `tight_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '紧张库存数量设置',
  `uom_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计量单位',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计量单位文本',
  `status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '库存状态（状态：好、损毁、有缺陷)',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '描述信息',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4702 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '库存项目' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_batch
-- ----------------------------
DROP TABLE IF EXISTS `inventory_batch`;
CREATE TABLE `inventory_batch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `shop_Id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `source_inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源单据Id',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品的pid',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存id',
  `batch_no` bigint(0) NOT NULL DEFAULT 0 COMMENT '批次编号',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联批次号',
  `price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '单价',
  `amount` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '金额',
  `freight` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '运费',
  `cost` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '成本',
  `total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存总数量',
  `can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '可用库存',
  `week_year` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '周期',
  `own_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '货主id',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `supplier_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商Id',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `product_attr_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库产品的属性（良品，不良品）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6649 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '库存批次表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_batch_flow
-- ----------------------------
DROP TABLE IF EXISTS `inventory_batch_flow`;
CREATE TABLE `inventory_batch_flow`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `source_inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源单据Id',
  `source_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源总单号',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `operation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型(入库,出库)',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源类型(采购入库 订单出库 盘盈入库 盘亏出库)',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品的pid',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存id',
  `batch_no` bigint(0) NOT NULL DEFAULT 0 COMMENT '批次编号',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联批次号',
  `price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '单价',
  `amount` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '金额',
  `freight` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '运费',
  `cost` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '成本',
  `before_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前库存总量',
  `after_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后库存总量',
  `current_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改当前库存数量',
  `before_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前可用库存',
  `after_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后可用库存',
  `current_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '当前可用库存',
  `week_year` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '周期',
  `own_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '货主id',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `supplier_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商Id',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `product_attr_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库产品的属性（良品，不良品）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14184 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '库存批次流水' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_exception_file
-- ----------------------------
DROP TABLE IF EXISTS `inventory_exception_file`;
CREATE TABLE `inventory_exception_file`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `relation_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '关联单号',
  `relation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型',
  `file_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文件名',
  `file_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文件地址',
  `file_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文件类型(图片 文件)',
  `is_active` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '清点差异附件' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_exception_record
-- ----------------------------
DROP TABLE IF EXISTS `inventory_exception_record`;
CREATE TABLE `inventory_exception_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '铺货单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '铺货类型(铺货,调拨)',
  `transfer_product_id` bigint(0) NOT NULL COMMENT '铺货产品单号',
  `package_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '包裹单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL COMMENT '货损数量',
  `exception_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货损类型(破损)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建,已处理)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '清点差异记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_flow_item
-- ----------------------------
DROP TABLE IF EXISTS `inventory_flow_item`;
CREATE TABLE `inventory_flow_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存项目id',
  `source_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '来源明细单号',
  `location_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库id',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称Id',
  `batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '批次号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `brand_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的Code',
  `brand_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的名称',
  `business_category` bigint(0) NOT NULL DEFAULT 0 COMMENT '业务分类：上传库存、2：修改现货库存 3： 修改锁定库存 4：下单',
  `business_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '业务分类编码',
  `business_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '业务分类名称',
  `first_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类Code',
  `first_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类名称',
  `second_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类Code',
  `second_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类名称',
  `third_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类Code',
  `third_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类名称',
  `four_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类Code',
  `four_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类名称',
  `five_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类code',
  `five_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类名称',
  `uom_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计量单位',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计量单位文本',
  `before_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存修改前总数量',
  `after_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后库存总数量',
  `change_total_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存总数改变数值',
  `before_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '改变之前可用库存',
  `after_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '改变之后可用库存',
  `change_can_use_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '改变的可用库存',
  `before_occupy_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前占用库存',
  `after_occupy_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后占用库存',
  `change_occupy_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改占用库存',
  `before_no_sale_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前不可销售库存（损坏的库存）',
  `after_no_sale_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后不可销售库存（损坏的库存）',
  `change_no_sale_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改不可销售库存（损坏的库存）',
  `before_lock_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前锁定的库存（活动/促销）',
  `after_lock_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后锁定的库存（活动/促销）',
  `after_invented_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后虚拟库存（仓库中没有实物库存，实物库存来源于供应商）',
  `before_invented_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前虚拟库存（仓库中没有实物库存，实物库存来源于供应商）',
  `change_lock_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改锁定库存数量',
  `before_allocation_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前调配库存',
  `after_allocation_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后调配库存',
  `change_invented_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改虚拟库存（仓库中没有实物库存，实物库存来源于供应商）',
  `change_allocation_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改调配库存',
  `after_allocationing_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后调配中库存',
  `before_allocationing_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前调配中库存',
  `change_allocationing_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改调配中库存',
  `before_safe_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前安全库存设置',
  `after_safe_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后安全库存设置',
  `change_safe_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改安全库存',
  `before_enough_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前充足库存设置的数量',
  `after_enough_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后充足库存设置的数量',
  `change_enough_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改充足库存设置的数量',
  `before_tight_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改前紧张库存数量设置',
  `after_tight_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改后紧张库存数量设置',
  `change_tight_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '修改紧张库存数量设置',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '库存流水' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for inventory_package_record
-- ----------------------------
DROP TABLE IF EXISTS `inventory_package_record`;
CREATE TABLE `inventory_package_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `transfer_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '铺货单号',
  `transfer_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '铺货类型',
  `transfer_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '铺货产品单号',
  `package_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '包裹号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '实收数量',
  `bad_num` int(0) NOT NULL DEFAULT 0 COMMENT '破损数量',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 992 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '记录包裹实收产品数量' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for product_claim_record
-- ----------------------------
DROP TABLE IF EXISTS `product_claim_record`;
CREATE TABLE `product_claim_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `warehouse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `tech_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师编号',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师名称',
  `receive_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取人',
  `receive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '领取时间',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(新建/部分退货/已退货)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_wms_log
-- ----------------------------
DROP TABLE IF EXISTS `shop_wms_log`;
CREATE TABLE `shop_wms_log`  (
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

-- ----------------------------
-- Table structure for source_inventory
-- ----------------------------
DROP TABLE IF EXISTS `source_inventory`;
CREATE TABLE `source_inventory`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '收货单号',
  `ref_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '关联Id',
  `from_warehouse_id` bigint(0) NULL DEFAULT 0 COMMENT '来源仓库',
  `from_warehouse_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '发货仓名称',
  `to_warehouse_id` bigint(0) NULL DEFAULT 0 COMMENT '目的仓库',
  `to_warehouse_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '目的仓库名称',
  `source_type` bigint(0) NULL DEFAULT 0 COMMENT '来源类型（1:铺货 2:调拨）',
  `source_type_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '来源类型名称',
  `data_transfer_type_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '数据同步的方式文本',
  `data_transfer_type` bigint(0) NULL DEFAULT 0 COMMENT '数据同步的方式',
  `status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '库存状态（状态：新建 、全部入库，部分入库、缺损  ）',
  `delivery_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '配送类型',
  `delivery_company` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '快递公司',
  `delivery_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '快递单号',
  `delivery_freight` decimal(10, 0) NULL DEFAULT 0 COMMENT '快递费',
  `expected_send_time` datetime(0) NULL DEFAULT NULL COMMENT '期望发货时间',
  `excepted_arrive_time` datetime(0) NULL DEFAULT NULL COMMENT '期望送达时间',
  `send_time` datetime(0) NULL DEFAULT NULL COMMENT '发出时间',
  `stock_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '库存类型(良品，不良品)',
  `arrived_time` datetime(0) NULL DEFAULT NULL COMMENT '货物到达时间',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '收货单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for source_inventory_item
-- ----------------------------
DROP TABLE IF EXISTS `source_inventory_item`;
CREATE TABLE `source_inventory_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `ref_item_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '关联明细id',
  `source_inventory_id` bigint(0) NULL DEFAULT 0 COMMENT '入库项目Id',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '外联批次号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '商品名称Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '商品名称',
  `brand_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '品牌的Code',
  `brand_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '品牌的名称',
  `first_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '一级分类Code',
  `first_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '一级分类名称',
  `second_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '二级分类Code',
  `second_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '二级分类名称',
  `third_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '三级分类Code',
  `third_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '三级分类名称',
  `four_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '四级分类Code',
  `four_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '四级分类名称',
  `five_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '五级商品分类code',
  `five_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '五级商品分类名称',
  `num` bigint(0) NULL DEFAULT 0 COMMENT '数量',
  `price` decimal(10, 0) NULL DEFAULT 0 COMMENT '单价',
  `amount` decimal(10, 0) NULL DEFAULT 0 COMMENT '金额',
  `cost` decimal(10, 0) NULL DEFAULT 0 COMMENT '成本',
  `freight` decimal(10, 0) NULL DEFAULT 0 COMMENT '运费',
  `own_id` bigint(0) NULL DEFAULT 0 COMMENT '货主id',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '0' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `supplier_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '供应商Id',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '供应商名称',
  `week_year` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '周期',
  `uom_id` bigint(0) NULL DEFAULT 0 COMMENT '单位id',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '单位文本',
  `is_deleted` tinyint(0) NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '收货单明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_in
-- ----------------------------
DROP TABLE IF EXISTS `stock_in`;
CREATE TABLE `stock_in`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `source_inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源单据Id',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `stock_in_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库单号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `stock_in_start_date` datetime(0) NOT NULL COMMENT '开始入库日期',
  `stock_in_finish_date` datetime(0) NOT NULL COMMENT '入库结束时间',
  `stock_in_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库类型(盘盈入库、采购入库、退货入库、其他入库）',
  `sotck_in_type_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库类型文本',
  `stock_in_num` bigint(0) NOT NULL DEFAULT 0 COMMENT '入库总数量',
  `status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态（全部入库、部分入库）',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '描述信息',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '入库' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_in_item
-- ----------------------------
DROP TABLE IF EXISTS `stock_in_item`;
CREATE TABLE `stock_in_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `stock_in_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '入库单Id',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联批次单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `brand_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的Code',
  `brand_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的名称',
  `first_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类Code',
  `first_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类名称',
  `second_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类Code',
  `second_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类名称',
  `third_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类Code',
  `third_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类名称',
  `four_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类Code',
  `four_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类名称',
  `five_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类code',
  `five_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类名称',
  `supplier_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商Id',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `own_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '货主id',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `week_year` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '周期',
  `num` bigint(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '单价',
  `amount` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '金额',
  `freight` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '运费',
  `cost` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '入库成本',
  `uom_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位id',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位文本',
  `product_attr_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '入库产品的属性（良品，不良品）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '入库明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_in_ref_stock_batch
-- ----------------------------
DROP TABLE IF EXISTS `stock_in_ref_stock_batch`;
CREATE TABLE `stock_in_ref_stock_batch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `stock_in_item_id` bigint(0) NULL DEFAULT 0 COMMENT '库存入库明细id',
  `stock_batch_id` bigint(0) NULL DEFAULT 0 COMMENT '库存批次',
  `is_deleted` tinyint(0) NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '入库单库存批次关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_inout
-- ----------------------------
DROP TABLE IF EXISTS `stock_inout`;
CREATE TABLE `stock_inout`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `location_id` bigint(0) NOT NULL COMMENT '门店编号',
  `location_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `operation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型(入库,出库)',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源类型(采购入库 订单出库 盘盈入库 盘亏出库)',
  `operator` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领用人',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `operate_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '出入库时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7723 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_inout_item
-- ----------------------------
DROP TABLE IF EXISTS `stock_inout_item`;
CREATE TABLE `stock_inout_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `inout_id` bigint(0) NOT NULL COMMENT '出入库编号',
  `releation_id` bigint(0) NOT NULL COMMENT '关联子单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `batch_no` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '批次编号',
  `supplier_id` bigint(0) NOT NULL COMMENT '供应商编号',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `num` int(0) NOT NULL COMMENT '出入库总数',
  `actual_num` int(0) NOT NULL COMMENT '已入数量',
  `good_num` int(0) NOT NULL COMMENT '良品数',
  `bad_num` int(0) NOT NULL COMMENT '不良品数',
  `cost` decimal(18, 2) NOT NULL COMMENT '成本单价',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位文本',
  `status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14280 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_out
-- ----------------------------
DROP TABLE IF EXISTS `stock_out`;
CREATE TABLE `stock_out`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `out_stock_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库单号',
  `out_stock_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库类型（order）',
  `out_stock_type_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库类型文本',
  `source_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联的单号',
  `num` bigint(0) NOT NULL DEFAULT 0 COMMENT '出库数量',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注描述',
  `status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库状态(失败、成功、部分成功）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '出库' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stock_out_item
-- ----------------------------
DROP TABLE IF EXISTS `stock_out_item`;
CREATE TABLE `stock_out_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `stock_out_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '出库id',
  `source_inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '来源单据Id',
  `source_inventory_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称Id',
  `inventory_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '库存id',
  `ref_batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '外联批次单号',
  `batch_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '批次号',
  `own_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '货主',
  `own_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '货主类型 (仓库、门店、供应商(Company,Shop))',
  `supplier_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商Id',
  `supplier_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商名称',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `brand_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的Code',
  `brand_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌的名称',
  `first_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类Code',
  `first_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级分类名称',
  `second_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类Code',
  `second_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级分类名称',
  `third_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类Code',
  `third_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '三级分类名称',
  `four_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类Code',
  `four_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四级分类名称',
  `five_category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类code',
  `five_category_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级商品分类名称',
  `num` bigint(0) NOT NULL DEFAULT 0 COMMENT '出库数量',
  `price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '出库单价',
  `amount` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '出库金额',
  `cost` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '出库成本',
  `sell_amoount` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '销售金额',
  `sell_unit_price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '销售单价',
  `discount_price` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '折扣金额',
  `uom_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位id',
  `uom_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位文本',
  `product_attr_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出库产品的属性（良品，次品）',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建用户ID',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '出库明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for storage_plan
-- ----------------------------
DROP TABLE IF EXISTS `storage_plan`;
CREATE TABLE `storage_plan`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `plan_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计划编号',
  `plan_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '计划名称',
  `source_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品来源(总部产品，外采产品)',
  `type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '盘点类型(指定产品 全盘)',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '执行状态(新建 盘点中 盘点完成 差异处理中)',
  `plan_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '计划时间',
  `actual_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '实际完成时间',
  `warehouse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 173 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB AUTO_INCREMENT = 3682 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for tech_claim_product
-- ----------------------------
DROP TABLE IF EXISTS `tech_claim_product`;
CREATE TABLE `tech_claim_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `record_id` bigint(0) NOT NULL COMMENT '记录单号',
  `product_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产名称',
  `category_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品类型',
  `cost_price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '成本价',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '领取数量',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '上门养车领取产品' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
