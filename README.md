# Sistema de Gestión de Red Eléctrica Municipal

Un sistema de consola en C# desarrollado para optimizar la gestión, autocompletado, búsqueda y análisis de rutas de transmisión en una red de subestaciones eléctricas municipales mediante la integración de **4 estructuras de datos avanzadas implementadas desde cero**.

---

## Integrantes y Responsabilidades

* **[Nombre del Integrante 1]** - *Estructuras:* Tabla Hash y Trie (Árbol de Prefijos)
* **[Nombre del Integrante 2]** - *Estructuras:* Grafo (BFS, DFS, Dijkstra) y Union-Find

> **Curso:** Estructura de Datos II  
> **Universidad:** Universidad Rafael Landívar  
> **Facultad:** Ingeniería en Informática y Sistemas  

---

## Estructuras de Datos Implementadas (Sin librerías externas)

1. **Tabla Hash (`TablaHash.cs`)**: Manejo de almacenamiento O(1) de subestaciones utilizando **Encadenamiento (Chaining)** con Listas Enlazadas para resolución de colisiones.
2. **Trie / Árbol de Prefijos (`Trie.cs`)**: Sistema de sugerencias y autocompletado en tiempo real basado en arreglos ASCII de 256 posiciones.
3. **Conjuntos Disjuntos (`UnionFind.cs`)**: Validación de conectividad y aislamiento de subredes eléctricas optimizado con **Path Compression** y **Union by Rank**.
4. **Grafo (`Grafo.cs`)**: Representación mediante **Lista de Adyacencia** soportando los algoritmos de recorrido BFS (Anchura), DFS (Profundidad) y el algoritmo de Dijkstra para rutas de menor costo.

---

## Requisitos e Instalación

### Prerrequisitos
* **.NET SDK 8.0 / 10.0** o superior instalado.
* IDE recomendado: Visual Studio 2022 / VS Code.

### Pasos para ejecutar

1. Clona el repositorio:
   ```bash
   git clone [https://github.com/AngeloGarI/Red_electrica_Estructuras.git)
   cd TU-REPOSITORIO

Asegúrate de que los archivos de datos iniciales existan en la carpeta Data/:

Data/subestaciones.csv

Data/lineas.csv

Compila y ejecuta el proyecto:

Bash
dotnet run

# Menú Principal de la Aplicación
1. Cargar Datos desde CSV: Carga inicial e integración entre estructuras.

2. Registrar Subestación: Inserta dinámicamente en memoria (Tabla Hash, Trie, Union-Find y Grafo).

3. Buscar Subestación por Código: Consulta instantánea en la Tabla Hash.

4. Autocompletar Nombre de Subestación: Búsqueda recursiva por prefijos en el Trie.

5. Registrar Línea Eléctrica: Conecta vértices en el Grafo y une subredes en Union-Find.

6. Calcular Ruta Óptima (Dijkstra): Encuentra la secuencia de subestaciones y costo mínimo.

7. Recorridos de Red (BFS / DFS): Muestra el orden de exploración en anchura y profundidad.

8. Salir.

📄 Documentación Adicional
La documentación técnica completa (Diagramas de Clases, Diagramas de Flujo y Análisis de Complejidad Big-O) y el Manual de Usuario detallado se encuentran disponibles en el archivo Manual_Tecnico_y_Usuario.pdf en la raíz de este repositorio.
