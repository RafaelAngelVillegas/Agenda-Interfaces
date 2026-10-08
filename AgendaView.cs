namespace Agenda.Views;

public static class AgendaView
{
    public static string MostrarMenuPrincipal()
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("           AGENDA");
        Console.WriteLine("=================================");
        Console.WriteLine("1. Menú de personas");
        Console.WriteLine("2. Menú de empresas");
        Console.WriteLine("3. Salir");
        Console.WriteLine("=================================");
        Console.Write("Selecciona una opción: ");
        return Console.ReadLine() ?? "";
    }

    public static string MostrarMenu(string titulo, params string[] opciones)
    {
        Console.WriteLine();
        Console.WriteLine($"===== {titulo} =====");
        for (int i = 0; i < opciones.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {opciones[i]}");
        }
        Console.WriteLine($"{opciones.Length + 1}. Volver al menú principal");
        Console.Write("Selecciona una opción: ");
        return Console.ReadLine() ?? "";
    }

    public static string PedirTextoObligatorio(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("Este campo es obligatorio.");
        }
    }

    public static int PedirEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int numero))
            {
                return numero;
            }

            Console.WriteLine("Debes introducir un número entero válido.");
        }
    }

    public static void Esperar()
    {
        Console.WriteLine("\nPulsa Enter para continuar...");
        Console.ReadLine();
    }
}
