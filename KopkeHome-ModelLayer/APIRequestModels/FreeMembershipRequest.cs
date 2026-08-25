// public class FreeMembershipRequest
// {
//     public int UserId { get; set; }
//     public int PlanId { get; set; }
// }

namespace KopkeHome_ModelLayer.ViewModels.PaymentModels
{
    public class FreeMembershipRequest
    {
        public int UserId { get; set; }

        public int PlanId { get; set; }

        public bool ReplaceExisting { get; set; }
    }
}