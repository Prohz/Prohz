using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models
{
    public class UserProfileModel:ObservableObject
    {
       
        public string UserName { get; set; }

      
        public string NormalizedUserName { get; set; }

       
        public string Email { get; set; }

      
        public string NormalizedEmail { get; set; }

      
        public bool EmailConfirmed { get; set; }

    
        public string PasswordHash { get; set; }

       
        public string SecurityStamp { get; set; }
  
        public string ConcurrencyStamp { get; set; }

      
        public bool PhoneNumberConfirmed { get; set; }

       
        public bool TwoFactorEnabled { get; set; }

      
        public DateTime LockoutEnd { get; set; }

       
        public bool LockoutEnabled { get; set; }

        
        public int AccessFailedCount { get; set; }


        public int Id { get; set; }
        public int RoleId { get; set; }

        //important  buser profile field
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string PhoneNumberOffice { get; set; }


        public string PhoneNumber { get; set; }


        public string Business_Name { get; set; }


        public string _City { get; set; }


        public string Business_Address { get; set; }


        public string Zip_Code { get; set; }


        public string State { get; set; }


        public bool IsEmailVerified { get; set; }

        public String Profile_Picture { get; set; }
        public DateTime CreatedOn { get; set; }


        public DateTime ModifiedOn { get; set; }


        public string UniqueMemberId { get; set; }

        public int WorkStatus { get; set; }

        public DateTime WorkStatusModifiedOn { get; set; }
        public bool IsDocumentsVerified { get; set; }
        public int HeardAboutProhzFrom { get; set; }
    }
}

