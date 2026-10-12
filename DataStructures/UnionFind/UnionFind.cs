namespace Red_electrica_Estructuras.DataStructures.UnionFind
{
    public class UnionFind
    {
        private int[] padre;
        private int[] rango;
        private int capacidad;

        public UnionFind(int capacidadInicial)
        {
            capacidad = capacidadInicial;
            padre = new int[capacidad];
            rango = new int[capacidad];

            for (int i = 0; i < capacidad; i++)
            {
                padre[i] = i;
                rango[i] = 0;
            }
        }

        //redimensiona los arreglos si se agregan mas subestaciones de las previstas
        public void AsegurarCapacidad(int nuevaCapacidad)
        {
            if (nuevaCapacidad <= capacidad)
                return;

            int[] nuevoPadre = new int[nuevaCapacidad];
            int[] nuevoRango = new int[nuevaCapacidad];

            for (int i = 0; i < capacidad; i++)
            {
                nuevoPadre[i] = padre[i];
                nuevoRango[i] = rango[i];
            }

            for (int i = capacidad; i < nuevaCapacidad; i++)
            {
                nuevoPadre[i] = i;
                nuevoRango[i] = 0;
            }

            padre = nuevoPadre;
            rango = nuevoRango;
            capacidad = nuevaCapacidad;
        }

        public int Find(int i)
        {
            if (padre[i] == i)
            {
                return i;
            }
            padre[i]= Find(padre[i]);
            return padre[i];
        }

        //
        public bool Union(int i, int j)
        {
            int raizI = Find(i);
            int raizJ = Find(j);

            if (raizI != raizJ)
            {
                // adjunta el árbol de menor rango al árbol de mayor rango
                if (rango[raizI] < rango[raizJ])
                {
                    padre[raizI] = raizJ;
                }
                else if (rango[raizI] > rango[raizJ])
                {
                    padre[raizJ] = raizI;
                }
                else
                {
                    padre[raizJ] = raizI;
                    rango[raizI]++;
                }
                return true; // Se realizó la unión
            }
            return false; // No se realizó la unión porque ya están en el mismo conjunto
        }

        // Método para verificar si dos elementos están conectados directa o indirectamente
        public bool EstanConectados(int i, int j)
        {
            return Find(i) == Find(j);
        }
    }
}