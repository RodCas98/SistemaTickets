namespace TicketsSoporte.logica
{
    /// <summary>
    /// Especializacion de Persona: tecnico de soporte con una especialidad y carga de tickets asignados.
    /// </summary>
    public class Tecnico : Persona
    {
        public string strEspecialidad { get; set; }
        public int intNivelExperiencia { get; set; }
        public int intTicketsAsignados { get; set; }

        public Tecnico(string strId, string strNombre, string strCorreo, string strEspecialidad, int intNivelExperiencia)
            : base(strId, strNombre, strCorreo)
        {
            this.strEspecialidad = strEspecialidad;
            this.intNivelExperiencia = intNivelExperiencia;
            this.intTicketsAsignados = 0;
        }

        public override void mostrarInformacion()
        {
            Console.WriteLine($"[Tecnico] {strId} - {strNombre} ({strCorreo})");
            Console.WriteLine($"  Especialidad: {strEspecialidad} | Nivel: {intNivelExperiencia} | Tickets asignados: {intTicketsAsignados}");
        }
    }
}
