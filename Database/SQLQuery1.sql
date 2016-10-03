/* ГЛАВА 3 */

/*
/* 1 задание */
select onum, snum, odate from Orders 

/* 4 задание */
select * from Orders
where snum = 1001

/* 5 задание */
select city, sname, snum, comm from	Salespeople

/* 8 задание */
select sname, comm from Salespeople
where city = 'London'

/* 10 задание */
select distinct cnum from Orders


/* ГЛАВА 4 */


/* 1 задание */
select * from Orders
where amt > 1000

/* 4 задание */
select cname, city from Customers
where city in ('San Jose') and rating > 150

/* 7 задание */
select * from Salespeople 
where city = 'London' and comm < 0.11 or comm > 0.13


/* ГЛАВА 5 */


/* 1 задание */
select * from Orders
where odate in ('2015-03-10','2015-06-10')

/* 5 задание */
select * from Customers
where cname between 'A' and 'G'

/* 8 задание */
select * from Salespeople
where sname like '%s'

*/
/* ГЛАВА 6 */

/*
/* 1 задание */
select sum(amt) as amt from Orders
where odate = '2015-03-10'

/* 6 задание */
select max(odate) as odate, cnum from Orders
group by cnum

/* 12 задание */
select count(distinct cnum), odate from Orders
group by odate


/* ГЛАВА 7 */


/* 1 задание */
select Orders.onum, Orders.snum, Salespeople.comm from Orders, Salespeople
where Orders.snum = Salespeople.snum

/* 4 задание */
select city, min(comm) as comm from Salespeople
group by city

/* 8 задание */
select cnum, sum(amt) as amt from Orders
group by cnum
order by amt

*/
/* ГЛАВА 8 */
/*

/* 1 задание */
select Orders.onum, Customers.cname from Orders, Customers
where Orders.cnum = Customers.cnum

/* 4 задание */
select Orders.onum, Customers.cname, Salespeople.sname from Orders, Customers, Salespeople
where Orders.cnum = Customers.cnum and Orders.snum = Salespeople.snum

/* 8 задание */

select onum, amt * (1 + Salespeople.comm) as total from Orders, Customers, Salespeople
where Orders.cnum = Customers.cnum and Orders.snum = Salespeople.snum and Customers.city = 'London'

*/
/* ГЛАВА 9 */


/* 1 задание */
select s1.sname, s2.sname, s1.city from Salespeople s1, Salespeople s2 
where s1.city = s2.city and s1.sname < s2.sname

/* 3 задание */
select c1.cname, c2.cname, o1.odate from Customers c1, Customers c2, Orders o1, Orders o2
where c1.cnum = o1.cnum and c2.cnum = o2.cnum and o1.odate = o2.odate and o1.onum<o2.onum

/* 5 задание */
select с2.cname, с2.city from Customers с1, Customers с2
where с1.cname = 'Hoffman' and с1.rating = с2.rating and с2.cname<>'Hoffman'

 /*

 select * from Salespeople
 where 0 < (select count(*) from Orders
				where Salespeople.snum = Orders.snum)
*/


/* ГЛАВА 10 */

/* 1 задание */
select * from Orders
where cnum = (select cnum from Customers
				where cname = 'Cisneros')

/* 3 задание */

select c.cname, c.cnum, rating, o.amt from Customers c, Orders o
where amt > (select avg(amt) from Orders)


/* 5 задание */


/* ГЛАВА 11-12 */

/* 1 задание */



