public class Solicitante extends Persona
{
    private String departamento;
    private String extension;

    public Solicitante(String id, String nombre, String correo, String departamento, String extension)
    {
        super(id, nombre, correo);
        this.departamento = departamento;
        this.extension = extension;
    }

    public String getDepartamento()
    {
        return departamento;
    }

    public void setDepartamento(String departamento)
    {
        this.departamento = departamento;
    }

    public String getExtension()
    {
        return extension;
    }

    public void setExtension(String extension)
    {
        this.extension = extension;
    }

    public void mostrarInformacion()
    {
        System.out.println("[Solicitante] " + getId() + " - " + getNombre() + " (" + getCorreo() + ")");
        System.out.println("  Departamento: " + departamento + " | Extension: " + extension);
    }
}
