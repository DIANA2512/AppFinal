
# APLICACIÓN MÓVIL
# TOMA DE PEDIDOS

El siguiente proyecto muestra el desarrollo de una Aplicación Móvil que permite el registro y búsqueda de pedidos, además  cuenta con un módulo para registro de clientes.

Video demostrativo: https://www.youtube.com/watch?v=O8Utbfiy8_M&t=7s

Realizado por: Diana Bonilla
## 1. Herramientas de Desarrollo
Visual Studio 2019

La Aplicación Móvil se desarrolla bajo el lenguaje Xamarin el cual está incluido en la plataforma de Visual Studio 2019.

SQL Server 2018

La Aplicación Móvil se conecta a un servidor para extraer los datos.

## 2. Estructura y Arquitectura
* Arquitectura
La arquitectura del Sistema Web se muestra en la siguiente imagen
* Estructura
Se utiliza el patrón arquitectónico MVC, que permite tener la administración del sistema de una
manera más eficiente y poder corregir errores o adaptar más capas, en la siguiente imagen muestra los directorios utilizados en el modelo, vista o controlador.

## 3. Funcionalidades Principales
Durante el desarrollo de la Aplicación Móvil se pudo obtener varios requerimientos y funcionalidades, algunas de las más importantes son las siguientes:

### Iniciar Sesión
El usuario puede iniciar sesión usando el nombre de usuario y contraseña, el sistema valida las credenciales ingresadas y permite el ingreso al sistema.
Estas credenciales serán las mismas que se han creado en el Sistema Web.
![iniciar Sesión](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/iniciar%20sesion.png?raw=true)

### Registar un Pedido
El usuario ingresa a la Aplicación Móvil, se carga el módulo para el registro del pedido, en él tiene que ingresar el número de cédula del cliente, registrar el producto y la cantidad deseada.
![registrar pedido](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Registro%20de%20pedido.png?raw=true)

### Modificar un Pedido
Para modificar un pedido el usuario debe ingresar el número de pedido, ahí puede eliminar o aumentar algún producto deseado.
![modificar pedido](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Modificar%20pedido.png?raw=true)

### Eliminar Producto
Para eliminar un producto el usuario debe presionar sobre el mismo, ahí aparece un mensaje que permite confirmar o denegar la eliminación del producto.
![eliminar producto](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Eliminar%20Producto.png?raw=true)

### Anular Pedido
Con el número de pedido se puede buscar y cambiar el estado ha anulado, cuando el pedido ha sido cambiado ha anulado el total se pone en cero, productos y cantidades se borran y no se puede cambiar el estado del pedido ni agregar más productos,  antes de anular el pedido aparece un mensaje para confirmar o denegar la anulación del pedido.
![confirmar pedido](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Anular%20pedido.png?raw=true)![confirmar anulacion](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Confirmar%20anular%20pedido.png?raw=true)

### Confirmar Pedido
Con el número de pedido se puede buscar y cambiar el estado ha entregado, cuando el pedido se cambia a un estado de entregado se calcula el total y no se puede agregar más productos,  antes de cerrar el pedido aparece un mensaje para confirmar o denegar la solicitud del pedido.
![anular pedido](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Confirmar%20pedido.png?raw=true)![confirmar solicitud](https://github.com/DIANA2512/AppFinal/blob/main/Capturas/Confirmar%20solicitar%20pedido.png?raw=true)

### Registrar Cliente
El módulo para registrar el cliente, para ello tiene que llenar todos los campos obligados en el formulario, entre ellos el nombre, apellido, cédula, correo y dirección; el estado se carga automáticamente en activo.

## 4. Anexos
En el siguiente link se encuentra toda la información como Manual Técnico y video explicativo
[Documentacion](https://github.com/DIANA2512/Documentacion_Tesis.git)

  