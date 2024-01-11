drop database biff;
create database biff;
use biff;

##################################################
# table_userInfo
-- usernum, userId, userpassword, character1Nick, character2Nick, character3Nick
drop table userInfo;
create table userInfo(
userId varchar(100) not null unique PRIMARY KEY,
userPassword varchar(100) not null
);
desc userInfo;

select * from userInfo;
Insert into userInfo(userId, userPassword) values("theka265", "asd123");
delete from userInfo where userId='s';

select userId as id, userPassword as pw from userInfo where userId = "theka265" && userPassword = "asd123";
select userId as id from userInfo where userId = "theka265";
##################################################
# table_characterInfo
-- num, nickname, 캐릭터 pw, model(F_KimHyeSoo, F_Navi, M_HaJeongWoo, M_MaDongSuck, M_JangChen, M_IronMan)

drop table characterInfo;
create table characterInfo(
userId varchar(100) not null,
foreign key(userId) references userInfo(userId)
on delete cascade
on update cascade,
NickName varchar(100) not null unique primary key,
model varchar(100) not null
);
desc characterInfo;

Insert into characterInfo(NickName, userId, model) values("sodam2", "theka265", "M_HaJeongWoo"); 
Insert into characterInfo(NickName, userId, model) values("sodam1", "theka265", "M_HaJeongWoo"); 
Insert into characterInfo(NickName, userId, model) values("sodam3", "theka265", "M_HaJeongWoo"); 
Insert into characterInfo(NickName, userId, model) values("aram1", "imaram", "F_Navi"); 
Insert into characterInfo(NickName, userId, model) values("aram2", "imaram", "M_JangChen"); 
delete from characterInfo where NickName = 'sdf';
delete from characterInfo where NickName = 'sdfsdf';
delete from characterInfo where NickName = 'sodam3';
delete from characterInfo where NickName = 'adfadf';

select * from characterInfo;
select * from characterInfo where userId = "theka265";
select * from characterInfo where NickName = "sodam1";
Insert into characterInfo(NickName, userId, model) values("sodam2", "theka265", "F_KimHyeSoo"); 
select count(NickName) from characterInfo where userId = "theka265";
##################################################
# user정보 & 캐릭터 정보 조회 view
-- userInfo & characterInfo join
desc userInfo;
desc characterInfo;
drop view v_userCharacterInfo;

create view v_userCharacterInfo
as select a.userId, a.userPassword, b.NickName
from userInfo as a
left join characterInfo as b
on a.userId = b.userId
;
select * from v_userCharacterInfo;
##################################################
# table_nowWearingInfo
-- character가 지금 착용 중인 아이템 정보 table (texture나 modelling 파일 이름 저장)
-- 남 여 구분 -> 여자 모델이면 Bottom은 null
-- characterNick, model_Top,  model_Bottom,  model_Shoes,  model_Hands,  model_Head,  model_Bag,  model_Pet 
-- characterNick, texture_Top,  texture_Bottom,  texture_Shoes,  texture_Hands,  texture_Head,  texture_Bag,  texture_Pet 
drop table nowWearingInfo;
create table nowWearingInfo(
characterNick varchar(100) not null unique primary key,
foreign key(characterNick) references characterInfo(NickName) on delete cascade on update cascade,
Clothes varchar(100),
Hands varchar(100),
Head varchar(100),
Bag varchar(100),
Pet varchar(100),
foreign key(Clothes) references item(name) on delete cascade on update cascade,
foreign key(Hands) references item(name) on delete cascade on update cascade,
foreign key(Head) references item(name) on delete cascade on update cascade,
foreign key(Bag) references item(name) on delete cascade on update cascade,
foreign key(Pet) references item(name) on delete cascade on update cascade
);
desc nowWearingInfo;
insert into nowWearingInfo values("sodam1", "T_Ironman","null","null","A_Wing","P_Slime");
insert into nowWearingInfo values("sodam1", "null","null","null","null","null");
insert into nowWearingInfo(characterNick) values("sodam2");
insert into nowWearingInfo(characterNick) values("sodam3");

