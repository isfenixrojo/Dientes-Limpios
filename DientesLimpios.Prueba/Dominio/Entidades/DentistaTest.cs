using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Prueba.Dominio.Entidades
{
    [TestClass]
    public class DentistaTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionReglaDeNegocio))]   
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("israel@gmail.com");
            new Dentista(null!, email);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExcepcionReglaDeNegocio))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            new Dentista("Israel", email);
        }

    }
}
