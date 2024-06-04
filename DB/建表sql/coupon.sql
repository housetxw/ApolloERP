/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : coupon

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:09:01
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for coupon_activity
-- ----------------------------
DROP TABLE IF EXISTS `coupon_activity`;
CREATE TABLE `coupon_activity`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '活动名称',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '兑换码',
  `code_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '活动编码',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态（0未发布 1可领取 2暂停领取 3已作废）',
  `is_new_user_only` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否仅新用户可领取',
  `is_auto_recommend` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否自动推荐（主要用于商品详情是否展示）',
  `max_num_per_user` int(0) NOT NULL DEFAULT 0 COMMENT '单用户最大领取数量（0为未设置）',
  `is_integral_exchange` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否是积分兑换活动（默认0否）',
  `need_integral_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '需要积分类型（0未设置 1鸡蛋 2鹅蛋）',
  `need_integral_num` int(0) NOT NULL DEFAULT 0 COMMENT '需要积分数量',
  `total_num` int(0) NOT NULL DEFAULT 0 COMMENT '发放总数',
  `receive_num` int(0) NOT NULL DEFAULT 0 COMMENT '领取总数',
  `url` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '访问地址',
  `start_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '开始时间',
  `end_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '结束时间',
  `grant_pattern` tinyint(0) NOT NULL DEFAULT 0 COMMENT '发放方式（0未设置 1系统发放-主动促发  2系统发放-被动响应 3人工发放）',
  `group_id` int(0) NOT NULL DEFAULT 0 COMMENT '互斥(用一张 其他作废)',
  `parent_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级活动id，父级领取之后，该活动自动领取',
  `num` int(0) NULL DEFAULT 1 COMMENT '数量',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7123 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券活动表（领取规则）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule`;
CREATE TABLE `coupon_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '优惠券规则名称（满减券¥20满0元可用）',
  `display_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '优惠券显示名称（电池优惠券）',
  `category` tinyint(0) NOT NULL DEFAULT 0 COMMENT '分类（0未设置 1总部优惠券）',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型（0未设置 1满减券 2实付券）',
  `range_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型使用范围（0未设置 1通用券 2线上券 3门店券）',
  `value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '面值',
  `threshold` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '使用级别阈值',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '图片地址',
  `pay_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '支付方式（0未设置 1线上支付 2到店支付）',
  `is_by_shop_region` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按门店所在区域',
  `is_by_shop_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按门店类型',
  `shop_type` int(0) NOT NULL DEFAULT 0 COMMENT '适用门店类型（位或运算结果）',
  `is_by_shop_id` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按门店Id',
  `is_by_product_category` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按产品类目（目前仅支持顶级类目）',
  `is_by_product_brand` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按产品品牌',
  `is_by_pid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否按产品Id',
  `use_rule_desc` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '使用规则描述',
  `valid_start_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '有效期开始类型（0未设置 1领取当天生效 2指定开始日期）',
  `valid_end_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '有效期结束类型（0未设置 1持续指定天数 2指定截止日期）',
  `valid_days` int(0) NOT NULL DEFAULT 0 COMMENT '有效时长天数',
  `earliest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最早使用日期',
  `latest_use_date` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最晚使用日期',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 87 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则表（使用规则）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule_brand
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule_brand`;
CREATE TABLE `coupon_rule_brand`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `brand_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '品牌Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则-品牌' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule_pid
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule_pid`;
CREATE TABLE `coupon_rule_pid`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `pid` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '产品Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 68 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则-产品Id' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule_product_category
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule_product_category`;
CREATE TABLE `coupon_rule_product_category`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `main_category_code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0' COMMENT '顶级类目Code',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 54 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则-产品类型' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule_region
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule_region`;
CREATE TABLE `coupon_rule_region`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `province_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '省Id',
  `province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '省',
  `city_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '市Id',
  `city` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '市',
  `district_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT '' COMMENT '区县Id',
  `district` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '区县',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则-区域' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for coupon_rule_shop_id
