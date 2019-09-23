﻿using System.Collections.Generic;

namespace FiiiPay.DTO.Merchant
{
    public class GetListByCodeListOM
    {
        public List<GetListByCodeListOMItem> List { get; set; }
    }

    public class GetListByCodeListOMItem
    {
        /// <summary>
        /// 商家Id，可以用于后面的支付
        /// </summary>
        public string Id { get; set; }
        public string IconUrl { get; set; }
        public string MerchantName { get; set; }

        /// <summary>
        /// 已经加上***，客户端直接显示
        /// </summary>
        public string MerchantAccount { get; set; }

        /// <summary>
        /// 是否已经认证
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// 随机码
        /// </summary>
        public string RandomCode { get; set; }

        /// <summary>
        /// 是否允许收款
        /// </summary>
        public bool IsAllowAcceptPayment { get; set; }
    }
}
