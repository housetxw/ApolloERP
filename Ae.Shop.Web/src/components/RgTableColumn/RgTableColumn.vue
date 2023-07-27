<template>
  <!--判断scopedSlots.default可知道是否存在子元素-->
  <el-table-column
    v-if="$scopedSlots.default"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot-scope="scope">
      <!--原element组件使用了作用域插槽, 这里仿照-->
      <slot v-bind="scope"></slot>
    </template>
  </el-table-column>
  <!--使用 slot 自定义 header-->
  <el-table-column
    v-else-if="$scopedSlots.header"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot="header" slot-scope="scope">
      <slot name="header" v-bind="scope"></slot>
    </template>
  </el-table-column>

  <!--默认情况使用原始 el-table-column-->
  <el-table-column
    v-else-if="attrs['is-datetime']!=undefined"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot-scope="scope">{{$jscom.toYMDHm(scope.row[attrs.prop])}}</template>
  </el-table-column>
  <el-table-column
    v-else-if="attrs['is-date']!=undefined"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot-scope="scope">{{$jscom.toYMD(scope.row[attrs.prop])}}</template>
  </el-table-column>
  <el-table-column
    v-else-if="attrs['is-money']!=undefined"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot-scope="scope">{{$jscom.toMoney(scope.row[attrs.prop])}}</template>
  </el-table-column>
  <el-table-column
    v-else-if="attrs['is-number']!=undefined"
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  >
    <template slot-scope="scope">{{$jscom.format(scope.row[attrs.prop],attrs.decimal_places||0 )}}</template>
  </el-table-column>

  <el-table-column
    v-else
    v-bind="attrs"
    :key="attrs.label"
    :class-name="className"
    :min-width="minWidth"
  ></el-table-column>
</template>

<script>
import $consts from "./constants";
export default {
  name: "rg-table-column",
  computed: {
    attrs() {
      if (
        this.$attrs["is-date"] != undefined ||
        this.$attrs["is-datetime"] != undefined
      ) {
        this.slotted = true;
        this.$attrs.align = "center";
        // this.$attrs["fix-min"] = true;
      } else if (
        this.$attrs["is-number"] != undefined ||
        this.$attrs["is-money"] != undefined
      ) {
        this.slotted = true;
        this.$attrs.align = "right";
        // this.$attrs["fix-min"] = true;
      }
      return this.$attrs;
    },
    // table数据
    values() {
      this.calcWidth = 50;
      const data = this.$parent.data || [];
      return data.map((item) => item[this.$attrs.prop]);
    },
    // 是否自适应列宽, 默认是
    isFit() {
      // return this.$attrs;
      let _is =
        (this.$parent.$attrs.autoFit === undefined &&
          this.$attrs.fit === undefined) ||
        (this.$parent.$attrs.autoFit === false &&
          this.$attrs.fit !== undefined);
      return _is;
    },
    // 为存在scope的添加className
    className() {
      const parentClass = this.$attrs["class-name"] || "";
      const classPre =
        this.$attrs.prop || `encode-${this.transChar(this.$attrs.label)}`; // 有的列因为有slotScope而不给prop
      return classPre ? `${parentClass} ${classPre}-column` : "";
    },
    // 列最小宽度
    minWidth() {
      if (!this.$attrs.label) return undefined;
      if (!this.isFit) return undefined;
      const maxOne = 
        Math.max(
          this.minLength,
          this.$attrs.label.length * this.fontRate.CHAR_RATE
        ) *
          this.fontSize +
        20;
      this.calcWidth = Math.max(this.calcWidth, maxOne);
      const min_width = this.$attrs["min-width"] || this.calcWidth;
      //按最小宽度设置列宽
      if (this.$attrs["fix-min"] != undefined) {
        this.$set(this.$attrs,"width",min_width)
      }
      return min_width;
    },
    // 字体大小
    fontSize() {
      return (
        this.$attrs.fontSize ||
        (this.$ELEMENT || {}).fontSize ||
        $consts.fontSize
      );
    },
    // 字体比例
    fontRate() {
      return {
        ...$consts.fontRate, // 默认值
        ...((this.$ELEMENT || {}).fontRate || {}), // 根注册参数 (Vue.use 时的第二个参数)
        ...(this.$attrs.fontRate || {}), // 父组件属性
      };
    },
  },
  watch: {
    values: {
      immediate: true,
      handler(val) {
        if (!val || !val.length) return;
        //父主体隐藏时，不执行
        // if (
        //   this.$parent &&
        //   this.$parent.$parent &&
        //   this.$parent.$parent._vnode &&
        //   this.$parent.$parent._vnode.elm &&
        //   this.$parent.$parent._vnode.elm.offsetParent === null
        // ) {
        //       console.log(val)
        //   this.calcWidth = 50;
        //   return;
        // }
        this.isFit !== false &&
          this.$nextTick(() => {
            // 详情中的列表需要在nextTick才能生效
            if (this.$scopedSlots.default || this.slotted) {
              // 存在自定义节点
              setTimeout(() => {
                // 首次获取不到子节点, setTimeout才行
                // 可能存在贴边列, 需要使用包含 fixed 的类名
                const bodyWrapper = this.$parent._vnode.elm;
                const nodes = bodyWrapper.querySelectorAll(
                  `.${
                    this.$attrs.prop ||
                    `encode-${this.transChar(this.$attrs.label)}`
                  }-column`
                );

                if (nodes.length) {
                  const target = [];
                  Array.from(nodes).map((item) => {
                    target.push(item.innerText);
                  });
                  this.$set(this, "minLength", this.getMaxLength(target));
                }
              }, 0);
            } else {
              this.$set(this, "minLength", this.getMaxLength(val));
            }
          });
      },
    },
  },
  data() {
    return {
      minLength: 5, // 初始也不要写成0, 避免未及时设置宽度太丑
      getComputedWidth: 0, // 自定义列中获取元素计算的宽度
      calcWidth: 50,
      slotted: false,
    };
  },
  methods: {
    // 获取数组中元素按字体比例最长长度
    getMaxLength(arr) {
      return arr.reduce((length, item) => {
        if (item) {
          const str = item.toString();
          const char = str.match(/[\u2E80-\u9FFF]/g);
          const charLength = char ? char.length : 0;
          const num = str.match(/\d|\./g);
          const numLength = num ? num.length : 0;
          const otherLength = str.length - charLength - numLength;

          let newLength =
            charLength * this.fontRate.CHAR_RATE +
            numLength * this.fontRate.NUM_RATE +
            otherLength * this.fontRate.OTHER_RATE;
          if (str.includes("\n"))
            newLength = this.getMaxLength(str.split("\n"));
          if (length < newLength) {
            length = newLength;
          }
        }
        return length;
      }, 0);
    },
    // 转换汉字为class支持的字母
    transChar(char) {
      return encodeURIComponent(char).replace(/[^a-zA-z]/g, "eUC");
    },
  },
};
</script>