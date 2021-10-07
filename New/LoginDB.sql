drop database if exists LoginDB;
create database LoginDB;
use LoginDB;

create table Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null, 
    customer_address varchar(100),
    telephone varchar(20)
);

create table Cashiers(
	cashier_id int auto_increment primary key,
    cashier_name varchar(100) not null,
    user_name varchar(100) not null unique,
    user_pass varchar(200) not null,
    telephone varchar(20) 
);

create table Colors(
	color_id int auto_increment primary key,
    color_name varchar(100) not null
);

create table Sizes(
	size_id int auto_increment primary key,
    size_name varchar(100) not null
);

create table Items(
	item_id int auto_increment primary key,
    item_name varchar(100) not null,
    item_description varchar(100),
    item_price decimal(18,3) default 0
);

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

create table Invoices(
	invoice_no int auto_increment primary key,
    cashier_id int not null,
    customer_id int not null,
    invoice_date datetime default now() not null,
	constraint fk_Invoices_Cashiers foreign key(cashier_id) references Cashiers(cashier_id),
	constraint fk_Invoices_Customers foreign key(customer_id) references Customers(customer_id)
);	

create table InvoiceDetails(
	invoice_no int not null,
    item_id int not null,
    item_price decimal(18,3) default 0,
    quantity int not null default 0,
    constraint pk_InvoiceDetails primary key(invoice_no, item_id),
    constraint fk_InvoiceDetails_Invoices foreign key(invoice_no) references Invoices(invoice_no),
	constraint fk_InvoiceDetails_Items foreign key(item_id) references Items(item_id)
);

insert into Customers(customer_id, customer_name, customer_address, telephone) values 
(1, 'Customer 1', 'HN', '09124124'),
(2, 'Customer 2', 'HP', '08234234'),
(3, 'Customer 3', 'VP', '07202202'),
(4, 'Customer 4', 'VT', '06397397'),
(5, 'Customer 5', 'HD', '09323875');
select * from Customers;

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on LoginDB.* to 'vtca'@'localhost';

-- Password: ShopClothes
insert into Cashiers (cashier_id, cashier_name, user_name, user_pass, telephone) values
(1, 'shopclothes', 'Clothes', 'f637569d1f2b1af93c463b312f2d77de', '0982942754');
select * from Cashiers;
-- select * from Cashiers where user_name='Clothes' and user_pass='f637569d1f2b1af93c463b312f2d77de';

insert into Colors (color_id, color_name) values
(1, 'Red'),
(2, 'Red orange'),
(3, 'Yellow'),
(4, 'Yellow green'),
(5, 'Blue'),
(6, 'Blue green');
select * from Colors;

insert into Sizes(size_id, size_name) values 
(1, 'S'), (2, 'M'), (3, 'L'), (4, 'XL'), (5, 'XXL'), (6, 'XXXL');
select * from Sizes;

insert into Items(item_id, item_name, item_description, item_price) values
(1, 'Own the run shorts', 'Inspired by adidas heritage, Adicolor is authentic but modern', 700),
(2, 'Three-Stripes shorts', 'Keep all your valuables  with packable front zip', 800),
(3, 'Tokyo pack sweat pants', 'A drawcord waist lets you adjust the fit so you comfortable', 900),
(4, 'Yoga pants', 'Wash light color separate from dark color', 600),
(5, 'Logo play badge tee', 'Do not use fabric softener, use mild detergent only', 100),
(6, 'Logo box graphic tee', 'By buying cotton products from us', 200),
(7, 'Rain jacket', 'Feel dry and serene wearing this light weight and breathable jacket', 500),
(8, 'Marathon jacket', 'Item is made with Primeblue, a high-performance recycled material', 100),
(9, 'Mike hoodie', 'This product is made with organic cotton', 550),
(10, 'Hulk hoodie', 'This product is made with of high-performance recycled materials', 450);
select * from Items;

insert into ItemDetails(item_id, color_id, size_id, quantity) values 
(1, 3, 1, 10),
(1, 4, 2, 10),
(2, 1, 3, 10),
(2, 2, 4, 10),
(3, 6, 4, 10),
(4, 5, 3, 10),
(5, 4, 2, 10),
(6, 3, 1, 10),
(7, 2, 6, 10),
(8, 1, 5, 10),
(9, 2, 4, 10),
(10, 3, 3, 10);
select * from ItemDetails;

insert into Invoices(invoice_no, cashier_id, customer_id, invoice_date) values 
(1, 1, 1, '2021-10-20 1:00:00'),
(2, 1, 2, '2021-10-20 2:00:00'),
(3, 1, 3, '2021-10-20 3:00:00'),
(4, 1, 4, '2021-10-20 4:00:00'),
(5, 1, 1, NOW());
select * from Invoices;

insert into InvoiceDetails(invoice_no, item_id) values 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
select * from InvoiceDetails;

select * from Items as it
inner join ItemDetails itd on it.item_id = itd.item_id
inner join Colors co on co.color_id = itd.color_id
inner join Sizes si on si.size_id = itd.size_id;
-- where it.item_id = 1;