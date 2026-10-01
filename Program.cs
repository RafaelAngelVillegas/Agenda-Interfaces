using Agenda.Models;

class Program
{
    static List<Trabajador> trabajadores = new List<Trabajador>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("          AGENDA DE TRABAJADORES");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Alta");
            Console.WriteLine("2. Buscar");
            Console.WriteLine("3. Modificar");
            Console.WriteLine("4. Eliminar");
            Console.WriteLine("5. Listado");
            Console.WriteLine("6. Salir");
            Console.WriteLine("=================================");
            Console.Write("Selecciona una opción: ");

            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    AltaTrabajador();
                    break;
                case "2":
                    BuscarTrabajador();
                    break;
                case "3":
                    ModificarTrabajador();
                    break;
                case "4":
                    EliminarTrabajador();
                    break;
                case "5":
                    ListarTrabajadores();
                    break;
                case "6":
                    Console.WriteLine("Saliendo del programa...");
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    Esperar();
                    break;
            }
        }
    }

    static void AltaTrabajador()
    {
        Console.WriteLine();
        Console.WriteLine("===== ALTA DE TRABAJADOR =====");

        string nombre = PedirTexto("Nombre: ");
        string apellidos = PedirTexto("Apellidos: ");
        string email = PedirEmail("Correo electrónico: ");
        string telefono = PedirTelefono("Teléfono: ");

        Trabajador trabajador = new Trabajador(nombre, apellidos, email, telefono);
        trabajadores.Add(trabajador);

        Console.WriteLine("\nTrabajador añadido correctamente.");
        Esperar();
    }

    static void BuscarTrabajador()
    {
        Console.WriteLine();
        Console.WriteLine("===== BUSCAR TRABAJADOR =====");
        Console.WriteLine("1. Por nombre");
        Console.WriteLine("2. Por apellidos");
        Console.WriteLine("3. Por ID");
        Console.Write("Seleccione una opción: ");

        string opcion = Console.ReadLine() ?? "";

        switch (opcion)
        {
            case "1":
                string nombre = PedirTexto("Introduce el nombre: ");
                MostrarResultado(trabajadores.Where(t => t.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)).ToList());
                break;
            case "2":
                string apellidos = PedirTexto("Introduce los apellidos: ");
                MostrarResultado(trabajadores.Where(t => t.Apellidos.Equals(apellidos, StringComparison.OrdinalIgnoreCase)).ToList());
                break;
            case "3":
                int id = PedirEntero("Introduce el ID: ");
                MostrarResultado(trabajadores.Where(t => t.Id == id).ToList());
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }

        Esperar();
    }

    static void ModificarTrabajador()
    {
        Console.WriteLine();
        Console.WriteLine("===== MODIFICAR TRABAJADOR =====");

        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay trabajadores registrados.");
            Esperar();
            return;
        }

        int id = PedirEntero("Introduce el ID del trabajador: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == id);

        if (trabajador == null)
        {
            Console.WriteLine("No se encontró ningún trabajador con ese ID.");
            Esperar();
            return;
        }

        Console.WriteLine($"\nTrabajador encontrado: {trabajador}");
        Console.Write("¿Deseas modificar este trabajador? (s/n): ");
        string confirm = Console.ReadLine() ?? "";

        if (!confirm.Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Modificación cancelada.");
            Esperar();
            return;
        }

        trabajador.Nombre = PedirTextoOpcional("Nuevo nombre [actual: " + trabajador.Nombre + "]: ", trabajador.Nombre);
        trabajador.Apellidos = PedirTextoOpcional("Nuevos apellidos [actual: " + trabajador.Apellidos + "]: ", trabajador.Apellidos);
        trabajador.Email = PedirEmailOpcional("Nuevo correo [actual: " + trabajador.Email + "]: ", trabajador.Email);
        trabajador.Telefono = PedirTelefonoOpcional("Nuevo teléfono [actual: " + trabajador.Telefono + "]: ", trabajador.Telefono);

        Console.WriteLine("\nTrabajador modificado correctamente.");
        Esperar();
    }

    static void EliminarTrabajador()
    {
        Console.WriteLine();
        Console.WriteLine("===== ELIMINAR TRABAJADOR =====");

        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay trabajadores registrados.");
            Esperar();
            return;
        }

        int id = PedirEntero("Introduce el ID del trabajador: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == id);

        if (trabajador == null)
        {
            Console.WriteLine("No se encontró ningún trabajador con ese ID.");
            Esperar();
            return;
        }

        Console.WriteLine($"\nTrabajador encontrado: {trabajador}");
        Console.Write("¿Seguro que quieres eliminarlo? (s/n): ");
        string confirm = Console.ReadLine() ?? "";

        if (!confirm.Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Eliminación cancelada.");
            Esperar();
            return;
        }

        trabajadores.Remove(trabajador);
        Console.WriteLine("\nTrabajador eliminado correctamente.");
        Esperar();
    }

    static void ListarTrabajadores()
    {
        Console.WriteLine();
        Console.WriteLine("===== LISTADO DE TRABAJADORES =====");

        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay trabajadores registrados.");
            Esperar();
            return;
        }

        foreach (Trabajador trabajador in trabajadores)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(trabajador);
        }

        Esperar();
    }

    static void MostrarResultado(List<Trabajador> resultado)
    {
        if (resultado.Count == 0)
        {
            Console.WriteLine("No se encontraron resultados.");
            return;
        }

        Console.WriteLine("\nResultados encontrados:");
        foreach (Trabajador trabajador in resultado)
        {
            Console.WriteLine(trabajador);
        }
    }

    static string PedirTexto(string mensaje)
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

    static string PedirTextoOpcional(string mensaje, string valorActual)
    {
        Console.Write(mensaje);
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

    static string PedirEmail(string mensaje)
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

    static string PedirEmailOpcional(string mensaje, string valorActual)
    {
        Console.Write(mensaje);
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

    static string PedirTelefono(string mensaje)
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

    static string PedirTelefonoOpcional(string mensaje, string valorActual)
    {
        Console.Write(mensaje);
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

    static int PedirEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int numero))
            {
                return numero;
            }

            Console.WriteLine("Debes introducir un número entero válido.");
        }
    }

    static bool EsTextoValido(string texto)
    {
        return texto.All(caracter => char.IsLetter(caracter) || char.IsWhiteSpace(caracter) || caracter == '-' || caracter == '.');
    }

    static bool EsEmailValido(string email)
    {
        return !string.IsNullOrWhiteSpace(email)
            && email.Contains("@")
            && email.Contains(".")
            && email.IndexOf('@') > 0
            && email.IndexOf('.') > email.IndexOf('@') + 1;
    }

    static bool EsTelefonoValido(string telefono)
    {
        return !string.IsNullOrWhiteSpace(telefono) && telefono.All(char.IsDigit);
    }

    static void Esperar()
    {
        Console.WriteLine("\nPulsa Enter para continuar...");
        Console.ReadLine();
    }
}
