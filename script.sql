USE [master]
GO
/****** Object:  Database [Tetris]    Script Date: 03.12.2022 14:33:35 ******/
CREATE DATABASE [Tetris]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tetris', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Tetris.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Tetris_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Tetris_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Tetris] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tetris].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tetris] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tetris] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tetris] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tetris] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tetris] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tetris] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tetris] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tetris] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tetris] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tetris] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tetris] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tetris] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tetris] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tetris] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tetris] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Tetris] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tetris] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tetris] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tetris] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tetris] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tetris] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tetris] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tetris] SET RECOVERY FULL 
GO
ALTER DATABASE [Tetris] SET  MULTI_USER 
GO
ALTER DATABASE [Tetris] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tetris] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tetris] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tetris] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tetris] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tetris] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tetris', N'ON'
GO
ALTER DATABASE [Tetris] SET QUERY_STORE = OFF
GO
USE [Tetris]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 03.12.2022 14:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 03.12.2022 14:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Score] [int] NOT NULL,
 CONSTRAINT [PK_Games_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 03.12.2022 14:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Amount] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03.12.2022 14:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NULL,
 CONSTRAINT [PK_UsersId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_UserId]
GO
USE [master]
GO
ALTER DATABASE [Tetris] SET  READ_WRITE 
GO