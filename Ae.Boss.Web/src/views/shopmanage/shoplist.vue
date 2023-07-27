<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.simpleName"
              clearable
              placeholder="请输入门店简称"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.ownerPhone"
              clearable
              placeholder="请输入联系电话"
            />
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.provinceId"
              clearable
              filterable
              placeholder="请选择省"
              style="width: 120px"
              @change="getCity"
            >
              <el-option
                v-for="item in provinceSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.cityId"
              clearable
              filterable
              @change="getDistrict"
              style="width: 120px"
              placeholder="请选择市"
            >
              <el-option
                v-for="item in citySel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.districtId"
              clearable
              filterable
              style="width: 120px"
              placeholder="请选择区"
            >
              <el-option
                v-for="item in districtSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.checkStatus"
              clearable
              filterable
              style="width: 120px"
              placeholder="请选择审核状态"
            >
              <el-option
                v-for="item in checkStatueSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.head"
              clearable
              filterable
              style="width: 120px"
              placeholder="请选择门店负责人"
            >
              <el-option
                v-for="item in headerList"
                :key="item.head"
                :label="item.head"
                :value="item.head"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.onLineStatus"
              clearable
              filterable
              style="width: 120px"
              placeholder="请选择上下架状态"
            >
              <el-option
                v-for="item in onlineStatusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="ShopType">
            <el-select
              v-model="condition.ShopType"
              style="width: 120px"
              placeholder="门店类型"
            >
              <el-option
                v-for="item in shopType"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="systemType">
            <el-select
              v-model="condition.systemType"
              style="width: 120px"
              placeholder="系统版本"
            >
              <el-option
                v-for="item in systemType"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>

        <template v-slot:header_btn>
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="
              (p) => {
                $router.push({ name: 'addshop' });
              }
            "
            >新增</el-button
          >
        </template>

        <template v-slot:tb_cols>
          <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" class="demo-table-expand">
                <el-form-item label="门店全称">{{
                  props.row.shopCompanyName
                }}</el-form-item>
                <el-form-item
                  label="驳回原因"
                  v-if="props.row.failedExaminedReason"
                >
                  <label>{{ props.row.failedExaminedReason }}</label>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <rg-table-column label="编号" prop="id" />

          <rg-table-column label="门店简称" prop="simpleName" />
          <!-- <rg-table-column label="门店名称" prop="fullName" /> -->
          <rg-table-column label="门店联系人">
            <template slot-scope="scope">
              {{ scope.row.contact }}&nbsp;{{ scope.row.mobile }}
            </template>
          </rg-table-column>
          <rg-table-column label="运营负责人">
            <template slot-scope="scope">
              {{ scope.row.head }}&nbsp;{{ scope.row.headPhone }}
            </template>
          </rg-table-column>
          <rg-table-column label="地址" prop="address" />
          <rg-table-column label="经营状态" align="center">
            <template slot-scope="scope">
              <div v-if="scope.row.status === 0">
                <label style="font-weight: 400">已开业</label>
              </div>
              <div v-else-if="scope.row.status === 1">
                <label style="color: red">终止合作</label>
              </div>
              <div v-else-if="scope.row.status === 2">
                <label style="color: orange">暂停营业</label>
              </div>
            </template>
          </rg-table-column>
          <rg-table-column label="审核状态" align="center">
            <template slot-scope="scope">
              <div v-if="scope.row.checkStatus === 0">
                <label style="color: orange">新建</label>
              </div>
              <div v-if="scope.row.checkStatus === 1">
                <label style="color: orange">待审核</label>
              </div>
              <div v-else-if="scope.row.checkStatus === 2">
                <label>已通过</label>
              </div>
              <div v-else-if="scope.row.checkStatus === 3">
                <label style="color: red">已驳回</label>
              </div>
            </template>
          </rg-table-column>
          <rg-table-column label="上下架状态" align="center">
            <template slot-scope="scope">
              <div v-if="scope.row.online === 0">
                <label style="color: red">下架</label>
              </div>
              <div v-else-if="scope.row.online === 1">
                <label>上架</label>
              </div>
            </template>
          </rg-table-column>
          <rg-table-column
            label="门店类型"
            prop="type"
            align="center"
          ></rg-table-column>
          <rg-table-column
            label="系统类型"
            prop="showSystemType"
            align="center"
          ></rg-table-column>
          <rg-table-column label="店公司名称" prop="shopCompanyName" />
          <el-table-column label="操作" width="180" fixed="right">
            <template slot-scope="scope">
              <el-button-group>
                <el-button
                  size="mini"
                  type="primary"
                  @click="
                    $router.push({
                      name: 'shopdetail',
                      params: { id: scope.row.id },
                    })
                  "
                  >编辑</el-button
                >

                <el-button
                  type="success"
                  size="mini"
                  v-show="scope.row.online == 0"
                  @click="onlineShop(scope.row, 1)"
                  >上架</el-button
                >
                <el-button
                  type="danger"
                  size="mini"
                  v-show="scope.row.online == 1"
                  @click="onlineShop(scope.row, 0)"
                  >下架</el-button
                >
                <router-link
                  tag="a"
                  :to="{
                    name: 'shophistory',
                    params: { ShopId: scope.row.id },
                  }"
                >
                  <el-button size="mini" type="warning">日志</el-button>
                </router-link>

                <el-button
                  type="primary"
                  size="mini"
                  :disabled="scope.row.checkStatus !== 1"
                  @click="auditShop(scope.row)"
                  >审核</el-button
                >
              </el-button-group>
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 审核门店 -->
    <section id="auditDialog">
      <el-dialog
        :title="auditFormTitle"
        :close-on-click-modal="false"
        :visible.sync="auditFormVisible"
        :before-close="auditCancel"
        width="550px"
      >
        <el-form :model="auditModel" ref="auditModel">
          <el-form-item label="备注:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入备注"
              v-model="auditModel.failedExaminedReason"
            ></el-input>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="auditCancel()">取消</el-button>
          <el-button type="danger" @click="auditSave(2)">驳回</el-button>
          <el-button type="primary" @click="auditSave(1)">通过</el-button>
        </div>
      </el-dialog>
    </section>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";

