CREATE DATABASE Student_Management;

use Student_Management;

/* 
*	Table : 
*/ 
Create Table Users (
	ID int primary key identity(1,1),
	Matricule varchar(10) unique,
	Name varchar(45) Unique,
	Password varchar(20),
	Phone int,
	DateNaissance date,
	Age int,
	Adresse varchar(255),
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

/* Insertion */
insert into FormersType values('Teacher'),('Directeur of School'),('General guard'),('Security'),('Driver'),('Director assistance');

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

/* */	
Select * from Users;
Select * from FormersType;
Select * from LockedUser;
Delete from LockedUser;
Select Count(*) from Users;
