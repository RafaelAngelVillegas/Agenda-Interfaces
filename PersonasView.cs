using Agenda.Models;

namespace Agenda.Views;

public static class PersonasView
{
    public static void MostrarMenu(List<Trabajador> trabajadores)
    {
        while (true)
        {
            switch (AgendaView.MostrarMenu("MENÚ DE PERSONAS",
                "Alta", "Listado", "Buscar", "Modificar", "Eliminar"))
            {
                case "1":
                    Alta(trabajadores);
                    break;
                case "2":
                    Listar(trabajadores);
                    break;
                case "3":
                    Buscar(trabajadores);
                    break;
                case "4":
                    Modificar(trabajadores);
                    break;
                case "5":
                    Eliminar(trabajadores);
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

    private static void Alta(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== ALTA DE TRABAJADOR =====");
        string nombre = PedirTexto("Nombre: ");
        string apellidos = PedirTexto("Apellidos: ");
        string email = PedirEmail("Correo electrónico: ");
        string telefono = PedirTelefono("Teléfono: ");

        trabajadores.Add(new Trabajador(nombre, apellidos, email, telefono));
        Console.WriteLine("\nTrabajador añadido correctamente.");
        AgendaView.Esperar();
    }

    private static void Buscar(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== BUSCAR TRABAJADOR =====");
        Console.WriteLine("1. Por nombre");
        Console.WriteLine("2. Por apellidos");
        Console.WriteLine("3. Por ID");
        Console.Write("Seleccione una opción: ");

        List<Trabajador> resultado;
        switch (Console.ReadLine())
        {
            case "1":
                string nombre = AgendaView.PedirTextoObligatorio("Introduce el nombre: ");
                resultado = trabajadores.Where(t => t.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "2":
                string apellidos = AgendaView.PedirTextoObligatorio("Introduce los apellidos: ");
                resultado = trabajadores.Where(t => t.Apellidos.Equals(apellidos, StringComparison.OrdinalIgnoreCase)).ToList();
                break;
            case "3":
                int id = AgendaView.PedirEntero("Introduce el ID: ");
                resultado = trabajadores.Where(t => t.Id == id).ToList();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                AgendaView.Esperar();
                return;
        }

        MostrarResultados(resultado);
        AgendaView.Esperar();
    }

    private static void Modificar(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== MODIFICAR TRABAJADOR =====");
        int id = AgendaView.PedirEntero("Introduce el ID del trabajador: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == id);

        if (trabajador is null)
        {
            Console.WriteLine("No se encontró ningún trabajador con ese ID.");
            AgendaView.Esperar();
            return;
        }

        Console.WriteLine($"\nTrabajador encontrado: {trabajador}");
        Console.Write("¿Deseas modificar este trabajador? (s/n): ");
        if (!(Console.ReadLine() ?? "").Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Modificación cancelada.");
            AgendaView.Esperar();
            return;
        }

        trabajador.Nombre = PedirTextoOpcional("Nuevo nombre", trabajador.Nombre);
        trabajador.Apellidos = PedirTextoOpcional("Nuevos apellidos", trabajador.Apellidos);
        trabajador.Email = PedirEmailOpcional("Nuevo correo", trabajador.Email);
        trabajador.Telefono = PedirTelefonoOpcional("Nuevo teléfono", trabajador.Telefono);
        Console.WriteLine("\nTrabajador modificado correctamente.");
        AgendaView.Esperar();
    }

    private static void Eliminar(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== ELIMINAR TRABAJADOR =====");
        int id = AgendaView.PedirEntero("Introduce el ID del trabajador: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == id);

        if (trabajador is null)
        {
            Console.WriteLine("No se encontró ningún trabajador con ese ID.");
            AgendaView.Esperar();
            return;
        }

        Console.WriteLine($"\nTrabajador encontrado: {trabajador}");
        Console.Write("¿Seguro que quieres eliminarlo? (s/n): ");
        if (!(Console.ReadLine() ?? "").Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Eliminación cancelada.");
            AgendaView.Esperar();
            return;
        }

        trabajadores.Remove(trabajador);
        Console.WriteLine("\nTrabajador eliminado correctamente.");
        AgendaView.Esperar();
    }

    private static void Listar(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== LISTADO DE TRABAJADORES =====");
        MostrarResultados(trabajadores);
        AgendaView.Esperar();
    }

    private static void MostrarResultados(List<Trabajador> resultado)
    {
        if (resultado.Count == 0)
        {
            Console.WriteLine("No se encontraron resultados.");
            return;
        }

        foreach (Trabajador trabajador in resultado)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(trabajador);
        }
    }

    private static string PedirTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada) && EsTextoValido(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("Debes introducir texto válido.");
        }
    }

    private static string PedirTextoOpcional(string mensaje, string valorActual)
    {
        Console.Write($"{mensaje} [actual: {valorActual}]: ");
        string entrada = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(entrada))
        {
            return valorActual;
        }

        if (EsTextoValido(entrada))
        {
            return entrada.Trim();
        }

        Console.WriteLine("Valor no válido. Se conserva el valor actual.");
        return valorActual;
    }

    private static string PedirEmail(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";
            if (EsEmailValido(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("El correo debe contener '@' y '.'.");
        }
    }

    private static string PedirEmailOpcional(string mensaje, string valorActual)
    {
        Console.Write($"{mensaje} [actual: {valorActual}]: ");
        string entrada = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(entrada))
        {
            return valorActual;
        }

        if (EsEmailValido(entrada))
        {
            return entrada.Trim();
        }

        Console.WriteLine("Correo no válido. Se conserva el valor actual.");
        return valorActual;
    }

    private static string PedirTelefono(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";
            if (EsTelefonoValido(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("El teléfono debe contener solo números.");
        }
    }

    private static string PedirTelefonoOpcional(string mensaje, string valorActual)
    {
        Console.Write($"{mensaje} [actual: {valorActual}]: ");
        string entrada = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(entrada))
        {
            return valorActual;
        }

        if (EsTelefonoValido(entrada))
        {
            return entrada.Trim();
        }

        Console.WriteLine("Teléfono no válido. Se conserva el valor actual.");
        return valorActual;
    }

    private static bool EsTextoValido(string texto)
    {
        return texto.All(caracter => char.IsLetter(caracter) || char.IsWhiteSpace(caracter) ||
            caracter == '-' || caracter == '.');
    }

    private static bool EsEmailValido(string email)
    {
        int posicionArroba = email.IndexOf('@');
        int posicionPunto = email.IndexOf('.');
        return !string.IsNullOrWhiteSpace(email) && posicionArroba > 0 &&
            posicionPunto > posicionArroba + 1;
    }

    private static bool EsTelefonoValido(string telefono)
    {
        return !string.IsNullOrWhiteSpace(telefono) && telefono.All(char.IsDigit);
    }
}
