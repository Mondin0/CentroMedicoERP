using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class Medico
{
    public int MedId { get; set; }

    public string MedNombre { get; set; } = null!;

    public string MedApellido { get; set; } = null!;

    public string MedEspecialidad { get; set; } = null!;

    public string MedMatricula { get; set; } = null!;

    public bool MedActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
