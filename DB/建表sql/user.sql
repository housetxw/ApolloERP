/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : user

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:15:21
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for member_grade_enum
-- ----------------------------
DROP TABLE IF EXISTS `member_grade_enum`;
CREATE TABLE `member_grade_enum`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `member_grade` int(0) NOT NULL DEFAULT 0 COMMENT '会员等级',
  `description` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '等级显示',
  `start_value` int(0) NOT NULL DEFAULT 0 COMMENT '成长开始值',
  `end_value` int(0) NOT NULL COMMENT '成长结束值',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `member_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '会员等级图片',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_login_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_login_log`;
CREATE TABLE `sys_login_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '登录日志主键',
  `login_name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT 'userid，一定不能存入登录账号(手机号)',
  `ip_address` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '登录IP地址',
  `login_location` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '登录地点',
  `browser` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '浏览器类型',
  `os` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '操作系统',
  `state` tinyint(0) NOT NULL DEFAULT 0 COMMENT '登录状态:（0: 成功; 1:失败）',
  `open_id` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '三方开放id',
  `login_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '记录登录类型：1：微信登录，2：QQ，3：微博，4：手机验证码登录',
  `comment` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '登录备注信息',
  `login_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '访问时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_login_name`(`login_name`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 39 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci COMMENT = '登录日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `user_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户姓名',
  `nick_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户昵称',
  `contact_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '联系姓名',
  `email` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '邮箱',
  `password` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '密码',
  `gender` int(0) NOT NULL DEFAULT 0 COMMENT '性别（0未设置 1男 2女）',
  `birth_day` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '生日',
  `mobile_number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手机号',
  `head_url` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '头像地址url',
  `personal_sign` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '个人签名',
  `contact_address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '通讯地址',
  `id_number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '身份证',
  `state` int(0) NOT NULL DEFAULT 0 COMMENT '是否锁定0正常，1锁定',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道来源，包括: B，C',
  `referrer_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道来源详细类型',
  `referrer_shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '推荐人门店Id',
  `referrer_user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '推荐人Id，包括UserId和EmployeeId\r\n',
  `driver_license_expiry` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '驾驶证到期日',
  `is_deleted` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0 COMMENT '是否可用',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_userTel`(`mobile_number`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_address
-- ----------------------------
DROP TABLE IF EXISTS `user_address`;
CREATE TABLE `user_address`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `address_type` int(0) NOT NULL COMMENT '地址类型',
  `address_tag` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地址名称',
  `user_name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收货人姓名',
  `mobile_number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收货人手机号',
  `phone_number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '收货人座机',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区',
  `address_line` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '街道',
  `province_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '省Id',
  `city_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '市Id',
  `district_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '区Id',
  `default_address` tinyint(0) NOT NULL DEFAULT 0 COMMENT '默认地址',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 133 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_address_tag
-- ----------------------------
DROP TABLE IF EXISTS `user_address_tag`;
CREATE TABLE `user_address_tag`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `tag_id` int(0) NOT NULL COMMENT '标签编号(1,2,3,4,5开始)',
  `tag_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标签名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_authentication
-- ----------------------------
DROP TABLE IF EXISTS `user_authentication`;
CREATE TABLE `user_authentication`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户id',
  `auth_type` int(0) NOT NULL DEFAULT 0 COMMENT '认证类型：默认0 二代身份证',
  `auth_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '认证姓名',
  `auth_id_card` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '认证身份证号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `userId_index`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_bank_card
-- ----------------------------
DROP TABLE IF EXISTS `user_bank_card`;
CREATE TABLE `user_bank_card`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户id',
  `bank_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行id',
  `bank_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行名称',
  `card_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行卡号',
  `card_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '银行卡名称（民生信用卡(银联卡)）',
  `card_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '卡类型（贷记卡）',
  `phone_no` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '预留手机号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `userId_index`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_car
-- ----------------------------
DROP TABLE IF EXISTS `user_car`;
CREATE TABLE `user_car`  (
  `id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车Id',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户Id',
  `nick_name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '昵称',
  `car_number` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `vehicle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `pai_liang` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排量',
  `nian` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '年',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'tid',
  `sales_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `buy_year` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '购买年份',
  `buy_month` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '购买月份',
  `insure_expire_date` datetime(0) NULL DEFAULT NULL COMMENT '保险到期日',
  `total_mileage` int(0) NULL DEFAULT NULL COMMENT '公里数',
  `last_bao_yang_km` int(0) NULL DEFAULT NULL COMMENT '最后一次保养公里数',
  `last_bao_yang_time` datetime(0) NULL DEFAULT NULL COMMENT '最后一次保养时间',
  `vin_code` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车架号',
  `default_car` tinyint(0) NOT NULL DEFAULT 0 COMMENT '默认车型',
  `engine_no` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发动机编号',
  `tire_size_for_single` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮胎尺寸',
  `insurance_company` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保险公司',
  `registration_time` datetime(0) NULL DEFAULT NULL COMMENT '行驶证注册时间',
  `car_no_province` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌所属省份',
  `car_no_city` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌所属城市',
  `data_source` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '数据来源',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_userId`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_car_components
-- ----------------------------
DROP TABLE IF EXISTS `user_car_components`;
CREATE TABLE `user_car_components`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `key_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `display_des` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `rank` int(0) NOT NULL DEFAULT 0,
  `type_des` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `parent_id` bigint(0) NOT NULL DEFAULT 0,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_car_components_result
-- ----------------------------
DROP TABLE IF EXISTS `user_car_components_result`;
CREATE TABLE `user_car_components_result`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `car_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `part_id` bigint(0) NOT NULL DEFAULT 0,
  `result_value` int(0) NOT NULL DEFAULT 0,
  `check_id` bigint(0) NOT NULL DEFAULT 0,
  `category_id` int(0) NOT NULL DEFAULT 0,
  `property_id` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 233 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_car_property
-- ----------------------------
DROP TABLE IF EXISTS `user_car_property`;
CREATE TABLE `user_car_property`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `car_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车id',
  `property_key` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级属性名',
  `property_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级属性值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_expand_shop
-- ----------------------------
DROP TABLE IF EXISTS `user_expand_shop`;
CREATE TABLE `user_expand_shop`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户id',
  `relation_shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '店长用户shopId',
  `recommend_shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '推荐店shopId，每一次分享都需要更新',
  `recommend_user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '推荐店长用户Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `userId_index`(`user_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_point
-- ----------------------------
DROP TABLE IF EXISTS `user_point`;
CREATE TABLE `user_point`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `current_point` int(0) NOT NULL DEFAULT 0 COMMENT '用户当前积分',
  `current_growth_value` int(0) NOT NULL DEFAULT 0 COMMENT '用户当前成长值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5570 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_point_record
-- ----------------------------
DROP TABLE IF EXISTS `user_point_record`;
CREATE TABLE `user_point_record`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `business_type` int(0) NOT NULL DEFAULT 0 COMMENT '业务类型：0 用户积分 1 用户成长',
  `operation_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型',
  `description` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `point_value` int(0) NOT NULL COMMENT '成长值',
  `referrer_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联编号',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15769 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_product_focus
-- ----------------------------
DROP TABLE IF EXISTS `user_product_focus`;
CREATE TABLE `user_product_focus`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品pid',
  `source` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_third
-- ----------------------------
DROP TABLE IF EXISTS `user_third`;
CREATE TABLE `user_third`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对应用户Id',
  `open_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '第三方用户唯一标识',
  `login_type` tinyint(0) NOT NULL COMMENT '1微信，2QQ，3微博',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2010 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
