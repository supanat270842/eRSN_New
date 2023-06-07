using Dapper;
using db = eRSN_New.Services.DatabaseConnectionService;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System;
using System.Globalization;
using System.Xml.Linq;

namespace eRSN_New.Models
{
    public class eRSNModels
    {
    } // end class: eRSNModels

    public class TB_SELECT
    {
        public string id { get; set; }
        public string name { get; set; }
    } // end class: TB_SELECT

    public class DellcommVMIparam
    {
        public string txtRSN { get; set; }
        public string txtInvoice { get; set; }
        public string selCustCode { get; set; }
        public string selRsnType { get; set; }
        public string txtCreateDtSt { get; set; }
        public string txtCreateDtEd { get; set; }
        public string txtShipmentDtSt { get; set; }
        public string txtShipmentDtEd { get; set; }
    } // end class: DellcommVMIparam

    public class TB_DELLCOMM_VMI
    {

        public string rdt_cuspn { get; set; }
        public string rdt_shipqty { get; set; }
        public string rdt_billup { get; set; }
        public string rsn_no { get; set; }
        public string inv_invoice { get; set; }
        public string ship_date { get; set; }
        public string ship_time { get; set; }
        public string rsn_shipmode { get; set; }
        public string asi_mawb { get; set; }
        public string asi_hawb { get; set; }
        public int rsn_totpack { get; set; }
        public decimal amt { get; set; }
    } // end class: TB_DELLCOMM_VMI_REPORT

    public class TB_DELLCOMM_VMI_REPORT
    {
        public string sales_document { get; set; }
        public string sales_document_item { get; set; }
        public string net_weight { get; set; }
        public string gross_weight { get; set; }
        public string weight { get; set; }
        public string number_of_packages { get; set; }
        public string po { get; set; }
        public string delivered { get; set; }
        public string serial_number { get; set; }
        public string hawb { get; set; }
        public string carton_info { get; set; }
        public string dates_actual_and_arrival { get; set; }
        public string box_id { get; set; }
        public string aus_net_ser { get; set; }
        public string pallet_id { get; set; }
        public string ship_to_code { get; set; }
        public string requestor_code { get; set; }
        public string otm_po_number { get; set; }
        public string po_line_number { get; set; }
        public string ship_date_yyyy_mm_dd { get; set; }
        public string ship_time_hh_mm { get; set; }
        public string promise_date_yyyy_mm_dd { get; set; }
        public string time_hh_mm { get; set; }
        public string handling_unit { get; set; }
        public string otm_weight { get; set; }
        public string shipped_lot_quantity { get; set; }
        public string volume { get; set; }
        public string expedite_flag { get; set; }
        public string approval_id { get; set; }
        public string bu { get; set; }
        public string pf { get; set; }
        public string elm_po_number { get; set; }
        public string elm_po_line_number { get; set; }
        public string country_key { get; set; }
        public string air_way_bill { get; set; }
        public string batch { get; set; }
        public string rsn { get; set; }
        public string sch_line_num { get; set; }
        public string est_arr_date { get; set; }
        public string packing_instruction { get; set; }
        public string lot_serial_nr { get; set; }
        public string lot_serial_qty { get; set; }
        public string msd_kit { get; set; }
        public string crp_po_nr { get; set; }
        public string manufacturing_date { get; set; }
        public string mode_of_transport { get; set; }
        public string delivery_order { get; set; }
        public string rdt_cuspn { get; set; }
        public string rdt_shipqty { get; set; }
        public string rdt_billup { get; set; }
        public string rsn_no { get; set; }
        public string inv_invoice { get; set; }
        public string ship_date { get; set; }
        public string delivery_date { get; set; }
        public string waybill { get; set; }
        public string bolnum { get; set; }
        public int rsn_totpack { get; set; }
        public string h { get; set; }
        public string l { get; set; }
        public string w { get; set; }
        public string gw { get; set; }
        public string vw { get; set; }
        public string container_id { get; set; }
        public decimal amt { get; set; }
    } // end class: TB_DELLCOMM_VMI_REPORT

    public class eRSNModels_Repository
    {
        public List<TB_SELECT> GetCustCode()
        {
            using (OracleConnection conn = new OracleConnection(db.eRSNConnection))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT cus_code id, cus_name name");
                sb.AppendLine("FROM rsn_cuscode");
                sb.AppendLine("ORDER BY cus_code");

                string sql = sb.ToString();
                return conn.Query<TB_SELECT>(sql).ToList();
            } // end using: conn
        } // end List<RSN_CUS_CODE>: GetCustCode

