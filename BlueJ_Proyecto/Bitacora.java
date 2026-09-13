import java.util.ArrayList;
import java.util.Date;

public class Bitacora
{
    private ArrayList<String> registros;

    public Bitacora()
    {
        registros = new ArrayList<String>();
    }

    public void registrarEvento(String evento)
    {
        registros.add("[" + new Date() + "] " + evento);
    }

    public void mostrarBitacora()
    {
        System.out.println("  --- Bitacora ---");
        for (String registro : registros) {
            System.out.println("  " + registro);
        }
    }

    public ArrayList<String> getRegistros()
    {
        return registros;
    }
}
