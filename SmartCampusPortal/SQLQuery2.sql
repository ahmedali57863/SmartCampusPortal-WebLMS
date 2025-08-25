CREATE TABLE [dbo].[Announcements] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CourseId] INT NOT NULL,
    [FacultyId] INT NOT NULL,
    [Message] NVARCHAR(500) NOT NULL,
    [DatePosted] DATETIME NOT NULL,
    CONSTRAINT FK_Announcements_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    CONSTRAINT FK_Announcements_Faculty FOREIGN KEY (FacultyId) REFERENCES Users(Id)
);