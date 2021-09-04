drop database if exists LoginDB;
create database LoginDB;
use LoginDB;

drop table Customers;
create table Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null, 
    customer_address varchar(100),
    telephone varchar(20)
);

drop table Cashiers;
create table Cashiers(
	cashier_id int auto_increment primary key,
    cashier_name varchar(100) not null,
    user_name varchar(100) not null unique,
    user_pass varchar(200) not null,
    telephone varchar(20) 
);

drop table Colors;
create table Colors(
	color_id int auto_increment primary key,
    color_name varchar(100) not null
);

drop table Sizes;
create table Sizes(
	size_id int auto_increment primary key,
    size_name varchar(100) not null
);

drop table Items;
create table Items(
	item_id int auto_increment primary key,
    item_name varchar(100) not null,
    item_description varchar(100),
    item_price decimal(18,3) default 0
);

drop table ItemDetails;
create table ItemDetails(
	item_id int not null,
    color_id int not null,
    size_id int not null,
	quantity int not null default 1,
    constraint pk_ItemDetails primary key(item_id, color_id, size_id),  
    constraint fk_ItemDetails_Items foreign key(item_id) references Items(item_id),
    constraint fk_ItemDetails_Colors foreign key(color_id) references Colors(color_id),
    constraint fk_ItemDetails_Sizes foreign key(size_id) references Sizes(size_id)
);

drop table Invoices;
create table Invoices(
	invoice_no int auto_increment primary key,
    cashier_id int not null,
    customer_id int not null,
	invoice_date datetime default now() not null,
	constraint fk_Invoices_Cashiers foreign key(cashier_id) references Cashiers(cashier_id),
	constraint fk_Invoices_Customers foreign key(customer_id) references Customers(customer_id)
);	

drop table InvoiceDetails;
create table InvoiceDetails(
	invoice_no int not null,
    item_id int not null,
	item_price decimal(18,3) not null,
    constraint pk_InvoiceDetails primary key(invoice_no, item_id),  
    constraint fk_InvoiceDetails_Invoices foreign key(invoice_no) references Invoices(invoice_no),
	constraint fk_InvoiceDetails_Items foreign key(item_id) references Items(item_id)
);

insert into Customers(customer_name, customer_address, telephone) values 
		('Khach hang 1', 'Hai Duong', '09124124'),
		('Khach hang 2', 'Hai Phong', '08234234'),
		('Khach hang 3', 'Vinh Phuc', '07202202'),
		('Khach hang 4', 'Vung Tau', '06397397');
select * from Customers;

insert into Colors (color_name) values
('Black'),
('White'),
('Blue'),
('Red');
select * from Colors;

insert into Sizes(size_name) values 
('S'), ('M'), ('L'), ('XL'), ('XXL');
select * from Sizes;

insert into Items (item_name, item_description) values
		('Áo khoác chống nắng UV', 'Bảo vệ tốt nhất - ngăn tới 99,4% tia UV được Viện Dệt May Việt Nam kiểm nghiệm và xác nhận'),
		('Áo Polo Nam', 'Sản phẩm được thiết kế từ chất liệu cotton mềm mại, họa tiết màu sắc nổi bật, phù hợp với nhiều phom dáng và hoàn cảnh khác nhau'),
		('Quần Khakis Nam dáng ôm', 'Phom dáng hiện đại, khỏe khoắn, cá tính thích hợp đi làm, đi chơi'),
		('Quần Shorts Kaki Nam', 'Với thiết kế đơn giản khỏe khoắn, quần sooc phù hợp cho những hoạt động đi chơi, du lịch');
select * from Items;

insert into ItemDetails(item_id, color_id, size_id) values 
(1, 3, 2),
(1, 4, 3),
(3, 2, 4),
(4, 1, 1);
select * from ItemDetails;

insert into Invoices(invoice_no, cashier_id, customer_id) values 
(1, 1, 3),
(2, 1, 1),
(3, 1, 2),
(4, 1, 4);
select * from Invoices;

insert into InvoiceDetails(invoice_no, item_id, item_price) values 
(1, 3, 229.000),
(2, 4, 599.000),
(3, 1, 111.000),
(4, 2, 499.000);
select * from InvoiceDetails;

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on LoginDB.* to 'vtca'@'localhost';

-- ShopClothes
insert into Cashiers (cashier_id, cashier_name, user_name, user_pass, telephone) values
		(1, 'shopclothes', 'Clothes', 'f637569d1f2b1af93c463b312f2d77de', '0982942754');
select * from Cashiers;
select * from Cashiers where user_name='Clothes' and user_pass='f637569d1f2b1af93c463b312f2d77de';












































