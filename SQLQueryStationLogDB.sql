﻿CREATE TABLE UserTable (
	UserID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	NameOfUser NVARCHAR(25) NOT NULL,
	Surname NVARCHAR(25) NOT NULL,
	Username NVARCHAR(50) NOT NULL,
	UserPassword VARCHAR(50) NOT NULL,
	UserType NVARCHAR(20)
	);

CREATE TABLE Station(
	StationID INT NOT NULL PRIMARY KEY,
	StationName NVARCHAR(25) NOT NULL,
	StationAddress NVARCHAR(100) NOT NULL
	);

CREATE TABLE Equipment(
	EquipmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	EquipmentName NVARCHAR(20) NOT NULL,
	EquipmentType NVARCHAR(20) NOT NULL,
	StationID INT FOREIGN KEY REFERENCES Station(StationID)
	);

CREATE TABLE Task(
	TaskID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TaskName NVARCHAR(50) NOT NULL,
	TaskSchedule NVARCHAR(20) NOT NULL,
	Registration NVARCHAR(150),
	TaskType NVARCHAR(15),
	DoneDate DATETIME NOT NULL,
	Comment NVARCHAR(150),
	DoneVar CHAR(1) DEFAULT'N',
	CONSTRAINT CheckIfDone
	CHECK (DoneVar IN('Y','N')),
	EquipmentID INT FOREIGN KEY REFERENCES Equipment(EquipmentID)
	);

CREATE TABLE Notes(
	NotesID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Note VARCHAR(200) NOT NULL,
	DueDate DATETIME,
	StationID  INT FOREIGN KEY REFERENCES Station(StationID),
	UserID INT FOREIGN KEY REFERENCES UserTable(UserID)
	);