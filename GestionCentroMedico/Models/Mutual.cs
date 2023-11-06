using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCentroMedico.Models;

public partial class Mutual
{
    public int MutId { get; set; }
    [Required]
    public string MutNombre { get; set; } = null!;
    [Required]
    public string MutDescripcion { get; set; } = null!;
    [Required]
    public double MutValor { get; set; }
    [Required]
    public bool? MutActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
