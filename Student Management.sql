CREATE DATABASE Student_Management;

use Student_Management;

/* 
*	Table : 
*/ 
Create Table Users (
	ID int primary key identity(1,1),
	Matricule varchar(10) unique,
	Name varchar(45) Unique,
	Password varchar(10),
	Phone varchar(45),
	DateNaissance varchar(20),
	Age int,
	City varchar(255),
	FormerType varchar(255),
	UserType varchar(25) default 'User',
	Constraint CK_UserType check (UserType in ('Admin', 'User')),
	Status varchar(25) default 'InLocked',
	Constraint CK_User_Status check (Status in ('Locked', 'InLocked')),
	Checks int default 1,
	Constraint CK_Checks check (Checks <= 5),
);

Create Table LockedUser (
	ID int primary key,
	ID_User int foreign key references Users(ID)
);

Create Table FormersType(
	ID int primary key identity(1,1),
	type varchar(255)
);

Create Table Students (
	ID int primary key,
	Matricule varchar(10) unique,
	Name varchar(45) Unique,
	DateNaissance varchar(20),
	Age int,
	City varchar(255),
	Adresse varchar(255),
	Gender varchar(20),
	Constraint Ck_Students_Gender check (Gender in ('Male', 'Female')),
);

Create Table Parents (
	ID int primary key,
	ID_Students int foreign key references Students(ID),
	Father_Name varchar(255),
	Father_CIN varchar(15),
	Father_DateNaissance varchar(10),
	Mother_Name varchar(255),
	Mother_CIN varchar(15),
	Mother_DateNaissance varchar(10),
);

/* Insertion */
insert into FormersType Values
('Teacher'),('Directeur of School'),('General guard'),('Security'),('Driver'),('Director assistance');
insert into Users values
('AA10274','Hamza Semmak','AA102374','0667786555','2001-07-28',21,'Rabat','Directeur of School','Admin','InLocked',1),
('KKA1050','Karim Aissa','Karim2001','0662504036','2001-06-05',21,'Rabat','Director assistance','Admin','InLocked',1),
('AbY7080','Yacine Abnai','Yacine4040','0668689530','2001-08-05',24,'Rabat','General guard','Admin','InLocked',1),
('TTR5070','Tarik Oulkhabou','Tarik2001','0825361452','1998-06-05',24,'Medelt','General guard','Admin','InLocked',1),
('SAIT404','Soufiane Ait Hammou','Souf1999','0452639874','1999-06-05',24,'Sale','Teacher','Admin','InLocked',1),
('OMAM404','Omar Amoun','Omar1950','0632323232','1993-12-20',30,'Kenitra','Teacher','User','InLocked',1),
('AKBAO20','Zakaria Baoune','ZkBa5065','046568705','1998-11-25',25,'Khenifra','Teacher','User','InLocked',1);

/* Requetes : */	
Select * from Users;
Select * from Students;
Select * from FormersType;
Select Count(*) from FormersType;
Select * from LockedUser;
Delete from LockedUser;
Select Count(*) from Users;
Select Count(*) from Students;
Delete from Users where ID = 14;

/* Procedure Stocke */
Create Procedure Authentification
(
	@UserName varchar(255), 
	@Password varchar(20),
	@msg int Out
)
As
Begin 
	Declare @ID int
	If Exists (Select COUNT(*) from Users where Name = @UserName Having COUNT(*) = 0 )
		Begin Set @msg = 40201 /* UserName and Password is Incorrect */
		Return @msg End
	Else If Exists (Select * from Users where Name = @UserName)
		If Exists (Select * from Users where Name = @UserName and Checks = 5)
			Begin Set @msg = 40203 /* Locked Account */
			Update Users Set Checks = 5, Status = 'Locked' where Name = @UserName
			Return @msg End
		Else If Not Exists (Select * from Users where Name = @UserName And Password = @Password)
			Begin Set @msg = 40202 /*Password is Incorrect */
			Update Users Set Checks = Checks + 1 where Name = @UserName
			Return @msg End
		Else If Exists (Select * from Users where Name = @UserName And Password = @Password)
			Begin Set @msg = 40200 /* UserName and Password Corrrect */
			Select @ID = ID from Users where Name = @UserName And Password = @Password;
			Insert into LockedUser(ID, ID_User) Values(@ID, @ID)
			Update Users Set Checks = 1, Status = 'InLocked' where Name = @UserName And Password = @Password
			
			Return @msg End
End
Declare @status int
Execute Authentification 'Hamza Semmak', 'test1', @status Output
Select @status

