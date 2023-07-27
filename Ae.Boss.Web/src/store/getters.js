const getters = {
  sidebar: state => state.app.sidebar,
  device: state => state.app.device,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  userInfo: state => state.user.userInfo,
  cachedViews: state => state.tagsView.cachedViews,
  permission_routes: state => state.permission.routes,
}
export default getters
