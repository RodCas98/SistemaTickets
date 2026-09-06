namespace TicketsSoporte.logica
{
    /// <summary>
    /// Historial de eventos de un ticket (creacion, asignacion, errores, resolucion, cierre).
    /// </summary>
    public class Bitacora
    {
        public List<string> lstRegistros { get; set; }

        public Bitacora()
        {
            lstRegistros = new List<string>();
        }

        public void registrarEvento(string strEvento)
        {
            lstRegistros.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm}] {strEvento}");
        }

        public void mostrarBitacora()
        {
            Console.WriteLine("  --- Bitacora ---");
            foreach (string strRegistro in lstRegistros)
            {
                Console.WriteLine($"  {strRegistro}");
            }
        }
    }
}
