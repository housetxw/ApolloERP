import { setError } from './error'

function formatNumber (n) {
  const str = n.toString()
  return str[1] ? str : `0${str}`
}

export function formatTime (date) {
  const year = date.getFullYear()
  const month = date.getMonth() + 1
  const day = date.getDate()

  const hour = date.getHours()
  const minute = date.getMinutes()
  const second = date.getSeconds()

  const t1 = [year, month, day].map(formatNumber).join('/')
  const t2 = [hour, minute, second].map(formatNumber).join(':')

  return `${t1} ${t2}`
}
export function handleError (response) {
  if (response && response.data && response.data.error_code === 0) {
    return true
  } else {
    const msg = response && response.data && response.data.msg
    if (msg) {
      showToast(msg)
    } else {
      setError('数据加载失败，请重试')
    }
    return false
  }
}

export function showToast (title, success = false) {
  success ? mpvue.showToast({ title })
    : mpvue.showToast({ title, icon: 'none' })
}

export default {
  formatNumber,
  formatTime
}
