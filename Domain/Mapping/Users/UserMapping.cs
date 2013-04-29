using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    internal class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Schema("usr");
            Table("Users");
            DiscriminatorValue((byte)UserType.Member);

            Discriminator(map => { map.Column("UserTypeID")                                                                                                                                 ; });
            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Type, x => { x.NotNullable(true); x.Column("UserTypeID"); x.Access(Accessor.ReadOnly); x.Insert(false); x.Update(false); });
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
            Property(x => x.AcceptedTermsAndConditions, map => { map.NotNullable(true); });
            Property(x => x.Membership, map => { map.NotNullable(true); map.Column("MembershipID"); });
         }
    }
}
