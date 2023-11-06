using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCentroMedico.Models;

public partial class Turno
{
    [Required]
    public int TurId { get; set; }
    [Required]
    public int CliId { get; set; }
    [Required]
    public int MedId { get; set; }
    [Required]
    public int MutId { get; set; }
    [Required]
    public DateTime TurFecha { get; set; }
    [Required]
    public double TurValor { get; set; }
    [Required]
    public bool TurPagoEfectivo { get; set; }
    [Required]
    public bool TurDescuentaPrepaga { get; set; }

    public virtual Cliente Cli { get; set; } = null!;

    public virtual Medico Med { get; set; } = null!;

    public virtual Mutual Mut { get; set; } = null!;
}
