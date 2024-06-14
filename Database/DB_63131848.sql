create database WebBanGear_63131848 on (
	name='WebBanGear_63131848data',
	filename='D:\WebBanGear_63131848\Database\WebBanGear_63131848.mdf'
)
log on (
	name='WebBanGear_63131848log',
	filename='D:\WebBanGear_63131848\Database\WebBanGear_63131848.ldf'
);
go
use WebBanGear_63131848;
go
CREATE TABLE LoaiSanPham (
    MaLoaiSP varchar(8) PRIMARY KEY NOT NULL,
    TenLoai NVARCHAR(100),
    MoTa NTEXT
);
go
CREATE TABLE NhaCungCap (
    MaNCC		varchar(8) PRIMARY KEY ,
    TenNCC		NVARCHAR(100),
	EmailNCC	nvarchar(100),
    SDTNCC		nvarchar(20),
	DiaChiNCC	nvarchar(100)
);
go
CREATE TABLE PhanQuyen (
    MaQuyen	 INT PRIMARY KEY IDENTITY,
    TenQuyen NVARCHAR(50)
);
go
CREATE TABLE SanPham (
    MaSP varchar(8) PRIMARY KEY,
    TenSP NVARCHAR(100),
    MoTa NTEXT,
    GiaTien DECIMAL(18, 2),
    SoLuong INT,
	TrangThai bit,
    MaNCC varchar(8),
    MaLoaiSP varchar(8),
    HinhAnh nvarchar(50),
    CONSTRAINT SANPHAM_NCC_FK FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC),
    CONSTRAINT SANPHAM_LOAISP_FK FOREIGN KEY (MaLoaiSP) REFERENCES LoaiSanPham(MaLoaiSP)
);
GO
CREATE TABLE NguoiDung (
    MaNguoiDung varchar(8) PRIMARY KEY,
	HoTenND	nvarchar(100),
    TenDangNhap NVARCHAR(50),
    Email NVARCHAR(50),
	DiaChiND nvarchar(100),
    MatKhau NVARCHAR(50),
    MaQuyen INT,
    CONSTRAINT NGUOIDUNG_PHANQUYEN_FK FOREIGN KEY (MaQuyen) REFERENCES PhanQuyen(MaQuyen)
);
GO
CREATE TABLE DonHang (
    MaDH varchar(8) PRIMARY KEY,
	MaNguoiDung varchar(8),
	DiaChiDH nvarchar(100),
    NgayDat DATE,
    TinhTrang BIT,
    CONSTRAINT DONHANG_NGUOIDUNG_FK FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO
CREATE TABLE ChiTietDonHang (
    MaDH varchar(8),
    MaSP varchar(8),
    SoLuong INT,
    DonGia DECIMAL(18, 2),
	ThanhTien DECIMAL(10,2),
    PRIMARY KEY (MaDH, MaSP),
    CONSTRAINT CTDH_DONHANG_FK FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
    CONSTRAINT CTDT_SANPHAM_FK FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
GO
INSERT INTO LoaiSanPham (MaLoaiSP, TenLoai, MoTa) VALUES
('LAP', N'Laptop', N'Máy tính xách tay'),
('MON', N'Màn hình', N'Màn hình hiển thị cho máy tính'),
('KEY', N'Bàn phím', N'Bàn phím nhập liệu cho máy tính'),
('MOU', N'Chuột', N'Chuột điều khiển cho máy tính'),
('CPU', N'CPU', N'Vi xử lý cho máy tính'),
('RAM', N'RAM', N'Bộ nhớ truy cập ngẫu nhiên cho máy tính'),
('DISK', N'Ổ cứng', N'Bộ nhớ lưu trữ cho máy tính'),
('MAIN', N'Mainboard', N'Bộ phận chính của máy tính kết nối các linh kiện khác nhau'),
('CARD', N'Card đồ họa', N'Card xử lý đồ họa cho máy tính'),
('PSU', N'Nguồn máy tính', N'Nguồn cung cấp điện cho máy tính'),
('CASE', N'Case', N'Thùng máy tính');
GO
INSERT INTO NhaCungCap (MaNCC, TenNCC, EmailNCC, SDTNCC, DiaChiNCC) VALUES
('NCC001', 'Intel Corporation', N'info@intel.com', '123456789', N'Santa Clara, California, USA'),
('NCC002', 'Advanced Micro Devices (AMD)', N'info@amd.com', '987654321', N'Sunnyvale, California, USA'),
('NCC003', 'NVIDIA Corporation', N'info@nvidia.com', '555555555', N'Santa Clara, California, USA'),
('NCC004', 'Samsung Electronics', N'info@samsung.com', '111111111', N'Suwon, Gyeonggi-do, South Korea'),
('NCC005', 'Corsair', N'info@corsair.com', '222222222', N'Fremont, California, USA'),
('NCC006', 'Kingston Technology', N'info@kingston.com', '333333333', N'Fountain Valley, California, USA'),
('NCC007', 'ASUS', N'info@asus.com', '444444444', N'Beitou District, Taipei, Taiwan'),
('NCC008', 'GIGABYTE Technology', N'info@gigabyte.com', '555555555', N'Taipei, Taiwan'),
('NCC009', 'MSI', N'info@msi.com', '666666666', N'New Taipei City, Taiwan'),
('NCC010', 'Crucial Technology', N'info@crucial.com', '777777777', N'Boise, Idaho, USA');
GO
INSERT INTO PhanQuyen (TenQuyen) VALUES
(N'Quản trị viên'),
(N'Nhân viên bán hàng'),
(N'Khách hàng');
GO
INSERT INTO SanPham (MaSP, TenSP, MoTa, GiaTien, SoLuong, TrangThai, MaNCC, MaLoaiSP, HinhAnh) VALUES
-- Laptop
('SP001', N'Laptop Dell Inspiron 15', N'Laptop phổ thông với hiệu suất ổn định từ Dell', 12000000, 30, 1, 'NCC005', 'LAP', N'dellinspiron15.jpg'),
('SP002', N'Laptop HP Pavilion 14', N'Laptop cơ bản với thiết kế mỏng nhẹ từ HP', 11000000, 25, 1, 'NCC007', 'LAP', N'hppavilion14.jpg'),
('SP003', N'Laptop ASUS VivoBook 15', N'Laptop phổ thông với màn hình NanoEdge từ ASUS', 13000000, 20, 1, 'NCC007', 'LAP', N'asusvivobook15.jpg'),
('SP004', N'Laptop Lenovo IdeaPad 5', N'Laptop phổ thông với hiệu suất cao từ Lenovo', 12500000, 28, 1, 'NCC009', 'LAP', N'lenovoideapad5.jpg'),
('SP005', N'Laptop Acer Swift 3', N'Laptop mỏng nhẹ với thiết kế sang trọng từ Acer', 13500000, 22, 1, 'NCC005', 'LAP', N'acerswift3.jpg'),
-- Màn hình
('SP006', N'Màn hình Dell P2419H', N'Màn hình IPS 24 inch với độ phân giải Full HD từ Dell', 3000000, 40, 1, 'NCC001', 'MON', N'dellp2419h.jpg'),
('SP007', N'Màn hình LG UltraGear 27GL850', N'Màn hình gaming IPS 27 inch với tần số làm mới 144Hz từ LG', 4500000, 35, 1, 'NCC004', 'MON', N'lg27gl850.jpg'),
('SP008', N'Màn hình ASUS TUF Gaming VG279QM', N'Màn hình gaming IPS 27 inch với tần số làm mới 280Hz từ ASUS', 6000000, 30, 1, 'NCC007', 'MON', N'asusvg279qm.jpg'),
('SP009', N'Màn hình Samsung Odyssey G7', N'Màn hình cong 27 inch với tốc độ làm mới 240Hz từ Samsung', 5500000, 32, 1, 'NCC004', 'MON', N'samsungodysseyg7.jpg'),
('SP010', N'Màn hình AOC 24G2', N'Màn hình IPS 24 inch với tần số làm mới 144Hz từ AOC', 3250000, 38, 1, 'NCC008', 'MON', N'aoc24g2.jpg'),
-- Chuột
('SP011', N'Chuột Logitech G502 Hero', N'Chuột gaming có độ chính xác cao từ Logitech', 500000, 50, 1, 'NCC005', 'MOU', N'logitechg502hero.jpg'),
('SP012', N'Chuột SteelSeries Rival 3', N'Chuột gaming có hiệu suất cao từ SteelSeries', 550000, 45, 1, 'NCC010', 'MOU', N'steelseriesrival3.jpg'),
('SP013', N'Chuột Razer DeathAdder V2', N'Chuột gaming với cảm biến quang học Focus+ 20.000 DPI từ Razer', 850000, 55, 1, 'NCC005', 'MOU', N'razerdeathadderv2.jpg'),
('SP014', N'Chuột Corsair Sabre RGB', N'Chuột gaming siêu nhẹ với cảm biến quang học 10.000 DPI từ Corsair', 580000, 60, 1, 'NCC005', 'MOU', N'corsairsabrergb.jpg'),
('SP015', N'Chuột HyperX Pulsefire FPS Pro', N'Chuột gaming với cảm biến Pixart 3389 và đèn LED RGB từ HyperX', 750000, 48, 1, 'NCC005', 'MOU', N'hyperxpulsefirefpspro.jpg'),
-- CPU
('SP016', N'CPU AMD Ryzen 5 5600X', N'Vi xử lý AMD Ryzen 5 thế hệ 5 với 6 nhân 12 luồng', 7300000, 40, 1, 'NCC002', 'CPU', N'amdryzen55600x.jpg'),
('SP017', N'CPU Intel Core i5-11400', N'Vi xử lý Intel Core i5 thế hệ 11 với 6 nhân 12 luồng', 3500000, 42, 1, 'NCC001', 'CPU', N'intelcorei511400.jpg'),
('SP018', N'CPU AMD Ryzen 7 5800X', N'Vi xử lý AMD Ryzen 7 thế hệ 5 với 8 nhân 16 luồng', 9400000, 38, 1, 'NCC002', 'CPU', N'amdryzen75800x.jpg'),
('SP019', N'CPU Intel Core i9-11900K', N'Vi xử lý Intel Core i9 thế hệ 11 với 8 nhân 16 luồng', 12900000, 36, 1, 'NCC001', 'CPU', N'intelcorei911900k.jpg'),
('SP020', N'CPU AMD Ryzen 9 5900X', N'Vi xử lý AMD Ryzen 9 thế hệ 5 với 12 nhân 24 luồng', 11900000, 34, 1, 'NCC002', 'CPU', N'amdryzen95900x.jpg'),
-- RAM
('SP021', N'RAM Corsair Vengeance LPX 16GB', N'Bộ nhớ RAM DDR4 16GB với hiệu suất cao từ Corsair', 1500000, 60, 1, 'NCC005', 'RAM', N'corsairvengeancelpx16gb.jpg'),
('SP022', N'RAM G.SKILL Trident Z RGB 32GB', N'Bộ nhớ RAM DDR4 32GB với đèn LED RGB từ G.SKILL', 2600000, 55, 1, 'NCC005', 'RAM', N'gskilltridentzrgb32gb.jpg'),
('SP023', N'RAM Crucial Ballistix 32GB', N'Bộ nhớ RAM DDR4 32GB với hiệu suất cao từ Crucial', 2400000, 57, 1, 'NCC010', 'RAM', N'crucialballistix32gb.jpg'),
('SP024', N'RAM Kingston HyperX Fury 8GB', N'Bộ nhớ RAM DDR4 8GB với hiệu suất cao từ Kingston', 800000, 62, 1, 'NCC006', 'RAM', N'kingstonhyperxfury8gb.jpg'),
('SP025', N'RAM Team T-Force Delta RGB 16GB', N'Bộ nhớ RAM DDR4 16GB với đèn LED RGB từ Team', 1800000, 58, 1, 'NCC006', 'RAM', N'teamtforcedeltargb16gb.jpg'),
-- Ổ cứng SSD
('SP026', N'Ổ cứng SSD WD Blue 1TB', N'Ổ cứng SSD dung lượng 1TB từ Western Digital', 1800000, 70, 1, 'NCC005', 'DISK', N'wdblue1tb.jpg'),
('SP027', N'Ổ cứng SSD Crucial MX500 2TB', N'Ổ cứng SSD dung lượng 2TB với tốc độ cao từ Crucial', 3000000, 65, 1, 'NCC010', 'DISK', N'crucialmx5002tb.jpg'),
('SP028', N'Ổ cứng SSD Samsung 970 EVO Plus 500GB', N'Ổ cứng SSD dung lượng 500GB với tốc độ đọc ghi cao từ Samsung', 1500000, 75, 1, 'NCC004', 'DISK', N'samsung970evoplus500gb.jpg'),
('SP029', N'Ổ cứng SSD Adata XPG SX8200 Pro 1TB', N'Ổ cứng SSD dung lượng 1TB với tốc độ cao từ Adata', 1900000, 72, 1, 'NCC006', 'DISK', N'adataxpgsx8200pro1tb.jpg'),
('SP030', N'Ổ cứng SSD Kingston A2000 500GB', N'Ổ cứng SSD dung lượng 500GB với tốc độ cao từ Kingston', 1200000, 78, 1, 'NCC006', 'DISK', N'kingstona2000500gb.jpg'),
-- Mainboard
('SP031', N'Mainboard ASUS Prime B560M-A', N'Mainboard ASUS hỗ trợ các dòng CPU thế hệ 11 với chipset B560', 1800000, 80, 1, 'NCC007', 'MAIN', N'asusprimeb560ma.jpg'),
('SP032', N'Mainboard MSI MPG B550 Gaming Plus', N'Mainboard MSI hỗ trợ các dòng CPU thế hệ 3 với chipset B550', 2500000, 85, 1, 'NCC009', 'MAIN', N'msipggb550gamingplus.jpg'),
('SP033', N'Mainboard Gigabyte B450 Aorus Elite', N'Mainboard Gigabyte hỗ trợ các dòng CPU thế hệ 3 với chipset B450', 1700000, 75, 1, 'NCC008', 'MAIN', N'gigabyteb450aoruselite.jpg'),
('SP034', N'Mainboard ASRock B460M Pro4', N'Mainboard ASRock hỗ trợ các dòng CPU thế hệ 10 với chipset B460', 2000000, 82, 1, 'NCC008', 'MAIN', N'asrockb460mpro4.jpg'),
('SP035', N'Mainboard Biostar Racing X570GT8', N'Mainboard Biostar hỗ trợ các dòng CPU thế hệ 3 với chipset X570', 3000000, 88, 1, 'NCC008', 'MAIN', N'biostarracingx570gt8.jpg'),
-- Card đồ họa
('SP036', N'Card đồ họa ASUS TUF Gaming GeForce GTX 1660', N'Card đồ họa NVIDIA GeForce GTX 1660 với hiệu suất cao từ ASUS', 3200000, 50, 1, 'NCC007', 'CARD', N'asustufgtx1660.jpg'),
('SP037', N'Card đồ họa Gigabyte Radeon RX 6700 XT', N'Card đồ họa AMD Radeon RX 6700 XT với hiệu suất cao từ Gigabyte', 7500000, 46, 1, 'NCC008', 'CARD', N'gigabyteradeonrx6700xt.jpg'),
('SP038', N'Card đồ họa MSI GeForce RTX 3080 Gaming X Trio', N'Card đồ họa NVIDIA GeForce RTX 3080 với hiệu suất cao từ MSI', 22000000, 42, 1, 'NCC009', 'CARD', N'msirtx3080gamingxtrio.jpg'),
('SP039', N'Card đồ họa Zotac GeForce RTX 3060 Twin Edge', N'Card đồ họa NVIDIA GeForce RTX 3060 với hiệu suất cao từ Zotac', 14000000, 48, 1, 'NCC010', 'CARD', N'zotacrtx3060twinedge.jpg'),
('SP040', N'Card đồ họa PowerColor Radeon RX 6900 XT Red Devil', N'Card đồ họa AMD Radeon RX 6900 XT với hiệu suất cao từ PowerColor', 30000000, 44, 1, 'NCC008', 'CARD', N'powercolorrx6900xtreddevil.jpg'),
-- Nguồn máy tính
('SP041', N'Nguồn máy tính Corsair RM750x 750W', N'Nguồn máy tính công suất 750W 80 Plus Gold từ Corsair', 1900000, 65, 1, 'NCC005', 'PSU', N'corsairrm750x750w.jpg'),
('SP042', N'Nguồn máy tính EVGA SuperNOVA 650 G5', N'Nguồn máy tính công suất 650W 80 Plus Gold từ EVGA', 1700000, 68, 1, 'NCC010', 'PSU', N'evgasupernova650g5.jpg'),
('SP043', N'Nguồn máy tính Seasonic Focus GX-850', N'Nguồn máy tính công suất 850W 80 Plus Gold từ Seasonic', 2400000, 72, 1, 'NCC006', 'PSU', N'seasonicfocusgx850.jpg'),
('SP044', N'Nguồn máy tính Cooler Master MWE Gold 750 V2', N'Nguồn máy tính công suất 750W 80 Plus Gold từ Cooler Master', 2000000, 70, 1, 'NCC010', 'PSU', N'coolermastermwegold750v2.jpg'),
('SP045', N'Nguồn máy tính Thermaltake Toughpower GF1 850W', N'Nguồn máy tính công suất 850W 80 Plus Gold từ Thermaltake', 2200000, 75, 1, 'NCC006', 'PSU', N'thermaltaketoughpowergf1850w.jpg'),
-- Case
('SP046', N'Case NZXT H510', N'Case PC ATX chất lượng cao với thiết kế hiện đại từ NZXT', 1500000, 80, 1, 'NCC010', 'CASE', N'nzxt510.jpg'),
('SP047', N'Case Phanteks Eclipse P400A', N'Case PC ATX với hệ thống làm mát tối ưu từ Phanteks', 1800000, 85, 1, 'NCC008', 'CASE', N'phantekseclipsep400a.jpg'),
('SP048', N'Case Corsair Carbide Series SPEC-06', N'Case PC ATX với thiết kế hiện đại và cửa kính cường lực từ Corsair', 2000000, 75, 1, 'NCC005', 'CASE', N'corsaircarbideseriesspec06.jpg'),
('SP049', N'Case Cooler Master MasterBox MB511 ARGB', N'Case PC ATX với hệ thống đèn LED ARGB từ Cooler Master', 2200000, 78, 1, 'NCC010', 'CASE', N'coolermastermasterboxmb511argb.jpg'),
('SP050', N'Case Fractal Design Meshify C', N'Case PC ATX với thiết kế hiện đại và làm mát tối ưu từ Fractal Design', 1900000, 82, 1, 'NCC008', 'CASE', N'fractaldesignmeshifyc.jpg');
GO
INSERT INTO NguoiDung (MaNguoiDung, HoTenND, TenDangNhap, Email, DiaChiND, MatKhau, MaQuyen)
VALUES
    ('ND001', N'Nguyễn Thị Hương', N'huongnt', N'huongnt@gmail.com', N'123 Đường Lê Lợi, Quận 1, Thành phố Hồ Chí Minh', N'Huong123', 1),
    ('ND002', N'Võ Văn Tuấn', N'tuanvv', N'tuanvv@gmail.com', N'456 Đường Nguyễn Huệ, Quận 2, Thành phố Hà Nội', N'Tuan456', 3),
    ('ND003', N'Hoàng Thị Mai', N'maiht', N'maiht@gmail.com', N'789 Đường Trần Hưng Đạo, Quận 3, Thành phố Đà Nẵng', N'Mai789', 3),
    ('ND004', N'Lê Văn Hoàng', N'hoanglv', N'hoanglv@gmail.com', N'321 Đường Lý Tự Trọng, Quận 4, Thành phố Cần Thơ', N'Hoang0123', 3),
    ('ND005', N'Nguyễn Thị Trang', N'trangnt', N'trangnt@gmail.com', N'654 Đường Lê Hồng Phong, Quận 5, Thành phố Vũng Tàu', N'Trang345', 3),
    ('ND006', N'Trần Văn Nam', N'namtv', N'namtv@gmail.com', N'987 Đường Phạm Văn Đồng, Quận 6, Thành phố Hồ Chí Minh', N'Nam678', 3),
    ('ND007', N'Phạm Thị Lan', N'lanpt', N'lanpt@gmail.com', N'234 Đường Nguyễn Công Trứ, Quận 7, Thành phố Hà Nội', N'Lan901', 3),
    ('ND008', N'Đặng Văn Đức', N'ducdv', N'ducdv@gmail.com', N'567 Đường Lê Văn Việt, Quận 8, Thành phố Đà Nẵng', N'Duc234', 3),
    ('ND009', N'Lê Thị Hoa', N'hoalth', N'hoalth@gmail.com', N'876 Đường Nguyễn Văn Linh, Quận 9, Thành phố Cần Thơ', N'Hoa567', 3),
    ('ND010', N'Nguyễn Văn Đạt', N'datnv', N'datnv@gmail.com', N'543 Đường Lý Thường Kiệt, Quận 10, Thành phố Vũng Tàu', N'Dat890', 3),
    ('ND011', N'Trần Thị Anh', N'anhtt', N'anhtt@gmail.com', N'101 Đường Lê Lợi, Quận 11, TP. Hồ Chí Minh', N'Anh123', 3),
    ('ND012', N'Hoàng Văn Bình', N'binhvh', N'binhvh@gmail.com', N'202 Đường Lê Lai, Quận 12, TP. Hà Nội', N'Binh456', 3),
    ('ND013', N'Nguyễn Thị Ngọc', N'ngocnt', N'ngocnt@gmail.com', N'303 Đường Nguyễn Huệ, Quận 1, TP. Đà Nẵng', N'Ngoc789', 3),
    ('ND014', N'Lê Văn Thanh', N'thanhlv', N'thanhlv@gmail.com', N'404 Đường Trần Phú, Quận 2, TP. Cần Thơ', N'Thanh0123', 3),
    ('ND015', N'Võ Thị Hồng', N'hongvt', N'hongvt@gmail.com', N'505 Đường Lý Thường Kiệt, Quận 3, TP. Vũng Tàu', N'Hong345', 3);
GO
INSERT INTO DonHang (MaDH, MaNguoiDung, DiaChiDH, NgayDat, TinhTrang)
VALUES
    ('DH001', 'ND001', N'123 Đường Lê Lợi, Quận 1, Thành phố Hồ Chí Minh', '2024-05-07', 1),
    ('DH002', 'ND002', N'456 Đường Nguyễn Huệ, Quận 2, Thành phố Hà Nội', '2024-05-08', 0),
    ('DH003', 'ND003', N'789 Đường Trần Hưng Đạo, Quận 3, Thành phố Đà Nẵng', '2024-05-09', 1),
    ('DH004', 'ND004', N'321 Đường Lý Tự Trọng, Quận 4, Thành phố Cần Thơ', '2024-05-10', 0),
    ('DH005', 'ND005', N'654 Đường Lê Hồng Phong, Quận 5, Thành phố Vũng Tàu', '2024-05-11', 1),
    ('DH006', 'ND006', N'987 Đường Phạm Văn Đồng, Quận 6, TP. Hồ Chí Minh', '2024-05-12', 1),
    ('DH007', 'ND007', N'234 Đường Nguyễn Công Trứ, Quận 7, TP. Hà Nội', '2024-05-13', 0),
    ('DH008', 'ND008', N'567 Đường Lê Văn Việt, Quận 8, TP. Đà Nẵng', '2024-05-14', 1),
    ('DH009', 'ND009', N'876 Đường Nguyễn Văn Linh, Quận 9, TP. Cần Thơ', '2024-05-15', 0),
    ('DH010', 'ND010', N'543 Đường Lý Thường Kiệt, Quận 10, TP. Vũng Tàu', '2024-05-16', 1),
    ('DH011', 'ND001', N'123 Đường Lê Lợi, Quận 1, Thành phố Hồ Chí Minh', '2024-05-17', 1),
    ('DH012', 'ND002', N'456 Đường Nguyễn Huệ, Quận 2, Thành phố Hà Nội', '2024-05-18', 0),
    ('DH013', 'ND003', N'789 Đường Trần Hưng Đạo, Quận 3, Thành phố Đà Nẵng', '2024-05-19', 1),
    ('DH014', 'ND004', N'321 Đường Lý Tự Trọng, Quận 4, Thành phố Cần Thơ', '2024-05-20', 0),
    ('DH015', 'ND005', N'654 Đường Lê Hồng Phong, Quận 5, Thành phố Vũng Tàu', '2024-05-21', 1),
    ('DH016', 'ND006', N'987 Đường Phạm Văn Đồng, Quận 6, TP. Hồ Chí Minh', '2024-05-22', 1),
    ('DH017', 'ND007', N'234 Đường Nguyễn Công Trứ, Quận 7, TP. Hà Nội', '2024-05-23', 0),
    ('DH018', 'ND008', N'567 Đường Lê Văn Việt, Quận 8, TP. Đà Nẵng', '2024-05-24', 1),
    ('DH019', 'ND009', N'876 Đường Nguyễn Văn Linh, Quận 9, TP. Cần Thơ', '2024-05-25', 0),
    ('DH020', 'ND010', N'543 Đường Lý Thường Kiệt, Quận 10, TP. Vũng Tàu', '2024-05-26', 1);
GO
INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, DonGia, ThanhTien)
VALUES ('DH001', 'SP012', 3, 550000, 1650000),
       ('DH002', 'SP037', 1, 7500000, 7500000),
       ('DH003', 'SP024', 3, 800000, 2400000),
       ('DH004', 'SP041', 2, 1900000, 3800000),
       ('DH005', 'SP005', 1, 13500000, 13500000),
       ('DH005', 'SP032', 2, 2500000, 2500000),
       ('DH006', 'SP017', 1, 3500000, 3500000),
       ('DH006', 'SP029', 3, 1900000, 1900000),
       ('DH007', 'SP002', 1, 11000000, 22000000),
       ('DH007', 'SP040', 2, 30000000, 60000000),
       ('DH008', 'SP027', 3, 3000000, 9000000),
       ('DH009', 'SP035', 1, 3000000, 3000000),
       ('DH009', 'SP036', 2, 3200000, 3200000),
       ('DH010', 'SP014', 1, 580000, 580000),
       ('DH011', 'SP003', 1, 13000000, 13000000),
       ('DH011', 'SP039', 2, 14000000, 14000000),
       ('DH012', 'SP011', 3, 500000, 1500000),
       ('DH013', 'SP048', 2, 2000000, 4000000),
       ('DH014', 'SP049', 1, 2200000, 2200000),
       ('DH015', 'SP025', 1, 1800000, 1800000),
       ('DH015', 'SP031', 2, 1800000, 1800000),
       ('DH016', 'SP007', 3, 4500000, 13500000),
       ('DH016', 'SP028', 2, 1500000, 4500000),
       ('DH017', 'SP050', 2, 1900000, 3800000),
       ('DH018', 'SP046', 1, 1500000, 1500000),
       ('DH019', 'SP001', 3, 12000000, 12000000),
       ('DH019', 'SP049', 1, 2200000, 2200000),
       ('DH020', 'SP016', 2, 7300000, 14600000);

GO
CREATE TRIGGER SuaTrangThai
ON SanPham
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	-- TrangThai = 0 (hết hàng) nếu số lượng = 0
    UPDATE SanPham
    SET TrangThai = 0
    WHERE SoLuong = 0
      AND EXISTS (
          SELECT 1
          FROM inserted i
          WHERE i.MaSP = SanPham.MaSP
            AND i.SoLuong = 0
      );
	  -- Đặt TrangThai = 1 (còn hàng) nếu số lượng > 0
    UPDATE SanPham
    SET TrangThai = 1
    WHERE SoLuong > 0
      AND EXISTS (
          SELECT 1
          FROM inserted i
          WHERE i.MaSP = SanPham.MaSP
            AND i.SoLuong > 0
      );
END;

GO
CREATE PROCEDURE TimKiemLSP
    @MaLoaiSP varchar(8)=NULL,
    @TenLoai nvarchar(100)=NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)
    SELECT @SqlStr = '
       SELECT *
       FROM LoaiSanPham
       WHERE 1=1
       '

    IF @MaLoaiSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND MaLoaiSP LIKE ''%' + @MaLoaiSP + '%''
        '

    IF @TenLoai IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
			AND TenLoai LIKE N''%' + @TenLoai + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END

