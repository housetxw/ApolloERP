import store from '../store'

// vue级别异常捕获
export function errorHandler (err, vm, info) {
  // 打印异常数据
  if (console && console.log) {
    console.log('errorHandler', err, info)
  }
  // 向子组件分发异常处理事件
  if (err && err.message) {
    setError(err.message)
  } else {
    setError('程序猿紧急抢修中...')
  }
}

// 应用级异常捕获
export function onError (err) {
  console.log('wx.onError', err)
  // 向子组件分发异常处理事件
  if (err) {
    // 尝试截取错误信息，进行精准展示
    try {
      const errMessage = err.split('\n')[1].split(';')[0].trim()
      if (errMessage && errMessage.length > 0) {
        setError(errMessage)
      } else {
        setError('非常抱歉，程序出现异常')
      }
    } catch (e) {
      setError('非常抱歉，程序出现异常')
    }
  } else {
    setError('程序猿紧急抢修中...')
  }
}

// 想Vuex发送错误通知
export function setError (message) {
  mpvue.hideLoading()
  store.commit('setError', message)
}
