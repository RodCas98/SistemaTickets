namespace TicketsSoporte.logica
{
    /// <summary>
    /// Clase base abstracta: define el contrato comun a Tecnico y Solicitante.
    /// </summary>
    public abstract class Usuario
    {
        public string strId { get; set; }
        public string strNombre { get; set; }
        public string strCorreo { get; set; }

        protected Usuario(string strId, string strNombre, string strCorreo)
        {
            this.strId = strId;
            this.strNombre = strNombre;
            this.strCorreo = strCorreo;
        }

        public abstract void mostrarInformacion();
    }
}
