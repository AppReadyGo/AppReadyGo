//using AppReadyGo.Core.Commands;
//using AppReadyGo.Domain.Model;
//using NHibernate;
//using AppReadyGo.Domain.Model.Users;
//using AppReadyGo.Core;

//namespace AppReadyGo.Domain.CommandHandlers
//{
//    public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, int>
//    {
//        private ISecurityContext securityContext;

//        public CreatePortfolioCommandHandler(ISecurityContext securityContext)
//        {
//            this.securityContext = securityContext;
//        }

//        public int Execute(ISession session, CreatePortfolioCommand cmd)
//        {
//            var user = session.Get<User>(securityContext.CurrentUser.Id);
//            var portfolio = new Portfolio(cmd.Description, cmd.TimeZone, user);
//            session.Save(portfolio);
//            return portfolio.Id;
//        }
//    }
//}
