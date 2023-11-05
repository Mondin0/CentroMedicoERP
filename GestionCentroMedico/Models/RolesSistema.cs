using System;
using System.Collections.Generic;

namespace GestionCentroMedico.Models;

public partial class RolesSistema
{
    public int RolId { get; set; }

    public string RolDescripcion { get; set; } = null!;

    public virtual ICollection<SistemaUsuario> SistemaUsuarios { get; set; } = new List<SistemaUsuario>();
}
