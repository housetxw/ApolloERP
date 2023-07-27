import * as qiniu from 'qiniu-js'

// token 找后端，obj 这里指代从 el-upload 接收到的 object
export const upload = (token, key, obj, next, error, complete) => {
  const {
    file
  } = obj
  // 因人而异，自行解决
  const putExtra = {
    fname: '',
    params: {},
    mimeType: [] || null
  }
  //       fname: string，文件原文件名
  // params: object，用来放置自定义变量
  // mimeType: null || array，用来限制上传文件类型，为 null 时表示不对文件类型限制；限制类型放到数组里： ["image/png", "image/jpeg", "image/gif"]
  const config = {
    // 上传是否使用cdn加速
    useCdnDomain: true,
    region: qiniu.region.z2,
    // 是否使用https域名
    useHttpsDomain: true
  }
//   const options = {
//     quality: 0.92,
//     noCompressIfLarger: true,
//     maxWidth: 1000,
//     maxHeight: 618
//   }
  // 调用sdk上传接口获得相应的observable，控制上传和暂停
  const observable = qiniu.upload(file, key, token, putExtra, config)
  const subscription = observable.subscribe(next, error, complete)
  return subscription

}
