using System;
using Red_electrica_Estructuras.Models;

namespace Red_electrica_Estructuras.DataStructures.HashTable
{
    // Implementación propia de Tabla Hash con resolución de colisiones por encadenamiento
    public class TablaHash
    {
        // Arreglo de cubetas que almacena las cabezas de cada lista enlazada
        private HashNode[] cubetas;
        private int capacidad;
        private int cantidad;

        public TablaHash(int capacidadInicial = 101)
        {
            capacidad = capacidadInicial;
            cubetas = new HashNode[capacidad];
            cantidad = 0;
        }

        private int ObtenerIndice(string clave)
        {
            if(string.IsNullOrEmpty(clave))
                return 0;

            int hash = 0;
            foreach (char c in clave)
            {
                hash = (hash * 31 + c) % capacidad;
            }
            return Math.Abs(hash);
        }

        public void Insertar(string clave, Subestacion valor)
        {
            int indice = ObtenerIndice(clave);
            HashNode actual = cubetas[indice];

            while (actual != null)
            {
                if (actual.Clave.Equals(clave, StringComparison.OrdinalIgnoreCase))
                {
                    actual.Valor = valor; // Actualizar valor si la clave ya existe
                    return;
                }
                actual = actual.Siguiente;
            }

            HashNode nuevoNodo = new HashNode(clave, valor);
            nuevoNodo.Siguiente = cubetas[indice];
            cubetas[indice] = nuevoNodo;
            cantidad++;
        }

        public Subestacion Buscar(string clave)
        {
            int indice = ObtenerIndice(clave);
            HashNode actual = cubetas[indice];

            while (actual != null)
            {
                if (actual.Clave.Equals(clave, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Valor;
                }
                actual = actual.Siguiente;
            }

            return null; // No encontrado
        }

        public bool Eliminar(string clave)
        {
            int indice = ObtenerIndice(clave);
            HashNode actual = cubetas[indice];
            HashNode anterior = null;

            while (actual != null)
            {
                if (actual.Clave.Equals(clave, StringComparison.OrdinalIgnoreCase))
                {
                    if (anterior == null)
                    {
                        cubetas[indice] = actual.Siguiente; // Eliminar el nodo de la cabeza
                    }
                    else
                    {
                        anterior.Siguiente = actual.Siguiente; // Saltar el nodo a eliminar
                    }
                    cantidad--;
                    return true; // Eliminación exitosa
                }
                anterior = actual;
                actual = actual.Siguiente;
            }
            return false; // No encontrado
        }
        public int ObtenerCantidad() //elementos en la tabla hash
        {
            return cantidad;
        }
    }
}