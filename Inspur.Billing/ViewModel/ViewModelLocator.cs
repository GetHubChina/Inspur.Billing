/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Inspur.Billing"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Inspur.Billing.ViewModel.Issue;
using Inspur.Billing.ViewModel.Login;
using Inspur.Billing.ViewModel.Setting;

namespace Inspur.Billing.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BasicViewModel>();
            SimpleIoc.Default.Register<CreditViewModel>();
            SimpleIoc.Default.Register<PrintViewModel>();
            SimpleIoc.Default.Register<PinViewModel>();
            SimpleIoc.Default.Register<TaxPayerSettingVm>();
            SimpleIoc.Default.Register<ParameterSettingVm>();
            SimpleIoc.Default.Register<SoftwareSettingVm>();
        }
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public BasicViewModel Basic
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BasicViewModel>();
            }
        }
        public CreditViewModel Credit
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreditViewModel>();
            }
        }
        public PrintViewModel Print
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PrintViewModel>();
            }
        }
        public PinViewModel Pin
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PinViewModel>();
            }
        }
        public TaxPayerSettingVm TaxPayerSetting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaxPayerSettingVm>();
            }
        }
        public ParameterSettingVm ParameterSetting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParameterSettingVm>();
            }
        }
        public SoftwareSettingVm SoftwareSetting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoftwareSettingVm>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}