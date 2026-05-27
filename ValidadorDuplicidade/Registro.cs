using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValidadorDuplicidade
{
    internal class Registro
    {
       public string NumeroRegistro;

        public string NomeRegistro;

        public string ValorRegistro;

        public string DataRegistro;



        public override string ToString()
        {
            return $"{NomeRegistro} - {ValorRegistro} - {DataRegistro}";
        }


    }
}
