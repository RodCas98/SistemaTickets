using TicketsSoporte.logica;

namespace TicketsSoporte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Herencia: Tecnico y Solicitante son tipos especializados de Persona.
            Tecnico objTecnicoSoftware = new Tecnico("T01", "Ana Lopez", "ana@empresa.com", "Software", 2);
            Tecnico objTecnicoHardware = new Tecnico("T02", "Carlos Mendez", "carlos@empresa.com", "Hardware", 2);
            Tecnico objTecnicoGeneral = new Tecnico("T03", "Maria Perez", "maria@empresa.com", "General", 3);

            Solicitante objSolicitanteContabilidad = new Solicitante("S01", "Luis Ramirez", "luis@empresa.com", "Contabilidad", "1201");
            Solicitante objSolicitanteVentas = new Solicitante("S02", "Karla Gomez", "karla@empresa.com", "Ventas", "1305");

            // 2. Polimorfismo: una lista de Persona puede contener tecnicos y solicitantes.
            List<Persona> lstPersonas = new List<Persona>()
            {
                objTecnicoSoftware,
                objTecnicoHardware,
                objTecnicoGeneral,
                objSolicitanteContabilidad,
                objSolicitanteVentas
            };

            // 3. Gestion centralizada: el gestor administra tecnicos, solicitantes y tickets.
            GestorTicket objGestor = new GestorTicket();
            objGestor.lstTecnicos.Add(objTecnicoSoftware);
            objGestor.lstTecnicos.Add(objTecnicoHardware);
            objGestor.lstTecnicos.Add(objTecnicoGeneral);
            objGestor.lstSolicitantes.Add(objSolicitanteContabilidad);
            objGestor.lstSolicitantes.Add(objSolicitanteVentas);

            bool blnContinuar = true;

            while (blnContinuar)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n========================================================");
                Console.WriteLine(" SISTEMA DE TICKETS DE SOPORTE TECNICO CORPORATIVO");
                Console.WriteLine("========================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. Ver usuarios del sistema (polimorfismo)");
                Console.WriteLine(" 2. Ver flujo de estados del ticket");
                Console.WriteLine(" 3. Crear ticket (asigna automaticamente)");
                Console.WriteLine(" 4. Ver tickets");
                Console.WriteLine(" 5. Asignar / reasignar ticket a un tecnico");
                Console.WriteLine(" 6. Registrar error en ticket");
                Console.WriteLine(" 7. Resolver ticket");
                Console.WriteLine(" 8. Escalar ticket");
                Console.WriteLine(" 9. Cerrar ticket");
                Console.WriteLine(" 10. Consultar bitacora de un ticket");
                Console.WriteLine(" 11. Generar metricas");
                Console.WriteLine(" 12. Salir");
                Console.Write("\n Seleccione una opcion (1-12): ");

                try
                {
                    string strOpcion = Console.ReadLine()?.Trim();

                    switch (strOpcion)
                    {
                        case "1":
                            mostrarPersonasPolimorfismo(lstPersonas);
                            break;

                        case "2":
                            objGestor.mostrarFlujo();
                            break;

                        case "3":
                            crearTicket(objGestor);
                            break;

                        case "4":
                            objGestor.mostrarTickets();
                            break;

                        case "5":
                            asignarTicket(objGestor);
                            break;

                        case "6":
                            registrarError(objGestor);
                            break;

                        case "7":
                            resolverTicket(objGestor);
                            break;

                        case "8":
                            escalarTicket(objGestor);
                            break;

                        case "9":
                            cerrarTicket(objGestor);
                            break;

                        case "10":
                            consultarBitacora(objGestor);
                            break;

                        case "11":
                            objGestor.generarMetricas();
                            break;

                        case "12":
                            blnContinuar = false;
                            Console.WriteLine("\nGracias por utilizar el sistema de soporte.");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opcion no valida. Ingrese un numero del 1 al 12.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error controlado por try/catch: " + ex.Message);
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Demuestra polimorfismo: cada objeto ejecuta su propia version de mostrarInformacion().
        /// </summary>
        static void mostrarPersonasPolimorfismo(List<Persona> lstPersonas)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== USUARIOS DEL SISTEMA ===");
            Console.ResetColor();

            foreach (Persona objPersona in lstPersonas)
            {
                objPersona.mostrarInformacion();
                Console.WriteLine();
            }
        }

        static void crearTicket(GestorTicket objGestor)
        {
            Console.WriteLine("\nSolicitantes disponibles:");
            for (int i = 0; i < objGestor.lstSolicitantes.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {objGestor.lstSolicitantes[i].strNombre} - {objGestor.lstSolicitantes[i].strDepartamento}");
            }

            Console.Write("Seleccione solicitante: ");
            int intIndice = int.Parse(Console.ReadLine() ?? "0") - 1;

            if (intIndice < 0 || intIndice >= objGestor.lstSolicitantes.Count)
            {
                throw new ArgumentOutOfRangeException("Solicitante", "Seleccion fuera de rango.");
            }

            Console.Write("Titulo del problema: ");
            string strTitulo = Console.ReadLine();

            Console.Write("Descripcion: ");
            string strDescripcion = Console.ReadLine();

            Console.Write("Categoria (Software/Hardware/Red/General): ");
            string strCategoria = Console.ReadLine();

            Console.Write("Prioridad (Baja/Media/Alta/Critica): ");
            string strPrioridad = Console.ReadLine();

            Ticket objTicket = objGestor.crearTicket(strTitulo, strDescripcion, strCategoria, strPrioridad, objGestor.lstSolicitantes[intIndice]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTicket creado correctamente.");
            Console.ResetColor();
            objTicket.mostrarResumen();
        }

        static void asignarTicket(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            Console.WriteLine("Tecnicos disponibles:");
            for (int i = 0; i < objGestor.lstTecnicos.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {objGestor.lstTecnicos[i].strNombre} - {objGestor.lstTecnicos[i].strEspecialidad}");
            }

            Console.Write("Seleccione tecnico (Enter para asignacion automatica): ");
            string strOpcion = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(strOpcion))
            {
                objGestor.asignarTicket(objTicket.intNumero);
            }
            else
            {
                int intIndice = int.Parse(strOpcion) - 1;
                if (intIndice < 0 || intIndice >= objGestor.lstTecnicos.Count)
                {
                    throw new ArgumentOutOfRangeException("Tecnico", "Seleccion fuera de rango.");
                }

                objGestor.asignarTicket(objTicket.intNumero, objGestor.lstTecnicos[intIndice].strId);
            }

            Console.WriteLine("Ticket asignado correctamente.");
            objTicket.mostrarBitacora();
        }

        static void escalarTicket(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            Console.Write("Nueva prioridad (Alta/Critica): ");
            string strNuevaPrioridad = Console.ReadLine();

            objGestor.escalarTicket(objTicket.intNumero, strNuevaPrioridad);
            Console.WriteLine("Ticket escalado correctamente.");
            objTicket.mostrarBitacora();
        }

        static void registrarError(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            Console.Write("Tipo de error (Software/Hardware/Red/Usuario): ");
            string strTipo = Console.ReadLine();

            Console.Write("Descripcion del error: ");
            string strDescripcion = Console.ReadLine();

            Console.Write("Impacto (Bajo/Medio/Alto/Critico): ");
            string strImpacto = Console.ReadLine();

            objTicket.registrarError(strTipo, strDescripcion, strImpacto);
            Console.WriteLine("Error registrado correctamente.");
            objTicket.mostrarBitacora();
        }

        static void resolverTicket(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            Console.Write("Solucion aplicada: ");
            string strSolucion = Console.ReadLine();

            objGestor.resolverTicket(objTicket.intNumero, strSolucion);
            Console.WriteLine("Ticket resuelto correctamente.");
            objTicket.mostrarBitacora();
        }

        static void cerrarTicket(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);

            objGestor.cerrarTicket(objTicket.intNumero);
            Console.WriteLine("Ticket cerrado correctamente.");
            objTicket.mostrarBitacora();
        }

        static void consultarBitacora(GestorTicket objGestor)
        {
            Ticket objTicket = solicitarTicket(objGestor);
            objTicket.mostrarBitacora();
        }

        static Ticket solicitarTicket(GestorTicket objGestor)
        {
            Console.Write("Ingrese numero de ticket: ");
            int intNumero = int.Parse(Console.ReadLine() ?? "0");
            return objGestor.buscarTicket(intNumero);
        }
    }
}
