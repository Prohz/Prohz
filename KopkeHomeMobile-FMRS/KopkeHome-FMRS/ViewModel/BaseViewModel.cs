using CommunityToolkit.Mvvm.ComponentModel;
using KopkeHome_FMRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {       
        
        private ImageSource profileImage;
        public ImageSource ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value; OnPropertyChanged(nameof(ProfileImage));
            }
        }
        private bool visibleStateList;
        public bool VisibleStateList
        {
            get { return visibleStateList; }
            set { visibleStateList = value; OnPropertyChanged(nameof(VisibleStateList)); }
        }
        //[ObservableProperty]
        //bool visibleStateList = false;
        [ObservableProperty]
        bool visibleCityList = false;
        // <summary>
        /// This property is used for Now
        /// </summary>
        private string current;
        public string Current
        {
            get { return current; }
            set
            {
                current = value;
                OnPropertyChanged(nameof(Current));
            }
        }
      
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; 
            OnPropertyChanged(nameof(Status));
            }
        }
        public BaseViewModel()
        {
            IsVisibleCurrentStatus = false;
            Current = UserProperties.StatusImage;
            if (Current != null)
            {
                Current = UserProperties.StatusImage;
                IsVisibleCurrentStatus = true;
            }
        }
        private bool isLoading;    
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                this.OnPropertyChanged(nameof(this.IsLoading));
            }
        }
   

        private bool closeFlyout;
        public bool CloseFlyout
        {
            get { return closeFlyout; }
            set { closeFlyout = value; OnPropertyChanged(nameof(CloseFlyout)); }
        }

        private bool isVisibleCurrentStatus;
        public bool IsVisibleCurrentStatus
        {
            get { return isVisibleCurrentStatus; }
            set { isVisibleCurrentStatus = value; OnPropertyChanged(nameof(IsVisibleCurrentStatus)); }
        }

        /// <summary>
        /// This command is used for the command Wrong
        /// </summary>
        private Command commandCross;
        public Command CommandCross
        {
            get
            {
                return commandCross = new Command(() =>
                {
                    try
                    {
                        CloseFlyout = false;
                    }
                    catch (Exception ex)
                    {
                    }
                });
            }
        }

    }
}
