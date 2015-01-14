using System.Linq;

namespace SolidNavigation {
    public class TaskDetailsViewModel : ObservableObject {
        private TaskViewModel _task;

        public TaskDetailsViewModel(long taskId) {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Title = task.Title };
        }

        public TaskViewModel Task {
            get { return _task; }
        }
    }
}
