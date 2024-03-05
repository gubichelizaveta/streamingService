using course_project.Views;
using DocumentFormat.OpenXml.Bibliography;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;



namespace course_project.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Page HomePage;
        private Page SearchPage;
        private Page Mediateka_Page;
        private Page LikedTrackPage;
        private Page CreatePlaylistPage;

        public event PropertyChangedEventHandler PropertyChanged;
        private Page _currentPage;
        public Page CurrentPage { get; set; }
        public MainWindowViewModel()
        {
            HomePage = new HomePage();
            SearchPage = new SearchPage();
            Mediateka_Page = new Mediateka_Page();
            LikedTrackPage = new LikedTrackPage();
            CreatePlaylistPage = new CreatePlaylistPage();

            CurrentPage = HomePage;
        }
        
        public ICommand Home_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = HomePage);
            }
          
        }
        public Page Search_Click
        {
            get
            {
                _currentPage = SearchPage;
                return _currentPage;
            }
            set
            {
                CurrentPage = _currentPage;
                OnPropertyChanged();
            }
            
        }
        public ICommand Mediateka_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = Mediateka_Page);
                
            }
            
        }
        public ICommand Playlist_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = CreatePlaylistPage);
            }
        }
        public ICommand LikeTrack_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = LikedTrackPage);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
