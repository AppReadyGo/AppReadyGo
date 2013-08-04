using System.Linq;
using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries.Admin
{
    public class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, AllMembersResult>
    {
        public AllMembersResult Run(ISession session, GetAllMembersQuery query)
        {
            var res = new AllMembersResult();

            var usersQuery = session.Query<Member>();

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                var str = query.SearchStr.ToLower();
                usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(str) || u.FirstName.ToLower().Contains(str) || u.LastName.ToLower().Contains(str));
            }

            res.Count = usersQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;


            var users = usersQuery.Select(u => new UserFullDetailsResult
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Activated = u.Activated,
                Id = u.Id,
                SpecialAccess = u.SpecialAccess,
                CreateDate = u.CreateDate,
                LastAccessDate = u.LastAccessDate
            });

            if (query.OrderBy == GetAllMembersQuery.OrderByColumn.Email)
            {
                users = query.ASC ? users.OrderBy(u => u.Email) : users.OrderByDescending(u => u.Email);
            }
            else if (query.OrderBy == GetAllMembersQuery.OrderByColumn.Name)
            {
                users = query.ASC ? users.OrderBy(u => u.LastName + " " + u.FirstName) : users.OrderByDescending(u => u.Email);
            }
            else if (query.OrderBy == GetAllMembersQuery.OrderByColumn.CreateDate)
            {
                users = query.ASC ? users.OrderBy(u => u.CreateDate) : users.OrderByDescending(u => u.CreateDate);
            }

            res.Users = users.Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            return res;
        }
    }
}
