using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCentroMedico.Models;

public partial class Medico
{
    public int MedId { get; set; }
    [Required]
    public string MedNombre { get; set; } = null!;
    [Required]
    public string MedApellido { get; set; } = null!;
    [Required]
    public string MedEspecialidad { get; set; } = null!;
    [Required]
    public string MedMatricula { get; set; } = null!;
    [Required]
    public bool MedActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
