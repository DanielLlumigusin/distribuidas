

-- Seleccionar la base de datos
USE SeguridadSistema;
GO

-- Creación de la tabla de usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Identificador único
    Name NVARCHAR(100) NOT NULL, -- Nombre del usuario
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Nombre de usuario único
    PasswordHash NVARCHAR(255) NOT NULL, -- Contraseña cifrada
    Rol NVARCHAR(20) CHECK (Rol IN ('Admin', 'Editor', 'Viewer')) NOT NULL, -- Roles basados en RBAC
    Status NVARCHAR(20) DEFAULT 'Active' CHECK (Status IN ('Active', 'Inactive', 'Blocked')), -- Estado del usuario
    SessionAttempts INT DEFAULT 0, -- Intentos de inicio de sesión fallidos
    CreatedAt DATETIME DEFAULT GETDATE(), -- Fecha de creación
    UpdatedAt DATETIME DEFAULT GETDATE() -- Última actualización
);
GO

-- Creación de la tabla para la gestión de sesiones
CREATE TABLE Sesiones (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Identificador único
    UserId INT NOT NULL, -- ID del usuario
    SessionToken NVARCHAR(255) NOT NULL UNIQUE, -- Token único para la sesión
    CreatedAt DATETIME DEFAULT GETDATE(), -- Fecha de inicio de sesión
    ExpiresAt DATETIME NOT NULL, -- Fecha de expiración de la sesión
    CONSTRAINT FK_Sesiones_Usuarios FOREIGN KEY (UserId) REFERENCES Usuarios(Id) -- Relación con la tabla Usuarios
);
GO

-- Creación de la tabla de logs de auditoría
CREATE TABLE Auditoria (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Identificador único
    UserId INT NULL, -- ID del usuario (si aplica)
    EventType NVARCHAR(50) NOT NULL CHECK (EventType IN ('LoginSuccess', 'LoginFailure', 'PasswordChange', 'RoleChange', 'AccountLock', 'AccountActivation')), -- Tipo de evento
    EventDescription NVARCHAR(MAX), -- Descripción del evento
    EventTime DATETIME DEFAULT GETDATE(), -- Fecha y hora del evento
    IpAddress NVARCHAR(45), -- Dirección IP del usuario
    CONSTRAINT FK_Auditoria_Usuarios FOREIGN KEY (UserId) REFERENCES Usuarios(Id) -- Relación con la tabla Usuarios
);
GO

-- Creación de la tabla para tokens de seguridad (CSRF y recuperación de contraseñas)
CREATE TABLE TokensSeguridad (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Identificador único
    UserId INT NOT NULL, -- ID del usuario
    Token NVARCHAR(255) NOT NULL UNIQUE, -- Token único
    TokenType NVARCHAR(20) NOT NULL CHECK (TokenType IN ('CSRF', 'PasswordRecovery')), -- Tipo de token
    ExpiresAt DATETIME NOT NULL, -- Fecha de expiración del token
    CONSTRAINT FK_TokensSeguridad_Usuarios FOREIGN KEY (UserId) REFERENCES Usuarios(Id) -- Relación con la tabla Usuarios
);
GO
