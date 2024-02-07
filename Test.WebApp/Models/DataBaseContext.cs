using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.WebApp.Models
{
	public class DataBaseContext:DbContext
	{
        public DataBaseContext(DbContextOptions options):base(options)
        {            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

	}

	[Table("tblUser")]
	public class User
	{
        public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<Role> Roles { get; set; } = default!;
        public Address? Address { get; set; }
    }

	[Table("tblRole")]
	public class Role
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
    }

	[Table("tblAddress")]
	public class Address
	{
        public int Id { get; set; }
        public string ParmanentAddress { get; set; } = default!;
		public string CurrentAddress { get; set; } = default!;
    }

	public class UserModel
	{
        public int Id { get; set; }
		public string FisrtName { get; set; } = default!;

    }
}
