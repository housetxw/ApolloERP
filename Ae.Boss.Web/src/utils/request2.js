import axios from 'axios'
import {
  MessageBox,
  Message
} from 'element-ui'
import store from '@/store'
import {
  getToken
} from '@/utils/auth'

// create an axios instance
const service = axios.create({
  baseURL: '', // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 60000 // request timeout
})

// request interceptor
service.interceptors.request.use(
  (config, request) => {
    if (config.method === 'post') {
      if (config.data) {
        const args = config.data
        config.data = {
          channel: 'b-pc',
          apiVersion: '1.0',
          deviceId: 'string',
          data: args
        }
      } else {
        config.data = {
          channel: 'b-pc',
          apiVersion: '1.0',
          deviceId: 'string',
          data: {}
        }
      }
    } else if (config.method === 'get') {
      if (config.params) {
        const args = config.params
        config.params = {
          ...args,
          channel: 'b-pc',
          apiVersion: '1.0',
          deviceId: 'string'
        }
      } else {
        config.params = {
          channel: 'b-pc',
          apiVersion: '1.0',
          deviceId: 'string'
        }
      }
    }
    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation

      config.headers['Authorization'] = 'Bearer ' + JSON.parse(getToken()).accessToken
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
   */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data
    // if the custom code is not 20000, it is judged as an error.
    if (res.code !== 10000) {
      // Message({
      //   message: res.message || 'Error',
      //   type: 'error',
      //   duration: 5 * 1000
      // })
      MessageBox.alert(res.message || 'Error', "警告", {
        confirmButtonText: "关闭",
        type: "error"
      })

      // 50008: Illegal token; 50012: Other clients logged in; 50014: Token expired;
      if (res.code === 50008 || res.code === 50012 || res.code === 50014) {
        // to re-login
        MessageBox.confirm('You have been logged out, you can cancel to stay on this page, or log in again', 'Confirm logout', {
          confirmButtonText: 'Re-Login',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          store.dispatch('user/resetToken').then(() => {
            location.reload()
          })
        })
      }
      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      return res
    }

  },

  error => {
    // debugger;
    console.log('error: ' + JSON.stringify(error)) // for debug

    // 900000：noSession 900001：noPermit

    if (error.response.status === 401) {
      // Message({
      //   message: '',
      //   type: 'error',
      //   duration: 5 * 1000
      // })
      // let activeTime = 0;
      // const current = Date.now();
      // if (current - activeTime > 50000) {
      //   MessageBox.confirm('你已被登出，可以取消继续留在该页面，或者重新登录', '确定登出', {
      //     confirmButtonText: '重新登录',
      //     cancelButtonText: '取消',
      //     type: 'warning',
      //   }).then(() => {

      //   })
      //   activeTime = Date.now();
      // }
      store.dispatch('user/resetToken').then(() => {
        location.reload()
      })
    } else {
      Message({
        message: error.message,
        type: 'error',
        duration: 5 * 1000
      })
    }
    return Promise.reject(error)
  }
)

export default service
