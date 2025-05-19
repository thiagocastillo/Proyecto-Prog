<img alt="UCU" src="https://www.ucu.edu.uy/plantillas/images/logo_ucu.svg"
width="150"/>

# Universidad Católica del Uruguay

## Facultad de Ingeniería y Tecnologías

### Programación II

# Consigna proyecto 2025 - Primer semestre

<br>

Este semestre, desarrollaremos... ¡chatbots! 🤖

![Bender](https://media.giphy.com/media/mIZ9rPeMKefm0/giphy.gif)

## Contexto

Un chatbot o [bot conversacional](https://es.wikipedia.org/wiki/Bot_conversacional)
es un programa que simula mantener una conversación con una persona al proveer
respuestas automáticas a entradas hechas por el usuario.

Existen gran variedad de chatbots actualmente y varios *sabores*. Hay chatbots
que simplemente responden a comandos pre-establecidos, y otros que integran
algoritmos de [inteligencia artificial](https://es.wikipedia.org/wiki/Inteligencia_artificial)
para procesar los mensajes de los usuarios e [interpretar lo que se está diciendo](https://es.wikipedia.org/wiki/Procesamiento_de_lenguajes_naturales).

Algunas de las aplicaciones más conocidas que abren sus puertas al desarrollo de
chatbots —tienen API— son:

* Telegram
* Messenger
* Whatsapp
* Slack
* Discord

entre otras; y nos integraremos al menos a una de ellas.

## Roadmap

El proyecto se divide en varias entregas a lo largo del semestre, que se
detallan [más abajo](#entregas).

Cada entrega es una parte del proyecto que construye sobre la anterior. Al final
del semestre tendremos un conjunto de chatbots funcionales con los que podremos
conversar.

El chatbot a desarrollar será propuesto por los estudiantes, según se indica en
la sección [Propuestas](#propuestas).

La estructura del trabajo en el proyecto será la siguiente:

* [x] Kick-off
* [ ] Presentación de propuestas
* [ ] Evaluación docente de propuestas
* [ ] Votación de propuestas
* [ ] Lanzamiento de la propuesta elegida
* [ ] Primera entrega
* [ ] Segunda entrega
* [ ] Entrega final
* [ ] Defensa

## Propuestas

Los equipos o sus integrantes podrán a presentar propuestas de chatbots para
desarrollar. El chatbot debe permitir un juego interactivo multijugador —por
turnos— de al menos dos jugadores, que no requiera de una interfaz gráfica, es
decir, las jugadas se describen mediante un mensaje como en el ajedrez o la
batalla naval.

Utiliza [este formulario](https://forms.office.com/r/yG6UeqJWRx) para enviar tus
propustas. ¡Pueden enviar todas las propuestas que quieran!

Puedes ver la fecha límite para entrega de propuestas en la sección
[Entregas](#entregas).

## Historias de Usuario - Age of Empires Simplificado
**Configuración y Creación**
1.	Como jugador, quiero poder crear una nueva partida especificando el tamaño del mapa y la cantidad de jugadores, para personalizar mi experiencia de juego.
   
Criterios de aceptación:
* Configurar mapa aleatorio de 100x100.
* Configurar partida entre 2 jugadores.

2.	Como jugador, quiero elegir una civilización con características únicas, para aprovechar sus ventajas estratégicas.
   
Criterios de aceptación:
* Disponer de al menos 3 civilizaciones diferentes
* Cada civilización debe tener al menos 2 bonificaciones únicas
* Cada civilización debe tener una unidad especial
  
3.	Como jugador, quiero comenzar con un centro cívico y algunos aldeanos, para iniciar inmediatamente la recolección de recursos.

Criterios de aceptación:
* Iniciar con 1 centro cívico (admite hasta 10 aldeanos)
* Iniciar con 3 aldeanos
* Recibir una cantidad inicial de recursos (100 de alimento, 100 de madera)

**Gestión de Recursos**

4.	Como jugador, quiero ordenar a mis aldeanos recolectar diferentes tipos de recursos, para expandir mi economía.

Criterios de aceptación:
* Poder recolectar 4 tipos de recursos: madera, alimento, oro y piedra
* Cada recurso debe tener una tasa de recolección diferente
* Los aldeanos deben trasladar los recursos al edificio de almacenamiento más cercano

5.	Como jugador, quiero construir edificios específicos para almacenar recursos, para optimizar la recolección.

Criterios de aceptación:
* Poder construir almacenes de recursos (molino, granja, depósitos de oro, piedra, madera)
* La proximidad del almacén debe afectar la eficiencia de recolección
* Los almacenes deben tener un límite de capacidad
  
6.	Como jugador, quiero visualizar la cantidad de recursos disponibles, para planificar mis estrategias.

Criterios de aceptación:
* Ver la cantidad actual de cada recurso
* Recibir alertas cuando un recurso esté por agotarse
* Visualizar la tasa de recolección por tipo de recurso

**Construcción y Desarrollo**

7.	Como jugador, quiero construir edificios en ubicaciones específicas del mapa, para expandir mi base.

Criterios de aceptación:
* Poder seleccionar una ubicación en el mapa para construir
* La construcción debe requerir ciertos recursos
* Mostrar el tiempo de construcción restante
  
8.	Como jugador, quiero crear diferentes tipos de edificios con funciones específicas, para desarrollar mi civilización.

Criterios de aceptación:
* Construir casas para aumentar la población máxima
* Construir cuarteles para entrenar unidades militares

**Unidades y Combate**

9.	Como jugador, quiero entrenar diferentes tipos de unidades militares, para defender mi base y atacar a mis oponentes.

Criterios de aceptación:
* Poder crear al menos 3 tipos de unidades (infantería, arqueros, caballería)
* Cada unidad debe tener estadísticas únicas (ataque, defensa, velocidad)
* Las unidades deben tener diferentes costos y tiempos de creación

10.	Como jugador, quiero mover mis unidades por el mapa usando comandos simples, para explorar y posicionarme estratégicamente.

Criterios de aceptación:
* Mover unidades a coordenadas específicas
* Poder seleccionar múltiples unidades para moverlas juntas
* Recibir retroalimentación si el destino es inaccesible

11.	Como jugador, quiero ordenar a mis unidades atacar a unidades o edificios enemigos, para debilitar a mis oponentes.

Criterios de aceptación:
* Poder seleccionar un objetivo para atacar
* Mostrar información del combate (daño causado/recibido)
* Implementar un sistema de ventaja por tipo de unidad (ej: arqueros efectivos contra infantería)

**Economía y Población**

12.	Como jugador, quiero entrenar más aldeanos para mejorar mi economía, pero necesito suficientes casas para mantener mi población.

Criterios de aceptación:
* Cada casa aumenta el límite de población en 5 unidades (puede tener hasta 20 aldeanos y hasta 30 unidades militares)
* No poder crear nuevas unidades si se ha alcanzado el límite de población
* Mostrar claramente la población actual/máxima

**Victoria y Objetivos**

13.	Como jugador, quiero destruir los centros cívicos enemigos para ganar la partida por dominación militar.

Criterios de aceptación:
* Cada jugador pierde si su último centro cívico es destruido
* Recibir notificación cuando un enemigo está cerca de ser derrotado
* Mostrar un resumen al finalizar la partida

**Interfaz y Comandos**

14.	Como jugador, quiero usar comandos intuitivos en la línea de comandos para interactuar con el juego.

Criterios de aceptación:
* Los comandos deben seguir una estructura consistente (verbo + objeto + parámetros)
* Proporcionar ayuda y ejemplos de comandos
* Ofrecer autocompletado o sugerencias

15.	Como jugador, quiero ver un mapa simplificado del juego en ASCII, para visualizar la disposición del terreno y unidades.

Criterios de aceptación:
* Mostrar un mapa con símbolos que representen unidades, edificios y recursos
* Poder desplazarse por el mapa
* Distinguir elementos por jugador usando colores o símbolos

**BONUS**

Como jugador, quiero guardar la partida y continuarla más tarde, para jugar en múltiples sesiones.

Criterios de aceptación:
* Guardar el estado completo del juego
* Cargar una partida guardada
* Ver la lista de partidas guardadas


## Entregas

> [!WARNING]
> **Importante:** Las entregas serán hasta las 23:59 del día indicado.

| Instancia | Fecha | Entregables |
| --- | --- | --- |
| Kick-off | 21 abril, primera clase de la semana 5 | |
| Presentación de propuestas | Hasta el 30 de abril | Completar [este formulario](https://forms.office.com/r/yG6UeqJWRx) |
| Evaluación docente de propuestas | 30 de abril al 5 de mayo | |
| Votación de propuestas | 5 de mayo | |
| Lanzamiento de proyectos | 7 de mayo | |
| [Primera entrega] | 2 de junio | Tarjetas CRC, diagrama de clases, código de clases de dominio + [fachada](https://refactoring.guru/design-patterns/facade) |
| [Segunda entrega] | 23 de junio | Entrega de [user stories](https://es.wikipedia.org/wiki/Historias_de_usuario) implementadas. Las historias de usuario deberán ser implementadas mediante [casos de prueba](https://en.wikipedia.org/wiki/Test_case) usando la fachada. |
| [Entrega final] | 7 de julio | Bot funcionando y entregables según se indica en la [consigna de la entrega](./Entregas/Entrega3.md) |
| Defensa | 7 al 9 de julio |
