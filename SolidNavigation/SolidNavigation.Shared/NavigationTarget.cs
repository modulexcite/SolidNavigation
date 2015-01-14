using System;
using SolidNavigation.Sdk;

namespace SolidNavigation {
    public class HomeTarget : NavigationTarget {
        public override Type PageType { get { return typeof(ListsPage); } }
    }

    public class TaskListTarget : NavigationTarget {
        public long ListId {
            get { return (long)_data["ListId"]; }
            set { _data["ListId"] = value; }
        }

        public override Type PageType { get { return typeof(TasksPage); } }

        public TaskListTarget(long listId) {
            ListId = listId;
        }
    }

    public class TaskDetailsTarget : NavigationTarget {
        public long TaskId {
            get { return (long)_data["TaskId"]; }
            set { _data["TaskId"] = value; }
        }

        public override Type PageType { get { return typeof(TaskDetailsPage); } }

        public TaskDetailsTarget(long taskId) {
            TaskId = taskId;
        }
    }

    public class CommentsTarget : NavigationTarget {
        public long TaskId {
            get { return (long)_data["TaskId"]; }
            set { _data["TaskId"] = value; }
        }

        public override Type PageType { get { return typeof(CommentsPage); } }

        public CommentsTarget(long taskId) {
            TaskId = taskId;
        }
    }
}