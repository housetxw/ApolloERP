<template>
  <el-dialog
    v-bind="$attrs"
    :close-on-click-modal="$attrs['close-on-click-modal']||false"
    :top="top"
    custom-class="rg-dialog"
    v-dialogDrag
    v-change
  >
    <div class="dialog_body" v-loading="loading" :style="{'max-height':dialogBodyMaxH}">
      <slot name="content"></slot>
    </div>
    <div slot="footer" class="dialog-footer" v-if="footbar" style="text-align:center">
      <el-button
        type="danger"
        size="mini"
        icon="el-icon-delete"
        @click="btnRemove.click"
        v-if="!(btnRemove ==false || btnRemove.show==false)"
        :disabled="btnRemove.disabled||false"
        :loading="btnRemove.loading||false"
        style="float:left"
      >{{btnRemove.label}}</el-button>
      <slot name="footer"></slot>
      <el-button
        icon="el-icon-close"
        size="mini"
        @click="btnCancel.click"
        v-if="btnCancel!=false"
      >{{btnCancel.label}}</el-button>
    </div>
  </el-dialog>
</template>
<script>
import * as jcom from "@/utils/directives.js";
export default {
  name: "rg-dialog",
  data() {
    return {
      elDialogHandle: null,
      dialogBodyMaxH: ""
    };
  },
  computed: {
    windowResize() {
      return this.$root.$data.screen;
    },
    dataloaded() {
      let _this = this;
      _this.$nextTick(() => {
        _this.adjustTop();
      });
      return _this.loading;
    },
    isOpen() {
      if (this.$vnode.elm) this.$vnode.elm.removeAttribute("title");
      return this.$attrs.visible || this.$attrs.visible.sync;
    },
    btnRemove() {
      return Object.assign(
        {
          label: "删除",
          show: false,
          disabled: true,
          click: () => {}
        },
        this.$attrs["btnRemove"] || {}
      );
    },
    btnCancel() {
      return Object.assign(
        { label: "关闭", click: this.$attrs["before-close"] },
        this.$attrs["btnCancel"] || {}
      );
    },
    footbar() {
      return this.$attrs["footbar"] || true;
    },
    maxWidth() {
      return this.$attrs["maxWidth"] || this.$attrs["max-width"] || "100%";
    },
    minWidth() {
      return this.$attrs["minWidth"] || this.$attrs["min-width"] || "200px";
    },
    height() {
      return this.$attrs["height"] || "";
    },
    minHeight() {
      return this.$attrs["minHeight"] || this.$attrs["min-height"] || "";
    },
    maxHeight() {
      return;
      this.$attrs["maxHeight"] ||
        this.$attrs["max-height"] ||
        "calc(100vh - 20px)";
    },
    top() {
      return this.$attrs["top"] || "10px";
    },
    loading() {
      return this.$attrs["loading"] || false;
    }
  },
  watch: {
    isOpen(val) {
      if (val) {
        this.$nextTick(() => {
          this.adjustTop();
        });
      }
    },
    windowResize(val) {
      if (this.elDialogHandle) {
        this.adjustTop();
      }
    },
    dataloaded(val) {
      //弹出页面数据加载完成后，重新计算弹出框尺寸及位置
      if (val == false && this.elDialogHandle) {
        this.adjustTop();
      }
    }
  },
  methods: {
    rgDlgStyle() {
      return { height: 200 };
    },

    adjustTop(dlgHandle) {
      this.dialogBodyMaxH =
        this.$root.$data.screen.height - (this.footbar ? 110 : 20) + "px";
      if (dlgHandle) {
        this.elDialogHandle = dlgHandle;
      }
      this.$nextTick(() => {
        if (this.height) this.elDialogHandle.style["height"] = this.height;
        if (this.maxHeight)
          this.elDialogHandle.style["max-height"] = this.maxHeight;
        if (this.minHeight)
          this.elDialogHandle.style["min-height"] = this.minHeight;
        this.elDialogHandle.style["top"] =
          "calc(50vh - " + this.elDialogHandle.offsetHeight / 2 + "px)";
        this.elDialogHandle.style["margin-top"] = "";
        this.elDialogHandle.style["max-width"] = this.maxWidth;
        this.elDialogHandle.style["min-width"] = this.minWidth;
      });
    },
    _open() {
      this.adjustTop();
      if (this.open) {
        this.open.call();
      }
    }
  },
  directives: {
    change: {
      //指令的定义
      inserted: function(el, binding, _this) {
        _this.context.adjustTop(el.firstElementChild);
      }
    }
  }
};
</script>
<style lang="scss">
.el-dialog__wrapper {
  background-color: #00000075;
  .rg-dialog {
    max-height: calc(100vh - 20px);
    overflow: hidden;
    .el-dialog__header {
      padding: 10px 20px;
    }
    .el-dialog__body {
      padding: 0;
      height: calc(100% - 112px);
    }
    .dialog_body {
      height: 100%;
      padding: 20px 10px;
      overflow: auto;
    }
  }
}
</style>