import java.util.ArrayList;
import java.util.HashMap;

public class FlujoTicket
{
    private HashMap<String, ArrayList<String>> transiciones;

    public FlujoTicket()
    {
        transiciones = new HashMap<String, ArrayList<String>>();

        ArrayList<String> desdeAbierto = new ArrayList<String>();
        desdeAbierto.add("Asignado");
        transiciones.put("Abierto", desdeAbierto);

        ArrayList<String> desdeAsignado = new ArrayList<String>();
        desdeAsignado.add("Escalado");
        desdeAsignado.add("Resuelto");
        transiciones.put("Asignado", desdeAsignado);

        ArrayList<String> desdeEscalado = new ArrayList<String>();
        desdeEscalado.add("Asignado");
        desdeEscalado.add("Resuelto");
        transiciones.put("Escalado", desdeEscalado);

        ArrayList<String> desdeResuelto = new ArrayList<String>();
        desdeResuelto.add("Cerrado");
        desdeResuelto.add("Asignado");
        transiciones.put("Resuelto", desdeResuelto);

        transiciones.put("Cerrado", new ArrayList<String>());
    }

    public boolean puedeCambiarEstado(String estadoActual, String estadoNuevo)
    {
        return transiciones.containsKey(estadoActual) && transiciones.get(estadoActual).contains(estadoNuevo);
    }

    public void mostrarFlujo()
    {
        System.out.println("=== FLUJO DE ESTADOS DEL TICKET ===");
        System.out.println("Abierto -> Asignado -> Escalado -> Resuelto -> Cerrado");

        for (String estado : transiciones.keySet()) {
            ArrayList<String> destinos = transiciones.get(estado);
            String texto = destinos.isEmpty() ? "(estado final)" : String.join(", ", destinos);
            System.out.println(" " + estado + " => " + texto);
        }
    }
}
