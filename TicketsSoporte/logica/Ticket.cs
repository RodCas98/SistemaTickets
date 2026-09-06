namespace TicketsSoporte.logica
{
    /// <summary>
    /// Representa un ticket de soporte y su ciclo de vida: Abierto -> Asignado -> (Escalado) -> Resuelto -> Cerrado.
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
        public bool blnEscalado { get; set; }

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
            if (objTecnicoAsignado != null)
            {
                objTecnicoAsignado.intTicketsAsignados--;
                objBitacora.registrarEvento($"Ticket reasignado de {objTecnicoAsignado.strNombre} a {objTecnico.strNombre} ({objTecnico.strEspecialidad})");
            }
            else
            {
                objBitacora.registrarEvento($"Ticket asignado a {objTecnico.strNombre} ({objTecnico.strEspecialidad})");
            }

            objTecnicoAsignado = objTecnico;
            objTecnico.intTicketsAsignados++;
            strEstado = "Asignado";
        }

        public void escalar(string strNuevaPrioridad, Tecnico objTecnicoSenior)
        {
            string strPrioridadAnterior = strPrioridad;
            strPrioridad = strNuevaPrioridad;
            blnEscalado = true;
            strEstado = "Escalado";
            objBitacora.registrarEvento($"Ticket escalado - Prioridad: {strPrioridadAnterior} -> {strNuevaPrioridad}");

            if (objTecnicoSenior != null && objTecnicoSenior != objTecnicoAsignado)
            {
                asignar(objTecnicoSenior);
                strEstado = "Escalado";
            }
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
            Console.WriteLine($"  Estado: {strEstado}{(blnEscalado ? " (escalado)" : "")} | Categoria: {strCategoria} | Prioridad: {strPrioridad}");
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
