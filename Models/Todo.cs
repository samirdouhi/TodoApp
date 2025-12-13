using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.Models
{
    public class Todo
    {
        public String Libelle { set; get; }

        public String Description { set; get; }
    
        public DateTime DateLimit { get; set; }

        public Statut statut { get; set; }
    }
}
