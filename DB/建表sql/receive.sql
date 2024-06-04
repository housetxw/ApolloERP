/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : receive

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:12:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for quque_number_generator
-- ----------------------------
DROP TABLE IF EXISTS `quque_number_generator`;
CREATE TABLE `quque_number_generator`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `generator_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '发号类型（0：未设置 1：预约 2：到店）',
  `generator_date` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '发号器发号的日期',
  `current_number` int(0) NOT NULL DEFAULT 0 COMMENT '当前发号的号码',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1920 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for quque_number_generator_copy
-- ----------------------------
DROP TABLE IF EXISTS `quque_number_generator_copy`;
CREATE TABLE `quque_number_generator_copy`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `generator_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '发号类型（0：未设置 1：预约 2：到店）',
  `generator_date` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '发号器发号的日期',
  `current_number` int(0) NOT NULL DEFAULT 0 COMMENT '当前发号的号码',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 119 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reserve_picture
-- ----------------------------
DROP TABLE IF EXISTS `reserve_picture`;
CREATE TABLE `reserve_picture`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `reserve_id` bigint(0) NOT NULL DEFAULT 0,
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `created_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `created_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `updated_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `updated_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 96 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reserve_receive_relation
-- ----------------------------
DROP TABLE IF EXISTS `reserve_receive_relation`;
CREATE TABLE `reserve_receive_relation`  (
  `id` bigint(0) NOT NULL COMMENT '主键id',
  `reserve_id` bigint(0) NOT NULL COMMENT '预约id',
  `receive_id` bigint(0) NOT NULL COMMENT '到店id',
  `status` tinyint(0) NOT NULL COMMENT '到店/逾期  0逾期 1到店 2到店逾期 ',
  `is_valid` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有效 0否 1是',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reserve_track
-- ----------------------------
DROP TABLE IF EXISTS `reserve_track`;
CREATE TABLE `reserve_track`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reserve_id` bigint(0) NOT NULL COMMENT '预约ID',
  `opt_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型',
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标题',
  `content` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 878 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for reserve_track_detail
-- ----------------------------
DROP TABLE IF EXISTS `reserve_track_detail`;
CREATE TABLE `reserve_track_detail`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `log_id` int(0) NOT NULL DEFAULT 0 COMMENT '日志Id',
  `field` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字段',
  `field_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '字段名称',
  `before_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改前',
  `after_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '修改后',
  `created_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `created_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1474 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival`;
CREATE TABLE `shop_arrival`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '到店时间',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态 0等待中 1施工中 2已完结 3 未消费离店',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户Id',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户姓名',
  `user_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户联系方式',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车辆Id',
  `car_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌号',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'tid',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系id',
  `brand` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌（奥迪）',
  `vehicle` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系（A4L)',
  `pai_liang` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排量',
  `nian` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '年款',
  `sales_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `total_mileage` int(0) NOT NULL DEFAULT 0 COMMENT '公里数',
  `car_properties` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `service_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务类型',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `shop_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '门店名称',
  `remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `tech_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '0' COMMENT '技师Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师姓名',
  `level` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '职级',
  `reserve_no` int(0) NOT NULL DEFAULT 0 COMMENT '预约编号',
  `queue_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排队类型（预约排队，到店排队）',
  `queue_number` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排队编号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `cancel_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '未消费离店时间',
  `finish_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '完结时间',
  `dispatch_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '派工时间',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14335 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店到店表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival_cancel
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival_cancel`;
CREATE TABLE `shop_arrival_cancel`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店记录',
  `reason_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '原因类型',
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1372 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival_car_report
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival_car_report`;
CREATE TABLE `shop_arrival_car_report`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店id',
  `car_report_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '车辆检测报告url',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 81 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival_order
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival_order`;
CREATE TABLE `shop_arrival_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `order_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单类型（0未设置 1轮胎 2保养 3美容）',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'Pid',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售单价',
  `num` tinyint(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `order_no`(`order_no`) USING BTREE,
  INDEX `is_deleted`(`is_deleted`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4196 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店到店订单关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival_reason
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival_reason`;
CREATE TABLE `shop_arrival_reason`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_arrival_video
-- ----------------------------
DROP TABLE IF EXISTS `shop_arrival_video`;
CREATE TABLE `shop_arrival_video`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店id',
  `video_path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '视频路径',
  `video_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '名称',
  `duration` bigint(0) NOT NULL DEFAULT 0 COMMENT '分钟',
  `file_size` bigint(0) NULL DEFAULT 0 COMMENT '存储空间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 150 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_check_property
