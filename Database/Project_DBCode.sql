drop database if exists chickenfarmdb;
create database chickenfarmdb;

use chickenfarmdb;

create table chicken(
	chicken_id int primary key auto_increment,
    chicken_name varchar(50) not null,
    import_price decimal(20,2) default 0,
    export_price decimal(20,2) default 0,
    description varchar(200)
);

create table chickenstatus(
	chicken_id int not null,
    quantity int not null default 0,
    chicken_status Enum('Giống','Nhỡ','Xuất Chuồng') not null,
    cage_id int not null
);

create table cage(
	cage_id int primary key auto_increment,
    cage_name varchar(50) not null,
    current_capacity int not null default 0,
    max_capacity int not null default 0,
    cage_status enum('Hoạt Động','Đóng','Bảo Trì') not null
);

create table transaction(
	transaction_id int primary key auto_increment,
    title varchar(100) not null,
    content varchar(200),
    amount decimal(20,2) not null default 0,
    transaction_time datetime default now() not null
);

create table user(
	user_id int primary key auto_increment,
    user_name varchar(50) unique not null,
    user_password varchar(50) not null
);

-- alter table chicken auto_increment = 1;
-- creat procedure,trigger,view

-- chicken start --------------------------------------------
DELIMITER $$
CREATE PROCEDURE `AddChicken`(in name varchar(50),in im_price decimal(20,2), in ex_price decimal(20,2), in description varchar(200), out id int)
BEGIN
	insert into chicken(chicken_name,import_price,export_price,description) values
    (name,im_price,ex_price,description);
    select max(chicken_id) into id from chicken;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `DeleteChickenByID`(in id int)
BEGIN
	delete from chicken where chicken_id = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `UpdateChickenInfo`(in name varchar(50),in im_price decimal(20,2), in ex_price decimal(20,2), in des varchar(200),in id int)
BEGIN
	update chicken
    set chicken_name = name,import_price = im_price, export_price = ex_price, description = des
    where chicken_id = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchChickenByID`(in id int)
BEGIN
	select * from chicken where chicken_id  = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchChickenByName`(in name varchar(50))
BEGIN
	select * from chicken where chicken_name  = name;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `GetAllChicken`()
BEGIN
	select * from chicken;
END$$
DELIMITER ;

delimiter $$
create trigger before_chicken_insert before insert 
on chicken for each row
begin
	if(new.import_price < 0) then signal sqlstate '45000' set message_text = 'tg_before_insert: import price must >= 0';
    end if;
    if(new.export_price < 0) then signal sqlstate '45000' set message_text = 'tg_before_insert: export price must >= 0';
    end if;
    if((select count(chicken_id) from chicken where chicken_name = new.chicken_name) = 1) then signal sqlstate '45000' set message_text = 'tg_before_insert: chicken already exists';
    end if;
end $$
delimiter ;

delimiter $$
create trigger before_chicken_update before update
on chicken for each row
begin
	if(new.import_price < 0) then signal sqlstate '45000' set message_text = 'tg_before_update: import price must >= 0';
    end if;
    if(new.export_price < 0) then signal sqlstate '45000' set message_text = 'tg_before_update: export price must >= 0';
    end if;
    if((select count(chicken_id) from chicken where chicken_name = new.chicken_name and chicken_name <> old.chicken_name) = 1) then signal sqlstate '45000' set message_text = 'tg_before_update: chicken already exists';
    end if;
end $$
delimiter ;

-- chicken status start --------------------------------------------

DELIMITER $$
CREATE PROCEDURE `AddStatus`(in id int, in qtity int, in status enum('Giống','Nhỡ','Xuất Chuồng'), in cage int)
BEGIN
	insert into chickenstatus(chicken_id,quantity,chicken_status,cage_id) values
    (id,qtity,status,cage);
END$$
DELIMITER ;

-- DELIMITER $$
-- CREATE PROCEDURE `DeleteChickenByID`(in id int)
-- BEGIN
-- 	delete from chicken where chicken_id = id;
-- END$$
-- DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchChickenStatusByID`(in id int)
BEGIN
	select * from chickenstatus where chicken_id  = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchChickenStatusByStatus`(in status Enum('Giống','Nhỡ','Xuất Chuồng'))
BEGIN
	select * from chickenstatus where chicken_status  = status;
END$$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE `SearchChickenStatus`(in chicken int,in cage int, in status Enum('Giống','Nhỡ','Xuất Chuồng'))
BEGIN
	select * from chickenstatus where chicken_status  = status and chicken_id = chicken and cage_id = cage;
END$$
DELIMITER ;

