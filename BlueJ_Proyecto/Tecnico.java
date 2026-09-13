public class Tecnico extends Usuario
{
    private String especialidad;
    private int cargaActual;
    private int capacidadMaxima;

    public Tecnico(String codigo, String nombre, String correo, String especialidad, int capacidadMaxima)
    {
        super(codigo, nombre, correo);
        this.especialidad = especialidad;
        this.cargaActual = 0;
        this.capacidadMaxima = capacidadMaxima;
    }

    public String obtenerRol()
    {
        return "Tecnico";
    }

    public boolean estaDisponible()
    {
        return isActivo() && cargaActual < capacidadMaxima;
    }

    public boolean puedeAtender(String categoria)
    {
        return estaDisponible() &&
               (especialidad.equalsIgnoreCase(categoria) || especialidad.equalsIgnoreCase("General"));
    }

    public void aumentarCarga()
    {
        if (cargaActual < capacidadMaxima) {
            cargaActual = cargaActual + 1;
        }
    }

    public void liberarCarga()
    {
        if (cargaActual > 0) {
            cargaActual = cargaActual - 1;
        }
    }

    public void mostrarInformacion()
    {
        super.mostrarInformacion();
        System.out.println(" Especialidad: " + especialidad + " | Carga: " + cargaActual + "/" + capacidadMaxima);
    }

    public String getEspecialidad()
    {
        return especialidad;
    }

    public int getCargaActual()
    {
        return cargaActual;
    }

    public int getCapacidadMaxima()
    {
        return capacidadMaxima;
    }
}
