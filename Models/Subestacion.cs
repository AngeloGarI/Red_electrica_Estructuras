namespace Red_electrica_Estructuras.Models
{
    public class Subestacion
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double CapacidadMW { get; set; }
        public string Ubicacion { get; set; }

        public Subestacion(){}

        public Subestacion (int id, string codigo, string nombre, double capacidadMW, string ubicacion)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            CapacidadMW = capacidadMW;
            Ubicacion = ubicacion;
        }

        public override string ToString()
        {
         return $"[ID: {Id}] Código: {Codigo} | Nombre: {Nombre} | Capacidad: {CapacidadMW} MW | Ubicación: {Ubicacion}";
        }

    }
}