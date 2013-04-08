//using AppReadyGo.Core.Commands;
//using AppReadyGo.Domain.Model;
//using NHibernate;

//namespace AppReadyGo.Domain.CommandHandlers
//{
//    public class UpdatePortfolioCommandHandler : ICommandHandler<UpdatePortfolioCommand, int>
//    {
//        public int Execute(ISession session, UpdatePortfolioCommand cmd)
//        {
//            var obj = session.Get<Portfolio>(cmd.Id);
//            obj.Update(cmd.Description, cmd.TimeZone);
//            return obj.Id;
//        }
//    }
//}
