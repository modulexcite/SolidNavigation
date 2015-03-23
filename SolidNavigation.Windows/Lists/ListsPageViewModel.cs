using System.Collections.Generic;
using System.Linq;
using SolidNavigation.Windows.Entities;
using SolidNavigation.Windows.Navigation;

namespace SolidNavigation.Windows.Lists
{
    public class ListsPageViewModel : ObservableObject
    {
        private ListViewModel _selectedList;
        public List<ListViewModel> Lists { get; set; }

        public ListsPageViewModel()
        {
            Lists = (from list in Workspace.Current.Lists
                     select new ListViewModel { Id = list.Id, Title = list.Title }).ToList();
        }

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
    }
}