-- ----------------------------
DROP TABLE IF EXISTS `coupon_rule_shop_id`;
CREATE TABLE `coupon_rule_shop_id`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '优惠券规则-门店Id' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for decap_award
-- ----------------------------
DROP TABLE IF EXISTS `decap_award`;
CREATE TABLE `decap_award`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'code',
  `amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '金额',
  `status` int(0) NOT NULL DEFAULT 0 COMMENT '领取状态',
  `draw_time` datetime(3) NULL DEFAULT NULL COMMENT '领取时间',
  `draw_open_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取人openId',
  `draw_user_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取人userId',
  `pay_status` int(0) NOT NULL DEFAULT 0 COMMENT '付款状态',
  `pay_time` datetime(3) NULL DEFAULT NULL COMMENT '打款时间',
  `pay_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '打款人',
  `client_channel` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '领取渠道',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  `amunt_back` decimal(18, 0) NULL DEFAULT 0 COMMENT '金额备份',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2019 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gift_coupon_rule
-- ----------------------------
DROP TABLE IF EXISTS `gift_coupon_rule`;
CREATE TABLE `gift_coupon_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规则名称',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '适用渠道：0未设置 1线下渠道 2线上渠道',
  `max_num_per_user` int(0) NOT NULL DEFAULT 0 COMMENT '单用户最大享受次数',
  `threshold` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '阈值，',
  `start_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '规则开始时间',
  `end_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '规则结束时间',
  `actived` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否生效',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gift_coupon_rule_activity
-- ----------------------------
DROP TABLE IF EXISTS `gift_coupon_rule_activity`;
CREATE TABLE `gift_coupon_rule_activity`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `gift_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '赠券规则id',
  `coupon_activity_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '优惠券活动id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gift_coupon_rule_product
-- ----------------------------
DROP TABLE IF EXISTS `gift_coupon_rule_product`;
CREATE TABLE `gift_coupon_rule_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `gift_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '赠券规则id',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for log_operation
-- ----------------------------
DROP TABLE IF EXISTS `log_operation`;
CREATE TABLE `log_operation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `log_id` varchar(1000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '日志记录的业务主键id',
  `log_type` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT 'C：新增；R：查询；U：更新；D：删除',
  `biz_type` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT 'couponactivity: 优惠券业务日志；couponrule：优惠券使用规则业务日志；userconpon：用户优惠券业务日志；等等...',
  `req_param` varchar(5000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '请求参数',
  `operated_before_content` varchar(5000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '操作前内容',
  `operated_after_content` varchar(5000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '操作后内容',
  `operator` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '操作人',
  `comment` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_grant_coupon
-- ----------------------------
DROP TABLE IF EXISTS `shop_grant_coupon`;
CREATE TABLE `shop_grant_coupon`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `activity_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '活动Id',
  `shop_id` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '0' COMMENT '门店Ids',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_coupon
-- ----------------------------
DROP TABLE IF EXISTS `user_coupon`;
CREATE TABLE `user_coupon`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `grant_method` tinyint(0) NOT NULL DEFAULT 0 COMMENT '发放方式（0未设置 1系统自动触发 2运营手动触发 3用户自主领取 4用户积分兑换 5用户优惠码兑换）',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `coupon_activity_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '活动Id',
  `coupon_rule_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '规则Id',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '使用状态（0未使用 1已使用 2已过期3已作废）',
  `start_valid_time` datetime(3) NOT NULL COMMENT '开始有效时间',
  `end_valid_time` datetime(3) NOT NULL COMMENT '截止有效时间',
  `user_ip` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '用户领取设备IP地址',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '发放的门店',
  `grant_user` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '发放的人员',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_coupon_activity_id`(`coupon_activity_id`) USING BTREE,
  INDEX `idx_coupon_rule_id`(`coupon_rule_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5690 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户优惠券表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_gift_coupon
-- ----------------------------
DROP TABLE IF EXISTS `user_gift_coupon`;
CREATE TABLE `user_gift_coupon`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户id',
  `gift_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '赠券规则id',
  `order_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 327 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
