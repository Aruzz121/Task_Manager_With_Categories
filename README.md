# Task_Manager_With_Categories

----------------------------------------------------
Team Members:
* Luis Antonio Casillas de la Cruz
* Alejandro Real Lopez
* Jesus Eduardo Romero Isiordia
* Angel Michel Vela Flores
----------------------------------------------------
# Challenge 3 — Task Manager with Categories

## 📌 Scenario
A to-do application that allows users to organize activities by category. This challenge introduces relational data, DTO mapping, and filtered queries.

## 🚀 Key Requirements

### 🗂️ Data Models
Two entities with a **1:N relationship** — one Category contains many Tasks.
* **Task:** `(Id, Title, Status, CategoryId)`
* **Category:** `(Id, Name)`

### 🔍 Filtered GET Endpoint
The `GET` endpoint for tasks must support optional query parameters to **filter by `CategoryId` or by `Status`** (Pending / Completed). Both filters should work independently.

### ✅ Service Validation
Before saving a new task, the service must **verify that the provided `CategoryId` actually exists** in the database. Return a meaningful error if it does not.

### 📦 DTO Mapping
Never expose raw entities. Return a `TaskDTO` that includes the **category name as a string** instead of just the foreign key ID — this is a core API design best practice.
------------------------------------------------------------------------------------------------------------------------

## 👥 Equipo de Desarrollo y Asignación de Tareas


### 💻 Frontend & Consumo de API
**Responsable:** Luis Antonio Casillas de la Cruz
* **Desarrollo de Interfaz:** Creación de la vista principal de la aplicación para visualizar, agregar y organizar tareas por categoría.
* **Filtros Dinámicos:** Implementación de controles en la UI para consumir el *Filtered GET Endpoint*, permitiendo al usuario filtrar por `CategoryId` o `Status` (Pendiente/Completado).
* **Manejo de Errores:** Renderizado de alertas en pantalla si falla la validación del servidor (ej. cuando se intenta guardar una categoría inexistente).
* **Integración DTO:** Consumo y mapeo en el cliente del `TaskDTO` para mostrar los datos limpios (el nombre de la categoría en texto en lugar del ID).

### 🗄️ Modelado de Datos & Base de Datos
**Responsable:** Alejandro Real López
* **Arquitectura de BD:** Implementación de los *Data Models* (tablas `Task` y `Category`).
* **Relaciones:** Configuración de las restricciones y llaves foráneas para garantizar la relación **1:N** entre Categorías y Tareas.
* **Optimización de Consultas:** Desarrollo de las consultas necesarias a la base de datos para el correcto funcionamiento de los filtros de manera eficiente y escalable.
* **Datos Iniciales:** Creación de scripts o *seeders* para poblar la base de datos con información base.

### ⚙️ Backend, Lógica de Negocios & Soporte Técnico
**Responsable:** Jesús Eduardo Romero Isiordia
* **Service Validation:** Programación de la lógica que verifica la existencia del `CategoryId` en la base de datos antes de guardar una nueva tarea, retornando los errores correspondientes.
* **DTO Mapping:** Configuración de la capa de transformación para evitar la exposición de entidades crudas, asegurando que los controladores devuelvan el `TaskDTO` estructurado.
* **Soporte de Integración:** Resolución de bloqueos técnicos (troubleshooting) y puente entre el desarrollo del frontend y el esquema de base de datos.

### 🔎 Control de Calidad (QA) & Supervisión
**Responsable:** Ángel Michel Vela Flores
* **Revisión de Requerimientos:** Supervisión general del avance del equipo para garantizar que se cumplan al 100% las rúbricas y reglas de negocio solicitadas por el profesor, coordinando los esfuerzos de los integrantes.
* **Pruebas de Backend y Endpoints:** Verificación de que el *Filtered GET Endpoint* funcione independientemente con los parámetros `CategoryId` y `Status`, y comprobación rigurosa de que la API solo devuelva DTOs (sin exponer entidades crudas).
* **Pruebas de Frontend (UI/UX):** Validación de la interfaz de usuario, asegurando que el consumo de la API, los filtros dinámicos y el manejo de errores operen sin fallos.
* **Auditoría de Base de Datos:** Confirmación de que las relaciones 1:N estén correctamente establecidas por Alejandro y que la integridad de los datos se mantenga al registrar nuevas tareas.





