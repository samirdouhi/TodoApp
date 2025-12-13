using TodoApp.ViewModels;
using TodoApp.Models;

namespace TodoApp.Mappers
{
    public class TodoMapper
    {
        public static Todo GetTodosFromTodoAddVm(TodoAddVM vm) //static pour appeller avec le nom de la class direct sans intancier
        {
            return new Todo
            {
                Libelle = vm.Libelle,
                Description = vm.Description,
                DateLimit = vm.DateLimit,
                statut = vm.statut,
            };

        }
    }
}
