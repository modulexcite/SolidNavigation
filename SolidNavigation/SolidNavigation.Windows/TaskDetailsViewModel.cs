using System.Collections.Generic;
using System.Linq;

namespace SolidNavigation
{
    public class TaskDetailsViewModel : ObservableObject
    {
        private TaskViewModel _task;
        public List<CommentViewModel> Comments { get; set; }
        private CommentViewModel _selectedComment;

        public TaskDetailsViewModel(long taskId)
        {
            LoadData(taskId);
        }

        public TaskDetailsViewModel(long taskId, long commentId)
        {
            LoadData(taskId);
            _selectedComment = Comments.FirstOrDefault(x => x.Id == commentId);
            // scroll to comment
        }

        private void LoadData(long taskId)
        {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel {Id=taskId, Title = task.Title };
            Comments = new List<CommentViewModel>();
            for (int i = 0; i < 10; i++)
            {
                Comments.Add(new CommentViewModel { Id = i });
            }

        }

        public CommentViewModel SelectedComment
        {
            get { return _selectedComment; }
            set
            {
                if (Equals(value, _selectedComment)) return;
                _selectedComment = value;
                NotifyOfPropertyChange(() => _selectedComment);

                NavigateService.Current.Navigate(new CommentTarget(_task.Id, _selectedComment.Id));
            }
        }


        public TaskViewModel Task
        {
            get { return _task; }
        }
    }
}
