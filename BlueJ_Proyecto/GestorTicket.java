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
