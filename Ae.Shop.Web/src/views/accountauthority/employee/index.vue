<template>
  <main class="container account-authority">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :tbRowDblClick="tbRowDblClick"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item label>
          <el-input
            class="input"
            size="mini"
            v-model="condition.employeeName"
            prefix-icon="el-icon-search"
            placeholder="员工姓名"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-input
            v-model="condition.mobile"
            size="mini"
            placeholder="员工手机"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-select
            v-model="condition.isDeleted"
            size="mini"
            placeholder="选择状态"
            style="width: 100px"
          >
            <el-option
              v-for="item in deleteType"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="姓名" prop="name" width="100"></el-table-column>
        <rg-table-column label="手机号码" prop="mobile" />
        <rg-table-column label="性别" prop="gender" align="center">
          <template slot-scope="scope">{{
            scope.row.gender == 0 ? "保密" : scope.row.gender == 1 ? "男" : "女"
          }}</template>
        </rg-table-column>
        <rg-table-column label="工号" prop="number" />
        <!-- <el-table-column label="部门" prop="departmentName" width="120"></el-table-column> -->
        <el-table-column label="岗位工种" prop="jobName" width="200">
          <template slot-scope="scope"
            >{{ scope.row.jobName }}
            {{
              scope.row.workKindName ? " - " + scope.row.workKindName : ""
            }}{{scope.row.level>0?" - " +scope.row.level+"级":""}}
            </template
          >
        </el-table-column>
        <el-table-column label="角色" prop="roleName"></el-table-column>
        <el-table-column
          label="邮箱"
          prop="email"
          width="120"
        ></el-table-column>
        <rg-table-column label="状态" prop="isDeleted" align="center">
          <template slot-scope="scope">{{
            scope.row.isDeleted === false ? "正常" : "删除"
          }}</template>
        </rg-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="100">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)"
                >编辑</el-button
              >
              <!-- <el-button
                type="danger"
                size="mini"
                v-show="scope.row.isDeleted==false"
                @click="deleted(scope.row)"
              >禁用</el-button>
              <el-button
                type="primary"
                size="mini"
                v-show="scope.row.isDeleted"
                @click="auditShop(scope.row)"
              >启用</el-button>-->
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
    </rg-page>
    <!-- 首页 -->
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        v-if="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel('formModel');
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
        v-loading="loading"
      >
        <template v-slot:content>
          <section class="basic_info_section">
            <el-form :model="formModel" :rules="rules" ref="formModel">
              <el-row>
                <el-col :span="10">
                  <el-form-item
                    :label-width="formLabelWidth"
                    label="所属单位"
                    prop="organizationId"
                  >
                    <span style="color: #606266">{{ organizationName }}</span>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-divider content-position="left">基本信息</el-divider>
              </el-row>
              <el-row>
                <el-col :span="16">
                  <el-form-item
                    label="员工姓名"
                    :label-width="formLabelWidth"
                    prop="name"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.name"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="员工姓名"
                      @change="getname"
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="7" style="margin-left: 10px">
                  <el-form-item prop="gender">
                    <el-radio-group v-model="formModel.gender">
                      <el-radio
                        v-for="(item, index) in genderSel"
                        :key="item.value"
                        :label="1 * (index + 1)"
                        :value="item.value"
                        @change="getgender"
                        >{{ item.label }}</el-radio
                      >
                      <!-- <el-radio :label="1">男</el-radio>
                      <el-radio :label="2">女</el-radio>-->
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16">
                  <el-form-item
                    label="手机号码"
                    :label-width="formLabelWidth"
                    prop="mobile"
                  >
                    <el-input
                      size="small"
                      v-model="formModel.mobile"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="员工手机号码"
                      @change="getmobile"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16" style="margin-bottom: 10px">
                  <el-form-item
                    label="工号"
                    :label-width="formLabelWidth"
                    prop="number"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.number"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="员工工号"
                      @change="getnumber"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16">
                  <el-form-item
                    label="身份证"
                    :label-width="formLabelWidth"
                    prop="identity"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.identity"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="身份证号码"
                      @change="getidentity"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16" v-if="showpass">
                  <el-form-item
                    label="登陆密码"
                    :label-width="formLabelWidth"
                    prop="password"
                    :required="!hasAccount"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.password"
                      autocomplete="off"
                      clearable
                      :disabled="hasAccount"
                      placeholder="系统登陆密码"
                      @change="getpassword"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- <el-row style="margin-bottom:10px;"> -->
              <!-- </el-col> -->
              <!-- <el-col :span="8"> -->
              <!-- <el-form-item label="性别" :label-width="formLabelWidth" prop="gender">
                  <el-select
                    v-model="formModel.gender"
                    size="medium"
                    placeholder="请选择性别"
                    :disabled="formCheck"
                    style="width:70%"
                  >
                    <el-option
                      v-for="item in genderSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
              </el-form-item>-->

              <!-- </el-col> -->
              <!-- </el-row> -->

              <el-row>
                <el-col :span="16">
                  <el-form-item
                    label="岗位工种"
                    :label-width="formLabelWidth"
                    prop="jobId"
                  >
                    <!-- <el-select
                  v-model="formModel.jobId"
                  size="medium"
                  placeholder="请选择所属单位"
                  width="100%"
                  :disabled="formCheck"
                >
                  <el-option
                    v-for="item in jobSel"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                    </el-select>-->
                    <el-cascader
                      v-model="formModel.jobId"
                      :options="jobSel"
                      :props="{ expandTrigger: 'hover' }"
                      separator="-"
                      filterable
                      :disabled="formCheck"
                      @change="handleJobChange"
                      style="width: 100%"
                      placeholder="职位"
                    ></el-cascader>
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="8">
              <el-form-item label="角色" :label-width="formLabelWidth" prop="roleIds">
                <el-input size="medium" v-model="formModel.roleIds" autocomplete="off" clearable></el-input>
              </el-form-item>
                </el-col>-->
              </el-row>
              <el-row>
                <el-col :span="16" style="margin-bottom: 10px">
                  <el-form-item
                    label="邮箱"
                    :label-width="formLabelWidth"
                    prop="email"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.email"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="邮箱"
                      @change="getemail"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16" style="margin-bottom: 10px">
                  <el-form-item
                    label="微信号"
                    :label-width="formLabelWidth"
                    prop="weChat"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.weChat"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="微信号"
                      @change="getweChat"
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="6" style="margin-left: 10px">
                  <el-form-item>
                    <el-checkbox v-model="checked" @change="getchecked"
                      >手机号为微信号</el-checkbox
                    >
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="16" style="margin-bottom: 10px">
                  <el-form-item
                    label="QQ"
                    :label-width="formLabelWidth"
                    prop="qq"
                  >
                    <el-input
                      size="medium"
                      v-model="formModel.qq"
                      autocomplete="off"
                      clearable
                      :disabled="formCheck"
                      placeholder="QQ"
                      @change="getqq"
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="备注"
                    :label-width="formLabelWidth"
                    prop="description"
                  >
                    <el-input
                      size="medium"
                      type="textarea"
                      :rows="2"
                      v-model="formModel.description"
                      :disabled="formCheck"
                      style="width: 100%"
                      placeholder="其他备注信息"
                      @change="getdescription"
                      maxlength="300"
                      show-word-limit
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6" style="margin-left: 60px">
                  <el-form-item prop="isDeleted">
                    <el-checkbox
                      v-model="formModel.isDeleted"
                      @change="isDeleted1"
                      >禁止使用系统</el-checkbox
                    >
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </section>

          <section class="role_section">
            <el-row>
              <el-divider content-position="left">角色</el-divider>
            </el-row>
            <el-form>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="系统角色"
                    :label-width="formLabelWidth"
                    prop="roleIds"
                  >
                    <el-col class="line" :span="24">
                      <el-checkbox-group v-model="formModel.roleIds">
                        <!-- <el-col class="line" :span="6"> -->
                        <el-checkbox
                          v-for="item in roledata"
                          :key="item.id"
                          :label="item.id"
                          :value="item.name"
                          @change="checkinlist"
                          >{{ item.name }}</el-checkbox
                        >
                        <!-- </el-col> -->
                      </el-checkbox-group>
                    </el-col>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </section>

          <!-- userInfo.employeeType !== 1 -->
          <section class="range_section" v-if="userInfo.employeeType !== 1">
            <el-divider content-position="left">管辖范围</el-divider>

            <el-table
              ref="companyData"
              v-loading="tableLoading"
              element-loading-text="加载中..."
              :data="companyData"
              stripe
              border
              size="mini"
              style="width: 100%"
              max-height="200px"
              @select="onTableSelect"
              @select-all="onTableSelectall"
              v-if="companyData.length > 0"
            >
              <rg-table-column
                type="selection"
                align="center"
              ></rg-table-column>

              <rg-table-column
                prop="organizationName"
                label="公司名称"
                width="100%"
                align="center"
              ></rg-table-column>

              <rg-table-column prop="roles" label="角色" align="center">
                <template slot-scope="scope">
                  <el-checkbox-group v-model="scope.row.roleId">
                    <el-checkbox
                      v-for="item in scope.row.roles"
                      :label="item.id"
                      :value="item.name"
                      :key="item.id"
                      @change="checkrole(scope.row)"
                      :checked="companychecked"
                      >{{ item.name }}</el-checkbox
                    >
                  </el-checkbox-group>
                </template>
              </rg-table-column>
            </el-table>
            <el-table
              ref="shopData"
              v-loading="tableLoading"
              element-loading-text="加载中..."
              :data="shopData"
              stripe
              border
              size="mini"
              style="width: 100%; margin-top: 20px"
              max-height="200px"
              @select="onshopSelect"
              @select-all="onshopSelectall"
              v-if="shopData.length > 0"
            >
              <rg-table-column
                type="selection"
                align="center"
              ></rg-table-column>
              <rg-table-column
                prop="organizationName"
                label="门店名称"
                width="100%"
                align="center"
              ></rg-table-column>
              <rg-table-column prop="shopList" label="角色" align="center">
                <template slot-scope="scope">
                  <el-checkbox-group v-model="scope.row.roleId">
                    <el-checkbox
                      v-for="item in scope.row.roles"
                      :label="item.id"
                      :value="item.name"
                      :key="item.id"
                      :checked="checked1"
                      @change="checkShopRole(scope.row)"
                      >{{ item.name }}</el-checkbox
                    >
                  </el-checkbox-group>
                </template>

                <!-- <el-form-item  v-for="(item,index) in scope">
    <el-checkbox-group v-model="checkList[index]" @change="checkShopRole()">
                 <el-checkbox v-for="itemChild in item.roles" :label="itemChild.id"    :value="itemChild.name"
                    :key="itemChild.id">
                         {{itemChild.name}}
                 </el-checkbox>
    </el-checkbox-group>
                </el-form-item>-->
              </rg-table-column>
            </el-table>
          </section>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            >保存</el-button
          >
          <el-button
            type="danger"
            size="mini"
            icon="el-icon-delete"
            v-show="deleteShow"
            style="float: left"
            @click="formdeleted(formModel)"
            >删除</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
    <!-- 权限树页面 -->
    <section id="deptTree">
      <el-dialog
        title="部门列表"
        :close-on-click-modal="false"
        :visible.sync="formDeptTreeVisible"
        :before-close="cancelDeptTree"
        top="9vh"
        width="40%"
      >
        <el-input
          placeholder="请输入部门名称"
          size="small"
          v-model="deptFilterText"
          clearable
          style="margin-bottom: 10px"
        ></el-input>
        <el-tree
          :data="deptTreeList"
          show-checkbox
          node-key="id"
          ref="deptTree"
          @node-click="deptNodeChange"
          @check="deptNodeChange"
          :check-strictly="true"
          :default-expanded-keys="defaultExpandedKeys"
          :default-checked-keys="defaultCheckedKeys"
          :props="defaultProps"
          :filter-node-method="deptFilter"
          :check-on-click-node="true"
          :highlight-current="true"
          class="custom-el-dialog-vh"
        ></el-tree>
        <div slot="footer" class="dialog-footer">
          <el-button icon="el-icon-close" @click="cancelDeptTree"
            >取消</el-button
          >
          <el-button
            size="mini"
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveDeptTreeLoading"
            @click="saveDeptTree"
            >保存</el-button
          >
        </div>
      </el-dialog>
    </section>
    <!-- 权限树页面 -->
  </main>
