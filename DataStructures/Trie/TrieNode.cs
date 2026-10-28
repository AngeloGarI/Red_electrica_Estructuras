namespace Red_electrica_Estructuras.DataStructures.Trie
{
    public class TrieNode
    {
        public TrieNode[] Hijos { get; set; }
        public bool EsFinDePalabra { get; set; }
        public string PalabraCompleta { get; set; }

        public TrieNode()
        {
            Hijos = new TrieNode[256]; // Asumiendo solo letras minúsculas a-z
            EsFinDePalabra = false;
            PalabraCompleta = null;
        }
    }
}