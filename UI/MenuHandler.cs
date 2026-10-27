using System;
using RedElectricaApp.Services;
using RedElectricaApp.Models;

namespace RedElectricaApp.UI
{
    public class MenuHandler
    {
        private RedElectricaService servicio;

        public MenuHandler()
        {
            servicio = new RedElectricaService(50);
        }

        public void MostrarMenu()
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("   Sistema De Gestion de Red Electrica Municipal   ");
                Console.WriteLine("1. Cargar datos desde archivos CSV");
                Console.WriteLine("2. Registrar nueva Subestación (hash + trie) ");
                Console.WriteLine("3. Buscar Subestación por código (hash)");
                Console.WriteLine("4. Autocompletar nombre por Prefijo (trie)");
                Console.WriteLine("5. Registrar nueva Línea Eléctrica (grafo + union-find)");
                Console.WriteLine("6. Calcular ruta optima de suministro (dijkstra)");
                Console.WriteLine("7. Ejecutar Recorridos de Red (BFS y DFS)");
                Console.WriteLine("8. Salir");
                Console.Write("\nSeleccione una opción (1-8): ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.Clear();
                    switch (opcion)
                    {
                        case 1:
                            CargarArchivosMenu();
                            break;
                        case 2:
                            RegistrarSubestacionMenu();
                            break;
                        case 3:
                            BuscarSubestacionMenu();
                            break;
                        case 4:
                            AutocompletarMenu();
                            break;
                        case 5:
                            RegistrarLineaMenu();
                            break;
                        case 6:
                            CalcularRutaMenu();
                            break;
                        case 7:
                            EjecutarRecorridosMenu();
                            break;
                        case 8:
                            Console.WriteLine("Saliendo del sistema...");
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Intente nuevamente.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ingresa un numero valido.");
                }
                
                if (opcion != 8)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
                
            } while (opcion != 8);
        }

        private void CargarArchivosMenu()
        {
           Console.WriteLine("   Cargar archivos    ");
           Console.WriteLine("Ruta subestacione (ejemplo. Data/subestaciones.csv): ");
           string rutaSubs = Console.ReadLine();
           Console.Write("Ruta Lineas ejemplo. Data/lineas.csv: ");
           string rutaLineas = Console.ReadLine();
           servicio.CargarDatosDesdeArchivos(rutaSubs, rutaLineas);
        }

        private void RegistrarSubestacionMenu()
        {
            Console.WriteLine("   Registrar Subestación    ");
            Console.Write("ID numérico: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Código Unico ejemplo. SUB-001: ");
                string codigo = Console.ReadLine();
                Console.Write("Nombre descriptivo: ");
                string nombre = Console.ReadLine();
                Console.Write("Capacidad (MW): ");
                double.TryParse(Console.ReadLine(), out double cap);
                Console.Write("Ubicación: ");
                string ub = Console.ReadLine();

                servicio.RegistrarSubestacion(id, codigo, nombre, cap, ub);
            }
        }

        private void BuscarSubestacionMenu()
        {
            Console.WriteLine("   Busqueda HASH   ");
            Console.Write("Ingrese el código: ");
            string codigo = Console.ReadLine();
            Subestacion sub = servicio.BuscarPorCodigo(codigo);
            if (sub != null) Console.WriteLine("\n" + sub.ToString());
            else Console.WriteLine("\nNo encontrada.");
        }

        private void AutocompletarMenu()
        {
            Console.WriteLine("   Autocompletar TRIE   ");
            Console.Write("Escriba el prefijo: ");
            string prefijo = Console.ReadLine();
            string[] sugerencias = servicio.SugerirNombres(prefijo);
            Console.WriteLine("\n Sugerencias: ");
            foreach (string s in sugerencias) if (!string.IsNullOrEmpty(s)) Console.WriteLine($"-{s}");
        }

        private void RegistrarLineaMenu()
        {
            Console.WriteLine("   REGISTRAR LÍNEA   ");
            Console.Write("ID Origen: "); int.TryParse(Console.ReadLine(), out int org);
            Console.Write("ID Destino: "); int.TryParse(Console.ReadLine(), out int dst);
            Console.Write("Longitud (km): "); double.TryParse(Console.ReadLine(), out double lon);
            Console.Write("Costo: "); double.TryParse(Console.ReadLine(), out double cos);
            Console.Write("¿Unidireccional? (s/n): "); bool uni = Console.ReadLine().Trim().ToLower() == "s";
            servicio.RegistrarLineaElectrica(org, dst, lon, cos, uni);
        }

        private void CalcularRutaMenu()
        {
            Console.WriteLine("=== RUTA DIJKSTRA ===");
            Console.Write("ID Origen: "); int.TryParse(Console.ReadLine(), out int org);
            Console.Write("ID Destino: "); int.TryParse(Console.ReadLine(), out int dst);
            servicio.CalcularRutaOptima(org, dst);
        }

        private void EjecutarRecorridosMenu()
        {
            Console.WriteLine("=== RECORRIDOS BFS / DFS ===");
            Console.Write("ID Inicio: "); int.TryParse(Console.ReadLine(), out int ini);
            servicio.EjecutarRecorridos(ini);
        }
    }
}