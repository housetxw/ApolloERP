<template>
  <main>
    <div ref="header" class="header-search">
      <el-form
        :inline="true"
        :class="[isCollapse?'search-line':'']"
        :style="barLeftStyle"
        ref="condition"
        size="mini"
        :model="conditionModel"
        :rules="conditionRules"
      >
        <slot name="condition"></slot>
      </el-form>
      <el-form :inline="true" class="search-line searchbar" :style="barRightStyle" size="mini">
        <el-form-item>
          <el-button-group ref="condition_btn">
            <el-button
              type="success"
              icon="el-icon-arrow-down"
              v-if="isCollapse && searchBarOverWidth"
              @click="toggleConditionBar()"
            >更多</el-button>
            <el-button
              type="success"
              icon="el-icon-arrow-up"
              v-if="!isCollapse && searchBarOverWidth"
              @click="toggleConditionBar()"
            >收缩</el-button>

            <el-dropdown
              v-if="showReset"
              split-button
              size="mini"
              type="warning"
              @click="()=> {this.currentPage = 1;  searching.call(this,true);}"
              @command="command=>{
                if(command=='reset'){
                 this.$refs['condition'].resetFields();
                this.currentPage = 1;  
                if(typeof $attrs['on-reset'] === 'function') $attrs['on-reset'].call();
                searching.call(this,true);
                }
              }"
              trigger="click"
            >
              <span class="el-dropdown-link">
                <i class="el-icon-search"></i> 搜索
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item icon="el-icon-refresh-right" command="reset">重置查询条件</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
            <el-button
              v-else
              type="warning"
              icon="el-icon-search"
              @click="()=> {this.currentPage = 1;  searching.call(this,true);}"
            >搜索</el-button>

            <slot name="header_btn"></slot>
          </el-button-group>
        </el-form-item>
      </el-form>
    </div>
    <main>
      <el-table
        ref="tb_handle"
        v-loading="tableLoading"
        element-loading-text="加载中..."
        :data="tableData"
        :height="tableHeight"
        stripe
        border
        size="mini"
        style="width: 100%"
        @row-click="(row, column, event)=>{tbRowClick.call(this,row, column, event,this.$refs.tb_handle);}"
        @row-dblclick="(row, column, event)=>{tbRowDblClick.call(this,row, column, event,this.$refs.tb_handle);}"
        @cell-click="(row, column, cell, event)=>{tbCellClick.call(this,row, column, cell, event,this.$refs.tb_handle);}"
        @cell-dblclick="(row, column, cell, event)=>{tbCellDblClick.call(this,row, column, cell, event,this.$refs.tb_handle);}"
        :row-class-name="({row, rowIndex})=>{ return this.tableRowClassName.apply(this,[row, rowIndex]);}"
        :cell-class-name="(p)=>{ return this.tbCellClassName.apply(this,[p])}"
        @selection-change="(p)=>{handleSelectionChange.call(this,p);}"
        highlight-current-row
        @current-change="(p1,p2)=>{handleSelectRow.call(this,p1,p2);}"
        @expand-change="expandChange"
      >
        <el-table-column
          type="index"
          label="行号"
          :index="indexMethod"
          v-if="showRowIndex"
          min-width="50"
        ></el-table-column>
        <!-- <el-table-column
          type="index"
          :width="indexColWidth"
          label="总数"
          :index="indexMethod"
          v-if="showRowIndex"
        ></el-table-column>-->
        <slot name="tb_cols"></slot>
      </el-table>

      <section class="pagination">
        <el-pagination
          background
          :page-size="pageSize"
          :page-sizes="[10, 20, 50, 100, 500]"
          layout="total, sizes, prev, pager, next, jumper"
          :total="dataCount"
          :current-page.sync="currentPage"
          @current-change="handleCurrentChange"
          @size-change="handleSizeChange"
        ></el-pagination>
      </section>
    </main>
  </main>
