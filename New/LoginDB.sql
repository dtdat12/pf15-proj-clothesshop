drop database if exists LoginDB;
create database LoginDB;
use LoginDB;

drop table customers;
create table customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null, 
    customer_address varchar(100) not null,
    telephone varchar(20)
);

drop table cashiers;
create table cashiers(
	cashier_id int auto_increment primary key,
    cashier_name varchar(100) not null,
    user_name varchar(100) not null unique,
    user_pass varchar(200) not null,
    telephone varchar(20) 
);

drop table colors;
create table colors(
	color_id int auto_increment primary key,
    color_name varchar(100) not null
);

drop table sizes;
create table sizes(
	size_id int auto_increment primary key,
    size_name varchar(100) not null
);

drop table items;
create table items(
	item_id int auto_increment primary key,
    item_name varchar(100) not null,
    item_description varchar(200) not null unique
);

drop table itemdetails;
create table itemdetails(
	quantity int not null,
    item_id int not null,
    color_id int not null,
    size_id int not null,
    foreign key (item_id) references items(item_id),
    foreign key (color_id) references colors(color_id),
    foreign key (size_id) references sizes(size_id),
    primary key (item_id, color_id, size_id)
);

drop table invoices;
create table invoices(
	invoice_no int auto_increment primary key,
    invoice_date datetime,
    cashier_id int not null,
    customer_id int not null,
    foreign key (customer_id) references customers(customer_id),
    foreign key (cashier_id) references cashiers(cashier_id)
);

drop table invoicedetails;
create table invoicedetails(
	item_price double,
    item_id int not null,
    invoice_no int not null,
    foreign key (invoice_no) references invoices(invoice_no),
    foreign key (item_id) references items(item_id),
    primary key (invoice_no, item_id)
);

insert into customers values ('01', 'Khách hàng 1', 'HN', '09124124');
insert into customers values ('02', 'Khách hàng 2', 'HP', '08234234');
insert into customers values ('03', 'Khách hàng 3', 'VP', '07202202');
insert into customers values ('04', 'Khách hàng 4', 'VT', '06397397');
select * from logindb.customers;

create user if not exists 'root'@'localhost' identified by '12345abc';
grant all on LoginDB.* to 'root'@'localhost';

insert into cashiers values('001', 'Thu ngân 1', 'pf15', '', '02406406');
select * from cashiers;
select * from cashiers where user_name='pf15' and user_pass='';

insert into colors values ('1001', 'Black');
insert into colors values ('1002', 'White');
insert into colors values ('1003', 'Red');
insert into colors values ('1004', 'Blue');
select * from logindb.colors;

insert into sizes values ('2001', 'S');
insert into sizes values ('2002', 'M');
insert into sizes values ('2003', 'L');
insert into sizes values ('2004', 'XL');
select * from logindb.sizes;

insert into items values ('0001', 'Áo khoác chống nắng UV', 'Bảo vệ tốt nhất - ngăn tới 99,4% tia UV được Viện Dệt May Việt Nam kiểm nghiệm và xác nhận');
insert into items values ('0002', 'Áo Polo Nam', 'Sản phẩm được thiết kế từ chất liệu cotton mềm mại, họa tiết màu sắc nổi bật, phù hợp với nhiều phom dáng và hoàn cảnh khác nhau');
insert into items values ('0003', 'Quần Khakis Nam dáng ôm', 'Phom dáng hiện đại, khỏe khoắn, cá tính thích hợp đi làm, đi chơi');
insert into items values ('0004', 'Quần Shorts Kaki Nam', 'Với thiết kế đơn giản khỏe khoắn, quần sooc phù hợp cho những hoạt động đi chơi, du lịch');
select * from logindb.items;

insert into itemdetails values ('10', '0001', '1003', '2001');
insert into itemdetails values ('11', '0004', '1001', '2002');
insert into itemdetails values ('12', '0003', '1002', '2001');
insert into itemdetails values ('13', '0002', '1004', '2004');
select * from logindb.itemdetails;

insert into invoices values ('3001', '2021-08-27 3:36:32', '001', '01');
insert into invoices values ('3002', '2021-08-27 3:46:32', '001', '04');
insert into invoices values ('3003', NOW(), '001', '03');
insert into invoices values ('3004', NOW(), '001', '02');
select * from logindb.invoices;

insert into invoicedetails values ('229.000', '0001', '1001');
insert into invoicedetails values ('500.000', '0002', '1002');
insert into invoicedetails values ('1.389.000', '0003', '1003');
insert into invoicedetails values ('799.000', '0004', '1004');
select * from logindb.invoicedetails;



























