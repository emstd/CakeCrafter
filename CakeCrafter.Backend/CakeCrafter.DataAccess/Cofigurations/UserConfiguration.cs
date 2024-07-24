using CakeCrafter.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CakeCrafter.DataAccess.Cofigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasData
            (
                new UserEntity()
                {
                    Id = new Guid("152bac7d-86b6-49fc-a4ac-6a5ebf8f71c4"),
                    Email = "test_user@gmail.ru",
                    Password = "Pa$$w0rd",
                }
            ); ;
        }
    }
}
