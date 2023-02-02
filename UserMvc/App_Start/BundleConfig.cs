using System.Security.Cryptography;
using System.Web;
using System.Web.Optimization;
using System.Xml.Linq;

namespace UserMvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
          
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));    
            
            bundles.Add(new StyleBundle("~/Css").Include(
                      "~/Content/PrimaryCss.css")); 

            bundles.Add(new StyleBundle("~/Scripts").Include(
                      "~/Scripts/jquery-3.6.0.min.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/ScriptsUI").Include(
                      "~/Scripts/jquery-ui-1.13.2.js", 
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include(
                    "~/Content/fontawesome/all.min.css"
                ));
        }
    }
}
