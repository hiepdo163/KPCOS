//using Bogus;
//using KPCOS.Data.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KPCOS.Data.Extensions
//{
//    public static class ModelBuilderExtension
//    {
//        public static void SeedData(this ModelBuilder modelBuilder)
//        {
//            var userFaker = new Faker<User>()
//                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
//                .RuleFor(u => u.Username, f => f.Internet.UserName())
//                .RuleFor(u => u.Password, f => f.Internet.Password())
//                .RuleFor(u => u.Fullname, f => f.Name.FullName())
//                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
//                .RuleFor(u => u.Address, f => f.Address.FullAddress())
//                .RuleFor(u => u.Role, f => f.PickRandom(new[] { "User", "Consultation", "Employee", "Customer", "Admin" }))
//                .RuleFor(u => u.Status, f => f.Random.Bool())
//                .RuleFor(u => u.CreateDate, f => f.Date.Past(2))
//                .RuleFor(u => u.UpdateDate, f => f.Date.Recent(1));

//            var customerFaker = new Faker<Customer>()
//                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
//                .RuleFor(c => c.LoyaltyPoint, f => f.Random.Int(0, 1000))
//                .RuleFor(c => c.MembershipStatus, f => f.PickRandom(new[] { "Bronze", "Silver", "Gold", "Platinum" }));

//            var customers = customerFaker.Generate(10);

//            var users = userFaker.Generate(10);

//            modelBuilder.Entity<User>().HasData(users);
//        }
//    }
//}
