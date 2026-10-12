namespace Red_electrica_Estructuras.DataStructures.Graph
{
    // Nodo para la lista de adyacencia del Grafo (representa una línea eléctrica conectada a un destino)
    public class AdjListNode
    {
        public int DestinoId { get; set; }
        public double LongitudKm { get; set; }
        public double Costo { get; set; }
        public AdjListNode Siguiente { get; set; }

        public AdjListNode(int destinoId, double longitudKm, double costo)
        {
            DestinoId = destinoId;
            LongitudKm = longitudKm;
            Costo = costo;
            Siguiente = null;
        }
    }
}