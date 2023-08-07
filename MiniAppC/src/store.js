import Vue from 'vue'
import Vuex from 'vuex'
import {
  GetAllUserVehicles,
  AddUserCar,
  DeleteUserCar,
  SetUserDefaultVehicle,
  EditUserCar
} from './api'
Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    carArr: [], // 用户车型库
    globalShop: null,
    error: null,
    curCityInfo: {},
    pointCityInfo: {},
    // curCity: '上海',//默认地址
    latitude: '31.22', // 纬度
    longitude: '121.48', // 经度
    active: 0 // 我的预约界面  切换标题栏索引
  },
  mutations: {
    updateCity: (state, change) => {
      state.curCityInfo = change
    },
    setPointCity: (state, change) => {
      state.pointCityInfo = change
    },
    setError: (state, message) => {
      if (message) {
        state.error = {
          message
        }
      } else {
        state.error = null
      }
    },
    setCarArr: (state, carArr) => {
      state.carArr = JSON.parse(JSON.stringify(carArr))
    },
    setGlobalShop: (state, shop) => {
      state.globalShop = shop
    }
  },
  actions: {
    getCarArr({
      commit
    }) {
      return new Promise((resolve, reject) => {
        GetAllUserVehicles().then(
          Response => {
            commit('setCarArr', Response.data)
            resolve(Response.data)
          }
        )
      })
    },
    addCar({
      dispatch
    }, carInfo) {
      return new Promise((resolve, reject) => {
        AddUserCar(carInfo).then(
          Response => {
            dispatch('getCarArr').then(() => {
              resolve()
            })
          }
        )
      })
    },

    deleteUserCar({
      dispatch
    }, carId) {
      return new Promise((resolve, reject) => {
        DeleteUserCar({
          carId
        }).then(() => {
            dispatch('getCarArr').then(() => {
              resolve()
            })
          }

        )
      })
    },
    setUserDefaultVehicle({
      dispatch
    }, carId) {
      return new Promise((resolve, reject) => {
        SetUserDefaultVehicle({
          carId
        }).then(() => {
            dispatch('getCarArr').then(() => {
              resolve()
            })
          }

        )
      })
    },
    editUserCar({
      dispatch
    }, carInfo) {
      return new Promise((resolve, reject) => {
        EditUserCar(carInfo).then(() => {
            dispatch('getCarArr').then(() => {
              resolve()
            })
          }

        )
      })
    }
  }
})

export default store
