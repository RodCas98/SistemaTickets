public abstract class Usuario
{
    private String codigo;
    private String nombre;
    private String correo;
    private boolean activo;

    public Usuario(String codigo, String nombre, String correo)
    {
        this.codigo = codigo;
        this.nombre = nombre;
        this.correo = correo;
        this.activo = true;
    }

    public abstract String obtenerRol();

    public void mostrarInformacion()
    {
        System.out.println("[" + obtenerRol() + "] " + codigo + " - " + nombre + " (" + correo + ")");
    }

    public String getCodigo()
    {
        return codigo;
    }

    public String getNombre()
    {
        return nombre;
    }

    public void setNombre(String nombre)
    {
        this.nombre = nombre;
    }

    public String getCorreo()
    {
        return correo;
    }

    public void setCorreo(String correo)
    {
        this.correo = correo;
    }

    public boolean isActivo()
    {
        return activo;
    }

    public void setActivo(boolean activo)
    {
        this.activo = activo;
    }
}
