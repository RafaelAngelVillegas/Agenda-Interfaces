namespace Agenda.Models;

public class Trabajador
{
    private static int _ultimoId = 1;

    public int Id { get; private set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public int? EmpresaId { get; set; }
    public List<HistorialEmpresa> HistorialEmpresas { get; } = new();

    public Trabajador(string nombre, string apellidos, string email, string telefono)
    {
        Id = _ultimoId++;
        Nombre = nombre;
        Apellidos = apellidos;
        Email = email;
        Telefono = telefono;
        EmpresaId = null;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre} | Apellidos: {Apellidos} | Email: {Email} | Teléfono: {Telefono}";
    }

    public void AsignarEmpresa(Empresa empresa)
    {
        if (EmpresaId == empresa.Id)
        {
            return;
        }

        CerrarPeriodoActual();
        EmpresaId = empresa.Id;
        HistorialEmpresas.Add(new HistorialEmpresa(empresa.Id, empresa.NombreComercial, DateTime.Now));
    }

    public bool DesvincularEmpresa()
    {
        if (EmpresaId is null)
        {
            return false;
        }

        CerrarPeriodoActual();
        EmpresaId = null;
        return true;
    }

    private void CerrarPeriodoActual()
    {
        HistorialEmpresa? periodoActual = HistorialEmpresas.LastOrDefault(periodo =>
            periodo.EmpresaId == EmpresaId && periodo.FechaFin is null);
        if (periodoActual is not null)
        {
            periodoActual.FechaFin = DateTime.Now;
        }
    }
}
