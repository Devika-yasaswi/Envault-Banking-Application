CREATE DATABASE Envault
USE Envault

CREATE TABLE Envault_Tbl_BasicDetails(CustomerId BIGINT IDENTITY(10000000,1) PRIMARY KEY,
									  CustomerName NVARCHAR(100) NOT NULL,
									  DateOfBirth DATE NOT NULL,
									  Gender NVARCHAR(50),
									  AadharNumber BIGINT CHECK(LEN(AadharNumber)=12) NOT NULL UNIQUE,
									  MobileNumber BIGINT CHECK(LEN(MobileNumber)=10) NOT NULL,
									  PANNumber NVARCHAR(10) UNIQUE NOT NULL,
									  CustomerEmail NVARCHAR(100) UNIQUE NOT NULL,
									  EmployementType NVARCHAR(100) NOT NULL,
									  AnnualIncome BIGINT NOT NULL,
									  Nationality BIT NOT NULL,
									  TaxResidentOfIndia BIT NOT NULL,
									  IsActive BIT DEFAULT 1,
									  CreatedBy NVARCHAR(100),
									  CreatedOn DATETIME2,
									  ModifiedBy NVARCHAR(100),
									  ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_CustomerAddress(CustomerId BIGINT FOREIGN KEY REFERENCES Envault_Tbl_BasicDetails(CustomerId) PRIMARY KEY,
										 PermanentHouseNo NVARCHAR(10) NOT NULL,
										 PermanentStreet NVARCHAR(100) NOT NULL,
										 PermanentCity NVARCHAR(50) NOT NULL,
										 PermanentState NVARCHAR(50) NOT NULL,
										 PermanentPincode INT CHECK(LEN(PermanentPincode)=6) NOT NULL,
										 PresentHouseNo NVARCHAR(10) NOT NULL,
										 PresentStreet NVARCHAR(100) NOT NULL,
										 PresentCity NVARCHAR(50) NOT NULL,
										 PresentState NVARCHAR(50) NOT NULL,
										 PresentPincode INT CHECK(LEN(PresentPincode)=6) NOT NULL,
										 IsActive BIT,
										 CreatedBy NVARCHAR(100),
										 CreatedOn DATETIME2,
										 ModifiedBy NVARCHAR(100),
										 ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_CustomerFamilyAndNomineeDetails(CustomerId BIGINT FOREIGN KEY REFERENCES Envault_Tbl_BasicDetails(CustomerId) PRIMARY KEY,
														 FatherName NVARCHAR(100) NOT NULL,
														 MotherName NVARCHAR(100) NOT NULL,
														 SpouseName NVARCHAR(100),
														 NomineeName NVARCHAR(100) NOT NULL,
														 NomineeDateOfBirth DATE NOT NULL,
														 RelationWithNominee NVARCHAR(20),
														 NomineeHouseNo NVARCHAR(10),
														 NomineeStreet NVARCHAR(100),
														 NomineeCity NVARCHAR(50),
														 NomineeState NVARCHAR(50),
														 NomineePincode INT CHECK(LEN(NomineePincode)=6),
														 IsActive BIT DEFAULT 1,
														 CreatedBy NVARCHAR(100),
														 CreatedOn DATETIME2,
														 ModifiedBy NVARCHAR(100),
														 ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_AccountType(AccountTypeId INT IDENTITY(1,1) PRIMARY KEY,
									 AccountType NVARCHAR(50),
									 IsActive BIT DEFAULT 1,
									 CreatedBy NVARCHAR(100),
									 CreatedOn DATETIME2,
									 ModifiedBy NVARCHAR(100),
									 ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_BranchesAvailable(BranchID INT IDENTITY(1,1) PRIMARY KEY,
										   BranchLocatedState NVARCHAR(50),
										   City NVARCHAR(50),
										   BranchAddress NVARCHAR(MAX),
										   IFSCCode NVARCHAR(11),
										   IsActive BIT,
										   CreatedBy NVARCHAR(100),
										   CreatedOn DATETIME2,
										   ModifiedBy NVARCHAR(100),
										   ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_LoginCredentials(CustomerId BIGINT FOREIGN KEY REFERENCES Envault_Tbl_BasicDetails(CustomerId) PRIMARY KEY,
										  CustomerPassword NVARCHAR(25) COLLATE SQL_Latin1_General_CP1_CS_AS,
										  SecurityMessage NVARCHAR(100),
										  IsActive BIT DEFAULT 1,
										  CreatedBy NVARCHAR(100),
										  CreatedOn DATETIME2,
										  ModifiedBy NVARCHAR(100),
										  ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_KYC(CustomerId BIGINT FOREIGN KEY REFERENCES Envault_Tbl_BasicDetails(CustomerId) PRIMARY KEY,
							 AadharCard VARBINARY(MAX),
							 PANCard VARBINARY(MAX),
							 CustomerPhoto VARBINARY(MAX),
							 KYCStatus NVARCHAR(20),
							 IsActive BIT,
							 CreatedBy NVARCHAR(100),
							 CreatedOn DATETIME2,
							 ModifiedBy NVARCHAR(100),
							 ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_Accounts(AccountNumber BIGINT PRIMARY KEY,
								  CustomerId BIGINT FOREIGN KEY REFERENCES Envault_Tbl_BasicDetails(CustomerId),
							   	  BranchID INT FOREIGN KEY REFERENCES Envault_Tbl_BranchesAvailable(BranchID),
								  AccountBalance FLOAT NOT NULL,
								  AccountTypeId INT FOREIGN KEY REFERENCES Envault_Tbl_AccountType(AccountTypeId),
								  FreezeWithdrawl BIT,
								  MaxBalanceWarningAmount FLOAT,
								  MinBalanceWarningAmount FLOAT,
								  IsActive BIT,
								  CreatedBy NVARCHAR(100),
								  CreatedOn DATETIME2,
								  ModifiedBy NVARCHAR(100),
								  ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_Transactions(TransactionID BIGINT IDENTITY(1000000,1) PRIMARY KEY,
									  SenderAccountNumber BIGINT FOREIGN KEY REFERENCES Envault_Tbl_Accounts(AccountNumber),
									  ReceiverAccountNumber BIGINT FOREIGN KEY REFERENCES Envault_Tbl_Accounts(AccountNumber),
									  Amount FLOAT,
									  TransactionTime DATETIME2,
									  Remarks NVARCHAR(100),
									  IsActive BIT DEFAULT 1,
									  CreatedBy NVARCHAR(100),
									  CreatedOn DATETIME2,
									  ModifiedBy NVARCHAR(100),
									  ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_DepositType(DepositTypeId INT PRIMARY KEY IDENTITY(1,1),
									  DepositType NVARCHAR(50),
									  IsActive BIT,
									  CreatedBy NVARCHAR(100),
									  CreatedOn DATETIME2,
									  ModifiedBy NVARCHAR(100),
									  ModifiedOn DATETIME2)

CREATE TABLE Envault_Tbl_DepositRates(DepositRateId INT PRIMARY KEY IDENTITY(1,1),
									  DepositTypeId INT FOREIGN KEY REFERENCES Envault_Tbl_DepositType(DepositTypeId),
									  NormalRate FLOAT,
									  SeniorCitizenRate FLOAT,
									  MaxTenureMonths INT,
									  IsActive BIT,
									  CreatedBy NVARCHAR(100),
									  CreatedOn DATETIME2,
									  ModifiedBy NVARCHAR(100),
									  ModifiedOn DATETIME2)


CREATE TABLE Envault_tbl_TransactionAmountEligibil

--INSERTING Branches
INSERT INTO Envault_tbl_BranchesAvailable(BranchLocatedState, City, BranchAddress, IFSCCode)
VALUES
('Andhra Pradesh', 'Visakhapatnam', '123 Beach Road, Near RK Beach', 'ENV0001234'),
('Andhra Pradesh', 'Visakhapatnam', '456 Beach Road, Near RK Beach', 'ENV0005678'),
('Andhra Pradesh', 'Vijayawada', '789 MG Road, Near Benz Circle', 'ENV0003456'),
('Andhra Pradesh', 'Vijayawada', '321 Rajaguru Road, Near Bus Station', 'ENV0006543'),
('Andhra Pradesh', 'Tirupati', '234 Tirumala Road, Near Alipiri', 'ENV0008765'),

('Arunachal Pradesh', 'Itanagar', '456 Rajiv Gandhi Market, Main Road', 'ENV0005678'),
('Arunachal Pradesh', 'Itanagar', '789 NH-415, Near Indira Gandhi Park', 'ENV0007654'),
('Arunachal Pradesh', 'Tawang', '123 Tawang Market, Near Tawang Monastery', 'ENV0008762'),
('Arunachal Pradesh', 'Tawang', '456 Chumik, Near Main Square', 'ENV0003459'),
('Arunachal Pradesh', 'Ziro', '234 Ziro Valley Road, Near Apatani Village', 'ENV0004321'),

('Assam', 'Guwahati', '789 GS Road, Near Paltan Bazar', 'ENV0009876'),
('Assam', 'Guwahati', '123 Kamrup Road, Near Beltola', 'ENV0001235'),
('Assam', 'Dibrugarh', '456 College Road, Near Dibrugarh University', 'ENV0007653'),
('Assam', 'Dibrugarh', '789 Chabua Road, Near Tinsukia', 'ENV0002469'),
('Assam', 'Silchar', '321 Kachari Road, Near Silchar Medical College', 'ENV0004324'),

('Bihar', 'Patna', '321 Dakbungalow Chowk, Boring Road', 'ENV0006543'),
('Bihar', 'Patna', '654 Frazer Road, Near Gandhi Maidan', 'ENV0004322'),
('Bihar', 'Gaya', '123 Bodh Gaya Road, Near Mahabodhi Temple', 'ENV0001234'),
('Bihar', 'Gaya', '456 Station Road, Near Gaya Junction', 'ENV0007651'),
('Bihar', 'Bhagalpur', '789 Bhagalpur Road, Near Kahalgaon', 'ENV0009873'),

('Chattisgarh', 'Raipur', '135 Pandri Market, Pandri', 'ENV0004321'),
('Chattisgarh', 'Raipur', '789 Station Road, Near Telibandha', 'ENV0005673'),
('Chattisgarh', 'Bilaspur', '321 Bilaspur Road, Near Civil Lines', 'ENV0007656'),
('Chattisgarh', 'Bilaspur', '456 Anupam Nagar, Near High Court', 'ENV0004325'),
('Chattisgarh', 'Korba', '234 Korba Road, Near Deepak Bhavan', 'ENV0009084'),

('Goa', 'Panaji', '567 Miramar Beach Road', 'ENV0001122'),
('Goa', 'Panaji', '123 Dona Paula, Near Caculo Mall', 'ENV0009085'),
('Goa', 'Margao', '789 Kamat Complex, Near Holy Spirit Church', 'ENV0006548'),
('Goa', 'Margao', '456 New Market Road, Near Railway Station', 'ENV0007657'),
('Goa', 'Mapusa', '321 Mapusa Market, Near Municipal Garden', 'ENV0004327'),

('Gujarat', 'Ahmedabad', '135 SG Highway, Satellite Area', 'ENV0002456'),
('Gujarat', 'Ahmedabad', '789 MG Road, Near C.G. Road', 'ENV0008761'),
('Gujarat', 'Surat', '123 Ring Road, Near Surat Railway Station', 'ENV0009086'),
('Gujarat', 'Surat', '456 Gopi Talav, Near Sarthana Nature Park', 'ENV0007659'),
('Gujarat', 'Vadodara', '234 Alkapuri, Near Narmada Canal', 'ENV0004326'),

('Haryana', 'Gurgaon', '202 MG Road, Sector 29', 'ENV0003452'),
('Haryana', 'Gurgaon', '567 Sohna Road, Near Sector 50', 'ENV0007653'),
('Haryana', 'Faridabad', '123 Sector 15, Near Bata Chowk', 'ENV0003459'),
('Haryana', 'Faridabad', '456 Sector 12, Near Railway Station', 'ENV0004321'),
('Haryana', 'Ambala', '789 Ambala Cantt, Near Railway Junction', 'ENV0008764'),

('Himachal Pradesh', 'Shimla', '345 Mall Road, The Ridge', 'ENV0007654'),
('Himachal Pradesh', 'Shimla', '789 Jakhoo Hill, Near The Mall', 'ENV0008763'),
('Himachal Pradesh', 'Manali', '123 Hidimba Temple Road, Near Mall Road', 'ENV0009087'),
('Himachal Pradesh', 'Manali', '456 Old Manali, Near Manu Temple', 'ENV0004328'),
('Himachal Pradesh', 'Kullu', '234 Kullu Road, Near Beas River', 'ENV0003458'),

('Jharkhand', 'Ranchi', '456 Kutchery Road, Near Firayalal Chowk', 'ENV0008765'),
('Jharkhand', 'Ranchi', '123 Ranchi Road, Near Albert Ekka Chowk', 'ENV0004329'),
('Jharkhand', 'Jamshedpur', '789 Bistupur, Near Jubilee Park', 'ENV0009082'),
('Jharkhand', 'Jamshedpur', '234 Sakchi, Near Bistupur', 'ENV0003450'),
('Jharkhand', 'Dhanbad', '567 Bank More, Near Dhanbad Junction', 'ENV0007651'),

('Karnataka', 'Bangalore', '101 MG Road, Near MG Road Metro', 'ENV0009084'),
('Karnataka', 'Bangalore', '567 Brigade Road, Near Church Street', 'ENV0003455'),
('Karnataka', 'Mysore', '234 Sayyaji Rao Road, Near Mysore Palace', 'ENV0004323'),
('Karnataka', 'Mysore', '789 KRS Road, Near Chamundi Hill', 'ENV0008761'),
('Karnataka', 'Hubli', '123 Hubli Road, Near Bus Stand', 'ENV0007658'),

('Kerala', 'Kochi', '134 Marine Drive, Fort Kochi', 'ENV0002467'),
('Kerala', 'Kochi', '567 MG Road, Near Lulu Mall', 'ENV0004320'),
('Kerala', 'Thiruvananthapuram', '789 East Fort, Near Padmanabhaswamy Temple', 'ENV0008769'),
('Kerala', 'Thiruvananthapuram', '123 Statue Road, Near Secretariat', 'ENV0009088'),
('Kerala', 'Kozhikode', '456 Mavoor Road, Near Mananchira Square', 'ENV0003454'),

('Madhya Pradesh', 'Indore', '789 MG Road, Near Rajwada', 'ENV0003459'),
('Madhya Pradesh', 'Indore', '123 Residency Road, Near Rajendra Nagar', 'ENV0009086'),
('Madhya Pradesh', 'Bhopal', '567 VIP Road, Near Habibganj', 'ENV0008763'),
('Madhya Pradesh', 'Bhopal', '345 Maharana Pratap Nagar, Near M.P. Nagar', 'ENV0004322'),
('Madhya Pradesh', 'Gwalior', '234 Lashkar Road, Near Gwalior Fort', 'ENV0007655'),

('Maharashtra', 'Mumbai', '123 Marine Drive, Colaba', 'ENV0004321'),
('Maharashtra', 'Mumbai', '567 Linking Road, Near Bandra', 'ENV0009083'),
('Maharashtra', 'Pune', '789 Fergusson College Road, Near FC Road', 'ENV0003457'),
('Maharashtra', 'Pune', '234 Kothrud, Near MIT College', 'ENV0004325'),
('Maharashtra', 'Nagpur', '345 Sitabuldi, Near Zero Mile', 'ENV0008768'),

('Manipur', 'Imphal', '567 Babupara, Near Ima Keithel', 'ENV0005672'),
('Manipur', 'Imphal', '123 Khwairamband Bazar, Near Maharaj Ghat', 'ENV0004324'),
('Manipur', 'Churachandpur', '789 Churachandpur Market, Near PHE Office', 'ENV0009087'),
('Manipur', 'Churachandpur', '456 New Lamka, Near Bank of India', 'ENV0003453'),
('Manipur', 'Thoubal', '234 Thoubal Bazar, Near Thoubal Market', 'ENV0008766'),

('Meghalaya', 'Shillong', '789 Police Bazaar, Laitumkhrah', 'ENV0006754'),
('Meghalaya', 'Shillong', '123 Laitumkhrah, Near St. Edmund’s College', 'ENV0004321'),
('Meghalaya', 'Tura', '234 Tura Road, Near Tura Market', 'ENV0007651'),
('Meghalaya', 'Tura', '567 Wadanang, Near Tura Railway Station', 'ENV0009086'),
('Meghalaya', 'Jowai', '345 Jowai Road, Near Jowai Market', 'ENV0003459'),

('Mizoram', 'Aizawl', '321 Chaltlang, Near Zarkawt', 'ENV0002453'),
('Mizoram', 'Aizawl', '456 Khatla, Near Aizawl Church', 'ENV0007653'),
('Mizoram', 'Lunglei', '123 Lunglei Market, Near Rajbari', 'ENV0004322'),
('Mizoram', 'Lunglei', '789 Chanmari, Near Kalpana Theatre', 'ENV0009084'),
('Mizoram', 'Champhai', '234 Champhai Road, Near St. Mary’s Church', 'ENV0003450'),

('Nagaland', 'Kohima', '234 Main Road, Near Super Market', 'ENV0003450'),
('Nagaland', 'Kohima', '456 Nagaland University Road, Near High School', 'ENV0004324'),
('Nagaland', 'Dimapur', '123 Dimapur Town, Near Railway Station', 'ENV0008765'),
('Nagaland', 'Dimapur', '567 Main Road, Near Supermart', 'ENV0007652'),
('Nagaland', 'Mokokchung', '789 Mokokchung Road, Near Mokokchung Town Hall', 'ENV0009087'),

('Odisha', 'Bhubaneswar', '678 Kharavel Nagar, Near Station Square', 'ENV0004323'),
('Odisha', 'Bhubaneswar', '123 Janpath, Near Master Canteen', 'ENV0003456'),
('Odisha', 'Cuttack', '234 Cuttack Road, Near Bidanasi', 'ENV0007654'),
('Odisha', 'Cuttack', '567 Mangalabag, Near Cuttack Railway Station', 'ENV0004327'),
('Odisha', 'Rourkela', '789 Rourkela Road, Near Uditnagar', 'ENV0009089'),

('Punjab', 'Amritsar', '987 Golden Temple Road, Near Clock Tower', 'ENV0008767'),
('Punjab', 'Amritsar', '234 Lawrence Road, Near Gandhi Gate', 'ENV0007656'),
('Punjab', 'Chandigarh', '345 Sector 17, Near Chandigarh Haat', 'ENV0009083'),
('Punjab', 'Chandigarh', '567 Sector 22, Near Rock Garden', 'ENV0004328'),
('Punjab', 'Ludhiana', '123 Feroze Gandhi Market, Near Clock Tower', 'ENV0004329'),

('Rajasthan', 'Jaipur', '258 MI Road, Near Choti Chaupar', 'ENV0009085'),
('Rajasthan', 'Jaipur', '567 Malviya Nagar, Near Durgapura', 'ENV0003450'),
('Rajasthan', 'Udaipur', '123 Fatehsagar Road, Near Jagdish Temple', 'ENV0004325'),
('Rajasthan', 'Udaipur', '234 Chetak Circle, Near Lake Palace', 'ENV0007652'),
('Rajasthan', 'Jodhpur', '345 Jodhpur Road, Near Clock Tower', 'ENV0008761'),

('Sikkim', 'Gangtok', '123 MG Road, Near White Hall', 'ENV0006756'),
('Sikkim', 'Gangtok', '789 Lal Bazar, Near Tadong', 'ENV0003452'),
('Sikkim', 'Namchi', '234 Namchi Bazar, Near Samdruptse Hill', 'ENV0004320'),
('Sikkim', 'Namchi', '456 South Sikkim, Near Solophok', 'ENV0009085'),
('Sikkim', 'Pakyong', '567 Pakyong Road, Near Pakyong Airport', 'ENV0008764'),

('Tamil Nadu', 'Chennai', '789 Anna Nagar, 1st Avenue', 'ENV0002543'),
('Tamil Nadu', 'Chennai', '123 T. Nagar, Near Pondy Bazaar', 'ENV0007657'),
('Tamil Nadu', 'Coimbatore', '567 Race Course Road, Near Gandhipuram', 'ENV0009089'),
('Tamil Nadu', 'Coimbatore', '234 Oppanakkara Street, Near RS Puram', 'ENV0004325'),
('Tamil Nadu', 'Madurai', '345 Meenakshi Amman Temple Road, Near Azhagar Kovil', 'ENV0004321'),

('Telangana', 'Hyderabad', '456 Banjara Hills, Road No 12', 'ENV0003451'),
('Telangana', 'Hyderabad', '123 Kukatpally, Near Hitech City', 'ENV0007659'),
('Telangana', 'Warangal', '789 MG Road, Near Bhadrakali Temple', 'ENV0009082'),
('Telangana', 'Warangal', '234 Hanamkonda, Near Kakatiya University', 'ENV0004320'),
('Telangana', 'Khammam', '567 Khammam Road, Near Old Bus Stand', 'ENV0008761'),

('Tripura', 'Agartala', '234 Abhoynagar, Near Shibnagar', 'ENV0007652'),
('Tripura', 'Agartala', '567 Ujjayanta Palace Road, Near City Centre', 'ENV0004327'),
('Tripura', 'Dharmanagar', '789 Dharmanagar Bazar, Near Railway Station', 'ENV0009084'),
('Tripura', 'Dharmanagar', '123 North Tripura, Near Subhash Nagar', 'ENV0003456'),
('Tripura', 'Udaipur', '456 Udaipur Road, Near Tripura Sundari Temple', 'ENV0007650'),

('Uttar Pradesh', 'Lucknow', '654 Hazratganj, Near Mall Road', 'ENV0006547'),
('Uttar Pradesh', 'Lucknow', '234 Gomti Nagar, Near SGPGI', 'ENV0009083'),
('Uttar Pradesh', 'Varanasi', '567 Dashashwamedh Ghat, Near Kashi Vishwanath Temple', 'ENV0003459'),
('Uttar Pradesh', 'Varanasi', '123 Godowlia, Near Assi Ghat', 'ENV0007654'),
('Uttar Pradesh', 'Agra', '789 Fatehabad Road, Near Taj Mahal', 'ENV0004329'),

('Uttarakhand', 'Dehradun', '234 Rajpur Road, Near Clock Tower', 'ENV0008762'),
('Uttarakhand', 'Dehradun', '123 Haridwar Road, Near Patel Nagar', 'ENV0009084'),
('Uttarakhand', 'Nainital', '567 Mall Road, Near Naini Lake', 'ENV0007655'),
('Uttarakhand', 'Nainital', '234 Naini Road, Near Tiffin Top', 'ENV0003452'),
('Uttarakhand', 'Haridwar', '345 Haridwar Road, Near Har Ki Pauri', 'ENV0004321'),

('West Bengal', 'Kolkata', '321 Park Street, Bhowanipore', 'ENV0004322'),
('West Bengal', 'Kolkata', '456 BBD Bagh, Near Esplanade', 'ENV0009086'),
('West Bengal', 'Howrah', '789 Howrah Bridge Road, Near Howrah Station', 'ENV0007659'),
('West Bengal', 'Howrah', '123 Grand Trunk Road, Near Belur Math', 'ENV0003457'),
('West Bengal', 'Siliguri', '234 Hill Cart Road, Near Mallaguri', 'ENV0004324');

INSERT INTO Envault_tbl_AccountType(AccountType) VALUES ('Easy Access Savings Account'), ('Amaze Savings Account'), 
('Liberty Savings Account'), ('Prestige Savings Account'), ('Priority Savings Account'), ('Burgundy Savings Account'), 
('Current Account'), ('Easy Access Salary Account'), ('Liberty Salary Account'), ('Prestige Salary Account'), 
('Priority Salary Account'), ('Burgundy Salary Account'), ('Power Salute Salary Account'), ('Republic Salary Account')

INSERT INTO Envault_Tbl_DepositType(DepositType) VALUES ('Fixed Deposit'), ('Recurring Deposit')

INSERT INTO Envault_Tbl_DepositRates(DepositTypeId,NormalRate, SeniorCitizenRate, MaxTenureMonths) VALUES
(1, 6.5, 7.25 , 36),
(1, 6.75, 7.5, 72),
(1, 7.25, 8.0, 120),
(2, 6.25, 7.0 , 36),
(2, 6.5, 7.25, 72),
(2, 7.0, 7.75, 120)

UPDATE Envault_Tbl_BranchesAvailable SET IsActive = 1, CreatedBy='admin@gmail.com', CreatedOn=GETDATE(), ModifiedBy='admin@gmail.com', ModifiedOn=GETDATE()
UPDATE Envault_Tbl_AccountType SET IsActive = 1, CreatedBy='admin@gmail.com', CreatedOn=GETDATE(), ModifiedBy='admin@gmail.com', ModifiedOn=GETDATE()
UPDATE Envault_Tbl_DepositType SET IsActive = 1, CreatedBy='admin@gmail.com', CreatedOn=GETDATE(), ModifiedBy='admin@gmail.com', ModifiedOn=GETDATE()
UPDATE Envault_Tbl_DepositRates SET IsActive = 1, CreatedBy='admin@gmail.com', CreatedOn=GETDATE(), ModifiedBy='admin@gmail.com', ModifiedOn=GETDATE()

--Select statements
SELECT * FROM Envault_tbl_BasicDetails
SELECT * FROM Envault_tbl_CustomerAddress
SELECT * FROM Envault_tbl_CustomerFamilyAndNomineeDetails
SELECT * FROM Envault_tbl_BranchesAvailable
SELECT * FROM Envault_tbl_Accounts
SELECT * FROM Envault_tbl_AccountType
SELECT * FROM Envault_tbl_LoginCredentials
SELECT * FROM Envault_Tbl_KYC
SELECT * FROM Envault_Tbl_Transactions
