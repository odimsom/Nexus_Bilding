# Nexus Billing — Estándar Universal de Desarrollo y Negocio (V1.0)

Este documento constituye la ley absoluta para el desarrollo de Nexus Billing. Estas reglas son **transversales** y se aplican a cada pantalla, tabla, reporte y proceso del sistema, sin excepciones.

---

## 1. Filosofía de Desarrollo e Integridad

### 1.1. Uso Total del Nodo de BD
- **Sin Campos Mínimos:** Nunca se implementa una funcionalidad con "campos mínimos". Si el nodo de la base de datos define 40 campos, se habilitan los 40. Buscamos robustez y escalabilidad, no simplicidad técnica.
- **Efecto Dominó:** Si una acción en una tabla requiere afectar 20 tablas más para mantener la integridad (contabilidad, auditoría, históricos), se afectan las 20. Es imperativo que la información sobre a que falte.
- **Lógica de Flujo de Negocio:** Cada tarea se diseña pensando en departamentos: ¿Quién inicia el proceso? ¿Cómo le afecta al siguiente departamento? ¿Qué debe cambiar allá porque aquí se hizo esto?

### 1.2. Ciclo de Vida y Terminación
- **Finalización Obligatoria:** Una funcionalidad no se abandona hasta que el flujo de negocio sea circular y completo (incluyendo su visualización, guardado, afectación de tablas relacionadas y emisión de reportes). No se aceptan "esqueletos" funcionales.
- **Regla de Oro:** Las cosas solo se siguen cuando se terminan.

---

## 2. Experiencia de Usuario (UX) y Automatización

### 2.1. El Silencio de la Automatización
- **Silencio Obligatorio:** Queda prohibido informar al usuario en la interfaz que un campo es "automático" o "autogenerado". El sistema simplemente calcula el valor y lo presenta en el campo. El software debe sentirse inteligente, no explicativo.
- **Autollenado Proactivo:** Todo lo que sea deducible mediante relaciones (ej. traer condiciones de pago al seleccionar un cliente o proveedor) debe autollenarse al instante. El usuario solo debe introducir lo estrictamente nuevo.

### 2.2. Gestión de Secuencias (No.)
- **Vinculación a Secuencia:** Todo lo que funcione con identificadores (No.) debe estar atado a una secuencia. 
- **Asociación en Caliente:** Si al interactuar con un campo que requiere secuencia (ej. Contratos) esta no existe, el sistema debe exigir al usuario asociar o crear una en ese preciso momento antes de continuar.
- **Auto-generación:** Las secuencias se auto-generan en el primer registro si no han sido configuradas previamente por el usuario.

### 2.3. Navegación y Estructura
- **Drill-Through:** Cualquier dato relacionado en una vista debe ser un atajo visual (enlace) a su origen (ej. saltar a la ficha del cliente desde una orden).
- **Sidebar:** Las **Órdenes de Venta** y las **Órdenes de Servicio** se mantienen estrictamente separadas en el menú lateral, respetando sus naturalezas distintas.

---

## 3. Estándares Técnicos y de Diseño

### 3.1. Código y Estética
- **Cero Código Comentado:** Nunca se sube ni se mantiene código comentado bajo ningún concepto.
- **Nexus Design System:** Respeto estricto a la paleta de colores (*Ledger Emerald* para acciones, *Slate* para interfaces), tipografías y el uso obligatorio de **FactBoxes** para datos de resumen.
- **Reportes Profesionales:** Los reportes (`pdfmake`) deben tener un formato corporativo serio, alineación perfecta y representar la solidez de una herramienta empresarial de alto nivel. Nada de diseños infantiles o innecesariamente brillantes.

---

## 4. Estrategia Legal y Marca (White Label)

### 4.1. Neutralidad de Marca
- **Desvinculación Legal:** Nexus Billing no está certificado. Para evitar riesgos legales, el sistema debe ser visualmente neutral en su salida (documentos).
- **Identidad del Usuario Final:** Es obligatorio que cada empresa configure su propio logo y cabecera de texto. El software debe emitir documentos que representen legalmente al usuario, eliminando rastro de nuestra marca para proteger la integridad de la plataforma.

---

## 5. Reglas Específicas de Dominio (Transversales)

### 5.1. Ajustes de Inventario
- Los ajustes manuales son excepcionales. 
- El número de documento de ajuste debe ser estrictamente secuencial e impuesto por el sistema.
- La descripción del ajuste debe venir obligatoriamente de un motivo u orden de origen.
- La entrada ordinaria de stock siempre debe ser el resultado de una **Orden de Compra**.
