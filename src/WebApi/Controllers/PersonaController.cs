using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        //-----------------------------------------------------------------------------------------

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        //-----------------------------------------------------------------------------------------

        [HttpGet]
        public IEnumerable<Persona> getPersonasAll()
        {
            return this._personaService.GetAll();
        }

        //-----------------------------------------------------------------------------------------

        [HttpGet]
        [Route("{id}")]
        public TypedBooleanResponse<Persona> getPersonasById(int id)
        {
            var persona = this._personaService.Get(id);

            if ( persona == null ) 
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError  = true,
                    descripcion = "No existe la persona con ID ." + id + ".",
                    entidad     = null
                };
            }

            return new TypedBooleanResponse<Persona>
            {
                tieneError  = false,
                descripcion = "Persona encontrada correctamente.",
                entidad     = persona
            };
        }

        //-----------------------------------------------------------------------------------------

        [HttpPost]
        [Route("persona")]
        public IEnumerable<Persona> getPersonasSegunFiltro(PersonaFiltro p)
        {
            return _personaService.getPersonasSegunFiltro ( p.TipoDocumento, p.NroDocumento, p.PaisId, p.SexoId);
        }

        //-----------------------------------------------------------------------------------------

        [HttpPost]
        [Route("add")]
        public TypedBooleanResponse<Persona> agregarPersona ( Persona persona )
        {
            var camposRequeridos = new List<string>();
            if ( string.IsNullOrEmpty(persona.TipoDocumento) ) 
                camposRequeridos.Add("TipoDocumento");
            
            if ( string.IsNullOrEmpty(persona.NroDocumento) ) 
                camposRequeridos.Add("NroDocumento");
            
            if ( persona.PaisId == 0 )
                camposRequeridos.Add("PaisId");

            if ( persona.SexoId == 0 )
                camposRequeridos.Add("SexoId");

            if ( persona.FechaNacimiento == null ) 
                camposRequeridos.Add("FechaNacimiento");

            var json = JsonSerializer.Serialize(camposRequeridos);

            if ( camposRequeridos.Any() )
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "Error al agregar nueva persona. Campos requeridos: " + json.ToString(),
                    entidad = persona
                };
            }

            //validacion de al menos un contacto
            if ( string.IsNullOrEmpty(persona.Email) && string.IsNullOrEmpty(persona.Telefono)) 
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "La persona deben tener al menos un dato de contacto. Posibles contactos: [Email, Telefono]",
                    entidad = persona
                };
            }

            // validacion mayor a 18 años
            DateTime nacimiento = new DateTime(persona.FechaNacimiento.Year, persona.FechaNacimiento.Month, persona.FechaNacimiento.Day);
            int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            if ( edad < 18 )
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "No pueden crearse personas menores a 18 años.",
                    entidad = persona
                };
            }

            // validar existe de la persona    
            if ( _personaService.existePersona(persona) )
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "Ya existe la persona.",
                    entidad = persona
                };
            }

            return new TypedBooleanResponse<Persona>{
                tieneError = false,
                descripcion = "Persona agregada correctamente.",
                entidad = _personaService.Create(persona)
            }; 
        }

        //-----------------------------------------------------------------------------------------

        [HttpDelete]
        [Route("delete/{id}")]
        public TypedBooleanResponse<Persona> DeletePersona(int id)
        {
            var persona = this._personaService.Get(id);

            if ( persona == null ) 
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError  = true,
                    descripcion = "No existe la persona con ID " + id + " para ser eliminada.",
                    entidad     = null
                };
            }

            return new TypedBooleanResponse<Persona>
            {
                tieneError  = false,
                descripcion = "Persona eliminada correctamente.",
                entidad     = _personaService.Delete(persona)
            };
        }

        //-----------------------------------------------------------------------------------------

        [HttpPut]
        [Route("update/{id}")]
        public TypedBooleanResponse<Persona> UpdatePersona ( int id, Persona persona )
        {
            //return _personaService.Update(persona);
            if ( id != persona.Id )
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "Error al actualizar la persona.",
                    entidad = persona
                };         
            } 
            else{
                var personaById = this._personaService.Get(id);
                if ( personaById == null ) 
                {
                    return new TypedBooleanResponse<Persona>{
                        tieneError  = true,
                        descripcion = "No existe la persona con ID " + id + " para ser actualizada.",
                        entidad     = null
                    };
                }
            }

            if ( !_personaService.existePersona(persona) )
            {
                return new TypedBooleanResponse<Persona>{
                    tieneError = true,
                    descripcion = "No existe la persona.",
                    entidad = persona
                };
            }

            return new TypedBooleanResponse<Persona>{
                tieneError = false,
                descripcion = "Persona actualizada correctamente.",
                entidad = _personaService.Update(persona)
            };
        }
                //-----------------------------------------------------------------------------------------

        [HttpGet]
        [Route("estadisticas")]
        public PersonaEstadisticas estadisticas()
        {
            return _personaService.getEstadisticas ();
        }

    }
}