﻿using System.Web;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.Commands.Users;

namespace AppReadyGo.Core
{
    public interface ISecurityContext
    {
        CurrentUserDetails CurrentUser { get; }
        void ClearCurrentUserDetails();
    }

    public class CurrentUserDetails
    {
        public int Id { get; private set; }

        public string DisplayName { get; private set; }

        public string Email { get; private set; }

        public UserType Type { get; private set; }

        public CurrentUserDetails(int id, UserType type, string email, string displayName)
        {
            this.Id = id;
            this.Email = email;
            this.DisplayName = displayName;
            this.Type = type;
        }
    }

    public class SecurityContext : ISecurityContext
    {
        public CurrentUserDetails CurrentUser { get; private set; }

        public SecurityContext(IObjectContainer container)
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userDetails = HttpContext.Current.Session["CurrentUserDetails"] as CurrentUserDetails;
                if (userDetails == null)
                {
                    int userId = int.Parse(HttpContext.Current.User.Identity.Name);
                    var details = container.RunQuery(new GetUserDetailsByIdQuery(userId));
                    string displayName = string.IsNullOrEmpty(details.FirstName) || string.IsNullOrEmpty(details.LastName) ? details.Email : string.Format("{0} {1}", details.FirstName, details.LastName);
                    this.CurrentUser = new CurrentUserDetails(userId, details.Type, details.Email, displayName);
                    HttpContext.Current.Session["CurrentUserDetails"] = this.CurrentUser;
                }
                else
                {
                    this.CurrentUser = userDetails;
                }
            }
        }

        public void ClearCurrentUserDetails()
        {
            HttpContext.Current.Session["CurrentUserDetails"] = null;
        }
    }
}
