IF DB_ID('Hotel') IS NULL
    create database Hotel;
GO	
use Hotel
--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblAbsence')
	drop table tblAbsence;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblEmployee')
	drop table tblEmployee;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblManager')
	drop table tblManager;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblUser')
	drop table tblUser;
IF EXISTS(select * FROM sys.views where name = 'vwAbsence')
	drop view vwAbsence;
IF EXISTS(select * FROM sys.views where name = 'vwEmployee')
	drop view vwEmployee;
IF EXISTS(select * FROM sys.views where name = 'vwManager')
	drop view vwManager;
IF EXISTS(select * FROM sys.views where name = 'vwUser')
	drop view vwUser;
GO
create table tblUser(
UserId int identity(1,1) PRIMARY KEY,
NameAndSurname varchar(100) NOT NULL,
DateOfBirth date NOT NULL,
Email varchar(30) UNIQUE NOT NULL,
Username varchar(40) UNIQUE NOT NULL,
Password varchar(40) NOT NULL
);
create table tblManager(
ManagerId int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
HotelFloor int NOT NULL,
ExperienceWorkingInHotels int NOT NULL,
ProfessionalQualifications varchar(3) NOT NULL,
);
create table tblEmployee(
EmployeeId int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
HotelFloor int NOT NULL,
Gender char NOT NULL,
Citizenship varchar(100) NOT NULL,
Engagement varchar(20) NOT NULL,
Salary numeric(8,2)
);
create table tblAbsence(
AbsenceId int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
FirstDay date NOT NULL,
LastDay date NOT NULL,
Reason varchar(100) NOT NULL,
Status varchar(20) NOT NULL
);
GO
create view vwUser as
select *
from tblUser;
GO
create view vwManager as
select u.*, m.ManagerId, m.ExperienceWorkingInHotels, m.HotelFloor, m.ProfessionalQualifications
from tblUser u
INNER JOIN tblManager m
ON u.UserId = m.UserId;
GO
create view vwEmployee as
select u.*, e.EmployeeId , e.Citizenship, e.Engagement, e.Gender, e.HotelFloor, e.Salary
from tblUser u
INNER JOIN tblEmployee e
ON u.UserId = e.UserId;
GO
create view vwAbsence as
select a.* , u.NameAndSurname
from tblAbsence a
INNER JOIN tblUser u
ON u.UserId = a.UserId;