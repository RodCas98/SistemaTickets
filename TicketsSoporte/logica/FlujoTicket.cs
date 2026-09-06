namespace TicketsSoporte.logica
{
    /// <summary>
    /// Define y valida el flujo de estados permitido de un ticket:
    /// Abierto -> Asignado -> (Escalado) -> Resuelto -> Cerrado.
    /// </summary>
    public class FlujoTicket
    {
        private readonly Dictionary<string, List<string>> dicTransiciones;

        public FlujoTicket()
        {
            dicTransiciones = new Dictionary<string, List<string>>
            {
                { "Abierto", new List<string> { "Asignado" } },
                { "Asignado", new List<string> { "Escalado", "Resuelto" } },
                { "Escalado", new List<string> { "Asignado", "Resuelto" } },
                { "Resuelto", new List<string> { "Cerrado", "Asignado" } },
                { "Cerrado", new List<string>() }
            };
        }

        public bool puedeCambiarEstado(string strEstadoActual, string strEstadoNuevo)
        {
            return dicTransiciones.ContainsKey(strEstadoActual) && dicTransiciones[strEstadoActual].Contains(strEstadoNuevo);
        }

        public void mostrarFlujo()
        {
            Console.WriteLine("\n=== FLUJO DE ESTADOS DEL TICKET ===");
            Console.WriteLine(" Abierto -> Asignado -> Escalado -> Resuelto -> Cerrado");
            Console.WriteLine("              \\<--------/  (reasignacion tras escalar)");
            Console.WriteLine("                           \\-> Asignado (reapertura)");
            foreach (KeyValuePair<string, List<string>> objPar in dicTransiciones)
            {
                string strDestinos = objPar.Value.Count > 0 ? string.Join(", ", objPar.Value) : "(estado final)";
                Console.WriteLine($" {objPar.Key} => {strDestinos}");
            }
        }
    }
}
