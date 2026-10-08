namespace Agenda.Models;

public class Empresa
{
    private static int _ultimoId = 1;

    public int Id { get; private set; }
    public string Cif { get; set; }
    public string NombreComercial { get; set; }
    public string Telefono { get; set; }
    public string CorreoElectronico { get; set; }
    public string Direccion { get; set; }
    public bool Estado { get; set; }

    public Empresa(string cif, string nombreComercial, string telefono, string correoElectronico, string direccion)
    {
        Id = _ultimoId++;
        Cif = cif;
        NombreComercial = nombreComercial;
        Telefono = telefono;
        CorreoElectronico = correoElectronico;
        Direccion = direccion;
        Estado = true;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre comercial: {NombreComercial} | CIF: {Cif} | " +
            $"Teléfono: {Telefono} | Correo electrónico: {CorreoElectronico} | Dirección: {Direccion}";
    }
}