        public List<TB_DELLCOMM_VMI_REPORT> GetDellCommVMITable(DellcommVMIparam param)
        {
            string rsnno = param.txtRSN;
            string invno = param.txtInvoice;
            string cuscode = param.selCustCode;
            string rsntype = param.selRsnType;
            string cdst = param.txtCreateDtSt;
            string cded = param.txtCreateDtEd;
            string sdst = param.txtShipmentDtSt;
            string sded = param.txtShipmentDtEd;
            double a_date = double.Parse(ConfigurationManager.AppSettings["RSN_A_DATE"].ToString());
            double l_date = double.Parse(ConfigurationManager.AppSettings["RSN_L_DATE"].ToString());

            List<TB_DELLCOMM_VMI_REPORT> result = new List<TB_DELLCOMM_VMI_REPORT>();

            using (OracleConnection conn = new OracleConnection(db.eRSNConnection))
            {
                string sql = @"
                    SELECT 
                        detail.rdt_cuspn , detail.rdt_shipqty,detail.rdt_billup,
                        info.rsn_no, invoice.inv_invoice, 
                        nvl(to_char(invoice.inv_pickdate,'MM-DD-YYYY HH24:MI:SS'),'') ship_date,
                        nvl(to_char(invoice.inv_picktime,'MM-DD-YYYY HH24:MI:SS'),'') ship_time,
                        info.rsn_shipmode, ship.asi_mawb, ship.asi_hawb, info.rsn_totpack, NVL(detail.rdt_billtot, 0) amt
                    FROM rsn_info info
                        LEFT JOIN rsn_invoice invoice ON info.rsn_no = invoice.inv_rsn
                        LEFT JOIN rsn_addshipinfo ship ON invoice.inv_invoice = ship.asi_invno
                        LEFT JOIN rsn_detail detail ON info.rsn_no = detail.rdt_rsnno
                    WHERE info.rsn_shipmode IN ('A','L') AND REGEXP_LIKE(info.rsn_totpack, '^[[:digit:]]+$') 
                ";

                StringBuilder sb = new StringBuilder();

                // ######################## CONDITION SEARCH ######################## //

                // RSN NUMBER
                if (!string.IsNullOrEmpty(rsnno))
                {
                    //sb.AppendLine("AND info.rsn_no LIKE '%" + rsnno + "%'");
                    sb.AppendLine("AND info.rsn_no = '" + rsnno + "'");
                } // end if: RSN NUMBER

                // INVOICE NUMBER
                if (!string.IsNullOrEmpty(invno))
                {
                    //sb.AppendLine("AND invoice.inv_invoice LIKE '%" + invno + "%'");
                    sb.AppendLine("AND invoice.inv_invoice = '" + invno + "'");
                } // end if: INVOICE NUMBER

                // CUSTOEMR CODE
                if (!string.IsNullOrEmpty(cuscode))
                {
                    sb.AppendLine("AND info.rsn_cuscode = '" + cuscode + "'");
                } // end if: CUSTOEMR CODE

                // RSN TYPE
                if (!string.IsNullOrEmpty(rsntype))
                {
                    sb.AppendLine("AND info.rsn_type = '" + rsntype + "'");
                } // end if: RSN TYPE

                // CREATE DATE
                if (!string.IsNullOrEmpty(cdst) && !string.IsNullOrEmpty(cded))
                {
                    sb.AppendLine("AND info.rsn_reqdate BETWEEN TO_DATE('" + cdst + "', 'YYYY-MM-DD HH24:MI:SS') AND TO_DATE('" + cded + "', 'YYYY-MM-DD HH24:MI:SS')");
                }
                else if (!string.IsNullOrEmpty(cdst) && string.IsNullOrEmpty(cded))
                {
                    sb.AppendLine("AND info.rsn_reqdate = TO_DATE('" + cdst + "', 'YYYY-MM-DD HH24:MI:SS')");
                }
                else if (string.IsNullOrEmpty(cdst) && !string.IsNullOrEmpty(cded))
                {
                    sb.AppendLine("AND info.rsn_reqdate = TO_DATE('" + cded + "', 'YYYY-MM-DD HH24:MI:SS')");
                }

                // SHIPMENT DATE
                if (!string.IsNullOrEmpty(sdst) && !string.IsNullOrEmpty(sded))
                {
                    sb.AppendLine("AND invoice.inv_pickdate BETWEEN TO_DATE('" + sdst + "', 'YYYY-MM-DD HH24:MI:SS') AND TO_DATE('" + sded + "', 'YYYY-MM-DD HH24:MI:SS')");
                }
                else if (!string.IsNullOrEmpty(sdst) && string.IsNullOrEmpty(sded))
                {
                    sb.AppendLine("AND invoice.inv_pickdate = TO_DATE('" + sdst + "', 'YYYY-MM-DD HH24:MI:SS')");
                }
                else if (string.IsNullOrEmpty(sdst) && !string.IsNullOrEmpty(sded))
                {
                    sb.AppendLine("AND invoice.inv_pickdate = TO_DATE('" + sded + "', 'YYYY-MM-DD HH24:MI:SS')");
                }

                string merge_sql = sql + sb.ToString();

                List<TB_DELLCOMM_VMI> dt = conn.Query<TB_DELLCOMM_VMI>(merge_sql).ToList();

                foreach (TB_DELLCOMM_VMI item in dt)
                {
                    int totpack = item.rsn_totpack;
                    string inv = item.inv_invoice;
                    string shipmode = item.rsn_shipmode;
                    string mawb = item.asi_mawb;
                    string hawb = item.asi_hawb;
                    string ship_date = item.ship_date;
                    string ship_time = item.ship_time;
                    DateTime out_date;
                    DateTime ship_dt;
                    string delivery_date = "";
                    string waybill = shipmode == "A" ? mawb : hawb;
                    string bolnum = shipmode == "A" ? hawb : inv;

                    if (DateTime.TryParseExact(ship_date, "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out out_date))
                    {
                        ship_dt = DateTime.Parse(ship_date);
                        delivery_date = ship_date == "A" ? ship_dt.AddDays(a_date).ToString("MM-dd-yyyy HH:mm:ss") : ship_dt.AddDays(l_date).ToString("MM-dd-yyyy HH:mm:ss");

                    }

                    string sub_shipDate = ship_date.Substring(0, item.ship_date.Length - 8);
                    string sub_shipTime = ship_time.Substring(item.ship_time.Length - 8);
                    string merge_ship = sub_shipDate + sub_shipTime;

                    string sub_delivery = delivery_date.Substring(0, delivery_date.Length - 8);
                    string merge_delivery = sub_delivery + sub_shipTime;

                    for (int iRow = 1; iRow <= totpack; iRow++)
                    {
                        TB_DELLCOMM_VMI_REPORT md = new TB_DELLCOMM_VMI_REPORT();

                        md.sales_document = "";
                        md.sales_document_item = "";
                        md.net_weight = "";
                        md.gross_weight = "";
                        md.weight = "";
                        md.number_of_packages = "";
                        md.po = "";
                        md.delivered = "";
                        md.serial_number = "";
                        md.hawb = "";
                        md.carton_info = "";
                        md.dates_actual_and_arrival = "";
                        md.box_id = "";
                        md.aus_net_ser = "";
                        md.pallet_id = "";
                        md.ship_to_code = "";
                        md.requestor_code = "";
                        md.otm_po_number = "";
                        md.po_line_number = "";
                        md.ship_date_yyyy_mm_dd = "";
                        md.ship_time_hh_mm = "";
                        md.promise_date_yyyy_mm_dd = "";
                        md.time_hh_mm = "";
                        md.handling_unit = "";
                        md.otm_weight = "";
                        md.shipped_lot_quantity = "";
                        md.volume = "";
                        md.expedite_flag = "";
                        md.approval_id = "";
                        md.bu = "";
                        md.pf = "";
                        md.elm_po_number = "";
                        md.elm_po_line_number = "";
                        md.country_key = "";
                        md.air_way_bill = "";
                        md.batch = "";
                        md.rsn = "";
                        md.sch_line_num = "";
                        md.est_arr_date = "";
                        md.packing_instruction = "";
                        md.lot_serial_nr = "";
                        md.lot_serial_qty = "";
                        md.msd_kit = "";
                        md.crp_po_nr = "";
                        md.manufacturing_date = "";
                        md.mode_of_transport = "";
                        md.delivery_order = "";
                        md.rdt_cuspn = item.rdt_cuspn;
                        md.rdt_shipqty = item.rdt_shipqty;
                        md.rdt_billup = item.rdt_billup;
                        md.rsn_no = item.rsn_no;
                        md.inv_invoice = inv;
                        md.ship_date = merge_ship;
                        md.delivery_date = merge_delivery;
                        md.waybill = waybill;
                        md.bolnum = bolnum;
                        md.rsn_totpack = totpack;
                        md.h = "";
                        md.l = "";
                        md.w = "";
                        md.gw = "";
                        md.vw = "";
                        md.container_id = inv + "-" + iRow;
                        md.amt = item.amt;

                        result.Add(md);
                    } // end for: totpack
                } // end foreach: dt
            } // end using: conn

            return result;
        } // end List<TB_DELLCOMM_VMI_REPORT>: GetDellCommVMITable
    } // end class: eRSNModels_Repository
} // end namespace: eRSN_New.Models