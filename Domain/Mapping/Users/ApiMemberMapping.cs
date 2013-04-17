using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    public class ApiMemberMapping : ClassMapping<ApiMember>
    {
        public ApiMemberMapping()
        {
            DiscriminatorValue((byte)UserType.ApiMember);

            Property(x => x.Email, map => { map.Length(50); map.NotNullable(true); });
            Property(x => x.Password, map => { map.Length(50); map.NotNullable(true); });
            Property(x => x.PasswordSalt, map => { map.Length(50); map.NotNullable(true); });
            Property(x => x.CreateDate, map => { map.NotNullable(true); });
            Property(x => x.LastAccessDate, map => { });
            Property(x => x.Activated, map => { map.NotNullable(true); });
            Property(x => x.Unsubscribed, map => { map.NotNullable(true); });
            Property(x => x.FirstName, map => { map.Length(100); });
            Property(x => x.LastName, map => { map.Length(100); });
            Property(x => x.SpecialAccess, map => { map.NotNullable(true); });

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

            Set(
              x => x.Country,
              map =>
              {
                  map.Key(k => k.Column("UserID"));
                  map.Access(Accessor.Field);
              },
              r => r.OneToMany());
        }
    }
}
