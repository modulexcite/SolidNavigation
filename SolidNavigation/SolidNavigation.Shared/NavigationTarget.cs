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

    public class CommentsTarget : NavigationTarget
    {
        public long TaskId { get; set; }
        public long Id { get; set; }

        public CommentsTarget(long taskId)
        {
            TaskId = taskId;
        }
        public CommentsTarget(long taskId, long id)
        {
            TaskId = taskId;
            Id = id;
        }

        public override string ToString()
        {
            return base.ToString() + "\nTaskId=" + TaskId + ", Id=" + Id;
        }
    }
}