GO
CREATE PROCEDURE TimKiemNCc
    @MaNCC varchar(8)=NULL,
    @TenNCC nvarchar(100)=NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)
    SELECT @SqlStr = '
       SELECT *
       FROM NhaCungCap
       WHERE 1=1
       '

    IF @MaNCC IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND MaNCC LIKE ''%' + @MaNCC + '%''
        '

    IF @TenNCC IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
			AND TenNCC LIKE N''%' + @TenNCC + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END

GO
CREATE PROCEDURE TimKiemSP
    @MaSP varchar(8)=NULL,
    @TenSP nvarchar(100)=NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)
    SELECT @SqlStr = '
       SELECT *
       FROM SANPHAM
       WHERE 1=1
       '

    IF @MaSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND MaSP LIKE ''%' + @MaSP + '%''
        '

    IF @TenSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
			AND TenSP LIKE N''%' + @TenSP + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END

GO
CREATE PROCEDURE TimKiemND
    @MaND varchar(8)=NULL,
    @TenND nvarchar(100)=NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)
    SELECT @SqlStr = '
       SELECT *
       FROM NGUOIDUNG
       WHERE 1=1
       '

    IF @MaND IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND MaNguoiDung LIKE ''%' + @MaND + '%''
        '

    IF @TenND IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
			AND HoTenND LIKE N''%' + @TenND + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END

