//using System.Linq;
//using AppReadyGo.Core.Queries.Users;
//using AppReadyGo.Core.QueryResults;
//using AppReadyGo.Core.QueryResults.Users;
//using AppReadyGo.Domain.Model;
//using NHibernate;
//using NHibernate.Linq;

//namespace AppReadyGo.Domain.Queries
//{
//    public class GetPortfolioDetailsQueryHandler : IQueryHandler<GetPortfolioDetailsQuery, PortfolioDetailsResult>
//    {
//        public PortfolioDetailsResult Run(ISession session, GetPortfolioDetailsQuery query)
//        {
//            return session.Query<Portfolio>()
//                    .Where(p => p.Id == query.Id)
//                    .Select(p => new PortfolioDetailsResult
//                    {
//                        Id = p.Id,
//                        Description = p.Description,
//                        TimeZone = p.TimeZone
//                    })
//                    .SingleOrDefault();
//        }
//    }
//}
