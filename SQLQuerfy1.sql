CREATE TABLE [dbo].[Attendance] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CourseId] INT NOT NULL,
    [StudentId] INT NOT NULL,
    [Date] DATE NOT NULL,
    [Status] NVARCHAR(10) NOT NULL, -- e.g., "Present" or "Absent"
    CONSTRAINT FK_Attendance_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    CONSTRAINT FK_Attendance_Students FOREIGN KEY (StudentId) REFERENCES Users(Id)
);