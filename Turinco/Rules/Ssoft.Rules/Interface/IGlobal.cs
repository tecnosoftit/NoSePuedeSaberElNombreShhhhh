using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.Rules.Interface
{
    public interface ICriterios
    {
        bool GET_CYTIES(string Data_);
        bool GET_CYTIES_COLOMBIA(string Data_);
    }

    public interface ISeleccionarDatos
    {
        string Texto_ { get; set;}
        string _Seleccionar_Precio();
        string _Seleccionar_PrecioDolar();
        string _Seleccionar_Dias();
        string _Seleccionar_FechaExpedicionTickete();
    }
}
