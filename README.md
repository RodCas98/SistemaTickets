# Sistema de Tickets de Soporte Tecnico

Proyecto de diseno y desarrollo de un sistema de tickets de soporte tecnico corporativo: diagramas UML (flujo, casos de uso, clases) y su implementacion en C# y Java.

## Contenido

- **Diagramas de flujo**: diagrama de flujo del ciclo de vida del ticket (Abierto -> Asignado -> Escalado -> Resuelto -> Cerrado)
- **Diagramas de caso de uso**: Crear Ticket, Asignar Ticket, Resolver Ticket, Escalar Ticket, Generar Metricas, Consultar Bitacora
- **Diagramas de clase**: diagrama de clases del sistema
- **TicketsSoporte**: proyecto en C# (.NET), con las clases del sistema en la carpeta `logica`
- **BlueJ_Proyecto**: version en Java del mismo modelo de clases, para el entorno BlueJ

## Casos de uso

1. **Crear Ticket**: el solicitante reporta un problema (titulo, descripcion, categoria, prioridad); el sistema numera el ticket e incluye la asignacion automatica.
2. **Asignar Ticket**: el gestor asigna (o reasigna) el ticket al tecnico con menor carga dentro de la especialidad, o de forma manual a un tecnico especifico.
3. **Resolver Ticket**: el tecnico registra la solucion aplicada; incluye el registro de errores en la bitacora y el cierre del ticket.
4. **Escalar Ticket**: el tecnico sube la prioridad del ticket y lo reasigna a un tecnico de mayor experiencia en la misma especialidad.
5. **Generar Metricas**: el administrador obtiene indicadores de tickets por estado, por prioridad, tickets escalados y carga por tecnico.
6. **Consultar Bitacora**: el solicitante o el tecnico buscan un ticket por numero y revisan el historial completo de eventos registrados.

## Autor

RodCas98
