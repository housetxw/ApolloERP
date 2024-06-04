/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : shop_manage

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:13:00
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for advertisement
-- ----------------------------
DROP TABLE IF EXISTS `advertisement`;
CREATE TABLE `advertisement`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '广告主键ID',
  `title` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '标题',
  `content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '内容',
  `url` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '链接地址',
  `img_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图片地址',
  `type` tinyint(0) NOT NULL COMMENT '广告类型  1默认 2普通网页 3原生页面',
  `start_date` datetime(3) NOT NULL COMMENT '有效开始时间',
  `end_date` datetime(3) NOT NULL COMMENT '有效结束时间',
  `is_display` tinyint(0) NOT NULL COMMENT '是否展示',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bank_information
-- ----------------------------
DROP TABLE IF EXISTS `bank_information`;
CREATE TABLE `bank_information`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `bank_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行名称',
  `icon_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行logo',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_service
-- ----------------------------
DROP TABLE IF EXISTS `base_service`;
CREATE TABLE `base_service`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `product_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '英文code',
  `category_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级分类id',
  `category_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分类类型',
  `default_sale_price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '默认销售价',
  `default_cost_price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '默认成本价',
  `default_sale_price_limit` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '默认销售价格限制',
  `default_cost_price_limit` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '成本价限制',
  `service_remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务备注',
  `sale_price_remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '价格备注',
  `accessories_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件名称',
  `service_charge` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '服务费',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 239 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for basic_performance_config
-- ----------------------------
DROP TABLE IF EXISTS `basic_performance_config`;
CREATE TABLE `basic_performance_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `service_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '服务类型',
  `config_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配置类型(比例 1/金额2)',
  `config_point` decimal(18, 2) NOT NULL COMMENT '比例/金额',
  `config_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '单项服务修改时间(备用字段)',
  `service_config_flag` tinyint(0) NOT NULL DEFAULT 1 COMMENT '单项服务开关(备用字段)',
  `config_flag` tinyint(0) NOT NULL COMMENT '绩效配置总开关',
  `config_flag_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '绩效配置修改时间',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 60 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for business_daily
-- ----------------------------
DROP TABLE IF EXISTS `business_daily`;
CREATE TABLE `business_daily`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '经营日报主键id',
  `shop_id` bigint(0) NOT NULL COMMENT '门店id',
  `today_earn` decimal(11, 2) NOT NULL COMMENT '今日收益',
  `month_earn` decimal(11, 2) NOT NULL COMMENT '本月收益',
  `customer_count` int(0) NOT NULL COMMENT '今日到店客户数量',
  `new_customer_count` int(0) NOT NULL COMMENT '新客户数量',
  `old_customer_count` int(0) NOT NULL COMMENT '老客户数量',
  `average_earn` decimal(11, 2) NOT NULL COMMENT '平均收益',
  `new_customer_earn` decimal(11, 2) NOT NULL COMMENT '新客户收益',
  `old_customer_earn` decimal(11, 2) NOT NULL COMMENT '老客户收益',
  `shop_score` decimal(3, 2) NOT NULL COMMENT '门店评分',
  `good_score_count` int(0) NOT NULL COMMENT '5星好评数',
  `general_score_count` int(0) NOT NULL COMMENT '4星及以下评论数',
  `attendance_count` int(0) NOT NULL COMMENT '出勤人数',
  `rider_count` int(0) NOT NULL COMMENT '车友人数',
  `early_meeting` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '早会记录',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for company
-- ----------------------------
DROP TABLE IF EXISTS `company`;
CREATE TABLE `company`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '公司id',
  `simple_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司简称',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司名称',
  `parent_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级公司id',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司编号',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '公司状态 1待提交 2审核中 3正常 4注销 5审核未通过',
  `type` tinyint(0) UNSIGNED NOT NULL DEFAULT 1 COMMENT '类型；1公司，2仓库，3拓展，4供应商',
  `system_type` int(0) NOT NULL DEFAULT 0 COMMENT '1平台公司，2普通公司，3区域合伙公司',
  `level` tinyint(1) NOT NULL DEFAULT 1 COMMENT '公司等级 1一级公司 2二级公司',
  `province_id` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司所在省Id',
  `city_id` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司所在市Id',
  `district_id` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司所在区Id',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省名',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市名',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区名',
  `address` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地址',
  `mobile` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机号',
  `email` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱',
  `head` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司负责人',
  `head_phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '负责人电话',
  `register_money` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '注册资金 单位：万元',
  `register_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '工商注册日期',
  `legal_person` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '公司法人',
  `legal_person_phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '法人联系电话',
  `business_license` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '营业执照',
  `opening_bank` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户银行',
  `bank_account` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行账号',
  `account_opening_license` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户许可证',
  `introduction` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '公司介绍',
  `license` tinyint(0) NOT NULL DEFAULT 0 COMMENT '执照 （废弃）',
  `examiner` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `failed_examined_reason` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '未通过审核原因',
  `deposit_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '支付押金额度',
  `service_level` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '加盟等级',
  `vender_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '供应商编号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 58 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for company_deposit_record
