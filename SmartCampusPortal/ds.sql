CREATE TABLE [dbo].[Submissions] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [AssignmentId] INT NOT NULL,
    [UserId] INT NOT NULL,
    [SubmissionText] NVARCHAR(1000),
    [Grade] NVARCHAR(10),
    [SubmissionFilePath] NVARCHAR(200) NULL,
    CONSTRAINT FK_Submissions_Assignments FOREIGN KEY (AssignmentId) REFERENCES Assignments(Id),
    CONSTRAINT FK_Submissions_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);