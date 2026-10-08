using Agenda.Models;

namespace Agenda.Views;

public static class PersonasView
{
    public static void MostrarMenu(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        while (true)
        {
            switch (AgendaView.MostrarMenu("MENÚ DE PERSONAS",
                "Alta", "Listado", "Buscar", "Modificar", "Eliminar",
                "Asignar o cambiar empresa", "Desvincular de empresa", "Ver empresa",
                "Histórico de empresas"))
            {
                case "1":
                    Alta(trabajadores);
                    break;
                case "2":
                    Listar(trabajadores, empresas);
                    break;
                case "3":
                    Buscar(trabajadores, empresas);
                    break;
                case "4":
                    Modificar(trabajadores, empresas);
                    break;
                case "5":
                    Eliminar(trabajadores);
                    break;
                case "6":
                    AsignarEmpresa(trabajadores, empresas);
                    break;
                case "7":
                    DesvincularEmpresa(trabajadores, empresas);
                    break;
                case "8":
                    VerEmpresa(trabajadores, empresas);
                    break;
                case "9":
                    MostrarHistorico(trabajadores);
                    break;
                case "10":
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

        Trabajador trabajador = new(nombre, apellidos, email, telefono);
        trabajadores.Add(trabajador);
        Console.WriteLine($"\nTrabajador añadido correctamente. ID asignado: {trabajador.Id}.");
        AgendaView.Esperar();
    }

    private static void Buscar(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        Console.WriteLine("\n===== BUSCAR TRABAJADOR =====");
        Console.WriteLine("1. Por nombre");
        Console.WriteLine("2. Por apellidos");
        Console.WriteLine("3. Por ID");
        Console.WriteLine("4. Varios por nombre");
        Console.WriteLine("5. Varios por apellidos");
        Console.WriteLine("6. Varios por ID");
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
            case "4":
                List<string> nombres = AgendaView.PedirVariosTextos("Introduce los nombres separados por comas: ");
                resultado = trabajadores.Where(t => nombres.Contains(t.Nombre, StringComparer.OrdinalIgnoreCase)).ToList();
                break;
            case "5":
                List<string> apellidosVarios = AgendaView.PedirVariosTextos("Introduce los apellidos separados por comas: ");
                resultado = trabajadores.Where(t => apellidosVarios.Contains(t.Apellidos, StringComparer.OrdinalIgnoreCase)).ToList();
                break;
            case "6":
                List<int> ids = AgendaView.PedirVariosIds();
                resultado = trabajadores.Where(t => ids.Contains(t.Id)).ToList();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                AgendaView.Esperar();
                return;
        }

        MostrarResultados(resultado, empresas);
        AgendaView.Esperar();
    }

    private static void Modificar(List<Trabajador> trabajadores, List<Empresa> empresas)
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

        Console.WriteLine($"\nTrabajador encontrado: {FormatoTrabajador(trabajador, empresas)}");
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

    private static void Listar(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        Console.WriteLine("\n===== LISTADO DE TRABAJADORES =====");
        MostrarResultados(trabajadores, empresas);
        AgendaView.Esperar();
    }

    private static void MostrarResultados(List<Trabajador> resultado, List<Empresa> empresas)
    {
        if (resultado.Count == 0)
        {
            Console.WriteLine("No se encontraron resultados.");
            return;
        }

        foreach (Trabajador trabajador in resultado)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(FormatoTrabajador(trabajador, empresas));
        }
    }

