using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    public class StaffMapping : SubclassMapping<Staff>
    {
        public StaffMapping()
        {
            DiscriminatorValue((byte)UserType.Staff);

            Set(
               x => x.Roles,
               map =>
               {
                   map.Schema("usr");
                   map.Table("UserStaffRoles");
                   map.Key(k => k.Column("UserID"));
                   map.Access(Accessor.Field);
               },
               r => r.Element(map => { map.Column("RoleID"); map.NotNullable(true); }));
        }
    }
}
