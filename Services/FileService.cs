using System;
using System.IO;
using Red_electrica_Estructuras.Models;
using Red_electrica_Estructuras.DataStructures.HashTable;
using Red_electrica_Estructuras.DataStructures.Trie;
using Red_electrica_Estructuras.DataStructures.UnionFind;
using Red_electrica_Estructuras.DataStructures.Graph;

namespace Red_electrica_Estructuras.Services
{
    public class FileService
    {
        // Método para cargar subestaciones desde un archivo CSV
        // Formato esperado por línea: Id,Codigo,Nombre,CapacidadMW,Ubicacion
        public static void CargarSubestaciones(string rutaArchivo, TablaHash tablaHash, Trie trie, UnionFind unionFind, ref int maxId)
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"[Aviso] No se encontró el archivo en: {rutaArchivo}. Se omitirá la carga.");
                return;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea) || linea.StartsWith("#")) continue;

                string[] partes = linea.Split(',');
                if (partes.Length >= 5)
                {
                    if (int.TryParse(partes[0].Trim(), out int id) &&
                        double.TryParse(partes[3].Trim(), out double capacidad))
                    {
                        string codigo = partes[1].Trim();
                        string nombre = partes[2].Trim();
                        string ubicacion = partes[4].Trim();

                        Subestacion sub = new Subestacion(id, codigo, nombre, capacidad, ubicacion);
                        
                        // Insertamos en Tabla Hash y Trie
                        tablaHash.Insertar(codigo, sub);
                        trie.Insertar(nombre);

                        if (id > maxId) maxId = id;
                    }
                }
            }

            unionFind.AsegurarCapacidad(maxId + 50);
            Console.WriteLine("[Éxito] Subestaciones cargadas correctamente.");
        }

        // Método para cargar líneas eléctricas desde un archivo CSV
        // Formato esperado por línea: OrigenId,DestinoId,LongitudKm,Costo,EsUnidireccional(true/false)
        public static void CargarLineas(string rutaArchivo, Grafo grafo, UnionFind unionFind)
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"[Aviso] No se encontró el archivo de líneas en: {rutaArchivo}.");
                return;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea) || linea.StartsWith("#")) continue;

                string[] partes = linea.Split(',');
                if (partes.Length >= 4)
                {
                    if (int.TryParse(partes[0].Trim(), out int origen) &&
                        int.TryParse(partes[1].Trim(), out int destino) &&
                        double.TryParse(partes[2].Trim(), out double longitud) &&
                        double.TryParse(partes[3].Trim(), out double costo))
                    {
                        bool unidireccional = false;
                        if (partes.Length >= 5)
                        {
                            bool.TryParse(partes[4].Trim(), out unidireccional);
                        }

                        grafo.AgregarArista(origen, destino, longitud, costo, unidireccional);
                        unionFind.Union(origen, destino);
                    }
                }
            }
            Console.WriteLine("[Éxito] Líneas eléctricas cargadas y conectadas en el Grafo y Union-Find.");
        }
    }
}