GO
CREATE PROCEDURE TimKiemDH
    @MaDH varchar(8) = NULL,
    @MaND varchar(8) = NULL,
    @TenND nvarchar(100) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)

    SELECT @SqlStr = '
        SELECT *
        FROM DonHang dh
        JOIN NguoiDung nd ON dh.MaNguoiDung = nd.MaNguoiDung
        WHERE 1=1
       '

    IF @MaDH IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND dh.MaDH LIKE ''%' + @MaDH + '%''
        '

    IF @MaND IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND nd.MaNguoiDung LIKE ''%' + @MaND + '%''
        '

    IF @TenND IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND nd.HoTenND LIKE N''%' + @TenND + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END

GO
CREATE PROCEDURE TimKiemCTDH
    @MaDH varchar(8) = NULL,
    @MaSP varchar(8) = NULL,
    @TenSP nvarchar(100) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)

    SELECT @SqlStr = '
        SELECT *
        FROM ChiTietDonHang ct
        JOIN SANPHAM sp
		ON ct.MaSP = sp.MaSP
        WHERE 1=1
       '

    IF @MaDH IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND ct.MaDH LIKE ''%' + @MaDH + '%''
        '

    IF @MaSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND sp.MaSP LIKE ''%' + @MaSP + '%''
        '

    IF @TenSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND sp.TenSP LIKE N''%' + @TenSP + '%''
        '

    EXEC SP_EXECUTESQL @SqlStr
END