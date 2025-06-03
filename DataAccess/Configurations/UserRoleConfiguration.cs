using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            string adminId = "43B64316-DE56-4300-AE86-C298AEA73C7B";
            string customerId = "99D74F43-7E23-41BE-9F98-10C5D6130312";

            string adminRoleId = "18FF4584-E530-4CFE-ADC1-9485EBCC1982";
            string customerRoleId = "A6975C7C-5397-4BBB-B450-2790781BCBAB";

            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = customerId,
                    RoleId = customerRoleId
                }
            );
        }
    }
}