update nowWearingInfo set Clothes = 'T_Ironman',  Hands = 'null', Head = 'null', 
Bag = 'A_Wing', Pet = 'P_Slime' where characterNick = 'sodam1';

select * from nowWearingInfo;
select * from nowWearingInfo where characterNick = "sodam1";

delete from nowWearingInfo where characterNick ="sodam1";

##################################################
# table_item
-- itemNum, itemName, gamePrice, cash
-- gamePrice <-> cash
drop table item;
create table item(
name varchar(100) not null unique,
price int not null,
cashprice int
);
desc item;

insert into item(name, price, cashprice) values("T_Ironman", 25000, 0);
insert into item(name, price, cashprice) values("A_Wing", 15000, 0);

insert into item(name, price, cashprice) values("A_Cap", 15000, 0);
insert into item(name, price, cashprice) values("A_Umbrella", 10000, 0);
insert into item(name, price, cashprice) values("P_Slime", 15000, 0);
insert into item(name, price, cashprice) values("null", 0, 0);
delete from item where name = 'I_Wing';


select * from item;
##################################################
# table_inventories
-- num, characterNickName(pk, fk), money, itemslot20개

drop table inventory;
create table inventory(
userId varchar(100) not null,
foreign key(userId) references userInfo(userId)
on delete cascade
on update cascade,
characterNick varchar(100) unique not null primary key,
foreign key(characterNick) references characterInfo(NickName)
on delete cascade
on update cascade,
money int not null,
itemslot1 varchar(100),
itemslot2 varchar(100),
itemslot3 varchar(100),
itemslot4 varchar(100),
itemslot5 varchar(100),
itemslot6 varchar(100),
itemslot7 varchar(100),
itemslot8 varchar(100),
itemslot9 varchar(100),
itemslot10 varchar(100),
itemslot11 varchar(100),
itemslot12 varchar(100),
itemslot13 varchar(100),
itemslot14 varchar(100),
itemslot15 varchar(100),
itemslot16 varchar(100),
itemslot17 varchar(100),
itemslot18 varchar(100),
itemslot19 varchar(100),
itemslot20 varchar(100),
foreign key(itemslot1) references item(name) on delete cascade on update cascade,
foreign key(itemslot2) references item(name) on delete cascade on update cascade,
foreign key(itemslot3) references item(name) on delete cascade on update cascade,
foreign key(itemslot4) references item(name) on delete cascade on update cascade,
foreign key(itemslot5) references item(name) on delete cascade on update cascade,
foreign key(itemslot6) references item(name) on delete cascade on update cascade,
foreign key(itemslot7) references item(name) on delete cascade on update cascade,
foreign key(itemslot8) references item(name) on delete cascade on update cascade,
foreign key(itemslot9) references item(name) on delete cascade on update cascade,
foreign key(itemslot10) references item(name) on delete cascade on update cascade,
foreign key(itemslot11) references item(name) on delete cascade on update cascade,
foreign key(itemslot12) references item(name) on delete cascade on update cascade,
foreign key(itemslot13) references item(name) on delete cascade on update cascade,
foreign key(itemslot14) references item(name) on delete cascade on update cascade,
foreign key(itemslot15) references item(name) on delete cascade on update cascade,
foreign key(itemslot16) references item(name) on delete cascade on update cascade,
foreign key(itemslot17) references item(name) on delete cascade on update cascade,
foreign key(itemslot18) references item(name) on delete cascade on update cascade,
foreign key(itemslot19) references item(name) on delete cascade on update cascade,
foreign key(itemslot20) references item(name) on delete cascade on update cascade
);
desc inventory;
delete from inventory where characterNick = "sodam1";
insert into inventory(userId, characterNick, money,itemslot1) values("theka265","sodam1",  100000, "T_Ironman");
insert into inventory values("theka265","sodam1",  100000, 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 
'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null');
delete from inventory where userId = "theka265";

select * from inventory;
select * from inventory where characterNick = "sodam1";

