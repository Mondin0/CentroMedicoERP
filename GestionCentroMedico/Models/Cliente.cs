using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class Cliente
{
    public int CliId { get; set; }

    public string CliNombre { get; set; } = null!;

    public string CliApellido { get; set; } = null!;

    public string CliEmail { get; set; } = null!;

    public int MedId { get; set; }

    public int MutId { get; set; }

    public bool CliActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
