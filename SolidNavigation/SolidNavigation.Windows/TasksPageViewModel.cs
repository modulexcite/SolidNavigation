using System.Collections.ObjectModel;
using System.Linq;

namespace SolidNavigation
{
    public class TasksPageViewModel : ObservableObject
    {
        public ObservableCollection<TaskViewModel> Tasks { get; set; }
        private TaskViewModel _selectedTask;
        public string ListTitle { get; set; }

        public TasksPageViewModel(long listId)
        {
            Tasks = new ObservableCollection<TaskViewModel>();
            foreach (var task in Workspace.Current.Tasks.Where(x => x.ListId == listId))
            {
                Tasks.Add(new TaskViewModel { Title = task.Title, Id = task.Id });
            }

            ListTitle = "Tasks from list " + Workspace.Current.Lists.FirstOrDefault(x => x.Id == listId).Title;
        }

        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (Equals(value, _selectedTask)) return;
                _selectedTask = value;
                NotifyOfPropertyChange(() => _selectedTask);

                NavigateService.Current.Navigate(new TaskDetailsTarget(_selectedTask.Id));
            }
        }
    }
}
