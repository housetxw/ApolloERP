import Vue from 'vue'

import 'normalize.css/normalize.css' // A modern alternative to CSS resets

import ElementUI from 'element-ui'
import * as jscom from "@/utils/common.js";
Vue.prototype.$jscom = jscom;
import RgTableColumn from "@/components/RgTableColumn/index";
import RgDialog from "@/components/RgDialog/index.vue";
import RgPage from "@/components/TemplatePage/index.vue";

import 'element-ui/lib/theme-chalk/index.css'
import locale from 'element-ui/lib/locale/lang/zh-CN' // lang i18n

import '@/styles/index.scss' // global css

import App from './App'
import store from './store'
import router from './router'

import '@/icons' // icon
import '@/permission' // permission control

import 'xe-utils' // VXETable
import VXETable from 'vxe-table'
import 'vxe-table/lib/index.css'
import JsonViewer from 'vue-json-viewer'
import VCharts from 'v-charts'
Vue.use(VCharts)
Vue.use(VXETable)
Vue.use(JsonViewer)

/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online! ! !
 */

// set ElementUI lang to EN
Vue.use(ElementUI, { locale })
Vue.component(RgTableColumn.name, RgTableColumn);
Vue.component(RgDialog.name, RgDialog);
Vue.component(RgPage.name, RgPage);

Vue.config.productionTip = false

//global variable
Vue.prototype.global = {
  //账号权限系统：搜索全局条件
  //数据库删除标志符字典表
  deletedFlag: [
    {
      value: undefined,
      label: "全部"
    },
    {
      value: 0,
      label: "否"
    },
    {
      value: 1,
      label: "是"
    }
  ],
  employeeSrchType: [
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

  //账号权限系统：Form全局条件
  genderSel: [
    {
      value: 0,
      label: "保密"
    },
    {
      value: 1,
      label: "男"
    },
    {
      value: 2,
      label: "女"
    }
  ],
  employeeFormType: [
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
}

//custom element-ui message
Vue.prototype.$messageInfo = function (msg, settings = {}) {
  Object.assign(settings, { message: msg || '操作成功', type: "info" });
  return this.$message(settings);
}

Vue.prototype.$messageSuccess = function (msg, settings = {}) {
  Object.assign(settings, { message: msg || '操作成功', type: "success" });
  return this.$message(settings);
}

Vue.prototype.$messageWarning = function (msg = '操作失败', settings = {}) {
  Object.assign(settings, { message: msg || '操作失败', type: "warning" });
  return this.$message(settings);
}

Vue.prototype.$messageError = function (msg, settings = {}) {
  Object.assign(settings, { message: msg || '操作失败', type: "error" });
  return this.$message(settings);
}

//custom element-ui alert
Vue.prototype.$alertInfo = function (msg, title = "信息", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "关闭",
    type: "info"
  });
  return this.$alert(msg, title, settings);
}

Vue.prototype.$alertSuccess = function (msg, title = "提示", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "关闭",
    type: "success"
  });
  return this.$alert(msg, title, settings);
}

Vue.prototype.$alertWarning = function (msg, title = "警告", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "关闭",
    type: "warning"
  });
  return this.$alert(msg, title, settings);
}

Vue.prototype.$alertError = function (msg, title = "错误", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "关闭",
    type: "error"
  });
  return this.$alert(msg, title, settings);
}

//custom element-ui confirm
Vue.prototype.$confirmInfo = function (msg, title = "信息", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "info"
    //, center: true
  });
  return this.$confirm(msg || '', title, settings);
}

Vue.prototype.$confirmSuccess = function (msg, title = "提示", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "success"
  });
  return this.$confirm(msg || '', title, settings);
}

Vue.prototype.$confirmWarning = function (msg, title = "警告", settings = {}) {
  Object.assign(settings, {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  });
  return this.$confirm(msg || '', title, settings);
}


new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App),
  data() {
    return {
      // 用于全局监听窗口大小
      screen: {
        width: document.body.clientWidth,
        height: document.body.clientHeight
      },
      toggleSideBar: false,
      timeRgPickerOpt: {
        shortcuts: [{
          text: "最近一周",
          onClick(picker) {
            const end = new Date();
            const start = new Date();
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
            picker.$emit("pick", [start, end]);
          }
        },
        {
          text: "最近一个月",
          onClick(picker) {
            const end = new Date();
            const start = new Date();
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
            picker.$emit("pick", [start, end]);
          }
        },
        {
          text: "最近三个月",
          onClick(picker) {
            const end = new Date();
            const start = new Date();
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
            picker.$emit("pick", [start, end]);
          }
        }
        ]
      },
    }
  },
  mounted() {
    // 用于全局监听窗口大小
    window.onresize = () => {
      this.screen = {
        width: document.body.clientWidth,
        height: document.body.clientHeight
      };
    };
  },
  methods: {
    //关闭当前页面【toPath：激活页面，reFresh：激活页面重新加载数据】
    close_curpage(toPath, reFresh) {
      this.$store.state.tagsView.visitedViews.splice(this.$store.state.tagsView.visitedViews.findIndex(item => item.path === this.$route.path), 1)
      if (toPath === undefined) {
        this.$router.push(this.$store.state.tagsView.visitedViews[this.$store.state.tagsView.visitedViews.length - 1].path)
      }
      else {
        // console.log(toPath)
        this.$router.push({ path: toPath })
        this.$route.meta.isFresh = reFresh || false;
      }
    },
  }
})
