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
  }
]
export const asyncRoutes = [{
    path: '/orderCenter',
    component: Layout,
    redirect: '/orderCenter/orderList',
    alwaysShow: true,
    name: 'ordercenter', // component name
    meta: {
      title: '订单管理',
      icon: 'orderCenter'
    },
    children: [{
        path: 'orderlist',
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
        path: 'installBill',
        name: 'installBill', // component name
        hidden: true,
        component: () => import('@/views/orderCenter/installBill'),
        meta: {
          title: '打印安装单',
          icon: 'installBill'
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
      }
    ]
  },
  {
    path: '/fms',
    component: Layout,
    redirect: '/fms/reconciliationList',
    alwaysShow: true,
    name: 'fms', // component name
    meta: {
      title: '财务对账',
      icon: 'purchase1'
    },
    children: [{
        path: 'reconciliationList',
        name: 'reconciliationList', // component name
        component: () => import('@/views/fms/reconciliationList'),
        meta: {
          title: '订单对账',
          icon: 'purchase1'
        }
      },
      {
        path: 'shopcostList',
        name: 'shopcostList', // component name
        component: () => import('@/views/fms/shopcostList'),
        meta: {
          title: '门店费用',
          icon: 'settlement'
        }
      },
      {
        path: 'settlementlist',
        name: 'settlementlist', // component name
        component: () => import('@/views/fms/settlementlist'),
        meta: {
          title: '门店提现',
          icon: 'settlement'
        }
      },
      {
        path: 'purchaseReconciliationList',
        name: 'purchaseReconciliationList', // component name
        component: () => import('@/views/fms/purchaseReconciliationList'),
        meta: {
          title: '供应商对账列表',
          icon: 'purchase1'
        }
      },
      {
        path: 'purchaseSettlementList',
        name: 'purchaseSettlementList', // component name
        component: () => import('@/views/fms/purchaseSettlementList'),
        meta: {
          title: '供应商结算管理',
          icon: 'purchase1'
        }
      },
    ]
  },
  {
    path: '/stockmanage',
    component: Layout,
    alwaysShow: true,
    redirect: '/stockmanage/stockpage/storageindex',
    name: 'stockmanage', // component name
    meta: {
      title: '库存管理',
      icon: 'system'
    },
    children: [
      {
        path: 'allotindex',
        name: 'allotindex', // component name
        component: () => import('@/views/stockmanage/allot/allotindex'),
        meta: {
          title: '调拨单',
          icon: 'system'
        }
      },
      {
        path: 'productclaimindex',
        name: 'productclaimindex', // component name
        component: () => import('@/views/stockmanage/productclaimindex'),
        meta: {
          title: '耗材领用单',
          icon: 'system'
        }
      },
      
      {
        path: 'storageindex',
        name: 'storageindex', // component name
        component: () => import('@/views/stockmanage/storageindex'),
        meta: {
          title: '盘点单',
          icon: 'system'
        }
      },
      {
        path: 'storagestockdiff',
        name: 'storagestockdiff', // component name
        component: () => import('@/views/stockmanage/storagestockdiff'),
        meta: {
          title: '盘盈亏单',
          icon: 'system'
        }
      },
      // {
      //   path: 'venderinitstock',
      //   name: 'venderinitstock', // component name
      //   component: () => import('@/views/stockmanage/purchasestock/venderinitstock'),
      //   meta: {
      //     title: '初始化库存',
      //     icon: 'system'
      //   }
      // },


      // {
      //   path: 'purchaseproduct',
      //   name: 'purchaseproduct', // component name
      //   component: () => import('@/views/stockmanage/purchasestock/purchaseproduct'),
      //   meta: {
      //     title: '要货列表',
      //     icon: 'system'
      //   }
      // },
      // {
      //   path: 'purchaseproductdeliveryindex',
      //   name: 'purchaseproductdeliveryindex', // component name
      //   component: () => import('@/views/stockmanage/purchasestock/purchaseproductdeliveryindex'),
      //   meta: {
      //     title: '采购发货列表',
      //     icon: 'system'
      //   }
      // },
      // {
      //   path: 'purchaseproductdreceiptindex',
      //   name: 'purchaseproductdreceiptindex', // component name
      //   component: () => import('@/views/stockmanage/purchasestock/purchaseproductdreceiptindex'),
      //   meta: {
      //     title: '采购收货列表',
      //     icon: 'system'
      //   }
      // },
      // {
      //   path: 'purchaseproductstock',
      //   name: 'purchaseproductstock', // component name
      //   component: () => import('@/views/stockmanage/purchasestock/purchaseproductstock'),
      //   meta: {
      //     title: '采购库存列表',
      //     icon: 'system'
      //   }
      // },



      // {
      //   path: 'orderoccupystock',
      //   name: 'orderoccupystock', // component name
      //   component: () => import('@/views/stockmanage/orderoccupystock'),
      //   meta: {
      //     title: '订单占库列表',
      //     icon: 'system'
      //   }
      // },
      {
        path: 'stockpage',
        name: 'stockpage', // component name
        component: () => import('@/views/stockmanage/stockpage/'),
        //  redirect:'/stockmanage/stockpage/viewstock/index',
        meta: {
          title: '库存查看',
          icon: 'system'
        },
        children: [{
            path: 'viewstock',
            name: 'viewstock', // component name
            component: () => import('@/views/stockmanage/stockpage/viewstock/index'),
            meta: {
              title: '当前库存',
              icon: 'system'
            }
          },
          {
            path: 'stockinout',
            name: 'stockinout', // component name
            component: () => import('@/views/stockmanage/stockpage/stockinout/index'),
            meta: {
              title: '出入库列表',
              icon: 'system'
            }
          }
        ]
      },
      {
        path: 'homecarepage',
        name: 'homecarepage', // component name
        component: () => import('@/views/stockmanage/homecarepage/'),
        //  redirect:'/stockmanage/stockpage/viewstock/index',
        meta: {
          title: '上门保养',
          icon: 'system'
        },
        children: [{
            path: 'techreceiveproduct',
            name: 'techreceiveproduct', // component name
            component: () => import('@/views/stockmanage/homecarepage/techreceiveproduct/index'),
            meta: {
              title: '技师领料',
              icon: 'system'
            }
          },
          {
            path: 'techreturnproduct',
            name: 'techreturnproduct', // component name
            component: () => import('@/views/stockmanage/homecarepage/techreturnproduct/index'),
            meta: {
              title: '技师退料',
              icon: 'system'
            }
          }
        ]
      }


      // {
      //   path: 'stockpage',
      //   name: 'stockpage', // component name
      //   component: () => import('@/views/stockmanage/stockpage/stockindex'),
      //   meta: { title: '库存查看', icon: 'system' },
      //   children: [
      //     {
      //       path: 'stockindex',
      //       name: 'stockindex', // component name
      //       component: () => import('@/views/stockmanage/stockpage/stockindex'),
      //       meta: { title: '当前库存', icon: 'system' }
      //     },
      //     {
      //       path: 'stockinoutindex',
      //       name: 'stockinoutindex', // component name
      //       component: () => import('@/views/stockmanage/stockpage/stockinoutindex'),
      //       meta: { title: '出入库记录', icon: 'tousu' }
      //     }
      //   ]
      // }
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
    path: '/supplierstock',
    component: Layout,
    alwaysShow: true,
    redirect: '/stockmanage/stockpage/storageindex',
    name: 'supplierstock', // component name
    meta: {
      title: '供应商库存',
      icon: 'system'
    },
    children: [

      {
        path: 'supplierproductstock',
        name: 'supplierproductstock', // component name
        component: () => import('@/views/stockmanage/supplierproductstock'),
        meta: {
          title: '供应商库存列表',
          icon: 'system'
        }
      }
    ]
  },

  {
    path: '/shoppurchase',
    component: Layout,
    alwaysShow: true,
    redirect: '/shoppurchase/purchasesearch',
    name: 'shoppurchase',
    meta: {
      title: '采销管理',
      icon: 'purchase'
    },
    children: [{
        path: 'purchasesearch',
        name: 'purchasesearch', // component name
        component: () => import('@/views/shoppurchase/purchase/purchasesearch'),
        meta: {
          title: '采销单',
          icon: 'system'
        },
      },
      {
        path: 'purchasemonthpay',
        name: 'purchasemonthpay', // component name
        component: () => import('@/views/shoppurchase/purchase/purchasemonthpay'),
        meta: {
          title: '采购月结付款列表',
          icon: 'system'
        },
      },
      
      // {
      //   path: 'purchaseinfoindex',
      //   name: 'purchaseinfoindex', // component name
      //   component: () => import('@/views/purchase/purchaseinfo/purchaseinfoindex'),
      //   meta: { title: '采购信息配置', icon: 'purchaseorder' },
      //   hidden: true
      // },
      {
        path: 'vender',
        name: 'vender', // component name
        component: () => import('@/views/purchase/vender'),
        meta: {
          title: '供应商/客户',
          icon: 'vendor'
        }
      },
      {
        path: 'shopproduct',
        name: 'shopproduct', // component name
        component: () => import('@/views/purchase/shopproduct'),
        meta: {
          title: '外采产品',
          icon: 'system'
        }
        
      }
    ]
  },

  // {
  //   path: '/fms',
  //   component: Layout,
  //   redirect: '/fms/accountcheck',
  //   name: 'purchase', // component name
  //   meta: { title: '门店对账', icon: 'fms' },
  //   children: [
  //     {
  //       path: 'index',
  //       name: 'index', // component name
  //       component: () => import('@/views/fms/accountcheck/index'),
  //       meta: { title: '门店对账汇总列表', icon: 'purchase1' }
  //     },
  //     {
  //       path: 'accountcheckindex',
  //       name: 'accountcheckindex', // component name
  //       component: () => import('@/views/fms/accountcheck/accountcheckindex'),
  //       meta: { title: '门店对账汇总详情', icon: 'purchase1' },
  //       hidden: true
  //     },
  //     {
  //       path: 'exceptionmonitorindex',
  //       name: 'exceptionmonitorindex', // component name
  //       component: () => import('@/views/fms/accountcheck/exceptionmonitorindex'),
  //       meta: { title: '对账异常监控列表', icon: 'accountcheckindex(1)' }
  //     },
  //     {
  //       path: 'settlement',
  //       name: 'settlement', // component name
  //       component: () => import('@/views/fms/settlement/index'),
  //       meta: { title: '结算管理', icon: 'settlement' }
  //     },
  //     {
  //       path: 'detail',
  //       name: 'detail', // component name
  //       component: () => import('@/views/fms/settlement/detail'),
  //       meta: { title: '结算详情', icon: 'settlement' },
  //       hidden: true
  //     }
  //   ]
  // },
  /*
  {
    path: '/wms',
    component: Layout,
    redirect: '/wms/asn',
    name: 'wms', // component name
    meta: { title: '仓储管理', icon: 'wms' },
    children: [
      {
        path: 'asn',
        name: 'asn', // component name
        component: () => import('@/views/wms/asn/asnlist'),
        meta: { title: '入库管理', icon: 'asnlist' }
      },
      {
        path: 'warehouseindex',
        name: 'warehouseindex', // component name
        component: () => import('@/views/wms/warehouse/warehouseindex'),
        meta: { title: '仓库管理', icon: 'asnlist' }
      },
      {
        path: 'replenishtask',
        name: 'replenishtask', // component name
        component: () => import('@/views/wms/replenishtask/replenishtasklist'),
        meta: { title: '出库申请管理', icon: 'replenishtasklist' }
      },
      {
        path: 'warehousetransfer',
        name: 'warehousetransfer', // component name
        component: () => import('@/views/wms/warehousetransfer/warehousetransfer'),
        meta: { title: '出库管理', icon: 'warehousetransfer' }
      },
      {
        path: 'stocktrackindex',
        name: 'stocktrackindex', // component name
        component: () => import('@/views/stock/stocktrackindex'),
        meta: { title: '库存链路查询', icon: 'system' }
      },
      {
        path: 'stocktracklist',
        name: 'stocktracklist',
        component: () => import('@/views/stock/stocktracklist'),
        meta: { title: '仓库库存列表', icon: 'tree' },
        hidden: true
      },
      {
        path: 'stocktrackdetail',
        name: 'stocktrackdetail',
        component: () => import('@/views/stock/stocktrackdetail'),
        meta: { title: '库存链路明细列表', icon: 'tree' },
        hidden: true
      }
    ]
  },
  */
  // {
  //   path: '/aftersale',
  //   component: Layout,
  //   alwaysShow: true,
  //   redirect: '/aftersale/tousu',
  //   name: 'aftersale', // component name
  //   meta: { title: '售后管理', icon: 'aftersale' },
  //   children: [
  //     {
  //       path: 'ordercommentindex',
  //       name: 'ordercommentindex', // component name
  //       component: () => import('@/views/ordercomment/ordercommentindex'),
  //       meta: { title: '评论管理', icon: 'system' }
  //     },
  //     {
  //       path: 'index',
  //       name: 'index', // component name
  //       component: () => import('@/views/aftersale/tousu/index'),
  //       meta: { title: '投诉管理', icon: 'tousu' }
  //     },

  //     {
  //       path: 'tousudetail',
  //       name: 'tousudetail',
  //       component: () => import('@/views/aftersale/tousu/tousudetail'),
  //       meta: { title: '投诉详情', icon: 'tree' },
  //       hidden: true
  //     },
  //     {
  //       path: 'createtousu',
  //       name: 'createtousu',
  //       component: () => import('@/views/aftersale/tousu/createtousu'),
  //       meta: { title: '创建投诉', icon: 'tree' },
  //       hidden: true
  //     }
  //   ]
  // },

  {
    path: '/company',
    component: Layout,
    redirect: '/company/companylist',
    name: 'company', // component name
    alwaysShow: true,
    meta: {
      title: '子公司管理',
      icon: 'system'
    },
    children: [{
      path: 'companylist',
      name: 'companylist', // component name
      component: () => import('@/views/company/companylist'),
      meta: {
        title: '子公司列表',
        icon: 'system'
      }
    }]
  },


  {
    path: '/customermanage',
    component: Layout,
    redirect: '/customermanage/userlist',
    name: 'customermanage', // component name
    alwaysShow: true,
    meta: {
      title: '客户管理',
      icon: 'user1'
    },
    children: [{
        path: 'arrivalList',
        name: 'arrivalList', // component name
        component: () => import('@/views/customermanage/arrival/index'),
        meta: {
          title: '到店记录',
          icon: 'userlist'
        }
      },
      {
        path: 'userlist',
        name: 'userlist', // component name
        component: () => import('@/views/customermanage/user/index'),
        meta: {
          title: '客户列表',
          icon: 'userlist'
        }
      },
      {
        path: 'reservelist',
        name: 'reservelist', // component name
        component: () => import('@/views/customermanage/reserve/index'),
        meta: {
          title: '预约记录',
          icon: 'userlist'
        }
      },
      {
        path: 'checkReport',
        name: 'checkReport',
        component: () => import('@/views/customermanage/arrival/checkReport'),
        meta: {
          title: '车况检查报告',
          icon: 'tree'
        },
        hidden: true
      },
      {
        path: 'carRecord',
        name: 'carRecord',
        component: () => import('@/views/customermanage/user/carRecord'),
        meta: {
          title: '车辆档案',
          icon: 'tree'
        },
        hidden: true
      },
      {
        path: 'abnormal',
        name: 'abnormal',
        component: () => import('@/views/customermanage/user/abnormal'),
        meta: {
          title: '异常详情',
          icon: 'tree'
        },
        hidden: true
      },
    ]
  },
  {
    path: '/shopmanage',
    component: Layout,
    redirect: '/shopmanage/shoplist',
    alwaysShow: true,
    name: 'shopmanage', // component name
    meta: {
      title: '门店管理',
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

      // {
      //   path: 'addbaseservice',
      //   name: 'addbaseservice', // component name
      //   hidden: true,
      //   component: () => import('@/views/shopmanage/addbaseservice'),
      //   meta: {
      //     title: '基础服务配置',
      //     icon: 'addbaseservice'
      //   }
      // },
      // {

      //   path: 'shopservicelist',
      //   hidden: true,
      //   name: 'shopservicelist', // component name
      //   component: () => import('@/views/shopmanage/shopservicelist'),
      //   meta: { title: '门店服务配置', icon: 'shopservicelist' }
      // },
      // {
      //   path: 'shopservice',
      //   name: 'shopservice', // component name
      //   hidden: true,
      //   component: () => import('@/views/shopmanage/shopservice'),
      //   meta: {
      //     title: '门店服务详情',
      //     icon: 'system'
      //   }
      // },
      //   {
      //     path: 'shopservicetypeindex',
      //     name: 'shopservicetypeindex', // component name
      //     component: () => import('@/views/shopmanage/shopservicetypeindex'),
      //     meta: {
      //       title: '门店服务类型管理',
      //       icon: 'system'
      //     }
      //   },

      //   {
      //     path: "article",
      //     name: "article", // component name
      //     component: () => import("@/views/activity/promote/article"),
      //     meta: { title: "文章转载管理", icon: "faqlist" }
      //   },
      //   {
      //     path: "poster",
      //     name: "poster", // component name
      //     component: () => import("@/views/activity/promote/poster"),
      //     meta: { title: "营销海报管理", icon: "faqlist" }
      //   },
      //   {
      //     path: "friendjokes",
      //     name: "friendjokes", // component name
      //     component: () => import("@/views/activity/promote/friendjokes"),
      //     meta: { title: "朋友圈段子管理", icon: "faqlist" }
      //   },
      //   {
      //     path: "createfriendjokes",
      //     name: "createfriendjokes", // component name
      //     component: () => import("@/views/activity/promote/createfriendjokes"),
      //     meta: { title: "新建朋友圈段子", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "editfriendjokes",
      //     name: "editfriendjokes", // component name
      //     component: () => import("@/views/activity/promote/editfriendjokes"),
      //     meta: { title: "编辑朋友圈段子", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "editarticle",
      //     name: "editarticle", // component name
      //     component: () => import("@/views/activity/promote/editarticle"),
      //     meta: { title: "文章转载详情", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "createarticle",
      //     name: "createarticle", // component name
      //     component: () => import("@/views/activity/promote/createarticle"),
      //     meta: { title: "创建文章转载", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "reviewarticle",
      //     name: "reviewarticle", // component name
      //     component: () => import("@/views/activity/promote/reviewarticle"),
      //     meta: { title: "预览文章", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "installguideindex",
      //     name: "installguideindex", // component name
      //     component: () => import("@/views/activity/installguide/installguideindex"),
      //     meta: { title: "安装指导配置", icon: "faqlist" }
      //   },
      //   {
      //     path: "addinstallguide",
      //     name: "addinstallguide", // component name
      //     component: () => import("@/views/activity/installguide/addinstallguide"),
      //     meta: { title: "新增安装指导", icon: "faqlist" },
      //     hidden: true
      //   },
      //   {
      //     path: "updateinstallguide",
      //     name: "updateinstallguide", // component name
      //     component: () => import("@/views/activity/installguide/updateinstallguide"),
      //     meta: { title: "更改安装指导", icon: "faqlist" },
      //     hidden: true
      //   }
    ]
  },
  {
    path: '/employeemanage',
    component: Layout,
    redirect: '/accountauthority/employee/index',
    name: 'employeemanage',
    alwaysShow: true,
    meta: {
      title: '员工管理',
      icon: 'component'
    },
    children: [{
      path: 'employee',
      name: 'employee',
      component: () => import('@/views/accountauthority/employee/index'),
      meta: {
        title: '员工列表',
        icon: 'employeerole'
      }
    },
    {
      path: 'role',
      name: 'role', // component name
      component: () => import('@/views/accountauthority/role/index'),
      meta: {
        title: '角色管理',
        icon: 'role'
      }
    },]
  },
  {
    path: '/shopsetting',
    component: Layout,
    redirect: '/shopsetting/shopasset',
    name: 'shopsetting', // component name
    alwaysShow: true,
    meta: {
      title: '门店设置',
      icon: 'shop-setting'
    },
    children: [
      {
      path: 'shopasset',
      name: 'shopasset', // component name
      component: () => import('@/views/shopsetting/shopasset'),
      meta: {
        title: '门店资产管理',
        icon: 'asset'
      }
    },
    {
      path: 'shopcar',
      name: 'shopcar',
      component: () => import('@/views/shopmanage/shopcar'),
      meta: {
        title: '车辆管理',
        icon: 'shopcar'
      }
    }
  ]
  },
  {
    path: '/shopoperation',
    component: Layout,
    redirect: '/shopoperation/customercomment',
    name: 'shopoperation', // component name
    alwaysShow: true,
    meta: {
      title: '门店运营',
      icon: 'operation'
    },
    children: [
      {
      path: 'customercomment',
      name: 'customercomment', // component name
      component: () => import('@/views/shopoperation/customercomment'),
      meta: {
        title: '客户评价',
        icon: 'comment'
      }
    },
    {
      path: 'shopcarrecord',
      name: 'shopcarrecord', // component name
      component: () => import('@/views/shopmanage/shopcarrecord'),
      meta: {
        title: '用车管理',
        icon: 'comment'
      }
    },
    {
      path: 'techtriprecord',
      name: 'techtriprecord', // component name
      component: () => import('@/views/shopmanage/techtriprecord'),
      meta: {
        title: '技师出行记录',
        icon: 'comment'
      }
    }
  ]
  },
  {
    path: '/marketingmanage',
    component: Layout,
    redirect: '/marketingmanage/promotion',
    name: 'marketingmanage', // component name
    alwaysShow: true,
    meta: { title: '营销管理', icon: 'operation' },
    children: [
      {
        path: 'promotion',
        name: 'promotion', // component name
        component: () => import('@/views/marketingmanage/promotion/index'),
        meta: { title: '促销活动', icon: 'comment' }
      },
      {
        path:'grouponproduct',
        name:'grouponproduct',
        component: () => import('@/views/marketingmanage/grouponproduct/index'),
        meta: { title: '美容团购', icon: 'comment' }
      }
    ]
  },
  {
    path: '/reportform',
    component: Layout,
    redirect: '/reportform/MonthlyReport',
    name: 'reportform', // component name
    alwaysShow: true,
    meta: { title: '门店报表', icon: 'component' },
    children: [
      {
        path: 'monthlyReport',
        name: 'monthlyReport', // component name
        component: () => import('@/views/reportform/monthlyReport/index'),
        meta: { title: '经营月报', icon: 'system' }
      },
      {
        path: 'enteringTrend',
        name: 'enteringTrend', // component name
        component: () => import('@/views/reportform/enteringTrend/index'),
        meta: { title: '进店趋势', icon: 'employeerole' }
      },
      {
        path: 'jixiao',
        name: 'jixiao', // component name
        component: () => import('@/views/reportform/jixiao/index'),
        meta: { title: '员工绩效', icon: 'jixiao' }
      }
    ]
  },
  {
    path: '/reportcenter',
    component: Layout,
    alwaysShow: true,
    redirect: '/reportcenter/orderreport',
    name: 'reportcenter',
    meta: {
      title: '订单报表',
      icon: 'purchase'
    },
    children: [{
        path: 'orderreport',
        name: 'orderreport', // component name
        component: () => import('@/views/reportcenter/orderreport'),
        meta: {
          title: '订单统计',
          icon: 'system'
        },
      },
      {
        path: 'orderoutpurchasereport',
        name: 'orderoutpurchasereport', // component name
        component: () => import('@/views/reportcenter/orderoutpurchasereport'),
        meta: {
          title: '订单外采统计',
          icon: 'system'
        },
      },
      {
        path: 'orderdetailreport',
        name: 'orderdetailreport', // component name
        component: () => import('@/views/reportcenter/orderdetailreport'),
        meta: {
          title: '订单统计明细',
          icon: 'system'
        },
        hidden:true
      },
      
      {
        path: 'orderproductreport',
        name: 'orderproductreport', // component name
        component: () => import('@/views/reportcenter/orderproductreport'),
        meta: {
          title: '订单产品统计',
          icon: 'vendor'
        }
      }
    ]
  },

  //BEGIN accountauthority
  {
    path: '/systemconfig',
    component: Layout,
    redirect: '/systemconfig/companyinfo',
    name: 'systemconfig',
    alwaysShow: true,
    meta: {
      title: '系统设置',
      icon: 'component'
    },
    children: [{
        path: 'companyinfo',
        name: 'companyinfo',
        component: () => import('@/views/systemconfig/companyinfo'),
        meta: { title: '公司基本信息维护', icon: 'system' }
      },
      {
        path: 'shopinfo',
        name: 'shopinfo',
        component: () => import('@/views/systemconfig/shopinfo'),
        meta: { title: '门店基本信息设置', icon: 'system' }
      }
    ]
  },


  //BEGIN personalcenter
  // {
  //   path: '/personalcenter',
  //   component: Layout,
  //   redirect: '/personalcenter/chanagepassword',
  //   name: 'personalcenter', // component name
  //   meta: { title: '个人中心', icon: 'accountauthority1' },
  //   children: [
  //     {
  //       path: 'basicinfo',
  //       name: 'basicinfo', // component name
  //       component: () => import('@/views/personalcenter/basicinfo'),
  //       meta: { title: '基本信息', icon: 'application' }
  //     },
  //     {
  //       path: 'chanagepassword',
  //       name: 'chanagepassword', // component name
  //       component: () => import('@/views/personalcenter/chanagepassword'),
  //       meta: { title: '修改密码', icon: 'shopmanage' }
  //     }
  //   ]
  // },
  {
    path: '/Demo',
    component: Layout,
    redirect: '/demo/',
    name: 'Demo',
    meta: {
      title: 'Demo',
      icon: 'accountauthority1'
    },
    children: [{
      path: 'demo',
      name: 'demo', // component name
      component: () => import('@/views/demo'),
      meta: {
        title: 'Demo',
        icon: 'application'
      }
    }]
  },
  //END personalcenter
  // 404 page must be placed at the end !!!
  {
    path: '*',
    redirect: '/404',
    hidden: true
  }
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
