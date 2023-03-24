using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? IdHistorial { get; set; }

    public virtual Historial? IdHistorialNavigation { get; set; }

    //Propiedades en stored 
    public int Numero { get; set; }
    public int Resultado { get; set; }
    public DateTime FechaHora { get; set; }
}
