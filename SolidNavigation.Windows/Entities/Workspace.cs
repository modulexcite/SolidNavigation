using System.Collections.Generic;
using SpartakiadeDemo.Entities;

namespace SolidNavigation.Windows.Entities
{
    public class Workspace
    {
        private static Workspace _current;
        public static Workspace Current { get { return _current ?? (_current = new Workspace()); } }

        public List<ListModel> Lists { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<CommentModel> Comments { get; set; }

        public Workspace()
        {
            Lists = new List<ListModel>
            {
                new ListModel {Id = 1, Title = "Groceries"},
                new ListModel {Id = 2, Title = "Restaurants"},
                new ListModel {Id = 3, Title = "Movies"}
            };

            Tasks = new List<TaskModel>
            {
                new TaskModel {Id = 1, ListId = 1, Title = "Ingwer"},
                new TaskModel {Id = 2, ListId = 1, Title = "Karotten"},
                new TaskModel {Id = 3, ListId = 1, Title = "Kokosmilch"},
                
                new TaskModel {Id = 4, ListId = 2, Title = "The Bird"},
                new TaskModel {Id = 5, ListId = 2, Title = "Lemongrass"},
                
                new TaskModel {Id = 6, ListId = 3, Title = "The godfather"},
                new TaskModel {Id = 7, ListId = 3, Title = "Casino"}
            };

            Comments = new List<CommentModel>();
            foreach (var task in Tasks)
            {
                for (int i = 0; i < 10; i++)
                {
                    Comments.Add(new CommentModel { Id = i * task.Id, TaskId = task.Id, Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." });
                }
            }
        }
    }
}
