import java.util.ArrayList;
import java.util.Date;

public class Ticket
{
    private int numero;
    private String titulo;
    private String descripcion;
    private String categoria;
    private String prioridad;
    private String estado;
    private boolean escalado;
    private Solicitante solicitante;
    private Tecnico tecnicoAsignado;
    private ArrayList<String> bitacora;
    private ArrayList<String> errores;

    public Ticket(int numero, String titulo, String descripcion, String categoria, String prioridad, Solicitante solicitante)
    {
        this.numero = numero;
        this.titulo = titulo;
        this.descripcion = descripcion;
        this.categoria = categoria;
        this.prioridad = prioridad;
        this.solicitante = solicitante;
        this.estado = "Abierto";
        this.escalado = false;
        this.bitacora = new ArrayList<String>();
        this.errores = new ArrayList<String>();
        registrarBitacora("Ticket creado");
    }

    public void registrarBitacora(String evento)
    {
        bitacora.add(new Date() + " - " + evento);
    }

    public void asignarTecnico(Tecnico tecnico)
    {
        if (tecnico == null) {
            throw new IllegalStateException("No se puede asignar un tecnico nulo.");
        }

        if (!tecnico.puedeAtender(categoria)) {
            throw new IllegalStateException("El tecnico no esta disponible o no atiende esta categoria.");
        }

        tecnicoAsignado = tecnico;
        tecnico.aumentarCarga();
        estado = "Asignado";
        registrarBitacora("Asignado a " + tecnico.getNombre());
    }

    public void registrarError(String tipo, String descripcionError, String impacto)
    {
        String error = tipo + ": " + descripcionError + " | Impacto: " + impacto;
        errores.add(error);
        registrarBitacora("Error registrado - " + error);

        if (impacto.equalsIgnoreCase("Alto") || impacto.equalsIgnoreCase("Critico")) {
            escalar("Error de alto impacto");
        }
    }

    public void resolver(String solucion)
    {
        if (tecnicoAsignado == null || !estado.equals("Asignado")) {
            throw new IllegalStateException("Solo se puede resolver un ticket asignado.");
        }

        estado = "Resuelto";
        registrarBitacora("Solucion registrada: " + solucion);
    }

    public void cerrar()
    {
        if (!estado.equals("Resuelto")) {
            throw new IllegalStateException("Solo se puede cerrar un ticket resuelto.");
        }

        estado = "Cerrado";
        if (tecnicoAsignado != null) {
            tecnicoAsignado.liberarCarga();
        }
        registrarBitacora("Ticket cerrado");
    }

    public void escalar(String motivo)
    {
        escalado = true;
        if (!prioridad.equalsIgnoreCase("Critica")) {
            prioridad = "Critica";
        }
        registrarBitacora("Ticket escalado: " + motivo);
    }

    public void mostrarResumen()
    {
        System.out.println("#" + numero + " | " + titulo + " | Estado: " + estado + " | Prioridad: " + prioridad + " | Categoria: " + categoria);
        System.out.println(" Solicitante: " + solicitante.getNombre());
        System.out.println(" Tecnico: " + (tecnicoAsignado != null ? tecnicoAsignado.getNombre() : "Sin asignar") + " | Escalado: " + escalado);
    }

    public void mostrarBitacora()
    {
        System.out.println();
        System.out.println("Bitacora del ticket #" + numero);
        for (String evento : bitacora) {
            System.out.println("- " + evento);
        }
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

    public ArrayList<String> getBitacora()
    {
        return bitacora;
    }

    public ArrayList<String> getErrores()
    {
        return errores;
    }
}
