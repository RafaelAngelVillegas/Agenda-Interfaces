using Agenda.Models;

namespace Agenda.Views;

public static class EmpresasView
{
    public static void MostrarMenu(List<Empresa> empresas)
    {
        while (true)
        {
            switch (AgendaView.MostrarMenu("MENÚ DE EMPRESAS",
                "Alta de empresa", "Listado de empresas", "Buscar empresa",
                "Modificar empresa", "Dar de baja empresa"))
            {
                case "1":
                    Alta(empresas);
                    break;
                case "2":
                    Listar(empresas);
                    break;
                case "3":
                    Buscar(empresas);
                    break;
                case "4":
                    Modificar(empresas);
                    break;
                case "5":
                    DarDeBaja(empresas);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    AgendaView.Esperar();
                    break;
            }
        }
    }

    private static void Alta(List<Empresa> empresas)
    {
        Console.WriteLine("\n===== ALTA DE EMPRESA =====");
        string cif = AgendaView.PedirTextoObligatorio("CIF: ");
        string nombreComercial = AgendaView.PedirTextoObligatorio("Nombre comercial: ");
        string telefono = PedirTelefono("Teléfono: ");
        string correoElectronico = PedirEmail("Correo electrónico: ");
        string direccion = AgendaView.PedirTextoObligatorio("Dirección: ");

        empresas.Add(new Empresa(cif, nombreComercial, telefono, correoElectronico, direccion));
        Console.WriteLine("\nEmpresa añadida correctamente.");
        AgendaView.Esperar();
    }

    private static void Listar(List<Empresa> empresas)
    {
        Console.WriteLine("\n===== LISTADO DE EMPRESAS =====");
        MostrarResultados(empresas.Where(e => e.Estado).ToList());
        AgendaView.Esperar();
    }

    private static void Buscar(List<Empresa> empresas)
    {
        Console.WriteLine("\n===== BUSCAR EMPRESA =====");
        Console.WriteLine("1. Por CIF");
        Console.WriteLine("2. Por nombre comercial");
        Console.WriteLine("3. Por ID");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                string cif = AgendaView.PedirTextoObligatorio("Introduce el CIF: ");
                MostrarResultados(empresas.Where(e => e.Estado &&
                    e.Cif.Contains(cif, StringComparison.OrdinalIgnoreCase)).ToList());
                break;
            case "2":
                string nombre = AgendaView.PedirTextoObligatorio("Introduce el nombre comercial: ");
                MostrarResultados(empresas.Where(e => e.Estado &&
                    e.NombreComercial.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList());
                break;
            case "3":
                int id = AgendaView.PedirEntero("Introduce el ID: ");
                MostrarResultados(empresas.Where(e => e.Estado && e.Id == id).ToList());
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }

        AgendaView.Esperar();
    }

    private static void Modificar(List<Empresa> empresas)
    {
        Console.WriteLine("\n===== MODIFICAR EMPRESA =====");
        int id = AgendaView.PedirEntero("Introduce el ID de la empresa: ");
        Empresa? empresa = empresas.FirstOrDefault(e => e.Estado && e.Id == id);

        if (empresa is null)
        {
            Console.WriteLine("No se encontró ninguna empresa activa con ese ID.");
            AgendaView.Esperar();
            return;
        }

        Console.WriteLine($"\nEmpresa encontrada: {empresa}");
        Console.Write("¿Deseas modificar esta empresa? (s/n): ");
        if (!(Console.ReadLine() ?? "").Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Modificación cancelada.");
            AgendaView.Esperar();
            return;
        }

        empresa.Cif = PedirValorObligatorioOActual("Nuevo CIF", empresa.Cif);
        empresa.NombreComercial = PedirValorObligatorioOActual("Nuevo nombre comercial", empresa.NombreComercial);
        empresa.Telefono = PedirTelefonoOpcional("Nuevo teléfono", empresa.Telefono);
        empresa.CorreoElectronico = PedirEmailOpcional("Nuevo correo electrónico", empresa.CorreoElectronico);
        empresa.Direccion = PedirValorObligatorioOActual("Nueva dirección", empresa.Direccion);
        Console.WriteLine("\nEmpresa modificada correctamente.");
        AgendaView.Esperar();
    }

    private static string PedirTelefono(string mensaje)
    {
        while (true)
        {
            string entrada = AgendaView.PedirTextoObligatorio(mensaje);
            if (entrada.All(char.IsDigit))
            {
                return entrada;
            }

            Console.WriteLine("El teléfono debe contener solo números.");
        }
    }

    private static string PedirTelefonoOpcional(string mensaje, string valorActual)
    {
        while (true)
        {
            Console.Write($"{mensaje} [actual: {valorActual}] (Enter para conservar): ");
            string entrada = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return valorActual;
            }

            if (entrada.All(char.IsDigit))
            {
                return entrada.Trim();
            }

            Console.WriteLine("El teléfono debe contener solo números.");
        }
    }

    private static string PedirEmail(string mensaje)
    {
        while (true)
        {
            string entrada = AgendaView.PedirTextoObligatorio(mensaje);
            if (EsEmailValido(entrada))
            {
                return entrada;
            }

            Console.WriteLine("El correo debe contener '@' y un punto después de '@'.");
        }
    }

    private static string PedirEmailOpcional(string mensaje, string valorActual)
    {
        while (true)
        {
            Console.Write($"{mensaje} [actual: {valorActual}] (Enter para conservar): ");
            string entrada = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return valorActual;
            }

            if (EsEmailValido(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("El correo debe contener '@' y un punto después de '@'.");
        }
    }

    private static bool EsEmailValido(string correo)
    {
        int posicionArroba = correo.IndexOf('@');
        int posicionPunto = correo.IndexOf('.', posicionArroba + 1);
        return posicionArroba > 0 && posicionPunto > posicionArroba + 1 &&
            posicionPunto < correo.Length - 1;
    }

    private static string PedirValorObligatorioOActual(string campo, string valorActual)
    {
        Console.Write($"{campo} [actual: {valorActual}] (Enter para conservar): ");
        string entrada = Console.ReadLine() ?? "";
        return string.IsNullOrWhiteSpace(entrada) ? valorActual : entrada.Trim();
    }

    private static void DarDeBaja(List<Empresa> empresas)
    {
        Console.WriteLine("\n===== BAJA DE EMPRESA =====");
        int id = AgendaView.PedirEntero("Introduce el ID de la empresa: ");
        Empresa? empresa = empresas.FirstOrDefault(e => e.Estado && e.Id == id);

        if (empresa is null)
        {
            Console.WriteLine("No se encontró ninguna empresa activa con ese ID.");
            AgendaView.Esperar();
            return;
        }

        Console.WriteLine($"\nEmpresa encontrada: {empresa}");
        Console.Write("¿Seguro que quieres darla de baja? (s/n): ");
        if (!(Console.ReadLine() ?? "").Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Baja cancelada.");
            AgendaView.Esperar();
            return;
        }

        empresa.Estado = false;
        Console.WriteLine("\nEmpresa dada de baja correctamente.");
        AgendaView.Esperar();
    }

    private static void MostrarResultados(List<Empresa> resultado)
    {
        if (resultado.Count == 0)
        {
            Console.WriteLine("No se encontraron empresas activas.");
            return;
        }

        foreach (Empresa empresa in resultado)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(empresa);
        }
    }
}
