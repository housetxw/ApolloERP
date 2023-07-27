import {
  LoginAsync,
  logout,
  getInfo,
  GetOrgPageListByAuthCodeAsync,
  GetTokenInfoByAuthCode,
  LoginWithSMS
} from '@/api/customermanage/user'
import {
  getToken,
  setToken,
  setUserInfo,
  removeToken
} from '@/utils/auth'
import {
  resetRouter
} from '@/router'

const state = {
  token: getToken(),
  userInfo: ''
}
let authCode = ''
const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_USER: (state, userInfo) => {
    state.userInfo = userInfo
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  }
}

const actions = {
  // user login
  login({
    commit
  }, userInfo) {
    const {
      username,
      password
    } = userInfo
    return new Promise((resolve, reject) => {
      LoginAsync({
        name: username.trim(),
        password: password,
        employeeType: "None",
        platform: "Shop"
      }).then(response => {
        const {
          data
        } = response
        authCode = data.authCode
        if (data.orgItems.length === 1) {
          commit('SET_TOKEN', data.token)
          commit('SET_USER', data.orgItems[0])
          commit('SET_AVATAR', 'https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif')
          setToken(data.token)
          setUserInfo(data.orgItems[0])
          resolve()
        } else {
          GetOrgPageListByAuthCodeAsync({
            "authCode": authCode,
            "employeeType": "None",
            "pageIndex": 1,
            "pageSize": 2
          }).then(res => {
            const {
              data
            } = res
            resolve(data)
          })
        }

      }).catch(error => {
        reject(error)
      })
    })

  },
  loginWithSMS({
    commit
  }, userInfo) {
    console.log(123456)
    const {
      username,
      password
    } = userInfo
    return new Promise((resolve, reject) => {
      LoginWithSMS({
        name: username.trim(),
        verificationCode: password,
        smsType: 'Login',
        employeeType: "None",
        platform: "Shop"
      }).then(response => {
        const {
          data
        } = response
        authCode = data.authCode
        if (data.orgItems.length === 1) {
          commit('SET_TOKEN', data.token)
          commit('SET_USER', data.orgItems[0])
          commit('SET_AVATAR', 'https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif')
          setToken(data.token)
          setUserInfo(data.orgItems[0])
          resolve()
        } else {
          GetOrgPageListByAuthCodeAsync({
            "authCode": authCode,
            "employeeType": "None",
            "pageIndex": 1,
            "pageSize": 2
          }).then(res => {
            const {
              data
            } = res
            resolve(data)
          })

        }

      }).catch(error => {
        reject(error)
      })
    })

  },
  setTokenInfoByAuthCode({
    commit
  }, item) {
    const {
      employeeId,
      organizationId,
      employeeName,
      employeeType
    } = item
    return new Promise((resolve, reject) => {
      GetTokenInfoByAuthCode({
        authCode: authCode,
        employeeId,
        employeeName,
        organizationId,
        employeeType
      }).then(response => {
        const {
          data
        } = response
        commit('SET_TOKEN', data)
        // console.log(989898989898, item)
        // JSON.parse(JSON.stringify(item.userInfo))
        commit('SET_USER', JSON.parse(JSON.stringify(item)))

        commit('SET_AVATAR', 'https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif')
        setUserInfo(item)
        setToken(data)

        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  // get user info
  getInfo({
    commit,
    state
  }) {
    getUserInfo()

    return new Promise((resolve, reject) => {
      getInfo(state.token).then(response => {
        const {
          data
        } = response

        if (!data) {
          reject('Verification failed, please Login again.')
        }

        const {
          name,
          avatar
        } = data

        commit('SET_NAME', name)
        commit('SET_AVATAR', avatar)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({
    commit,
    state
  }) {
    return new Promise((resolve, reject) => {
      console.log(5556666)
      commit('SET_TOKEN', '')
      removeToken()
      resetRouter()
      resolve()

    })
  },

  // remove token
  resetToken({
    commit
  }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      removeToken()
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
