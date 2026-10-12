namespace Red_electrica_Estructuras.DataStructures.Trie
{
    public class Trie
    {
        private TrieNode raiz;
        public Trie()
        {
            raiz = new TrieNode();
        }
        public void Insertar(string palabra)
        {
            if (string.IsNullOrEmpty(palabra))
                return;

            TrieNode actual = raiz;
            string palabraMin = palabra.ToLower();

            foreach (char c in palabraMin)
            {
                int indice = (int)c;
                if (indice < 0 || indice >= 256) continue; // Ignorar caracteres fuera del rango

                if (actual.Hijos[indice] == null)
                {
                    actual.Hijos[indice] = new TrieNode();
                }
                actual = actual.Hijos[indice];
            }
            actual.EsFinDePalabra = true;
            actual.PalabraCompleta = palabra; // Guardar la palabra completa en el nodo final
        }
        public TrieNode BuscarNodoPrefijo(string prefijo)
        {
            TrieNode actual = raiz;
            string prefijoMin = prefijo.ToLower();

            foreach (char c in prefijoMin)
            {
                int indice = (int)c;
                if (indice < 0 || indice >= 256 || actual.Hijos[indice] == null) // Ignorar caracteres fuera del rango
                {
                    return null;
                }
                actual = actual.Hijos[indice];
            }
            return actual;
        }

        private void RecolectarPalabras(TrieNode nodo, string[] resultados, ref int contador, int maxResultados)
        {
            if (nodo == null || contador >= maxResultados)
                return;

            if (nodo.EsFinDePalabra)
            {
                resultados[contador] = nodo.PalabraCompleta;
                contador++;
            }

            for (int i = 0; i < 256; i++)
            {
                if (nodo.Hijos[i] != null  && contador < maxResultados)
                {
                    RecolectarPalabras(nodo.Hijos[i], resultados, ref contador, maxResultados);
                }
            }
        }

        public string[] BuscarPorPrefijo(string prefijo, int maxResultados = 10)
        {
            string[] resultados = new string[maxResultados];
            int contador = 0;

            TrieNode nodoPrefijo = BuscarNodoPrefijo(prefijo);
            if (nodoPrefijo == null)
            {
                return new string[0]; // No se encontraron palabras con el prefijo
            }
            RecolectarPalabras(nodoPrefijo, resultados, ref contador, maxResultados);
            
            string[] sugerenciasFinales = new string[contador];
            for (int i = 0; i < contador; i++)
            {
                sugerenciasFinales[i] = resultados[i];
            }
            return sugerenciasFinales;
        }
    }
}