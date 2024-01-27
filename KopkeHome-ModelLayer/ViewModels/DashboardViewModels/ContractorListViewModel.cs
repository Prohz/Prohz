namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels
{
    public class ContractorListViewModel
    {
        public int Id { get; set; }
        public string ContractorType { get; set; }
        public string Name { get; set; }
        public string ProfilePic { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Mobile { get; set; }
        public string OfficeNumber { get; set; }
        public string Category { get; set; }
        public int Status { get; set; }
        public int Activity { get; set; }
        public bool IsLiked { get; set; } = false;
        public bool IsDisLiked { get; set; } = false;
        public int? WorkStatus { get; set; }
        public DateTime WorkStatusUpdated { get; set; }
    }
}
