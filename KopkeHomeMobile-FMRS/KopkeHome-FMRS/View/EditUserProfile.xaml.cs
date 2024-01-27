using KopkeHome_FMRS.Models;
using Microsoft.AppCenter.Crashes;

namespace KopkeHome_FMRS.View;

public partial class EditUserProfile : ContentPage
{
	public EditUserProfile(UserProfileModel userProfileModel)
	{		  
	   InitializeComponent();
	   try
	   {
            VM.FirstNameProfile = userProfileModel.First_Name;
            VM.LaststNameProfile = userProfileModel.Last_Name;
            VM.Firstname = userProfileModel.First_Name;
            VM.LastName = userProfileModel.Last_Name;
            VM.UniqueMeberId = userProfileModel.UniqueMemberId;
            VM.UserEmail = userProfileModel.Email;
            VM.MobileNumber = userProfileModel.PhoneNumber;
            VM.OfficeNumber = userProfileModel.PhoneNumberOffice;
            VM.StateName = userProfileModel.State;
            VM.BusinessName = userProfileModel.Business_Name;
            VM.BusinessAdress = userProfileModel.Business_Address;
            VM.StateName = userProfileModel._City;
            VM.CityName = userProfileModel.State;
            VM.ZipCode = userProfileModel.Zip_Code;
            if (userProfileModel.Profile_Picture != null)
            {
                MemoryStream stream = new(Convert.FromBase64String(userProfileModel.Profile_Picture.ToString()));
                ImageSource image = ImageSource.FromStream(() => stream);
                VM.Profile_Image = image;
            }           
            VM.AccountType = userProfileModel.Business_Name;
        }
	   catch (Exception ex)
	   {
		Crashes.TrackError(ex);
	   }	  
    }
}