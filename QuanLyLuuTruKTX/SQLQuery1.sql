﻿SELECT MaSo, HopDong.MSSV, NgayBatDau, NgayKetThuc, HoTen, NgaySinh, Khoa, CMND, SoDienThoai, Email, QueQuan FROM HopDong, SinhVien WHERE HopDong.MSSV=SinhVien.MSSV