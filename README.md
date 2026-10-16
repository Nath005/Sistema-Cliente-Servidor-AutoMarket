# Sistema Cliente-Servidor AutoMarket

Sistema de escritorio desarrollado en C# bajo una arquitectura de capas y comunicación Cliente-Servidor mediante Sockets TCP, conectado a una base de datos relacional en SQL Server.

### Características:
* **Arquitectura en Capas:** Separación de responsabilidades (`CapaDatos`, `CapaLogica`, `CapaEntidades`).
* **Comunicación de Red:** Arquitectura Cliente-Servidor utilizando Sockets TCP.
* **Gestión de Base de Datos:** Integración con SQL Server (Procedimientos almacenados y consultas estructuradas).
* **Interfaz Gráfica:** Formularios en Windows Forms (`.NET Framework 4.8`).

### Estructura del Repositorio:
* `Cliente/`: Aplicación de escritorio para la interfaz de usuario.
* `EmpresaAutoMarket/`: Servidor TCP encargado de procesar y gestionar las peticiones.
* `CapaDatos/`: Conexión y ejecución de consultas con la base de datos.
* `CapaLogica/`: Reglas de negocio del sistema.
* `CapaEntidades/`: Clases y modelos de datos.
* `SQLQuery1.sql/`: Script de creación y configuración de la base de datos.
* `Manual de uso_Sistema AutoMarket.pdf`: Documentación detallada del funcionamiento del sistema.

<img width="562" height="494" alt="Captura de pantalla (693)" src="https://github.com/user-attachments/assets/96475a7d-b50c-4e85-87e0-1a43b0d895a1" />

<img width="527" height="347" alt="image" src="https://github.com/user-attachments/assets/d7fa24a5-6ae7-4603-9f04-cc15ac91768e" />

<img width="684" height="428" alt="Captura de pantalla (694)" src="https://github.com/user-attachments/assets/c8d941c2-68ed-4099-87ff-a73ec6121f73" />

<img width="705" height="448" alt="Captura de pantalla (695)" src="https://github.com/user-attachments/assets/f57d532f-dca1-41a9-959b-bab3fe5ac7db" />

<img width="760" height="390" alt="Captura de pantalla (616)" src="https://github.com/user-attachments/assets/f60f626b-0cd4-4d49-9094-95330b6f80cf" />

<img width="733" height="428" alt="Captura de pantalla (696)" src="https://github.com/user-attachments/assets/e82f16ff-90ca-42d8-92e1-40de6aa58e54" />


