using KopkeHome_ModelLayer.DataModel;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IBusinessProfile
    {
        Task<BusinessProfileDataModel> BusinessProfile(BusinessProfileDataModel userDataModel);
        Task<BusinessProfileDataModel> UpdateBusinessProfile(BusinessProfileDataModel userDataModel);

        Task<BusinessProfileSubContractor> BusinessProfileSubContractor(BusinessProfileSubContractor userDataModel);
        Task<BusinessProfileSubContractor> UpdateBusinessProfileSubContractor(BusinessProfileSubContractor userDataModel);
    }
}