export default {
  data() {
    return {
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      tableData: [],
      totalList: 0,
      auditFormTitle: undefined,
      auditFormVisible: false,
      provinceSel: [],

      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined,
      },

      checkStatueSel: [
        {
          key: "4",
          value: "所有",
        },
        {
          key: "1",
          value: "待审核",
        },
        {
          key: "2",
          value: "审核通过",
        },
        {
          key: "3",
          value: "审核不通过",
        },
      ],
      onlineStatusSel: [
        {
          key: "2",
          value: "所有",
        },
        {
          key: "0",
          value: "下架",
        },
        {
          key: "1",
          value: "上架",
        },
      ],
      shopType: [
        {
          key: 0,
          value: "门店类型",
        },
        {
          key: 1,
          value: "合作店",
        },
        {
          key: 2,
          value: "工场店",
        },
        {
          key: 4,
          value: "上门养车",
        },
        {
          key: 8,
          value: "认证店",
        },
        {
          key: 16,
          value: "工作室",
        },
        {
          key: 32,
          value: "前置仓",
        },
      ],
      systemType: [
        {
          key: -1,
          value: "系统版本",
        },
        {
          key: 0,
          value: "普通",
        },
        {
          key: 1,
          value: "高级",
        },
        {
          key: 2,
          value: "个人",
        },
      ],
      auditModel: {
        failedExaminedReason: undefined,
        ShopId: undefined,
        checkStatus: undefined,
      },

      auditModelInit: {
        failedExaminedReason: undefined,
        ShopId: undefined,
        checkStatus: undefined,
      },
      onlineShopModel: {
        shopId: undefined,
        online: undefined,
      },
      onlineShopModelInit: {
        shopId: undefined,
        online: undefined,
      },
      //table页面的条件
      condition: {
        simpleName: undefined,
        status: 0,
        ownerPhone: undefined,
        ShopType: 0,
        pageIndex: 1,
        pageSize: 20,
        ServiceType: 0,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        onLineStatus: undefined,
        checkStatus: undefined,
        head: undefined,
        systemType: -1,
      },
      headerList: [],
    };
  },

  created() {
    //页面初始化函数
    this.fetchData();
  },

  methods: {
    auditCancel() {
      this.auditFormVisible = false;
      this.auditModel = JSON.parse(JSON.stringify(this.auditModelInit));
    },
    onlineShop(row, type) {
      var msg = type == 1 ? "上架" : "下架";
      this.$confirm("确定" + msg + "吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.onlineShopModel.shopId = row.id;
          this.onlineShopModel.online = type;

          shopManageSvc
            .updateShopOnline(this.onlineShopModel)
            .then(
              (res) => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }

                this.onlineShopModel = JSON.parse(
                  JSON.stringify(this.onlineShopModelInit)
                );
                row.online = type;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    auditSave(type) {
      var msg = type == 1 ? "审核通过" : "驳回";
      this.$confirm("确定" + msg + "吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          var vm = this;
          if (type == 1) {
            this.auditModel.checkStatus = 1;
          } else if (type == 2) {
            if (
              this.auditModel.failedExaminedReason == "" ||
              this.auditModel.failedExaminedReason == undefined
            ) {
              this.$message({
                message: "请填写驳回理由!",
                type: "warning",
              });
              return false;
            }
            this.auditModel.checkStatus = 2;
          }
          shopManageSvc
            .updateExaminedStatus(this.auditModel)
            .then(
              (res) => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }

                this.auditFormVisible = false;
                this.auditModel = JSON.parse(
                  JSON.stringify(this.auditModelInit)
                );
                vm.search();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    auditShop(row) {
      //auditModel
      this.auditFormTitle = "审核【" + row.simpleName + "】门店";
      this.auditFormVisible = true;
      this.auditModel.ShopId = row.id;
    },
    getCity() {
      debugger;
      if (
        this.condition.provinceId != "" &&
        this.condition.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.condition.provinceId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            (res) => {
              this.citySel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.cityId = undefined;
        this.condition.districtId = undefined;
        this.citySel = [];
        this.districtSel = [];
      }
    },

    getDistrict() {
      if (this.condition.cityId != "" && this.condition.cityId != undefined) {
        this.regionCondition.regionId = this.condition.cityId;

        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            (res) => {
              this.districtSel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.districtId = undefined;
        this.districtSel = [];
      }
    },
    fetchData() {
      this.regionCondition.regionId = 0;
      shopManageSvc
        .getRegionChinaListByRegionId(this.regionCondition)
        .then(
          (res) => {
            this.provinceSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});

      this.getPaged();
      this.getShopHeaderListByAsync();
    },
    //门店列表
    GetShopList() {
      shopManageSvc
        .GetShopList()
        .then(
          (res) => {
            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //分页 列表
    getPaged(flag) {
      if (this.condition.onLineStatus == "") {
        this.condition.onLineStatus = undefined;
      }

      if (this.condition.checkStatus == "") {
        this.condition.checkStatus = undefined;
      }
      console.log("condition: " + JSON.stringify(this.condition));

      //debugger;
      this.tableLoading = true;
      shopManageSvc
        .GetShopList(this.condition)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    pageChange(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // sizeChange(val) {
    //   this.condition.pageIndex = this.currentPage = 1;
    //   this.condition.pageSize = val;
    //   this.getPaged();
    // },
    handleEdit(row) {
      console.log(row.id);
      this.$router.push({
        path: `/shopdetail/${row.id}`,
      });
    },
    handleDelete(index, row) {
      console.log(index, row);
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },
    //搜索
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },

    getShopHeaderListByAsync() {
      shopManageSvc
        .GetShopHeaderListByAsync()
        .then(
          (res) => {
            this.headerList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
  },
};
</script>
