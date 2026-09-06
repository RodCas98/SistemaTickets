namespace TicketsSoporte.logica
{
    /// <summary>
    /// Administra tecnicos, solicitantes y tickets; asigna automaticamente el tecnico con menor carga
    /// que coincida con la categoria del ticket (o el de categoria General si no hay coincidencia).
    /// </summary>
    public class GestorTickets
    {
        public List<Tecnico> lstTecnicos { get; set; }
        public List<Solicitante> lstSolicitantes { get; set; }
        public List<Ticket> lstTickets { get; set; }

        private int intSiguienteNumero;

        public GestorTickets()
        {
            lstTecnicos = new List<Tecnico>();
            lstSolicitantes = new List<Solicitante>();
            lstTickets = new List<Ticket>();
            intSiguienteNumero = 1;
        }

        public Ticket crearTicket(string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            Ticket objTicket = new Ticket(intSiguienteNumero++, strTitulo, strDescripcion, strCategoria, strPrioridad, objSolicitante);

            Tecnico objTecnico = asignarTecnicoAutomatico(strCategoria);
            if (objTecnico != null)
            {
                objTicket.asignar(objTecnico);
            }

            lstTickets.Add(objTicket);
            return objTicket;
        }

        private Tecnico asignarTecnicoAutomatico(string strCategoria)
        {
            Tecnico objTecnico = lstTecnicos
                .Where(t => t.strEspecialidad.Equals(strCategoria, StringComparison.OrdinalIgnoreCase))
                .OrderBy(t => t.intTicketsAsignados)
                .FirstOrDefault();

            if (objTecnico == null)
            {
                objTecnico = lstTecnicos
                    .Where(t => t.strEspecialidad.Equals("General", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(t => t.intTicketsAsignados)
                    .FirstOrDefault();
            }

            return objTecnico;
        }

        public Ticket buscarTicket(int intNumero)
        {
            Ticket objTicket = lstTickets.FirstOrDefault(t => t.intNumero == intNumero);
            if (objTicket == null)
            {
                throw new KeyNotFoundException($"No existe el ticket #{intNumero}.");
            }
            return objTicket;
        }

        public void mostrarTickets()
        {
            Console.WriteLine("\n=== TICKETS REGISTRADOS ===");
            if (lstTickets.Count == 0)
            {
                Console.WriteLine("  No hay tickets registrados.");
                return;
            }

            foreach (Ticket objTicket in lstTickets)
            {
                objTicket.mostrarResumen();
                Console.WriteLine();
            }
        }

        public void generarResumenControl()
        {
            Console.WriteLine("\n=== RESUMEN DE CONTROL ===");
            Console.WriteLine($" Total de tickets: {lstTickets.Count}");

            foreach (string strEstado in new[] { "Abierto", "Asignado", "Resuelto", "Cerrado" })
            {
                int intCantidad = lstTickets.Count(t => t.strEstado == strEstado);
                Console.WriteLine($"  {strEstado}: {intCantidad}");
            }

            Console.WriteLine("\n Carga por tecnico:");
            foreach (Tecnico objTecnico in lstTecnicos)
            {
                Console.WriteLine($"  {objTecnico.strNombre} ({objTecnico.strEspecialidad}): {objTecnico.intTicketsAsignados} ticket(s)");
            }
        }
    }
}
