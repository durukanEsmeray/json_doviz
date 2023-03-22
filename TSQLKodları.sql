create database Doviz
go
use Doviz
go

create table ParaBirimi
(
ID uniqueidentifier primary key,
Code nvarchar(8),
Tanim nvarchar(70),
UyariLimit decimal
)
go

insert into ParaBirimi (ID,Code,Tanim,UyariLimit) Values (newid(),'USD','Amerikan Dolarý',19.02)
insert into ParaBirimi (ID,Code,Tanim,UyariLimit) Values (newid(),'EUR','EURO',0)
insert into ParaBirimi (ID,Code,Tanim,UyariLimit) Values (newid(),'GBP','Ýngiliz Sterlini',0)

select * from ParaBirimi

create table Kur
(
ID uniqueidentifier primary key,
ParaBirimiID uniqueidentifier,
Alis decimal,
Satis decimal,
OlusturmaTarih datetime
)

go

create table KurGecmis
(
ID uniqueidentifier primary key,
KurID uniqueidentifier,
ParaBirimiID uniqueidentifier,
Alis decimal,
Satis decimal,
OlusturmaTarih datetime
)

go

create proc KurKayitEkle
(
@ID uniqueidentifier,
@ParaBirimiID uniqueidentifier,
@Alis decimal,
@Satis decimal,
@OlusturmaTarih datetime
)
as
begin

if((select count(*) from Kur where ParaBirimiID = @ParaBirimiID)>0)
begin
insert into KurGecmis (ID, KurID, ParaBirimiID,Alis,Satis,OlusturmaTarih) SELECT NEWID(),ID, ParaBirimiID, Alis,Satis, OlusturmaTarih 
from Kur where ParaBirimiID = @ParaBirimiID

update Kur set
Alis = @Alis,
Satis = @Satis
where
ParaBirimiID = @ParaBirimiID
end

else
begin
insert into Kur (ID, ParaBirimiID,Alis,Satis,OlusturmaTarih) values (@ID, @ParaBirimiID,@Alis,@Satis,@OlusturmaTarih)
end

end