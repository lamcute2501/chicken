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
    chicken_status Enum('Giong','Dang Lon','Xuat Chuong') not null,
    cage_id int not null,
    constraint pk_chickenstatus primary key (chicken_id)
--     constraint pk_chickenstatus primary key (chicken_status),
    -- constraint pk_chickenstatus primary key (cage_id)
    -- constraint fk_chickenstatus_chicken foreign key (chicken_id) references chicken(chicken_id)
);

create table cage(
	cage_id int primary key auto_increment,
    cage_name varchar(50) not null,
    max_capacity int not null default 0,
    current_capacity int not null default 0,
    cage_status enum('Open','Close','Maintenance') not null
);

create table transaction(
	transaction_id int primary key auto_increment,
    title varchar(100) not null,
    content varchar(200),
    amount decimal(20,2) not null default 0,
    transaction_time datetime default now() not null
);


-- alter table chicken auto_increment = 1;
-- creat procedure,trigger,view

-- chicken start --------------------------------------------
DELIMITER $$
CREATE PROCEDURE `AddChicken`(in name varchar(50),in im_price decimal(20,2), in ex_price decimal(20,2), in description varchar(200))
BEGIN
	insert into chicken(chicken_name,import_price,export_price,description) values
    (name,im_price,ex_price,description);
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `DeleteChickenByID`(in id int)
BEGIN
	delete from chicken where chicken_id = id;
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `UpdateChickenInfo`(in name varchar(50),in im_price decimal(20,2), in ex_price decimal(20,2), in description varchar(200),in id int)
BEGIN
	update chicken
    set chicken_name = name,import_price = im_price, export_price = ex_price, description = description
    where chicken_id = id;
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
    if((select count(new.chicken_name) from chicken) = 1) then signal sqlstate '45000' set message_text = 'tg_before_insert: chicken already exists';
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
    if((select count(new.chicken_name) from chicken) = 1) then signal sqlstate '45000' set message_text = 'tg_before_update: chicken already exists';
    end if;
end $$
delimiter ;



-- chicken status start --------------------------------------------

DELIMITER $$
CREATE PROCEDURE `AddStatus`(in id int, in quantity int, in status enum('Giong','Dang Lon','Xuat Chuong'), in cage int)
BEGIN
	insert into chickenstatus(chicken_id,quantity,chicken_status,cage_id) values
    (id,quantity,status,cage);
END$$
DELIMITER ;

-- DELIMITER $$
-- CREATE PROCEDURE `DeleteChickenByID`(in id int)
-- BEGIN
-- 	delete from chicken where chicken_id = id;
-- END$$
-- DELIMITER ;

-- khi chuyen chuong kich hoat trigger tru cua chuong nay + vao chuong kia 
DELIMITER $$
CREATE PROCEDURE `UpdateChickenStatus`(in id int, in quantity int, in status Enum('Giong','Dang Lon','Xuat Chuong'), in cage int)
BEGIN
	update chickenstatus
    set quantity = quantity
    where chicken_id = id and chicken_status = status and cage_id = cage; -- hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
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
CREATE PROCEDURE `AddCage`(in name varchar(50), in max_cap int, in cur_cap int ,in status enum('Open','Close','Maintenance'))
BEGIN
	insert into cage(cage_name,max_capacity,current_capacity,cage_status) values
    (name,max_cap,cur_cap,status);
END$$
DELIMITER ;

-- DELIMITER $$
-- CREATE PROCEDURE `DeleteChickenByID`(in id int)
-- BEGIN
-- 	delete from chicken where chicken_id = id;
-- END$$
-- DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `UpdateCage`(in id int, in name varchar(50), in max_cap int, in cur_cap int ,in status Enum('Open','Close','Maintenance'))
BEGIN
	update cage
    set cage_name = name,max_capacity = max_cap,current_capacity = cur_cap,cage_status = status
    where cage_id = id;
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
    if(new.current_capacity > 0 and (new.cage_status = 'Close' or new.cage_status = 'Maintenance')) 
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
    if(new.current_capacity > (old.max_capacity or new.max_capacity)) then signal sqlstate '45000' set message_text = 'tg_before_update: current capacity cannot greater than max capacity';
    end if;
    if(new.current_capacity > 0 and (new.cage_status = 'Close' or new.cage_status = 'Maintenance')) 
    then signal sqlstate '45000' set message_text = 'tg_before_update: quantity and status are not compatible';
    end if;
end $$
delimiter ;


-- transaction start --------------------------------------------


