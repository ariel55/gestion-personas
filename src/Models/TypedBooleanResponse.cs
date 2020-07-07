
namespace Models
{
    public class TypedBooleanResponse<T>
    {
        public bool tieneError { get; set; } 
        public string descripcion { get; set; }
        public T entidad { get; set; }

        public TypedBooleanResponse() { }

        public TypedBooleanResponse(T entitie_) 
        {
            this.tieneError = false;
            this.descripcion = "";
            this.entidad = entitie_;
         }
    }
}