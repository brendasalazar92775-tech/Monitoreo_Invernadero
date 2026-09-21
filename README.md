# Invernadero UPT

Aplicación para la **gestión y cuidado del invernadero de la Universidad Politécnica de Tecámac (UPT)**.

El proyecto integra una aplicación móvil con dispositivos IoT y servicios en la nube para permitir el monitoreo de variables ambientales y el control de elementos del invernadero.

---

## Descripción

**Invernadero UPT** es un sistema desarrollado para apoyar el monitoreo y cuidado de un invernadero universitario.

La solución combina:

* Aplicación móvil.
* Sensores ambientales.
* Microcontrolador.
* Base de datos en tiempo real.
* Sistema de autenticación.
* Control de dispositivos.

La aplicación permite consultar información del ambiente del invernadero y controlar determinados dispositivos desde una interfaz móvil.

---

## Objetivo

Desarrollar una herramienta tecnológica que facilite el monitoreo y cuidado del invernadero de la Universidad Politécnica de Tecámac mediante la integración de **IoT, desarrollo móvil y servicios en la nube**.

---

## Funcionalidades

### Monitoreo ambiental

La aplicación permite consultar datos obtenidos mediante sensores:

* Temperatura.
* Humedad.

Los datos son enviados a Firebase y posteriormente pueden ser consultados desde la aplicación.

### Control de dispositivos

El sistema permite controlar dispositivos conectados al microcontrolador.

Entre ellos:

* Bomba de agua.
* LED indicador.

El estado de los dispositivos puede visualizarse desde la aplicación.

### Registro de riego

El sistema registra información relacionada con el funcionamiento de la bomba de agua.

Se contempla el almacenamiento de:

* Hora de activación.
* Hora de registro.
* Estado de la bomba.

---

## Tecnologías utilizadas

| Tecnología                 | Uso                                         |
| -------------------------- | ------------------------------------------- |
| C#                         | Lenguaje de programación                    |
| .NET MAUI                  | Desarrollo de la aplicación multiplataforma |
| Visual Studio              | Entorno de desarrollo                       |
| Firebase Authentication    | Autenticación de usuarios                   |
| Firebase Realtime Database | Almacenamiento de datos en tiempo real      |
| ESP32                      | Microcontrolador                            |
| DHT11                      | Sensor de temperatura y humedad             |
| DS3231                     | Módulo de reloj en tiempo real              |
| Relay                      | Control de la bomba                         |

---

## Arquitectura del sistema

El proyecto está compuesto por tres partes principales:

```text
┌──────────────────────────────┐
│       INVERNADERO UPT        │
└───────────────┬──────────────┘
                │
                ▼
       ┌─────────────────┐
       │      ESP32       │
       └────────┬────────┘
                │
       ┌────────┴─────────┐
       │                  │
       ▼                  ▼
    DHT11               Relay
       │                  │
       │                  ▼
       │              Bomba de agua
       │
       ▼
Firebase Realtime Database
       │
       ▼
┌───────────────────────────┐
│       Aplicación MAUI     │
│                           │
│  Monitoreo                │
│  Control                  │
│  Registros                │
└───────────────────────────┘
```

---

## Componentes IoT

### ESP32

El ESP32 funciona como el controlador principal del sistema.

Se encarga de:

* Obtener información de los sensores.
* Enviar información a Firebase.
* Recibir o ejecutar acciones de control.
* Gestionar los dispositivos conectados.

### DHT11

El sensor **DHT11** permite obtener:

* Temperatura.
* Humedad relativa.

Configuración utilizada:

```text
DHT11
│
└── GPIO 4
```

### Relay

El módulo relay permite controlar la bomba de agua mediante una señal proveniente del ESP32.

Configuración utilizada:

```text
Relay
│
└── GPIO 23
```

### LED

El LED funciona como indicador visual del estado del sistema.

Configuración utilizada:

```text
LED
│
└── GPIO 2
```

## Firebase Realtime Database

