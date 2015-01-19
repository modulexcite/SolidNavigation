using System;
using SolidNavigation.Sdk;

namespace SolidNavigation {
    public class HomeTarget : NavigationTarget {
        public override Type PageType { get { return typeof(ListsPage); } }
    }

    public class TaskListTarget : NavigationTarget {
        public long ListId { get; private set; }

        public override Type PageType { get { return typeof(TasksPage); } }

        public TaskListTarget(long listId) {
            ListId = listId;
        }

        public override string ToString() {
            return base.ToString() + "\nListId=" + ListId;
        }
    }

    public class TaskDetailsTarget : NavigationTarget {
        public long TaskId { get; set; }
        public override Type PageType { get { return typeof(TaskDetailsPage); } }

        public TaskDetailsTarget(long taskId) {
            TaskId = taskId;
        }
        public override string ToString() {
            return base.ToString() + "\nTaskId=" + TaskId;
        }
    }

    public class CommentsTarget : NavigationTarget {
        public long TaskId { get; set; }
        public long Id { get; set; }

        public override Type PageType { get { return typeof(CommentsPage); } }

        public CommentsTarget(long taskId) {
            TaskId = taskId;
        }
        public CommentsTarget(long taskId, long id) {
            TaskId = taskId;
            Id = id;
        }

        public override string ToString() {
            return base.ToString() + "\nTaskId=" + TaskId + ", Id=" + Id;
        }
    }
}