-- ----------------------------
DROP TABLE IF EXISTS `shop_check_property`;
CREATE TABLE `shop_check_property`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `key_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `display_des` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `rank` decimal(18, 2) NOT NULL DEFAULT 0.00,
  `category_id` int(0) NOT NULL DEFAULT 0,
  `condition` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `parent_id` bigint(0) NOT NULL DEFAULT 0,
  `version_num` int(0) NOT NULL DEFAULT 0,
  `function_des` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `is_check_item_main` tinyint(0) NOT NULL DEFAULT 0,
  `is_require` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否必填',
  `car_components` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对应车辆部位信息',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 139 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_check_result
-- ----------------------------
DROP TABLE IF EXISTS `shop_check_result`;
CREATE TABLE `shop_check_result`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `check_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查报告Id',
  `property_id` bigint(0) NOT NULL COMMENT '检查项目Id',
  `property_type` int(0) NOT NULL DEFAULT 0 COMMENT '属性值类型：0检查项值  1检查结果值',
  `text_value` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文本值',
  `numeric_value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '数值值',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '分类Id',
  `submit_batch_id` bigint(0) NOT NULL COMMENT '提交批次Id',
  `result_words_json` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '结果词Json串',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9626 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_check_result_image
-- ----------------------------
DROP TABLE IF EXISTS `shop_check_result_image`;
CREATE TABLE `shop_check_result_image`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `check_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查报告Id',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '分类Id',
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片连接',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `property_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查项目Id',
  `image_type` int(0) NOT NULL DEFAULT 1 COMMENT '图片类型0正常 1异常',
  `submit_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '提交批次Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 871 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive`;
