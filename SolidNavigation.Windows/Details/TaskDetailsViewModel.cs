using System.Collections.Generic;
using System.Linq;
using SolidNavigation.Sdk;
using SolidNavigation.Windows.Entities;
using SolidNavigation.Windows.Navigation;
using SolidNavigation.Windows.Tasks;

namespace SolidNavigation.Windows.Details
{
    public class TaskDetailsViewModel : ObservableObject
    {
        private TaskViewModel _task;
        public TaskViewModel Task { get { return _task; } }
        public List<CommentViewModel> Comments { get; set; }
        private CommentViewModel _selectedComment;

        public TaskDetailsViewModel(NavigationTarget target)
        {
            if (target is TaskDetailsTarget)
            {
                LoadData((target as TaskDetailsTarget).TaskId);
            }
            if (target is CommentTarget)
            {
                LoadData((target as CommentTarget).TaskId);
                _selectedComment = Comments.FirstOrDefault(x => x.Id == (target as CommentTarget).CommentId);
            }
        }

        public TaskDetailsViewModel(long taskId)
        {
            LoadData(taskId);
        }

        public TaskDetailsViewModel(long taskId, long commentId)
        {
            LoadData(taskId);
            _selectedComment = Comments.FirstOrDefault(x => x.Id == commentId);
        }

        private void LoadData(long taskId)
        {
            var task = Workspace.Current.Tasks.FirstOrDefault(x => x.Id == taskId);
            _task = new TaskViewModel { Id = taskId, Title = task.Title };

            Comments = (from comment in Workspace.Current.Comments.Where(x => x.TaskId == _task.Id)
                        select new CommentViewModel { Text = comment.Text, Id = comment.Id, }).ToList();
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
    }
}
