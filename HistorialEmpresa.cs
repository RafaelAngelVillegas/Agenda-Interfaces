namespace Agenda.Models;

public class HistorialEmpresa
{
    public int EmpresaId { get; }
    public string NombreComercial { get; }
    public DateTime FechaInicio { get; }
    public DateTime? FechaFin { get; set; }

    public HistorialEmpresa(int empresaId, string nombreComercial, DateTime fechaInicio)
    {
        EmpresaId = empresaId;
        NombreComercial = nombreComercial;
        FechaInicio = fechaInicio;
    }
}
