USE [master]
GO
/****** Object:  Database [DuAnPhanCung]    Script Date: 5/6/2017 10:46:25 AM ******/
CREATE DATABASE [DuAnPhanCung]
GO
USE [DuAnPhanCung]
GO
/****** Object:  Table [dbo].[Benh]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Benh](
	[idBenh] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](500) NULL,
	[idTV] [int] NULL,
	[NgayDang] [date] NULL,
	[NguoiDang] [nvarchar](500) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[HinhAnh] [nvarchar](max) NULL,
 CONSTRAINT [PK_Benh] PRIMARY KEY CLUSTERED 
(
	[idBenh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BenhVeNam]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BenhVeNam](
	[idbenhvenam] [int] IDENTITY(1,1) NOT NULL,
	[idcaytrong] [int] NULL,
	[Loai] [nvarchar](100) NULL,
	[DocTo] [nvarchar](100) NULL,
	[DauHieuNhanBiet] [nvarchar](100) NULL,
	[CachPhongNgua] [nvarchar](100) NULL,
	[CachKhacPhuc] [nvarchar](100) NULL,
 CONSTRAINT [PK_BenhVeNam] PRIMARY KEY CLUSTERED 
(
	[idbenhvenam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CayTrong]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CayTrong](
	[idcaytrong] [int] IDENTITY(1,1) NOT NULL,
	[TenCayTrong] [nvarchar](100) NULL,
	[MuaTrong] [nvarchar](100) NULL,
 CONSTRAINT [PK_CayTrong] PRIMARY KEY CLUSTERED 
(
	[idcaytrong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DichBenhPhoBien]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichBenhPhoBien](
	[idbenhphobien] [int] IDENTITY(1,1) NOT NULL,
	[idcaytrong] [int] NULL,
	[TenBenh] [nvarchar](100) NULL,
	[TacNhanGayBenh] [nvarchar](100) NULL,
	[DauHieuNhanBiet] [nvarchar](100) NULL,
	[CachPhongNgua] [nvarchar](100) NULL,
	[CachKhacPhuc] [nvarchar](100) NULL,
 CONSTRAINT [PK_DichBenhPhoBien] PRIMARY KEY CLUSTERED 
(
	[idbenhphobien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Form]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Form](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[idTV] [int] NULL,
	[thoigian] [datetime] NULL,
	[NhietDo1] [float] NULL,
	[NhietDo2] [float] NULL,
	[Nhietdo3] [float] NULL,
	[Gas] [int] NULL,
	[TGoff] [int] NULL,
	[Tgon] [int] NULL,
 CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NguoiLam]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiLam](
	[idNguoiLam] [int] IDENTITY(1,1) NOT NULL,
	[TenNguoiLam] [nvarchar](100) NULL,
	[SoDienThoai] [int] NULL,
	[CMND] [int] NULL,
	[NgayCong] [int] NULL,
	[ViecLam] [nvarchar](100) NULL,
	[TrangThai] [int] NULL,
 CONSTRAINT [PK_NguoiLam] PRIMARY KEY CLUSTERED 
(
	[idNguoiLam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhanBon]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanBon](
	[idPhanBon] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](500) NULL,
	[idTV] [int] NULL,
	[NgayDang] [date] NULL,
	[NguoiDang] [nvarchar](500) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[HinhAnh] [nvarchar](max) NULL,
 CONSTRAINT [PK_PhanBon] PRIMARY KEY CLUSTERED 
(
	[idPhanBon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Phanhoi]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phanhoi](
	[idPhanHoi] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NULL,
	[SDT] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[NoiDung] [nvarchar](500) NULL,
 CONSTRAINT [PK_PhanHoi] PRIMARY KEY CLUSTERED 
(
	[idPhanHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhanTich]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanTich](
	[idPhanTich] [int] IDENTITY(1,1) NOT NULL,
	[TienGiong] [decimal](18, 0) NULL,
	[TienCong] [decimal](18, 0) NULL,
	[TienPhan] [decimal](18, 0) NULL,
	[SoNgay] [int] NULL,
	[GiaBan] [decimal](18, 0) NULL,
	[LoiNhuan] [decimal](18, 0) NULL,
 CONSTRAINT [PK_PhanTich] PRIMARY KEY CLUSTERED 
(
	[idPhanTich] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThanhVien]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhVien](
	[idTV] [int] IDENTITY(1,1) NOT NULL,
	[MaSudung] [nvarchar](100) NULL,
	[TaiKhoan] [nvarchar](100) NULL,
	[MatKhau] [nvarchar](100) NULL,
	[TenDayDu] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[SoDienThoai] [int] NULL,
	[TrangThai] [int] NULL,
	[Quyen] [int] NULL,
 CONSTRAINT [PK_ThanhVien] PRIMARY KEY CLUSTERED 
(
	[idTV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThuocTruSau]    Script Date: 5/6/2017 10:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuocTruSau](
	[idThuocTruSau] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](500) NULL,
	[idTV] [int] NULL,
	[NgayDang] [date] NULL,
	[NguoiDang] [nvarchar](500) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[HinhAnh] [nvarchar](max) NULL,
 CONSTRAINT [PK_ThuocTruSau] PRIMARY KEY CLUSTERED 
(
	[idThuocTruSau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[BenhVeNam]  WITH CHECK ADD  CONSTRAINT [FK_BenhVeNam_CayTrong] FOREIGN KEY([idcaytrong])
REFERENCES [dbo].[CayTrong] ([idcaytrong])
GO
ALTER TABLE [dbo].[BenhVeNam] CHECK CONSTRAINT [FK_BenhVeNam_CayTrong]
GO
ALTER TABLE [dbo].[DichBenhPhoBien]  WITH CHECK ADD  CONSTRAINT [FK_DichBenhPhoBien_CayTrong] FOREIGN KEY([idcaytrong])
REFERENCES [dbo].[CayTrong] ([idcaytrong])
GO
ALTER TABLE [dbo].[DichBenhPhoBien] CHECK CONSTRAINT [FK_DichBenhPhoBien_CayTrong]
GO
ALTER TABLE [dbo].[Form]  WITH CHECK ADD  CONSTRAINT [FK_Form_ThanhVien] FOREIGN KEY([idTV])
REFERENCES [dbo].[ThanhVien] ([idTV])
GO
ALTER TABLE [dbo].[Form] CHECK CONSTRAINT [FK_Form_ThanhVien]
GO
USE [master]
GO
ALTER DATABASE [DuAnPhanCung] SET  READ_WRITE 
GO
