using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Pages.AdminPanel
{
    public static class ManageAdminNavPages
    {
        public static string ClientUsers => "ClientUsers";
        public static string BusinessUsers => "BusinessUsers";
        public static string Admins => "Admins";
        public static string RoyaltyCards => "RoyaltyCards";
        public static string AdminPanel => "AdminPanel";

        public static bool IsCurrentPage(ViewContext viewContext, string page) => CurrentPage(viewContext) == page;

        public static string GetCurrentNavClass(ViewContext viewContext, string page) => IsCurrentPage(viewContext,page)? "active" : null;

        private static string CurrentPage(ViewContext viewContext)
        {
            var pageName = viewContext.RouteData.Values["page"].ToString();
            if (pageName.Contains(ClientUsers))
                return ClientUsers;
            else if (pageName.Contains(BusinessUsers))
                return BusinessUsers;
            else if (pageName.Contains(Admins))
                return Admins;
            else if (pageName.Contains(RoyaltyCards))
                return RoyaltyCards;
            else
                return AdminPanel;
        }
    }
}
