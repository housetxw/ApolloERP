import {
  RefreshToken
} from '../api'
let needLoadingRequestCount = 0
// loading
function showFullScreenLoading () {
  if (needLoadingRequestCount === 0) {
    wx.showToast({
      title: '加载中...',
      icon: 'loading',
      duration: 20000
    })
  }
  needLoadingRequestCount++
}
// // 获取当前页面栈
// const currentPages = getCurrentPages() || []
// const getCurrentPage = needOpts => {
//   if (currentPages.length) {
//     const currentPage = currentPages.pop()
//     if (!currentPage) return ''
//     const pagePath = currentPage.__route__
//     let pageOptions = ''
//     if (needOpts) {
//       const arr = []
//       Object.keys(currentPage.options).forEach(prop => {
//         arr.push({
//           key: prop,
//           value: currentPage.options[prop]
//         })
//       })
//       arr.forEach((item, index) => {
//         if (index === arr.length - 1) {
//           pageOptions += `${item.key}=${item.value}`
//         } else {
//           pageOptions += `${item.key}=${item.value}&`
//         }
//       })
//     }
//     return `/${pagePath}?${pageOptions}`
//   }
//   return ''
// }

function tryHideFullScreenLoading () {
  if (needLoadingRequestCount <= 0) return
  needLoadingRequestCount--
  if (needLoadingRequestCount === 0) {
    wx.hideToast()
  }
}

function createFly () {
  if (mpvuePlatform === 'wx') {
    const Fly = require('flyio/dist/npm/wx')
    const myFly = new Fly()
    myFly.config.timeout = 50000
    myFly.interceptors.request.use(request => {
      if (!request.hideLoad) {
        showFullScreenLoading()
      }

      if (wx.getStorageSync('tokenInfo') && wx.getStorageSync('tokenInfo') !== '') {
        request.headers = {
          Authorization: 'Bearer ' + wx.getStorageSync('tokenInfo').accessToken
        }
      }
    })
    // 添加响应拦截器，响应拦截器会在then/catch处理之前执行
    // 10000, "成功"; 99999, "失败"; 30200, "已存在相同的名称和备注"; 30101, "数据不存在";
    myFly.interceptors.response.use(
      response => {
        // 只将请求结果的data字段返回
        tryHideFullScreenLoading()
        // console.log(
        //   'fly response ===>',
        //   response.request.url,
        //   response.data,
        //   response
        // )
        const statusCode = response.engine.status
        // 对有code的进行code检查
        if (statusCode === 200) {
          if (response.request.showMsg && response.data.code !== 10000) {
            wx.showToast({
              title: `${response.data.message || '数据异常'}`,
              icon: 'none'
            })
          }
          return response.data
        }
        if (statusCode === 401) {
          console.log(401)
          wx.showToast({
            title: `${statusCode}，${response.data.message || ''}`,
            icon: 'none'
          })
        } else {
          return Promise.reject(response.data)
        }
        return response.data
      },
      res => {
        // 发生网络错误后会走到这里
        tryHideFullScreenLoading()
        const statusCode = res.engine.status
        if (statusCode === 401) {
          // wx.navigateTo({
          //   url: '/pages/authUserInfo/main'
          // })
          wx.showToast({
            title: "请登录后查看",
            icon: 'none',
            duration: 2000,
          })
        } else {
          return Promise.reject(res)
        }
        return res
      }
    )
    return myFly
  } else if (mpvuePlatform === 'my') {
    const Fly = require('flyio/dist/npm/ap')
    return new Fly()
  } else {
    return null
  }
}

export function get (url, params = {}, hideLoad = false, showMsg = true) {
  const fly = createFly()
  if (fly) {
    return new Promise((resolve, reject) => {
      let args = params
      params = {
        ...args,
        channel: 'miniAPP',
        apiVersion: '1.0',
        deviceId: 'string'
      }
      fly
        .get(url, params, {
          hideLoad,
          showMsg
        })
        .then(response => {
          resolve(response)
        })
        .catch(err => {
          if (err.status === 401) {
            // RefreshToken({
            //   refreshToken: wx.getStorageSync('tokenInfo').refreshToken
            // }).then((res) => {
            //   wx.setStorageSync('tokenInfo', res.data)
            //   console.log(1116677)
            //   get(url, params = {}, hideLoad = false, showMsg = true)
            // }).catch(() => {
            //   wx.redirectTo({
            //     url: '/pages/authUserInfo/main'
            //   })
            // })
          }
          console.log('封装的get错误', err)
          // handleError(err)
          reject(err)
        })
    })
  }
}

export function post (url, params = {}, hideLoad = false, showMsg = true) {
  const fly = createFly()
  if (fly) {
    return new Promise((resolve, reject) => {
      let args = params
      params = {
        channel: 'miniAPP',
        apiVersion: '1.0',
        deviceId: 'string',
        data: args
      }
      fly
        .post(url, params, {
          hideLoad,
          showMsg
        })
        .then(response => {
          console.log('封装的post返回数据', response)
          resolve(response)
        })
        .catch(err => {
          if (err.status === 401) {
            // RefreshToken({
            //   refreshToken: wx.getStorageSync('tokenInfo').refreshToken
            // }).then((res) => {
            //   wx.setStorageSync('tokenInfo', res.data)
            //   console.log(1116677)
            //   post(url, params = {}, hideLoad = false, showMsg = true)
            // }).catch(() => {
            //   wx.navigateTo({
            //     url: '/pages/authUserInfo/main'
            //   })
            // })
          }
          console.log('封装的get错误', err)
          // handleError(err)
          reject(err)
        })
    })
  }
}
