using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCentroMedico.Models;

public partial class Cliente
{
    public int CliId { get; set; }
    [Required]
    public string CliNombre { get; set; } = null!;
    [Required]
    public string CliApellido { get; set; } = null!;
    [Required]
    public string CliEmail { get; set; } = null!;

    public int MedId { get; set; }
    [Required]
    public int MutId { get; set; }
    [Required]
    public bool CliActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
