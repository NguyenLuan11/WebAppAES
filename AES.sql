--DROP DATABASE AES;
GO
CREATE DATABASE AES;
GO
USE AES;
GO
CREATE TABLE Class(
	ClassId VARCHAR(8) NOT NULL PRIMARY KEY,
	ClassName NVARCHAR(64) NOT NULL
);
GO
INSERT INTO Class  VALUES
	('TMDT', N'Thương Mại Điện Tử'),
	('THMT', N'Tin Học Môi Trường'),
	('TTMT', N'Thông Tin Môi Trường'),
	('CNPM1', N'Công Nghệ Phầm Mềm 1'),
	('CNPM2', N'Công Nghệ Phầm Mềm 2'),
	('CNPM3', N'Công Nghệ Phầm Mềm 3');
GO
--SELECT * FROM Class;
--DROP TABLE Student;
--C#,java, php
--SQL
GO
CREATE TABLE Student(
	StudentId VARCHAR(10) NOT NULL PRIMARY KEY,
	ClassId VARCHAR(8) NOT NULL REFERENCES Class(ClassId),
	LastName NVARCHAR(32) NOT NULL,
	FirstName NVARCHAR(16) NOT NULL,
	Email VARCHAR(64) NOT NULL,
	Gender BIT NOT NULL,
	Score VARBINARY(64) NOT NULL
);
--SELECT * FROM Student;
--DELETE FROM student where ClassId = 'TMDT';

--SELECT ClassId, AVG(Score) AS Mean 
--	FROM Student GROUP BY ClassId;
--Encrypt database => ko dung C#

--Hash => dùng C#

