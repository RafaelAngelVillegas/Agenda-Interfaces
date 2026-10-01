namespace Agenda.Models;

public class Trabajador
{
    private static int _ultimoId = 1;

    public int Id { get; private set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public Trabajador(string nombre, string apellidos, string email, string telefono)
    {
        Id = _ultimoId++;
        Nombre = nombre;
        Apellidos = apellidos;
        Email = email;
        Telefono = telefono;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre} | Apellidos: {Apellidos} | Email: {Email} | Teléfono: {Telefono}";
    }
}
