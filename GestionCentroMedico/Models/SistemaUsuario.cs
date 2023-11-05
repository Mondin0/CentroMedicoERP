using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class SistemaUsuario
{
    public int SisId { get; set; }

    public int RolId { get; set; }

    public string SisNombreUsuario { get; set; } = null!;

    public string SisPassword { get; set; } = null!;

    public virtual RolesSistema Rol { get; set; } = null!;
}
