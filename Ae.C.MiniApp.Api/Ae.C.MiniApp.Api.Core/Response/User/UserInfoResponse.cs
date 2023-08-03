using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.User
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 用户手机号[脱敏]
        /// </summary>
        public string UserTelDes { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 会员等级图片
        /// </summary>
        public string MemberUrl { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int MemberGrade { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 默认收货地址
        /// </summary>
        public UserAddressVO DefaultAddress { get; set; }

        /// <summary>
        /// 默认车型
        /// </summary>
        public UserVehicleVO DefaultVehicle { get; set; }

        /// <summary>
        /// 可用优惠券数量
        /// </summary>
        public int AvailCouponCount { get; set; }

        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        public string DriverLicenseExpiry { get; set; }

        /// <summary>
        /// 套餐卡数量
        /// </summary>
        public int PackageCardNum { get; set; }
    }
}
