using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class Mutual
{
    public int MutId { get; set; }

    public string MutNombre { get; set; } = null!;

    public string MutDescripcion { get; set; } = null!;

    public double MutValor { get; set; }

    public bool? MutActivo { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