    private static void AsignarEmpresa(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        Console.WriteLine("\n===== ASIGNAR O CAMBIAR EMPRESA =====");
        int personaId = AgendaView.PedirEntero("Introduce el ID de la persona: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == personaId);
        if (trabajador is null)
        {
            Console.WriteLine("No se encontró ninguna persona con ese ID.");
            AgendaView.Esperar();
            return;
        }

        int empresaId = AgendaView.PedirEntero("Introduce el ID de la empresa: ");
        Empresa? empresa = empresas.FirstOrDefault(e => e.Estado && e.Id == empresaId);
        if (empresa is null)
        {
            Console.WriteLine("No se encontró ninguna empresa activa con ese ID.");
            AgendaView.Esperar();
            return;
        }

        trabajador.AsignarEmpresa(empresa);
        Console.WriteLine($"La persona {trabajador.Id} pertenece ahora a {empresa.NombreComercial}.");
        AgendaView.Esperar();
    }

    private static void DesvincularEmpresa(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        Console.WriteLine("\n===== DESVINCULAR PERSONA =====");
        int personaId = AgendaView.PedirEntero("Introduce el ID de la persona: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == personaId);
        if (trabajador is null)
        {
            Console.WriteLine("No se encontró ninguna persona con ese ID.");
        }
        else if (trabajador.EmpresaId is null)
        {
            Console.WriteLine("La persona ya no tiene una empresa asignada.");
        }
        else
        {
            Empresa? empresa = empresas.FirstOrDefault(e => e.Id == trabajador.EmpresaId);
            trabajador.DesvincularEmpresa();
            Console.WriteLine(empresa is null
                ? "La persona ha quedado sin empresa asignada."
                : $"La persona ha sido desvinculada de {empresa.NombreComercial}.");
        }

        AgendaView.Esperar();
    }

    private static void VerEmpresa(List<Trabajador> trabajadores, List<Empresa> empresas)
    {
        Console.WriteLine("\n===== VER EMPRESA DE UNA PERSONA =====");
        int personaId = AgendaView.PedirEntero("Introduce el ID de la persona: ");
        Trabajador? trabajador = trabajadores.FirstOrDefault(t => t.Id == personaId);
        if (trabajador is null)
        {
            Console.WriteLine("No se encontró ninguna persona con ese ID.");
        }
        else
        {
            Console.WriteLine(FormatoTrabajador(trabajador, empresas));
        }

        AgendaView.Esperar();
    }

    private static string FormatoTrabajador(Trabajador trabajador, List<Empresa>? empresas)
    {
        string empresa = trabajador.EmpresaId is null
            ? "Sin empresa"
            : empresas?.FirstOrDefault(e => e.Id == trabajador.EmpresaId)?.NombreComercial
                ?? $"Empresa ID {trabajador.EmpresaId} (no disponible)";

        return $"{trabajador} | Empresa: {empresa}";
    }

    private static void MostrarHistorico(List<Trabajador> trabajadores)
    {
        Console.WriteLine("\n===== HISTÓRICO LABORAL =====");
        if (trabajadores.Count == 0)
        {
            Console.WriteLine("No hay personas registradas.");
            AgendaView.Esperar();
            return;
        }

        foreach (Trabajador trabajador in trabajadores)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Persona ID: {trabajador.Id} | {trabajador.Nombre} {trabajador.Apellidos}");

            if (trabajador.HistorialEmpresas.Count == 0)
            {
                Console.WriteLine("Empresas: ninguna.");
            }
            else
            {
                foreach (HistorialEmpresa periodo in trabajador.HistorialEmpresas)
                {
                    string estado = periodo.FechaFin is null
                        ? "ACTUAL"
                        : $"hasta {periodo.FechaFin.Value:dd/MM/yyyy HH:mm}";
                    Console.WriteLine($"Empresa ID: {periodo.EmpresaId} | {periodo.NombreComercial} | " +
                        $"desde {periodo.FechaInicio:dd/MM/yyyy HH:mm} | {estado}");
                }
            }

            string empresaActual = trabajador.EmpresaId is null
                ? "Ninguna"
                : trabajador.HistorialEmpresas.LastOrDefault(periodo =>
                    periodo.EmpresaId == trabajador.EmpresaId && periodo.FechaFin is null)?.NombreComercial
                    ?? $"Empresa ID {trabajador.EmpresaId}";
            Console.WriteLine($"Trabaja actualmente en: {empresaActual}");
        }

        AgendaView.Esperar();
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
