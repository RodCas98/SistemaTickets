public class Solicitante extends Usuario
{
    private String departamento;
    private String extension;

    public Solicitante(String codigo, String nombre, String correo, String departamento, String extension)
    {
        super(codigo, nombre, correo);
        this.departamento = departamento;
        this.extension = extension;
    }

    public String obtenerRol()
    {
        return "Solicitante";
    }

    public void mostrarInformacion()
    {
        super.mostrarInformacion();
        System.out.println(" Departamento: " + departamento + " | Extension: " + extension);
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
}
