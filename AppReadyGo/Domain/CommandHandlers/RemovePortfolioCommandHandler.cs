//using AppReadyGo.Core.Commands;
//using AppReadyGo.Domain.Model;
//using NHibernate;

//namespace AppReadyGo.Domain.CommandHandlers
//{
//    public class RemovePortfolioCommandHandler : ICommandHandler<RemovePortfolioCommand, int>
//    {
//        public int Execute(ISession session, RemovePortfolioCommand cmd)
//        {
//            var obj = session.Get<Portfolio>(cmd.Id);
//            session.Delete(obj);
//            return obj.Id;
//        }
//    }
//}
