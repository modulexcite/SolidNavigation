using SolidNavigation.Sdk;

namespace SolidNavigation
{
    public class HomeTarget : NavigationTarget
    {
    }

    public class TaskListTarget : NavigationTarget
    {
        public long ListId { get; private set; }

        public TaskListTarget(long listId)
        {
            ListId = listId;
        }

        public override string ToString()
        {
            return base.ToString() + "\nListId=" + ListId;
        }
    }

    public class TaskDetailsTarget : NavigationTarget
    {
        public long TaskId { get; set; }

        public TaskDetailsTarget(long taskId)
        {
            TaskId = taskId;
        }
        public override string ToString()
        {
            return base.ToString() + "\nTaskId=" + TaskId;
        }
    }

    public class CommentTarget : NavigationTarget
    {
        public long TaskId { get; set; }
        public long CommentId { get; set; }

        public CommentTarget(long taskId)
        {
            TaskId = taskId;
        }

        public CommentTarget(long taskId, long commentId)
        {
            TaskId = taskId;
            CommentId = commentId;
        }

        public override string ToString()
        {
            return base.ToString() + "\nTaskId=" + TaskId + ", CommentId=" + CommentId;
        }
    }
}