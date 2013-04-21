using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    public class ApiMemberMapping : SubclassMapping<ApiMember>
    {
        public ApiMemberMapping()
        {
            DiscriminatorValue((byte)UserType.ApiMember);

            Property(x => x.Age, map => { map.NotNullable(true); });
            Property(x => x.Gender, map => { map.NotNullable(true); map.Column("GenderID"); });

            Set(
              x => x.DownloadedApplications,
              map =>
              {
                  map.Key(k => k.Column("UserID"));
                  map.Access(Accessor.Field);
              },
              r => r.OneToMany());

            //Set(
            //  x => x.ApplicationTypes,
            //  map =>
            //  {
            //      map.Key(k => k.Column("UserID"));
            //      map.Access(Accessor.Field);
            //  },
            //  r => r.OneToMany());

            ManyToOne(p => p.Country, map =>
            {
                map.NotNullable(true);
                map.Column("CountryID");
                map.Cascade(Cascade.All);
            });
        }
    }
}
