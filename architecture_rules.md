# Reglas de Arquitectura - Nexus Bilding

Utilizo una arquitectura basada en **Onion Architecture** y **Domain-Driven Design (DDD)**, donde el dominio constituye el núcleo de la aplicación y contiene exclusivamente las reglas de negocio, entidades, eventos y contratos. Las dependencias siempre apuntan hacia el dominio, evitando que la lógica de negocio dependa de detalles de infraestructura, bases de datos o tecnologías externas.

La solución se organiza mediante módulos funcionales y no únicamente por responsabilidad técnica. Cada módulo mantiene su propia estructura dentro de las diferentes capas (Domain, Application, Infrastructure, Presentation, Tests, etc.), permitiendo una alta cohesión, bajo acoplamiento y una evolución independiente de cada contexto de negocio.

En la capa de aplicación utilizo casos de uso, comandos y consultas para orquestar los procesos de negocio sin contener reglas de dominio. La infraestructura implementa los contratos definidos por el dominio para persistencia, autenticación, almacenamiento e integraciones externas. Las capas de presentación (API, Web o aplicaciones cliente) actúan únicamente como puntos de entrada al sistema.

- Las vistas deben tener *breadcrumbs* de navegación y un título descriptivo claro (`nx-page-header`).
- Toda lista/tabla paginada debe mostrar spinners de carga (`nx-spinner`) si los datos están demorando y estados vacíos (`nx-empty`) claros.
- **Regla de Ordenamiento (Sorting):** TODAS las columnas de TODAS las tablas deben ser ordenables (sortable). No se debe dejar ninguna columna sin la capacidad de ordenar. Esto incluye vistas existentes y cualquier vista futura.
- Toda funcionalidad que registre operaciones principales debe incluir un botón de **Exportación Excel** enriquecido (con `ExcelExportService`) para ofrecer reportes Premium.

Adicionalmente, aplico una organización modular estricta en toda la solución. Ningún componente se ubica directamente en la raíz de una capa; todo pertenece a un módulo explícito, incluso cuando inicialmente contiene pocos archivos. Este enfoque facilita la escalabilidad, el mantenimiento y la eventual separación de módulos en componentes o servicios independientes si el crecimiento del proyecto lo requiere.

### Estilo de Codificación y Comentarios
- **Comentarios Restringidos:** Está prohibido comentar el código de manera excesiva.
- **Ubicación:** Si un comentario es estrictamente necesario, debe ser muy simple y ubicarse siempre en la parte superior del bloque o línea a comentar. Nunca se deben colocar comentarios a la derecha del código.
- **Claridad:** Se prefiere el código autodocumentado mediante nombres claros de variables y métodos por encima de las explicaciones textuales.

