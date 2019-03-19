using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mvcCACSLA.Test
{
    [TestClass]
    public class UnitTest1
    {
        private const string ConSignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇ";
        private const string SinSignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUcC";
        public static string RemoverSignosAcentos(string texto)
        {
            var textoSinAcentos = string.Empty;

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }
            return textoSinAcentos;
        }

        [TestMethod]
        public void TestAcentos()
        {
            var s = "5-512-3 4 Relación de equipos empresariales registrados en la Expo Emprendedora 2015-2 y 2015-1 LAE.pdf";
            var nuevos = RemoverSignosAcentos(s);


        }


    }
}
