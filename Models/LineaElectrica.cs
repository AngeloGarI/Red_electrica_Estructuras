namespace Red_electrica_Estructuras.Models
{
    public class LineaElectrica
    {
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public double LongitudKm { get; set; }
        public double Costo { get; set; }
        public bool EsUniDireccional { get; set; }

        public LineaElectrica(int origenId, int destinoId, double longitudKm, double costo, bool esUniDireccional)
        {
            OrigenId = origenId;
            DestinoId = destinoId;
            LongitudKm = longitudKm;
            Costo = costo;
            EsUniDireccional = esUniDireccional;
        }
        public override string ToString()
        {
            string sentido = EsUniDireccional ? "Unidireccional (-->)" : "Bidireccional (<-->)";
            return $"Línea: Subestación {OrigenId} {sentido} Subestación {DestinoId} | Distancia: {LongitudKm} km | Costo: ${Costo}";
        }
    }
}