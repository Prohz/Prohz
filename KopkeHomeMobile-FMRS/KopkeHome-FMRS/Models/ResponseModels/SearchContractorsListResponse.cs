using KopkeHome_FMRS.Models.RequestModels;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class SearchContractorsListResponse : BindableObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("contractorType")]
        public string ContractorType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profilePic")]
        public string ProfilePic { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("officeNumber")]
        public string OfficeNumber { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("activity")]
        public int Activity { get; set; }

        [JsonProperty("isLiked")]       
        private bool isLiked;
        public bool IsLiked
        {
            get { return isLiked; }
            set { isLiked = value;
                OnPropertyChanged(nameof(IsLiked));
            }
        }
        [JsonProperty("isDisLiked")]       
        private bool isDisLiked;

        public bool IsDisLiked
        {
            get { return isDisLiked; }
            set
            {
                isDisLiked = value;
                OnPropertyChanged(nameof(IsDisLiked));
            }
        }

        [JsonProperty("workStatus")]
        public int WorkStatus { get; set; }

        [JsonProperty("workStatusUpdated")]
        public DateTime WorkStatusUpdated { get; set; }
        private string workStatusImage;

        public string WorkStatusImage
        {
            get { return workStatusImage; }
            set { workStatusImage = value; OnPropertyChanged(nameof(WorkStatusImage)); }
        }
        private string dislikeImage;

        public string DislikeImage
        {
            get { return dislikeImage; }
            set
            {
                dislikeImage = value;
                OnPropertyChanged(nameof(DislikeImage));               
            }
        }

        private string likeImage;
        public string LikeImage
        {
            get { return likeImage; }
            set
            {
                likeImage = value; OnPropertyChanged(nameof(LikeImage));
            }
        }
        private bool isLikeOption;

        public bool IsLikeOption
        {
            get { return isLikeOption; }
            set { isLikeOption = value;
                OnPropertyChanged(nameof(IsLikeOption));
            }
        }     
        public Command ViewDetailsCommand { get; set; }
        public Command LikeCommand { get; set; }
        public Command DislikeCommand { get; set; }

    }
}
