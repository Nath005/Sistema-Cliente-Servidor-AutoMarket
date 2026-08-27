-- Crear la base de datos
CREATE DATABASE AutoMarket;
GO

USE AutoMarket;
GO

-- Crear tabla CategoriaVehiculo
CREATE TABLE CategoriaVehiculo (
    IdCategoria INT PRIMARY KEY,
    NombreCategoria VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500) NULL
);
GO

-- Crear tabla Vendedor
CREATE TABLE Vendedor (
    IdVendedor INT PRIMARY KEY,
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    NombreCompleto VARCHAR(200) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    FechaIngreso DATE NOT NULL,
    Telefono VARCHAR(15) NULL
);
GO

-- Crear tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY,
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    NombreCompleto VARCHAR(200) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    FechaRegistro DATE NOT NULL,
    Activo BIT DEFAULT 1
);
GO

-- Crear tabla Vehiculo
CREATE TABLE Vehiculo (
    IdVehiculo INT PRIMARY KEY,
    Marca VARCHAR(100) NOT NULL,
    Modelo VARCHAR(100) NOT NULL,
    Ano INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    IdCategoria INT NOT NULL,
    Estado CHAR(1) NOT NULL CHECK (Estado IN ('N', 'U')),
    FOREIGN KEY (IdCategoria) REFERENCES CategoriaVehiculo(IdCategoria)
);
GO

-- Crear tabla Sucursal
CREATE TABLE Sucursal (
    IdSucursal INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(300) NOT NULL,
    Telefono VARCHAR(15) NULL,
    IdVendedor INT NOT NULL,
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdVendedor) REFERENCES Vendedor(IdVendedor)
);
GO

-- Crear tabla VehiculoxSucursal
CREATE TABLE VehiculoxSucursal (
    IdSucursal INT NOT NULL,
    IdVehiculo INT NOT NULL,
    Cantidad INT NOT NULL DEFAULT 0,
    PRIMARY KEY (IdSucursal, IdVehiculo),
    FOREIGN KEY (IdSucursal) REFERENCES Sucursal(IdSucursal),
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculo(IdVehiculo)
);
GO

-- Crear tabla Venta
CREATE TABLE Venta (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdSucursal INT NOT NULL,
    IdVehiculo INT NOT NULL,
    FechaVenta DATETIME NOT NULL,
    Monto DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (IdSucursal, IdVehiculo) REFERENCES VehiculoxSucursal(IdSucursal, IdVehiculo)
);
GO

