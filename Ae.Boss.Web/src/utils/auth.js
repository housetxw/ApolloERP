import Cookies from 'js-cookie'

const TokenKey = 'vue_admin_template_token'
const UserKey = 'vue_admin_template_user'

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  return Cookies.set(TokenKey, token)
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}

export function setUserInfo(token) {
  return Cookies.set(UserKey, token)
}

export function getUserInfo() {
  return Cookies.get(UserKey)
}