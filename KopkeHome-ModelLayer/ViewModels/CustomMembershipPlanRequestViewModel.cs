using KopkeHome_ModelLayer.DataModel.MembershipBenifits;

namespace KopkeHome_ModelLayer.ViewModels
{
    public class CustomMembershipPlanRequestViewModel
    {
        public CustomZipcodesRequest? CustomZipcodesRequest { get; set; }
        public string? UserName { get; set; }
        public string? OfficePhone { get; set; }
        public string? HomePhone { get; set; }
        public string? Email { get; set; }

    }
}
