TollControl

Proyecto personal (no vinculado a la facultad) para aprender .NET en profundidad y construir un portfolio para mostrar a empresas. TollControl v1.0 es un sistema de gestión de telepeaje: administra usuarios, clientes, cuentas, vehículos, TAGs, estaciones, vías, tarifas, transacciones de peaje, movimientos de saldo, incidencias y auditoría.

📁 Estructura del repositorio
TollControll/
├── TollControl.sln    # Solución .NET (raíz)
├── server/             # Backend — ASP.NET Core Web API (.NET 8)
├── client/             # Frontend — React + TypeScript + Vite
└── .gitignore          # Combinado (bin/, obj/, .vs/, node_modules/, dist/, etc.)

El repo es único, con server/ y client/ como carpetas hermanas. Visual Studio 2022 se usa solo para server/ (no gestiona client/, ya que no es un proyecto .NET). VS Code se usa para client/.

🛠️ Stack tecnológico
Capa	Tecnología
Backend	ASP.NET Core Web API sobre .NET 8 (LTS)
Frontend	React + TypeScript + Vite
Base de datos	PostgreSQL (administrada visualmente con pgAdmin)
ORM	Entity Framework Core — enfoque Code First

Enfoque de base de datos: las entidades en C# y el DbContext son la fuente de verdad. La base se genera con migraciones de EF Core (dotnet ef migrations add / dotnet ef database update); no se ejecutan scripts SQL a mano.

Paquetes NuGet en server (fijados en 8.0.10, compatibles con .NET 8 — la versión 10.x por defecto rompe por requerir .NET 10):

Npgsql.EntityFrameworkCore.PostgreSQL
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
🗂️ Modelo de datos

14 entidades: User, Role, Customer, Account, Vehicle, VehicleCategory, Tag, Station, Lane, Tariff, TollTransaction, BalanceMovement, AuditLog, Incident.

Decisiones de diseño cerradas:

Customer tiene una única Account (relación 1:1) en esta versión.
Un Vehicle puede tener como máximo un Tag en estado ACTIVO (constraint de negocio).
BalanceMovement se vincula a TollTransaction mediante FK nullable y única (relación 1:0..1, no 1:1 estricta), ya que BalanceMovement también existe para otros tipos (RECARGA, AJUSTE, REVERSO) que no vienen de un peaje.
TollTransaction no guarda vehicle_id; se obtiene navegando Tag → Vehicle, evitando redundancia.
Incident todavía no tiene atributos definitivos — se van a definir según casos de uso concretos.

En el DbContext (TollControlDbContext, carpeta Data/) ya está toda la configuración Fluent API: índices únicos parciales para las dos reglas de negocio mencionadas, DeleteBehavior.Restrict por defecto (SetNull solo en Incident), y montos tipados como numeric(12,2).

🚧 Estado actual

Backend

Proyecto server creado y corriendo (Web API con controllers, HTTPS).
Las 14 entidades (Models/) y el DbContext (Data/) ya están copiados y compilan sin errores.
Pendiente: registrar el DbContext en Program.cs (AddDbContext<TollControlDbContext> con UseNpgsql y cadena de conexión desde user-secrets), y correr dotnet ef migrations add InitialCreate + dotnet ef database update para crear la base tollcontrol en PostgreSQL.

Frontend

Proyecto client creado con Vite (React + TypeScript + ESLint), corre con npm run dev en localhost:5173.
Pendiente: conectar con el backend (configurar CORS en server y probar un fetch de prueba).
▶️ Cómo correr el proyecto
Backend
bash
cd server
dotnet restore
dotnet ef database update
dotnet run
Frontend
bash
cd client
npm install
npm run dev
🎯 Objetivo del proyecto

Proyecto personal orientado a profundizar el manejo del ecosistema .NET de punta a punta (diseño, modelo de datos, API, base de datos, frontend) y a tener un caso completo y prolijo para mostrar en entrevistas laborales.

👤 Autor

Martín Zanandrea GitHub
