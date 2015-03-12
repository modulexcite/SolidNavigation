using System.Linq;

namespace SolidNavigation
{
    public class CommentsPageViewModel : ObservableObject
    {
        private TaskViewModel _task;

        public CommentsPageViewModel(long taskId)
        {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Title = "Comments for task " + task.Title };
        }

        public TaskViewModel Task
        {
            get { return _task; }
        }
    }
}
