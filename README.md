# Sistema de Tickets de Soporte Tecnico

Proyecto de diseno y desarrollo de un sistema de tickets de soporte tecnico corporativo: diagramas UML (flujo, casos de uso, clases) y su implementacion en C#.

## Contenido

- **Diagramas de flujo**: diagrama de flujo del ciclo de vida del ticket (Abierto -> Asignado -> Resuelto -> Cerrado)
- **Diagramas de caso de uso**: Crear Ticket, Registrar Error, Resolver Ticket, Cerrar Ticket, Generar Resumen de Control
- **Diagramas de clase**: diagrama de clases del sistema
- **TicketsSoporte**: proyecto en C# (.NET), con las clases del sistema en la carpeta `logica`

## Modelo

- `Usuario` (abstracta): clase base con id, nombre y correo.
- `Tecnico` : `Usuario` — especialidad, nivel de experiencia y carga de tickets asignados.
- `Solicitante` : `Usuario` — departamento y extension.
- `Ticket`: numero, titulo, descripcion, categoria, prioridad, estado, solicitante, tecnico asignado y bitacora.
- `Bitacora`: historial de eventos de un ticket.
- `FlujoTicket`: valida las transiciones de estado permitidas.
- `GestorTickets`: administra tecnicos, solicitantes y tickets; asigna automaticamente el tecnico con menor carga segun la categoria.

## Diagramas

Los archivos `.drawio` se abren con [draw.io](https://app.diagrams.net/) o en VS Code con la extension **Draw.io Integration**.

## Autor

RodCas98
