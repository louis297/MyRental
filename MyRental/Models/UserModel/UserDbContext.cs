//using System;
//using IdentityServer4.EntityFramework.Options;
//using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace MyRental.Models.UserModel
//{
//    public class UserDbContext: ApiAuthorizationDbContext<ApplicationUser>
//    {
//        public UserDbContext(
//            DbContextOptions options,
//            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
//        {
            
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseMySql(CustomSettings.ConnectionString);
//        }
//    }
//}
