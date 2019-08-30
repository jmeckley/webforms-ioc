create database MyDatabase;
go

use MyDatabase;
go

create table [Table] (
	  [Id] int not null identity
	, [Name] varchar(50) not null
	, [Age] int not null
	, constraint [PK_Table] primary key ([Id])
);
go