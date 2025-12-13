using System.ComponentModel.DataAnnotations;
using TodoApp.Enums;

namespace TodoApp.ViewModels
{
    public class TodoAddVM
    {
        [Required(ErrorMessage ="veuillez saisir le Libelle de votre tache")]
        public String Libelle { set; get; }

        [Required(ErrorMessage ="veuillez saisir la Description de votre tache")]

        public String Description { set; get; }

        [Required(ErrorMessage ="Veuillez Definir la Date Limite de votre tache ")]
        [DataType(DataType.Date)]
        public DateTime DateLimit { get; set; }

        [Required(ErrorMessage ="Veuillez Definir le Statut de votre tache")]

        public Statut statut { get; set; }
    }
}
