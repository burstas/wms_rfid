﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using THOK.Wms.Bll.Interfaces;
using THOK.Wms.DbModel;
using THOK.WebUtil;

namespace Authority.Controllers.Wms.StockIn
{
    public class StockInBillController : Controller
    {
        [Dependency]
        public IInBillMasterService InBillMasterService { get; set; }
        [Dependency]
        public IInBillDetailService InBillDetailService { get; set; }
        //
        // GET: /StockInBill/

        public ActionResult Index()
        {
            ViewBag.hasSearch = true;
            ViewBag.hasAdd = true;
            ViewBag.hasEdit = true;
            ViewBag.hasDelete = true;
            return View();
        }

        //
        // GET: /InBillMaster/Details/

        public ActionResult Details(int page, int rows, FormCollection collection)
        {
            string BillNo = collection["BillNo"] ?? "";
            string BillDate = collection["BillDate"] ?? "";
            string OperatePersonCode = collection["OperatePersonCode"] ?? "";
            string Status = collection["Status"] ?? "";
            string IsActive = collection["IsActive"] ?? "";
            var inBillMaster = InBillMasterService.GetDetails(page,rows,BillNo,BillDate,OperatePersonCode,Status,IsActive);
            return Json(inBillMaster, "text", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /InBillDetail/InBillDetails/

        public ActionResult InBillDetails(int page, int rows, string BillNo)
        {
            var inBillDetail = InBillDetailService.GetDetails(page,rows,BillNo);
            return Json(inBillDetail, "text", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /InBillMaster/GenInBillNo/

        public ActionResult GenInBillNo()
        {
            var inBillNo = InBillMasterService.GenInBillNo();
            return Json(inBillNo, "text", JsonRequestBehavior.AllowGet);
        }

    }
}