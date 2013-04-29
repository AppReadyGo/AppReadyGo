using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    internal class MemberMapping : SubclassMapping<Member>
    {
        public MemberMapping()
        {
            DiscriminatorValue((byte)UserType.Member);

            Set(
              x => x.Applications,
              map =>
              {
                  map.Key(k => k.Column("UserID"));
                  map.Access(Accessor.Field);
              },
              r => r.OneToMany());
        }
    }
}
