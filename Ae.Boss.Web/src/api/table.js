import request2 from '@/utils/request2'

export function getList(params) {
  return request2({
    url: '/Demo/GetShopInfo',
    method: 'get',
    params
  })
}

export function UpdateShopInfo(data) {
  return request2({
    url: '/Demo/UpdateShopInfo',
    method: 'post',
    data
  })
}