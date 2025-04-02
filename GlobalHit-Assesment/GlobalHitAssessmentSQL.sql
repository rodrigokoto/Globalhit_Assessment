CREATE TABLE Pessoas (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Nome NVARCHAR(100) NOT NULL,
    Nome_Complemento NVARCHAR(100) NOT NULL,
    Rg NVARCHAR(20) NULL,
    Cpf NVARCHAR(15) NULL,
    Email NVARCHAR(100) NULL,
    Telefone NVARCHAR(15) NULL,
    Id_Anexo_Sys INT NULL,
    Data_Alteracao DATETIME NULL,
    Data_Cadastro DATETIME NULL
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Name NVARCHAR(MAX) NOT NULL -- 
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    UserName NVARCHAR(MAX) NOT NULL, 
    Email NVARCHAR(MAX) NULL,
    PasswordHash NVARCHAR(MAX) NULL,
    SecurityStamp NVARCHAR(MAX) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL,
    PhoneNumber NVARCHAR(MAX) NULL,
    EmailConfirmed BIT NOT NULL,
    PhoneNumberConfirmed BIT NOT NULL,
    TwoFactorEnabled BIT NOT NULL,
    LockoutEnd DATETIME NULL,
    LockoutEnabled BIT NOT NULL,
    AccessFailedCount INT NOT NULL
);

CREATE TABLE UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL, 
    PRIMARY KEY (UserId, RoleId), 
    CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

select * from Users




INSERT INTO [dbo].[Roles]
           ([Name])
     VALUES
           ('Admin')
GO

INSERT INTO [dbo].[Roles]
           ([Name])
     VALUES
           ('User')
GO


Create table Proposal (
id int identity(1, 1) not null primary key , 
loanAmount float not null, 
annualInterestRate float not null, 
numberOfMonths int not null 
)



Create table PaymentFlowSummary(
id int identity(1, 1) not null primary key ,
monthlyPayment float not null ,
totalInterest float not null , 
totalPayment float not null, 
)

Create table PaymentSchedule(
id int identity(1,1) not null primary key , 
principal float not null , 
balance float not null , 
interest float not null , 
paymentSummaryId int not null ,
CONSTRAINT FK_PaymentSchedule_Summary FOREIGN KEY (paymentSummaryId) REFERENCES PaymentFlowSummary(Id),
)

CREATE TABLE ProposalPaymentFlowSummary (
    ProposalId INT NOT NULL,
    PaymentFlowSummaryId INT NOT NULL,
    PRIMARY KEY (ProposalId, PaymentFlowSummaryId),
    FOREIGN KEY (ProposalId) REFERENCES Proposal(Id),
    FOREIGN KEY (PaymentFlowSummaryId) REFERENCES PaymentFlowSummary(Id)
)



