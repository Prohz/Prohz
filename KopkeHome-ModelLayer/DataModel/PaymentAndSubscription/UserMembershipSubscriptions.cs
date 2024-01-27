
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KopkeHome_ModelLayer.DataModel.PaymentAndSubscription
{
#nullable disable
    [Table("UserMembershipSubscriptions")]
    public class UserMembershipSubscriptions
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(100)]
        public string Email { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int PlanId { get; set; }
        [StringLength(50)]
        public string StripeStatus { get; set; }
        [StringLength(50)]
        public string PaymentStatus { get; set; }
        [StringLength(250)]
        public string StripeSubscriptionId { get; set; }
        [StringLength(250)]
        public string StripeCustomerID { get; set; }
        [StringLength(250)]
        public string StripePriceId { get; set; }
        [StringLength(250)]
        public string InvoiceNumber { get; set; }
        [StringLength(1000)]
        public string   InvoiceUrl          { get; set; }
        public DateTime PeriodStartDate     { get; set; }
        public DateTime PeriodEndDate       { get; set; }
        public DateTime CreatedOn           { get; set; }
        public DateTime CancelledOn         { get; set; }
        public DateTime UpgradedOn          { get; set; }
        public DateTime DowngradedOn        { get; set; }
        public int Extensions { get; set; } =0;
        public bool     IsActive            { get; set; }

    }
}
