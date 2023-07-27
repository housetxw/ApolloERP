import request2 from '@/utils/request2'

const domain = process.env.VUE_APP_BASE_Product_API
const proudctConfigSvc = {
  listCategory(params) {
    return request2({
      url: domain + '/ProductConfig/ListCategory',
      method: 'get',
      params
    })
  },
  getCategory(id) {
    return request2({
      url: domain + '/ProductConfig/GetCategory?id=' + id,
      method: 'get'
    })
  },
  treeSelect() {
    return request2({
      url: domain + '/ProductConfig/CategoryTreeSelect',
      method: 'get'
    })
  },
  updateCategory(data) {
    return request2({
      url: domain + '/ProductConfig/UpdateCategory',
      method: 'post',
      data
    })
  },
  addCategory(data) {
    return request2({
      url: domain + '/ProductConfig/AddCategory',
      method: 'post',
      data
    })
  },
  deleteCategory(data) {
    return request2({
      url: domain + '/ProductConfig/DeleteCategory',
      method: 'post',
      data
    })
  }
}
export {
  proudctConfigSvc
}
