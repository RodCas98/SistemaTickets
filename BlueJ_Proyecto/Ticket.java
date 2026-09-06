import java.util.Date;

public class Ticket
{
    private int numero;
    private String titulo;
    private String descripcion;
    private String categoria;
    private String prioridad;
    private String estado;
    private String fechaCreacion;
    private String solucion;
    private boolean escalado;

    private Solicitante solicitante;
    private Tecnico tecnicoAsignado;
    private Bitacora bitacora;

    public Ticket(int numero, String titulo, String descripcion, String categoria, String prioridad, Solicitante solicitante)
    {
        this.numero = numero;
        this.titulo = titulo;
        this.descripcion = descripcion;
        this.categoria = categoria;
        this.prioridad = prioridad;
        this.estado = "Abierto";
        this.fechaCreacion = new Date().toString();
        this.solicitante = solicitante;
        this.bitacora = new Bitacora();
        this.escalado = false;

        bitacora.registrarEvento("Ticket creado por " + solicitante.getNombre() + " - Categoria: " + categoria + " - Prioridad: " + prioridad);
    }

    public void asignar(Tecnico tecnico)
    {
        if (tecnicoAsignado != null) {
            tecnicoAsignado.decrementarCarga();
            bitacora.registrarEvento("Ticket reasignado de " + tecnicoAsignado.getNombre() + " a " + tecnico.getNombre() + " (" + tecnico.getEspecialidad() + ")");
        } else {
            bitacora.registrarEvento("Ticket asignado a " + tecnico.getNombre() + " (" + tecnico.getEspecialidad() + ")");
        }

        tecnicoAsignado = tecnico;
        tecnico.incrementarCarga();
        estado = "Asignado";
    }

    public void escalar(String nuevaPrioridad, Tecnico tecnicoSenior)
    {
        String prioridadAnterior = prioridad;
        prioridad = nuevaPrioridad;
        escalado = true;
        estado = "Escalado";
        bitacora.registrarEvento("Ticket escalado - Prioridad: " + prioridadAnterior + " -> " + nuevaPrioridad);

        if (tecnicoSenior != null && tecnicoSenior != tecnicoAsignado) {
            asignar(tecnicoSenior);
            estado = "Escalado";
        }
    }

    public void registrarError(String tipo, String descripcionError, String impacto)
    {
        bitacora.registrarEvento("Error registrado - Tipo: " + tipo + " - Impacto: " + impacto + " - Detalle: " + descripcionError);
    }

    public void resolver(String solucionAplicada)
    {
        solucion = solucionAplicada;
        estado = "Resuelto";
        bitacora.registrarEvento("Ticket resuelto - Solucion: " + solucionAplicada);
    }

    public void cerrar()
    {
        estado = "Cerrado";
        bitacora.registrarEvento("Ticket cerrado");
    }

    public void mostrarResumen()
    {
        System.out.println("  Ticket #" + numero + " - " + titulo);
        System.out.println("  Estado: " + estado + (escalado ? " (escalado)" : "") + " | Categoria: " + categoria + " | Prioridad: " + prioridad);
        System.out.println("  Solicitante: " + solicitante.getNombre());
        System.out.println("  Tecnico asignado: " + (tecnicoAsignado != null ? tecnicoAsignado.getNombre() : "Sin asignar"));
    }

    public void mostrarBitacora()
    {
        mostrarResumen();
        bitacora.mostrarBitacora();
    }

    public int getNumero()
    {
        return numero;
    }

    public String getTitulo()
    {
        return titulo;
    }

    public String getDescripcion()
    {
        return descripcion;
    }

    public String getCategoria()
    {
        return categoria;
    }

    public String getPrioridad()
    {
        return prioridad;
    }

    public String getEstado()
    {
        return estado;
    }

    public boolean isEscalado()
    {
        return escalado;
    }

    public Solicitante getSolicitante()
    {
        return solicitante;
    }

    public Tecnico getTecnicoAsignado()
    {
        return tecnicoAsignado;
    }

    public Bitacora getBitacora()
    {
        return bitacora;
    }
}
