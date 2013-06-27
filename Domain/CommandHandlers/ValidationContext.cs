using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using System.Text.RegularExpressions;
using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.CommandHandlers
{
    public class ValidationContext : IValidationContext
    {
        private ISession session = null;
        public const string MatchEmailPattern = "^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])$";

        public ValidationContext(ISession session)
        {
            this.session = session;
        }
        public bool IsEmailExists(string email, int? userId = null)
        {
            var query = this.session.Query<User>()
                            .Where(u => u.Email.ToLower() == email.ToLower());
            if (userId.HasValue)
            {
                query = query.Where(u => userId.Value != u.Id);
            }
            return query.Any();
        }

        public bool IsCorrectEmail(string email)
        {
            return Regex.IsMatch(email, MatchEmailPattern);
        }

        public bool IsCorrectPassword(string password)
        {
            return true;
        }

        public bool IsExistsTag(string tag)
        {
            return false;
        }
    }
}
