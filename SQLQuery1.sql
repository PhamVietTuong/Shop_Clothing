	--create database QuanLyQuanAo
use QuanLyQuanAo
--------------------

Drop table chitiethoadonban
Drop table chitiethoadonnhap
Drop table sanpham
Drop table hoadonban
Drop table hoadonnhap
Drop table nhacungcap
Drop table loaisanpham
Drop table khachhang
Drop table nhanvien
Drop table mau
Drop table size

go


create table nhanvien
(
	manv varchar(20) primary key,
	tennv nvarchar(100),
	phai nvarchar(10),
	ngaysinh datetime,
	diachi nvarchar(100),
	email varchar(100),
	phone varchar(24),
	trangthai int
)


create table khachhang
(
    makh varchar(20) primary key,
	tenkh nvarchar(100),
	phai nvarchar(10),
	diachi nvarchar(100),
	email nvarchar(100),
	phone nvarchar(24),
	trangthai int
)

create table loaisanpham
(
	maloai varchar(20) primary key,
	tenloai nvarchar(50),
	trangthai int,
	--mach varchar(20)
)

create table nhacungcap
(
	mancc varchar(20) primary key,
	tenncc nvarchar(100),
	tennganhang nvarchar(100),
	phone nvarchar(24),
	maloai varchar(20),
	trangthai int
)
create table sanpham
(
	masp varchar(100) primary key,
	tensp nvarchar(100),
	mancc varchar(20),
	--mach varchar(20),
	maloai varchar(20),
	mamau varchar(20),
	masize varchar(20),
	mota nvarchar(100),
	hinh varchar(30),
	soluong int,
	gianhap float,
	giaban float,
	trangthai int 
)
--create table chitietsanpham
--(
--	mactsp varchar(50),
--	masp varchar(100),
--	mamau varchar(20),
--	masize varchar(20),
--	hinh varchar(30),
--	soluong int,
--	giaban float,
--	trangthai int 
--)
--masp, masize, mamau, mactsp, dongia, soluong, hinh, ghichu, trangthai
--alter table nhacungcap
--	add loaisp varchar(20)

--create table loaicuahang
--(
--	mach varchar(20) primary key,
--	tench varchar(20),
--	trangthai int
--)
create table size(
	masize varchar(20) primary key,
	tensize nvarchar(20),
	trangthai int
)

create table mau(
	mamau varchar(20) primary key,
	tenmau nvarchar(20),
	trangthai int
)
create table hoadonban
(
	mahdban varchar(20) primary key,
	manv varchar(20),
	makh varchar(20),
	ngaylaphd datetime,
	tongtien float,
	phone varchar(24),
	trangthai int
)

create table chitiethoadonban
(
	macthdnban varchar(50) primary key,
	mahdban varchar(20),
	masp varchar(100),
	soluong int,
	dongia float,
	khuyenmai float,
	thanhtien float,
	trangthai int,
)

create table hoadonnhap
(
	mahdnhap varchar(20) primary key,
	mancc varchar(20),
	manv varchar(20),
	ngaynhap datetime,
	tongtien float,
	trangthai int
)

create table chitiethoadonnhap
(
	macthdnhap varchar(50) primary key,
	mahdnhap varchar(20),
	masp varchar(100),
	soluong int,
	dongia float,
	thanhtien float,
	trangthai int,
)

--Khóa ngoại
alter table sanpham
add Foreign key(mancc) References nhacungcap(mancc),
Foreign key(maloai) References loaisanpham(maloai),
Foreign key(mamau) References mau(mamau),
Foreign key(masize) References size(masize)
--Foreign key(mach) References loaicuahang(mach)

alter table nhacungcap
add Foreign key(maloai) References loaisanpham(maloai)

--alter table loaisanpham
--add Foreign key(mach) References loaicuahang(mach)

alter table hoadonban
add Foreign key(manv) References nhanvien(manv),
Foreign key(makh) References khachhang(makh)


alter table chitiethoadonban
add Foreign key(masp) References sanpham(masp),
Foreign key(mahdban) References hoadonban(mahdban)

alter table hoadonnhap
add Foreign key(mancc) References nhacungcap(mancc),
Foreign key(manv) References nhanvien(manv)

