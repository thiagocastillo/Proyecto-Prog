using System.IO;
using System.Collections.Generic;

namespace Library.Services
{
    public static class GestorPartidas
    {
        private static string CarpetaPartidas = "Partidas";

        public static void GuardarPartida(string nombreArchivo, Library.Domain.Partida partida)
        {
            if (!Directory.Exists(CarpetaPartidas))
            {
                Directory.CreateDirectory(CarpetaPartidas);
            }

            string ruta = Path.Combine(CarpetaPartidas, nombreArchivo + ".json");
            string json = partida.ConvertToJson();
            File.WriteAllText(ruta, json);
        }

        public static Library.Domain.Partida CargarPartida(string nombreArchivo)
        {
            string ruta = Path.Combine(CarpetaPartidas, nombreArchivo + ".json");

            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException("No se encontró el archivo de partida.");
            }

            string json = File.ReadAllText(ruta);
            Library.Domain.Partida partida = new Library.Domain.Partida();
            partida.LoadFromJson(json);
            return partida;
        }

        public static List<string> ListarPartidasGuardadas()
        {
            List<string> partidas = new List<string>();

            if (!Directory.Exists(CarpetaPartidas))
            {
                return partidas;
            }

            string[] archivos = Directory.GetFiles(CarpetaPartidas, "*.json");
            foreach (string archivo in archivos)
            {
                string nombre = Path.GetFileNameWithoutExtension(archivo);
                partidas.Add(nombre);
            }

            return partidas;
        }
    }
}