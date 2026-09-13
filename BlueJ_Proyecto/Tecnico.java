public class Tecnico extends Persona
{
    private String especialidad;
    private int nivelExperiencia;
    private int ticketsAsignados;

    public Tecnico(String id, String nombre, String correo, String especialidad, int nivelExperiencia)
    {
        super(id, nombre, correo);
        this.especialidad = especialidad;
        this.nivelExperiencia = nivelExperiencia;
        this.ticketsAsignados = 0;
    }

    public void incrementarCarga()
    {
        ticketsAsignados = ticketsAsignados + 1;
    }

    public void decrementarCarga()
    {
        ticketsAsignados = ticketsAsignados - 1;
    }

    public String getEspecialidad()
    {
        return especialidad;
    }

    public int getNivelExperiencia()
    {
        return nivelExperiencia;
    }

    public int getTicketsAsignados()
    {
        return ticketsAsignados;
    }

    public void mostrarInformacion()
    {
        System.out.println("[Tecnico] " + getId() + " - " + getNombre() + " (" + getCorreo() + ")");
        System.out.println("  Especialidad: " + especialidad + " | Nivel: " + nivelExperiencia + " | Tickets asignados: " + ticketsAsignados);
    }
}
