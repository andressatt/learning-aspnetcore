using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Lab02.App
{
    public static class WebApp
    {
        private static string comentarioArquivo
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/comentarios.txt");
            }
        }

        public static void ComentarioIncluir( string nome, string comentario)
        {
            using (var writer = new StreamWriter(comentarioArquivo, true, Encoding.UTF8))
            {
                writer.WriteLine($"{DateTime.Now:dd/MM/yyyy} - {DateTime.Now:HH:mm:ss}");
                writer.WriteLine($"{nome}: {comentario}\r\n");
            }
        }

        public static string ComentariosObter()
        {
            string texto = string.Empty;
            if (!File.Exists(comentarioArquivo))
            {
                return texto;
            }
            using (var reader = new StreamReader(comentarioArquivo))
            {
                texto = reader.ReadToEnd();
            }
            return texto;
        }
    }
}