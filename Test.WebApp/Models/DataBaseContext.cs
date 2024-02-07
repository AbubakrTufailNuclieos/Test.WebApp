using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
        public DbSet<patient_payments> patient_Payments { get; set; }

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

    public class patient_payments
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        [Column("pid")]
        public long pid { get; set; }
        [Column("app_id")]
        public long? app_id { get; set; }
        [Column("paymentType")]
        public string paymentType { get; set; }
        [Column("amount", TypeName = "decimal(18, 2)")]
        public decimal amount { get; set; }
        [Column("paymentDateTime", TypeName = "datetime")]
        public DateTime paymentDateTime { get; set; }
        [Column("is_active")]
        public bool? is_active { get; set; }
        [Column("is_deleted")]
        public bool is_deleted { get; set; }
        [Column("card_id")]
        public long? card_id { get; set; }
        [Column("card_Key")]
        public string card_Key { get; set; }
        [Column("transaction_Key")]
        public string transaction_Key { get; set; }
        [Column("createdBy")]
        public long? createdBy { get; set; }
        [Column("is_Otherpayment")]
        public bool? is_Otherpayment { get; set; }
        [Column("paymentMethod")]
        public string paymentMethod { get; set; }
        [Column("paymentDescription")]
        public string paymentDescription { get; set; }
        [Column("stripe_amount_captured")]
        public decimal? StripeAmountCaptured { get; set; }
        [Column("stripe_charge_create_on")]
        public DateTime? StripeChargeCreatedOn { get; set; }
        [Column("is_stripe_charge_captured")]
        public bool? IsStripeChargeCaptured { get; set; }
        [Column("is_payment_refund")]
        public bool? IsPaymentRefund { get; set; }
        [Column(nameof(ppId))]
        public Guid? ppId { get; set; }
        [Column(nameof(isPreferredPartnerPayment))]
        public bool isPreferredPartnerPayment { get; set; }
    }

}
