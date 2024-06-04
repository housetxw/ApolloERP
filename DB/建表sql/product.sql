/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : product

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:11:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for bao_yang_antifreeze_setting
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_antifreeze_setting`;
CREATE TABLE `bao_yang_antifreeze_setting`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `freezing_point` int(0) NOT NULL DEFAULT 0 COMMENT '冰点',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `province_ids` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '省Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_category_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_category_config`;
CREATE TABLE `bao_yang_category_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `category_alias` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分类英文代码',
  `category_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目名称',
  `category_simple_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目名称（简称）',
  `package_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养包类型packageType',
  `display_index` int(0) NOT NULL DEFAULT 0 COMMENT '排序显示',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 42 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_config`;
CREATE TABLE `bao_yang_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `config_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配置名称',
  `config` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '配置内容',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_install_fee_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_install_fee_config`;
CREATE TABLE `bao_yang_install_fee_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `service_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务pid',
  `car_min_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '车辆最小指导价',
  `car_max_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '车辆最大指导价',
  `additional_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '加价金额',
  `remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  `is_deleted` tinyint(0) UNSIGNED NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 60 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_install_type_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_install_type_config`;
CREATE TABLE `bao_yang_install_type_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `package_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养项目',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片',
  `install_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '安装方式',
  `install_type_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '安装方式名称',
  `bao_yang_types` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养项目',
  `is_default` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否默认',
  `need_all` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否需要全部',
  `text_format` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文字格式',
  `channel` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '渠道',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 24 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_install_type_vehicle_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_install_type_vehicle_config`;
