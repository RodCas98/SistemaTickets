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
            lstTickets.Add(objTicket);

            try
            {
                asignarTicket(objTicket.intNumero);
            }
            catch (InvalidOperationException)
            {
                // No hay tecnicos disponibles: el ticket queda Abierto para asignarse mas adelante.
            }

            return objTicket;
        }

        /// <summary>
        /// Caso de uso "Asignar Ticket": asigna (o reasigna) automaticamente al tecnico
        /// con menor carga que coincida con la categoria del ticket, o a un tecnico General.
        /// </summary>
        public Ticket asignarTicket(int intNumero)
        {
            Ticket objTicket = buscarTicket(intNumero);
            Tecnico objTecnico = asignarTecnicoAutomatico(objTicket.strCategoria);

            if (objTecnico == null)
            {
                throw new InvalidOperationException("No hay tecnicos disponibles para asignar el ticket.");
            }

            objTicket.asignar(objTecnico);
            return objTicket;
        }

        /// <summary>
        /// Asignacion manual a un tecnico especifico (por id), usada para reasignaciones puntuales.
        /// </summary>
        public Ticket asignarTicket(int intNumero, string strIdTecnico)
        {
            Ticket objTicket = buscarTicket(intNumero);
            Tecnico objTecnico = lstTecnicos.FirstOrDefault(t => t.strId == strIdTecnico);

            if (objTecnico == null)
            {
                throw new KeyNotFoundException($"No existe el tecnico '{strIdTecnico}'.");
            }

            objTicket.asignar(objTecnico);
            return objTicket;
        }

        /// <summary>
        /// Caso de uso "Escalar Ticket": sube la prioridad y reasigna a un tecnico de mayor
        /// nivel de experiencia dentro de la misma especialidad (si existe uno disponible).
        /// </summary>
        public Ticket escalarTicket(int intNumero, string strNuevaPrioridad)
        {
            Ticket objTicket = buscarTicket(intNumero);

            Tecnico objTecnicoSenior = lstTecnicos
                .Where(t => t.strEspecialidad.Equals(objTicket.strCategoria, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(t => t.intNivelExperiencia)
                .ThenBy(t => t.intTicketsAsignados)
                .FirstOrDefault();

            objTicket.escalar(strNuevaPrioridad, objTecnicoSenior);
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

        /// <summary>
        /// Caso de uso "Generar Metricas": indicadores de control sobre los tickets del sistema.
        /// </summary>
        public void generarMetricas()
        {
            Console.WriteLine("\n=== METRICAS DEL SISTEMA ===");
            Console.WriteLine($" Total de tickets: {lstTickets.Count}");

            Console.WriteLine("\n Tickets por estado:");
            foreach (string strEstado in new[] { "Abierto", "Asignado", "Escalado", "Resuelto", "Cerrado" })
            {
                int intCantidad = lstTickets.Count(t => t.strEstado == strEstado);
                Console.WriteLine($"  {strEstado}: {intCantidad}");
            }

            Console.WriteLine("\n Tickets por prioridad:");
            foreach (string strPrioridad in new[] { "Baja", "Media", "Alta", "Critica" })
            {
                int intCantidad = lstTickets.Count(t => t.strPrioridad.Equals(strPrioridad, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"  {strPrioridad}: {intCantidad}");
            }

            int intEscalados = lstTickets.Count(t => t.blnEscalado);
            Console.WriteLine($"\n Tickets escalados: {intEscalados}");

            Console.WriteLine("\n Carga por tecnico:");
            foreach (Tecnico objTecnico in lstTecnicos)
            {
                Console.WriteLine($"  {objTecnico.strNombre} ({objTecnico.strEspecialidad}): {objTecnico.intTicketsAsignados} ticket(s)");
            }
        }
    }
}
