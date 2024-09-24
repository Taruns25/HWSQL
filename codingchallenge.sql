--Coding Challenge - Car Rental System – SQL
CREATE DATABASE CARRENTAL
USE CARRENTAL

CREATE TABLE Vehicle (
    vehicleID INT PRIMARY KEY,            
    make VARCHAR(50),                     
    model VARCHAR(50),                    
    [year] INT,                            
    dailyRate DECIMAL(10, 2),            
    status VARCHAR(12) CHECK (status IN ('available', 'notAvailable')), 
    passengerCapacity INT,               
    engineCapacity INT                    
)

INSERT INTO Vehicle (vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
VALUES
(1, 'Toyota', 'Camry', 2022, 50.00, 'available', 4, 1450),
(2, 'Honda', 'Civic', 2023, 45.00, 'available', 7, 1500),
(3, 'Ford', 'Focus', 2022, 48.00, 'notAvailable', 4, 1400),
(4, 'Nissan', 'Altima', 2023, 52.00, 'available', 7, 1200),
(5, 'Chevrolet', 'Malibu', 2022, 47.00, 'available', 4, 1800),
(6, 'Hyundai', 'Sonata', 2023, 49.00, 'notAvailable', 7, 1400),
(7, 'BMW', '3 Series', 2023, 60.00, 'available', 7, 2499)

INSERT INTO Vehicle (vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
VALUES
(8, 'Mercedes', 'C-Class', 2022, 58.00, 'available', 8, 2599),
(9, 'Audi', 'A4', 2022, 55.00, 'notAvailable', 4, 2500),
(10, 'Lexus', 'ES', 2023, 54.00, 'available', 4, 2500);


CREATE TABLE Customer (
    customerID INT PRIMARY KEY,         
    firstName VARCHAR(50), lastName VARCHAR(50),             
    email VARCHAR(100) UNIQUE,          
    phoneNumber VARCHAR(15)            
)

INSERT INTO Customer (customerID, firstName, lastName, email, phoneNumber)
VALUES
(1, 'John', 'Doe', 'johndoe@example.com', '555-555-5555'),
(2, 'Jane', 'Smith', 'janesmith@example.com', '555-123-4567'),
(3, 'Robert', 'Johnson', 'robert@example.com', '555-789-1234'),
(4, 'Sarah', 'Brown', 'sarah@example.com', '555-456-7890'),
(5, 'David', 'Lee', 'david@example.com', '555-987-6543'),
(6, 'Laura', 'Hall', 'laura@example.com', '555-234-5678'),
(7, 'Michael', 'Davis', 'michael@example.com', '555-876-5432'),
(8, 'Emma', 'Wilson', 'emma@example.com', '555-432-1098'),
(9, 'William', 'Taylor', 'william@example.com', '555-321-6547'),
(10, 'Olivia', 'Adams', 'olivia@example.com', '555-765-4321')

CREATE TABLE Lease (
    leaseID INT PRIMARY KEY,               
    vehicleID INT,                          
    customerID INT,                       
    startDate DATE,                        
    endDate DATE,                          
    type VARCHAR(20) CHECK (type IN ('DailyLease', 'MonthlyLease')), 
     FOREIGN KEY (vehicleID) REFERENCES Vehicle(vehicleID),FOREIGN KEY (customerID) REFERENCES Customer(customerID)
)
INSERT INTO Lease (leaseID, vehicleID, customerID, startDate, endDate, type)
VALUES
(1, 1, 1, '2023-01-01', '2023-01-05', 'DailyLease'),
(2, 2, 2, '2023-02-15', '2023-02-28', 'MonthlyLease'),
(3, 3, 3, '2023-03-10', '2023-03-15', 'DailyLease'),
(4, 4, 4, '2023-04-20', '2023-04-30', 'MonthlyLease'),
(5, 5, 5, '2023-05-05', '2023-05-10', 'DailyLease'),
(6, 4, 3, '2023-06-15', '2023-06-30', 'MonthlyLease'),
(7, 7, 7, '2023-07-01', '2023-07-10', 'DailyLease'),
(8, 8, 8, '2023-08-12', '2023-08-15', 'MonthlyLease'),
(9, 3, 3, '2023-09-07', '2023-09-10', 'DailyLease'),
(10, 10, 10, '2023-10-10', '2023-10-31', 'MonthlyLease')

CREATE TABLE Payment (
    paymentID INT PRIMARY KEY,         
    leaseID INT,                       
    paymentDate DATE,
    amount float,             
   FOREIGN KEY (leaseID) REFERENCES Lease(leaseID)
)
INSERT INTO Payment (paymentID, leaseID, paymentDate, amount)
VALUES
(1, 1, '2023-01-03', 200.00),
(2, 2, '2023-02-20', 1000.00),
(3, 3, '2023-03-12', 75.00),
(4, 4, '2023-04-25', 900.00),
(5, 5, '2023-05-07', 60.00),
(6, 6, '2023-06-18', 1200.00),
(7, 7, '2023-07-03', 40.00),
(8, 8, '2023-08-14', 1100.00),
(9, 9, '2023-09-09', 80.00),
(10, 10, '2023-10-25', 1500.00)

--1. Update the daily rate for a Mercedes car to 68.
UPDATE Vehicle set dailyRate=68 WHERE make='MERCEDES'

--2 Delete a specific customer and all associated leases and payments
DECLARE @delete INT=1

DELETE FROM Payment 
WHERE leaseID IN (SELECT leaseID FROM Lease WHERE customerID = @delete)

DELETE FROM Lease 
WHERE customerID = @delete;

DELETE FROM Customer 
WHERE customerID =@delete

--3. Rename the "paymentDate" column in the Payment table to "transactionDate".
EXEC SP_RENAME 'Payment.paymentDate', 'transactionDate', 'COLUMN'

SELECT * FROM Payment

--4. Find a specific customer by email
DECLARE @EMAIL VARCHAR ='johndoe@example.com'
SELECT * FROM Customer WHERE EMAIL=@EMAIL

--5. Get active leases for a specific customer
SELECT DISTINCT * 
FROM Lease AS L
JOIN VEHICLE AS V ON L.vehicleID=V.vehicleID
WHERE customerID = 3 AND status='NOTAVAILABLE' 

--6. Find all payments made by a customer with a specific phone number.
DECLARE @PHONE VARCHAR(15)=555-456-7890
SELECT Payment.* 
FROM Payment 
JOIN Lease ON Payment.leaseID = Lease.leaseID 
JOIN Customer ON Lease.customerID = Customer.customerID 
WHERE Customer.phoneNumber = '555-456-7890'

--7. Calculate the average daily rate of all available cars.
SELECT AVG(dailyRate) AS AVG_RATE FROM Vehicle WHERE status='available' 

--8.Find the car with the highest daily rate.
SELECT TOP 1 *
FROM Vehicle 
ORDER BY dailyRate DESC 

--9 Retrieve all cars leased by a specific customer.
DECLARE @CUSTID INT =6
SELECT Vehicle.* 
FROM Vehicle 
JOIN Lease ON Vehicle.vehicleID = Lease.vehicleID 
WHERE Lease.customerID =@CUSTID

--10. Find the details of the most recent lease.
SELECT TOP 1 *
FROM Lease
ORDER BY startDate DESC

--11. List all payments made in the year 2023.
SELECT * 
FROM Payment AS P
WHERE (P.paymentDate) = '%2023%'

--12.Retrieve customers who have not made any payments.
SELECT * 
FROM Customer 
WHERE customerID NOT IN (SELECT L.customerID FROM Lease AS L JOIN Payment AS P ON L.leaseID = P.leaseID)

--13.Retrieve Car Details and Their Total Payments.
SELECT V.vehicleID,V.make,V.model,V.year, SUM(P.amount) AS totalPayments 
FROM Vehicle  AS V
JOIN Lease AS L ON V.vehicleID = L.vehicleID 
JOIN Payment AS P ON L.leaseID = P.leaseID 
GROUP BY V.vehicleID,V.make,V.model,V.year

--14. Calculate Total Payments for Each Customer.
SELECT C.FIRSTNAME,C.LASTNAME , SUM(P.AMOUNT)AS TOTAL_PAYMENT 
FROM Customer AS C
JOIN LEASE AS L ON C.customerID=L.customerID
JOIN Payment AS P ON P.leaseID=L.leaseID
GROUP BY C.firstName,C.lastName

--15 List Car Details for Each Lease.
SELECT Lease.*, Vehicle.* 
FROM Lease 
JOIN Vehicle ON Lease.vehicleID = Vehicle.vehicleID;

--16. Retrieve Details of Active Leases with Customer and Car Information
SELECT DISTINCT Lease.*, Customer.*, Vehicle.* 
FROM Lease 
JOIN Customer ON Lease.customerID = Customer.customerID 
JOIN Vehicle ON Lease.vehicleID = Vehicle.vehicleID 
WHERE  Vehicle.status = 'AVAILABLE'

--17. Find the Customer Who Has Spent the Most on Leases.
SELECT TOP 1 C.*, SUM(P.AMOUNT) AS TOTAL_PAID 
FROM CUSTOMER AS C
JOIN LEASE AS L ON C.CUSTOMERID = L.CUSTOMERID
JOIN PAYMENT AS P ON P.LEASEID = L.LEASEID
GROUP BY C.customerID, C.firstName, C.lastName, C.email, C.phoneNumber 
ORDER BY TOTAL_PAID DESC
 
 --18.List All Cars with Their Current Lease Information.
 SELECT Vehicle.*, Lease.type 
FROM Vehicle 
JOIN Lease ON Vehicle.vehicleID = Lease.vehicleID
