using System;
using Red_electrica_Estructuras.Models;
using Red_electrica_Estructuras.DataStructures.HashTable;
using Red_electrica_Estructuras.DataStructures.Trie;
using Red_electrica_Estructuras.DataStructures.UnionFind;
using Red_electrica_Estructuras.DataStructures.Graph;

namespace Red_electrica_Estructuras.Services
{
    public class RedElectricaService
    {
        private TablaHash tablaHash;
        private Trie trie;
        private UnionFind unionFind;
        private Grafo grafo;
        private int maxIdRegistrado;

        public RedElectricaService(int capacidadInicial = 100)
        {
            tablaHash = new TablaHash(101);
            trie = new Trie();
            unionFind = new UnionFind(capacidadInicial);
            grafo = new Grafo(capacidadInicial);
            maxIdRegistrado = 0;
        }

        public void RegistrarSubestacion(int id, string codigo, string nombre, double capacidad, string ubicacion)
        {
            Subestacion sub = new Subestacion(id, codigo, nombre, capacidad, ubicacion);
            tablaHash.Insertar(codigo, sub);
            trie.Insertar(nombre);

            if (id > maxIdRegistrado)
            {
                maxIdRegistrado = id;
                unionFind.AsegurarCapacidad(maxIdRegistrado + 50);
                grafo.AsegurarCapacidad(maxIdRegistrado + 50);
            }

            Console.WriteLine($"\n[Registro Exitoso] Subestación '{nombre}' guardada con código {codigo}.");
        }

        public Subestacion BuscarPorCodigo(string codigo)
        {
            return tablaHash.Buscar(codigo); // Búsqueda O(1) en Tabla Hash
        }

        public string[] SugerirNombres(string prefijo)
        {
            return trie.BuscarPorPrefijo(prefijo, 5); // Autocompletado con Trie
        }

        public void RegistrarLineaElectrica(int origenId, int destinoId, double longitud, double costo, bool unidireccional)
        {
            // Validación con Union-Find antes de conectar en el Grafo
            bool yaConectados = unionFind.EstanConectados(origenId, destinoId);

            if (yaConectados)
            {
                Console.WriteLine($"\n[Aviso Union-Find] Las subestaciones {origenId} y {destinoId} ya pertenecen a la misma zona eléctrica.");
            }
            else
            {
                Console.WriteLine($"\n[Aviso Union-Find] Zonas distintas detectadas. Uniendo subredes...");
                unionFind.Union(origenId, destinoId);
            }

            grafo.AgregarArista(origenId, destinoId, longitud, costo, unidireccional);
            Console.WriteLine($"[Línea Registrada] Enlace creado exitosamente.");
        }

        public void CalcularRutaOptima(int origenId, int destinoId)
        {
            double[] distancias = grafo.Dijkstra(origenId, out int[] predecesores); // Algoritmo Dijkstra

            if (distancias[destinoId] == double.MaxValue)
            {
                Console.WriteLine($"\n[Ruta] No existe un camino eléctrico disponible entre {origenId} y {destinoId}.");
                return;
            }

            Console.WriteLine($"\n RUTA ÓPTIMA DE SUMINISTRO (Costo Total: {distancias[destinoId]}) ");
            
            int[] caminoInvertido = new int[maxIdRegistrado + 50];
            int contador = 0;
            int actual = destinoId;

            while (actual != -1)
            {
                caminoInvertido[contador++] = actual;
                actual = predecesores[actual];
            }

            Console.Write("Secuencia de Subestaciones: ");
            for (int i = contador - 1; i >= 0; i--)
            {
                Console.Write(caminoInvertido[i] + (i > 0 ? " -> " : ""));
            }
            Console.WriteLine();
        }

        public void EjecutarRecorridos(int inicioId)
        {
            Console.WriteLine($"\n   Recorrido BFS (Anchura)    ");
            grafo.BFS(inicioId, id => Console.Write($"[{id}] "));
            Console.WriteLine();

            Console.WriteLine($"\n    Recorrido DFS (Profundidad)    ");
            grafo.DFS(inicioId, id => Console.Write($"[{id}] "));
            Console.WriteLine();
        }

        public void CargarDatosDesdeArchivos(string rutaSubestaciones, string rutaLineas)
        {
            FileService.CargarSubestaciones(rutaSubestaciones, tablaHash, trie, unionFind, ref maxIdRegistrado);
            grafo.AsegurarCapacidad(maxIdRegistrado + 50);
            FileService.CargarLineas(rutaLineas, grafo, unionFind);
        }
    }
}