using System;
using System.Collections.Generic;
namespace Red_electrica_Estructuras.DataStructures.Graph
{
    public class Grafo
    {
        private AdjListNode[] ListaAdyacencia;
        private int capacidadVertices;

        public Grafo (int numeroVertices)
        {
            capacidadVertices = numeroVertices;
            listaAdyacencia = new AdjListNode[capacidadVertices];
        }

        public void AsegurarCapacidad(int nuevaCapacidad)
        {
            if (nuevaCapacidad <= capacidadVertices) return;

            AdjListNode[] nuevaLista = new AdjListNode[nuevaCapacidad];
            for (int i = 0; i < capacidadVertices; i++)
            {
                nuevaLista[i] = listaAdyacencia[i];
            }
            listaAdyacencia = nuevaLista;
            capacidadVertices = nuevaCapacidad;
        }
        
        // Agregar una arista (Línea eléctrica)
        public void AgregarArista(int origenId, int destinoId, double longitud, double costo, bool esUnidireccional = false)
        {
            // Validamos que los IDs estén dentro de los límites del arreglo
            if (origenId < 0 || origenId >= capacidadVertices || destinoId < 0 || destinoId >= capacidadVertices)
                return;

            // Insertamos al inicio de la lista enlazada del nodo origen
            AdjListNode nuevoNodo = new AdjListNode(destinoId, longitud, costo);
            nuevoNodo.Siguiente = listaAdyacencia[origenId];
            listaAdyacencia[origenId] = nuevoNodo;

            // Si es bidireccional, agregamos también la arista inversa
            if (!esUnidireccional)
            {
                AdjListNode nodoInverso = new AdjListNode(origenId, longitud, costo);
                nodoInverso.Siguiente = listaAdyacencia[destinoId];
                listaAdyacencia[destinoId] = nodoInverso;
            }
        }

        // 1. Recorrido BFS (Anchura) - Utiliza Queue<T> nativa como estructura de control permitida
        public void BFS(int inicioId, Action<int> procesarNodo)
        {
            bool[] visitados = new bool[capacidadVertices];
            Queue<int> cola = new Queue<int>();

            visitados[inicioId] = true;
            cola.Enqueue(inicioId);

            while (cola.Count > 0)
            {
                int actualId = cola.Dequeue();
                procesarNodo(actualId);

                AdjListNode temp = listaAdyacencia[actualId];
                while (temp != null)
                {
                    if (!visitados[temp.DestinoId])
                    {
                        visitados[temp.DestinoId] = true;
                        cola.Enqueue(temp.DestinoId);
                    }
                    temp = temp.Siguiente;
                }
            }
        }

        // 2. Recorrido DFS (Profundidad) - Implementación recursiva
        public void DFS(int inicioId, Action<int> procesarNodo)
        {
            bool[] visitados = new bool[capacidadVertices];
            DFSRecursivo(inicioId, visitados, procesarNodo);
        }

        private void DFSRecursivo(int actualId, bool[] visitados, Action<int> procesarNodo)
        {
            visitados[actualId] = true;
            procesarNodo(actualId);

            AdjListNode temp = listaAdyacencia[actualId];
            while (temp != null)
            {
                if (!visitados[temp.DestinoId])
                {
                    DFSRecursivo(temp.DestinoId, visitados, procesarNodo);
                }
                temp = temp.Siguiente;
            }
        }

        // 3. Algoritmo de Dijkstra para encontrar la ruta de menor costo/distancia
        public double[] Dijkstra(int inicioId, out int[] predecesores)
        {
            double[] distancias = new double[capacidadVertices];
            predecesores = new int[capacidadVertices];
            bool[] visitados = new bool[capacidadVertices];

            for (int i = 0; i < capacidadVertices; i++)
            {
                distancias[i] = double.MaxValue;
                predecesores[i] = -1;
                visitados[i] = false;
            }

            distancias[inicioId] = 0;

            for (int count = 0; count < capacidadVertices - 1; count++)
            {
                // Encontrar el vértice con la distancia mínima no visitado
                int u = -1;
                double minDist = double.MaxValue;

                for (int v = 0; v < capacidadVertices; v++)
                {
                    if (!visitados[v] && distancias[v] < minDist)
                    {
                        minDist = distancias[v];
                        u = v;
                    }
                }

                if (u == -1 || distancias[u] == double.MaxValue)
                    break; // Ya no hay vértices accesibles

                visitados[u] = true;

                // Actualizar las distancias de los vecinos adyacentes
                AdjListNode temp = listaAdyacencia[u];
                while (temp != null)
                {
                    int v = temp.DestinoId;
                    double peso = temp.Costo; // O puedes usar temp.LongitudKm según prefieras evaluar costo o distancia

                    if (!visitados[v] && distancias[u] + peso < distancias[v])
                    {
                        distancias[v] = distancias[u] + peso;
                        predecesores[v] = u;
                    }
                    temp = temp.Siguiente;
                }
            }

            return distancias;
        }
    }
}
