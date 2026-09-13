namespace TicketsSoporte.logica
{
    /// <summary>
    /// Administra tecnicos, solicitantes y tickets; asigna automaticamente al tecnico
    /// disponible con menor carga que pueda atender la categoria del ticket.
    /// </summary>
    public class GestorTicket
    {
        public List<Tecnico> lstTecnicos { get; set; }
        public List<Solicitante> lstSolicitantes { get; set; }
        public List<Ticket> lstTickets { get; set; }

        private int intSiguienteNumero;

        public GestorTicket()
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
        /// Caso de uso "Asignar Ticket": asigna automaticamente al tecnico disponible
        /// con menor carga que pueda atender la categoria del ticket.
        /// </summary>
        public Ticket asignarTicket(int intNumero)
        {
            Ticket objTicket = buscarTicket(intNumero);
            Tecnico objTecnico = lstTecnicos
                .Where(t => t.puedeAtender(objTicket.strCategoria))
                .OrderBy(t => t.intCargaActual)
                .FirstOrDefault();

            if (objTecnico == null)
            {
                throw new InvalidOperationException("No hay tecnicos disponibles para asignar el ticket.");
            }

            objTicket.asignarTecnico(objTecnico);
            return objTicket;
        }

        /// <summary>
        /// Asignacion manual a un tecnico especifico (por codigo), usada para reasignaciones puntuales.
        /// </summary>
        public Ticket asignarTicket(int intNumero, string strCodigoTecnico)
        {
            Ticket objTicket = buscarTicket(intNumero);
            Tecnico objTecnico = lstTecnicos.FirstOrDefault(t => t.strCodigo == strCodigoTecnico);

            if (objTecnico == null)
            {
                throw new KeyNotFoundException($"No existe el tecnico '{strCodigoTecnico}'.");
            }

            objTicket.asignarTecnico(objTecnico);
            return objTicket;
        }

        /// <summary>
        /// Caso de uso "Escalar Ticket": sube la prioridad a Critica y deja constancia del motivo.
        /// </summary>
        public Ticket escalarTicket(int intNumero, string strMotivo)
        {
            Ticket objTicket = buscarTicket(intNumero);
            objTicket.escalar(strMotivo);
            return objTicket;
        }

        /// <summary>
        /// Caso de uso "Resolver Ticket": el propio Ticket valida que este Asignado.
        /// </summary>
        public Ticket resolverTicket(int intNumero, string strSolucion)
        {
            Ticket objTicket = buscarTicket(intNumero);
            objTicket.resolver(strSolucion);
            return objTicket;
        }

        /// <summary>
        /// Cierra el ticket; el propio Ticket valida que este Resuelto.
        /// </summary>
        public Ticket cerrarTicket(int intNumero)
        {
            Ticket objTicket = buscarTicket(intNumero);
            objTicket.cerrar();
            return objTicket;
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
            foreach (string strEstado in new[] { "Abierto", "Asignado", "Resuelto", "Cerrado" })
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
                Console.WriteLine($"  {objTecnico.strNombre} ({objTecnico.strEspecialidad}): {objTecnico.intCargaActual}/{objTecnico.intCapacidadMaxima}");
            }
        }
    }
}
