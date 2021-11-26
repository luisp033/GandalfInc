using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Utils
{
    public static class StringUtils
    {

        public static string ToTitleCase(this string param) {

            var cultura = new CultureInfo("pt-PT", false).TextInfo;
            var resultado = cultura.ToTitleCase(param);
            return resultado;
        }


    }
}
