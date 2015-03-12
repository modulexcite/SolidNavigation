using System.Linq;
using Windows.UI.Xaml;

namespace SolidNavigation
{
    public class TaskDetailsViewModel : ObservableObject
    {
        private TaskViewModel _task;

        public TaskDetailsViewModel(long taskId)
        {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Title = task.Title };
        }

        public TaskDetailsViewModel(long taskId, long commentId)
        {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Title = task.Title };
            ShowCommentsVisibility = Visibility.Collapsed;
            Comment = "This is comment " + commentId;
        }

        public TaskViewModel Task
        {
            get { return _task; }
        }

        public string Comment { get; set; }
        public Visibility ShowCommentsVisibility { get; set; }
    }
}
