using System.Collections.Generic;

namespace SolidNavigation
{
    public class Workspace
    {
        private static Workspace _current;
        public static Workspace Current { get { return _current ?? (_current = new Workspace()); } }

        public List<ListModel> Lists { get; set; }
        public List<TaskModel> Tasks { get; set; }

        public Workspace()
        {
            Lists = new List<ListModel>
            {
                new ListModel {Id = 1, Title = "Groceries"},
                new ListModel {Id = 2, Title = "Restaurants"},
                new ListModel {Id = 3, Title = "Movies"}
            };

            Tasks=new List<TaskModel>
            {
                new TaskModel {Id = 1, ListId = 1, Title = "Ingwer"},
                new TaskModel {Id = 2, ListId = 1, Title = "Karotten"},
                new TaskModel {Id = 3, ListId = 1, Title = "Kokosmilch"},
                new TaskModel {Id = 4, ListId = 2, Title = "The Bird"},
                new TaskModel {Id = 5, ListId = 2, Title = "Lemongrass"},
                new TaskModel {Id = 6, ListId = 3, Title = "The godfather"},
                new TaskModel {Id = 7, ListId = 3, Title = "Casino"}
            };
        }
    }
}
