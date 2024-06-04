/*
 Navicat Premium Data Transfer

 Source Server         : aerp
 Source Server Type    : MySQL
 Source Server Version : 80033
 Source Host           : db.aerp.com.cn:3306
 Source Schema         : account_authority

 Target Server Type    : MySQL
 Target Server Version : 80033
 File Encoding         : 65001

 Date: 25/09/2023 10:07:34
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account`  (
  `id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '账号id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '账号；目前仅支持手机号登录',
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '登录密码；',
  `password_iteration` int(0) NOT NULL COMMENT '密码加密次数',
  `password_salt` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密码盐',
  `state` tinyint(0) NOT NULL DEFAULT 0 COMMENT '状态是否正常；如账号正常，账号异常，密码锁定等等状态',
  `error_count` tinyint(0) NOT NULL DEFAULT 0 COMMENT '密码错误次数统计；每次登陆成功此字段更新为0',
  `password_update_time` datetime(3) NOT NULL DEFAULT '1900-01-01 00:00:00.000' COMMENT '密码修改时间',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否禁用，可用',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for application
-- ----------------------------
DROP TABLE IF EXISTS `application`;
CREATE TABLE `application`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '应用id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '应用名称',
  `initialism` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '应用名称简称',
  `route` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '应用路由(应用域名)',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '应用的简单描述，前台页面非必填项',
  `sort` int(0) NOT NULL DEFAULT 0 COMMENT '菜单排序',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 112 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for authority
-- ----------------------------
DROP TABLE IF EXISTS `authority`;
CREATE TABLE `authority`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `parent_id` bigint(0) NOT NULL DEFAULT 0 COMMENT '父级权限id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '权限名称',
  `type` tinyint(0) UNSIGNED NOT NULL COMMENT '类型；0：模块；1：页面；2：按钮；',
  `application_id` bigint(0) NOT NULL COMMENT '所属应用Id',
  `route` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '权限路由， 前端路由URL。例如：@/views/accountauthority/application/index',
  `route_parameter` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '路由参数；目前基于前端路由，此参数暂时用不到；',
  `menu_icon` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '菜单图标Unicode或是Url',
  `sort` int(0) NOT NULL DEFAULT 0 COMMENT '菜单排序',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '权限的简单描述，前台页面非必填项',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 404 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for employee_role
-- ----------------------------
DROP TABLE IF EXISTS `employee_role`;
CREATE TABLE `employee_role`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `employee_id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '员工id',
  `role_id` bigint(0) NOT NULL COMMENT '角色id',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2352 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for log_operation
-- ----------------------------
DROP TABLE IF EXISTS `log_operation`;
CREATE TABLE `log_operation`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `log_id` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '日志记录的业务主键id',
  `log_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'C：新增；R：查询；U：更新；D：删除',
  `biz_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'account: 账号业务日志；authority：权限业务日志；等等...',
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
) ENGINE = InnoDB AUTO_INCREMENT = 556 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色名称',
  `type` tinyint(0) UNSIGNED NOT NULL COMMENT '0：公司；1：门店；2：仓库；',
  `organization_id` int(0) NOT NULL COMMENT '所属单位Id',
  `shop_type` tinyint(0) NOT NULL COMMENT '门店类型 0公司 1合作店 2直营店 4上门养车 8认证店 16总部先生',
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '角色的简单描述，前台页面非必填项',
  `features` tinyint(0) NOT NULL COMMENT '门店的0：普通 1：高级 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 128 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for role_authority
-- ----------------------------
DROP TABLE IF EXISTS `role_authority`;
CREATE TABLE `role_authority`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `role_id` bigint(0) NOT NULL COMMENT '角色id',
  `authority_id` bigint(0) NOT NULL COMMENT '权限id',
  `half_check` tinyint(1) NOT NULL DEFAULT 0 COMMENT '标志前端树结构，节点是否真正选中；(为了前端比较方便的渲染权限树，而冗余的一个字段，无任何实际的业务含义)',
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `create_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人',
  `create_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '创建时间',
  `update_by` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '修改人',
  `update_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_role_id`(`role_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23803 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_login_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_login_log`;
CREATE TABLE `sys_login_log`  (
  `id` bigint(0) NOT NULL AUTO_INCREMENT COMMENT '登录日志主键',
  `login_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT 'EmployeeId或是AccountId，切记，一定不能存入登录账号(手机号)',
  `ip_address` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '登录IP地址',
  `login_location` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '登录地点',
  `browser` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '浏览器类型',
  `os` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '操作系统',
  `state` tinyint(0) NOT NULL DEFAULT 0 COMMENT '登录状态:（0: 成功; 1:失败）',
  `login_name_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '记录日志login_name类型，0：EmployeeId，1：AccountId',
  `login_type` tinyint(0) NOT NULL DEFAULT 0 COMMENT '记录登录类型：0：账号密码登录，1：手机验证码登录',
  `comment` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '' COMMENT '登录备注信息',
  `login_time` datetime(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3) COMMENT '访问时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_login_name`(`login_name`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 46320 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '登录日志表' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
