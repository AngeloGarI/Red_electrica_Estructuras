using Red_electrica_Estructuras.Models;

namespace Red_electrica_Estructuras.DataStructures.HashTable
{
    public class HashNode
    {
        public string Clave { get; set; }
        public Subestacion Valor { get; set; }
        public HashNode Siguiente { get; set; }

        public HashNode(string clave, Subestacion valor)
        {
            Clave = clave;
            Valor = valor;
            Siguiente = null;
        }
    }
}