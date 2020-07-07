using System;
using System.Collections.Generic;
using Models;
using DataAccess;
using Services.Contracts;
using System.Linq;

namespace Services.Implementation
{
    public class PersonaService : IPersonaService
    {
        private readonly IRepository<Persona> _repository;

        //-----------------------------------------------------------------------------------------

        public PersonaService(IRepository<Persona> repository)
        {
            this._repository = repository;
        }

        //-----------------------------------------------------------------------------------------

        public Persona Get(int id)
        {
            return this._repository.Find( x => x.Id.Equals(id)).FirstOrDefault();
        }

        //-----------------------------------------------------------------------------------------

        public List<Persona> GetAll()
        {
            return this._repository.GetAll().ToList();
        }

        //-----------------------------------------------------------------------------------------

        public IEnumerable<Persona> getPersonasSegunFiltro ( string tipoDoc, string nroDoc, 
                                                                int paisId, int sexoId )
        {
            return this._repository.Find ( p =>  
                            (tipoDoc == null ? true : ( tipoDoc != null && p.TipoDocumento.Equals(tipoDoc)))
                            && (nroDoc == null ? true : (nroDoc != null && p.NroDocumento.Equals(nroDoc)))
                            && (paisId == 0 ? true : (paisId != 0 && p.PaisId == paisId))
                            && (sexoId == 0 ? true : (sexoId != 0 && p.SexoId == sexoId))
            );
        }

        //-----------------------------------------------------------------------------------------

        public bool existePersona ( Persona p )
        {
            var personas = getPersonasSegunFiltro ( p.TipoDocumento, p.NroDocumento, p.PaisId, p.SexoId );
            return personas.Any();
        }

        //-----------------------------------------------------------------------------------------

        public Persona Create ( Persona nuevaPersona )
        {
            return this._repository.Add ( nuevaPersona );
        }

        //-----------------------------------------------------------------------------------------

        public Persona Delete(Persona persona)
        {
            return this._repository.Delete(persona);
        }

        //-----------------------------------------------------------------------------------------

        public Persona Update(Persona persona)
        {
            return this._repository.Update(persona);        
        }

        //-----------------------------------------------------------------------------------------

        public Persona GetFakePersona()
        {
            return new Persona 
                {   Nombre="invalid",Apellido="invalid",TipoDocumento="invalid",
                    NroDocumento="invalid", FechaNacimiento = DateTime.Now.Date,
                    Email="invalid", Telefono="invalid"
                };
        }

        //-----------------------------------------------------------------------------------------

        public PersonaEstadisticas getEstadisticas()
        {
            var estadisticas = new PersonaEstadisticas();
    
            estadisticas.cantidad_hombres = this._repository.Find( x => x.SexoId.Equals(1)).ToList().Count();
            estadisticas.cantidad_mujeres = this._repository.Find( x => x.SexoId.Equals(2)).ToList().Count();

            var num1 = this._repository.Find( x => x.PaisId.Equals(1)).ToList().Count * 100 ;
            var num2 = this._repository.GetAll().ToList().Count;
            estadisticas.porcentaje_argentinos = num1 / num2;

            return estadisticas;
        }
    }
}