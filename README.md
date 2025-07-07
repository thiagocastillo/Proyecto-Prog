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
| [Primera entrega](Entregas/Entrega1.md) | 2 de junio | Tarjetas CRC, diagrama de clases, código de clases de dominio + [fachada](https://refactoring.guru/design-patterns/facade) |
| [Segunda entrega](Entregas/Entrega2.md) | 23 de junio | Entrega de [user stories](https://es.wikipedia.org/wiki/Historias_de_usuario) implementadas. Las historias de usuario deberán ser implementadas mediante [casos de prueba](https://en.wikipedia.org/wiki/Test_case) usando la fachada. |
| [Entrega final](Entregas/Entrega3.md) | 7 de julio | Bot funcionando y entregables según se indica en la [consigna de la entrega](./Entregas/Entrega3.md) |
| Defensa | 7 al 9 de julio |

   
<br>

# DOCUMENTACION PATRONES Y PRINCIPIOS DE DISEÑO:

El diseño del sistema implementa de forma rigurosa los principios de la programación orientada a objetos, en particular los principios SOLID y los patrones GRASP.

## Fachada (Facade):

La clase JuegoFachada centraliza y simplifica la interacción con la lógica del juego, ocultando la complejidad de las operaciones internas. Esto es un claro ejemplo del patrón Fachada.


## Herencia y Polimorfismo:

Las unidades militares como Infanteria, GuerreroJaguar, Arquero y Caballería implementan la interfaz común IUnidadMilitar, además de heredar de clases base (Infanteria, etc.). Esto aprovecha el polimorfismo, permitiendo tratar todas las unidades de forma uniforme desde la fachada (JuegoFachada) o el motor (Motor.cs), sin importar su tipo específico.

Esto cumple con el Principio de Sustitución de Liskov (LSP), cualquier clase que herede de otra puede reemplazar a su clase base sin alterar la lógica del programa.

Además, la herencia permite reutilizar código común, como la lógica de movimiento y combate, y redefinir solo lo necesario con override / new, manteniendo cada clase simple y enfocada.

## Interfaz (Interface):

En este proyecto se utiliza el concepto de interfaces como IUnidad, IEdificio, IAlmacenamiento, IRecolector e IUnidadMilitar, lo que colabora con el polimorfismo, el principio de abstracción y separación de responsabilidades y especialmente el principio de inversión de dependencias (Dependency Inversion Principle del grupo SOLID). 

De esta forma, las interfaces permiten que las clases dependan de abstracciones y no de implementaciones concretas, lo que reduce en gran medida el acoplamiento entre las mismas y facilita tanto el mantenimiento del codigo como la testeabilidad del mismo.

Por ejemplo, el uso de la interfaz IUnidad permite que cualquier tipo de unidad (Aldeano, Arquero, Caballería, etc.) pueda ser utilizada de forma uniforme desde el motor del juego y la fachada. Esto significa que si mañana se agrega una nueva unidad especial como una Ballesta, no será necesario modificar el código que ya existe, sino simplemente agregar nuevas clases que implementen la interfaz. Este diseño sigue el Open/Closed Principle (abierto para extensión, cerrado para modificación), otro principio esencial de los patrones SOLID vistos en clase.

Asimismo, IAlmacenamiento define operaciones genéricas para depósitos de recursos (Recursos, CapacidadMaxima, Eficiencia, etc.), lo cual permite que edificios como CentroCivico, Granja, Molino y Depositos compartan comportamiento sin necesidad de duplicar código. Además, gracias al polimorfismo, un aldeano puede recolectar recursos y depositarlos sin necesidad de conocer el tipo exacto de edificio, siempre y cuando implemente IAlmacenamiento.

Desde el enfoque de patrones de diseño, este uso de interfaces se alinea también con el patrón Strategy, ya que permite variar comportamientos de forma intercambiable por ejemplo, las unidades militares implementan AtacarUnidad() con distintas lógicas según su tipo (bonificaciones, daño), pero todas exponen la misma operación. También está presente el patrón de composición, ya que tanto Jugador como Partida manipulan colecciones de IUnidad y IEdificio de forma identica, delegando comportamiento sin necesidad de conocer los detalles internos de cada tipo concreto.


## Encapsulamiento:

Las propiedades privadas y los métodos públicos o protegidos en las clases aseguran que los datos internos estén protegidos y solo sean accesibles a través de métodos concretos.

Por ejemplo, en el tiempo de construcción, solo se permite leer su estado (TiempoRestante, EstaCompleta) desde fuera, pero el avance del tiempo está controlado internamente. Por otro lado, las clases Jugador, Mapa y Unidad gestionan sus colecciones y propiedades de forma controlada, previniendo efectos colaterales o manipulaciones indebidas.


## Single Responsibility Principle (SRP):

Cada clase encapsula una sola responsabilidad clara y única, por ejemplo, Mapa se encarga de la representación y administración del entorno de juego, mientras que Jugador gestiona sus propios recursos, población, edificios y unidades. Casa representa un edificio específico, etc.

Se respeta el Principio de Responsabilidad Única (SRP) en todas las clases, por ejemplo, Tiempo Construccion se ocupa exclusivamente de calcular los tiempos de construcción, y Motor se encarga de recibir comandos y delegar la ejecución a la lógica de fachada. Esto evita acoplamientos innecesarios y mejora la claridad del código.

## Principio Expert:

El principio de Expert se aplica al asignar responsabilidades a las clases que tienen la información suficiente para cumplirlas, por ejemplo, Jugador calcula el total de recursos porque posee tanto los recursos individuales como los contenidos en edificios de almacenamiento. RecursoNatural conoce su propia tasa de recolección y agotamiento, y Unidad sabe cómo moverse o atacar basándose en sus atributos internos.

## Excepciones y Diseño por Contrato:

En nuestro proyecto, utilizamos Excepciones y Diseño por Contrato especialmente para validar precondiciones, y sabemos que quizás no fue de la forma más exhaustiva en todos los métodos de nuestras clases. Hay excepciones para validar precondiciones y algunas postcondiciones.

Ejemplo de aplicación de los conceptos:

Precondición validada en AgregarJugadorAPartida (JuegoFachada): 
  
  if(_partidaActual.Jugadores.Any(j => j.Nombre.Equals(nombreJugador, StringComparison.OrdinalIgnoreCase)))
    throw new InvalidOperationException($"Ya existe un jugador con el nombre '{nombreJugador}'.");

En este caso concreto, por diseño por contrato, no se puede agregar dos veces el mismo jugador con el mismo nombre. Se lanza una excepción si no se cumple dicha precondición.

Por otro lado, en el caso del método Recolectar dentro de RecursoNatural.cs, se realiza la validación de la postcondición de si un recurso se encuentra agotado o no. En este caso, la postcondición es que haya recurso disponible, pero en caso contrario, se lanza una excepción.

if (EstaAgotado)
    throw new InvalidOperationException("El recurso está agotado.");


## Uso de Colecciones y LINQ:

Se utilizan colecciones genéricas (List <>, Dictionary <>) y expresiones LINQ como FirstOrDefault (), Where (), Any (), Select () y ToList (), para manipular datos de manera eficiente y expresiva.

Por ejemplo, la búsqueda de unidades en una coordenada específica se realiza de manera eficiente con Where y SelectMany. Por otro lado, la lógica de verificación del ganador (verificarGanador) o la validación de construcciones (any) emplea LINQ para evitar estructuras repetitivas.  


## ANÁLISIS PRINCIPIO SRP EN LA RELACIÓN EDIFICIO - POSICIÓN:

Continuando con lo ya documentado acerca del principio de diseño SRP (Single Responsibility Principle), una clase debe tener una sola razón para cambiar.
En este caso, nuestras clases DepositoMadera.cs, CentroCivico.cs, Granja.cs, Molino.cs, etc., que implementan IEdificio, IAlmacenamiento, actualmente tienen dos responsabilidades claramente diferenciables.

En primer lugar, estas tienen la responsabilidad funcional propias de cada una, que consiste en representar y gestionar los atributos y comportamientos de un edificio como por ejemplo, la vida, los recursos almacenados, la eficiencia, el tiempo de construcción, etc.

Por otro lado, estas tienen la responsabilidad de saber cual es su posicion geografica en el mapa actual de dicha partida. A traves de la clase Point, que almacena las coordenadas de donde se encuentra dicho edificio.


Estas dos responsabilidades no deberían convivir en la misma clase, ya que responden a distintas razones de cambio. Si cambia la lógica del juego, por ejemplo, cómo se calcula la eficiencia o el daño a edificios, entonces hay una razón para modificar la clase.
Pero si cambia la forma en que se representa o gestiona el mapa, por ejemplo, si se introduce una grilla con otro tamaño o comportamiento, eso también requeriría modificar las mismas clases, lo cual viola SRP.

Por lo tanto, luego de escuchar y conversar sobre la devolución de nuestro docente en esta segunda etapa del proyecto, decidimos documentar el por qué la posición no deberia estar en las clases que implementan IEdificio como una propiedad y decidimos documentar está observación reconociendo la violación del principio de diseño SRP en las clases ya mencionadas.


<br>


# NOTAS Y ACLARACIONES DEL EQUIPO:

Reflexion de equipo: (...)

Tiago: Durante la entrega y el desarrollo del proyecto, senti que lo que mas me costaba y desafiaba mas era el entender como las clases iban a interactuar entre ellas para lograr el resultado esperado, ademas de otros varios desafios como aplicar los conceptos aprendidos durante el curso, en el proyecto. Tambien, ya que en Programación 2 trabajamos con C#, me costo acomodarme con el lenguaje ya que solo habia programado en Pascal y en Python, pero con ayuda de compañeros experimentados y varias repasadas al lenguaje, le logre agarrar la mano, y no solo eso, sino que aprendi mas alla del lenguaje basico de C#. 
Durante el desarrollo del Proyecto, logre entender el lenguaje y la Programación Orientada a Objetos mas a fondo. Fui creciendo con algunas paginas web que voy a dejar al final de mi reflexion, y mirando la forma de programar y diferentes formas de encarar los problemas de mis compañeros experimentados, ya que considero que aprendo rapido mirando y observando a detalle como trabajan personas con cierta experiencia. Resumiendo, este Proyecto me ayudo mucho para expandir mi conocimiento, entender mejor, ver las capacidades y a hasta donde puede llegar la Programación.
Estas son algunas de las paginas que utilice para expandir mi conocimiento: 
- https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop => Microsoft Learn
- https://www.freecodecamp.org/news/how-to-use-oop-in-c-sharp/ => freeCodeCamp
- https://learn.microsoft.com/en-us/dotnet/csharp/ => Microsoft Learn