</template>
<script>
export default {
  name: "rg-page",
  props: {
    //自适应表格高度偏差值
    tbFullDeviationHeight: { type: Number, default: 160 },
    // 头部按钮总宽度
    // headerBtnWidth: { type: Number, default: 160 },
    // 表格数据总条数
    dataCount: { type: Number, default: 0 },
    // 默认页序
    pageIndex: { type: Number, default: 1 },
    // 默认分页显示条数
    pageSize: { type: Number, default: 20 },
    // 表格数据加载中变量
    tableLoading: { type: Boolean, default: false },
    // 表格是否显示第一列【行号】
    showRowIndex: { type: Boolean, default: true },
    showReset: { type: Boolean, default: false },
    defaultCollapse: { type: Boolean, default: true },
    // 表格数据
    tableData: { type: Array, default: [] },
    //分页变化时调用方法
    pageChange: { type: Function, default: ({}) => {} }, //{pageIndex: val,currentPage: val, pageSize: this.pageSize      }
    //搜索按钮点击事件
    searching: { type: Function, default: () => {} },
    //数据行单击击
    tbRowClick: {
      type: Function,
      default: (row, column, event, tb_handle) => {},
    },
    //数据行双击
    tbRowDblClick: {
      type: Function,
      default: (row, column, event, tb_handle) => {},
    },
    //数据单元格单击
    tbCellClick: {
      type: Function,
      default: (row, column, cell, event, tb_handle) => {},
    },
    //数据单元格双击
    tbCellDblClick: {
      type: Function,
      default: (row, column, cell, event, tb_handle) => {},
    },
    //该边行css
    tableRowClassName: { type: Function, default: ({ row, rowIndex }) => {} },
    tbCellClassName: { type: Function, default: (p) => {} },
    //行选中事件
    handleSelectionChange: {
      type: Function,
      default: (p) => {},
    },
    handleSelectRow: {
      type: Function,
      default: (p) => {},
    },
    conditionModel: {},
    conditionRules: {},
    tbSubHeight: { type: Number, default: 0 },
  },
  data() {
    return {
      currentPage: 1,
      searchBarHeight: 45,
      searchBarWidth: 0,
      searchBarOverWidth: false,
      isCollapse: this.defaultCollapse,
      tableHeight: 50,
      barLeftStyle: {
        float: "left",
      },
      barRightStyle: {},
      tbHandle: null,
      isMounted: false,
    };
  },

  computed: {
    windowResize() {
      return this.$root.$data.screen;
    },
    toggleSideBar() {
      return this.$root.$data.toggleSideBar;
    },
  },
  watch: {
    windowResize(val) {
      this.calcConditionWidth();
    },
    toggleSideBar(val) {
      setTimeout(() => {
        this.calcConditionWidth();
      }, 500);
    },
  },
  mounted() {
    if (typeof this.$parent["synicVar"] == "function") {
      let p = { tbHandle: this.$refs.tb_handle };
      this.$parent.synicVar(p);
    }
    this.isMounted = true;
    this.resetBrnWidth(null, (p) => {
      this.calcConditionWidth();
    });
  },
  methods: {
    expandChange(row, expandedRows) {
      this.$emit("expand-change", row, expandedRows);
    },
    // 每页查看条数变化
    handleSizeChange(val) {
      this.currentPage = 1;
      this.pageChange.call(this, {
        pageIndex: 1,
        currentPage: 1,
        pageSize: val,
      });
    },
    // 当前页码变化
    handleCurrentChange(val) {
      this.currentPage = val;
      this.pageChange.call(this, {
        pageIndex: val,
        currentPage: val,
        pageSize: this.pageSize,
      });
    },
    indexMethod(index) {
      return (this.pageIndex - 1) * this.pageSize + index + 1;
    },
    calcConditionWidth() {
      this.$nextTick(() => {
        //判别搜索条件是否超宽
        let firstOffsetTop = 0;
        let searchBarOverWidth = this.searchBarOverWidth;
        this.$refs.condition.$children.some((p) => {
          if (firstOffsetTop == 0) {
            firstOffsetTop = p.$el.offsetTop;
          } else {
            searchBarOverWidth = p.$el.offsetTop > firstOffsetTop;
            return searchBarOverWidth;
          }
        });
        if (this.searchBarOverWidth != searchBarOverWidth) {
          this.searchBarOverWidth = searchBarOverWidth;
          this.resetBrnWidth();
        }
      });
    },
    resetBrnWidth(totalWidth, callback) {
      totalWidth = totalWidth || 0;
      this.$nextTick(() => {
        if (totalWidth == 0) {
          this.$refs.condition_btn.$children.forEach((p) => {
            totalWidth += p.$el.clientWidth || 0;
          });
          totalWidth += this.showReset ? 15 : 10;
        }
        this.$set(
          this.barLeftStyle,
          "max-width",
          "calc(100% - " + (totalWidth + 5) + "px)"
        );
        this.$set(this.barRightStyle, "width", totalWidth + "px");
        this.$nextTick(() => {
          if (
            this.$refs.condition_btn.$el.offsetHeight > 50 &&
            totalWidth < 1000
          ) {
            totalWidth += 10;
            this.resetBrnWidth(totalWidth);
          } else {
            this.calcTbHeight();
            if (callback != undefined) {
              callback.call();
            }
          }
        });
      });
    },
    calcTbHeight() {
      this.$nextTick((p) => {
        this.tableHeight =
          document.body.clientHeight -
          this.$refs.condition.$el.offsetHeight -
          this.tbSubHeight -
          this.tbFullDeviationHeight;
      });
    },
    toggleConditionBar() {
      this.isCollapse = !this.isCollapse;
      this.calcTbHeight();
    },
  },
};
</script>