La información del sistema se almacena en Firebase Realtime Database.

Una estructura simplificada es:

```text
monitoreo-invernadero
│
├── Sensor
│   ├── Temperatura
│   └── Humedad
│
├── led
│   └── estado
│
└── BombaInvernadero
    └── Hora
        └── registros
```

Ejemplo de datos del sensor:

```json
{
  "Sensor": {
    "Temperatura": 25.8,
    "Humedad": 40
  }
}
```

---

## Flujo de información

El funcionamiento general del sistema es:

```text
Sensor DHT11
      │
      ▼
     ESP32
      │
      ▼
Firebase Realtime Database
      │
      ▼
Aplicación .NET MAUI
      │
      ▼
Visualización de datos
```

Para el control:

```text
Aplicación MAUI
      │
      ▼
Firebase
      │
      ▼
ESP32
      │
      ▼
Relay
      │
      ▼
Bomba de agua
```

---

## Interfaz de la aplicación

La aplicación permite consultar información del invernadero desde un dispositivo móvil.

### Monitoreo

![Monitoreo](screenshots/Monitoreo_de_la_temperatura_y_humedad.jpeg)

Permite consultar valores como:

* Temperatura.
* Humedad.
* Estado de dispositivos.

## Estructura del proyecto

```text
Invernadero-UPT/
│
├── App/
├── Models/
├── Views/
├── ViewModels/
├── Services/
│
├── Resources/
│
├── screenshots/
│   ├── monitoreo.png
│   ├── control.png
│   └── registro-riego.png
│
├── .gitignore
├── App.xaml
├── App.xaml.cs
├── MauiProgram.cs
└── README.md
```

---

## Requisitos

Para ejecutar el proyecto se requiere:

* Visual Studio.
* .NET SDK compatible con la versión del proyecto.
* .NET MAUI.
* Una cuenta de Firebase.
* Proyecto configurado en Firebase.
* Dispositivo Android o emulador.

Para la parte IoT:

* ESP32.
* DHT11.
* Relay.
* DS3231.
* LED.
* Bomba de agua.
* Cables y fuente de alimentación adecuados.


## Aplicación de IoT

El proyecto permite aplicar conceptos de **Internet de las Cosas (IoT)** mediante la comunicación entre sensores, dispositivos físicos, servicios en la nube y una aplicación móvil.

```text
      MUNDO FÍSICO
           │
     ┌─────┴─────┐
     │           │
   DHT11       Bomba
     │           │
     └─────┬─────┘
           │
          ESP32
           │
           ▼
        Firebase
           │
           ▼
      Aplicación
        MAUI
```

---

## Beneficios del sistema

El proyecto busca facilitar:

* Monitoreo de las condiciones ambientales.
* Consulta de información en tiempo real.
* Control de dispositivos.
* Registro de actividades.
* Centralización de información.
* Apoyo al cuidado del invernadero.

---

## Materiales

Para el desarrollo del prototipo se utilizaron materiales disponibles y componentes destinados a la creación del sistema de monitoreo y control.

Entre los componentes principales se encuentran:

* ESP32.
* DHT11.
* DS3231.
* Relay.
* LED.
* Bomba de agua.
* Materiales reutilizados y/o donados.

---

## Estado del proyecto

**Versión:** 1.0.0

**Estado:** Proyecto académico / Prototipo

---

## Aprendizajes

Durante el desarrollo del proyecto se aplicaron conocimientos relacionados con:

* Desarrollo de aplicaciones móviles con .NET MAUI.
* Programación en C#.
* Desarrollo de interfaces.
* Firebase Realtime Database.
* Firebase Authentication.
* Internet de las Cosas (IoT).
* Comunicación entre dispositivos y servicios en la nube.
* Lectura de sensores.
* Control de dispositivos mediante ESP32.
* Manejo de datos en tiempo real.
* Control de versiones con Git y GitHub.

---

## Autora

**Brenda Salazar Mendiola**

Ingeniería en Software
Universidad Politécnica de Tecámac