-- khi chuyen chuong kich hoat trigger tru cua chuong nay + vao chuong kia 
DELIMITER $$
CREATE PROCEDURE `UpdateChickenStatus`(in id int, in qtity int, in status Enum('Giống','Nhỡ','Xuất Chuồng'), in oldCage int, in newcage int)
BEGIN
	update chickenstatus
    set quantity = qtity, cage_id = newCage
    where chicken_id = id and chicken_status = status and cage_id = oldCage;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `GetAllChickenStatus`()
BEGIN
	select * from chickenstatus;
END$$
DELIMITER ;

delimiter $$
create trigger before_chickenstatus_insert before insert 
on chickenstatus for each row
begin
	if(new.quantity < 0) then signal sqlstate '45000' set message_text = 'tg_before_insert: quantity must >= 0';
    end if;
    if((select count(quantity) from chickenstatus where chicken_id = new.chicken_id and chicken_status = new.chicken_status and cage_id = new.cage_id) = 1)
    then signal sqlstate '45000' set message_text = 'tg_before_insert: chicken status already exists';
    end if;
end $$
delimiter ;

delimiter $$
create trigger before_chickenstatus_update before update
on chickenstatus for each row
begin
	if(new.quantity < 0) then signal sqlstate '45000' set message_text = 'tg_before_update: quantity must >= 0';
    end if;
end $$
delimiter ;


-- cage start --------------------------------------------

DELIMITER $$
CREATE PROCEDURE `AddCage`(in name varchar(50), in max_cap int, in cur_cap int ,in status enum('Đóng','Hoạt Động','Bảo Trì'), out id int)
BEGIN
	insert into cage(cage_name,max_capacity,current_capacity,cage_status) values
    (name,max_cap,cur_cap,status);
    select max(cage_id) into id from cage;
END$$
DELIMITER ;

-- DELIMITER $$
-- CREATE PROCEDURE `DeleteChickenByID`(in id int)
-- BEGIN
-- 	delete from chicken where chicken_id = id;
-- END$$
-- DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchCageByID`(in id int)
BEGIN
	select * from cage where cage_id  = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `SearchCageByName`(in name varchar(50))
BEGIN
	select * from cage where cage_name  = name;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `UpdateCage`(in id int, in name varchar(50), in max_cap int, in cur_cap int ,in status Enum('Đóng','Hoạt Động','Bảo Trì'))
BEGIN
	update cage
    set cage_name = name,max_capacity = max_cap,current_capacity = cur_cap,cage_status = status
    where cage_id = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `GetAllCage`()
BEGIN
	select * from cage;
END$$
DELIMITER ;

-- mot chuong dong hoac bao tri se khong the chua ga, khong the update voi trang thai close hoac maintenance voi so ga nhieu hon 0
-- mot chuong so luong hien tai khong bi vuot qua
delimiter $$
create trigger before_cage_insert before insert 
on cage for each row
begin
	if(new.max_capacity <= 0) then signal sqlstate '45000' set message_text = 'tg_before_insert: max capacity must > 0';
    end if;
    if(new.current_capacity < 0) then signal sqlstate '45000' set message_text = 'tg_before_insert: current capacity must >= 0';
    end if;
    -- herreeeeeeeee
    if(new.current_capacity > new.max_capacity) then signal sqlstate '45000' set message_text = 'tg_before_insert: current capacity cannot greater than max capacity';
    end if;
    if(new.current_capacity > 0 and (new.cage_status = 'Đóng' or new.cage_status = 'Bảo Trì')) 
    then signal sqlstate '45000' set message_text = 'tg_before_insert: quantity and status are not compatible';
    end if;
    if((select count(cage_name) from cage where cage_name = new.cage_name) = 1)
    then signal sqlstate '45000' set message_text = 'tg_before_insert: cage already exists';
    end if;
end $$
delimiter ;

delimiter $$
create trigger before_cage_update before update
on cage for each row
begin
	if(new.max_capacity <= 0) then signal sqlstate '45000' set message_text = 'tg_before_update: max capacity must > 0';
    end if;
    if(new.current_capacity < 0) then signal sqlstate '45000' set message_text = 'tg_before_update: current capacity must >= 0';
    end if;
    if(new.current_capacity > new.max_capacity) then signal sqlstate '45000' set message_text = 'tg_before_update: current capacity cannot greater than max capacity';
    end if;
    if(new.current_capacity > 0 and (new.cage_status = 'Đóng' or new.cage_status = 'Bảo Trì')) 
    then signal sqlstate '45000' set message_text = 'tg_before_update: quantity and status are not compatible';
    end if;
    
end $$
delimiter ;


-- transaction start --------------------------------------------


-- user start ----------------------------------------------

DELIMITER $$
CREATE PROCEDURE `SearchUserName`(in userName varchar(50), out id int)
BEGIN
	select user_id from user where user_name = userName;
END$$
DELIMITER ;

 select user_id from user where user_name = 'hakira' and user_password = '123123123';
