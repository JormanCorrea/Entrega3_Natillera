using Examen_3_ServWeb_Natillera.Clases;
using Examen_3_ServWeb_Natillera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen_3_ServWeb_Natillera.Controllers
{
    [Authorize]
    [RoutePrefix("api/Eventos")]
    public class EventosController : ApiController
    {
        // Registrar evento
        [HttpPost]
        [Route("Registrar")]
        public string Registrar([FromBody] Evento evento)
        {
            clsEvento cls = new clsEvento();
            cls.evento = evento;
            return cls.Insertar();
        }

        // Consultar eventos por tipo, nombre o fecha
        [HttpGet]
        [Route("Consultar")]
        public IQueryable Consultar(string tipo, string nombre, DateTime? fecha)
        {
            clsEvento cls = new clsEvento();
            return cls.ConsultarPorFiltro(tipo, nombre, fecha);
        }

        // Actualizar evento
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Evento evento)
        {
            clsEvento cls = new clsEvento();
            cls.evento = evento;
            return cls.Actualizar();
        }

        // Eliminar evento
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id)
        {
            clsEvento cls = new clsEvento();
            return cls.Eliminar(id);
        }
    }
}