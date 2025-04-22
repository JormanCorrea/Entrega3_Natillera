using Examen_3_ServWeb_Natillera.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Examen_3_ServWeb_Natillera.Clases
{
    public class clsEvento
    {
        private DBNatilleraEntities db = new DBNatilleraEntities();
        public Evento evento { get; set; }

        public string Insertar()
        {
            try
            {
                db.Eventos.Add(evento);
                db.SaveChanges();
                return "Evento insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el evento: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                Evento existente = ConsultarPorId(evento.idEventos);
                if (existente == null)
                {
                    return "El evento no existe";
                }
                db.Eventos.AddOrUpdate(evento);
                db.SaveChanges();
                return "Evento actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el evento: " + ex.Message;
            }
        }

        public Evento ConsultarPorId(int id)
        {
            return db.Eventos.FirstOrDefault(e => e.idEventos == id);
        }

        public IQueryable ConsultarPorFiltro(string tipo, string nombre, DateTime? fecha)
        {
            var query = from E in db.Eventos
                        where
                            (tipo == "" || E.TipoEvento.Contains(tipo)) &&
                            (nombre == "" || E.NombreEvento.Contains(nombre)) &&
                            (!fecha.HasValue || E.FechaEvento == fecha.Value)
                        orderby E.FechaEvento
                        select new
                        {
                            E.idEventos,
                            E.TipoEvento,
                            E.NombreEvento,
                            E.TotalIngreso,
                            E.FechaEvento,
                            E.Sede,
                            E.ActiviadesPlaneadas
                        };

            return query;
        }


        public string Eliminar(int id)
        {
            try
            {
                Evento existente = ConsultarPorId(id);
                if (existente == null)
                    return "El evento no existe";

                db.Eventos.Remove(existente);
                db.SaveChanges();
                return "Evento eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el evento: " + ex.Message;
            }
        }
    }
}