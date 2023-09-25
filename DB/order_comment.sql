/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : order_comment

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:10:07
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for comment
-- ----------------------------
DROP TABLE IF EXISTS `comment`;
CREATE TABLE `comment`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道（0未设置 1总部C端 2总部门店）',
  `order_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '订单类型（0未设置 1轮胎 2保养 3美容）',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `head_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户头像',
  `user_nick_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户昵称',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车辆Id',
  `vehicle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `shop_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '手写评价内容',
  `is_anonymous` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否匿名（客户选择，对技师展示时默认匿名不由此控制）',
  `check_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '审核状态（0待审核 1审核通过 2审核不通过）',
  `check_comment` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核意见',
  `check_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '审核时间',
  `is_top` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否置顶',
  `is_best` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否精华点评',
  `like_num` int(0) NOT NULL DEFAULT 0 COMMENT '被点赞喜欢数',
  `shop_reply_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '门店商家回复类型（0：未回复 1：商家回复 ）',
  `office_type` tinyint(0) NOT NULL COMMENT '客服官方回复（0：客服未回复 1：客服回复）',
  `user_reply_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '用户回复（0：客户未追评 1：客户追评论）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 197 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单点评表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_detail_product
-- ----------------------------
DROP TABLE IF EXISTS `comment_detail_product`;
CREATE TABLE `comment_detail_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` bigint(0) NOT NULL COMMENT '评论Id',
  `order_product_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '订单商品Id',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品Id',
  `product_display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品显示名称',
  `product_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品图片',
  `price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '商品价格',
  `number` int(0) NOT NULL DEFAULT 0 COMMENT '商品数量',
  `unit` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位（个/升/斤等）',
  `score` int(0) NOT NULL DEFAULT 0 COMMENT '五级分值',
  `is_anonymous` tinyint(0) NOT NULL COMMENT '是否匿名',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 232 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单商品点评明细（评价套餐或单品不含服务）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_detail_shop
-- ----------------------------
DROP TABLE IF EXISTS `comment_detail_shop`;
CREATE TABLE `comment_detail_shop`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '评论Id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `shop_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店图片',
  `score` int(0) NOT NULL DEFAULT 0 COMMENT '分值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 190 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单客户点评明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_detail_tech
-- ----------------------------
DROP TABLE IF EXISTS `comment_detail_tech`;
CREATE TABLE `comment_detail_tech`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '评论Id',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师员工Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师名称',
  `tech_level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师级别',
  `tech_head_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师头像',
  `score` int(0) NOT NULL DEFAULT 3 COMMENT '分值',
  `is_anonymous` tinyint(0) NOT NULL DEFAULT 1 COMMENT '是否匿名（对技师评价默认是）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单客户点评明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_image
-- ----------------------------
DROP TABLE IF EXISTS `comment_image`;
CREATE TABLE `comment_image`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '点评Id',
  `relation_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '(1 客户初评 2回评)',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片地址',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 44 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_label_config
-- ----------------------------
DROP TABLE IF EXISTS `comment_label_config`;
CREATE TABLE `comment_label_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_detail_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '评论明细类型（0未设置 1技师 2门店 3商品）',
  `score` int(0) NOT NULL COMMENT '五级分值',
  `label_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标签名称',
  `is_valid` tinyint(0) NOT NULL COMMENT '是否有效',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '点评标签配置表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_label_selected
-- ----------------------------
DROP TABLE IF EXISTS `comment_label_selected`;
CREATE TABLE `comment_label_selected`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `comment_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '评论主表ID',
  `comment_detail_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '评论明细表ID',
  `label_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '标签ID',
  `label_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标签名称',
  `comment_detail_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '评论明细类型（0未设置 1技师 2门店 3商品）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 487 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '点评选中的标签表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_reply
-- ----------------------------
DROP TABLE IF EXISTS `comment_reply`;
CREATE TABLE `comment_reply`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reply_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '回复id',
  `reply_part_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '若为回复，回复方类型（0未设置 1门店商家 2官方客服 3:用户）',
  `reply_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '回复类型（0未设置  1回复点评 2客户追评 3回复追评）',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `shop_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `channel` tinyint(0) NOT NULL DEFAULT 0 COMMENT '渠道（0未设置 1总部C端 2总部门店）',
  `order_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '订单类型（0未设置 1轮胎 2保养 3美容）',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `check_status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '审核状态（0待审核 1审核通过 2审核不通过）',
  `check_comment` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核意见',
  `check_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '审核时间',
  `content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '手写评价内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_reward
-- ----------------------------
DROP TABLE IF EXISTS `comment_reward`;
CREATE TABLE `comment_reward`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '点评Id',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `reward_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '奖励类型（0未设置 1返现 2积分 3鸡蛋 4鹅蛋）',
  `reward_value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '奖励值',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态（0发起奖励 1已奖励 2退款收回）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '点评奖励（审核后发放，精华点评再发放一次）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for comment_reward_rule_config
-- ----------------------------
DROP TABLE IF EXISTS `comment_reward_rule_config`;
CREATE TABLE `comment_reward_rule_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '奖励规则名称',
  `member_level` int(0) NOT NULL DEFAULT 0 COMMENT '会员等级',
  `is_best` tinyint(0) NOT NULL DEFAULT 0 COMMENT '仅精华点评',
  `reward_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '奖励类型（0未设置 1返现 2积分 3鸡蛋 4鹅蛋）',
  `reward_value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '奖励值',
  `is_valid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '点评奖励配置' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