CREATE TABLE `shop_receive`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `arrival_time` datetime(3) NOT NULL DEFAULT '1990-01-01 00:00:00.000' COMMENT '到店时间',
  `departure_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '离店时间',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态 0等待中 1施工中 2已完结 3 未消费离店',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户Id',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户姓名',
  `user_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户联系方式',
  `user_sex` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户性别',
  `is_new` tinyint(0) NOT NULL COMMENT '是否是新用户',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车辆Id',
  `vin_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'VIN码',
  `car_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `station_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '工位号',
  `tech_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '技师Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师姓名',
  `remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店到店表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_check
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_check`;
CREATE TABLE `shop_receive_check`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `receive_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店记录Id',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `person_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '员工Id',
  `person_name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工姓名',
  `check_status` int(0) NOT NULL DEFAULT 0 COMMENT '检查状态0待提交 1已提交',
  `narration` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户描述',
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `user_tel` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户手机号',
  `user_name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户姓名',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车辆Id',
  `car_plate` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌',
  `mileage` int(0) NOT NULL DEFAULT 0 COMMENT '公里数',
  `vin_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'VIN码',
  `dashboard_img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '仪表盘',
  `technician_signature` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师签字',
  `zhijian_signature` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '质检签字',
  `customer_signature` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '客户签字',
  `submit_channel` int(0) NOT NULL DEFAULT 0 COMMENT '提交渠道0管家 1小程序',
  `version_num` int(0) NOT NULL DEFAULT 1 COMMENT '版本号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 821 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_check_log
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_check_log`;
CREATE TABLE `shop_receive_check_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `check_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查Id',
  `check_module_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '检查项Code',
  `check_module` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '检查项',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '类型id',
  `error_count` int(0) NOT NULL DEFAULT 0 COMMENT '异常数量',
  `normal_count` int(0) NOT NULL DEFAULT 0 COMMENT '正常数量',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11220 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_check_report
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_check_report`;
CREATE TABLE `shop_receive_check_report`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `check_id` bigint(0) NOT NULL DEFAULT 0,
  `category_id` int(0) NOT NULL DEFAULT 0,
  `receive_id` bigint(0) NOT NULL DEFAULT 0,
  `normal_count` int(0) NOT NULL DEFAULT 0,
  `abnormal_count` int(0) NOT NULL DEFAULT 0,
  `uncheck_count` int(0) NOT NULL DEFAULT 0,
  `version_num` int(0) NOT NULL DEFAULT 0,
  `mobile_summary` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 609 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_check_result_word
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_check_result_word`;
CREATE TABLE `shop_receive_check_result_word`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `check_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查报告Id',
  `check_result_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '结果Id',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '分类Id',
  `result_word_id` int(0) NOT NULL DEFAULT 0 COMMENT '结果词Id',
  `submit_batch_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '提交批次Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8388 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_check_sub_item
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_check_sub_item`;
CREATE TABLE `shop_receive_check_sub_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `config_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '配置Id',
  `prefix` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前缀',
  `suffix` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '后缀',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `rank` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '排序',
  `check_type` int(0) NOT NULL DEFAULT 0 COMMENT '0单选  1多选',
  `check_count` int(0) NOT NULL DEFAULT 0 COMMENT 'input 数量',
  `is_compute` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要计算',
  `need_photo` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要拍照',
  `opt_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作类型 input-num,input-txt,checkbox,checkbox-input-num,checkbox-input-txt,checkbox-scancode-battery,image,radio',
  `suggestion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作建议',
  `group_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分组',
  `check_item_main_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查项Id',
  `number_limit` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '数量显示',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 147 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_order
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_order`;
CREATE TABLE `shop_receive_order`  (
  `id` bigint(0) NOT NULL COMMENT '主键',
  `receive_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '到店Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店到店订单关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_result_word
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_result_word`;
CREATE TABLE `shop_receive_result_word`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'code',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'name',
  `value` int(0) NOT NULL DEFAULT 0 COMMENT '值',
  `work_group` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分组',
  `type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类型',
  `rank` tinyint(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_result_word_and_sub_item
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_result_word_and_sub_item`;
CREATE TABLE `shop_receive_result_word_and_sub_item`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `check_sub_item_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查项结果Id',
  `result_word_id` int(0) NOT NULL DEFAULT 0 COMMENT '结果词Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 154 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_result_word_compute_rule
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_result_word_compute_rule`;
CREATE TABLE `shop_receive_result_word_compute_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `rule_id` bigint(0) NOT NULL DEFAULT 0,
  `result_word_id` int(0) NOT NULL DEFAULT 0,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_receive_sub_item_compute_rule
-- ----------------------------
DROP TABLE IF EXISTS `shop_receive_sub_item_compute_rule`;
CREATE TABLE `shop_receive_sub_item_compute_rule`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规则code',
  `ruleDes` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '规则名',
  `min_value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '最小',
  `max_value` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '最大',
  `sub_item_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '检查结果项id',
  `suggestion` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '建议',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_reserve
-- ----------------------------
DROP TABLE IF EXISTS `shop_reserve`;
CREATE TABLE `shop_reserve`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reserve_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '预约到店时间',
  `reserve_no` int(0) NOT NULL DEFAULT 0 COMMENT '预约编号',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `is_wait` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否到店等待',
  `is_any_order` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否有订单预约',
  `channel` tinyint(0) NOT NULL DEFAULT 1 COMMENT '渠道 1小程序 2shop',
  `status` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态 0待确认 1已确认 2已完成 3已取消',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '0到店  1上门',
  `user_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户姓名',
  `user_tel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户联系方式',
  `user_sex` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户性别',
  `car_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车辆Id',
  `vin_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'VIN码',
  `car_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车牌号',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `vehicle` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `vehicle_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `pai_liang` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排量',
  `nian` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '年',
  `sales_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `tid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型Tid',
  `car_logo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车型logo',
  `station_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '工位号',
  `tech_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师Id',
  `tech_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '技师姓名',
  `service_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '预约服务大类',
  `service_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '预约类型名称',
  `service_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务类型',
  `address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '地址',
  `remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '备注',
  `cancel_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '取消人',
  `cancel_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '取消时间',
  `cancel_reason` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '取消原因',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 746 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店预约表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_reserve_order
-- ----------------------------
DROP TABLE IF EXISTS `shop_reserve_order`;
CREATE TABLE `shop_reserve_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `reserve_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '预约Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 555 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店预约订单关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop_reserve_time_config
-- ----------------------------
DROP TABLE IF EXISTS `shop_reserve_time_config`;
CREATE TABLE `shop_reserve_time_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` int(0) NOT NULL DEFAULT 0 COMMENT '门店编号',
  `week_day` int(0) NOT NULL DEFAULT 0 COMMENT '星期',
  `year_day` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '日期',
  `start_time` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '开始时间',
  `end_time` varchar(29) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '结束时间',
  `reserve_count` int(0) NOT NULL DEFAULT 0 COMMENT '可预约数量',
  `config_type` int(0) NOT NULL DEFAULT 0 COMMENT '配置类型（0：默认配置，1：自定义配置）',
  `reserve_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '预约类型（TireMaintenance：轮胎\\\\保养\\\\维修，OtherTemporary：美容\\\\装潢，SheetMetalSprayPainting：钣金喷漆）',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7836 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
