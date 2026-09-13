import java.util.ArrayList;
import java.util.NoSuchElementException;

public class GestorTicket
{
    private ArrayList<Tecnico> tecnicos;
    private ArrayList<Solicitante> solicitantes;
    private ArrayList<Ticket> tickets;
    private FlujoTicket flujo;
    private int siguienteNumero;

    public GestorTicket()
    {
        tecnicos = new ArrayList<Tecnico>();
        solicitantes = new ArrayList<Solicitante>();
        tickets = new ArrayList<Ticket>();
        flujo = new FlujoTicket();
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

    public Ticket asignarTicket(int numero)
    {
        Ticket ticket = buscarTicket(numero);
        Tecnico tecnico = asignarTecnicoAutomatico(ticket.getCategoria());

        if (tecnico == null) {
            throw new IllegalStateException("No hay tecnicos disponibles para asignar el ticket.");
        }

        ticket.asignar(tecnico);
        return ticket;
    }

    public Ticket asignarTicket(int numero, String idTecnico)
    {
        Ticket ticket = buscarTicket(numero);
        Tecnico tecnico = null;

        for (Tecnico t : tecnicos) {
            if (t.getId().equals(idTecnico)) {
                tecnico = t;
                break;
            }
        }

        if (tecnico == null) {
            throw new NoSuchElementException("No existe el tecnico '" + idTecnico + "'.");
        }

        ticket.asignar(tecnico);
        return ticket;
    }

    public Ticket escalarTicket(int numero, String nuevaPrioridad)
    {
        Ticket ticket = buscarTicket(numero);

        if (!flujo.puedeCambiarEstado(ticket.getEstado(), "Escalado")) {
            throw new IllegalStateException("El flujo no permite escalar el ticket desde el estado actual.");
        }

        Tecnico tecnicoSenior = null;

        for (Tecnico t : tecnicos) {
            if (t.getEspecialidad().equalsIgnoreCase(ticket.getCategoria())) {
                if (tecnicoSenior == null || t.getNivelExperiencia() > tecnicoSenior.getNivelExperiencia()) {
                    tecnicoSenior = t;
                }
            }
        }

        ticket.escalar(nuevaPrioridad, tecnicoSenior);
        return ticket;
    }

    /**
     * Caso de uso "Resolver Ticket": valida el flujo antes de registrar la solucion.
     */
    public Ticket resolverTicket(int numero, String solucion)
    {
        Ticket ticket = buscarTicket(numero);

        if (!flujo.puedeCambiarEstado(ticket.getEstado(), "Resuelto")) {
            throw new IllegalStateException("El flujo no permite resolver el ticket desde el estado actual.");
        }

        ticket.resolver(solucion);
        return ticket;
    }

    /**
     * Cierra el ticket, validando el flujo de estados.
     */
    public Ticket cerrarTicket(int numero)
    {
        Ticket ticket = buscarTicket(numero);

        if (!flujo.puedeCambiarEstado(ticket.getEstado(), "Cerrado")) {
            throw new IllegalStateException("El flujo no permite cerrar el ticket desde el estado actual.");
        }

        ticket.cerrar();
        return ticket;
    }

    public void mostrarFlujo()
    {
        flujo.mostrarFlujo();
    }

    private Tecnico asignarTecnicoAutomatico(String categoria)
    {
        Tecnico elegido = null;

        for (Tecnico t : tecnicos) {
            if (t.getEspecialidad().equalsIgnoreCase(categoria)) {
                if (elegido == null || t.getTicketsAsignados() < elegido.getTicketsAsignados()) {
                    elegido = t;
                }
            }
        }

        if (elegido == null) {
            for (Tecnico t : tecnicos) {
                if (t.getEspecialidad().equalsIgnoreCase("General")) {
                    if (elegido == null || t.getTicketsAsignados() < elegido.getTicketsAsignados()) {
                        elegido = t;
                    }
                }
            }
        }

        return elegido;
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

    public void generarMetricas()
    {
        System.out.println("=== METRICAS DEL SISTEMA ===");
        System.out.println(" Total de tickets: " + tickets.size());

        String[] estados = { "Abierto", "Asignado", "Escalado", "Resuelto", "Cerrado" };
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
            System.out.println("  " + t.getNombre() + " (" + t.getEspecialidad() + "): " + t.getTicketsAsignados() + " ticket(s)");
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
