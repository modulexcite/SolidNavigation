using System.Collections.ObjectModel;

namespace SolidNavigation
{
    public class ListsPageViewModel : ObservableObject
    {
        private ListViewModel _selectedList;
        public ObservableCollection<ListViewModel> Lists { get; set; }

        public ListViewModel SelectedList
        {
            get { return _selectedList; }
            set
            {
                if (Equals(value, _selectedList)) return;
                _selectedList = value;
                NotifyOfPropertyChange(() => SelectedList);

                NavigateService.Current.Navigate(new TaskListTarget(_selectedList.Id));
            }
        }

        public ListsPageViewModel()
        {
            Lists = new ObservableCollection<ListViewModel>();
            foreach (var list in Workspace.Current.Lists)
            {
                Lists.Add(new ListViewModel { Id = list.Id, Title = list.Title });
            }
        }
    }
}
