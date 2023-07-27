

<template>
  <rg-dialog
    dlgId="dlgBooking"
    title="选择预约时间"
    :loading="loading"
    :visible.sync="dlg_booking"
    :beforeClose="()=>{this.close(false)}"
    :btnCancel="{label:'关闭',click:(done)=>{this.close(false)}}"
    :btnRemove="false"
    :footbar="true"
    width="900px"
  >
    <template v-slot:content>
      <el-table
        :data="bookingRangData.data"
        ref="bookingTb"
        border
        size="mini"
        class="booking-table"
        style="width: 100%"
        :max-height="$root.$data.screen.height - 275"
        :cell-class-name="cellClass"
        @cell-click="cellClick"
      >
        <el-table-column label="时间" prop="time" align="center" width="60" fixed="left"></el-table-column>
        <el-table-column
          align="center"
          prop="col_1"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_2"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_3"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_4"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_5"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_6"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_7"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_8"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_9"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
        <el-table-column
          align="center"
          prop="col_10"
          :formatter="colFormatter"
          :render-header="renderHeader"
        ></el-table-column>
      </el-table>
    </template>
    <template v-slot:footer>
      <el-button icon="el-icon-check" size="mini" type="primary" @click="changeReserveTime">确定</el-button>
    </template>
  </rg-dialog>
</template>

<script>
import { appSvc } from "@/api/user/user";
export default {
  name: "dlgBookingTime",
  props: {
    dlg_booking: { type: Boolean, default: false },
    shopId: { type: Number, default: 0 },
  },
  data() {
    return {
      loading: true,
      bookingRangData: [] //预约时间数据
    };
  },
  watch: {
    dlg_booking: function(v, oldv) {
      if (v == true) {
        this.queryReserveDashboardForAdd();
      }
    }
  },
  methods: {
    queryReserveDashboardForAdd() {
      this.loading = true;
      var request = {
        shopId:this.shopId
      };
      appSvc
        .getReserveDateForWeb(request)
        .then(
          res => {
            var data = res.data;
            this.bindReserveDateData(data);
            this.chooseType = 1;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.loading = false;
        });
    },
    bindReserveDateData(data) {
      let d = {
        data: [],
        header: [{ label: "时间", field: "time", width: 90 }]
      };
      data.timePartList.forEach(t => {
        d.data.push({ time: t });
      });
      let iCol = 0,
        iRow = 0;
      data.reserveDateInfoList.forEach(e => {
        iCol++;
        d.header.push({
          date: e.date,
          week: e.week,
          fullReserve: e.fullReserve,
          label: e.date + "\n" + e.week
        });
        iRow = 0;
        e.items.forEach(item => {
          d.data[iRow]["col_" + iCol] = item;
          iRow++;
        });
      });
      this.bookingRangData = d;
    },
    changeReserveTime() {
      this.close(this.bookingRangData.selected);
      return;
      if (!this.bookingRangData.selected) {
        return this.$message({
          message: "请选择预约时间",
          type: "error"
        });
      } else {
        this.close(this.bookingRangData.selected);
      }
    },
    close(value) {
      this.$emit("onclose", value);
    },
    colFormatter(row, column, cellValue, index) {
      if (Math.max(cellValue.enableNum, cellValue.reservedNum || 0) == 0)
        return "";
      return (cellValue.reservedNum || 0) + "/" + (cellValue.enableNum || 0);
    },
    renderHeader(h, { column, $index }) {
      try {
        return this.bookingRangData.header[$index].label;
      } catch {}
    },
    cellClick(row, column, cell, event) {
      if (this.$jscom.hasClass(cell, "normal")) {
        this.$jscom.removeClass(
          document.querySelector(".cell-selected"),
          "cell-selected"
        );
        this.$nextTick(() => {
          this.$jscom.addClass(cell, "cell-selected");
        });
        //选中预约时间
        this.bookingRangData.selected = row[column.property].dateTimePart;
      }
    },
    cellClass({ row, column, rowIndex, columnIndex }) {
      if (columnIndex === 0) return "time";
      let cellValue = row["col_" + columnIndex];
      if (Math.max(cellValue.enableNum, cellValue.reservedNum || 0) == 0)
        return "disabled";
      if (cellValue.overdue) return "overdue";
      if ((cellValue.enableNum || 0) == (cellValue.reservedNum || 0))
        return "full";
      return "normal";
    }
  }
};
</script>
<style lang="scss" scoped>
/deep/ .booking-table {
  th .cell {
    white-space: pre-line;
  }
  td.time {
    background: content-box;
    &:hover {
      background: content-box;
    }
  }
  td.full {
    background: #f8ac59;
    cursor: not-allowed;
    .cell {
      display: inline;
    }
    &::before {
      text-shadow: 1px 1px 1px white;
      content: "约满";
    }
  }
  td.overdue {
    background: #ccc;
    cursor: not-allowed;
    .cell {
      display: inline;
    }
    &::before {
      text-shadow: 1px 1px 1px white;
      content: "过时";
    }
  }

  td.disabled {
    text-shadow: 1px 1px 1px white;
    background: #ccc;
    cursor: not-allowed;
    &::after {
      content: "未开放";
    }
  }
  td.normal {
    background: white;
    cursor: default;
    &:hover {
      background: darkcyan;
      color: white;
    }
  }
  td.cell-selected {
    background: darkcyan;
    color: white;
    cursor: default;
    &:hover {
      background: darkcyan;
      color: white;
    }
  }
}
</style>