Create Procedure CheckUserIfAdmin
(
	@ID int, 
	@msg int Out
)
As
Begin 
	If Exists (Select COUNT(*) from Users where ID = @ID Having COUNT(*) = 0 )
		Begin Set @msg = 40210 Return @msg End /* User Dons't Esict */
	Else
		If Exists (Select * from Users where ID = @ID and UserType = 'Admin')
			Begin Set @msg = 40211 Return @msg End /*User is Admin */
		Else 
			Begin Set @msg = 40212 Return @msg End /*User is User */
End
Declare @status int
Execute CheckUserIfAdmin 9, @status Output
Select @status

Create Procedure CreateUser
(
	@matricule varchar(10),
	@name varchar(45),
	@password varchar(10),
	@phone varchar(45),
	@dateNaissance varchar(20),
	@age int,
	@city varchar(255),
	@formerType varchar(255),
	@userType varchar(25),
	@msg int Out
)
As
Begin
	If Exists (Select COUNT(*) from Users where Matricule = @matricule Having COUNT(*) > 0 )
		Begin Set @msg = 40221 Return @msg End /* Matricule Exists */
	Else If Exists (Select COUNT(*) from Users where Name = @name Having COUNT(*) > 0 )
		Begin Set @msg = 40222 Return @msg End /* Name Exists */
	Else If Exists (Select COUNT(*) from Users where Password = @password Having COUNT(*) > 0 )
		Begin Set @msg = 40223 Return @msg End /* Password Exists */
	Else
		Insert into Users values(@matricule, @name, @password, @phone, @dateNaissance, @age, @city, @formerType, @userType, 'InLocked', 1);
		Begin Set @msg = 40224 Return @msg End /* (1 row(s) affected) */
End
Declare @status int
Execute CreateUser 'AAZZEE', 'Mohemed Semmak', 'MM102345', 0666666666, '2001-07-28', 21, 'Rabat', 'Teacher', 'User', @status Output
Select @status

Create Procedure DeleteFormer
(
	@matricule varchar(10),
	@msg int Out
)
As
Begin
	Declare @ID int
	Select @ID = ID from Users where Matricule = @matricule
	If Exists (Select COUNT(*) from LockedUser where ID = @ID Having Count(*) > 0)
		Begin 
			Delete from LockedUser where ID = @ID
			Delete from Users where Matricule = @matricule
			Set @msg = 40311 Return @msg 
		End /* Authentificate User */
	If Exists (Select COUNT(*) from Users where ID = @ID Having Count(*) = 0)
		Begin 
			Set @msg = 40312 Return @msg 
		End /* Former is Undefiened */
	Else 
		Begin 
			Delete from Users where Matricule = @matricule
			Delete from LockedUser where ID = @ID
			Set @msg = 40310 Return @msg 
		End /* Delete Former */
End
Declare @status int
Execute DeleteFormer 'rFGhayFkYA', @status Output
Select @status

Create Procedure CreateStudent
(
	@matricule varchar(10),
	@nameOfStudents varchar(45),
	@dateNaissance varchar(10),
	@age int,
	@city varchar(255),
	@adresse varchar(255),
	@gender varchar(20),
	@fatherName varchar(255),
	@fatherCIN varchar(15),
	@fatherDateNaissance varchar(10), 
	@motherName varchar(255),
	@motherCIN varchar(15),
	@motherDateNaissance varchar(10),
	@msg int Out
)
As
Begin
	Declare @ID int
	Select @ID = Count(*) from Students;
	Set @ID = @ID + 1
	IF Exists (Select COUNT(*) from Students where Matricule = @matricule Having COUNT(*) > 0 )
		Begin Set @msg = 10101 Return @msg End /* Matricule Exists */
	IF Exists (Select COUNT(*) from Students where Name = @nameOfStudents Having COUNT(*) > 0 )
		Begin Set @msg = 10102 Return @msg End /* Name Of Student Exists */
	Else 
		Begin
			Insert into Students values (@ID, @matricule, @nameOfStudents, @dateNaissance, @age, @city, @adresse, @gender)
			Insert into Parents values (@ID, @ID, @fatherName, @fatherCIN, @fatherDateNaissance, @motherName, @motherCIN, @motherDateNaissance);
			Set @msg = 10100 Return @msg  /* Students Created Successfly */
		End
End
Declare @status int
Execute CreateStudent 'rFGhayFksA', 'Student2', '2001-01-28', 22,
		 'Rabat', 'IMM 7 APPT 13 CYM Rabat', 'Male', 'Student1Father',
		 'AA102374s', '1989-02-02', 'Student2Mother',
		 'AA102384s', '1989-02-02', @status Output
Select @status