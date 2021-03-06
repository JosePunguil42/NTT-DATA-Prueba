USE [master]
GO
/****** Object:  Database [Banca]    Script Date: 20/3/2022 22:33:24 ******/
CREATE DATABASE [Banca]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'[Banca]', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Banca.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'[Banca]_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Banca_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Banca].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Banca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Banca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Banca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Banca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Banca] SET ARITHABORT OFF 
GO
ALTER DATABASE [Banca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Banca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Banca] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Banca] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Banca] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Banca] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Banca] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Banca] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Banca] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Banca] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Banca] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Banca] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Banca] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Banca] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Banca] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Banca] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Banca] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Banca] SET RECOVERY FULL 
GO
ALTER DATABASE [Banca] SET  MULTI_USER 
GO
ALTER DATABASE [Banca] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Banca] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Banca] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Banca] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Banca] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Banca', N'ON'
GO
ALTER DATABASE [Banca] SET QUERY_STORE = OFF
GO

USE [Banca]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 04/11/2022 23:30:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[cl_id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[cl_identificacion] [varchar](30) NOT NULL,
	[cl_contrasenia] [varchar](30) NOT NULL,
	[cl_estado] [bit] NOT NULL,
	[cl_nombre] [varchar](30) NOT NULL,
	[cl_genero] [varchar](30) NOT NULL,
	[cl_edad] [varchar](3) NOT NULL,
	[cl_direccion] [varchar](50) NULL,
	[cl_telefono] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[cl_id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 04/11/2022 23:30:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[cu_numero_cuenta] [varchar](30) NOT NULL,
	[cu_id_cliente] [int] NOT NULL,
	[cu_tipo] [varchar](30) NOT NULL,
	[cu_estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cu_numero_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 04/11/2022 23:30:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[mo_id_movimiento] [int] IDENTITY(1,1) NOT NULL,
	[mo_numero_cuenta] [varchar](30) NOT NULL,
	[mo_fecha] [datetime] NOT NULL,
	[mo_tipo_movimiento] [varchar](30) NOT NULL,
	[mo_saldo_inicial] [money] NOT NULL,
	[mo_movimiento] [money] NOT NULL,
	[mo_saldo_disponible] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[mo_id_movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (2, N'1720477346', N'1234', 1, N'Jose Lema', N'Masculino', N'22', N'Otavalo sn y principal', N'098254785')
INSERT [dbo].[Clientes] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (3, N'1720477347', N'5678', 1, N'Marianela Montalvo', N'Masculino', N'22', N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[Clientes] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (4, N'1720477348', N'1245', 1, N'Juan Osorio', N'Femenino', N'20', N'13 junio y Equinoccial', N'098874587')
SET IDENTITY_INSERT [dbo].[Clientes] OFF
INSERT [dbo].[Cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo], [cu_estado]) VALUES (N'225487', 3, N'Corriente', 1)
INSERT [dbo].[Cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo], [cu_estado]) VALUES (N'478758', 2, N'Ahorro', 1)
INSERT [dbo].[Cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo], [cu_estado]) VALUES (N'495878', 4, N'Ahorros', 1)
INSERT [dbo].[Cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo], [cu_estado]) VALUES (N'496825', 3, N'Ahorros', 1)
INSERT [dbo].[Cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo], [cu_estado]) VALUES (N'585545', 2, N'Corriente', 1)
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (10, N'225487', CAST(N'2022-04-11T16:13:18.597' AS DateTime), N'Credito', 100.0000, 600.0000, 700.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (12, N'225487', CAST(N'2022-04-11T16:23:19.717' AS DateTime), N'Credito', 700.0000, 600.0000, 1300.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (13, N'225487', CAST(N'2022-04-11T16:23:38.017' AS DateTime), N'Credito', 1300.0000, 600.0000, 1900.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (14, N'496825', CAST(N'2022-04-11T16:27:01.713' AS DateTime), N'Debito', 540.0000, -540.0000, 0.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (16, N'496825', CAST(N'2022-04-11T16:41:55.663' AS DateTime), N'Credito', 0.0000, 540.0000, 540.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (17, N'496825', CAST(N'2022-04-11T16:45:45.827' AS DateTime), N'Debito', 540.0000, -100.0000, 440.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (18, N'496825', CAST(N'2022-04-11T17:19:28.000' AS DateTime), N'Debito', 440.0000, -100.0000, 340.0000)
INSERT [dbo].[Movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimiento], [mo_saldo_disponible]) VALUES (19, N'225487', CAST(N'2022-04-11T17:20:43.440' AS DateTime), N'Credito', 1900.0000, 600.0000, 2500.0000)
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD FOREIGN KEY([cu_id_cliente])
REFERENCES [dbo].[Clientes] ([cl_id_cliente])
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD FOREIGN KEY([mo_numero_cuenta])
REFERENCES [dbo].[Cuentas] ([cu_numero_cuenta])
GO
