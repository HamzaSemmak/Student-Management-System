CREATE DATABASE Student_Management;

use Student_Management;

/* 
*	Table : 
*/ 
Create Table Users (
	ID int primary key identity(1,1),
	Name varchar(45) Unique,
	Password varchar(20),
	UserType varchar(25) default 'User',
	Constraint CK_UserType check (UserType in ('Admin', 'User')),
	Status varchar(25) default 'InLocked',
	Constraint CK_User_Status check (Status in ('Locked', 'InLocked')),
	Checks int default 1,
	Constraint CK_Checks check (Checks <= 5),
);

/* Procedure Stocke */
Create Procedure Authentification
(
	@UserName varchar(255), 
	@Password varchar(20),
	@msg int Out
)
As
Begin 
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
			Update Users Set Checks = 1, Status = 'InLocked' where Name = @UserName And Password = @Password  
			Return @msg End
End
Declare @status int
Execute Authentification 'Hamza Semmak', 'aa102374', @status Output
Select @status

Create Procedure CreateUser
(
	@Name varchar(255), 
	@Password varchar(20),
	@UserType varchar(25),
	@msg int Out
)
As
Begin 
	If Exists (Select * from Users where Name = @Name)
		Begin Set @msg = 40204
		Return @msg End
	Else
		Begin Insert Into Users(Name, Password, UserType) Values(@Name, @Password, @UserType);
		Set @msg = 40205
		Return @msg End
End

Declare @status int
Execute CreateUser 'Test', 'aa102374', 'Admin', @status Output
Select @status

Select *  from Users;