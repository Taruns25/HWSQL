create database Transport_Management
use Transport_Management

-- Create table for Trips
create table Routes( 
	RouteID int not null primary key,
	StartDestination varchar(255),
	EndDestination varchar(255),
	Distance decimal(10,2),
)

create table Vehicles( 
	VehicleID int  not null primary key,
	Model varchar(255),
	Capacity decimal(10,2),
	Type varchar(50),
	VehicleStatus varchar(50)
)


CREATE TABLE Trips( 
	TripID INT NOT NULL PRIMARY KEY,
	VehicleID INT FOREIGN KEY(VehicleID) REFERENCES Vehicles(VehicleID),
	RouteID INT FOREIGN KEY(RouteID) REFERENCES Routes(RouteID),
	DepartureDate DATETIME,
	ArrivalDate DATETIME,
	VehicleStatus VARCHAR(50),
	TripType VARCHAR(50) DEFAULT 'Freight',
	MaxPassenger INT
)

-- Create table for Passengers
CREATE TABLE Passengers( 
	PassengerID INT NOT NULL PRIMARY KEY, 
	FirstName VARCHAR(255),
	Gender VARCHAR(255),
	Age INT,
	Email VARCHAR(255) UNIQUE,
	PhoneNumber VARCHAR(255)
)

-- Create table for Bookings
CREATE TABLE Bookings(
	BookingID INT NOT NULL PRIMARY KEY,
	TripID INT FOREIGN KEY(TripID) REFERENCES Trips(TripID),
	PassengerID INT FOREIGN KEY(PassengerID) REFERENCES Passengers(PassengerID),
	BookingDate DATETIME,
	VehicleStatus VARCHAR(50)
)

-- Inserting sample values

-- Vehicles
INSERT INTO Vehicles VALUES
(1, 'Ford Transit', 12.50, 'Van', 'Available'),
(2, 'Mercedes Actros', 30.00, 'Truck', 'In Service'),
(3, 'Volvo Bus 9700', 55.00, 'Bus', 'On Trip')

-- Routes
INSERT INTO Routes VALUES
(1, 'Mumbai', 'Pune', 150.00),
(2, 'Chennai', 'Bangalore', 350.00),
(3, 'Delhi', 'Agra', 233.00)

-- Trips
INSERT INTO Trips VALUES
(1, 1, 1, '2024-09-21 07:30:00', '2024-09-21 11:30:00', 'In Progress', 'Passenger', 10),
(2, 2, 2, '2024-09-22 06:00:00', '2024-09-22 18:00:00', 'Completed', 'Freight', NULL),
(3, 3, 3, '2024-09-23 09:00:00', '2024-09-23 12:00:00', 'Scheduled', 'Passenger', 40)

-- Passengers
INSERT INTO Passengers VALUES
(1, 'Amit', 'Male', 30, 'amitkumar@gmail.com', '9998887777'),
(2, 'Neha', 'Female', 25, 'neha_mehra@gmail.com', '8889997776'),
(3, 'Vikram', 'Male', 28, 'vikram.singh@gmail.com', '7776665554')

-- Bookings
INSERT INTO Bookings VALUES
(1, 1, 1, '2024-09-19 10:00:00', 'Confirmed'),
(2, 1, 2, '2024-09-19 11:00:00', 'Confirmed'),
(3, 3, 3, '2024-09-20 12:00:00', 'Cancelled')

-- Selecting data to verify
SELECT * FROM Vehicles
SELECT * FROM Routes
SELECT * FROM Trips
SELECT * FROM Passengers
SELECT * FROM Bookings