CREATE TABLE `bao_yang_install_type_vehicle_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `package_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养项目',
  `install_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '安装方式',
  `vehicle_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1160 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_oe_code_map
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_oe_code_map`;
CREATE TABLE `bao_yang_oe_code_map`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `oe_part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'oe号',
  `part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件号',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品品牌',
  `source` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源',
  `part_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件名称',
  `vehicle_brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车型品牌',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_oeCode`(`oe_part_code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_package_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_package_config`;
CREATE TABLE `bao_yang_package_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `package_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'PackageType',
  `display_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名',
  `suggest_tip` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '建议提示',
  `brief_description` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '简要描述',
  `description_link` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述链接url',
  `detail_description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '详细描述',
  `product_type` int(0) NOT NULL DEFAULT 0 COMMENT '适配展示商品类型：0套餐  1单品  2套餐&单品',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片logo',
  `display_index` int(0) NOT NULL DEFAULT 0 COMMENT '显示排序',
  `service_id` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务pid',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 43 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_package_map_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_package_map_config`;
CREATE TABLE `bao_yang_package_map_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `package_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '养护包类型：xby;dby……',
  `bao_yang_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养类型',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 134 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_package_ref
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_package_ref`;
CREATE TABLE `bao_yang_package_ref`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `bao_yang_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养套餐类型',
  `package_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '套餐pid',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6652 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_package_type_relation
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_package_type_relation`;
CREATE TABLE `bao_yang_package_type_relation`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `main_package_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养类型',
  `related_package_types` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '强关联的保养类型',
  `content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '文案解释',
  `is_strong_related` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否强制关联',
  `cancel_content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '取消后提示文案',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 104 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_part_accessory
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_part_accessory`;
CREATE TABLE `bao_yang_part_accessory`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `accessory_name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '辅料名称',
  `volume` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '升数',
  `level` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '等级/防冻液沸点',
  `quantity` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `size` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '大小',
  `interface` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '接口',
  `description` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `viscosity` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '粘度',
  `source` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源',
  `grade` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '机油等级//防冻液冰点',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_tid`(`tid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 44854 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_part_accessory_map
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_part_accessory_map`;
CREATE TABLE `bao_yang_part_accessory_map`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `bao_yang_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养类型',
  `accessory_names` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '辅料名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_part_audit_record
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_part_audit_record`;
CREATE TABLE `bao_yang_part_audit_record`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `part_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '零件名称',
  `original_oe_code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '原始OE号',
  `original_brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '原始品牌',
  `original_part_code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '原始零件号',
  `audit_type` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核类型',
  `audit_status` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核状态',
  `new_oe_code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '新的OE号',
  `new_brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '新的品牌',
  `new_part_code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '新的零件号',
  `audit_user` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `audit_time` datetime(0) NULL DEFAULT NULL COMMENT '审核时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_part_num
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_part_num`;
CREATE TABLE `bao_yang_part_num`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `part_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件类型',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_parts
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_parts`;
CREATE TABLE `bao_yang_parts`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型Tid',
  `part_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件名称',
  `part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件号',
  `oe_part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'OE号',
  `source` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '来源',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品品牌',
  `validated` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否通过验证',
  `validated_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `validated_time` datetime(0) NULL DEFAULT NULL COMMENT '验证时间',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_tid`(`tid`) USING BTREE,
  INDEX `index_partCode`(`part_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 99357 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_parts_map_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_parts_map_config`;
CREATE TABLE `bao_yang_parts_map_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `bao_yang_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'BaoYangType:jv；qv;',
  `part_names` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件类型',
  `ref_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '指向类型',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 48 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_priority_setting
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_priority_setting`;
CREATE TABLE `bao_yang_priority_setting`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `bao_yang_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `priority_field` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '优先级字段',
  `priority_value` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '优先级value',
  `priority` int(0) NOT NULL DEFAULT 0 COMMENT '优先级：1>2>3',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_product_ref
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_product_ref`;
CREATE TABLE `bao_yang_product_ref`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `part_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件类型',
  `part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件号',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3106 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_property_adaptation
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_property_adaptation`;
CREATE TABLE `bao_yang_property_adaptation`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `part_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件类型',
  `part_name_abbr` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件类型code',
  `property` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级属性',
  `property_value` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级属性值',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片地址',
  `oe_part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'oe号',
  `part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '配件号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_property_description
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_property_description`;
CREATE TABLE `bao_yang_property_description`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `en_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `content` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 29 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bao_yang_type_config
-- ----------------------------
DROP TABLE IF EXISTS `bao_yang_type_config`;
CREATE TABLE `bao_yang_type_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `bao_yang_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '保养类型BaoYangType',
  `display_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名称',
  `category_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品类目',
  `child_categories` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '最小商品类目',
  `type_group` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '组别',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '图片url',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新人',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 112 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_advertisement
-- ----------------------------
DROP TABLE IF EXISTS `config_advertisement`;
CREATE TABLE `config_advertisement`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动code',
  `title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动标题',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '展示图url',
  `begin_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '详图url',
  `btn_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮图url',
  `btn2_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮图url2',
  `btn3_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮图url3',
  `end_image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '尾图url',
  `route_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '跳转路由url',
  `goto_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮跳转url',
  `goto2_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮跳转url2',
  `goto3_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '按钮跳转url3',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '类型：区分Top头部/Bottom底部…',
  `start_time` datetime(0) NOT NULL COMMENT '活动开始时间',
  `end_time` datetime(0) NOT NULL COMMENT '活动结束时间',
  `terminal_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '终端类型：ShopApp管家/CbJApplet门店小程序/YcJApplet客户小程序/QpJApplet汽配',
  `is_online` tinyint(0) NOT NULL DEFAULT 0 COMMENT '上下架状态',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `extend_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '扩展id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_front_category
-- ----------------------------
DROP TABLE IF EXISTS `config_front_category`;
CREATE TABLE `config_front_category`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `category` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分类',
  `display_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '展示图url',
  `route_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '跳转路由url',
  `sub_title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '二级标题',
  `parent_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级Id',
  `terminal_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '终端类型：ShopApp管家/CbJApplet门店小程序/YcJApplet客户小程序/QpJApplet汽配',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `version` int(0) NOT NULL DEFAULT 0 COMMENT '版本',
  `extend_param` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '扩展参数',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 21 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_front_category_product
-- ----------------------------
DROP TABLE IF EXISTS `config_front_category_product`;
CREATE TABLE `config_front_category_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `category_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '分类id',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '对应产品/类目/品牌等id',
  `product_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '关联类型：product/category/brand',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_hot_product
-- ----------------------------
DROP TABLE IF EXISTS `config_hot_product`;
CREATE TABLE `config_hot_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `terminal_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '终端类型',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_package_card_product
-- ----------------------------
DROP TABLE IF EXISTS `config_package_card_product`;
CREATE TABLE `config_package_card_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '套餐卡pid',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '类型：保养  美容 洗车等',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 53 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config_shop_package_card
-- ----------------------------
DROP TABLE IF EXISTS `config_shop_package_card`;
CREATE TABLE `config_shop_package_card`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `card_id` bigint(0) NOT NULL DEFAULT 0,
  `shop_ids` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'shopIds集合，分割',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 45 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_attributemame
-- ----------------------------
DROP TABLE IF EXISTS `dim_attributemame`;
CREATE TABLE `dim_attributemame`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `attribute_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '属性名称',
  `display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名称',
  `Description` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `control_type` int(0) NOT NULL DEFAULT 0 COMMENT '控件类型（0 文本框  1 radio  2 单选  3 多选 ）',
  `data_type` int(0) NOT NULL DEFAULT 0 COMMENT '数据类型 0 文本 1 数值 ',
  `min_value` int(0) NULL DEFAULT NULL COMMENT '最小Int值',
  `max_value` int(0) NULL DEFAULT NULL COMMENT '最大Int值',
  `max_length` int(0) NULL DEFAULT NULL COMMENT '字符串长度',
  `is_required` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否必填',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  `attribute_type` int(0) NOT NULL COMMENT '属性类型 0 通用属性 1 普通属性  2 销售属性 ',
  `sort` int(0) NULL DEFAULT NULL COMMENT '显示顺序',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 74 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '属性名称表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_attributevalue
-- ----------------------------
DROP TABLE IF EXISTS `dim_attributevalue`;
CREATE TABLE `dim_attributevalue`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `attribute_name_id` int(0) NOT NULL DEFAULT 0 COMMENT '属性名称ID',
  `attribute_value` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '属性值',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  `is_deleted` tinyint(0) NOT NULL COMMENT '标记删除',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '属性值表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_brand
-- ----------------------------
DROP TABLE IF EXISTS `dim_brand`;
CREATE TABLE `dim_brand`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `brand_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌名称',
  `brand_img` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌logo地址',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 81 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '品牌表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_category
-- ----------------------------
DROP TABLE IF EXISTS `dim_category`;
CREATE TABLE `dim_category`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` int(0) NOT NULL DEFAULT 0 COMMENT '门店ID',
  `category_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目Code',
  `category_code_short` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目Code缩写',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名',
  `description` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `parent_id` int(0) NOT NULL DEFAULT 0 COMMENT '父级id',
  `category_Type` int(0) NOT NULL DEFAULT 0 COMMENT '类型 1 后台类目 2 前台类目',
  `category_level` int(0) NULL DEFAULT NULL COMMENT '类型级别',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 347 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '产品类目表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_sequence
-- ----------------------------
DROP TABLE IF EXISTS `dim_sequence`;
CREATE TABLE `dim_sequence`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `seq_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `current_val` int(0) NOT NULL,
  `increment_val` int(0) NOT NULL DEFAULT 1,
  `category_id` int(0) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 191 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_shop_bisicservice
-- ----------------------------
DROP TABLE IF EXISTS `dim_shop_bisicservice`;
CREATE TABLE `dim_shop_bisicservice`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '类目Id',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品显示名称',
  `description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '产品描述',
  `standard_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '市场价格',
  `sales_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售价',
  `icon` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '缩率图',
  `remark` varchar(600) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店基础服务表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dim_unit
-- ----------------------------
DROP TABLE IF EXISTS `dim_unit`;
CREATE TABLE `dim_unit`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `unit_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位名称',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 30 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '单位定义表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for door_product
-- ----------------------------
DROP TABLE IF EXISTS `door_product`;
CREATE TABLE `door_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '产品类目Id',
  `free_door_fee` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否包上门费',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fct_associateproduct
-- ----------------------------
DROP TABLE IF EXISTS `fct_associateproduct`;
CREATE TABLE `fct_associateproduct`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `group_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分组名称',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '类目Id',
  `attributes` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分组属性值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '关联产品主表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fct_product
-- ----------------------------
DROP TABLE IF EXISTS `fct_product`;
CREATE TABLE `fct_product`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `class_type` int(0) NOT NULL COMMENT '类别（ 2子产品，4 父产品）',
  `parent_id` int(0) NOT NULL DEFAULT 0 COMMENT ' 父产品的Id',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT ' 产品编码',
  `name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品显示名称',
  `advertisement` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '促销语',
  `brand` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '产品描述',
  `tax_rate` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT ' 税率',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '关联类目Id',
  `standard_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '市场价格',
  `sales_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售价',
  `wholesale_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '批发价',
  `return_cash` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '返现价',
  `settlement_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '门店结算价',
  `unit` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT ' 单位',
  `length` decimal(18, 2) NULL DEFAULT NULL COMMENT ' 长',
  `width` decimal(18, 2) NULL DEFAULT NULL COMMENT ' 宽',
  `height` decimal(18, 2) NULL DEFAULT NULL COMMENT ' 高',
  `weight` decimal(18, 2) NULL DEFAULT NULL COMMENT ' 产品重量',
  `color` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT ' 颜色',
  `image1` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '主图连接',
  `image2` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '图片链接2',
  `image3` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '图片链接3',
  `image4` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '图片链接4',
  `image5` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '图片链接5',
  `part_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '通用 - 标准配件号',
  `part_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '通用 - 配件编号 （用于保养品）',
  `product_refer` int(0) NULL DEFAULT 0 COMMENT ' 推荐位置（填写>=1的整数，0为不推荐），用于搜索结果',
  `product_attribute` tinyint(0) NOT NULL DEFAULT 0 COMMENT ' 产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品',
  `on_sale` tinyint(0) NOT NULL DEFAULT 0 COMMENT ' 上下架',
  `stockout` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否缺货',
  `is_show` tinyint(0) NULL DEFAULT 1 COMMENT ' 是否显示  -  0 否 1 是 用于控制产品是否在搜索及产品列表页显示。',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `is_storeoutside` tinyint(0) NULL DEFAULT 0 COMMENT '是否门店外采商品 1 是 0 否 默认 0',
  `shop_number` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '门店编号',
  `is_retail` tinyint(0) NULL DEFAULT 0 COMMENT ' 是否零售 0 否 1 是',
  `is_insurance` tinyint(0) NULL DEFAULT 0 COMMENT ' 是否有保险 0 否 1 是',
  `remark` varchar(600) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `score` decimal(18, 2) NULL DEFAULT 0.00 COMMENT '评分',
  `sales` int(0) NULL DEFAULT 0 COMMENT ' 销量',
  `favorable_rate` decimal(18, 2) NULL DEFAULT 0.00 COMMENT '好评率',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `productCode_index`(`product_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8527 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fct_shop_product
-- ----------------------------
DROP TABLE IF EXISTS `fct_shop_product`;
CREATE TABLE `fct_shop_product`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `shop_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '门店Id',
  `category_id` int(0) NOT NULL DEFAULT 0 COMMENT '类目Id',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT ' 产品编码',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `display_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品显示名称',
  `description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '产品描述',
  `standard_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '市场价格',
  `sales_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '销售价',
  `discount_rate` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '折扣率',
  `sort_type` int(0) NOT NULL DEFAULT 0 COMMENT '1 置顶  2 按照上架时间顺序',
  `is_top` int(0) NOT NULL DEFAULT 0 COMMENT '是否置顶',
  `on_sale` int(0) NOT NULL DEFAULT 0 COMMENT '上下架',
  `on_sale_time` datetime(0) NULL DEFAULT NULL COMMENT '上架时间',
  `icon` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '缩率图',
  `remark` varchar(600) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识 0否 1是',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  `ref_pid` int(0) NULL DEFAULT 0 COMMENT '引用服务PId',
  `detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '产品描述',
  `image_url` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '图片Url',
  `apply_time` datetime(0) NULL DEFAULT NULL COMMENT '申请时间',
  `audit_time` datetime(0) NULL DEFAULT NULL COMMENT '审核时间',
  `audit_status` tinyint(0) NULL DEFAULT 0 COMMENT '审核状态 1 通过 2 审核中 3驳回',
  `audit_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '审核人',
  `audit_opinion` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '审核意见',
  `is_storeoutside` int(0) NULL DEFAULT 0 COMMENT '是否门店外采商品 1 是 0 否 默认 0',
  `specification` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '型号规格',
  `unit` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '单位',
  `oe_number` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '原厂编号',
  `purchase_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购单价',
  `purchase_cost` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '采购成本价格',
  `is_disabled` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否禁用',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `product_code`(`product_code`) USING BTREE,
  INDEX `product_name`(`product_name`) USING BTREE,
  INDEX `display_name`(`display_name`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4304 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '门店商品表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fct_shopproduct_auditlog
-- ----------------------------
DROP TABLE IF EXISTS `fct_shopproduct_auditlog`;
CREATE TABLE `fct_shopproduct_auditlog`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `audit_user` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `audit_status` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '审核状态 通过 拒绝',
  `audit_opinion` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '审核意见',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 38 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '门店商品上下架审核日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for flash_sale_config
-- ----------------------------
DROP TABLE IF EXISTS `flash_sale_config`;
CREATE TABLE `flash_sale_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `active_start_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '有效开始时间',
  `active_end_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '有效结束时间',
  `product_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品名称',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品编号',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '初始数量',
  `sale_num` int(0) NOT NULL DEFAULT 0 COMMENT '已售数量',
  `price` decimal(18, 2) NOT NULL COMMENT '价格',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '状态(待审核,已生效,已结束,已取消)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for groupon_product_config
-- ----------------------------
DROP TABLE IF EXISTS `groupon_product_config`;
CREATE TABLE `groupon_product_config`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `min_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '最小价格',
  `max_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '最大价格',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for product_security_code
-- ----------------------------
DROP TABLE IF EXISTS `product_security_code`;
CREATE TABLE `product_security_code`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT,
  `security_code` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `route_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `search_count` int(0) NOT NULL DEFAULT 0,
  `first_search_time` datetime(3) NULL DEFAULT NULL,
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `status` int(0) NOT NULL DEFAULT 0,
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `security_code_index`(`security_code`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22015 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for promotion_activity
-- ----------------------------
DROP TABLE IF EXISTS `promotion_activity`;
CREATE TABLE `promotion_activity`  (
  `id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动Id',
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '活动名称',
  `subtitle` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '副标题-广告语',
  `description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '活动描述',
  `thumb_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '缩略图Url',
  `activity_type` int(0) NOT NULL DEFAULT 0 COMMENT '活动类型（0门店商品促销）',
  `promotion_type` int(0) NOT NULL DEFAULT 0 COMMENT '促销类型（0自定义价格 1打折）',
  `label` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '标签展示',
  `start_time` datetime(0) NOT NULL COMMENT '活动开始时间',
  `end_time` datetime(0) NOT NULL COMMENT '活动结束时间',
  `apply_channel` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '适用渠道',
  `audit_status` int(0) NOT NULL DEFAULT 0 COMMENT '审核状态（0待审核 1已审核 2已拒绝 3已结束4已售完）',
  `audit_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核人',
  `audit_remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '审核备注',
  `audit_time` datetime(0) NULL DEFAULT NULL COMMENT '审核时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '最后更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '最后更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for promotion_activity_order
-- ----------------------------
DROP TABLE IF EXISTS `promotion_activity_order`;
CREATE TABLE `promotion_activity_order`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `activity_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动Id',
  `order_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '订单号',
  `user_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '用户Id',
  `product_code` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '商品Pid',
  `num` int(0) NOT NULL DEFAULT 0 COMMENT '数量',
  `order_status` int(0) NOT NULL DEFAULT 0 COMMENT '订单状态0 有效  1取消',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for promotion_activity_product
-- ----------------------------
DROP TABLE IF EXISTS `promotion_activity_product`;
CREATE TABLE `promotion_activity_product`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `activity_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动Id',
  `product_code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品Id',
  `product_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品名称',
  `promotion_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '促销价',
  `limit_quantity` int(0) NOT NULL COMMENT '限购数量',
  `sold_quantity` int(0) NOT NULL COMMENT '已售数量',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 52 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for promotion_activity_shop
-- ----------------------------
DROP TABLE IF EXISTS `promotion_activity_shop`;
CREATE TABLE `promotion_activity_shop`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `activity_id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '活动Id',
  `shop_id` bigint(0) NOT NULL COMMENT '门店Id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除标记',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 52 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rel_associateproduct_detail
-- ----------------------------
DROP TABLE IF EXISTS `rel_associateproduct_detail`;
CREATE TABLE `rel_associateproduct_detail`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `group_id` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '分组名称',
  `product_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品编码',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '关联产品明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rel_category_attribute
-- ----------------------------
DROP TABLE IF EXISTS `rel_category_attribute`;
CREATE TABLE `rel_category_attribute`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `category_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目id',
  `attribute_mame_id` int(0) NOT NULL DEFAULT 0 COMMENT '父级id',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 438 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '属性关联类目表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rel_product_attributevalue
-- ----------------------------
DROP TABLE IF EXISTS `rel_product_attributevalue`;
CREATE TABLE `rel_product_attributevalue`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品id',
  `attribute_name_id` int(0) NOT NULL DEFAULT 0 COMMENT '属性名id',
  `product_attribute_value` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品属性值',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9797 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商品属性值表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rel_product_installservice
-- ----------------------------
DROP TABLE IF EXISTS `rel_product_installservice`;
CREATE TABLE `rel_product_installservice`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '安装方式（例如：添加防冻液/更换防冻液）',
  `product_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '商品id',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '产品pid',
  `service_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务id',
  `service_name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '服务名称',
  `service_price` decimal(18, 2) NOT NULL COMMENT '服务价格',
  `sort` int(0) NOT NULL COMMENT '顺序',
  `free_install` tinyint(0) NOT NULL DEFAULT 0 COMMENT '是否免安装',
  `change_num` tinyint(0) NOT NULL DEFAULT 0 COMMENT '安装服务数量是否随产品数量改变',
  `is_default` tinyint(0) NOT NULL DEFAULT 1 COMMENT '是否默认',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 166 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商品安装服务' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rel_product_package
-- ----------------------------
DROP TABLE IF EXISTS `rel_product_package`;
CREATE TABLE `rel_product_package`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `package_pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '套餐pid',
  `project_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '项目Id',
  `project_Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '项目Id',
  `project_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '项目类型 1 产品PId 2 配件 （机油滤芯，燃油滤芯，汽油滤芯） ',
  `project_brand` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目品牌',
  `standard_price` decimal(18, 2) NOT NULL COMMENT '基准价格',
  `sales_price` decimal(18, 2) NOT NULL COMMENT '销售价',
  `quantity` int(0) NOT NULL COMMENT '产品数量',
  `sort` int(0) NOT NULL COMMENT '排序',
  `replace_product` tinyint(0) NOT NULL DEFAULT 0 COMMENT '能否替换产品',
  `category_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '替换产品类目',
  `shop_category_id` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '替换外采产品类目',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 925 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商品套装明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for service_type_enum
-- ----------------------------
DROP TABLE IF EXISTS `service_type_enum`;
CREATE TABLE `service_type_enum`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `service_type` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '服务类型',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '显示名称',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'icon图片地址',
  `route_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前端跳转路由',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '描述',
  `category_ids` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目ID集合',
  `display_index` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `deposit_amount` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '押金额度限制',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for tire_category_config
-- ----------------------------
DROP TABLE IF EXISTS `tire_category_config`;
CREATE TABLE `tire_category_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `category_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `category_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '类目Id',
  `display_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `brief_description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `image_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `product_type` int(0) NOT NULL DEFAULT 0 COMMENT '适配展示商品类型：0套餐  1单品  2套餐&单品',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0,
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_brand
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_brand`;
CREATE TABLE `vehicle_brand`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌：A - 奥迪',
  `brand_prefix` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌首字母',
  `brand_suffix` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌中文名',
  `rank` int(0) NOT NULL DEFAULT 0 COMMENT '排序',
  `hot_level` int(0) NOT NULL DEFAULT 0 COMMENT '热门程度：大于0 是热门车牌',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 234 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_fuel_type_config
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_fuel_type_config`;
CREATE TABLE `vehicle_fuel_type_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `fuel_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '机油类别',
  `not_supported_types` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '不支持的packageType',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_type
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_type`;
CREATE TABLE `vehicle_type`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系Id',
  `vehicle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `tires` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮胎尺寸;分割',
  `tires_match` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮胎匹配',
  `brand_category` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '德系;意系;日系……',
  `vehicle_body_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '大型车;中型车;',
  `avg_price` decimal(18, 2) NOT NULL DEFAULT 0.00 COMMENT '均价',
  `max_price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '最高价',
  `min_price` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '最低价',
  `vehicle_level` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系级别',
  `service_price_level` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '四轮定位服务等级',
  `factory_rank` int(0) NOT NULL DEFAULT 0 COMMENT '品牌下的厂商排序',
  `vehicle_rank` int(0) NOT NULL DEFAULT 0 COMMENT '厂商下的车型排序',
  `brand_py` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌拼音',
  `brand_jpy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌简音',
  `vehicle_py` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系拼音',
  `vehicle_jpy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系简音',
  `actived` int(0) NOT NULL DEFAULT 0 COMMENT '是否生效',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_vehicleId`(`vehicle_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 79797 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_type_timing
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_type_timing`;
CREATE TABLE `vehicle_type_timing`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'tid',
  `vehicle_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系id',
  `factory` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '厂商',
  `brand` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '品牌',
  `vehicle_series` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '系列',
  `vehicle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系',
  `pai_liang` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '排量',
  `sales_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型',
  `nian` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '年款',
  `vehicle_level` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系级别：中型车，紧凑型SUV，紧凑型车……',
  `vehicle_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车系类型：三厢车，SUV，两厢车，三厢车……',
  `listed_month` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '上市月份',
  `listed_year` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '上市年份',
  `manufacture_year` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '生产年份',
  `production_status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '生产状况',
  `emission_standard` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '排气标准：国Ⅴ，国Ⅳ（国Ⅴ），欧Ⅵ……',
  `stop_production_year` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '停产年份',
  `country` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '品牌所属国家',
  `sales_status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '销售状态：在销，停销',
  `joint_venture` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '国产;合资;进口',
  `fuel_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '机油类别：汽油，柴油，',
  `guide_price` decimal(18, 2) NULL DEFAULT NULL COMMENT '指导价',
  `min_price` decimal(18, 2) NULL DEFAULT NULL COMMENT '最小价',
  `avg_price` decimal(18, 2) NULL DEFAULT NULL COMMENT '均价',
  `max_price` decimal(18, 2) NULL DEFAULT NULL COMMENT '最大价',
  `cylinder_number` int(0) NULL DEFAULT NULL COMMENT '气缸数量',
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '状态',
  `factory_code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '厂商代码',
  `factory_en` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '厂商英文',
  `vehicle_en` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '车系英文',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_tid`(`tid`) USING BTREE,
  INDEX `index_vehicleId`(`vehicle_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 46544 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_type_timing_config
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_type_timing_config`;
CREATE TABLE `vehicle_type_timing_config`  (
  `id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '款型Id',
  `car_body` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '车身结构：4门5座三厢车',
  `engine_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发动机编号',
  `chassis_no` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '底盘编号',
  `transmission_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '变速箱类型（自动 or 手动）',
  `transmission_type_e` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '变速箱类型(英文)',
  `transmission_type_c` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '变速箱类型(中文)',
  `power_kw` decimal(9, 1) NULL DEFAULT NULL COMMENT '功率 单位KW',
  `valve_num_per_cylinder` int(0) NULL DEFAULT NULL COMMENT '气缸数',
  `air_intake_mode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '进气形式',
  `drive_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '驱动方式',
  `steering_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '助力转向类型',
  `front_brake_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前制动器类型',
  `back_brake_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '后制动器类型',
  `halogen_lamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '卤素灯',
  `xenon_lamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '氙灯',
  `led_lamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'LED灯',
  `fuel_filter_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '燃油滤类型',
  `front_tire_size` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前胎规格',
  `rear_tire_size` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '后胎规格',
  `oil_supply_mode` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '供油方式',
  `alloy_wheel` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '铝合金轮毂',
  `tire_monitor_system` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '胎压监测装置',
  `auto_start_stop_sys` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '发动机启停技术',
  `front_suspension_type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '前悬架类型',
  `rear_suspension_type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '后悬架类型',
  `high_beam_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '远光灯类型',
  `dipped_headlight_type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '近光灯类型',
  `transmission` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '变速箱型号',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_tid`(`tid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 47640 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_type_timing_id_map
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_type_timing_id_map`;
CREATE TABLE `vehicle_type_timing_id_map`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '五级车型tid',
  `external_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '扩展Id',
  `source` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '来源',
  `create_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '创建时间',
  `update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '更新时间',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '删除',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `extendId_index`(`external_id`) USING BTREE,
  INDEX `tid_index`(`tid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 157238 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for vehicle_type_timing_tires_match
-- ----------------------------
DROP TABLE IF EXISTS `vehicle_type_timing_tires_match`;
CREATE TABLE `vehicle_type_timing_tires_match`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `tid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '五级车型tid',
  `pid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '轮胎pid',
  `no_product_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '原配轮胎产品(无产品)',
  `is_deleted` tinyint(0) NOT NULL DEFAULT 0 COMMENT '标记删除',
  `create_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '创建人',
  `create_time` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_by` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '更新人',
  `update_time` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_tid`(`tid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
