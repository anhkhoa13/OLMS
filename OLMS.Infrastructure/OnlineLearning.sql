CREATE DATABASE OnlineLearningDB;
GO

USE OnlineLearningDB;
GO

-- User Table
CREATE TABLE [User] (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    [Password] NVARCHAR(32) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) UNIQUE NOT NULL,
    Age INT CHECK (Age > 0),
    Role NVARCHAR(20) CHECK (Role IN ('Student', 'Instructor', 'Admin')) NOT NULL
);

-- Student Table (inherits from User)
CREATE TABLE Student (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    Major NVARCHAR(100),
    FOREIGN KEY (Id) REFERENCES [User](Id) ON DELETE CASCADE
);

-- Instructor Table (inherits from User)
CREATE TABLE Instructor (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HireDate DATETIME DEFAULT GETDATE(),
    Department NVARCHAR(100),
    FOREIGN KEY (Id) REFERENCES [User](Id) ON DELETE CASCADE
);

-- Course Table
CREATE TABLE Course (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Code NVARCHAR(6) UNIQUE NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    InstructorID UNIQUEIDENTIFIER,
    FOREIGN KEY (InstructorID) REFERENCES Instructor(Id) ON DELETE SET NULL
);

-- Enrollment Table (Many-to-Many Relationship between Student and Course)
CREATE TABLE Enrollment (
    StudentID UNIQUEIDENTIFIER,
    CourseID UNIQUEIDENTIFIER,
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Student(Id) ON DELETE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Course(Id) ON DELETE CASCADE
);

-- Quiz
CREATE TABLE Quiz (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
	InstructorID UNIQUEIDENTIFIER NOT NULL,
	Code NVARCHAR(6) UNIQUE NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    IsTimeLimited BIT NOT NULL,
	TimeLimit Time NULL,
	NumberOfAttempts INT CHECK (NumberOfAttempts > 0),

	FOREIGN KEY (InstructorID) REFERENCES Instructor(Id) ON DELETE CASCADE,
);

-- many-to-many table
CREATE TABLE QuizCourse (
    QuizID UNIQUEIDENTIFIER NOT NULL,
    CourseID UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (QuizID, CourseID),
    FOREIGN KEY (QuizID) REFERENCES Quiz(Id) ON DELETE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Course(Id) ON DELETE CASCADE
);

CREATE TABLE Question (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
	QuizId UNIQUEIDENTIFIER NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(50) CHECK (QuestionType IN ('MultipleChoice', 'ShortAnswer', 'Essay', 'FillInTheBlanks')) NOT NULL,

    FOREIGN KEY (QuizId) REFERENCES Quiz(Id) ON DELETE CASCADE
);

CREATE TABLE MultipleChoiceQuestion (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Options NVARCHAR(MAX) NOT NULL, -- Stored as a semicolon-separated string
    CorrectOptionIndex INT NOT NULL,
    FOREIGN KEY (Id) REFERENCES Question(Id) ON DELETE CASCADE
);
CREATE TABLE ShortAnswerQuestion (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CorrectAnswer NVARCHAR(500) NOT NULL,

    FOREIGN KEY (Id) REFERENCES Question(Id) ON DELETE CASCADE
);

CREATE TABLE QuizAttempt (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StudentId UNIQUEIDENTIFIER NOT NULL,
    QuizId UNIQUEIDENTIFIER NOT NULL,
    SubmittedAt DATETIME NULL,
    StartTime DATETIME NOT NULL,
    Status INT NOT NULL, 
    Score FLOAT CHECK (Score >= 0) NOT NULL DEFAULT 0,
	AttemptNumber INT CHECK (AttemptNumber > 0),

	FOREIGN KEY (StudentId) REFERENCES Student(Id) ON DELETE CASCADE,
    FOREIGN KEY (QuizId) REFERENCES Quiz(Id) ON DELETE NO ACTION
);

CREATE TABLE StudentResponse  (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    QuizAttemptId UNIQUEIDENTIFIER NOT NULL,
    QuestionId UNIQUEIDENTIFIER NOT NULL,
    ResponseText NVARCHAR(MAX) NOT NULL,
	IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (QuizAttemptId) REFERENCES QuizAttempt(Id) ON DELETE CASCADE,
    FOREIGN KEY (QuestionId) REFERENCES Question(Id) ON DELETE NO ACTION 
);

-- Insert Users (for Students and Instructors)
INSERT INTO [User] (Id, Username, [Password], FullName, Email, Age, Role)
VALUES
    (NEWID(), 'Student1', 'Password123!', 'John Doe', 'john.doe@example.com', 22, 'Student'),
    (NEWID(), 'Student2', 'Password123!', 'Jane Smith', 'jane.smith@example.com', 20, 'Student'),
    (NEWID(), 'Teacher1', 'Password123!', 'Dr. Emily Brown', 'emily.brown@example.com', 40, 'Instructor'),
    (NEWID(), 'Teacher2', 'Password123!', 'Prof. Michael Wilson', 'michael.wilson@example.com', 45, 'Instructor');

-- Insert Students
INSERT INTO Student (Id, EnrollmentDate, Major)
SELECT Id, GETDATE(), 'Computer Science'
FROM [User] WHERE Username = 'Student1';

INSERT INTO Student (Id, EnrollmentDate, Major)
SELECT Id, GETDATE(), 'Information Technology'
FROM [User] WHERE Username = 'Student2';

-- Insert Instructors
INSERT INTO Instructor (Id, HireDate, Department)
SELECT Id, GETDATE(), 'Computer Science Department'
FROM [User] WHERE Username = 'Teacher1';

INSERT INTO Instructor (Id, HireDate, Department)
SELECT Id, GETDATE(), 'Information Technology Department'
FROM [User] WHERE Username = 'Teacher2';
