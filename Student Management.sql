CREATE DATABASE Student_Management;

use Student_Management;

/* 
*	Table : 
*/

Create Table Users (
	ID int primary key identity(1,1),
	Name varchar(255) Unique,
	Password varchar(255),
	Email varchar(255),
	Phone varchar(10),
	Status varchar(25) default 'InLocked',
	Constraint CK_User_Status check (Status in ('Locked', 'InLocked')),
	Checks int default 1,
	Constraint CK_Checks check (Checks <= 5),
);

/* Procedure Stocke */
Create Procedure Authentification
(
	@UserName varchar(255), 
	@Password varchar(255),
	@msg int Out
)
As
Begin 
	If exists (Select * from Users where Name = @UserName)
		Begin Set @msg = 200 /* Test Passed Succefly */
		Return @msg End
	Else
		Set @msg = 201 /* UserName and Password is Incorrect */
		Return @msg
End

Declare @status int
Execute Authentification 'test', 'test', @status Output
Select @status

Select * from Users;