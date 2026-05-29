using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValidadorDuplicidade
{
    public class Registro
    {
       public string NumeroRegistro;

        public string NomeRegistro;

        public string ValorRegistro;

        public string DataRegistro;



        public override string ToString()
        {
            return $"{NomeRegistro} - {ValorRegistro} - {DataRegistro}";
        }

        public string[] RetornArray()
        {
            return new string[] { NumeroRegistro, NomeRegistro, ValorRegistro, DataRegistro };
        }

    }
}
