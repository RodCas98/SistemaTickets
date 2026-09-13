import java.util.ArrayList;
import java.util.NoSuchElementException;

public class GestorTicket
{
    private ArrayList<Tecnico> tecnicos;
    private ArrayList<Solicitante> solicitantes;
    private ArrayList<Ticket> tickets;
    private int siguienteNumero;

    public GestorTicket()
    {
        tecnicos = new ArrayList<Tecnico>();
        solicitantes = new ArrayList<Solicitante>();
        tickets = new ArrayList<Ticket>();
        siguienteNumero = 1;
    }

    public Ticket crearTicket(String titulo, String descripcion, String categoria, String prioridad, Solicitante solicitante)
    {
        Ticket ticket = new Ticket(siguienteNumero, titulo, descripcion, categoria, prioridad, solicitante);
        siguienteNumero = siguienteNumero + 1;
        tickets.add(ticket);

        try {
            asignarTicket(ticket.getNumero());
        } catch (IllegalStateException e) {
            // No hay tecnicos disponibles: el ticket queda Abierto para asignarse mas adelante.
        }

        return ticket;
    }

    /**
     * Caso de uso "Asignar Ticket": asigna automaticamente al tecnico disponible
     * con menor carga que pueda atender la categoria del ticket.
     */
    public Ticket asignarTicket(int numero)
    {
        Ticket ticket = buscarTicket(numero);
        Tecnico elegido = null;

        for (Tecnico t : tecnicos) {
            if (t.puedeAtender(ticket.getCategoria())) {
                if (elegido == null || t.getCargaActual() < elegido.getCargaActual()) {
                    elegido = t;
                }
            }
        }

        if (elegido == null) {
            throw new IllegalStateException("No hay tecnicos disponibles para asignar el ticket.");
        }

        ticket.asignarTecnico(elegido);
        return ticket;
    }

    /**
     * Asignacion manual a un tecnico especifico (por codigo), usada para reasignaciones puntuales.
     */
    public Ticket asignarTicket(int numero, String codigoTecnico)
    {
        Ticket ticket = buscarTicket(numero);
        Tecnico tecnico = null;

        for (Tecnico t : tecnicos) {
            if (t.getCodigo().equals(codigoTecnico)) {
                tecnico = t;
                break;
            }
        }

        if (tecnico == null) {
            throw new NoSuchElementException("No existe el tecnico '" + codigoTecnico + "'.");
        }

        ticket.asignarTecnico(tecnico);
        return ticket;
    }

    /**
     * Caso de uso "Escalar Ticket": el propio Ticket sube la prioridad y deja constancia del motivo.
     */
    public Ticket escalarTicket(int numero, String motivo)
    {
        Ticket ticket = buscarTicket(numero);
        ticket.escalar(motivo);
        return ticket;
    }

    /**
     * Caso de uso "Resolver Ticket": el propio Ticket valida que este Asignado.
     */
    public Ticket resolverTicket(int numero, String solucion)
    {
        Ticket ticket = buscarTicket(numero);
        ticket.resolver(solucion);
        return ticket;
    }

    /**
     * Cierra el ticket; el propio Ticket valida que este Resuelto.
     */
    public Ticket cerrarTicket(int numero)
    {
        Ticket ticket = buscarTicket(numero);
        ticket.cerrar();
        return ticket;
    }

    public Ticket buscarTicket(int numero)
    {
        for (Ticket t : tickets) {
            if (t.getNumero() == numero) {
                return t;
            }
        }
        throw new NoSuchElementException("No existe el ticket #" + numero + ".");
    }

    public void mostrarTickets()
    {
        System.out.println("=== TICKETS REGISTRADOS ===");
        if (tickets.isEmpty()) {
            System.out.println("  No hay tickets registrados.");
            return;
        }

        for (Ticket t : tickets) {
            t.mostrarResumen();
            System.out.println();
        }
    }

    /**
     * Caso de uso "Generar Metricas": indicadores de control sobre los tickets del sistema.
     */
    public void generarMetricas()
    {
        System.out.println("=== METRICAS DEL SISTEMA ===");
        System.out.println(" Total de tickets: " + tickets.size());

        String[] estados = { "Abierto", "Asignado", "Resuelto", "Cerrado" };
        for (String estado : estados) {
            int cantidad = 0;
            for (Ticket t : tickets) {
                if (t.getEstado().equals(estado)) {
                    cantidad = cantidad + 1;
                }
            }
            System.out.println("  " + estado + ": " + cantidad);
        }

        int escalados = 0;
        for (Ticket t : tickets) {
            if (t.isEscalado()) {
                escalados = escalados + 1;
            }
        }
        System.out.println(" Tickets escalados: " + escalados);

        System.out.println(" Carga por tecnico:");
        for (Tecnico t : tecnicos) {
            System.out.println("  " + t.getNombre() + " (" + t.getEspecialidad() + "): " + t.getCargaActual() + "/" + t.getCapacidadMaxima());
        }
    }

    public ArrayList<Tecnico> getTecnicos()
    {
        return tecnicos;
    }

    public ArrayList<Solicitante> getSolicitantes()
    {
        return solicitantes;
    }

    public ArrayList<Ticket> getTickets()
    {
        return tickets;
    }
}
