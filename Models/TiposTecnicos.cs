using System.ComponentModel.DataAnnotations;

namespace RegistrosTecnicos1.Models;

public class TiposTecnicos
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual que 1.")]
    public int TipoTecnicoId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; }
    public ICollection<Tecnicos> Tecnicos { get; set; }
}
