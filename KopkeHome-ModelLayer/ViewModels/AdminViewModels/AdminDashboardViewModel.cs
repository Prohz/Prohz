using KopkeHome_ModelLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.ViewModels.AdminViewModels
{
#nullable disable
    public class AdminDashboardViewModel
    {
        public int NumberOfActiveUsers { get; set; }
        public int NumberOfHomeOwners { get; set; }
        public int NumberOfContractors { get; set; }
        public int SubscriptionPurchased { get; set; }
        public List<RoleName> RoleName { get; set; }
        public List<User> User { get; set; }
        public List<string> SalesAssociates { get; set; }
    }
    public class RoleName
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumberOffice { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BusinessAddress { get; set; }
        public long UniqueMemberId { get; set; }

    }
}
