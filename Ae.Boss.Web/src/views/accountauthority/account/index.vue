<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <!-- 搜索 -->
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageTurning"
        :headerBtnWidth="180"
        :searching="search"
        :handleSelectionChange="tableRowClick1"
        :tbRowClick="tableRowClick"
        element-loading-text="加载中..."
        ref="refAccountTable"
      >
        <template v-slot:condition>
          <el-form-item prop="organizationId">
            <el-select
              v-model="condition.organizationId"
              clearable
              filterable
              placeholder="请选择所属单位"
            >
              <el-option
                v-for="item in organizationSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <el-tag size="medium" type="info" style="float: right;">{{filterOrgType(item.type)}}</el-tag>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="employeeName">
            <el-input
              v-model="condition.employeeName"
              autocomplete="off"
              placeholder="请输入员工名称"
              clearable
            ></el-input>
          </el-form-item>
          <el-form-item label="是否禁用" prop="isDeleted">
            <el-select v-model="condition.isDeleted" placeholder="请选择">
              <el-option
                v-for="item in flag"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item></el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button type="primary" size="mini" icon="el-icon-unlock" @click="unlock()">解锁</el-button>
          <el-button type="danger" size="mini" icon="el-icon-lock" @click="lock()">锁定</el-button>
          <el-button type="warning" size="mini" icon="el-icon-key" @click="resetPwd()">重置密码</el-button>
          <el-button type="danger" size="mini" icon="el-icon-delete" @click="deleted()">禁用</el-button>
        </template>
        <template v-slot:tb_cols>
          <el-table-column type="selection"></el-table-column>
          <!-- Table数据 -->
          <el-table-column label="员工Id" prop="employeeId" v-if="false"></el-table-column>
          <el-table-column label="账号Id" prop="accountId" v-if="false"></el-table-column>
          <el-table-column label="登录账号" prop="accountName" width="110"></el-table-column>
          <el-table-column label="员工名称" prop="employeeName" show-overflow-tooltip></el-table-column>
          <el-table-column label="员工类型" prop="type" width="90">
            <template slot-scope="scope">
              <el-tag size="medium" type="info">{{filterOrgType(scope.row.type)}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="所属单位Id" prop="organizationId" v-if="false"></el-table-column>
          <el-table-column label="所属单位" prop="organizationName" show-overflow-tooltip width="240"></el-table-column>
          <el-table-column label="部门Id" prop="departmentId" v-if="false"></el-table-column>
          <el-table-column label="部门" prop="departmentName" show-overflow-tooltip></el-table-column>
          <el-table-column label="岗位Id" prop="jobId" v-if="false"></el-table-column>
          <el-table-column label="岗位" prop="jobName" show-overflow-tooltip width="120"></el-table-column>
          <el-table-column label="密码错误次数" prop="errorCount" width="110">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="genErrorSytle(scope.row.errorCount)"
              >{{scope.row.errorCount}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="状态" prop="state" width="90" fixed="right">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="genStateSytle(scope.row.state)"
              >{{genStateValue(scope.row.state)}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="是否禁用" prop="isDeleted" width="90">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="scope.row.isDeleted === false ? '' : 'danger'"
              >{{scope.row.isDeleted === false ? '否' : '是'}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="创建人" prop="createBy" show-overflow-tooltip width="110"></el-table-column>
          <el-table-column label="创建人Id" prop="createId" v-if="false"></el-table-column>
          <el-table-column label="创建时间" prop="createTime" show-overflow-tooltip width="110"></el-table-column>
          <el-table-column label="修改人" prop="updateBy" show-overflow-tooltip width="110"></el-table-column>
          <el-table-column label="修改人Id" prop="updateId" v-if="false"></el-table-column>
          <el-table-column label="修改时间" prop="updateTime" show-overflow-tooltip width="110"></el-table-column>
        </template>
      </rg-page>
      <!-- 列表 -->
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { comBusSvc, accountSvc } from "@/api/accountauthority/account";

export default {
  name: "account",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      type: [
        {
          value: undefined,
          label: "全部"
        },
        {
          value: 0,
          label: "公司"
        },
        {
          value: 1,
          label: "门店"
        },
        {
          value: 2,
          label: "仓库"
        }
      ],
      accountState: [
        {
          value: -1,
          label: "未设置"
        },
        {
          value: 0,
          label: "正常"
        },
        {
          value: 1,
          label: "异常"
        },
        {
          value: 2,
          label: "锁定"
        }
      ],
      condition: {
        organizationId: undefined,
        type: undefined,
        id: undefined,
        employeeName: "",
        isDeleted: 0,
        pageIndex: 1,
        pageSize: 20
      },
      organizationSel: [],
      tableData: [],
      totalList: 0,
      formType: [
        {
          value: 0,
          label: "公司"
        },
        {
          value: 1,
          label: "门店"
        },
        {
          value: 2,
          label: "仓库"
        }
      ]
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      this.getConditionData();
      this.getPaged();
    },
    getAllOrganizationExceptShopSelectListAsync() {
      comBusSvc
        .getAllOrganizationExceptShopSelectListAsync()
        .then(
          res => {
            this.organizationSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getConditionData() {
      this.getAllOrganizationExceptShopSelectListAsync();
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
    genErrorSytle(value) {
      if (value == 0) {
        return "";
      } else if (value >= 3 && value < 5) {
        return "info";
      } else if (value >= 5 && value < 10) {
        return "warning";
      } else {
        return "danger";
      }
    },
    genStateSytle(value) {
      switch (value) {
        case -1:
          return "info";
        case 0:
          return "";
        case 1:
          return "danger";
        case 2:
          return "warning";
      }
    },
    genStateValue(value) {
      let newState = JSON.parse(JSON.stringify(this.accountState));
      for (let index = 0; index < newState.length; index++) {
        let item = newState[index];
        if (item.value === value) {
          return item.label;
        }
      }
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.organizationId) {
        var arr = data.organizationId.split("|");
        data.organizationId = arr[0];
        data.type = arr[1];
      } else {
        data.organizationId = undefined;
        data.type = undefined;
      }

      console.log("data: " + JSON.stringify(data));

      this.tableLoading = true;
      accountSvc
        .getAccountPage(data)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    tableRowClick(row, column, event, refAccountTable) {
      console.log("tableRowClick: " + JSON.stringify(row));
      refAccountTable.toggleRowSelection(row);
      this.$refs.refAccountTable.selection = refAccountTable.selection;
    },
    tableRowClick1(val) {
      console.log(val);
      this.$refs.refAccountTable.selection = val;
      console.log(121, this.$refs.refAccountTable.selection);
      // this.$refs.refAccountTable.toggleRowSelection(row);
    },
    unlock() {
      var vm = this;

      let sels = this.$refs.refAccountTable.selection;
      if (sels) {
        var msg = "";
        if (sels.length <= 0) {
          msg = "至少要选择一个要解锁的账号";
        } else if (sels.length != 1) {
          msg = "同时只能解锁一个账号";
        }
        if (msg.length > 0) {
          return this.$alertWarning(msg);
        }
      }

      let sel = sels[0];
      if (sel.isDeleted) {
        return this.$alertInfo("已禁用的账号不允许解锁");
      }
      if (sel.state == 0) {
        return this.$alertInfo("账号状态正常，无需解锁");
      }

      this.$confirmWarning(`是否要解锁员工 ${sel.employeeName} ?`).then(
        () => {
          this.tableLoading = true;
          accountSvc
            .unlockAccountById({ id: sel.accountId })
            .then(
              res => {
                if (res.data) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        cancel => {
          console.log(JSON.stringify(cancel));
        }
      );
    },
    lock() {
      var vm = this;

      let sels = this.$refs.refAccountTable.selection;
      if (sels && sels.length <= 0) {
        return this.$alertWarning("至少要选择一个要锁定的账号");
      }

      let ids = sels
        .filter(s => {
          if (s.state == 0 && !s.isDeleted) {
            return s;
          }
        })
        .map(s => s.accountId);

      if (ids.length <= 0) {
        return this.$alertWarning("至少要选择一个正常或没有禁用的账号锁定");
      }

      var msg = "";
      if (sels.length == 1) {
        msg = `是否要锁定员工 ${sels[0].employeeName} 的账号?`;
      } else {
        msg = "是否要锁定所选员工的账号?";
      }

      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;

          accountSvc
            .lockAccountById({ id: ids })
            .then(
              res => {
                if (res.data) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        cancel => {
          console.log(JSON.stringify(cancel));
        }
      );
    },
    resetPwd() {
      var vm = this;

      let sels = this.$refs.refAccountTable.selection;
      if (sels) {
        var msg = "";
        if (sels.length <= 0) {
          msg = "至少要选择一个要重置密码的账号";
        } else if (sels.length != 1) {
          msg = "同时只能为一个账号重置密码";
        }
        if (msg.length > 0) {
          return this.$alertWarning(msg);
        }
      }

      let sel = sels[0];

      if (sel.isDeleted) {
        return this.$alertWarning("已禁用的账号不允许重置密码");
      }
      if (sel.state !== 0) {
        return this.$alertWarning("只能重置正常状态的账户密码");
      }

      this.$confirmWarning(`是否要重置员工 ${sel.employeeName} 的密码?`).then(
        () => {
          this.tableLoading = true;

          accountSvc
            .resetAccountPasswordById({ id: sel.accountId })
            .then(
              res => {
                var data = res.data;
                if (data.flag) {
                  this.$messageSuccess(res.message);
                  vm.search();
                  this.$alertInfo(
                    `${sel.employeeName}重置后的密码为: ${data.resetPassword}`
                  );
                } else {
                  this.$messageWarning(res.message);
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        cancel => {
          console.log(JSON.stringify(cancel));
        }
      );
    },
    deleted() {
      var vm = this;

      let sels = this.$refs.refAccountTable.selection;
      if (sels) {
        var msg = "";
        if (sels.length <= 0) {
          msg = "至少要选择一个要禁用的账号";
        }
        if (msg.length > 0) {
          return this.$alertWarning(msg);
        }
      }

      var msg = "";
      if (sels.length == 1) {
        msg = `是否要禁用员工 ${sels[0].employeeName} 的账号?`;

        if (sels[0].isDeleted) {
          return this.$alertWarning("已禁用的账号不允许再次禁用");
        }
      } else {
        msg = "是否要禁用所选员工的账号?";
      }

      let ids = sels
        .filter(s => {
          if (!s.isDeleted) {
            return s;
          }
        })
        .map(s => s.accountId);

      if (ids.length <= 0) {
        return this.$alertWarning("至少要选择一个没有禁用的账号禁用");
      }

      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;

          accountSvc
            .deleteBatchAccountById({ id: ids })
            .then(
              res => {
                if (res.data) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        cancel => {
          console.log(JSON.stringify(cancel));
        }
      );
    }
  }
};
</script>
<style lang="scss" scoped>
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px !important ;
}
</style>