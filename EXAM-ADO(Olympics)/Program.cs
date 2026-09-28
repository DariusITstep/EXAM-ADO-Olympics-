namespace EXAM_ADO_Olympics_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OlympicsEntities())
            {
                UI.ShowMainMenu(db);
            }
        }
    }
}


//CREATE DATABASE Olympics;
//GO

//USE Olympics;
//GO

//---Countries

//CREATE TABLE Countries
//(
//    Id INT IDENTITY PRIMARY KEY,
//    [Name] NVARCHAR(100) NOT NULL UNIQUE,
//    IsActive BIT NOT NULL DEFAULT 1
//);

//---Cities

//CREATE TABLE Cities
//(
//    Id INT IDENTITY PRIMARY KEY,
//    [Name] NVARCHAR(100) NOT NULL,
//    IsActive BIT NOT NULL DEFAULT 1,

//    CountryId INT NOT NULL FOREIGN KEY REFERENCES Countries(Id),
//    UNIQUE ([Name], CountryId)
//);

//---Olympiads

//CREATE TABLE Olympiads
//(
//    Id INT IDENTITY PRIMARY KEY,
//    [Year] INT NOT NULL CHECK ([Year] >= 1896),
//    [Type] NVARCHAR(10) NOT NULL CHECK ([Type] IN ('Summer', 'Winter')),
//    IsActive BIT NOT NULL DEFAULT 1,

//    HostCountryId INT NOT NULL  FOREIGN KEY REFERENCES Countries(Id),
//    HostCityId INT NOT NULL FOREIGN KEY REFERENCES Cities(Id),
//    UNIQUE ([Year], [Type])
//);

//---Sports

//CREATE TABLE Sports
//(
//    Id INT IDENTITY PRIMARY KEY,
//    [Name] NVARCHAR(100) NOT NULL UNIQUE,
//    IsActive BIT NOT NULL DEFAULT 1
//);

//---Disciplines

//CREATE TABLE Disciplines
//(
//    Id INT IDENTITY PRIMARY KEY,
//    [Name] NVARCHAR(100) NOT NULL,
//    IsActive BIT NOT NULL DEFAULT 1,

//    SportId INT NOT NULL FOREIGN KEY REFERENCES Sports(Id),
//    UNIQUE ([Name], SportId)
//);

//---Athletes

//CREATE TABLE Athletes
//(
//    Id INT IDENTITY PRIMARY KEY,
//    FirstName NVARCHAR(50) NOT NULL,
//    LastName NVARCHAR(50) NOT NULL,
//    BirthDate DATE NOT NULL CHECK (BirthDate <= GETDATE()),
//    IsActive BIT NOT NULL DEFAULT 1,

//    CountryId INT NOT NULL FOREIGN KEY REFERENCES Countries(Id)
//);

//---Participations

//CREATE TABLE Participations
//(
//    Id INT IDENTITY PRIMARY KEY,
//    Result NVARCHAR(100) NOT NULL DEFAULT 'Empty',
//    Medal NVARCHAR(10) NOT NULL DEFAULT 'Empty' CHECK (Medal IN ('Gold', 'Silver', 'Bronze', 'Empty')),
//    IsActive BIT NOT NULL DEFAULT 1,

//    OlympiadId INT NOT NULL FOREIGN KEY REFERENCES Olympiads(Id),
//    AthleteId INT NOT NULL FOREIGN KEY REFERENCES Athletes(Id),
//    DisciplineId INT NOT NULL FOREIGN KEY REFERENCES Disciplines(Id),
//    UNIQUE (OlympiadId, AthleteId, DisciplineId),
//    CHECK (NOT(Result = 'Empty' AND Medal<> 'Empty'))
//);

//---Insert...

//---Countries
//INSERT INTO Countries(Name)
//VALUES
//('Ukraine'),
//('USA'),
//('Japan'),
//('Norway');

//---Cities
//INSERT INTO Cities(Name, CountryId)
//VALUES
//('Kyiv', 1),
//('Los Angeles', 2),
//('Tokyo', 3),
//('Lillehammer', 4);

//---Olympiads
//INSERT INTO Olympiads([Year], [Type], HostCountryId, HostCityId)
//VALUES
//(2020, 'Summer', 3, 3),
//(2028, 'Summer', 2, 2),
//(1994, 'Winter', 4, 4);

//---Sports
//INSERT INTO Sports(Name)
//VALUES
//('Athletics'),
//('Swimming'),
//('Biathlon');

//---Disciplines
//INSERT INTO Disciplines(Name, SportId)
//VALUES
//('100m', 1),
//('200m Freestyle', 2),
//('10km Sprint', 3);

//---Athletes
//INSERT INTO Athletes(FirstName, LastName, BirthDate, CountryId)
//VALUES
//('Oleksandr', 'Kovalenko', '1998-05-12', 1),
//('Michael', 'Johnson', '1995-08-21', 2),
//('Haruto', 'Sato', '1999-03-15', 3),
//('Erik', 'Hansen', '1997-11-02', 4);

//---Participations
//INSERT INTO Participations(OlympiadId, AthleteId, DisciplineId, Result, Medal)
//VALUES
//(1, 1, 1, '10.12 s', 'Bronze'),
//(1, 2, 1, '9.98 s', 'Gold'),
//(1, 3, 2, '1:45.30', 'Silver'),
//(3, 4, 3, '25:18', 'Gold');