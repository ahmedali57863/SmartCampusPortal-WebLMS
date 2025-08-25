CREATE TABLE [dbo].[Fees] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [StudentId] INT NOT NULL,
    [Amount] DECIMAL(10,2) NOT NULL,
    [DueDate] DATE NOT NULL,
    [Status] NVARCHAR(20) NOT NULL, -- e.g., "Paid" or "Unpaid"
    CONSTRAINT FK_Fees_Students FOREIGN KEY (StudentId) REFERENCES Users(Id)
);