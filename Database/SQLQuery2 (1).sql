/*
create table Tovary(
	kod_tovar int not null primary key,
	name varchar(30),
	ed_izm nvarchar(10) default N'шт',
	price decimal(15,2) check(price>0),
	kolvo int check(kolvo>=0))
*/
/*
create table Customers(
	kod_cust int not null identity(101,1),
	name nvarchar(30),
	city nvarchar(20),
	adres nvarchar(100),
	phone decimal(10),
	constraint pk_cust primary key(kod_cust));
	--unique(city,phone));
*/
/*
create table Orders(
	kod_order int not null identity(101,1),
	date_order date not null,
	kol decimal(15,3) check(kol>0),
	amt decimal(14,2),
	kod_cust int not null,
	kod_tovar int not null,
	constraint pk_orders primary key(kod_order),
	constraint fk_ord_cust foreign key(kod_cust) references Customers(kod_cust) on delete cascade on update cascade,
	constraint fk_ord_tov foreign key(kod_tovar) references Tovary(kod_tovar));
	*/