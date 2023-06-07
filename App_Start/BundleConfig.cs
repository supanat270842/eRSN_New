using System.Web;
using System.Web.Optimization;

namespace eRSN_New
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/sweetalert.min.js.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/jszip.min.js",
                        "~/Scripts/DataTables/dataTables.buttons.min.js",
                        "~/Scripts/DataTables/buttons.html5.min.js",
                        "~/Scripts/default.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/DataTables/css/buttons/dataTables.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/assets/fontawesome/css/fontawesome.css",
                      "~/assets/fontawesome/css/bands.css",
                      "~/assets/fontawesome/css/solid.css"));

            // ********************** LOGIN ************************ //

            bundles.Add(new ScriptBundle("~/bundles/loginn").Include(
                      "~/Scripts/login/bootstrap.min.js",
                      "~/Scripts/login/bootstrap-colorpalette.js",
                      "~/Scripts/login/bootstrap-hover-dropdown.min.js",
                      "~/Scripts/login/jquery.blockUI.js",
                      "~/Scripts/login/jquery.cookie.js",
                      "~/Scripts/login/jquery.icheck.min.js",
                      "~/Scripts/login/jquery.mousewheel.js",
                      "~/Scripts/login/jquery-ui-1.10.2.custom.min.js",
                      "~/Scripts/login/LayoutPage.js",
                      "~/Scripts/login/less-1.5.0.min.js",
                      "~/Scripts/login/perfect-scrollbar.js",
                      "~/Scripts/login/sweetalert.min.js.js"));

            bundles.Add(new StyleBundle("~/Content/loginn").Include(
                      "~/Content/login/bootstrap.min.css",
                      "~/Content/login/bootstrap-colorpalette.css",
                      "~/Content/login/font-awesome/css/font-awesome.min.css",
                      "~/Content/login/main.css",
                      "~/Content/login/main-responsive.css",
                      "~/Content/login/perfect-scrollbar.css",
                      "~/Content/login/print.css",
                      "~/Content/login/style.css",
                      "~/Content/login/theme_navy.css"));
        }
    }
}
