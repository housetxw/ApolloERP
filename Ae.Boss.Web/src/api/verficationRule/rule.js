
import request2 from '@/utils/request2'
const verficationRule = process.env.VUE_APP_BASE_ORDERCENTER_API

const verficationRuleSvc = {
  // 获取订单列表
  GetVerificationRule(params) {
    // debugger

    return request2({
      url: verficationRule + '/OrderQuery/GetVerificationRule',
      method: 'get',
      params
    })
  },
  // 获取首次加载订单确认页信息
  SaveVerificationRule(data) {
    return request2({
      url: verficationRule + '/OrderCommand/SaveVerificationRule',
      method: 'post',
      data
    })
  },
  // 获取首次加载订单确认页信息
  SaveBeautiyOrPackageCardVerificationProduct(data) {
    return request2({
      url: verficationRule + '/OrderCommand/SaveBeautiyOrPackageCardVerificationProduct',
      method: 'post',
      data
    })
  },

}
export {
  verficationRuleSvc
}