update inventory set money = '200000', itemslot1 = 'null', itemslot2 = 'null', itemslot3 = 'null', itemslot4 = 'null', itemslot5 = 'null', 
itemslot6 = 'null', itemslot7 = 'null', itemslot8 = 'null', itemslot9 = 'null', itemslot10 = 'null', 
itemslot11 = 'null', itemslot12 = 'A_Umbrella', itemslot13 = 'null', itemslot14 = 'null', itemslot15 = 'null', itemslot16 = 'null', 
itemslot17 = 'null', itemslot18 = 'null', itemslot19 = 'A_Cap', itemslot20 = 'T_Ironman' where characterNick = 'sodam1';

update inventory set money = '200000', itemslot1 = 'null', itemslot2 = 'A_Wing', itemslot3 = 'null', itemslot4 = 'null', itemslot5 = 'A_Cap', 
itemslot6 = 'A_Cap', itemslot7 = 'A_Cap', itemslot8 = 'null', itemslot9 = 'T_Ironman', itemslot10 = 'T_Ironman', 
itemslot11 = 'T_Ironman', itemslot12 = 'A_Cap', itemslot13 = 'null', itemslot14 = 'T_Ironman', itemslot15 = 'A_Cap', itemslot16 = 'A_Cap', 
itemslot17 = 'A_Cap', itemslot18 = 'T_Ironman', itemslot19 = 'A_Cap', itemslot20 = 'T_Ironman' where characterNick = 'sodam1';

##################################################
# table_gameZombieWinInfo
-- 캐릭터 별 게임 승률 정보
-- characterNickName(pk, fk), rank_before, rank_after, 총출전수, 승, 패, 무, 승률
drop table game1WinInfo;
create table game1WinInfo(
userId varchar(100) not null,
foreign key(userId) references userInfo(userId)
on delete cascade
on update cascade,
characterNick varchar(100) unique not null,
foreign key(characterNick) references characterInfo(NickName)
on delete cascade
on update cascade,
ranking int,
gameCount int,
winCount int,
loseCount int,
draw int,
winningRate float8
);
insert into game1WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "김논희", 10,3,7,0.3);
insert into game1WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "맹수콩", 10,7,3,0.7);
delete from game1WinInfo where characterNick = '김논희';
select * from game1WinInfo order by winningrate desc;

##################################################
# table_gameChickenWinInfo
-- characterNickName(pk, fk), rank_before, rank_after, 총출전수, 승, 패, 무, 승률
drop table game2WinInfo;
create table game2WinInfo(
userId varchar(100) not null,
foreign key(userId) references userInfo(userId)
on delete cascade
on update cascade,
characterNick varchar(100) unique not null,
foreign key(characterNick) references characterInfo(NickName)
on delete cascade
on update cascade,
ranking int,
gameCount int,
winCount int,
loseCount int,
draw int,
winningRate float8
);
insert into game2WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "김논희", 10,3,7,0.3);
insert into game2WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "맹수콩", 10,7,3,0.7);
delete from game2WinInfo where characterNick = '김논희';
select * from game2WinInfo order by winningRate desc;
##################################################
# table_game3WinInfo
-- characterNickName(pk, fk), rank_before, rank_after, 총출전수, 승, 패, 무, 승률

drop table game3WinInfo;
create table game3WinInfo(
userId varchar(100) not null,
foreign key(userId) references userInfo(userId)
on delete cascade
on update cascade,
characterNick varchar(100) unique not null,
foreign key(characterNick) references characterInfo(NickName)
on delete cascade
on update cascade,
ranking int,
gameCount int,
winCount int,
loseCount int,
draw int,
winningRate float8
);
insert into game3WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "김논희", 10,3,7,0.3);
insert into game3WinInfo(userId, characterNick, gameCount, winCount, loseCount, winningRate) values ("theka265", "맹수콩", 10,7,3,0.7);
delete from game3WinInfo where characterNick = '김논희';
select * from game3WinInfo order by winningrate desc;