namespace Red_electrica_Estructuras.DataStructures.Trie
{
    public class TrieNode
    {
        public TriNode[] Hijos { get; set; }
        public bool EsFinDePalabra { get; set; }
        public string PalabraCompleta { get; set; }

        public TrieNode()
        {
            Hijos = new TriNode[256]; // Asumiendo solo letras minúsculas a-z
            EsFinDePalabra = false;
            PalabraCompleta = null;
        }
    }
}