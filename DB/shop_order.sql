/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : shop_order

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:13:41
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for employee_performance_order
-- ----------------------------
DROP TABLE IF EXISTS `employee_performance_order`;
CREATE TABLE `employee_performance_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `install_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '安装时间',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工编号',
  `employee_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工名称',
  `employee_phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工手机号',
  `tab_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '展示类型( 1套餐卡，2次数项目，3金额项目)',
  `order_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '订单类型（0未设置 1轮胎 2保养 3美容 4 钣金喷漆 5 维修改装 6 其他  ）',
  `produce_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `total_number` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总数量',
  `total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总金额',
  `share_discount_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '分摊优惠金额',
  `actual_pay_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实际支付金额',
  `total_cost_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总成本价',
  `settlement_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务结算金额',
  `maoli` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '毛利金额',
  `percent` decimal(5, 2) NOT NULL DEFAULT 0.00 COMMENT '派工比例',
  `install_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '安装绩效',
  `sale_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售绩效',
  `total_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总绩效',
  `performance_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '绩效类型(0默认， 1单品，2小品类)',
  `type_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类型参数（多个用逗号隔开）',
  `config_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配置类型(比例 1/金额2)',
  `config_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '比例/金额',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 36 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号（RGS前缀）',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道（0未设置 1总部C端 2总部门店）',
  `terminal_type` tinyint(0) NOT NULL COMMENT '终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）',
  `app_version` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '应用版本号',
  `order_type` tinyint(0) NOT NULL COMMENT '订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）',
  `produce_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '产生类型（0普通产生 1购买核销码产生 2使用核销码产生）',
  `order_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '订单状态（10已提交 20已确认 30已完成 40已取消）',
  `order_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '下单时间',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '下单人用户Id',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '下单人姓名',
  `user_phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '下单人电话',
  `contact_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系人用户Id',
  `contact_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系人姓名',
  `contact_phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '联系人电话',
  `total_product_num` int(0) NOT NULL COMMENT '商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）',
  `total_product_amount` decimal(18, 2) NOT NULL COMMENT '商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）',
  `service_num` int(0) NOT NULL DEFAULT 0 COMMENT '服务数量（指带出的安装服务，不含套餐内安装服务）',
  `service_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '服务费（指带出的安装服务，不含套餐内安装服务）',
  `delivery_fee` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '运费',
  `total_coupon_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总优惠金额',
  `actual_amount` decimal(18, 2) NOT NULL COMMENT '实付款',
  `pay_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付方式（0未设置 1在线支付 2到店付款）',
  `pay_channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付渠道（0未设置 1微信支付 2支付宝）',
  `pay_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付状态（0未支付 1已支付）',
  `money_arrive_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '到账状态（0未到账 1已到账）',
  `stock_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）',
  `is_need_invoice` tinyint(0) NOT NULL COMMENT '是否需要开发票（0否 1是）',
  `is_need_delivery` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要配送（0否 1是）',
  `delivery_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配送类型（0未设置 1配送到店 2配送到家）',
  `delivery_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配送方式（0未设置 1自配 2快递）',
  `delivery_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配送状态（0未配送 1已配送）',
  `sign_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '签收状态（0未签收 1已签收）',
  `is_need_install` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要安装服务（0否 1是）',
  `install_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '安装码',
  `company_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '公司Id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `reserve_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '预约状态（0未预约 1已预约）',
  `receive_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '到店状态（0未到店 1已到店）',
  `dispatch_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '派工状态（0未派工 1已派工）',
  `install_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '安装服务状态（0未安装 1已安装）',
  `comment_status` tinyint(0) NOT NULL COMMENT '点评状态（0待客户点评  1 客户已点评 2 客户已追评）',
  `is_occur_reverse` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否发生过逆向申请（0否 1是）',
  `reverse_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '逆向申请单状态（10已提交 20已确认 30已完成 40已取消）',
  `refund_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '退款状态（0未退款 1已退款）',
  `settle_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '结算状态（0未结算 1已结算）',
  `reconciliation_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '订单对账状态（0未对账 1异常 2已对账）',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '订单备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识（0否 1是）',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `pay_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '支付时间',
  `confirm_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '确认时间',
  `delivery_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '开始配送的时间',
  `sign_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '签收时间',
  `reserve_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '预约时间',
  `receive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '到店时间',
  `dispatch_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '派工时间',
  `install_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '安装时间',
  `reconciliation_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '总部对账的时间',
  `settle_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '结算时间',
  `cancel_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '整单取消的时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `is_deleted`(`is_deleted`) USING BTREE,
  INDEX `order_no`(`order_no`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 603 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店订单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_address
-- ----------------------------
DROP TABLE IF EXISTS `order_address`;
CREATE TABLE `order_address`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `address_type` tinyint(0) NOT NULL COMMENT '地址类型（0未设置 1门店地址 2客户地址）',
  `shop_id` bigint(0) NOT NULL COMMENT '门店Id（当地址类型为门店时存储）',
  `address_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '地址Id（当地址类型为客户时存储）',
  `label` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '地址标签（家/公司/学校等）',
  `is_default` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否默认（1是）',
  `receiver_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '收货人姓名',
  `receiver_phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '收货人电话',
  `province_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '省Id',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '省',
  `city_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '市Id',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '市',
  `district_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '区县Id',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '区县',
  `detail_address` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '详细地址',
  `remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 591 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单地址表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_car
-- ----------------------------
DROP TABLE IF EXISTS `order_car`;
CREATE TABLE `order_car`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '车辆Id',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `nick_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '车辆昵称',
  `car_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '车牌号',
  `brand` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '品牌（奥迪）',
  `vehicle` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '车系（A4L)',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `pai_liang` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '排量（2.0T）',
  `nian` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '年份（2009）',
  `tid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Tid',
  `sales_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '款型（2009款 2.0T 无极 标准版）',
  `total_mileage` int(0) NOT NULL COMMENT '总公里数',
  `last_bao_yang_km` int(0) NOT NULL COMMENT '最后一次保养公里数',
  `last_bao_yang_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最后一次保养时间',
  `vin_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT 'VIN码（车辆识别代号）',
  `default_car` tinyint(0) NOT NULL DEFAULT 0 COMMENT '默认车型',
  `engine_no` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '发动机编号',
  `tire_size_for_single` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '轮胎尺寸',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 591 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单车辆表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_contact_change
-- ----------------------------
DROP TABLE IF EXISTS `order_contact_change`;
CREATE TABLE `order_contact_change`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` int(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `before_user_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '变更前联系人用户Id',
  `before_open_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '变更前微信OpenId',
  `after_user_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '变更后联系人用户Id',
  `after_open_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '变更后微信OpenId',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单联系人变更表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_coupon
-- ----------------------------
DROP TABLE IF EXISTS `order_coupon`;
CREATE TABLE `order_coupon`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `user_coupon_id` bigint(0) NOT NULL COMMENT '用户使用的优惠券Id',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '使用规则Id',
  `coupon_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '优惠券标题名称',
  `coupon_amount` decimal(18, 2) NOT NULL COMMENT '优惠券金额',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_dispatch
-- ----------------------------
DROP TABLE IF EXISTS `order_dispatch`;
CREATE TABLE `order_dispatch`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `tech_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '技师Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '技师姓名',
  `level` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '职级',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `percent` decimal(5, 2) NOT NULL DEFAULT 0.00 COMMENT '比例',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `order_no`(`order_no`) USING BTREE,
  INDEX `tech_name`(`tech_name`) USING BTREE,
  INDEX `is_deleted`(`is_deleted`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23278 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_log
-- ----------------------------
DROP TABLE IF EXISTS `order_log`;
CREATE TABLE `order_log`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `type1` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义类型1',
  `type2` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义类型2',
  `filter1` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义过滤1',
  `filter2` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义过滤2',
  `summary` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作摘要',
  `before_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作前值',
  `after_value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作后值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_master
-- ----------------------------
DROP TABLE IF EXISTS `order_master`;
CREATE TABLE `order_master`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `person_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '负责人标识',
  `person_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '负责人姓名',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '负责人类型（Owner...）',
  `follow_stage` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '开始跟进阶段（BeforeCreate...)',
  `remark` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单负责人表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_not_adapter
-- ----------------------------
DROP TABLE IF EXISTS `order_not_adapter`;
CREATE TABLE `order_not_adapter`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NULL DEFAULT 0 COMMENT '门店Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '录入的内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识（0否 1是）',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_product
-- ----------------------------
DROP TABLE IF EXISTS `order_product`;
CREATE TABLE `order_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品显示名称',
  `description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '产品描述',
  `product_attribute` tinyint(0) NOT NULL DEFAULT 0 COMMENT '产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品）',
  `parent_order_package_pid` bigint(0) NOT NULL DEFAULT 0 COMMENT '所属父级订单套餐产品Id',
  `serve_for_order_pids` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '服务属于订单实物产品Id（多个pid以;分割)',
  `category_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '商品类目编号',
  `item_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '适配项目编号（暂不存储）',
  `label` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '标签',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '图片路径',
  `price_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '价格Id',
  `marketing_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '市场单价',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售单价',
  `total_cost_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总成本价（实物取自库存，服务取自门店）（指乘以购买数量后）',
  `unit` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '单位（个/升/斤等）',
  `number` int(0) NOT NULL DEFAULT 0 COMMENT '数量（指单个套餐中该商品）',
  `total_number` int(0) NOT NULL DEFAULT 0 COMMENT '总数量（指乘以购买数量后）',
  `stock_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额（指单个套餐中该商品）',
  `total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '总金额（指乘以购买数量后）',
  `tax_rate` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '税率',
  `share_discount_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '分摊优惠金额',
  `actual_pay_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '实际支付金额',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识（0否 1是）',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `is_deleted`(`is_deleted`) USING BTREE,
  INDEX `order_id`(`order_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 595 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单商品表（含套餐和服务）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_sale
-- ----------------------------
DROP TABLE IF EXISTS `order_sale`;
CREATE TABLE `order_sale`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `tech_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '技师Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '技师姓名',
  `level` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '职级',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `percent` decimal(5, 2) NOT NULL DEFAULT 0.00 COMMENT '比例',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order_user
-- ----------------------------
DROP TABLE IF EXISTS `order_user`;
CREATE TABLE `order_user`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `is_first_order` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否首单用户',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `user_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户姓名',
  `nick_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '昵称',
  `head_url` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '头像地址',
  `gender` tinyint(0) UNSIGNED NOT NULL DEFAULT 0 COMMENT '性别（0未设置 1男 2女）',
  `birth_day` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '生日',
  `user_tel_des` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户手机号[脱敏]',
  `user_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户手机号',
  `member_level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '会员等级显示',
  `point` int(0) NOT NULL DEFAULT 0 COMMENT '会员积分',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 591 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订单用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sign
-- ----------------------------
DROP TABLE IF EXISTS `sign`;
CREATE TABLE `sign`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `relation_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `source_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '来源类型（0：未设置 1：订单)',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '包裹数量',
  `have_sign_num` int(0) NOT NULL DEFAULT 0 COMMENT '已签收包裹的数量',
  `sign_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '签收状态(0:设置 1：未签收 2: 部分签收 1:全部签收）',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `package_nos` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '包裹号（以、分割)',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '订单备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 173 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sign_detail
-- ----------------------------
DROP TABLE IF EXISTS `sign_detail`;
CREATE TABLE `sign_detail`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `sign_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '签收记录id',
  `relation_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '关联单号',
  `package_no` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '物流单号',
  `sign_user` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '签收用户',
  `sign_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '签收时间',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '订单备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 182 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
