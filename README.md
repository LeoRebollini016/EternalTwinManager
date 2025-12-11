# EternalTwin Manager – Daily Tasks Bot for Brute/EternalTwin

Automated bot escrito en **.NET 8** para ejecutar tareas diarias en Brute (EternalTwin). Implementa Clean Architecture, CQRS, MediatR y pipelines de `HttpClient` para orquestar sesiones, obtener datos de usuario y ejecutar combates automáticos con control de errores y reintentos.

El objetivo principal es automatizar:
- Login de múltiples cuentas
- Obtención y persistencia de datos del usuario
- Ejecución secuencial de combates automáticos para cada brute
- Manejo inteligente de errores, retiros y reintentos controlados
- Detección y reporte de brutos sin combates

---

## ✨ Características principales
- .NET 8, Clean Architecture y separación de responsabilidades
- CQRS con MediatR para casos de uso claros y testables
- HttpClientFactory, pipelines y deserialización segura con `record`
- Fight Pipeline con reintentos configurables y backoff
- Detección automática de brutos con 0 combates y lógica configurable
- Logs detallados mediante `ILogger`
- Mecanismos fail-safe para evitar fallos por cadenas de errores

---

## Requisitos previos
- .NET 8 SDK instalado
- Credenciales válidas para las cuentas Brute/EternalTwin
- Windows (para la UI WinForms) o entorno compatible para ejecutar el servicio/CLI
- Variables de entorno o archivo `appsettings.json` con configuración sensible

---

## Instalación rápida
1. Clonar el repositorio:
   git clone <repo-url>
2. Restaurar paquetes y compilar:
   dotnet restore
   dotnet build -c Release
3. Ejecutar (modo consola/servicio):
   dotnet run --project src/EternalTwinManager.Console
   O abrir `WinForms/` en Visual Studio y ejecutar la UI.

---

## Configuración (ejemplo)
Se recomienda usar `appsettings.json` o variables de entorno para datos sensibles.

Ejemplo mínimo `appsettings.json`:
```json
{
  "ApiBaseUrl": "https://api.ejemplo.com",
}
````

Variables importantes:
- `ApiBaseUrl`: URL base de la API
- `ConcurrentAccounts`: número de cuentas procesadas en paralelo
- `Fight:MaxRetries` y `Fight:RetryDelayMs`: control de reintentos
- `Fight:MaxConsecutiveFailures`: fail-safe por fallos consecutivos
Advertencia: No aumentar variables sin considerar límites de la API.
---

## Gestión de cuentas (Upgrade)
- Formato esperado (ejemplo JSON):
```json
[
  {
    "user": "user1",
    "password": "pass1"
  },
  {
    "user": "user2",
    "password": "pass2"
  }
]
````
- No almacenar contraseñas en texto plano en repositorios.
- Soporta cargar cuentas desde fichero, variable de entorno o UI WinForms.

---

## Arquitectura
- Presentación: `WinForms/` y/o proyecto consola para ejecuciones programadas
- Application: Casos de uso basados en CQRS y MediatR
- Domain: Entidades inmutables (records) y reglas del dominio
- Infrastructure: HttpClientFactory, clone API externa, adaptadores API
- Tests: Unitarios y de integración para pipelines y servicios críticos (Upgrade)

Patrones relevantes:
- MediatR para handlers y pipelines
- HttpClientFactory + DelegatingHandlers para reintentos, logging y resiliencia
- Clean Architecture para aislar lógica de negocio de infraestructuras externas

---

## Pipeline de combate (Fight Pipeline)
- Inicio de sesión y obtención de token por cuenta
- Recuperación de combates restantes del brute
- Ejecución del combate con reintentos y backoff
- Monitorización de fallos consecutivos y activación de fail-safe

---

## Logs y troubleshooting
- Logs en consola y archivo (configurable)
- Niveles: Debug, Information, Warning, Error
- Rutas comunes para investigar fallos:
  - Errores de autenticación (credenciales inválidas / IP bloqueada)
  - Rate limiting o 429 Responses (aumentar retry/backoff)
  - Errores de deserialización (cambiar modelos si la API cambió)
- Habilitar `Debug` para trazar pipelines y handlers

---

## Ejecución de pruebas (Upgrade)
- Ejecutar tests unitarios:
  dotnet test tests/EternalTwinManager.Tests
- Añadir tests para nuevos `Handlers`, `Pipelines` y adaptadores de API

---

## Preguntas frecuentes / Troubleshooting breve
- ¿Por qué una cuenta no realiza combates?
  - Verificar energía/cooldown del brute y logs de decisión.
- ¿Se puede ejecutar en paralelo?
  - Sí, controlar `ConcurrentAccounts` en configuración.
- ¿Cómo evitar bans por automatización?
  - EternalTwin no tiene bans por automatización debido a que es hecho por los fans de Motion-Twin. 
  **Sin embargo, se recomienda no exceder los límites de la API y usar delays razonables.**

---

## Licencia y contacto
- Añadir licencia en `LICENSE` (por defecto MIT si procede).
- Contacto / autor: mantener en archivo `AUTHORS.md` o en la cabecera del repo.

---

## Cambios y roadmap
- UI para carga de multicuentas.
- Soporte para tareas diarias de Dino.
- Integración del servicio de marcar torneos en Brute.
- Análisis y mejora en la selección de oponentes.
- Telemetría y dashboards para estado global de la ejecución.
- Dashboard para brutos.
