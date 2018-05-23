CREATE TABLE UserTable (
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
	DueDate DATETIME NOT NULL,
	DoneDate DATETIME,
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

INSERT INTO STATION VALUES  (1103, 'HCAB', 'H.C. Andersens Boulevard'),
						    (2090, 'RISØ/DMU', 'Lille Valb'),
						    (1257, 'Jagtvej', 'Jagtvej'),
							(1259, 'HCØ', 'H. C. Ørsted Institutet'),
							(2091, 'RISØ ENVS', 'RISØ ENVS'),
							(2650, 'Hvidovre', 'Hvidovre'),
							(7060, 'Ulborg', 'Ulborg'),
							(9054, 'Føllesbjerg', 'Keldsnor'),
							(9159, 'Odense tag', 'Odense Rådhus'),
							(9156, 'Odense gade', 'Odense Rådhus'),
							(6160, 'Aarhus Botanisk', 'Botanisk Have'),
							(6003, 'TANGE FPO', 'TANGE FPO'),
							(6001, 'Anholt', 'Anholt');

INSERT INTO EQUIPMENT VALUES ('NOx', 'Gas monitor', 6001),
							 ('CO', 'Gas monitor', 1259),
							 ('O3', 'Gas monitor', 7060),
							 ('SO2', 'Gas monitor', 2091),
							 ('TEOM PM-10', 'Particle monitor', 9159),
							 ('SMPS', 'Particle monitor', 1103),
							 ('LVS PM10', 'Particle monitor', 9054),
							 ('LVS PM 2,5', 'Particle monitor', 2091),
							 ('LVS EC-OC', 'Particle monitor', 2650),
							 ('LVS PM 2,5 ION', 'Particle monitor', 1257),
							 ('FPO', 'Particle monitor', 6160),
							 ('Denuder ap.', 'Particle monitor', 6003),
							 ('LVS PM10 HM', ' Particle monitor', 9156),
							 ('Gas', 'Equipment', 2650),
							 ('Pump', 'Equipment', 6160),
							 ('Tube', 'Equipment', 7060),
							 ('Heads', 'Equipment', 2091),
							 ('Spanskab', 'Equipment', 9054),
							 ('Battery', 'Equipment', 1259);

INSERT INTO TASK VALUES ('Change filters on API monitors', 'Every week', ' ', 'check', '2018-01-03', ' ', ' ', 'Y', 1),
                        ('Check TEOM', 'Every week', ' ', 'check', '2018-08-08 ', ' ', ' ', 'N', 2),
						('Change filter HVS', 'Every week', '2018-03-09', 'check', ' ', ' ', ' ', 'Y', 3),
						('SMPS check ipactor flow (between 0,95-1,05)', 'Every three months', ' ', 'register', '2018-11-09', ' ', ' ', 'N', 4),
						('SMPS check butanol', 'Every week', ' ', 'check', '2018-07-16', ' ', ' ', 'N', 5),
						('SMPS graph/curve - bell shaped', 'Every week', ' ', 'check', '2018-10-12', ' ', ' ', 'N', 6),
						('Change BTX passive sampler', 'Every week', ' ', 'check', '2018-02-03', ' ', ' ', 'Y', 7),
						('FPO filter change', 'Every week', ' ', 'check', '2018-08-17 ', ' ', ' ', 'N', 8),
						('Change filter EC/OC', 'Every two weeks', ' ', 'check', '2018-03-29', ' ', ' ', 'Y', 9),
						('Change filter LVS', 'Every two weeks', ' ', 'check', '2018-12-08', ' ', ' ', 'N', 10),
						('Change impactor, clean nozzles PM heads', 'Every two weeks', '2018-01-19', 'check', ' ', ' ', ' ', 'Y', 11),
						('Denuder tubes change', 'Every two weeks', ' ', 'check', '2018-04-05', ' ', ' ', 'Y', 12),
						('VOC tube change', 'Every three weeks', ' ', 'check', '2018-04-10', ' ', ' ', 'Y', 13),
						('SMPS front impactor clean', 'Every month', ' ', 'check', '2018-11-11', ' ', ' ', 'N', 14),
						('Clean HVS heads', 'Every two months', ' ', 'check', '2018-01-23', ' ', ' ', 'Y', 15),
						('Change API tubes', 'Every three months', ' ', 'check', '2018-05-05', ' ', ' ', 'N', 16),
						('LVS control', 'Every three months', ' ', 'register', ' ', '2018-09-08', ' ', 'N', 17),
						('O3 calibration', 'Every three months', ' ', 'register', '2018-10-03', ' ', ' ', 'N', 18),
						('TEOM flow measurements', 'Every three months', ' ', 'register', '2018-12-12', ' ', ' ', 'N', 19),
						('FPO control/calibration', 'Every three months', ' ', 'register', '2018-02-28', ' ', ' ', 'Y', 2),
						('CO calibration (change CO gas)' , 'Every three months', ' ', 'register', '2018-09-18', ' ', ' ', 'N', 1),
						('NOx calibration (change NO gas)' , 'Every three months', ' ', 'register', '2018-08-18', ' ', ' ', 'N', 3),
						('SO2 gas calibration' , 'Every three months', ' ', 'register', '2018-06-03', ' ', ' ', 'N', 4),
						('SMPS PM head change' , 'Every six months', ' ', 'check', '2018-07-24', ' ', ' ', 'N', 4),
						('SMPS PM1 cyclone clean/change', 'Every six months', ' ', 'check', '2018-12-04', ' ', ' ', 'N', 5),
						('Change LVS EC/OC+ION PM-2,5 head' , 'Every six months', ' ', 'check', '2018-11-06', ' ', ' ', 'N', 6),
						('Change TEOM heads' , 'Every six months', ' ', 'check', '2018-04-19', ' ', ' ', 'Y', 7),
						('LVS cool lamella and main filter changes' , 'Every year', ' ', 'register', '2018-05-11', ' ', ' ', 'N', 8),
						('LVS calibration','Every year', ' ', 'register', '2018-03-14', ' ', ' ', 'Y', 9),
						('Check cool in 0-air cartridge' , 'Every three months', '2018-10-09', 'check', ' ', ' ', ' ', 'N', 10),
						('Check pura fill in 0-air cartriadge', 'Every year', '2018-06-06', 'check', ' ', ' ', ' ', 'N', 11);

INSERT INTO UserTable VALUES ("John", "Smith", "Admin", "pass", "admin");
INSERT INTO UserTable VALUES ("Diya", "B", "Diya", "123", "scientist");
INSERT INTO UserTable VALUES ("Iza", "K", "Iza", "123", "technician");
INSERT INTO UserTable VALUES ("Mimi", "O", "Mimi", "123", "technician");

--1 Get all tasks that are defined as 'register' in the database.
SELECT * FROM dbo.Task WHERE TaskType = 'register'

--2 Get all tasks that should be performed every week.
SELECT * FROM dbo.Task WHERE TaskSchedule = 'Every week'

--3 Put all equipement in order by it's type.
SELECT * FROM dbo.Equipment ORDER BY EquipmentType ASC

--4 Get all users who are defined as 'technicians' in the database.
SELECT * FROM dbo.UserTable WHERE UserType = 'Technician'

--5 Simply count how many tasks are in the database for this moment.
SELECT COUNT(TaskID) FROM dbo.Task 

--6 Get all already performed tasks.
SELECT * FROM dbo.Task WHERE DoneVar = 'Y'

--7 Get all information about Stations
SELECT * FROM dbo.Station ORDER BY StationID

--8 Testing normalization
INSERT INTO STATION VALUES  (1103, 'HCAB', 'H.C. Andersens Boulevard')