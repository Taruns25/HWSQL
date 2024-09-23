--TASK 1
CREATE DATABASE HMBank;
USE HMBank;
CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    email NVARCHAR(100),
    phone_number NVARCHAR(15),
    address NVARCHAR(255)
);
CREATE TABLE Accounts (
    account_id INT PRIMARY KEY ,
    customer_id INT NOT NULL FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    account_type NVARCHAR(20) CHECK (account_type IN ('savings', 'current', 'zero_balance')),
    balance DECIMAL(18, 2) DEFAULT 0,
);
CREATE TABLE Transactions (
    transaction_id INT PRIMARY KEY ,
    account_id INT NOT NULL,
    transaction_type NVARCHAR(20) CHECK (transaction_type IN ('deposit', 'withdrawal', 'transfer')),
    amount DECIMAL(18, 2) NOT NULL,
    transaction_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (account_id) REFERENCES Accounts(account_id)
);

--TASK 2

INSERT INTO Customers (customer_id, first_name, last_name, DOB, email, phone_number, address) VALUES
(1, 'John', 'Doe', '1980-01-15', 'john.doe@example.com', '1234567890', '123 Elm St, Cityville'),
(2, 'Jane', 'Smith', '1992-03-22', 'jane.smith@example.com', '0987654321', '456 Oak St, Townsville'),
(3, 'Michael', 'Johnson', '1985-07-10', 'michael.j@example.com', '1230984567', '789 Pine St, Villagetown'),
(4, 'Emily', 'Davis', '1990-11-05', 'emily.davis@example.com', '2345678901', '321 Cedar St, Hamlet'),
(5, 'David', 'Brown', '1978-02-28', 'david.brown@example.com', '3456789012', '654 Maple St, Citytown'),
(6, 'Sarah', 'Wilson', '1987-05-17', 'sarah.wilson@example.com', '4567890123', '987 Birch St, Smallville'),
(7, 'Robert', 'Martinez', '1993-08-14', 'robert.martinez@example.com', '5678901234', '123 Spruce St, Bigcity'),
(8, 'Jessica', 'Garcia', '1982-10-30', 'jessica.garcia@example.com', '6789012345', '456 Willow St, Oldtown'),
(9, 'James', 'Rodriguez', '1975-12-25', 'james.rodriguez@example.com', '7890123456', '789 Fir St, Newtown'),
(10, 'Patricia', 'Anderson', '1991-04-09', 'patricia.anderson@example.com', '8901234567', '321 Ash St, Middletown');

INSERT INTO Accounts (account_id, customer_id, account_type, balance) VALUES
(1, 1, 'savings', 5000.00),
(2, 2, 'current', 2500.00),
(3, 3, 'savings', 7000.00),
(4, 4, 'zero_balance', 0.00),
(5, 5, 'current', 1500.00),
(6, 6, 'savings', 4000.00),
(7, 7, 'savings', 600.00),
(8, 8, 'current', 8000.00),
(9, 9, 'zero_balance', 0.00),
(10, 10, 'savings', 3000.00);

INSERT INTO Transactions (transaction_id, account_id, transaction_type, amount, transaction_date) VALUES
(1, 1, 'deposit', 500.00, '2024-09-01'),
(2, 2, 'withdrawal', 200.00, '2024-09-02'),
(3, 3, 'deposit', 1000.00, '2024-09-03'),
(4, 4, 'deposit', 0.00, '2024-09-04'),
(5, 5, 'withdrawal', 300.00, '2024-09-05'),
(6, 6, 'deposit', 800.00, '2024-09-06'),
(7, 7, 'withdrawal', 100.00, '2024-09-07'),
(8, 8, 'deposit', 1500.00, '2024-09-08'),
(9, 9, 'deposit', 0.00, '2024-09-09'),
(10, 10, 'withdrawal', 500.00, '2024-09-10');

-- 1.query to retrieve the name, account type and email of all customers
SELECT FIRST_NAME , LAST_NAME, ACCOUNT_TYPE,EMAIL FROM CUSTOMERS AS C, ACCOUNTS AS A WHERE C.customer_id=A.customer_id

--2.query to list all transaction corresponding customer
SELECT CONCAT(FIRST_NAME,'',LAST_NAME) AS NAME,A.customer_id, C.CUSTOMER_ID,transaction_type, amount, transaction_date FROM Customers AS C , Accounts AS A,Transactions AS T WHERE C.customer_id=A.customer_id AND A.account_id=T.account_id

--3. query to increase the balance of a specific account by a certain amount
SELECT * FROM ACCOUNTS WHERE customer_id=1
UPDATE Accounts SET BALANCE= BALANCE +500 WHERE CUSTOMER_ID=1
SELECT * FROM ACCOUNTS WHERE customer_id=1

--4.query to Combine first and last names of customers as a full_name
SELECT CONCAT(FIRST_NAME ,' ' ,LAST_NAME) AS NAME FROM Customers 

--5.query to remove accounts with a balance of zero where the account type is savings
DELETE FROM Accounts WHERE balance=0.00 AND account_type='SAVINGS'

--6.L query to Find customers living in a specific city
SELECT * FROM Customers WHERE address='123 Elm St, Cityville'

--7. query to Get the account balance for a specific account.
SELECT BALANCE FROM Accounts WHERE account_type='SAVINGS' AND CUSTOMER_ID =1

--8. query to List all current accounts with a balance greater than $1,000
SELECT * FROM ACCOUNTS WHERE account_type='CURRENT' AND balance>1000.00

--9. query to Retrieve all transactions for a specific account.
SELECT * FROM Transactions WHERE account_id=transaction_id AND AMOUNT>1000