alter table chitiethoadonnhap
add Foreign key(masp) References sanpham(masp),
Foreign key(mahdnhap) References hoadonnhap(mahdnhap)

----insert

insert nhanvien(manv, tennv, phai, ngaysinh, diachi, email, phone, trangthai)
values('manv01', 'tennv01','Nam01', '7/20/2003', N'Quãng Ngãi 01', N'EmailTường01','0101', 0),
('manv02', 'tennv03','Nam02', '7/20/2003', N'Quãng Ngãi 02', N'EmailTường02','0102', 0),
('manv03', 'tennv03','Nam03', '7/20/2003', N'Quãng Ngãi 03', N'EmailTường03','0103', 0)

insert khachhang(makh, tenkh, phai, diachi, email, phone, trangthai)
values
('makh01', 'tenkh01','Nam01', N'Quãng Ngãi 01', N'EmailTường01','0101', 0),
('makh02', 'tenkh03','Nam02', N'Quãng Ngãi 02', N'EmailTường02','0102', 0),
('makh03', 'tenkh03','Nam03', N'Quãng Ngãi 03', N'EmailTường03','0103', 0)

insert loaisanpham(maloai, tenloai, trangthai)
values('maloai01', 'tenloai01', 0), 
('maloai02', 'tenloai02', 0), 
('maloai03', 'tenloai03', 0)

insert nhacungcap(mancc, tenncc, tennganhang, phone, maloai, trangthai)
values
('mancc01', 'tenncc01','atm01','0101','maloai01', 0), 
('mancc02', 'tenncc02','atm02','0102','maloai02', 0), 
('mancc03', 'tenncc03','atm03','0103','maloai03', 0)

insert hoadonban(mahdban, manv, makh, ngaylaphd, tongtien, phone, trangthai)
values
('mahdban01', 'manv01','makh01', '7/20/2003', 1, '0101', 0),
('mahdban02', 'manv03','makh02', '7/20/2003', 2, '0102', 0),
('mahdban03', 'manv03','makh03', '7/20/2003', 3, '0103', 0)

insert hoadonnhap(mahdnhap, mancc, manv, ngaynhap, tongtien, trangthai)
values
('mahdnhap01', 'mancc01','manv01', '7/20/2003', 1, 0),
('mahdnhap02', 'mancc03','manv02', '7/20/2003', 2, 0),
('mahdnhap03', 'mancc03','manv03', '7/20/2003', 3, 0)

insert size(masize, tensize, trangthai)
values('masize01', 'tensize01', 0), 
('masize02', 'tensize02', 0), 
('masize03', 'tensize03', 0)

insert mau(mamau, tenmau, trangthai)
values('mamau01', 'tensize01', 0), 
('mamau02', 'tensize02', 0), 
('mamau03', 'tensize03', 0)

insert sanpham(masp, tensp, mancc, maloai, mamau, masize, mota, hinh, soluong, gianhap, giaban, trangthai)
values('masp01', 'tensp01','mancc01','maloai01','mamau01','masize01','mota01', 'Rac/1.jpg',1, 1, 1, 0), 
('masp02', 'tensp02','mancc02','maloai02','mamau02','masize02','mota02', 'Rac/2.jpg',2, 2, 2, 0),
('masp03', 'tensp03','mancc03','maloai03','mamau03','masize03','mota03', 'Rac/1.jpg',3, 3, 3, 0)

insert chitiethoadonban(macthdnban, mahdban, masp, soluong, dongia, khuyenmai, thanhtien, trangthai)
values
('macthdnban01','mahdban01', 'masp01',1, 1, 1, 1, 0),
('macthdnban02','mahdban02', 'masp03',2, 2, 2, 2, 0),
('macthdnban03','mahdban03', 'masp03',3, 3, 3, 3, 0)

insert chitiethoadonnhap(macthdnhap, mahdnhap, masp, soluong, dongia, thanhtien, trangthai)
values
('macthdnhap01','mahdnhap01', 'masp01',1, 1, 1, 0),
('macthdnhap02','mahdnhap02', 'masp02',2, 2, 2, 0),
('macthdnhap03','mahdnhap03', 'masp03',3, 3, 3, 0)