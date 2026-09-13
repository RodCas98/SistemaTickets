# Sistema de Tickets de Soporte Tecnico

Proyecto de diseno y desarrollo de un sistema de tickets de soporte tecnico corporativo: diagramas UML (flujo, casos de uso, clases) y su implementacion en C# y Java.

## Contenido

- **Diagramas de flujo**: diagrama de flujo del ciclo de vida del ticket (Abierto -> Asignado -> Resuelto -> Cerrado; el escalamiento sube la prioridad sin cambiar el estado)
- **Diagramas de caso de uso**: Crear Ticket, Asignar Ticket, Resolver Ticket, Escalar Ticket, Generar Metricas, Consultar Bitacora
- **Diagramas de clase**: diagrama de clases del sistema
- **TicketsSoporte**: proyecto en C# (.NET), con las clases del sistema en la carpeta `logica`
- **BlueJ_Proyecto**: version en Java del mismo modelo de clases, para el entorno BlueJ

## Casos de uso

1. **Crear Ticket**: el solicitante reporta un problema (titulo, descripcion, categoria, prioridad); el sistema numera el ticket e incluye la asignacion automatica.
2. **Asignar Ticket**: el gestor asigna (o reasigna) el ticket al tecnico disponible con menor carga que pueda atender la categoria (misma especialidad o General), o de forma manual a un tecnico especifico por codigo.
3. **Resolver Ticket**: el tecnico registra la solucion aplicada; el propio ticket valida que este Asignado antes de resolverlo.
4. **Escalar Ticket**: sube la prioridad del ticket a Critica y deja constancia del motivo; ocurre manualmente o automaticamente al registrar un error de impacto Alto o Critico.
5. **Generar Metricas**: el administrador obtiene indicadores de tickets por estado, por prioridad, tickets escalados y carga por tecnico.
6. **Consultar Bitacora**: el solicitante o el tecnico buscan un ticket por numero y revisan el historial completo de eventos registrados.

## Autor

RodCas98
