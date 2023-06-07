using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using eRSN_New.Filter;
using eRSN_New.Models;
using Newtonsoft.Json;
using System.Data;
using eRSN_New.Services;
using OfficeOpenXml;

namespace eRSN_New.Controllers
{
    [Auth]
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("DellCommVMIReport");
        } // end ActionResult: Index

        public ActionResult DellCommVMIReport()
        {
            return View();
        } // end ActionResult: DellCommVMIReport

        public JsonResult GetCustCode()
        {
            try
            {
                eRSNModels_Repository repo = new eRSNModels_Repository();
                List<TB_SELECT> list = repo.GetCustCode();

                return Json(new { Result = true, Message = "Success", List = list });
            } // end try:

            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            } // end catch: Exception ex
        } // end JsonResult: GetCustCode

        public JsonResult GetDellCommVMITable(DellcommVMIparam param)
        {
            try
            {
                eRSNModels_Repository repo = new eRSNModels_Repository();
                List<TB_DELLCOMM_VMI_REPORT> list = repo.GetDellCommVMITable(param);

                return Json(new { Result = true, Message = "Success", List = JsonConvert.SerializeObject(list) });
            } // end try:

            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            } // end catch: Exception ex
        } // end JsonResult: GetDellCommVMITable

        public JsonResult ExportDellCommVMITable(DellcommVMIparam param, List<string> col)
        {
            try
            {
                bool result = false;
                string msg = string.Empty;

                eRSNModels_Repository repo = new eRSNModels_Repository();
                List<TB_DELLCOMM_VMI_REPORT> list = repo.GetDellCommVMITable(param);

                ListtoDataTableConverter convert = new ListtoDataTableConverter();
                DataTable dt = convert.ToDataTable(list);
                string filename = "/Files/" + Session["USER_NAME"].ToString() + ".xlsx";
                string path = Server.MapPath("~" + filename);

                ExportExcelService xlsx = new ExportExcelService();
                if (xlsx.ExportXLSX(dt, col, path))
                {
                    result = true;
                    msg = "Success";
                }
                else
                {
                    result = true;
                    msg = "Cannot export excel!";
                } // end if: export excel success

                return Json(new { Result = result, Message = msg, File = filename });
            } // end try:

            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            } // end catch: Exception ex
        } // end JsonResult: ExportDellCommVMITable
    } // end class: ReportController : Controller
} // end namespace: eRSN_New.Controllers