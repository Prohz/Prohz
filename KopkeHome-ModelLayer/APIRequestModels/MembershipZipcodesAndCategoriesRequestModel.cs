namespace KopkeHome_ModelLayer.APIRequestModels
{
#nullable disable
    public class MembershipZipcodesAndCategoriesRequestModel
    {
        public string[] Zipcodes { get; set; }
        public string[] Categories { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }

    }
}
