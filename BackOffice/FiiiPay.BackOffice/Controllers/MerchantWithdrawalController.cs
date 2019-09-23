﻿using FiiiPay.BackOffice.BLL;
using FiiiPay.BackOffice.Common;
using FiiiPay.BackOffice.Models;
using FiiiPay.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.BackOffice.Controllers
{
    [BOAccess("MerchantWithdrawalMenu")]
    public class MerchantWithdrawalController : BaseController
    {
        public ActionResult Index(int id)
        {
            var list = FoundationDB.CryptocurrencyDb.GetList();
            List<SelectListItem> oList = new List<SelectListItem>();
            oList.Add(new SelectListItem() { Text = "ALL", Value = "" });
            foreach (var item in list)
            {
                oList.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.CURList = oList;
            return View();
        }

        public ActionResult LoadData(GridPager pager, string orderNo, string username, string address, string txid, string status, string cryptoId)
        {
            WithdrawalsBLL fb = new WithdrawalsBLL();
            var data = fb.GetMerchantWithdrawalViewList(orderNo, username, address, txid, status, cryptoId, ref pager);
            var coinList = FoundationDB.CryptocurrencyDb.GetList();
            var obj = data.ToGridJson(pager,
                r => new
                {
                    id = r.Id,
                    cell = new
                    {
                        Id = r.Id,
                        OrderNo = r.OrderNo,
                        Username = r.Username,
                        SelfPlatform = r.SelfPlatform ? "In platform" : "Outside platform",
                        CryptoName = coinList.Where(t => t.Id == r.CryptoId).Select(t => t.Name).FirstOrDefault(),
                        Amount = r.Amount,
                        WithdrawalFee = r.WithdrawalFee,
                        Address = r.Address,
                        CountryId = r.CountryId,
                        RequestId = r.RequestId,
                        Tag = r.Tag,
                        TXID = r.TXID,
                        Status = r.Status.ToString(),
                        Timestamp = r.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                    }
                });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AjaxAntiForgeryToken]
        public ActionResult GetConfirmTimes(int countryId, long requestId)
        {
            FiiiFinanceBLL fb = new FiiiFinanceBLL();
            return Json(fb.GetStatus(countryId, requestId));
        }
    }
}