-- ----------------------------
DROP TABLE IF EXISTS `company_deposit_record`;
CREATE TABLE `company_deposit_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `company_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '公司Id',
  `operation_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型',
  `description` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额',
  `remark` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for company_img
-- ----------------------------
DROP TABLE IF EXISTS `company_img`;
CREATE TABLE `company_img`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `company_id` bigint(0) NOT NULL COMMENT '公司id',
  `img_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图片路径',
  `type` tinyint(0) NOT NULL COMMENT '图片类型 1公司照 ',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department`  (
  `id` bigint(0) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '部门id',
  `parent_id` int(0) NOT NULL COMMENT '父级部门id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门名称',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '部门编号',
  `organization_id` int(0) UNSIGNED NOT NULL COMMENT '所属单位Id',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型；0：公司；1：门店；2：仓库；',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee`  (
  `id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工id',
  `account_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '账号Id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '姓名（登录token中已用，不可修改）',
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱地址',
  `mobile` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '手机号',
  `gender` tinyint(0) NOT NULL DEFAULT 0 COMMENT '性别（0保密 1男 2女）',
  `number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工号',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型；0：公司；1：门店；2：仓库；',
  `organization_id` int(0) UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属单位Id',
  `department_id` int(0) NOT NULL DEFAULT 0 COMMENT '部门id\r\n',
  `job_id` int(0) NOT NULL DEFAULT 0 COMMENT '职位id\r\n',
  `level` tinyint(0) NOT NULL DEFAULT 0 COMMENT '职级',
  `work_kind_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '工种id',
  `identity` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '身份证号码',
  `we_chat` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '微信号',
  `qq` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'QQ号',
  `address` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地址',
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '照片地址',
  `score` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '评分',
  `qualification_certificate` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工证书照片地址，多张以英文 ‘;’ 隔开',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工描述',
  `dimission_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '离职类型；0：自动离职；1：辞退 等等',
  `dimission_cause` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '离职原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee_group
-- ----------------------------
DROP TABLE IF EXISTS `employee_group`;
CREATE TABLE `employee_group`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '员工id',
  `employee_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '姓名（登录token中已用，不可修改）',
  `mobile` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机号',
  `group_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `group_leader` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `shop_id` bigint(0) NOT NULL COMMENT '门店Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee_organization_range
-- ----------------------------
DROP TABLE IF EXISTS `employee_organization_range`;
CREATE TABLE `employee_organization_range`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '应主键id',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工Id',
  `organization_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '管辖范围的单位Id',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型；0：公司；1：门店；2：仓库；',
  `role_ids` varchar(3000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '管辖范围的角色',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否禁用，可用',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 375 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '员工的管辖范围' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee_performance_log
-- ----------------------------
DROP TABLE IF EXISTS `employee_performance_log`;
CREATE TABLE `employee_performance_log`  (
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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 143 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee_performance_report
-- ----------------------------
DROP TABLE IF EXISTS `employee_performance_report`;
CREATE TABLE `employee_performance_report`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工编号',
  `employee_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工名',
  `employee_phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工手机号',
  `install_point` decimal(18, 2) NOT NULL COMMENT '安装绩效',
  `pull_new_point` decimal(18, 2) NOT NULL COMMENT '新客绩效',
  `cunsume_point` decimal(18, 2) NOT NULL COMMENT '新客首消费绩效',
  `total_point` decimal(18, 2) NOT NULL COMMENT '总绩效',
  `report_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '生成记录时间',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 44 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for faq
-- ----------------------------
DROP TABLE IF EXISTS `faq`;
CREATE TABLE `faq`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `channel_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '渠道',
  `category_one_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '一级分类ID',
  `category_two_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '二级分类ID',
  `category_three_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '三级分类ID',
  `question` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '问题',
  `answer` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '回答',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 966 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for faq_category
-- ----------------------------
DROP TABLE IF EXISTS `faq_category`;
CREATE TABLE `faq_category`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '分类名称',
  `parent_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级分类id',
  `level` tinyint(0) NOT NULL DEFAULT 0 COMMENT '分类级别 ',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 185 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for faq_channel
-- ----------------------------
DROP TABLE IF EXISTS `faq_channel`;
CREATE TABLE `faq_channel`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '渠道名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hot_city
-- ----------------------------
DROP TABLE IF EXISTS `hot_city`;
CREATE TABLE `hot_city`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '城市名称',
  `region_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '城市编号',
  `parent_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '省编号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for job
-- ----------------------------
DROP TABLE IF EXISTS `job`;
CREATE TABLE `job`  (
  `id` bigint(0) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '职位id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '职位名称',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '职位编号',
  `organization_id` int(0) UNSIGNED NOT NULL COMMENT '所属单位Id',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型；0：公司；1：门店；2：仓库；',
  `is_show` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否在门店展示',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '岗位描述',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for job_work_kind_relation
-- ----------------------------
DROP TABLE IF EXISTS `job_work_kind_relation`;
CREATE TABLE `job_work_kind_relation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `job_id` bigint(0) NOT NULL DEFAULT 0,
  `work_kind_id` bigint(0) NOT NULL DEFAULT 0,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for join_in
-- ----------------------------
DROP TABLE IF EXISTS `join_in`;
CREATE TABLE `join_in`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '名称',
  `phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机号',
  `email` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱',
  `province_id` int(0) NOT NULL DEFAULT 0 COMMENT '省id',
  `city_id` int(0) NOT NULL DEFAULT 0 COMMENT '市id',
  `district_id` int(0) NOT NULL DEFAULT 0 COMMENT '区id',
  `short_address` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '短地址（省市区）',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 139 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

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
-- Table structure for notice
-- ----------------------------
DROP TABLE IF EXISTS `notice`;
CREATE TABLE `notice`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '公告主键id',
  `title` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '标题',
  `content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '内容',
  `department` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '发布部门',
  `is_display` tinyint(0) NOT NULL COMMENT '是否展示',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for notice_reader
-- ----------------------------
DROP TABLE IF EXISTS `notice_reader`;
CREATE TABLE `notice_reader`  (
  `id` bigint(0) NOT NULL COMMENT '主键id',
  `notice_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '公告id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `user_id` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户id',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for notice_relation
-- ----------------------------
DROP TABLE IF EXISTS `notice_relation`;
CREATE TABLE `notice_relation`  (
  `id` bigint(0) NOT NULL COMMENT '主键id',
  `notice_id` bigint(0) NOT NULL COMMENT '公告id',
  `to_obj_id` bigint(0) NOT NULL COMMENT '对象id',
  `to_obj_type` tinyint(0) NOT NULL COMMENT '对象类型',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pull_new_config
-- ----------------------------
DROP TABLE IF EXISTS `pull_new_config`;
CREATE TABLE `pull_new_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `pull_new_flag` tinyint(0) NOT NULL COMMENT '拉新开关',
  `pull_new_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '拉新开关修改时间',
  `pull_new_point` decimal(18, 2) NOT NULL COMMENT '拉新奖励点数',
  `first_consume_flag` tinyint(0) NOT NULL COMMENT '首次消费开关',
  `first_consume_type` tinyint(0) NOT NULL COMMENT '消费配置类型(按比例1,固定金额2)',
  `first_consume_point` decimal(18, 2) NOT NULL COMMENT '绩效点/固定金额',
  `first_consume_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '首次消费配置修改时间',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `is_active` tinyint(0) NOT NULL COMMENT '是否生效(总开关)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop
-- ----------------------------
DROP TABLE IF EXISTS `shop`;
CREATE TABLE `shop`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `simple_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '简单名称',
  `full_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '店全称',
  `shop_company_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '店公司名称',
  `company_id` bigint(0) NOT NULL COMMENT '公司id',
  `business_type` tinyint(0) UNSIGNED NOT NULL DEFAULT 1 COMMENT '营业类型 1 4S店，2 快修店，3修理厂 ',
  `type` int(0) NOT NULL DEFAULT 0 COMMENT '门店类型 1合作店 2工场店 4上门养车 8认证店',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '门店状态 0正常 1终止 2暂停',
  `system_type` int(0) NOT NULL DEFAULT 0 COMMENT '系统版本   (门店的0：普通 1：高级 2：个人 ；)',
  `online` tinyint(0) NOT NULL DEFAULT 0 COMMENT '上下架状态：0下架，1上架',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `province_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省',
  `city_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市',
  `district_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省名称',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市名称',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区县名称',
  `address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '详细地址',
  `longitude` double(15, 10) NOT NULL DEFAULT 0.0000000000 COMMENT '经度',
  `latitude` double(15, 10) NOT NULL DEFAULT 0.0000000000 COMMENT '维度',
  `post` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮编',
  `contact` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系人',
  `telephone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '电话',
  `mobile` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机',
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱',
  `external_phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对外电话',
  `fax` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '传真',
  `head` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '运营负责人（专职顾问）',
  `head_phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '运营负责人电话',
  `head_email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '运营负责人邮箱',
  `accounting_contact` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对账联系人',
  `accounting_contact_phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对账联系人电话',
  `accounting_person` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对账人',
  `accounting_period` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对账周期',
  `update_accounting_period_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '对账周期更新时间',
  `payable_account` int(0) NOT NULL DEFAULT 0 COMMENT '应付账户',
  `recievable_account` int(0) NOT NULL DEFAULT 0 COMMENT '回款账户',
  `rebate_account_one` int(0) NOT NULL DEFAULT 0 COMMENT '财务账号（月返）',
  `rebate_account_two` int(0) NOT NULL DEFAULT 0 COMMENT '财务账号（季返）',
  `rebate_account_three` int(0) NOT NULL DEFAULT 0 COMMENT '财务账号（年返）',
  `reconciliation_mode` tinyint(0) NOT NULL DEFAULT 0 COMMENT '对账方式',
  `check_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '门店审核状态   1待审核 2已通过审核 3未通过审核',
  `examiner` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `examiner_tel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人电话',
  `failed_examined_reason` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '未通过审核原因',
  `submitor` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '提交人',
  `submitor_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '提交人电话',
  `owner_name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店老板姓名',
  `owner_phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店老板电话',
  `id_card_front` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '身份证正面',
  `id_card_back` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '身份证背面',
  `charge` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主管',
  `charge_phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '主管电话',
  `category` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品类',
  `service_type` int(0) NOT NULL DEFAULT 1 COMMENT '服务类型',
  `level` int(0) NOT NULL DEFAULT 0 COMMENT '门店等级',
  `score` decimal(10, 0) NOT NULL DEFAULT 0 COMMENT '门店评分',
  `total_order` int(0) NOT NULL DEFAULT 0 COMMENT '总订单数',
  `applause_rate` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '好评率',
  `applet_code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '快速排队小程序码',
  `shop_applet_code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店小程序码',
  `tag_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店标签',
  `nature` tinyint(0) NOT NULL DEFAULT 0 COMMENT '商户性质 0：门店加盟 1：总部先生2：配件改装 3：工厂直销',
  `deposit_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '支付押金额度',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店的Icon图片',
  `default_product_category_img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '默认种养品类图片',
  `default_product_category_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '默认种养品类名称（葡萄、玉米）',
  `business_category` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '经营类目（粮食、蔬菜、经作、畜牧、水产、其他）',
  `open_camera` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有摄像头（0：未开通 1：已开通）',
  `camera_img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '摄像头图片',
  `green_certification` tinyint(0) NOT NULL DEFAULT 0 COMMENT ' 绿色认证（0：未通过 1：已通过)',
  `green_certification_img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '绿色认证图片',
  `safety_supervision` tinyint(0) NOT NULL DEFAULT 0 COMMENT '安全监督( 0:未通过 1：已经通过)',
  `safety_supervision_img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '安全监督图片',
  `map_search_condition` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地图搜索条件（数据量大的做优化使用）',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `is_deleted`(`is_deleted`) USING BTREE,
  INDEX `province_id`(`province_id`) USING BTREE,
  INDEX `city_id`(`city_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4487 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_asset
-- ----------------------------
DROP TABLE IF EXISTS `shop_asset`;
CREATE TABLE `shop_asset`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '资产编码',
  `first_type_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '一级类别编号',
  `second_type_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级类别编号',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '资产名称',
  `specification` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规格型号',
  `number` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `start_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '开始使用日期',
  `storage_location` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '存放地点',
  `add_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '增加方式（0未设置 1购入 2转入）',
  `owner_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '责任人员工ID',
  `use_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '使用状态（0未设置 1使用中 2已报废 3已调拨）',
  `plan_use_months` int(0) NOT NULL DEFAULT 0 COMMENT '计划使用月份',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '原单价',
  `estimate_depreciation` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '估计折旧',
  `images` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '图片地址逗号拼接（最多五张）',
  `remark` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 918 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店资产' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_asset_discard
-- ----------------------------
DROP TABLE IF EXISTS `shop_asset_discard`;
CREATE TABLE `shop_asset_discard`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `asset_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '资产ID',
  `discard_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '报废类型（0未设置 1自然报废 2灾害事故 3人为损坏）',
  `reason` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店资产报废' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_asset_maintain
-- ----------------------------
DROP TABLE IF EXISTS `shop_asset_maintain`;
CREATE TABLE `shop_asset_maintain`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `asset_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '资产ID',
  `date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '维修日期',
  `cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '维修费用',
  `supplier` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供应商',
  `content` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '维修内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店资产维修' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_asset_type
-- ----------------------------
DROP TABLE IF EXISTS `shop_asset_type`;
CREATE TABLE `shop_asset_type`  (
  `id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键ID',
  `parent_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '父级ID',
  `value` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '值',
  `label` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '名',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店资产类别' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_bank_card
-- ----------------------------
DROP TABLE IF EXISTS `shop_bank_card`;
CREATE TABLE `shop_bank_card`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `shop_id` bigint(0) NOT NULL COMMENT '门店id',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '账户类型 0个人账号 1企业账号',
  `bank_id` bigint(0) NOT NULL COMMENT '银行ID',
  `opening_bank` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '开户支行名称',
  `opening_branch` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户支行',
  `bank_no` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '银行行号',
  `opening_user_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '开户人名称',
  `bank_card_no` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行卡号',
  `company_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '企业名称',
  `opening_licence` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户许可证',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 198 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_car
-- ----------------------------
DROP TABLE IF EXISTS `shop_car`;
CREATE TABLE `shop_car`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '门店车ID',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态 0正常 1 禁用',
  `car_number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `vehicle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `pai_liang` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排量',
  `nian` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '年',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'tid',
  `sales_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `vin_code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车架号',
  `total_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '公里数',
  `price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '车辆期初价格',
  `insure_start` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '保险开始日期',
  `insure_end` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '保险结束日期',
  `insurance_company` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保险公司',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 30 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_car_record
-- ----------------------------
DROP TABLE IF EXISTS `shop_car_record`;
CREATE TABLE `shop_car_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '门店车辆使用记录',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `car_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '车辆ID',
  `car_number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌',
  `technician` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师名称',
  `mobile` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师手机号',
  `start_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '用车开始时间',
  `end_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '用车结束时间',
  `start_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '开始公里数',
  `end_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '结束公里数',
  `oil_wear` decimal(8, 2) NOT NULL DEFAULT 0.00 COMMENT '油耗 L',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 423 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_config
-- ----------------------------
DROP TABLE IF EXISTS `shop_config`;
CREATE TABLE `shop_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `shop_certification` int(0) NOT NULL DEFAULT 0 COMMENT '门店认证',
  `start_work_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '营业开始时间',
  `end_work_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '营业开始时间',
  `tax_number` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '税号',
  `contract_type` int(0) NOT NULL DEFAULT 0 COMMENT '合同类型',
  `service` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮胎/保养服务',
  `serivce_level` int(0) NOT NULL DEFAULT 1 COMMENT '服务等级',
  `tire_tech` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技术水平',
  `payment_method` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收款方式',
  `fee_per_tire` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '换轮胎费用',
  `fee_per_front_brake` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '前刹车片费用',
  `fee_per_rear_brake` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '后刹车片费用',
  `fee_per_front_disc` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '前刹车盘费用',
  `fee_per_rear_disc` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '后刹车盘费用',
  `fee_per_maintain` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '保养服务费用',
  `fee_per_4_wheel` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '四轮定位费用',
  `fee_pm_installation` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT 'PM2.5滤芯安装费用',
  `daily_order_quantity` int(0) NOT NULL DEFAULT 0 COMMENT '每日保养约单量',
  `daily_order_upper_limit` int(0) NOT NULL DEFAULT 0 COMMENT '每日保养爆单量',
  `option_order_count` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '保养爆单量/约单量比例',
  `daily_tire_order_quantity` int(0) NOT NULL DEFAULT 0 COMMENT '每日轮胎约单量',
  `daily_tire_order_upper_limit` int(0) NOT NULL DEFAULT 0 COMMENT '每日轮胎爆单量',
  `option_tire_order_count` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '轮胎爆单量/约单量比例',
  `online_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '正式上线时间',
  `lease_free_period` int(0) NOT NULL DEFAULT 0 COMMENT '租赁期',
  `lease_start_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '租赁开始日期',
  `lease_end_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '租赁结束日期',
  `is_vip_shop` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否是VIP门店',
  `is_default_shop` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否是默认门店',
  `car_fault_diagnostic_tool` tinyint(0) NOT NULL DEFAULT 0 COMMENT '汽车故障诊断仪',
  `is_lounge_room` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有休息室',
  `is_rest_room` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有卫生间',
  `is_free_wifi` tinyint(0) NOT NULL DEFAULT 0 COMMENT '免费wifi',
  `is_post_lift` tinyint(0) NOT NULL DEFAULT 0 COMMENT '柱式举升机',
  `is_check_install_code` tinyint(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000 COMMENT '是否需要安装确认码',
  `joint_account` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对接账号',
  `joint_password` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对接密码',
  `is_distribute` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否铺货 0：否 1：是',
  `lunbao_responsible_person` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮保负责人',
  `lunbao_responsible_person_tel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮保负责人电话',
  `meirong_responsible_person` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '美容负责人',
  `meirong_responsible_person_tel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '美容负责人电话',
  `suspend_start_date_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '暂停营业开始时间',
  `suspend_end_date_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '暂停营业结束时间',
  `is_create_account` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否创建老板账户 0否 1是',
  `is_send_message` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否发送短信通知 0否 1是',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_shopid`(`shop_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4294 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_cost
-- ----------------------------
DROP TABLE IF EXISTS `shop_cost`;
CREATE TABLE `shop_cost`  (
  `id` bigint(0) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店id',
  `date` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '年月份',
  `cost_type` bigint(0) NOT NULL DEFAULT 0 COMMENT '费用类别',
  `money` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额',
  `pay_channel` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '支付渠道',
  `invoice_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '发票类型，0无需发票,1普通发票,2增值税普通发票,3增值税专用发票',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态0新建,1已核对,2已取消',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 266 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_cost_type
-- ----------------------------
DROP TABLE IF EXISTS `shop_cost_type`;
CREATE TABLE `shop_cost_type`  (
  `id` bigint(0) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` bigint(0) NOT NULL DEFAULT 0 COMMENT '编号',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 21 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_customer
-- ----------------------------
DROP TABLE IF EXISTS `shop_customer`;
CREATE TABLE `shop_customer`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `user_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户名',
  `phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系电话',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '常用车辆Id',
  `brand` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌（奥迪）',
  `vehicle` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车系（A4L)',
  `receive_num` int(0) NOT NULL DEFAULT 0 COMMENT '到店数',
  `order_num` int(0) NOT NULL DEFAULT 0 COMMENT '订单数',
  `total_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '消费金额',
  `last_receive_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最后到店时间',
  `last_receive_car_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '最后到店车牌',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店客户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_delivery_config
-- ----------------------------
DROP TABLE IF EXISTS `shop_delivery_config`;
CREATE TABLE `shop_delivery_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `shop_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `delivery_user` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配送人',
  `delivery_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系方式',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_delivery_record
-- ----------------------------
DROP TABLE IF EXISTS `shop_delivery_record`;
CREATE TABLE `shop_delivery_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '订单号',
  `shop_id` bigint(0) NULL DEFAULT 0 COMMENT '门店编号',
  `delivery_user` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '配送人',
  `delivery_fee` decimal(11, 2) NULL DEFAULT 0.00 COMMENT '配送费用',
  `delivery_time` int(0) NULL DEFAULT 0 COMMENT '配送时间(分钟)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_groupon_product
-- ----------------------------
DROP TABLE IF EXISTS `shop_groupon_product`;
CREATE TABLE `shop_groupon_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `service_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务pid',
  `service_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务名称',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '团购售价',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_img
-- ----------------------------
DROP TABLE IF EXISTS `shop_img`;
CREATE TABLE `shop_img`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '门店图片主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `img_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片路径',
  `type` tinyint(0) NOT NULL DEFAULT 1 COMMENT '图片类型 1门头图片 2门店照片 3资质证明 4正面照  5营业执照  7经营许可证',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_shopid`(`shop_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18166 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_join
-- ----------------------------
DROP TABLE IF EXISTS `shop_join`;
CREATE TABLE `shop_join`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `shop_address` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店详细地址',
  `province_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省',
  `city_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市',
  `district_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省名称',
  `city` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市名称',
  `district` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区县名称',
  `shop_pic` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片',
  `contact_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系人',
  `contact_tel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系电话',
  `shop_director` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店店长',
  `shop_director_tel` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '店长联系电话',
  `is_agred_cooperate` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否同意合作',
  `is_agree_join` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否同意加盟',
  `avg_qimian` int(0) NOT NULL DEFAULT 0 COMMENT '月均漆面数',
  `count_kaofang` int(0) NOT NULL DEFAULT 0 COMMENT '烤房数',
  `count_beauty` int(0) NOT NULL DEFAULT 0 COMMENT '美容工位数',
  `count_alignment` int(0) NOT NULL DEFAULT 0 COMMENT '四轮定位工位数',
  `count_maintenance` int(0) NOT NULL DEFAULT 0 COMMENT '轮保工位数',
  `count_tech` int(0) NOT NULL COMMENT '技师数量',
  `area_shop` int(0) NOT NULL COMMENT '门店面积',
  `service_type` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务类型 1:轮胎保养 2:美容安装 3:洗车 4:钣钣喷漆',
  `red_shop_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '总部店名',
  `shop_nature` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店性质 1:快修快保,2:汽车厂,3:4S店 ,4:社区服务店',
  `shop_credential` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店资质 1:营业执照, 2:税务登记证, 3:道路运输许可证',
  `shop_contract_target` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '签约对象:1:非大型连锁店,2:非街边三无营业店,3:以证件齐全加盟店为主',
  `corporate_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '法人身份证号',
  `bank_account_code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行账号',
  `bank_account_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户名称',
  `bank_branch` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开户行及支行',
  `remark` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_confirmed` tinyint(0) NOT NULL DEFAULT 1 COMMENT '审核状态 0待审核 1已审核 2未通过',
  `confirm_user` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `confirm_user_phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人手机号',
  `confirm_remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核备注',
  `confir_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '审核时间',
  `creator_Phone` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人手机号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 40 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_log
-- ----------------------------
DROP TABLE IF EXISTS `shop_log`;
CREATE TABLE `shop_log`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `identifier` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '标识符（用来搜索的标识）',
  `type1` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义类型1',
  `type2` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义类型2',
  `filter1` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义过滤1',
  `filter2` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '自定义过滤2',
  `compare_text` varchar(3000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '比对的文本',
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
-- Table structure for shop_performance_config
-- ----------------------------
DROP TABLE IF EXISTS `shop_performance_config`;
CREATE TABLE `shop_performance_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `performance_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '绩效类型(0默认， 1单品，2小品类)',
  `type_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类型参数（多个用逗号隔开）',
  `config_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '配置类型(比例 1/金额2)',
  `config_point` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '比例/金额',
  `config_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '配置启用时间(备用字段)',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_point
-- ----------------------------
DROP TABLE IF EXISTS `shop_point`;
CREATE TABLE `shop_point`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `current_point` int(0) NOT NULL DEFAULT 0 COMMENT '当前积分',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_point_detail
-- ----------------------------
DROP TABLE IF EXISTS `shop_point_detail`;
CREATE TABLE `shop_point_detail`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `operation_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作描述',
  `point_value` int(0) NOT NULL DEFAULT 0 COMMENT '积分值',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 34 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_relation
-- ----------------------------
DROP TABLE IF EXISTS `shop_relation`;
CREATE TABLE `shop_relation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `company_id` bigint(0) UNSIGNED NOT NULL COMMENT '所属公司Id',
  `shop_id` bigint(0) UNSIGNED NOT NULL COMMENT '门店Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_service
-- ----------------------------
DROP TABLE IF EXISTS `shop_service`;
CREATE TABLE `shop_service`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `base_service_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '基础服务ID',
  `product_id` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '服务PID',
  `cost_price` decimal(10, 2) NOT NULL COMMENT '成本价',
  `sale_price` decimal(10, 2) NOT NULL COMMENT '销售价',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '上下架状态 0下架  1上架',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5329 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_service_area
-- ----------------------------
DROP TABLE IF EXISTS `shop_service_area`;
CREATE TABLE `shop_service_area`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `shop_id` bigint(0) NOT NULL DEFAULT 0,
  `type` int(0) NOT NULL DEFAULT 0 COMMENT '类型：0三级省市区 1公里数',
  `province_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `city_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `district_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `distance` decimal(18, 2) NOT NULL DEFAULT 0.00,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 46 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_service_brand
-- ----------------------------
DROP TABLE IF EXISTS `shop_service_brand`;
CREATE TABLE `shop_service_brand`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL COMMENT '门店编号',
  `brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '专修品牌',
  `brand_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '品牌图片',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 668 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_service_category
-- ----------------------------
DROP TABLE IF EXISTS `shop_service_category`;
CREATE TABLE `shop_service_category`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `category_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '服务大类id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8314 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_service_type
-- ----------------------------
DROP TABLE IF EXISTS `shop_service_type`;
CREATE TABLE `shop_service_type`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店id',
  `service_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务类型',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26197 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_tag
-- ----------------------------
DROP TABLE IF EXISTS `shop_tag`;
CREATE TABLE `shop_tag`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `tag_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标签名',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `remark` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '备注',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_user_relation
-- ----------------------------
DROP TABLE IF EXISTS `shop_user_relation`;
CREATE TABLE `shop_user_relation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `shop_id` int(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `member_tag` int(0) NOT NULL DEFAULT 0 COMMENT '100普通会员 200高级会员 300VIP贵宾',
  `last_arrive_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '最后一次到店Id',
  `last_arrive_time` datetime(3) NULL DEFAULT NULL COMMENT '最后到店时间',
  `last_order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '最后一次下单No（总部汽配用）',
  `last_order_time` datetime(3) NULL DEFAULT NULL COMMENT '最后一次下单时间（总部汽配用）',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道来源(推广人)类型（0未设置 1 B端员工 2 C端用户）',
  `referrer_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型（0未设置 1文章 2海报 3段子 4门店码 5管家码 6商品促销 7自行搜索 8直接转发小程序）',
  `referrer_shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '推荐人门店Id',
  `referrer_user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '推荐人Id，B端员工是EmployId，C端用户是UserId',
  `is_first_order` tinyint(0) NOT NULL DEFAULT 0 COMMENT '用户在门店的首单消费标识(默认0 消费后为1)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6455 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for tech_trip_record
-- ----------------------------
DROP TABLE IF EXISTS `tech_trip_record`;
CREATE TABLE `tech_trip_record`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师Id',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态 0未还车 1已还车',
  `car_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '车辆ID',
  `car_number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌',
  `start_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '开始公里数',
  `end_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '结束公里数',
  `start_mileage_img` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开始里程图片',
  `end_mileage_img` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '结束里程图片',
  `start_oil` int(0) NOT NULL DEFAULT 0 COMMENT '出发油耗 （格）',
  `end_oil` int(0) NOT NULL DEFAULT 0 COMMENT '还车油耗 （格）',
  `start_oil_img` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '出发油耗图片',
  `end_oil_img` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '还车油耗图片',
  `refuelled` decimal(8, 2) NOT NULL DEFAULT 0.00 COMMENT '加油L',
  `start_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '用车时间',
  `return_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '还车时间',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 225 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for technician
-- ----------------------------
DROP TABLE IF EXISTS `technician`;
CREATE TABLE `technician`  (
  `id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '技师id',
  `account_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '账号Id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '姓名',
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱地址',
  `mobile` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '手机号',
  `gender` tinyint(0) NOT NULL DEFAULT 0 COMMENT '性别（0保密 1男 2女）',
  `number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工号',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型；0：公司；1：门店；2：仓库；',
  `organization_id` int(0) UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属单位Id',
  `department_id` int(0) NOT NULL DEFAULT 0 COMMENT '部门id\r\n',
  `level` tinyint(0) NOT NULL DEFAULT 0 COMMENT '职级',
  `level_name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '职级名称',
  `job_id` int(0) NOT NULL DEFAULT 0 COMMENT '职位id\r\n',
  `work_kind_id` int(0) NOT NULL DEFAULT 0 COMMENT '工种id',
  `work_kind_level` tinyint(0) NOT NULL DEFAULT 0 COMMENT '工种级别',
  `identity` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '身份证号码',
  `we_chat` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '微信号',
  `qq` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'QQ号',
  `address` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地址',
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '照片地址',
  `score` decimal(11, 2) NOT NULL DEFAULT 0.00 COMMENT '评分',
  `qualification_certificate` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工证书照片地址，多张以英文 ‘;’ 隔开',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工描述',
  `dimission_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '离职类型；0：自动离职；1：辞退 等等',
  `dimission_cause` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '离职原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0未删除 1已删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `account_id_idx`(`account_id`) USING BTREE,
  INDEX `mobile_idx`(`mobile`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for work_kind
-- ----------------------------
DROP TABLE IF EXISTS `work_kind`;
CREATE TABLE `work_kind`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '工种ID',
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
