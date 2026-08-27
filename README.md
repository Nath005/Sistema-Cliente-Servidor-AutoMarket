# Sistema Cliente-Servidor AutoMarket

Sistema de escritorio desarrollado en C# bajo una arquitectura de capas y comunicación Cliente-Servidor mediante Sockets TCP, conectado a una base de datos relacional en SQL Server.

### Características:
* **Arquitectura en Capas:** Separación de responsabilidades (`CapaDatos`, `CapaLogica`, `CapaEntidades`).
* **Comunicación de Red:** Arquitectura Cliente-Servidor utilizando Sockets TCP.
* **Gestión de Base de Datos:** Integración con SQL Server (Procedimientos almacenados y consultas estructuradas).
* **Interfaz Gráfica:** Formularios en Windows Forms (`.NET Framework 4.8`).

### Estructura del Repositorio:
* **Cliente/**: Aplicación de escritorio para la interfaz de usuario.
* **EmpresaAutoMarket/**: Servidor TCP encargado de procesar y gestionar las peticiones.
* **CapaDatos/**: Conexión y ejecución de consultas con la base de datos.
* **CapaLogica/**: Reglas de negocio del sistema.
* **CapaEntidades/**: Clases y modelos de datos.
* **SQLQuery1.sql/**: Script de creación y configuración de la base de datos.
* **Manual de uso_Sistema AutoMarket.pdf**: Documentación detallada del funcionamiento del sistema.

<img width="741" height="649" alt="image" src="https://github.com/user-attachments/assets/fd1e652c-90b4-447e-9654-b5275a8b97e7" />

<img width="527" height="347" alt="image" src="https://github.com/user-attachments/assets/d7fa24a5-6ae7-4603-9f04-cc15ac91768e" />

<img width="639" height="412" alt="Captura de pantalla (612)" src="https://github.com/user-attachments/assets/1e3f1065-d75f-4f54-9a30-e91d5c319679" />

<img width="649" height="409" alt="Captura de pantalla (613)" src="https://github.com/user-attachments/assets/47a98b95-fab7-498b-9719-432d61dcc091" />

<img width="760" height="390" alt="Captura de pantalla (616)" src="https://github.com/user-attachments/assets/f60f626b-0cd4-4d49-9094-95330b6f80cf" />

<img width="784" height="472" alt="Captura de pantalla (617)" src="https://github.com/user-attachments/assets/396fbc97-6659-49c6-8f88-d0e5caf50bb7" />


