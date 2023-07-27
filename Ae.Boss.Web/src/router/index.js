import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [{
  path: '/login',
  component: () => import('@/views/login/index'),
  hidden: true
},

{
  path: '/404',
  component: () => import('@/views/404'),
  hidden: true
},

{
  path: '/',
  component: Layout,
  redirect: '/dashboard',
  children: [{
    path: 'dashboard',
    name: 'Dashboard',
    component: () => import('@/views/dashboard/index'),
    meta: {
      title: '首页',
      icon: 'dashbo',
      affix: true
    }
  },
  {
    path: 'dash1',
    name: 'dash1', // component name
    hidden: true,
    component: () => import('@/views/dashboard/dash1'),
    meta: {
      title: '财务报销管理办法',
      icon: 'system'
    }
  },
  {
    path: 'dash2',
    name: 'dash2', // component name
    hidden: true,
    component: () => import('@/views/dashboard/dash2'),
    meta: {
      title: '行政管理制度',
      icon: 'system'
    }
  },
  {
    path: 'dash3',
    name: 'dash3', // component name
    hidden: true,
    component: () => import('@/views/dashboard/dash3'),
    meta: {
      title: '国庆放假通知',
      icon: 'system'
    }
  },
  {
    path: 'dash4',
    name: 'dash4', // component name
    hidden: true,
    component: () => import('@/views/dashboard/dash4'),
    meta: {
      title: '春节放假通知',
      icon: 'system'
    }
  }

  ]
},
]
export const asyncRoutes = [
  {
    path: '/orderCenter',
    component: Layout,
    redirect: '/orderCenter/orderList',
    alwaysShow: true,
    name: 'ordercenter', // component name
    meta: {
      title: '订单中心',
      icon: 'orderCenter'
    },
    children: [{
      path: 'orderList',
      name: 'orderlist', // component name
      component: () => import('@/views/orderCenter/orderList'),
      meta: {
        title: '订单列表',
        icon: 'orderlist'
      }
    },
    {
      path: 'orderDetails/:orderNo',
      name: 'orderdetails', // component name
      hidden: true,
      component: () => import('@/views/orderCenter/orderDetails'),
      meta: {
        title: '订单详情',
        icon: 'orderdetails'
      }
    },
    {
      path: 'orderMergeList',
      name: 'orderMergeList', // component name
      component: () => import('@/views/orderCenter/orderMergeList'),
      meta: {
        title: '合并支付订单列表',
        icon: 'orderlist'
      }
    },
    {
      path: 'orderpackagecardindex',
      name: 'orderpackagecardindex',
      component: () => import('@/views/orderCenter/orderpackagecardindex'),
      meta: {
        title: '套餐卡核销列表',
        icon: 'orderlist'
      }
    },
    {
      path: 'orderpackagecard',
      name: 'orderpackagecard',
      component: () => import('@/views/orderCenter/orderpackagecard'),
      meta: {
        title: '套餐卡列表',
        icon: 'orderlist'
      }
    },
    {
      path: 'newOrder',
      name: 'newOrder',
      hidden: true,
      component: () => import('@/views/orderCenter/newOrder'),
      meta: {
        title: '新创订单',
        icon: 'newOrder'
      }
    },
    {
      path: 'orderStatistics',
      name: 'orderStatistics',
      component: () => import('@/views/orderCenter/orderStatistics'),
      meta: {
        title: '订单统计',
        icon: 'orderlist'
      }
    },
    {
      path: 'orderStatisticsDetail/:dateArea',
      name: 'orderStatisticsDetail',
      hidden: true,
      component: () => import('@/views/orderCenter/orderStatisticsDetail'),
      meta: {
        title: '订单统计明细',
        icon: 'newOrder'
      }
    },
    {
      path: 'orderOutProduct',
      name: 'orderOutProduct',
      component: () => import('@/views/orderCenter/orderOutProduct'),
      meta: {
        title: '订单外采统计',
        icon: 'orderlist'
      }
    },
    {
      path: 'orderProduct',
      name: 'orderProduct',
      component: () => import('@/views/orderCenter/orderProduct'),
      meta: {
        title: '订单产品统计',
        icon: 'orderlist'
      }
    },
    ]
  },
  {
    path: '/purchase',
    component: Layout,
    alwaysShow: true,
    redirect: '/purchase/purchaseorder',
    name: 'purchase', // component name
    meta: {
      title: '采购管理系统',
      icon: 'purchase1'
    },
    children: [
     
    {
      path: 'purchaseorder',
      name: 'purchaseorder', // component name
      component: () => import('@/views/purchase/purchaseorder/purchaseorderlist'),
      meta: {
        title: '采购单订单管理',
        icon: 'purchaseorder'
      }
    },
    // {
    //   path: 'role',
    //   name: 'role', // component name
    //   component: () => import('@/views/accountauthority/authority/index'),
    //   meta: { title: '角色管理', icon: 'peoples' }
    // },
    {
      path: 'purchaseinfoindex',
      name: 'purchaseinfoindex', // component name
      component: () => import('@/views/purchase/purchaseinfo/purchaseinfoindex'),
      meta: {
        title: '采购信息配置',
        icon: 'purchaseorder'
      }
    },
    {
      path: 'venderindex',
      name: 'venderindex', // component name
      component: () => import('@/views/purchase/vender/venderindex'),
      meta: {
        title: '供应商管理',
        icon: 'purchaseorder'
      }
    },
    {
      path: 'venderproduct',
      name: 'venderproduct', // component name
      component: () => import('@/views/purchase/vender/venderproduct'),
      meta: {
        title: '供应商产品管理',
        icon: 'purchaseorder'
      }
    },
    {
      path: 'createvender',
      name: 'createvender', // component name
      component: () => import('@/views/purchase/vender/createvender'),
      meta: {
        title: '创建供应商',
        icon: 'purchaseorder'
      },
      hidden: true
    },
    {
      path: 'editvender',
      name: 'editvender', // component name
      component: () => import('@/views/purchase/vender/editvender'),
      meta: {
        title: '编辑供应商',
        icon: 'purchaseorder'
      },
      hidden: true
    },
    {
      path: 'purchasewarehouseconfigindex',
      name: 'purchasewarehouseconfigindex', // component name
      component: () => import('@/views/purchase/purchasewarehouseconfig/purchasewarehouseconfigindex'),
      meta: {
        title: '订单占库配置',
        icon: 'purchaseorder'
      }
    },
    {
      path: 'vendersetstock',
      name: 'vendersetstock', // component name
      component: () => import('@/views/purchase/vender/vendersetstock'),
      meta: {
        title: '供应商预设库存',
        icon: 'purchaseorder'
      }
    },
    {
      path: 'purchasetaskallocationindex',
      name: 'purchasetaskallocationindex', // component name
      component: () => import('@/views/purchase/purchasetaskallocation/purchasetaskallocationindex'),
      meta: {
        title: '采购任务管理',
        icon: 'purchaseorder'
      }
    }
    ]
  },
  {
    path: '/wms',
    component: Layout,
    redirect: '/wms/asn',
    name: 'wms', // component name
    meta: {
      title: '仓储管理系统',
      icon: 'wms'
    },
    children: [{
      path: 'asn',
      name: 'asn', // component name
      component: () => import('@/views/wms/asn/asnlist'),
      meta: {
        title: '入库管理',
        icon: 'asnlist'
      }
    },
    {
      path: 'warehouseindex',
      name: 'warehouseindex', // component name
      component: () => import('@/views/wms/warehouse/warehouseindex'),
      meta: {
        title: '仓库管理',
        icon: 'asnlist'
      }
    },
    {
      path: 'replenishtask',
      name: 'replenishtask', // component name
      component: () => import('@/views/wms/replenishtask/replenishtasklist'),
      meta: {
        title: '铺货出库申请',
        icon: 'replenishtasklist'
      }
    },
    {
      path: 'warehousetransfer',
      name: 'warehousetransfer', // component name
      component: () => import('@/views/wms/warehousetransfer/warehousetransfer'),
      meta: {
        title: '出库管理',
        icon: 'warehousetransfer'
      }
    },
    {
      path: 'productbarcodeindex',
      name: 'productbarcodeindex',
      component: () => import('@/views/wms/productbarcode/productbarcodeindex'),
      meta: {
        title: '产品条码列表',
        icon: 'tree'
      }
    },
    {
      path: 'allotindex',
      name: 'allotindex', // component name
      component: () => import('@/views/wms/allot/allotindex'),
      meta: {
        title: '调拨任务',
        icon: 'system'
      }
    },
    {
      path: 'storageindex',
      name: 'storageindex', // component name
      component: () => import('@/views/wms/storage/storageindex'),
      meta: {
        title: '盘库任务',
        icon: 'system'
      }
    },
    {
      path: 'storagestockdiff',
      name: 'storagestockdiff', // component name
      component: () => import('@/views/wms/storage/storagestockdiff'),
      meta: {
        title: '盘库差异',
        icon: 'system'
      }
    },
    {
      path: 'shopstockindex',
      name: 'shopstockindex', // component name
      component: () => import('@/views/stock/shopstockindex'),
      meta: {
        title: '库存查询',
        icon: 'system'
      }
    },
    {
      path: 'stocktrackindex',
      name: 'stocktrackindex', // component name
      component: () => import('@/views/stock/stocktrackindex'),
      meta: {
        title: '库存链路查询',
        icon: 'system'
      }
    },
    {
      path: 'stocktracklist',
      name: 'stocktracklist',
      component: () => import('@/views/stock/stocktracklist'),
      meta: {
        title: '仓库库存列表',
        icon: 'tree'
      },
      hidden: true
    },
    {
      path: 'stocktrackdetail',
      name: 'stocktrackdetail',
      component: () => import('@/views/stock/stocktrackdetail'),
      meta: {
        title: '库存链路明细列表',
        icon: 'tree'
      },
      hidden: true
    },
    {
      path: 'productstockindex',
      name: 'productstockindex',
      component: () => import('@/views/stock/productstock/productstockindex'),
      meta: {
        title: '产品库存列表',
        icon: 'tree'
      }
    },
    {
      path: 'productstockdetail',
      name: 'productstockdetail',
      component: () => import('@/views/stock/productstock/productstockdetail'),
      meta: {
        title: '产品库存详情',
        icon: 'tree'
      },
      hidden: true
    },
    {
      path: 'availablestockdetail',
      name: 'availablestockdetail',
      component: () => import('@/views/stock/productstock/availablestockdetail'),
      meta: {
        title: '可用库存详情',
        icon: 'tree'
      },
      hidden: true
    }
    ]
  },
  {
    path: '/fms',
    component: Layout,
    redirect: '/fms/accountcheck',
    name: 'fms', // component name
    meta: {
      title: '财务对账管理',
      icon: 'fms'
    },
    children: [{
      path: 'index',
      name: 'index', // component name
      component: () => import('@/views/fms/accountcheck/index'),
      meta: {
        title: '门店对账汇总列表',
        icon: 'purchase1'
      }
    },
    {
      path: 'accountcheckindex',
      name: 'accountcheckindex', // component name
      component: () => import('@/views/fms/accountcheck/accountcheckindex'),
      meta: {
        title: '门店对账汇总详情',
        icon: 'purchase1'
      },
      hidden: true
    },
    {
      path: 'exceptionmonitorindex',
      name: 'exceptionmonitorindex', // component name
      component: () => import('@/views/fms/accountcheck/exceptionmonitorindex'),
      meta: {
        title: '对账异常监控列表',
        icon: 'accountcheckindex(1)'
      }
    },
    {
      path: 'settlement',
      name: 'settlement', // component name
      component: () => import('@/views/fms/settlement/index'),
      meta: {
        title: '结算管理',
        icon: 'settlement'
      }
    },
    {
      path: 'detail',
      name: 'detail', // component name
      component: () => import('@/views/fms/settlement/detail'),
      meta: {
        title: '结算详情',
        icon: 'detail'
      },
      hidden: true
    }
    ]
  },
  {
    path: '/user',
    component: Layout,
    redirect: '/user/userlist',
    name: 'user', // component name
    alwaysShow: true,
    meta: {
      title: '客户管理系统',
      icon: 'user1'
    },
    children: [{
      path: 'userlist',
      name: 'userlist', // component name
      component: () => import('@/views/user/userlist'),
      meta: {
        title: '客户列表',
        icon: 'userlist'
      }
    },
    {
      path: 'userinfo/:userId',
      name: 'userinfo', // component name
      hidden: true,
      component: () => import('@/views/user/userinfo'),
      meta: {
        title: '客户详情',
        icon: 'system'
      }
    }
    ]
  },
  {
    path: '/shopmanage',
    component: Layout,
    redirect: '/shopmanage/shoplist',
    alwaysShow: true,
    name: 'shopmanage', // component name
    meta: {
      title: '门店管理系统',
      icon: 'shopmanage'
    },
    children: [{
  
      path: 'shoplist',
      name: 'shoplist', // component name
      component: () => import('@/views/shopmanage/shoplist'),
      meta: {
        title: '门店列表',
        icon: 'shoplist'
      }
    },
    {
      path: 'shopoccupymappingindex',
      name: 'shopoccupymappingindex', // component name
      component: () => import('@/views/wms/shopoccupymapping/index'),
      meta: {
        title: '门店前置仓配置',
        icon: 'system'
      }
    },
    {
      path: 'addshop',
      name: 'addshop', // component name
      hidden: true,
      component: () => import('@/views/shopmanage/addshop'),
      meta: {
        title: '新增门店',
        icon: 'system'
      }
    },
    {
      path: 'shopdetail/:id',
      name: 'shopdetail', // component name
      hidden: true,
      component: () => import('@/views/shopmanage/shopdetail'),
      meta: {
        title: '门店详情',
        icon: 'system'
      }
    },
    {
      path: 'addbaseservice',
      name: 'addbaseservice', // component name
      hidden: false,
      component: () => import('@/views/shopmanage/addbaseservice'),
      meta: {
        title: '基础服务配置',
        icon: 'addbaseservice'
      }
    },
    {
  
      path: 'shopservicelist',
      hidden: false,
      name: 'shopservicelist', // component name
      component: () => import('@/views/shopmanage/shopservicelist'),
      meta: {
        title: '门店服务配置',
        icon: 'shopservicelist'
      }
    },
    {
      path: 'shopservice',
      name: 'shopservice', // component name
      hidden: true,
      component: () => import('@/views/shopmanage/shopservice'),
      meta: {
        title: '门店服务详情',
        icon: 'system'
      }
    }, {
      path: 'shopservicetypeindex',
      name: 'shopservicetypeindex', // component name
      component: () => import('@/views/shopmanage/shopservicetypeindex'),
      meta: {
        title: '门店服务类型管理',
        icon: 'system'
      }
  
    }, {
      path: 'shophistory/:ShopId',
      name: 'shophistory', // component name
      component: () => import('@/views/shopmanage/shophistory'),
      meta: {
        title: '门店操作历史',
        icon: 'system'
      },
      hidden: true,
    },
    {
      path: 'shopproductstatemanage',
      name: 'shopproductstatemanage',
      component: () => import('@/views/productmanage/shopproduct/shopproductstatemanage'),
      meta: {
        title: '项目上架审核管理',
        icon: 'tree'
      }
    },
    {
      path: 'joinlist',
      name: 'joinlist',
      component: () => import('@/views/shopmanage/joinlist'),
      meta: {
        title: '官网加盟列表',
        icon: 'tree'
      }
    }
    ]
  },
  {
    path: "/marketingconfig",
    component: Layout,
    redirect: '/marketingconfig/baoyang',
    alwaysShow: true,
    name: "marketingconfig", // component name
    meta: {
      title: "营销配置",
      icon: "system"
    },
    children: [
      {
        path: 'shouye',
        name: 'shouye', // component name
        component: () => import('@/views/baoyang/index'),
        meta: {
          title: '首页配置',
          icon: '分层配置'
        },
        children: [
          {
            path: 'promotebannerindex',
            name: 'promotebannerindex', // component name
            component: () => import('@/views/productmanage/promotion/promotebannerpage/promotebannerindex'),
            meta: {
              title: '促销Banner配置',
              icon: 'system'
            }
          },
          {
            path: 'flashsaleconfig',
            name: 'flashsaleconfig', // component name
            component: () => import('@/views/productmanage/flashsale/flashsaleconfig'),
            meta: {
              title: '秒杀配置',
              icon: 'system'
            }
          },
          {
            path: 'hotproduct',
            name: 'hotproduct',
            component: () => import('@/views/productmanage/product/hotproduct'),
            meta: {
              title: '热销产品配置',
              icon: 'system'
            }
          },
          {
            path: 'packagecard',
            name: 'packagecard',
            component: () => import('@/views/productmanage/product/packagecard'),
            meta: {
              title: '套餐卡配置',
              icon: 'system'
            }
          },
          {
            path: 'giftcouponconfig',
            name: 'giftcouponconfig',
            component: () => import('@/views/productmanage/product/giftcouponconfig'),
            meta: {
              title: '消费赠券活动',
              icon: 'system'
            }
          }
        ]
      },
      // {
      //   path: 'baoyang',
      //   name: 'baoyang', // component name
      //   component: () => import('@/views/baoyang/index'),
      //   meta: {
      //     title: '保养配置',
      //     icon: '分层配置'
      //   },
      //   children: [{
      //     path: 'packageconfig',
      //     name: 'packageconfig', // component name
      //     component: () => import('@/views/baoyang/packageconfig'),
      //     meta: {
      //       title: 'PackageType配置',
      //       icon: 'system'
      //     }
      //   },
      //   {
      //     path: 'baoyangtypeconfig',
      //     name: 'baoyangtypeconfig', // component name
      //     component: () => import('@/views/baoyang/baoyangtypeconfig'),
      //     meta: {
      //       title: 'BaoYangType配置',
      //       icon: 'system'
      //     }
      //   }
      //   ]
      // },
      {
        path: 'shop',
        name: 'shop', // component name
        component: () => import('@/views/baoyang/index'),
        meta: {
          title: '门店营销',
          icon: '分层配置'
        },
        children: [
          {
            path: 'grouponproduct',
            name: 'grouponproduct',
            component: () => import('@/views/productmanage/product/grouponproduct'),
            meta: {
              title: '美容团购产品',
              icon: 'system'
            }
          },
          {
            path: 'shopproductmanage',
            name: 'shopproductmanage',
            component: () => import('@/views/productmanage/shopproduct/shopproductmanage'),
            meta: {
              title: '门店项目管理',
              icon: 'tree'
            }
          },
          {
            path: 'promotionactivitylist',
            name: 'promotionactivitylist',
            component: () => import('@/views/productmanage/promotion/promotionactivitylist/index'),
            meta: {
              title: '促销活动列表',
              icon: 'system'
            }
          },
          {
            path: 'promotiondetail/:activityId',
            name: 'promotiondetail', // component name
            hidden: true,
            component: () => import('@/views/productmanage/promotion/promotiondetail/index'),
            meta: {
              title: '促销详情',
              icon: 'system'
            }
          },
          {
            path: 'addpromotion',
            name: 'addpromotion',
            hidden: true,
            component: () => import('@/views/productmanage/promotion/addpromotion/index'),
            meta: {
              title: '新增促销',
              icon: 'system'
            }
          },
          {
            path: 'shopgrantcouponlist',
            name: 'shopgrantcouponlist',
            component: () => import('@/views/productmanage/promotion/shopgrantcouponlist/index'),
            meta: {
              title: '门店优惠卷配置',
              icon: 'system'
            }
          },
          {
            path: 'shoppackagecardlist',
            name: 'shoppackagecardlist',
            component: () => import('@/views/productmanage/promotion/shoppackagecardlist/index'),
            meta: {
              title: '门店套餐卡配置',
              icon: 'system'
            }
          },
        ]
      }
    ]
  },
  {
    path: '/verificationCode',
    component: Layout,
    redirect: '/verificationCode/configIndex',
    name: 'verificationCode', // component name
    meta: {
      title: '核销码',
      icon: 'system'
    },
    alwaysShow: true,
    children: [
      {
        path: 'configIndex',
        name: 'configIndex', // component name
        component: () => import('@/views/verificationCode/configIndex'),
        meta: {
          title: '规则配置',
          icon: 'system'
        }
      }
    ]
  },
  {
    path: '/product',
    component: Layout,
    alwaysShow: true,
    redirect: '/product/productsearch',
    name: 'product',
    meta: {
      title: '产品中心',
      icon: 'table'
    },
    children: [{
      path: 'productsearch',
      name: 'productsearch',
      component: () => import('@/views/productmanage/product/productsearch'),
      meta: {
        title: '产品管理',
        icon: 'tree'
      }
    },
    {
      path: 'productedit/:productCode/:classType/:isEdit',
      name: 'productedit',
      component: () => import('@/views/productmanage/product/productedit'),
      meta: {
        title: '产品编辑',
        icon: 'tree'
      },
      hidden: true
    },
    {
      path: 'brandmanage',
      name: 'brandmanage',
      component: () => import('@/views/productmanage/brand/brandmanage'),
      meta: {
        title: '品牌管理',
        icon: 'system'
      },
      hidden: false
    },
    {
      path: 'unitmanage',
      name: 'unitmanage',
      component: () => import('@/views/productmanage/unit/unitmanage'),
      meta: {
        title: '单位管理',
        icon: 'system'
      },
      hidden: false
    },
    {
      path: 'categorymanage',
      name: 'categorymanage',
      component: () => import('@/views/productmanage/category/categorymanage'),
      meta: {
        title: '类目管理',
        icon: 'system'
      }
    },
    // {
    //   path: 'attributemanage',
    //   name: 'attributemanage',
    //   component: () => import('@/views/productmanage/attribute/attributemanage'),
    //   meta: {
    //     title: '属性管理',
    //     icon: 'system'
    //   }
    // },
    // {
    //   path: 'categoryattributemanage',
    //   name: 'categoryattributemanage',
    //   component: () => import('@/views/productmanage/attribute/categoryattributemanage'),
    //   meta: {
    //     title: '分类属性管理',
    //     icon: 'system'
    //   }
    // },
  
    // {
    //   path: 'installservice',
    //   name: 'installservice',
    //   component: () => import('@/views/productmanage/product/installservice'),
    //   meta: {
    //     title: '安装服务配置',
    //     icon: 'system'
    //   }
    // },
    // {
    //   path: 'addinstallfee',
    //   name: 'addinstallfee',
    //   hidden: true,
    //   component: () => import('@/views/baoyang/addinstallfee'),
    //   meta: {
    //     title: '服务加价配置',
    //     icon: 'system'
    //   }
    // },
    // {
    //   path: 'addinstallfeevehicle',
    //   name: 'addinstallfeevehicle',
    //   component: () => import('@/views/baoyang/addinstallfeevehicle'),
    //   meta: {
    //     title: '服务费加价',
    //     icon: 'system'
    //   }
    // },
    // {
    //   path: 'doorproduct',
    //   name: 'doorproduct',
    //   component: () => import('@/views/productmanage/product/doorproduct'),
    //   meta: {
    //     title: '上门产品管理',
    //     icon: 'system'
    //   }
    // },
      // {
      //   path:'promotion',
      //   name:'Promotion',
      //   component: () => import('@/views/productmanage/promotion/index'),
      //   meta: { title: '促销活动配置', icon: 'tree' },
      //   children:[
      //     {
      //       path:'promotionactivitylist',
      //       name:'PromotionActivityList',
      //       component:()=> import('@/views/productmanage/promotion/promotionactivitylist/index'),
      //       meta:{title:'促销活动列表',icon:'system'}
      //     },
      //     {
      //       path: 'promotiondetail/:activityId',
      //       name: 'PromotionDetail', // component name
      //       hidden: true,
      //       component: () => import('@/views/productmanage/promotion/promotiondetail/index'),
      //       meta: { title: '促销详情', icon: 'system' }
      //     },
      //     {
      //       path:'addpromotion',
      //       name:'AddPromotion',
      //       hidden:true,
      //       component: () => import('@/views/productmanage/promotion/addpromotion/index'),
      //       meta:{title:'新增促销',icon:'system'}
      //     }
      //   ]
      // }
    ]
  },
  {
    path: '/coupon',
    component: Layout,
    alwaysShow: true,
    redirect: '/coupon/rulelist',
    name: 'coupon',
    meta: {
      title: '优惠券管理系统',
      icon: 'table'
    },
    children: [{
      path: 'rulelist',
      name: 'rulelist', // component name
      component: () => import('@/views/coupon/rulelist.vue'),
      meta: {
        title: '优惠券设置',
        icon: 'system'
      }
    },
    {
      path: 'activitylist',
      name: 'activitylist', // component name
      component: () => import('@/views/coupon/activitylist.vue'),
      meta: {
        title: '活动配置',
        icon: 'system'
      }
    },
    {
      path: 'grantrecord',
      name: 'grantrecord', // component name
      component: () => import('@/views/coupon/grantrecord.vue'),
      meta: {
        title: '发放记录',
        icon: 'system'
      }
    },
    {
      path: 'decapaward',
      name: 'decapaward', // component name
      component: () => import('@/views/coupon/decapaward.vue'),
      meta: {
        title: '开瓶有奖',
        icon: 'system'
      }
    }
    ]
  },
  {
    path: '/bosscompany',
    component: Layout,
    redirect: '/shopmanage/companylist',
    name: 'bosscompany',
    alwaysShow: true,
    meta: {
      title: '公司管理',
      icon: 'system'
    },
    children: [{
      path: 'bosscompanylist',
      name: 'bosscompanylist', // component name
      component: () => import('@/views/shopmanage/companylist'),
      meta: {
        title: '公司列表',
        icon: 'system'
      }
    }]
  },
  // {
  //   path: "/activity",
  //   component: Layout,
  //   redirect: "/activity/promote",
  //   alwaysShow: true,
  //   name: "activity", // component name
  //   meta: {
  //     title: "推广内容管理",
  //     icon: "shopmanage"
  //   },
  //   children: [{
  //     path: "article",
  //     name: "article", // component name
  //     component: () => import("@/views/activity/promote/article"),
  //     meta: {
  //       title: "文章转载管理",
  //       icon: "faqlist"
  //     }
  //   },
  //   {
  //     path: "poster",
  //     name: "poster", // component name
  //     component: () => import("@/views/activity/promote/poster"),
  //     meta: {
  //       title: "营销海报管理",
  //       icon: "faqlist"
  //     }
  //   },
  //   {
  //     path: "friendjokes",
  //     name: "friendjokes", // component name
  //     component: () => import("@/views/activity/promote/friendjokes"),
  //     meta: {
  //       title: "朋友圈段子管理",
  //       icon: "faqlist"
  //     }
  //   },
  //   {
  //     path: "createfriendjokes",
  //     name: "createfriendjokes", // component name
  //     component: () => import("@/views/activity/promote/createfriendjokes"),
  //     meta: {
  //       title: "新建朋友圈段子",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "editfriendjokes",
  //     name: "editfriendjokes", // component name
  //     component: () => import("@/views/activity/promote/editfriendjokes"),
  //     meta: {
  //       title: "编辑朋友圈段子",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "editarticle",
  //     name: "editarticle", // component name
  //     component: () => import("@/views/activity/promote/editarticle"),
  //     meta: {
  //       title: "文章转载详情",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "createarticle",
  //     name: "createarticle", // component name
  //     component: () => import("@/views/activity/promote/createarticle"),
  //     meta: {
  //       title: "创建文章转载",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "reviewarticle",
  //     name: "reviewarticle", // component name
  //     component: () => import("@/views/activity/promote/reviewarticle"),
  //     meta: {
  //       title: "预览文章",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "installguideindex",
  //     name: "installguideindex", // component name
  //     component: () => import("@/views/activity/installguide/installguideindex"),
  //     meta: {
  //       title: "安装指导配置",
  //       icon: "faqlist"
  //     }
  //   },
  //   {
  //     path: "addinstallguide",
  //     name: "addinstallguide", // component name
  //     component: () => import("@/views/activity/installguide/addinstallguide"),
  //     meta: {
  //       title: "新增安装指导",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   },
  //   {
  //     path: "updateinstallguide",
  //     name: "updateinstallguide", // component name
  //     component: () => import("@/views/activity/installguide/updateinstallguide"),
  //     meta: {
  //       title: "更改安装指导",
  //       icon: "faqlist"
  //     },
  //     hidden: true
  //   }
  //   ]
  // },
  {
    path: '/ordercommment',
    component: Layout,
    alwaysShow: true,
    redirect: '/components/ordercommentindex',
    name: 'ordercommment', // component name
    meta: {
      title: '评论管理系统',
      icon: 'ordercommment'
    },
    children: [{
      path: 'ordercommentindex',
      name: 'ordercommentindex', // component name
      component: () => import('@/views/ordercomment/ordercommentindex'),
      meta: {
        title: '评论管理',
        icon: 'system'
      }
    }]
  },
  {
    path: '/faq',
    component: Layout,
    redirect: '/shopmanage1/faqlist',
    alwaysShow: true,
    name: 'faq', // component name
    meta: {
      title: '客服系统',
      icon: 'shopmanage'
    },
    children: [{
  
      path: 'faqlist',
      name: 'faqlist', // component name
      component: () => import('@/views/shopmanage/faqlist'),
      meta: {
        title: '客服FAQ列表',
        icon: 'faqlist'
      }
    },
    {
  
      path: 'faqinfo/:id',
      name: 'faqinfo', // component name
      hidden: true,
      component: () => import('@/views/shopmanage/faqinfo'),
      meta: {
        title: 'FAQ详情',
        icon: 'faqlist'
      }
    },
    ]
  },
  {
    path: '/aftersale',
    component: Layout,
    alwaysShow: true,
    redirect: '/aftersale/tousu',
    name: 'aftersale', // component name
    meta: {
      title: '售后管理系统',
      icon: 'aftersale'
    },
    children: [{
      path: 'index',
      name: 'index', // component name
      component: () => import('@/views/aftersale/tousu/index'),
      meta: {
        title: '投诉管理',
        icon: 'tousu'
      }
    },
    {
      path: 'tousudetail',
      name: 'tousudetail',
      component: () => import('@/views/aftersale/tousu/tousudetail'),
      meta: {
        title: '投诉详情',
        icon: 'tree'
      },
      hidden: true
    },
    {
      path: 'createtousu',
      name: 'createtousu',
      component: () => import('@/views/aftersale/tousu/createtousu'),
      meta: {
        title: '创建投诉',
        icon: 'tree'
      },
      hidden: true
    }
    ]
  },
  // {
  //   path: "/baoyang",
  //   component: Layout,
  //   redirect: '/baoyang/adaptationsearch',
  //   name: 'baoyang', // component name
  //   alwaysShow: true,
  //   meta: {
  //     title: '保养适配系统',
  //     icon: 'system'
  //   },
  //   children: [{
  //     path: 'maintainoemapping',
  //     name: 'maintainoemapping', // component name
  //     component: () => import('@/views/baoyang/maintainoemapping'),
  //     meta: {
  //       title: '保养OE映射关系',
  //       icon: 'system'
  //     }
  //   },
  //   {
  //     path: 'adaptationsearch',
  //     name: 'adaptationsearch', // component name
  //     component: () => import('@/views/baoyang/adaptationsearch'),
  //     meta: {
  //       title: '保养配件适配查询',
  //       icon: 'system'
  //     }
  //   },
  //   {
  //     path: 'partacessory',
  //     name: 'partacessory', // component name
  //     component: () => import('@/views/baoyang/partacessory'),
  //     meta: {
  //       title: '保养油液适配查询',
  //       icon: 'system'
  //     }
  //   },
  //   {
  //     path: 'taocan',
  //     name: 'taocan', // component name
  //     component: () => import('@/views/baoyang/taocan'),
  //     meta: {
  //       title: '保养套餐查询',
  //       icon: 'system'
  //     }
  //   },
  //   {
  //     path: 'propertyadaptation',
  //     name: 'propertyadaptation',
  //     component: () => import('@/views/baoyang/propertyadaptation'),
  //     meta: {
  //       title: '保养五级属性适配',
  //       icon: 'system'
  //     }
  //   },
  //   {
  //     path: 'productref',
  //     name: 'productref',
  //     component: () => import('@/views/baoyang/productref'),
  //     meta: {
  //       title: '配件号关联产品',
  //       icon: 'system'
  //     }
  //   },
      // {
      //   path: 'packageconfig',
      //   name: 'packageconfig',
      //   component: () => import('@/views/baoyang/packageconfig'),
      //   meta: {
      //     title: 'PackageType配置',
      //     icon: 'system'
      //   }
      // },
      // {
      //   path:'baoyangtypeconfig',
      //   name:'baoyangtypeconfig',
      //   component: () => import('@/views/baoyang/baoyangtypeconfig'),
      //   meta: {
      //     title: 'BaoYangType配置',
      //     icon: 'system'
      //   }
      // }
      // {
      //   path: 'brandmanage',
      //   name: 'BrandManage',
      //   component: () => import('@/views/baoyang/vehicle/brandmanage'),
      //   meta: { title: '车型品牌管理', icon: 'system' }
      // },
      // {
      //   path: 'seriesmanage',
      //   name: 'SeriesManage',
      //   component: () => import('@/views/baoyang/vehicle/seriesmanage'),
      //   meta: { title: '二级车型管理', icon: 'system' }
      // },
      // {
      //   path: 'modelsmanage',
      //   name: 'modelsmanage',
      //   component: () => import('@/views/baoyang/vehicle/modelsmanage'),
      //   meta: { title: '五级车型管理', icon: 'system' }
      // },
      // {
      //   path: 'tidlist',
      //   name: 'TidList',
      //   component: () => import('@/views/baoyang/vehicle/tidlist'),
      //   meta: {
      //     title: '五级Tid查询',
      //     icon: 'system'
      //   }
      // }
  //   ]
  // },
  {
    path: '/accountauthority',
    component: Layout,
    redirect: '/accountauthority/application',
    name: 'accountauthority', // component name
    meta: {
      title: '账号权限管理',
      icon: 'accountauthority1'
    },
    children: [{
      path: 'application',
      name: 'application', // component name
      component: () => import('@/views/accountauthority/application/index'),
      meta: {
        title: '系统管理',
        icon: 'application'
      }
    },
    {
      path: 'account',
      name: 'account', // component name
      component: () => import('@/views/accountauthority/account/index'),
      meta: {
        title: '账号管理',
        icon: 'shopmanage'
      }
    },
    {
      path: 'employee',
      name: 'employee', // component name
      component: () => import('@/views/accountauthority/employee/index'),
      meta: {
        title: '员工管理',
        icon: 'employeerole'
      }
    },
    {
      path: 'authorization',
      name: 'authorization', // component name
      component: () => import('@/views/accountauthority/authorization/index'),
      meta: {
        title: '授权管理',
        icon: 'authorization'
      },
      children: [{
        path: 'employeerole',
        name: 'employeerole', // component name
        component: () => import('@/views/accountauthority/authorization/employeerole/index'),
        meta: {
          title: '员工角色',
          icon: 'employeerole'
        }
      },
      {
        path: 'role',
        name: 'role', // component name
        component: () => import('@/views/accountauthority/authorization/role/index'),
        meta: {
          title: '角色管理',
          icon: 'role'
        }
      },
      {
        path: 'authority',
        name: 'authority', // component name
        component: () => import('@/views/accountauthority/authorization/authority/index'),
        meta: {
          title: '权限管理',
          icon: 'authority'
        }
      }
      ]
    }
    ]
  },
  {
    path: '/personalcenter',
    component: Layout,
    redirect: '/personalcenter/chanagepassword',
    name: 'personalcenter', // component name
    meta: {
      title: '个人中心',
      icon: 'accountauthority1'
    },
    alwaysShow: true,
    children: [
      // {
      //   path: 'basicinfo',
      //   name: 'basicinfo', // component name
      //   component: () => import('@/views/personalcenter/basicinfo'),
      //   meta: { title: '基本信息', icon: 'application' }
      // },
      {
        path: 'chanagepassword',
        name: 'chanagepassword', // component name
        component: () => import('@/views/personalcenter/chanagepassword'),
        meta: {
          title: '修改密码',
          icon: 'shopmanage'
        }
      }
    ]
  }
  //END personalcenter
  // 404 page must be placed at the end !!!
  // {
  //   path: '*',
  //   redirect: '/404',
  //   hidden: true
  // },
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({
    y: 0
  }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
