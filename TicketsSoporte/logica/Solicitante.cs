namespace TicketsSoporte.logica
{
    /// <summary>
    /// Especializacion de Persona: empleado que reporta problemas y solicita soporte.
    /// </summary>
    public class Solicitante : Persona
    {
        public string strDepartamento { get; set; }
        public string strExtension { get; set; }

        public Solicitante(string strId, string strNombre, string strCorreo, string strDepartamento, string strExtension)
            : base(strId, strNombre, strCorreo)
        {
            this.strDepartamento = strDepartamento;
            this.strExtension = strExtension;
        }

        public override void mostrarInformacion()
        {
            Console.WriteLine($"[Solicitante] {strId} - {strNombre} ({strCorreo})");
            Console.WriteLine($"  Departamento: {strDepartamento} | Extension: {strExtension}");
        }
    }
}
