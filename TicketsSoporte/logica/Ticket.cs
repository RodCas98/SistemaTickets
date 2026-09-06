namespace TicketsSoporte.logica
{
    /// <summary>
    /// Representa un ticket de soporte y su ciclo de vida: Abierto -> Asignado -> Resuelto -> Cerrado.
    /// </summary>
    public class Ticket
    {
        public int intNumero { get; set; }
        public string strTitulo { get; set; }
        public string strDescripcion { get; set; }
        public string strCategoria { get; set; }
        public string strPrioridad { get; set; }
        public string strEstado { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string strSolucion { get; set; }

        public Solicitante objSolicitante { get; set; }
        public Tecnico objTecnicoAsignado { get; set; }
        public Bitacora objBitacora { get; set; }

        public Ticket(int intNumero, string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            this.intNumero = intNumero;
            this.strTitulo = strTitulo;
            this.strDescripcion = strDescripcion;
            this.strCategoria = strCategoria;
            this.strPrioridad = strPrioridad;
            this.strEstado = "Abierto";
            this.dtFechaCreacion = DateTime.Now;
            this.objSolicitante = objSolicitante;
            this.objBitacora = new Bitacora();

            objBitacora.registrarEvento($"Ticket creado por {objSolicitante.strNombre} - Categoria: {strCategoria} - Prioridad: {strPrioridad}");
        }

        public void asignar(Tecnico objTecnico)
        {
            objTecnicoAsignado = objTecnico;
            objTecnico.intTicketsAsignados++;
            strEstado = "Asignado";
            objBitacora.registrarEvento($"Ticket asignado a {objTecnico.strNombre} ({objTecnico.strEspecialidad})");
        }

        public void registrarError(string strTipo, string strDescripcionError, string strImpacto)
        {
            objBitacora.registrarEvento($"Error registrado - Tipo: {strTipo} - Impacto: {strImpacto} - Detalle: {strDescripcionError}");
        }

        public void resolver(string strSolucionAplicada)
        {
            strSolucion = strSolucionAplicada;
            strEstado = "Resuelto";
            objBitacora.registrarEvento($"Ticket resuelto - Solucion: {strSolucionAplicada}");
        }

        public void cerrar()
        {
            strEstado = "Cerrado";
            objBitacora.registrarEvento("Ticket cerrado");
        }

        public void mostrarResumen()
        {
            Console.WriteLine($"  Ticket #{intNumero} - {strTitulo}");
            Console.WriteLine($"  Estado: {strEstado} | Categoria: {strCategoria} | Prioridad: {strPrioridad}");
            Console.WriteLine($"  Solicitante: {objSolicitante.strNombre}");
            Console.WriteLine($"  Tecnico asignado: {(objTecnicoAsignado != null ? objTecnicoAsignado.strNombre : "Sin asignar")}");
        }

        public void mostrarBitacora()
        {
            mostrarResumen();
            objBitacora.mostrarBitacora();
        }
    }
}
