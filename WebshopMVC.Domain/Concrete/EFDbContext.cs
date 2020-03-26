using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Concrete
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	class EFDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles {get;set;}
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ItemRating> ItemRatings { get; set; }
	}
}
