![image](https://github.com/Jesusm1229/Prueba-Tecnica-Jesus-Medina-Juego-Ruleta/assets/48733708/f7b13a2c-9707-4b3f-84eb-076f4b77a160)

![image](https://github.com/Jesusm1229/Prueba-Tecnica-Jesus-Medina-Juego-Ruleta/assets/48733708/3ee1a796-04da-4062-8bfc-24dc0e915bac)


## Win Wheel Prueba técnica para Unilink Jesús Medina

## Acceso
https://unilinkwinwheel.netlify.app/

## Overview

Este juego permite disfrutar de una partida del juego de la ruleta asignando un score y una apuesta.

## Features

- **Muestra de Resultados**: El sistema le mostrará a los usuarios si ganaron o perdieron y modificará su score
- **Winning History**: El usuario podrá consultar los últimos resultados arrojados por la ruleta.
- **Top Winner**: Los usuarios con mayor puntaje aparecerán en el ranking mundial. 
- **Guardar Score**: Una vez hecha una apuesta, el usuario podrá guardar su score resultante con una cuenta
- **Sistema de usuarios**: Los usuarios registrados disfrutan de un almacenamiento de score, así como también la posibilidad de aparecer en el ranking mundial
- **Sistema de Tokens**: Para garantizar la integridad de los usuarios registrados, se utiliza un accessToken otorgado mediante JWT para los aquellos que inicien sesión. El token ajustará un refrescamiento automático menor a 5min para garantizar la protección del mismo.
- **Local Storage**: El proyecto aprovecha la funcionalidad del localStorage para almacenar datos relevantes del usuario mientras se desarrollad la partida y ha iniciado sesión.

## Estructura
- **Base de Datos**: SQL Server. Aprovechando la integración entre ASP NET CORE y SQL Server, el sistema aloja a los jugadores y scores en tablas únicas con relación Uno a Uno basado en sus identificadores
- **Backend ASP. NET CORE Web API**: REST API conteniendo los controlador y funcionamiento del sistema. Se utilizó la arquitectura cebolla para una orgazinación eficiente. Así como también se cumplieron con las buenas prácticas de desarrollo implementando CORS, JWT, Dto, Distribución en repositorio, Identity para control y manejo de los usuarios. La API es consultable desde Swagger. 
- **Frontend Vue 3**: Desarrollado mediante SPA Vue, el sistema aprovecha la funcionalidad reactiva de Vue para destacar su reendirazo y mantenimiento de sesión

## Cómo jugar
Para jugar basta con asignar una categoría, posteriormente seleccionar tu bet, y finalmente tu posición.  
Según la categoría seleccionada serán los requisitos de números o color. Por ejemplo, 'Straight', indica que se apostará por un número y color específico; 'Color', un color; o los pares o impares de un color
dado o a un número y color específicos

## Hosting y enlaces
- **Backend**: Se utilizó Somee como sitio para alojar la Base de datos y la Web API, se añadió un certificado SSL como requisito de seguridad. Consultable: https://unilinkwinwheel.somee.com/swagger/index.html
- **Frontend**: Se utilizó la plataforma de Netlify, la cual ofrece un deployment sencillo. Consultable acá: https://unilinkwinwheel.netlify.app/




![image](https://github.com/Jesusm1229/Prueba-Tecnica-Jesus-Medina-Juego-Ruleta/assets/48733708/e767c49e-be15-484b-81fe-3787de8d7a81)
![image](https://github.com/Jesusm1229/Prueba-Tecnica-Jesus-Medina-Juego-Ruleta/assets/48733708/9b34025f-f989-4235-b084-88a7e5baf452)
![image](https://github.com/Jesusm1229/Prueba-Tecnica-Jesus-Medina-Juego-Ruleta/assets/48733708/1708d812-25df-43ff-9548-5efcafcc154d)







