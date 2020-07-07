namespace Services.Contracts
{
    using Models;
    using System.Collections.Generic;
    public interface IPersonaService
    {
        Persona Get(int id);
        IEnumerable<Persona> getPersonasSegunFiltro ( string tipoDoc, string nroDoc, 
                                                        int paisId, int sexoId );
        bool existePersona ( Persona persona );
        List<Persona> GetAll();
        Persona Create(Persona nuevaPersona);
        Persona Update(Persona persona);
        Persona Delete(Persona persona);
        Persona GetFakePersona();

        PersonaEstadisticas getEstadisticas();

    }
}