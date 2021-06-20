# CRUDConLogin-CSharp

//Program
	- Ejecución del método App

//Procedimientos
	- Métodos comunes a toda la aplicación

//Validaciones
	- Métodos de validación de datos

//Visualización
	- Métodos de navegación y visualización de la app

//Usuarios
	- Contiene matrizUsuarios y los métodos CRUD correspondientes a dicha matriz. 
	- Método OcultarPass: Se agrega en esta clase porque hace refencia a la identidad del usuario

//Stock
	- Contiene matrices libros y precioCantidad y los métodos CRUD correspondientes a dichas matrices

//Ventas
	- Contiene matrices matrizCarro y facturaCompra
	- matrizCarro se utiliza para crear un carrito de compra que permite individualizar las compras
	- facturaCompra se utiliza para crear la factura de cada compra
	- Modifica las cantidades en la matriz precioCantidad de Stock

//Facturación
	- Contine matriz acumuladoFacturas que almacena todas las facuras creadas en Ventas
	- Métodos de impresión de datos de la matriz acumuladoFacturas