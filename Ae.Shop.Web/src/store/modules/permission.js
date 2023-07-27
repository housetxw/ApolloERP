import {
  asyncRoutes,
  constantRoutes
} from '@/router'
import {
  GetEmpAuthorityListForShopWebByEmpId
} from '@/api/customermanage/user'
import store from './../../store'
const clientRoutes = asyncRoutes

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */

function makePermissionRouters(serverRouter, clientAsyncRoutes) {
  // let clientAsyncRoutes1 = JSON.parse(JSON.stringify(clientAsyncRoutes))
  let clientAsyncRoutesNew = clientAsyncRoutes.map(ele => {
    if (!ele.name) return
    let roles_obj
    for (let i = 0; i < serverRouter.length; i++) {
      const element = serverRouter[i]
      if (ele.name === element.path) {
        roles_obj = element
        ele.hasPermission = true
      }
    }
    if (ele.children) {
      makePermissionRouters(serverRouter, ele.children)
    }
    return ele
  })
  return clientAsyncRoutesNew

}
// function makePermissionRouters(serverRouter, clientAsyncRoutes) {
//   let clientAsyncRoutes1 = clientAsyncRoutes
//   clientAsyncRoutes1.map(ele => {
//     if (!ele.name) return
//     let roles_obj
//     for (let i = 0; i < serverRouter.length; i++) {
//       const element = serverRouter[i]
//       if (ele.name === element.path) {
//         roles_obj = element
//         ele.hasPermission = roles_obj.hasPermission
//       }
//     }
//     if (ele.children) {
//       makePermissionRouters(serverRouter, ele.children)
//     }
//   })

//   return clientAsyncRoutes1
// }

function filterFirstMenu(topMenu, clientAsyncRoutes) {
  topMenu.map(ele => {
    let roles_obj

    for (let i = 0; i < clientAsyncRoutes.length; i++) {
      const element = clientAsyncRoutes[i]
      if (ele.title === element.meta.title) {
        roles_obj = element
        ele.path = roles_obj.name
      }
    }
  })
  return topMenu
}

function hasPermission(route) {
  if (route.hasPermission) {
    return true
  } else {
    return false
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes) {
  const res = []

  routes.forEach(route => {
    const tmp = {
      ...route
    }
    if (hasPermission(tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: [],
  hasRoutes: false,
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
    state.hasRoutes = true
  },
  RESET_ROUTES: (state) => {
    state.hasRoutes = false
  }
}

const actions = {
  async generateRoutes({
    commit,
    rootGetters
  }) {
    let PermissionRouters = await GetEmpAuthorityListForShopWebByEmpId({
        EmployeeId: rootGetters.userInfo.employeeId,
        EmployeeType: rootGetters.userInfo.employeeType,
        OrganizationId: rootGetters.userInfo.organizationId,
        IsOrganizationRange: rootGetters.userInfo.isOrganizationRange
      }

    ).then(res => {
      // const data = res.data
      // const topMenu = res.data.topMenu
      const subMenu = res.data
      // let myTopMenu = filterFirstMenu(topMenu, clientRoutes)
      const data = subMenu
      // let clientRoutes1 = JSON.parse(JSON.stringify(clientRoutes))

      //开发环境时，基于本地路由
      if (process.env.NODE_ENV === "development") {
        let setPermission = (item) => {
          if (item.constructor == Array) {
            item.forEach(sub => {
              setPermission(sub);
            })
          } else {
            item.hasPermission = true;
            if (item.children) setPermission(item.children);
          }
        }
        setPermission(clientRoutes);
        PermissionRouters = makePermissionRouters(clientRoutes, clientRoutes)
      } else {
        // UT或线上环境时，基于权限配置
        PermissionRouters = makePermissionRouters(data, clientRoutes)
      }

      return PermissionRouters
    })
    return new Promise(resolve => {
      let accessedRoutes
      accessedRoutes = filterAsyncRoutes(PermissionRouters)
      accessedRoutes.push({
        path: '*',
        redirect: '/404',
        hidden: true
      })

      commit('SET_ROUTES', accessedRoutes)
      resolve(accessedRoutes)
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
