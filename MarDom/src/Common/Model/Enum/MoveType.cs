using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Enum
{
    public enum MoveType
    {
        [Description("Entrada")]
        Input = 0,
        [Description("Salida")]
        Output =1
    }
}
