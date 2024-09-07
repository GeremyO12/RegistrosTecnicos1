using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnicos1.Models;

public class Tecnicos
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual que 1.")]
    public int IdTecnico { get; set; }
    [Required(ErrorMessage = "El campo nombre es obligatorio.")]
    public string? Nombre { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "El campo sueldo por hora debe ser mayor que cero.")]
    public double SueldoHora { get; set; }


    public int TipoTecnicoId { get; set; }
    public TiposTecnicos TipoTecnico { get; set; }
}
