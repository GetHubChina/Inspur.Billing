using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        #region ����
        /// <summary>
        /// ��ȡ������
        /// </summary>
        private string _uri;
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public string Uri
        {
            get { return _uri; }
            set { Set<string>(ref _uri, value, "Uri"); }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ȡ������
        /// </summary>
        private ICommand _navigationCommand;
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand ?? (_navigationCommand = new RelayCommand<string>(p =>
                {
                    Uri = p;
                }, a =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}