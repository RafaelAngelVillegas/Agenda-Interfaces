using Agenda.Models;
using Agenda.Views;

class Program
{
    static readonly List<Trabajador> trabajadores = new();
    static readonly List<Empresa> empresas = new();

    static void Main()
    {
        while (true)
        {
            switch (AgendaView.MostrarMenuPrincipal())
            {
                case "1":
                    PersonasView.MostrarMenu(trabajadores);
                    break;
                case "2":
                    EmpresasView.MostrarMenu(empresas);
                    break;
                case "3":
                    Console.WriteLine("Saliendo del programa...");
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    AgendaView.Esperar();
                    break;
            }
        }
    }
}