</template>

<script>
import { mapGetters } from "vuex";
import { comBusSvc, empSvc } from "@/api/accountauthority/employee";
import { roleSvc } from "@/api/accountauthority/role";
export default {
  name: "employee",

  data() {
    let validatePass = (rule, value, callback) => {
      // 当活动名称为空值且为必填时，抛出错误，反之通过校验
      if (this.showpass == true && this.hasAccount == false) {
        if (this.formModel.password === "" && this.hasAccount == false) {
          callback(new Error("请输入密码"));
        } else if (this.formModel.password.length < 6) {
          callback(new Error("密码不得小于6位"));
        } else {
          callback();
        }
      } else {
        callback();
      }
    };
    return {
      comArr: [],
      shopArr: [],
      shopsave: true,
      loading: true,
      orgRanges: [],
      accountId: "",
      accountId1: "", //有账号时获取的账号id
      employeeId: "",
      checked: false,
      checked1: false,
      companychecked: false,
      roleId: [],
      showpass: true,
      roleList1: [],
      AllroleList: [], //所有的角色数据
      hasAccount: true, //判断是否有登陆账号
      organizationId: "", //公司id
      organizationName: "", //公司名称
      employeeType: "", //员工类型
      shoprows: [],
      deleteShow: false,
      characterData: [], //公司角色
      companyData: [], //子公司数据
      shoprole: [],
      shopData: [],
      roles: [],
      tableData1: [],
      roledata: [],
      checked: false,
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      type: this.global.employeeSrchType,
      condition: {
        organizationId: undefined,
        organizationName: "",
        type: undefined,
        employeeName: "",
        mobile: undefined,
        isDeleted: undefined,
        pageIndex: 1,
        pageSize: 20,
      },
      tableData: [],
      totalList: 0,

      deleteType: [
        {
          key: "-1",
          value: "全部",
        },
        {
          key: "0",
          value: "正常",
        },
        {
          key: "1",
          value: "删除",
        },
      ],
      organizationSel: [],
      departmentSel: [],
      jobSel: [],
      genderSel: this.global.genderSel,
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formType: this.global.employeeFormType,
      formInit: {
        employeeId: undefined,
        type: 1,
        name: "",
        gender: 1,
        email: "",
        mobile: "",
        identity: "",
        organizationId: undefined,
        departmentId: undefined,
        level: 0,
        workKindId: 0,
        jobId: [],
        number: "",
        roleIds: [],
        weChat: "",
        qq: "",
        description: "",
        password: "",
        isDeleted: false,
      },
      formModel: {
        employeeId: undefined,
        name: "",
        type: 1,
        name: "",
        gender: 1,
        email: "",
        mobile: "",
        identity: "",
        departmentId: undefined,
        level: 0,
        workKindId: 0,
        jobId: [],
        number: "",
        roleIds: [],
        weChat: "",
        qq: "",
        description: "",
        password: "",
        isDeleted: false,
      },
      shopData: {
        organizationId: "",
        organizationName: "",
        type: "",
        roles: [],
        roleId: [],
      },
      companyData: {
        organizationId: "",
        organizationName: "",
        type: "",
        roles: [],
        roleId: [],
      },

      rules: {
        name: [
          { required: true, message: "请输入员工姓名", trigger: "blur" },
          { max: 50, message: "员工姓名长度不允许超过50", trigger: "blur" },
        ],
        password: [{ validator: validatePass }],
        email: [
          // { required: true, message: "请输入邮箱", trigger: "blur" },
          { max: 50, message: "邮箱长度不允许超过50", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入手机号", trigger: "blur" },
          { max: 20, message: "手机号长度不允许超过20", trigger: "blur" },
        ],
        // identity: [
        //   { required: true, message: "请输入身份证号", trigger: "blur" },
        //   { max: 30, message: "身份证号长度不允许超过30", trigger: "blur" }
        // ],
        number: [
          // { required: true, message: "请输入员工工号", trigger: "blur" },
          { max: 10, message: "员工工号长度不允许超过10", trigger: "blur" },
        ],
        weChat: [
          { max: 30, message: "WeChat长度不允许超过30", trigger: "blur" },
        ],
        qq: [{ max: 30, message: "QQ号长度不允许超过30", trigger: "blur" }],
        description: [
          { max: 255, message: "描述长度不允许超过255", trigger: "blur" },
        ],

        // organizationId: [
        //   { required: true, message: "请选择所属单位", trigger: "blur" }
        // ],
        // jobId: [{ required: true, message: "请选择职位", trigger: "blur" }]
        // type: [{ required: true, message: "请选择员工类型", trigger: "blur" }]
      },
      formLabelWidth: "100px",

      originalDeptId: -1,
      formDeptTreeVisible: false,
      btnSaveDeptTreeLoading: false,
      defaultProps: {
        children: "children",
        label: "label",
      },
      defaultExpandedKeys: [],
      defaultCheckedKeys: [],
      deptTreeList: [],
      deptFilterText: "",
    };
  },
  computed: {
    ...mapGetters(["userInfo"]),
  },
  created() {
    this.fetchData();
    console.log(this.genderSel);
    var i = this.genderSel.length;
    while (i--) {
      if (this.genderSel[i].value == 0) {
        this.genderSel.splice(i, 1);
      }
    }
    console.log(this.genderSel);
    console.log(123321, this.userInfo);
    this.organizationName = this.userInfo.organizationName;
    this.employeeType = this.userInfo.employeeType;
    this.organizationId = this.userInfo.organizationId;
    // this.accountId = this.userInfo.accountId;
    // this.employeeId = this.userInfo.employeeId;
    console.log("flag:" + JSON.stringify(this.global.deletedFlag));
  },
  watch: {
    deptFilterText(val) {
      this.$refs.deptTree.filter(val);
    },
  },

  methods: {
    //删除员工
    formdeleted(form) {
      console.log("删除");
      console.log(form.id);
      var vm = this;
      let msg = `是否要删除员工 ${form.name}?`;
      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;
          empSvc
            .batchDeleteEmployeeById({ EmployeeIds: [form.id] })
            .then(
              (res) => {
                if (res.data) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              (err) => {
                console.error(err);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
              this.cancel();
            });
        },
        (cancel) => {
          console.error(JSON.stringify(cancel));
        }
      );
    },
    //弹框table表格选中事件
    onshopSelectall() {
      let shopchecked = this.$refs.shopData.selection;
      console.log(shopchecked);
      this.shoprows = shopchecked;
      console.log("this.shoprows", this.shoprows);
      shopchecked.forEach((item, index) => {
        if (item.roleId.length == 0) {
          // this.$alertInfo("请选择角色");
        }
      });
      if (shopchecked.length == 0) {
        this.shopData.forEach((item, index) => {
          item.roleId = [];
        });
      }
    },
    onshopSelect(rows, row) {
      console.log(rows);

      let selected = rows.length && rows.indexOf(row) !== -1;
      console.log(selected); // true就是选中，0或者false是取消选中
      if (selected == true) {
        // if (row.roleId.length == 0) {
        //   this.$alertInfo("请选择角色");
        // }
      } else {
        row.roleId = [];
      }
    },
    onTableSelect(rows, row) {
      let comchecked1 = this.$refs.companyData.selection;
      console.log(comchecked1);
      let comchecked = rows.length && rows.indexOf(row) !== -1;
      // true就是选中，0或者false是取消选中
      if (comchecked == true) {
        if (row.roleId.length == 0) {
          // this.$alertInfo("请选择角色");
        }
      } else {
        row.roleId = [];
      }
    },
    onTableSelectall(rows, row) {
      let comchecked = this.$refs.companyData.selection;
      console.log(comchecked);
      console.log(111, rows);
      comchecked.forEach((item, index) => {
        // if (item.roleId.length == 0) {
        //   this.$alertInfo("请选择角色");
        // }
      });
      if (comchecked.length == 0) {
        this.companyData.forEach((item, index) => {
          item.roleId = [];
        });
      }
    },

    getname() {
      console.log("name", this.formModel.name);
    },
    getmobile() {
      console.log("mobile", this.formModel.mobile);
      if (this.checked == true) {
        this.formModel.weChat = this.formModel.mobile;
      }
      if (this.formModel.mobile != "") {
        this.tableLoading = true;
        empSvc
          .CheckAccountIsExistByName({ Name: this.formModel.mobile })
          .then(
            (res) => {
              console.log(22, res.data);

              this.hasAccount = res.data.hasAccount;
              if (this.hasAccount == true) {
                this.formModel.password = "";
              }
              this.accountId1 = res.data.accountId;
              console.log(this.accountId1);
            },
            (err) => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
    },
    getnumber() {
      console.log("number", this.formModel.number);
    },
    getidentity() {
      console.log("identity", this.formModel.identity);
    },
    getpassword() {
      console.log("password", this.formModel.password);
    },
    getemail() {
      console.log("email", this.formModel.email);
    },
    getweChat() {
      console.log("weChat", this.formModel.weChat);
    },
    getqq() {
      console.log("qq", this.formModel.qq);
    },
    getdescription() {
      console.log("description", this.formModel.description);
    },
    getgender() {
      console.log("gender", this.formModel.gender);
    },
    checkinlist() {
      console.log("roleIds", this.formModel.roleIds);
    },
    checkShopRole(row) {
      this.checked1 = !this.checked1;
      if (row.roleId.length > 0) {
        this.$refs.shopData.toggleRowSelection(row, true);
      } else {
        this.$refs.shopData.toggleRowSelection(row, false);
      }
      // console.log("shoproles", row.roleId);
      // console.log("roles", row.roles);
    },
    checkrole(row) {
      this.companychecked = !this.companychecked;
      if (row.roleId.length > 0) {
        this.$refs.companyData.toggleRowSelection(row, true);
      } else {
        this.$refs.companyData.toggleRowSelection(row, false);
      }
      // console.log("roles", row.roles);
    },
    isDeleted1() {
      //  this.formModel.isNeedLogin = !this.formModel.isNeedLogin;
      console.log(122, this.formModel.isDeleted);
    },
    getchecked() {
      console.log("checked", this.checked);

      if (this.checked == true) {
        if (this.formModel.mobile == "") {
          console.log("请输入手机号码");
          this.$alertWarning("请输入手机号码");
        }

        this.formModel.weChat = this.formModel.mobile;
      } else {
        this.formModel.weChat = "";
      }
    },
    fetchData() {
      this.getConditionData();
      this.getPaged();
    },
    // getUnitsForSelByType(type) {
    //   comBusSvc
    //     .getUnitsForSelByType({ type: type })
    //     .then(
    //       res => {
    //         this.organizationSel = res.data;
    //       },
    //       err => {
    //         console.error(err);
    //       }
    //     )
    //     .catch(() => {});
    // },
    getDepartmentSelByOrgIdType(orgId, type) {
      empSvc
        .getDepartmentSelByOrgIdType({ organizationId: orgId, type: type })
        .then(
          (res) => {
            this.departmentSel = res.data;
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    getDepartmentTreeByOrgIdType(orgId, type) {
      empSvc
        .getDepartmentTreeByOrgIdType({ organizationId: orgId, type: type })
        .then(
          (res) => {
            this.deptTreeList = res.data;
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    getJobListByOrgIdForWeb(orgId, type) {
      empSvc
        .getJobListByOrgIdForWeb({
          organizationId: this.organizationId,
          type: this.employeeType,
        })
        .then(
          (res) => {
            this.jobSel = res.data;
            console.log("jobSel", this.jobSel);
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {});
    },

    GetRoleList(orgId, type) {
      empSvc
        .GetRoleList({
          organizationId: this.organizationId,
          type: this.employeeType,
        })
      // roleSvc
      //   .GetRoleListByOrgId({
      //     organizationId: this.organizationId,
      //     type: this.employeeType,
      //   })
        .then(
          (res) => {
            this.roledata = res.data;
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    GetRangeRoleListByOrgId(orgId, type) {
      empSvc
        .GetRangeRoleListByOrgId({
          organizationId: this.organizationId,
          type: this.employeeType,
        })
        .then(
          (res) => {
            this.tableLoading = true;
            this.AllroleList = res.data;
            console.log("AllroleList", this.AllroleList);
            this.$forceUpdate();
            var newArr = this.AllroleList.filter(function (obj) {
              return obj.type == 0;
            });
            console.log("newArr", newArr);
            this.companyData = newArr;
            this.companyData.forEach((item, index) => {
              item.roleId = [];
            });

            console.log("12333characterData", this.characterData);
            var newArr1 = this.AllroleList.filter(function (obj) {
              return obj.type == 1;
            });
            console.log("newArr1", newArr1);
            this.shopData = newArr1;
            this.shopData.forEach((item, index) => {
              item.roleId = [];
            });
            //       this.shopData.forEach((item, index) => {
            //   item.roles.forEach((item1, index) => {
            //   item1.companychecked=false
            // });
            //  });
            if (this.orgRanges.length > 0) {
              for (let i = 0; i < this.shopData.length; i++) {
                for (let j = 0; j < this.orgRanges.length; j++) {
                  if (
                    this.orgRanges[j].organizationId ==
                      this.shopData[i].organizationId &&
                    this.orgRanges[j].type == this.shopData[i].type
                  ) {
                    this.shopData[i].roleId = this.orgRanges[j].roleId;
                  }
                }
              }
              for (let i = 0; i < this.companyData.length; i++) {
                for (let j = 0; j < this.orgRanges.length; j++) {
                  if (
                    this.orgRanges[j].organizationId ==
                      this.companyData[i].organizationId &&
                    this.orgRanges[j].type == this.companyData[i].type
                  ) {
                    this.companyData[i].roleId = this.orgRanges[j].roleId;
                  }
                }
              }
            }
            // orgRanges.forEach(item1=>{
            //   this.shopData.forEach(item2=>{
            //     if( item2.organizationId==item1.organizationId&& item1.type ==
            //       item2.type){
            //          this.$refs.shopData.toggleRowSelection(item2,true);
            //       }
            //   })
            // })
            let arr2 = [];
            this.shopData.forEach((item) => {
              this.orgRanges.forEach((val) => {
                if (
                  val.organizationId === item.organizationId &&
                  val.type === item.type
                ) {
                  arr2.push(item);
                }
              });
            });
            this.toggleSelection(arr2);
            let arr3 = [];
            this.companyData.forEach((item) => {
              this.orgRanges.forEach((val) => {
                if (
                  val.organizationId === item.organizationId &&
                  val.type === item.type
                ) {
                  arr3.push(item);
                }
              });
            });
            this.toggleSelection1(arr3);
            this.$forceUpdate();
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
          this.loading = false;
        });
    },
    toggleSelection(rows) {
      this.$nextTick(() => {
        rows.forEach((row) => {
          this.$refs.shopData.toggleRowSelection(row, true);
        });
        this.$forceUpdate();
      });
    },

    toggleSelection1(rows) {
      this.$nextTick(() => {
        rows.forEach((row) => {
          this.$refs.companyData.toggleRowSelection(row, true);
        });
        this.$forceUpdate();
      });
    },
    getEmployeeInfo(empId, orgId, empType) {
      empSvc
        .getEmployeeInfo({ employeeId: empId })
        .then((res) => {
          let data = res.data;
          this.formModel = data;
          this.formModel.employeeId = data.id;
          // this.isNeedLogin= data.isDeleted
          // this.formModel.isNeedLogin = data.isDeleted;

          this.orgRanges = data.orgRanges;
          console.log("orgRanges", this.orgRanges);
          if (data.type == 1 || data.departmentId == 0) {
            this.formModel.departmentId = undefined;
          } else {
            this.originalDeptId = data.departmentId;
          }
          console.log("data.isTechnican " + data.isTechnican);
          let jobId = this.formModel.jobId;
          this.formModel.jobId = [];
          if (data.isTechnican) {
            this.formModel.jobId.push(jobId);
            this.formModel.jobId.push(data.workKindId);
            this.formModel.jobId.push(data.level);
          } else {
            this.formModel.jobId.push(jobId);
          }

          console.log(" this.formModel.jobId " + this.formModel.jobId);

          // this.getUnitsForSelByType(empType);
          this.getDepartmentSelByOrgIdType(orgId, empType);
          this.getJobListByOrgIdForWeb(orgId, empType);
          this.GetRangeRoleListByOrgId(orgId, empType);
          this.GetRoleList(orgId, empType);
          this.$forceUpdate();
        })
        .catch((err) => {
          console.error(err);
        })
        .finally(() => {});
    },
    getConditionData() {
      // this.getAllOrganizationExceptShopSelectListAsync();
    },
    filterOrgType(value) {
      let newType = JSON.parse(JSON.stringify(this.formType));
      for (let index = 0; index < newType.length; index++) {
        let item = newType[index];
        if (item.value === value) {
          return item.label;
        }
      }
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    getPaged(flag) {
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.organizationId) {
        // var arr = data.organizationId.split("|");
        // data.organizationId = arr[0];
        // data.type = arr[1];
        data.organizationId = this.userInfo.organizationId;
      }
      console.log("userInfo: " + JSON.stringify(this.userInfo));
      data.organizationId = this.userInfo.organizationId;
      console.log("data: " + JSON.stringify(data));

      this.tableLoading = true;
      empSvc
        .getEmployeePage(data)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },

    search(flag) {
      this.condition.pageIndex = 1;
      this.getPaged(flag);
    },
    tbRowDblClick(row, column, event, tb) {
      // if (row.isDeleted) {
      //   this.$alert("员工 " + row.name + " 已禁用，不允许查看详情！", "警告", {
      //     confirmButtonText: "关闭",
      //     type: "warning"
      //   });
      //   return;
      // }

      this.resetSel();
      //console.log("tbRowDblClick: " + JSON.stringify(row));
      tb.toggleRowSelection(row);
      // tb.clearSelection();
      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;

      this.getEmployeeInfo(row.id, row.organizationId, row.employeeType);
    },
    handleTypeChange(value) {
      //console.log(`EmpType: ${value}`);
      this.formModel.organizationId = undefined;
      this.formModel.departmentId = undefined;
      this.formModel.jobId = [];

      // this.getUnitsForSelByType(value);
    },
    handleOrgChange(value) {
      // console.log(`OrgType: ${value}`);
      this.formModel.jobId = [];

      this.getDepartmentSelByOrgIdType(value, this.formModel.type);
      this.getJobListByOrgIdForWeb(value, this.formModel.type);
    },
    handleJobChange(value) {
      console.log(`jobId: ${value}`);
    },
    add() {
      this.loading = true;
      this.resetSel();
      this.formTitle = "新增员工";
      this.formVisible = true;
      this.showpass = true;
      this.deleteShow = false;
      this.hasAccount = false;
      this.orgRanges = [];
      this.checked = false;
      // this.getUnitsForSelByType(this.formModel.type);
      this.getJobListByOrgIdForWeb();
      this.GetRangeRoleListByOrgId();
      this.GetRoleList();
      // this.roleIds = [];
      this.loading = false;
    },
    edit(sel) {
      this.loading = true;
      this.deleteShow = true;
      this.formTitle = "编辑员工";
      this.formVisible = true;
      this.showpass = false;
      console.log("id", sel.id);

      console.log("showpass", this.showpass);
      this.getEmployeeInfo(sel.id, sel.organizationId, sel.employeeType);
      
      this.loading = false;
    },
    save(formName) {
      this.shopsave = true;
      console.log(this.$refs.companyData);

      if (this.$refs.companyData) {
        let comchecked = this.$refs.companyData.selection;
        let comArr = [];
        comchecked.forEach(function (item) {
          comArr.push({
            organizationId: item.organizationId,
            type: item.type,
            roleId: item.roleId,
          });
        });
        this.comArr = comArr;
      }
      // console.log(comchecked);

      if (this.$refs.shopData) {
        let shopchecked = this.$refs.shopData.selection;

        let shopArr = [];
        shopchecked.forEach(function (item) {
          shopArr.push({
            organizationId: item.organizationId,
            type: item.type,
            roleId: item.roleId,
          });
        });
        this.shopArr = shopArr;
      }

      console.log(this.shopArr);
      console.log("comArr", this.comArr);
      let allChecked = [];
      allChecked = this.comArr.concat(this.shopArr);

      //  let allChecked = [];
      console.log(allChecked);
      //       // console.log("allChecked", allChecked);
      var vm = this;
      if (allChecked != []) {
        allChecked.forEach((item, index) => {
          if (item.roleId.length == 0) {
            vm.shopsave = false;
          }
        });
      }
      // console.log("save formModel: " + JSON.stringify(this.formModel));
      // allChecked.forEach((item, index) => {
      //   if (item.roleId.length == 0) {
      //     this.$alertInfo("请将对应的角色勾选完整");
      //   }
      // });
      //      if(vm.shopsave==false){
      //         this.$alertInfo("请将管辖范围中相对应的角色选择完整");
      // return false
      //     }
      if (vm.shopsave) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            vm.btnSaveLoading = true;

            var jobIds = this.formModel.jobId;
            var len = jobIds.length;
            if (this.formModel.type == 1 && len != 1) {
              this.formModel.jobId = jobIds[0];
              this.formModel.workKindId = jobIds[1];
              this.formModel.level = jobIds[2];
            } else {
              this.formModel.jobId =
                jobIds[0] == undefined ? jobIds : jobIds[0];
              this.formModel.workKindId = 0;
              this.formModel.level = 0;
            }
            let savedata = {
              employeeId: this.formModel.employeeId,
              accountId: this.accountId1,
              password: this.formModel.password,
              organizationId: this.organizationId,
              type: this.employeeType,
              name: this.formModel.name,
              email: this.formModel.email,
              mobile: this.formModel.mobile,
              gender: this.formModel.gender,
              identity: this.formModel.identity,
              number: this.formModel.number,
              level: this.formModel.level,
              workKindId: this.formModel.workKindId,
              jobId: this.formModel.jobId,
              weChat: this.formModel.weChat,
              qq: this.formModel.qq,
              avatar: "",
              qualificationCertificate: [],
              description: this.formModel.description,
              isNeedLogin: this.formModel.isDeleted,
              roleIds: this.formModel.roleIds,
              orgRanges: allChecked,
            };
            console.log("savedata", savedata);
            empSvc
              .createOrUpdateEmployeeForWeb(savedata)
              .then(
                (res) => {
                  if (res.data) {
                    this.$message({
                      message: res.message,
                      type: "success",
                    });
                    vm.search();
                    if (vm.formVisible) {
                      vm.getConditionData();
                    }
                    vm.cancel();
                  }
                },
                (err) => {
                  //恢复所选的职位
                  this.formModel.jobId = [];

                  var len = jobIds.length;
                  if (this.formModel.type == 1 && len != 1) {
                    this.formModel.jobId.push(jobIds[0]);
                    this.formModel.jobId.push(jobIds[1]);
                    this.formModel.jobId.push(jobIds[2]);
                  } else {
                    this.formModel.jobId.push(jobIds[0]);
                    this.formModel.workKindId = 0;
                    this.formModel.level = 0;
                  }
                }
              )
              .catch((err) => {
                console.error(err);
              })
              .finally(() => {
                vm.btnSaveLoading = false;
              });
          } else {
            return false;
          }
        });
      } else {
        this.$alertInfo("请将管辖范围中相对应的角色选择完整");
      }
    },

    //   deleted(sel) {
    //     var vm = this;
    //     msg = `是否要禁用员工 ${sel.name}?`;
    //     this.$confirmWarning(msg).then(
    //       () => {
    //         this.tableLoading = true;
    //         empSvc
    //           .batchDeleteEmployeeById({ EmployeeIds: sel.id })
    //           .then(
    //             res => {
    //               if (res.data) {
    //                 this.$messageSuccess(res.message);
    //                 vm.search();
    //               } else {
    //                 this.$messageWarning(res.message);
    //               }
    //             },
    //             err => {
    //               console.error(err);
    //             }
    //           )
    //           .catch(() => {})
    //           .finally(() => {
    //             this.tableLoading = false;
    //           });
    //       },
    //       cancel => {
    //         console.error(JSON.stringify(cancel));
    //       }
    //     );
    // },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;
      this.resetForm();
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
      // this.$refs.shopData.clearSelection();
      // this.$refs.companyData.clearSelection();
      this.shopData.forEach((item, index) => {
        item.roleId = [];
      });
      this.companyData.forEach((item, index) => {
        item.roleId = [];
      });
      this.$forceUpdate();
    },
    remove(formName) {
      this.formVisible = false;
      this.formCheck = false;
      this.resetForm();
      console.log("删除");
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    resetSel() {
      this.organizationSel = [];
      this.jobSel = [];
    },
    selDept() {
      console.log(
        "this.formModel.departmentId: " + this.formModel.departmentId
      );

      // if (
      //   !this.formModel.organizationId ||
      //   this.formModel.organizationId <= 0
      // ) {
      //   return this.$alertWarning("请选择公司！");
      // }

      this.formDeptTreeVisible = true;

      this.getDepartmentTreeByOrgIdType(
        this.formModel.organizationId,
        this.formModel.type
      );

      if (this.originalDeptId == this.formModel.departmentId) {
        this.defaultExpandedKeys = this.defaultCheckedKeys = [];

        this.defaultExpandedKeys.push(this.formModel.departmentId);
        this.defaultCheckedKeys.push(this.formModel.departmentId);
      }
    },
    deptFilter(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    deptNodeChange(data, node) {
      // console.log("data: ", data);
      // console.log("node: ", node);

      // if (data.children.length > 0) {
      //   return this.$messageWarning("请选择最后一级部门");
      // }

      let deptIds = this.$refs.deptTree.getCheckedKeys();
      let len = deptIds.length;

      if (len == 0) {
        this.formModel.departmentId = undefined;
        this.defaultExpandedKeys = this.defaultCheckedKeys = [];
        return;
      }

      for (let index = 0; index < len; index++) {
        this.$refs.deptTree.setChecked(
          deptIds[index],
          deptIds[index] == data.id ? true : false
        );
      }

      this.formModel.departmentId = data.id;
      this.defaultExpandedKeys = this.defaultCheckedKeys = [];
      this.defaultExpandedKeys.push(data.id);
      this.defaultCheckedKeys.push(data.id);
    },
    saveDeptTree() {
      var deptIds = this.$refs.deptTree.getCheckedKeys();
      var len = deptIds.length;
      if (len > 1) {
        return this.$alertWarning("只能选择一个部门");
      }
      if (len == 1) {
        this.formModel.departmentId = deptIds[0];

        this.defaultExpandedKeys = this.defaultCheckedKeys = [];
        this.defaultExpandedKeys.push(deptIds[0]);
        this.defaultCheckedKeys.push(deptIds[0]);
      }
      this.cancelDeptTree();
    },
    cancelDeptTree() {
      this.formDeptTreeVisible = false;
      this.deptTreeList = [];
    },
  },
};
</script>

<style lang="scss" scoped>
.el-input.is-disabled .el-input__inner,
.el-textarea.is-disabled .el-textarea__inner {
  background-color: #ffffff !important;
  color: #606266 !important;
}
.company {
  text-align: right;
  vertical-align: middle;
  float: left;
  font-size: 16px;
  color: #2c2c2e;
  line-height: 16px;
  padding: 0 12px 0 20px;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  font-weight: 700;
  margin-bottom: 1px;
}
>>> .el-checkbox {
  color: #606266;
  font-weight: 500;
  font-size: 14px;
  cursor: pointer;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  width: 19%;
}
>>> .el-table .cell {
  padding-right: 9px;
}
</style>
