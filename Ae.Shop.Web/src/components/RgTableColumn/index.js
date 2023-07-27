import RgTableColumn from './RgTableColumn.vue'

RgTableColumn.install = (Vue, options) => {
  Vue.prototype.$ELEMENT = options
  Vue.component(RgTableColumn.name, RgTableColumn)
}

export default RgTableColumn