<template>
  <main>
    <el-button type="text" @click="dialogVisible = true">点击打开 Dialog</el-button>

    <section id="ttt">
      <rg-dialog
        title="部门列表"
        :visible.sync="dialogVisible"
        :btnRemove="{label: '删除', show: true,          disabled: true,          click: () => {}        }"
        :btnCancel="{label: '关闭', click: (done) => {dialogVisible=false;} }"
        top="9vh"
        width="80%"
        height="800px"
      >
        <template v-slot:content>
          <el-form ref="form" :model="form" label-width="120px">
            <el-form-item label="活动名称">
              <el-input v-model="form.name"></el-input>
            </el-form-item>
            <el-form-item label="活动区域" label-suffix="fdsafdsafd">
              <el-col class="line">活动区域</el-col>
            </el-form-item>
            <el-form-item label="活动时间">
              <el-col class="line v-label">活动时间活动时间活动时间活动时间活动时间</el-col>
            </el-form-item>
            <el-form-item label="即时配送">
              <label>即时配送</label>
            </el-form-item>
            <el-form-item label="活动性质">
              <el-checkbox-group v-model="form.type">
                <el-checkbox label="美食/餐厅线上活动" name="type"></el-checkbox>
                <el-checkbox label="地推活动" name="type"></el-checkbox>
                <el-checkbox label="线下主题活动" name="type"></el-checkbox>
                <el-checkbox label="单纯品牌曝光" name="type"></el-checkbox>
              </el-checkbox-group>
            </el-form-item>
            <el-form-item label="特殊资源">
              <el-radio-group v-model="form.resource">
                <el-radio label="线上品牌商赞助"></el-radio>
                <el-radio label="线下场地免费"></el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="活动形式">
              <el-input type="textarea" v-model="form.desc"></el-input>
            </el-form-item>
          </el-form>
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
export default {
  name: "demo",
  data() {
    return {
      dialogVisible: false,
      form: {
        name: "",
        region: "",
        date1: "",
        date2: "",
        delivery: false,
        type: [],
        resource: "",
        desc: ""
      }
    };
  },
  methods: {
    onSubmit() {
      console.log("submit!");
    },
    remove() {
      this.$confirm("确定审核通过吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      });
    }
  }
};
</script>
