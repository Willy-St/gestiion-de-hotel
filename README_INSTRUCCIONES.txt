SISTEMA WEB DE RESERVA DE HABITACIONES
======================================

TECNOLOGÍAS
- Visual Studio 2022/2026
- C# y ASP.NET Core MVC
- .NET 8
- Entity Framework Core 8
- SQL Server
- Razor Views, HTML, CSS y JavaScript

1. REQUISITOS
- Visual Studio con la carga de trabajo "ASP.NET y desarrollo web".
- SDK de .NET 8.
- SQL Server instalado y en ejecución.
- SQL Server Management Studio (SSMS), recomendado.

2. CREAR LA BASE DE DATOS
- Abre SQL Server Management Studio.
- Conéctate a tu instancia local (normalmente localhost).
- Abre y ejecuta SQL/01_Crear_Base_Datos.sql.
- El script crea la base ReservaHotelDB.
- Las tablas y los datos de prueba se crean automáticamente al iniciar el proyecto por primera vez.

3. ABRIR EL PROYECTO
- Descomprime ReservaHotel_Web.zip.
- Abre ReservaHotel.sln con Visual Studio.
- Espera a que Visual Studio restaure los paquetes NuGet.
- Abre ReservaHotel.Web/appsettings.json.
- Revisa la conexión:
  Server=localhost;Database=ReservaHotelDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true

Si tu instancia es SQL Express, cambia Server=localhost por Server=.\SQLEXPRESS.
Si usas LocalDB, cambia Server=localhost por Server=(localdb)\MSSQLLocalDB.

4. EJECUTAR
- Selecciona ReservaHotel.Web como proyecto de inicio.
- Presiona F5 o el botón Ejecutar.
- La primera ejecución crea las tablas y carga los datos de demostración.
- Se abrirá el navegador en https://localhost:7180 o http://localhost:5180.

5. CUENTA DE PRUEBA
Usuario: recepcionista
Contraseña: Hotel123*

La contraseña se almacena con hash, no como texto plano.
Esta cuenta es solo para pruebas académicas. Cámbiala antes de usar el sistema en un entorno real.

6. PRUEBA RECOMENDADA
A. Inicia sesión.
B. Entra a Clientes y verifica CLI-001, CLI-002 y CLI-003.
C. Entra a Habitaciones y confirma que las habitaciones estén Disponibles.
D. Selecciona Generar reserva.
E. Elige un cliente.
F. Coloca llegada mañana y salida dos días después (2 días).
G. Selecciona habitación 101 (S/ 90 por día) y escribe el nombre del huésped.
H. Selecciona también habitación 102 (S/ 140 por día) y escribe el huésped.
I. El total esperado es S/ 460.00:
   (90 x 2) + (140 x 2) = 460.
J. Presiona Grabar reserva.
K. Debe aparecer un número RES-00000X y el detalle de la reserva.
L. En Habitaciones, 101 y 102 deben aparecer como Reservado.
M. Intenta generar otra reserva con esas habitaciones: ya no deben aparecer en la lista disponible.

7. ALCANCE IMPLEMENTADO
- Inicio de sesión de recepcionista.
- Panel principal.
- Listado y registro de clientes.
- Listado de habitaciones y cambio manual de estado para pruebas.
- Búsqueda de clientes desde el formulario de reserva.
- Selección de una o varias habitaciones disponibles.
- Nombre del huésped por habitación.
- Cálculo de días, importe por habitación y monto total.
- Generación automática de número de reserva.
- Registro de reserva y detalle.
- Actualización del estado de habitación a Reservado.
- Validaciones de fechas, cliente y disponibilidad.

NOTAS IMPORTANTES
- El caso de uso entregado no define cobros, pasarela de pago, IGV ni checkout; por eso no se agregaron.
- La aplicación usa datos de demostración iniciales. Puedes agregar clientes desde el módulo Clientes.
- El proyecto se entrega como primera versión funcional académica. Antes de producción se requiere ampliar permisos, auditoría, recuperación de contraseña y reglas de liberación de habitaciones.
- Si Visual Studio muestra error de conexión, revisa el nombre de la instancia SQL Server en appsettings.json.
