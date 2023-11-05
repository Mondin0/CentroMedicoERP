using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class Turno
{
    public int TurId { get; set; }

    public int CliId { get; set; }

    public int MedId { get; set; }

    public int MutId { get; set; }

    public DateTime TurFecha { get; set; }

    public double TurValor { get; set; }

    public bool TurPagoEfectivo { get; set; }

    public bool TurDescuentaPrepaga { get; set; }

    public virtual Cliente Cli { get; set; } = null!;

    public virtual Medico Med { get; set; } = null!;

    public virtual Mutual Mut { get; set; } = null!;
}