--10.query to Calculate the interest accrued on savings accounts based on a given interest rate.
DECLARE @INTEREST FLOAT = 0.03 SELECT DISTINCT CONCAT(C.FIRST_NAME ,' ' ,C.LAST_NAME) AS NAME , A.BALANCE , A.BALANCE*@INTEREST AS AMOUNT FROM CUSTOMERS AS C , Accounts AS A WHERE A.account_type = 'SAVINGS' AND C.customer_id =A.customer_id

--11 query to Identify accounts where the balance is less than a specified overdraft limit.
SELECT * FROM Accounts WHERE balance < 500.00

--12.query to Find customers not living in a specific city
SELECT * FROM Customers WHERE address ! = '%Cityville'


-- TASK 3

--1. query to Find the average account balance
SELECT AVG(balance) AS average_balance FROM Accounts
 
 --2.query to Retrieve the top 10 highest account balances.
SELECT TOP 10 C.first_name, C.last_name, A.balance FROM Customers AS C, Accounts AS A WHERE C.customer_id = A.customer_id ORDER BY A.balance DESC

--3.query to Calculate Total Deposits for All Customers in specific date
SELECT SUM(amount) AS total_deposits
FROM Transactions
WHERE transaction_type = 'deposit' 
AND transaction_date = '2024-09-01';

--4. query to Find the Oldest and Newest Customers
SELECT top 1 * FROM Customers ORDER BY DOB ASC --oldest
SELECT top 1 * FROM Customers ORDER BY DOB DESC --newest

--5.query to Retrieve transaction details along with the account type.
SELECT T.*, A.account_type FROM Transactions T JOIN Accounts A ON T.account_id = A.account_id;

--6.query to Get a list of customers along with their account details.
SELECT C.*, A.account_id, A.account_type, A.balance FROM Customers C JOIN Accounts A ON C.customer_id = A.customer_id

--7 query to Retrieve transaction details along with customer information for a specific account.
DECLARE @accountid INT = 9
SELECT T.*, C.*
FROM Transactions T
JOIN Accounts A ON T.account_id = A.account_id
JOIN Customers C ON A.customer_id = C.customer_id
WHERE T.account_id = @accountid

--8  query to Identify customers who have more than one account
SELECT customer_id, COUNT(*) AS account_count
FROM Accounts
GROUP BY customer_id
HAVING COUNT(*) > 1

--9 query to Calculate the difference in transaction amounts between deposits and withdrawals

--10 query to Calculate the average daily balance for each account over a specified period.
SELECT A.account_id, AVG(balance) AS avg_daily_balance
FROM Accounts A
JOIN Transactions T ON A.account_id = T.account_id
WHERE transaction_date BETWEEN '2024-09-01' AND '2024-09-10'
GROUP BY A.account_id

--11 Calculate the total balance for each account type.
SELECT account_type, SUM(balance) AS total_balance
FROM Accounts
GROUP BY account_type

--12 Identify accounts with the highest number of transactions order by descending order
SELECT account_id, COUNT(*) AS transaction_count
FROM Transactions
GROUP BY account_id
ORDER BY transaction_count DESC

--13 List customers with high aggregate account balances, along with their account types.
SELECT C.customer_id, C.first_name, C.last_name, A.account_type, SUM(A.balance) AS total_balance
FROM Customers C
JOIN Accounts A ON C.customer_id = A.customer_id
GROUP BY C.customer_id,C.first_name,C.last_name, A.account_type
HAVING SUM(A.balance) > 5000

--14 Identify and list duplicate transactions based on transaction amount, date, and account
SELECT account_id, amount, transaction_date, COUNT(*) AS duplicate_count
FROM Transactions
GROUP BY account_id, amount, transaction_date
HAVING COUNT(*) > 1

-- TASK 4
--1  Retrieve the customer(s) with the highest account balance.
SELECT * 
FROM Customers C
WHERE C.customer_id = (SELECT top 1 customer_id FROM Accounts ORDER BY balance DESC)

--2  Calculate the average account balance for customers who have more than one account
SELECT *
FROM Transactions
WHERE amount > (SELECT AVG(amount) FROM Transactions);

--3 Retrieve accounts with transactions whose amounts exceed the average transaction amount
select * from Transactions where amount>(select avg(amount) from Transactions)

--4 Identify customers who have no recorded transactions
SELECT C.*
FROM Customers C
WHERE C.customer_id NOT IN (SELECT customer_id
                            FROM Accounts A
                            JOIN Transactions T ON A.account_id = T.account_id);

--5 Calculate the total balance of accounts with no recorded transactions
SELECT SUM(balance) AS total_balance
FROM Accounts
WHERE account_id NOT IN (SELECT account_id FROM Transactions)

--6 Retrieve transactions for accounts with the lowest balance.
select * from Transactions as t where t.account_id=(select top 1account_id from Accounts order by balance asc)

--7 identify customers who have accounts of multiple types.
SELECT customer_id
FROM Accounts
GROUP BY customer_id
HAVING COUNT(distinct account_type) > 1

--8  Calculate the percentage of each account type out of the total number of accounts.
select account_type, COUNT(*) * 100 / (select COUNT(*) from Accounts) as Percentage 
from Accounts
group by account_type

--9 Retrieve all transactions for a customer with a given customer_id.
declare @accid int = 2
SELECT T.*
FROM Transactions T
JOIN Accounts A ON T.account_id = A.account_id
WHERE A.customer_id =@accid

--10 Calculate the total balance for each account type, including a subquery within the SELECT clause
select account_type, (select sum(balance) from Accounts where account_type = a.account_type) as Total_Balance
from Accounts a
group by account_type