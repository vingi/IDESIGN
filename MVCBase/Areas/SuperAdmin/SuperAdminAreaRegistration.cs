using System.Web.Mvc;

namespace MVCBase.Areas.SuperAdmin
{
    public class SuperAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SuperAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SuperAdmin_default",
                "SuperAdmin/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
