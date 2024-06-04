/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : shop_purchase

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:14:00
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for purchase_month_pay
-- ----------------------------
DROP TABLE IF EXISTS `purchase_month_pay`;
CREATE TABLE `purchase_month_pay`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NULL DEFAULT NULL,
  `vender_id` bigint(0) NULL DEFAULT NULL,
  `vender_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `bank_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `account_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `amount` decimal(18, 2) NULL DEFAULT NULL,
  `pay_method` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '支付方式(支付宝 微信 银联)',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '状态(新建，已取消，已审核，已付款)',
  `audit_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000',
  `audit_user` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `serial_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `payer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `pay_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000',
  `cancle_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000',
  `cancle_user` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `is_deleted` int(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `relation_purchaseIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '关联采购单',
  `opening_bank` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '开户行',
  `receive_bank_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '收款户名',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 35 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_order
-- ----------------------------
DROP TABLE IF EXISTS `purchase_order`;
CREATE TABLE `purchase_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `purchase_type` int(0) NOT NULL COMMENT '类型 1 商品内采 2 商品外采，3内销，4外销',
  `vender_id` bigint(0) NOT NULL COMMENT '供应商客户编号',
  `vender_short_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商客户简称',
  `vender_warehouse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '供客仓库Id',
  `vender_warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供客仓库名称',
  `status` int(0) NOT NULL DEFAULT 0 COMMENT '状态(1新建 2待发货 3已取消 4已发货 5部分收货 6已收货)',
  `pinvoice_type` int(0) NOT NULL DEFAULT 0 COMMENT '发票类型(0 无需发票 2 普通发票 3 增值税发票)',
  `pay_type` int(0) NOT NULL DEFAULT 0 COMMENT '结算方式(1 现金 2 账期 3 月结 )',
  `pay_status` int(0) NOT NULL DEFAULT 0 COMMENT '支付状态(1未付款 2部分付款 3已付款)',
  `total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总金额',
  `buyer` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '采购员',
  `waybill_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '运单号',
  `own_freight` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '自负运费',
  `coupon_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '优惠金额',
  `actual_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实付款',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `warehouse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  `hc_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '红冲订单类型',
  `relation_purchase_id` bigint(0) NULL DEFAULT NULL COMMENT '关联原始采购单',
  `is_deleted` int(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '备注说明',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `pay_method` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '支付方式(支付宝，微信，银联)',
  `serial_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '交易参考号',
  `payer` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '支付人',
  `pay_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '支付时间',
  `mouth_pay_flag` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '月结标识(审核中,已失效,已结算)',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3382 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_order_log
-- ----------------------------
DROP TABLE IF EXISTS `purchase_order_log`;
CREATE TABLE `purchase_order_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `object_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `object_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型',
  `log_level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '日志级别(1Info  2Warning 3Error 4Critical)',
  `before_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改前值',
  `after_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改后值',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`, `object_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purchase_order_product
-- ----------------------------
DROP TABLE IF EXISTS `purchase_order_product`;
CREATE TABLE `purchase_order_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `po_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '主单号',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `unit` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '单价',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量 ',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额',
  `purchase_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购成本价格',
  `total_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总成本',
  `tax_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '税点',
  `sale_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售单价',
  `purchase_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购价格不含税',
  `total_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购总价不含税',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `plan_instock_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '计划入库时间',
  `instock_num` int(0) NOT NULL DEFAULT 0 COMMENT '已入库数量',
  `status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(1新建 2预约中 3已取消 4待审核 5待财务审核 6已驳回 7审核通过 8已发货 9部分收货 10已收货，11已退货)',
  `is_deleted` int(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `specification` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '型号规格',
  `oe_number` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '原厂编号',
  `category_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分类名称',
  `stock_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '库存id,逗号分割',
  `back_num` int(0) NOT NULL DEFAULT 0 COMMENT '退货数量',
  `warehouse_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '仓库编号',
  `warehouse_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '仓库名称',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5166 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vender
-- ----------------------------
DROP TABLE IF EXISTS `vender`;
CREATE TABLE `vender`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `vender_short_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商简称',
  `vender_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商全称',
  `class_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区分类型(1普通 2高级)',
  `supply_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应类型(1轮胎 2保养 3车品 4轮毂 5美容)',
  `office_address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '办公地址',
  `cooperative_brand` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '合作品牌',
  `qualification` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '资质编号',
  `business_license` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '营业执照编号',
  `organization_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '组织机构代码',
  `enterprise_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '企业名称',
  `enterprise_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '企业电话',
  `account` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行账号',
  `bank` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行名称',
  `register_address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '注册地址',
  `payee` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收款人',
  `tex` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '税号',
  `tel_num` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系电话',
  `finance_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '财务收款人',
  `finance_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '财务联系方式',
  `cooperation_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '合作类型(1三方 2加盟)',
  `rebate_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '返利类型(1月返 2季返 3年返)',
  `biz_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主体人',
  `biz_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主体人联系方式',
  `payment_day` int(0) NOT NULL DEFAULT 0 COMMENT '付款有效期（账期天数）',
  `over_sale_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '售后联系人',
  `over_sale_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '售后联系电话',
  `over_sale_address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '售后服务地址',
  `is_active` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否激活',
  `bulk_biz_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主体人',
  `bulk_biz_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主体电话',
  `email_address` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮件地址',
  `fax` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '传真',
  `tax_point` int(0) NOT NULL DEFAULT 0 COMMENT '税点',
  `shipment_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发货方式(1供应商送货 2总部自提)',
  `province_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省编号',
  `city_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市编号',
  `district_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区编号',
  `province_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省名称',
  `city_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市名称',
  `district_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区名称',
  `other_supply_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '其他类型',
  `other_supply_category` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '其他供应商品类',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `is_deleted` int(0) NOT NULL DEFAULT 0 COMMENT '是否删除 1 是 0 否 默认 0',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `offcial_website` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '官网地址',
  `contact` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系人',
  `invoice_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '开票类型（0未设置 1无需发票 2普通发票 3增值税发票）',
  `settlement_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '结算方式（0未设置 1现结  2账期(废弃) 3月结）',
  `classify_mark` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分类标注（例如：供应机油）',
  `opening_bank` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户银行',
  `receive_bank_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '收款户名',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 98 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
