-- ============================================
-- Hotel Booking System - Full Schema + Data
-- Project: DBS26CE4thAF011
-- Course: CMPE-232L | UET Lahore
-- ============================================

DROP DATABASE IF EXISTS HotelBookingSystem;
CREATE DATABASE HotelBookingSystem;
USE HotelBookingSystem;

-- ============================================
-- TABLES
-- ============================================

CREATE TABLE RoomType (
    TypeID      INT AUTO_INCREMENT PRIMARY KEY,
    TypeName    VARCHAR(50)   NOT NULL UNIQUE,
    Description VARCHAR(255)  NOT NULL,
    Capacity    INT           NOT NULL CHECK (Capacity > 0),
    BasePrice   DECIMAL(10,2) NOT NULL CHECK (BasePrice > 0)
);

CREATE TABLE Room (
    RoomID        INT AUTO_INCREMENT PRIMARY KEY,
    RoomNumber    VARCHAR(10)   NOT NULL UNIQUE,
    TypeID        INT           NOT NULL,
    Floor         INT           NOT NULL CHECK (Floor >= 0),
    PricePerNight DECIMAL(10,2) NOT NULL CHECK (PricePerNight > 0),
    Status        VARCHAR(20)   NOT NULL DEFAULT 'Available'
                  CHECK (Status IN ('Available', 'Booked', 'Maintenance', 'CheckedIn')),
    FOREIGN KEY (TypeID) REFERENCES RoomType(TypeID)
);

CREATE TABLE Amenity (
    AmenityID   INT AUTO_INCREMENT PRIMARY KEY,
    Name        VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(255)
);

CREATE TABLE RoomAmenity (
    RoomID    INT NOT NULL,
    AmenityID INT NOT NULL,
    PRIMARY KEY (RoomID, AmenityID),
    FOREIGN KEY (RoomID)    REFERENCES Room(RoomID)    ON DELETE CASCADE,
    FOREIGN KEY (AmenityID) REFERENCES Amenity(AmenityID) ON DELETE CASCADE
);

CREATE TABLE Guest (
    GuestID      INT AUTO_INCREMENT PRIMARY KEY,
    FullName     VARCHAR(100) NOT NULL,
    CNIC         VARCHAR(13)  NOT NULL UNIQUE CHECK (CHAR_LENGTH(CNIC) = 13),
    Email        VARCHAR(100) NOT NULL UNIQUE,
    Phone        VARCHAR(15)  NOT NULL,
    Address      VARCHAR(255),
    RegisteredAt DATETIME DEFAULT NOW()
);

CREATE TABLE Employee (
    EmployeeID  INT AUTO_INCREMENT PRIMARY KEY,
    FullName    VARCHAR(100)  NOT NULL,
    CNIC        VARCHAR(13)   NOT NULL UNIQUE,
    Phone       VARCHAR(15)   NOT NULL,
    Role        VARCHAR(50)   NOT NULL
                CHECK (Role IN ('Admin', 'Receptionist', 'Manager', 'Housekeeper')),
    Salary      DECIMAL(10,2) NOT NULL CHECK (Salary > 0),
    HireDate    DATE          NOT NULL DEFAULT (CURRENT_DATE)
);

CREATE TABLE Login (
    LoginID      INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeID   INT          NOT NULL UNIQUE,
    Username     VARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    IsActive     TINYINT(1)   NOT NULL DEFAULT 1,
    LastLogin    DATETIME,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

CREATE TABLE Booking (
    BookingID   INT AUTO_INCREMENT PRIMARY KEY,
    GuestID     INT           NOT NULL,
    RoomID      INT           NOT NULL,
    BookingDate DATETIME      NOT NULL DEFAULT NOW(),
    CheckIn     DATE          NOT NULL,
    CheckOut    DATE          NOT NULL,
    Status      VARCHAR(20)   NOT NULL DEFAULT 'Confirmed'
                CHECK (Status IN ('Confirmed', 'CheckedIn', 'CheckedOut', 'Cancelled')),
    TotalAmount DECIMAL(10,2) NOT NULL CHECK (TotalAmount >= 0),
    CONSTRAINT CHK_Dates CHECK (CheckOut > CheckIn),
    FOREIGN KEY (GuestID) REFERENCES Guest(GuestID),
    FOREIGN KEY (RoomID)  REFERENCES Room(RoomID)
);

CREATE TABLE Payment (
    PaymentID   INT AUTO_INCREMENT PRIMARY KEY,
    BookingID   INT           NOT NULL,
    Amount      DECIMAL(10,2) NOT NULL CHECK (Amount > 0),
    PaymentDate DATETIME      NOT NULL DEFAULT NOW(),
    Method      VARCHAR(20)   NOT NULL
                CHECK (Method IN ('Cash', 'Card', 'Online', 'Cheque')),
    Status      VARCHAR(20)   NOT NULL DEFAULT 'Paid'
                CHECK (Status IN ('Paid', 'Pending', 'Refunded')),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);

CREATE TABLE Invoice (
    InvoiceID     INT AUTO_INCREMENT PRIMARY KEY,
    BookingID     INT           NOT NULL UNIQUE,
    TotalAmount   DECIMAL(10,2) NOT NULL,
    GeneratedDate DATETIME      NOT NULL DEFAULT NOW(),
    PaidStatus    TINYINT(1)    NOT NULL DEFAULT 0,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);

CREATE TABLE ServiceType (
    ServiceTypeID INT AUTO_INCREMENT PRIMARY KEY,
    Name          VARCHAR(100)  NOT NULL UNIQUE,
    Price         DECIMAL(10,2) NOT NULL CHECK (Price >= 0)
);

CREATE TABLE RoomService (
    ServiceID     INT AUTO_INCREMENT PRIMARY KEY,
    BookingID     INT         NOT NULL,
    ServiceTypeID INT         NOT NULL,
    Quantity      INT         NOT NULL DEFAULT 1 CHECK (Quantity > 0),
    RequestTime   DATETIME    NOT NULL DEFAULT NOW(),
    Status        VARCHAR(20) NOT NULL DEFAULT 'Pending'
                  CHECK (Status IN ('Pending', 'InProgress', 'Delivered', 'Cancelled')),
    FOREIGN KEY (BookingID)     REFERENCES Booking(BookingID),
    FOREIGN KEY (ServiceTypeID) REFERENCES ServiceType(ServiceTypeID)
);

CREATE TABLE Housekeeping (
    TaskID        INT AUTO_INCREMENT PRIMARY KEY,
    RoomID        INT         NOT NULL,
    EmployeeID    INT         NOT NULL,
    ScheduledDate DATE        NOT NULL,
    Status        VARCHAR(20) NOT NULL DEFAULT 'Pending'
                  CHECK (Status IN ('Pending', 'InProgress', 'Done')),
    Notes         VARCHAR(255),
    FOREIGN KEY (RoomID)     REFERENCES Room(RoomID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE Cancellation (
    CancellationID INT AUTO_INCREMENT PRIMARY KEY,
    BookingID      INT           NOT NULL UNIQUE,
    Reason         VARCHAR(255)  NOT NULL,
    RefundAmount   DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (RefundAmount >= 0),
    CancelDate     DATETIME      NOT NULL DEFAULT NOW(),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);

CREATE TABLE ErrorLog (
    LogID      INT AUTO_INCREMENT PRIMARY KEY,
    Message    VARCHAR(500) NOT NULL,
    StackTrace TEXT,
    LogTime    DATETIME     NOT NULL DEFAULT NOW(),
    UserID     INT,
    FOREIGN KEY (UserID) REFERENCES Login(LoginID)
);

-- ============================================
-- VIEWS
-- ============================================

CREATE VIEW vw_AvailableRooms AS
SELECT r.RoomID, r.RoomNumber, rt.TypeName, r.Floor, r.PricePerNight, r.Status
FROM Room r
JOIN RoomType rt ON r.TypeID = rt.TypeID
WHERE r.Status = 'Available';

CREATE VIEW vw_ActiveBookings AS
SELECT b.BookingID, g.FullName AS GuestName, g.Phone AS GuestPhone,
       r.RoomNumber, rt.TypeName AS RoomType,
       b.CheckIn, b.CheckOut, b.TotalAmount, b.Status AS BookingStatus
FROM Booking b
JOIN Guest g    ON b.GuestID = g.GuestID
JOIN Room r     ON b.RoomID  = r.RoomID
JOIN RoomType rt ON r.TypeID = rt.TypeID
WHERE b.Status IN ('Confirmed', 'CheckedIn');

CREATE VIEW vw_MonthlyRevenue AS
SELECT YEAR(p.PaymentDate) AS PaymentYear, MONTH(p.PaymentDate) AS PaymentMonth,
       COUNT(p.PaymentID) AS TotalPayments, SUM(p.Amount) AS TotalRevenue
FROM Payment p
WHERE p.Status = 'Paid'
GROUP BY YEAR(p.PaymentDate), MONTH(p.PaymentDate);

CREATE VIEW vw_RoomServiceDetails AS
SELECT rs.ServiceID, b.BookingID, g.FullName AS GuestName, r.RoomNumber,
       st.Name AS ServiceName, st.Price AS UnitPrice, rs.Quantity,
       (st.Price * rs.Quantity) AS TotalPrice, rs.RequestTime, rs.Status
FROM RoomService rs
JOIN Booking b     ON rs.BookingID     = b.BookingID
JOIN Guest g       ON b.GuestID        = g.GuestID
JOIN Room r        ON b.RoomID         = r.RoomID
JOIN ServiceType st ON rs.ServiceTypeID = st.ServiceTypeID;

CREATE VIEW vw_HousekeepingTasks AS
SELECT e.EmployeeID, e.FullName AS EmployeeName, e.Role,
       r.RoomNumber, h.ScheduledDate, h.Status AS TaskStatus, h.Notes
FROM Housekeeping h
JOIN Employee e ON h.EmployeeID = e.EmployeeID
JOIN Room r     ON h.RoomID     = r.RoomID;

CREATE VIEW vw_GuestBookingHistory AS
SELECT g.GuestID, g.FullName, g.Email, g.Phone,
       COUNT(b.BookingID) AS TotalBookings,
       SUM(b.TotalAmount) AS TotalSpent,
       MAX(b.CheckIn)     AS LastStay
FROM Guest g
LEFT JOIN Booking b ON g.GuestID = b.GuestID
GROUP BY g.GuestID, g.FullName, g.Email, g.Phone;

CREATE VIEW vw_CancellationReport AS
SELECT c.CancellationID, b.BookingID, g.FullName AS GuestName,
       r.RoomNumber, c.Reason, c.RefundAmount, c.CancelDate
FROM Cancellation c
JOIN Booking b ON c.BookingID = b.BookingID
JOIN Guest g   ON b.GuestID   = g.GuestID
JOIN Room r    ON b.RoomID    = r.RoomID;

-- ============================================
-- STORED PROCEDURES
-- ============================================

DELIMITER $$

CREATE PROCEDURE sp_MakeBooking(
    IN p_GuestID INT, IN p_RoomID INT,
    IN p_CheckIn DATE, IN p_CheckOut DATE, IN p_TotalAmount DECIMAL(10,2)
)
BEGIN
    DECLARE room_status VARCHAR(20);
    SELECT Status INTO room_status FROM Room WHERE RoomID = p_RoomID;
    IF room_status != 'Available' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Room is not available for booking.';
    END IF;
    IF EXISTS (
        SELECT 1 FROM Booking
        WHERE RoomID = p_RoomID AND Status IN ('Confirmed', 'CheckedIn')
          AND p_CheckIn < CheckOut AND p_CheckOut > CheckIn
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Room is already booked for the selected dates.';
    END IF;
    START TRANSACTION;
        INSERT INTO Booking (GuestID, RoomID, CheckIn, CheckOut, TotalAmount, Status)
        VALUES (p_GuestID, p_RoomID, p_CheckIn, p_CheckOut, p_TotalAmount, 'Confirmed');
        UPDATE Room SET Status = 'Booked' WHERE RoomID = p_RoomID;
    COMMIT;
END$$

CREATE PROCEDURE sp_CheckIn(IN p_BookingID INT)
BEGIN
    DECLARE v_RoomID INT;
    DECLARE v_Status VARCHAR(20);
    SELECT Status, RoomID INTO v_Status, v_RoomID FROM Booking WHERE BookingID = p_BookingID;
    IF v_Status != 'Confirmed' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Booking not found or already checked in.';
    END IF;
    START TRANSACTION;
        UPDATE Booking SET Status = 'CheckedIn' WHERE BookingID = p_BookingID;
        UPDATE Room    SET Status = 'CheckedIn' WHERE RoomID    = v_RoomID;
    COMMIT;
END$$

CREATE PROCEDURE sp_CheckOut(IN p_BookingID INT)
BEGIN
    DECLARE v_RoomID      INT;
    DECLARE v_TotalAmount DECIMAL(10,2);
    DECLARE v_Status      VARCHAR(20);
    SELECT Status, RoomID, TotalAmount INTO v_Status, v_RoomID, v_TotalAmount
    FROM Booking WHERE BookingID = p_BookingID;
    IF v_Status != 'CheckedIn' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Guest is not checked in.';
    END IF;
    START TRANSACTION;
        UPDATE Booking SET Status = 'CheckedOut' WHERE BookingID = p_BookingID;
        UPDATE Room    SET Status = 'Available'  WHERE RoomID    = v_RoomID;
        INSERT INTO Invoice (BookingID, TotalAmount, PaidStatus)
        VALUES (p_BookingID, v_TotalAmount, 0);
    COMMIT;
END$$

CREATE PROCEDURE sp_CancelBooking(
    IN p_BookingID INT, IN p_Reason VARCHAR(255), IN p_RefundAmount DECIMAL(10,2)
)
BEGIN
    DECLARE v_RoomID INT;
    DECLARE v_Status VARCHAR(20);
    SELECT Status, RoomID INTO v_Status, v_RoomID FROM Booking WHERE BookingID = p_BookingID;
    IF v_Status != 'Confirmed' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Only confirmed bookings can be cancelled.';
    END IF;
    START TRANSACTION;
        UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = p_BookingID;
        UPDATE Room    SET Status = 'Available'  WHERE RoomID   = v_RoomID;
        INSERT INTO Cancellation (BookingID, Reason, RefundAmount)
        VALUES (p_BookingID, p_Reason, p_RefundAmount);
    COMMIT;
END$$

CREATE PROCEDURE sp_RecordPayment(
    IN p_BookingID INT, IN p_Amount DECIMAL(10,2), IN p_Method VARCHAR(20)
)
BEGIN
    START TRANSACTION;
        INSERT INTO Payment (BookingID, Amount, Method, Status)
        VALUES (p_BookingID, p_Amount, p_Method, 'Paid');
        UPDATE Invoice SET PaidStatus = 1 WHERE BookingID = p_BookingID;
    COMMIT;
END$$

DELIMITER ;

-- ============================================
-- TRIGGERS
-- ============================================

DELIMITER $$

CREATE TRIGGER trg_AfterBookingUpdate
AFTER UPDATE ON Booking
FOR EACH ROW
BEGIN
    IF NEW.Status = 'Cancelled' AND OLD.Status != 'Cancelled' THEN
        UPDATE Room SET Status = 'Available' WHERE RoomID = NEW.RoomID;
    END IF;
END$$

CREATE TRIGGER trg_PreventDoubleBooking
BEFORE INSERT ON Booking
FOR EACH ROW
BEGIN
    DECLARE conflict_count INT;
    SELECT COUNT(*) INTO conflict_count
    FROM Booking
    WHERE RoomID = NEW.RoomID AND Status IN ('Confirmed', 'CheckedIn')
      AND NEW.CheckIn < CheckOut AND NEW.CheckOut > CheckIn;
    IF conflict_count > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Double booking detected. Room already booked for these dates.';
    END IF;
END$$

CREATE TRIGGER trg_LogPaymentErrors
AFTER INSERT ON Payment
FOR EACH ROW
BEGIN
    IF NEW.Amount <= 0 THEN
        INSERT INTO ErrorLog (Message, StackTrace, LogTime)
        VALUES ('Invalid payment amount detected.', 'trg_LogPaymentErrors', NOW());
    END IF;
END$$

DELIMITER ;

-- ============================================
-- SAMPLE DATA
-- ============================================

-- RoomType
INSERT INTO RoomType (TypeName, Description, Capacity, BasePrice) VALUES
('Single',  'Single bed room for one guest',        1, 3000.00),
('Double',  'Double bed room for two guests',       2, 5000.00),
('Suite',   'Luxury suite with all amenities',      4, 12000.00),
('Deluxe',  'Deluxe room with premium facilities',  2, 8000.00),
('Family',  'Spacious family room',                 6, 15000.00);

-- Room
INSERT INTO Room (RoomNumber, TypeID, Floor, PricePerNight, Status) VALUES
('101', 1, 1, 3000.00, 'Available'),
('102', 1, 1, 3000.00, 'Available'),
('201', 2, 2, 5000.00, 'Available'),
('202', 2, 2, 5000.00, 'Available'),
('301', 3, 3, 12000.00, 'Available'),
('302', 4, 3, 8000.00, 'Available'),
('401', 5, 4, 15000.00, 'Available'),
('103', 1, 1, 3000.00, 'Maintenance'),
('203', 2, 2, 5500.00, 'Available'),
('303', 3, 3, 13000.00, 'Available');

-- Amenity
INSERT INTO Amenity (Name, Description) VALUES
('WiFi',        'High-speed wireless internet'),
('AC',          'Air conditioning'),
('TV',          'LED Smart TV'),
('Mini Bar',    'In-room mini bar'),
('Jacuzzi',     'Private jacuzzi'),
('Parking',     'Free parking'),
('Breakfast',   'Complimentary breakfast');

-- RoomAmenity
INSERT INTO RoomAmenity (RoomID, AmenityID) VALUES
(1, 1),(1, 2),(1, 3),
(2, 1),(2, 2),(2, 3),
(3, 1),(3, 2),(3, 3),(3, 6),
(4, 1),(4, 2),(4, 3),(4, 6),
(5, 1),(5, 2),(5, 3),(5, 4),(5, 5),(5, 6),(5, 7),
(6, 1),(6, 2),(6, 3),(6, 4),(6, 6),
(7, 1),(7, 2),(7, 3),(7, 4),(7, 5),(7, 6),(7, 7);

-- Guest
INSERT INTO Guest (FullName, CNIC, Email, Phone, Address) VALUES
('Ali Hassan',       '3520112345671', 'ali.hassan@gmail.com',    '03001234567', 'House 12, Lahore'),
('Sara Khan',        '3520198765432', 'sara.khan@gmail.com',     '03111234567', 'Flat 5, Karachi'),
('Usman Malik',      '3520155544433', 'usman.malik@gmail.com',   '03221234567', 'Street 7, Islamabad'),
('Ayesha Noor',      '3520177788899', 'ayesha.noor@gmail.com',   '03331234567', 'Block C, Faisalabad'),
('Bilal Ahmed',      '3520133322211', 'bilal.ahmed@gmail.com',   '03441234567', 'Model Town, Lahore'),
('Zara Siddiqui',    '3520144455566', 'zara.sid@gmail.com',      '03551234567', 'DHA Phase 5, Lahore'),
('Hamza Qureshi',    '3520166677788', 'hamza.q@gmail.com',       '03661234567', 'Gulberg III, Lahore'),
('Fatima Zahra',     '3520188899900', 'fatima.z@gmail.com',      '03771234567', 'Johar Town, Lahore'),
('Tariq Mehmood',    '3520111122233', 'tariq.m@gmail.com',       '03881234567', 'Bahria Town, Lahore'),
('Nadia Iqbal',      '3520144433322', 'nadia.i@gmail.com',       '03991234567', 'Clifton, Karachi');

-- Employee
INSERT INTO Employee (FullName, CNIC, Phone, Role, Salary, HireDate) VALUES
('Mr. Rashad Khan',  '3520100011112', '03001111111', 'Manager',      85000.00, '2020-01-15'),
('Ahmed Raza',       '3520100022223', '03002222222', 'Receptionist', 45000.00, '2021-03-10'),
('Sana Butt',        '3520100033334', '03003333333', 'Receptionist', 45000.00, '2021-06-01'),
('Kamran Ali',       '3520100044445', '03004444444', 'Housekeeper',  30000.00, '2022-01-20'),
('Rizwan Shah',      '3520100055556', '03005555555', 'Housekeeper',  30000.00, '2022-04-15'),
('Asma Perveen',     '3520100066667', '03006666666', 'Admin',        60000.00, '2019-08-01'),
('Waqar Hussain',    '3520100077778', '03007777777', 'Housekeeper',  30000.00, '2023-02-10'),
('Rubina Tariq',     '3520100088889', '03008888888', 'Receptionist', 47000.00, '2022-11-05'),
('Saad Farooq',      '3520100099990', '03009999999', 'Housekeeper',  31000.00, '2023-06-01'),
('Hira Baig',        '3520100010001', '03000000001', 'Admin',        62000.00, '2020-09-15');

-- Login
INSERT INTO Login (EmployeeID, Username, PasswordHash, IsActive, LastLogin) VALUES
(1,  'rashad.mgr',   SHA2('Manager@123',   256), 1, '2025-06-01 09:00:00'),
(2,  'ahmed.rec',    SHA2('Ahmed@123',     256), 1, '2025-06-01 08:30:00'),
(3,  'sana.rec',     SHA2('Sana@123',      256), 1, '2025-06-01 08:45:00'),
(6,  'asma.admin',   SHA2('Admin@123',     256), 1, '2025-06-01 09:15:00'),
(10, 'hira.admin',   SHA2('Hira@123',      256), 1, '2025-05-30 10:00:00');

-- ServiceType
INSERT INTO ServiceType (Name, Price) VALUES
('Room Cleaning',   500.00),
('Laundry',         800.00),
('Breakfast',      1200.00),
('Dinner',         2000.00),
('Airport Pickup', 3000.00),
('Spa',            5000.00),
('Extra Bedding',   400.00);

-- Booking (mix of statuses for demo)
INSERT INTO Booking (GuestID, RoomID, BookingDate, CheckIn, CheckOut, Status, TotalAmount) VALUES
(1,  1, '2025-05-01', '2025-05-05', '2025-05-08', 'CheckedOut', 9000.00),
(2,  3, '2025-05-10', '2025-05-15', '2025-05-18', 'CheckedOut', 15000.00),
(3,  5, '2025-05-12', '2025-05-20', '2025-05-25', 'Cancelled',  60000.00),
(4,  6, '2025-05-14', '2025-05-22', '2025-05-26', 'CheckedOut', 32000.00),
(5,  2, '2025-05-18', '2025-06-01', '2025-06-04', 'CheckedOut', 9000.00),
(6,  4, '2025-05-20', '2025-06-05', '2025-06-08', 'Confirmed',  15000.00),
(7,  9, '2025-05-22', '2025-06-10', '2025-06-13', 'Confirmed',  16500.00),
(8,  7, '2025-05-25', '2025-06-15', '2025-06-20', 'Confirmed',  75000.00),
(9,  3, '2025-05-28', '2025-06-20', '2025-06-23', 'Confirmed',  15000.00),
(10, 10,'2025-05-30', '2025-07-01', '2025-07-05', 'Confirmed',  52000.00);

-- Payment
INSERT INTO Payment (BookingID, Amount, PaymentDate, Method, Status) VALUES
(1, 9000.00,  '2025-05-08', 'Cash',   'Paid'),
(2, 15000.00, '2025-05-18', 'Card',   'Paid'),
(4, 32000.00, '2025-05-26', 'Online', 'Paid'),
(5, 9000.00,  '2025-06-04', 'Cash',   'Paid'),
(3, 60000.00, '2025-05-21', 'Card',   'Refunded');

-- Invoice
INSERT INTO Invoice (BookingID, TotalAmount, GeneratedDate, PaidStatus) VALUES
(1, 9000.00,  '2025-05-08', 1),
(2, 15000.00, '2025-05-18', 1),
(4, 32000.00, '2025-05-26', 1),
(5, 9000.00,  '2025-06-04', 1);

-- Cancellation (BookingID 3 is 'Cancelled' so FK is valid)
INSERT INTO Cancellation (BookingID, Reason, RefundAmount, CancelDate) VALUES
(3, 'Guest requested cancellation due to emergency', 50000.00, '2025-05-21 14:00:00');

-- RoomService
INSERT INTO RoomService (BookingID, ServiceTypeID, Quantity, RequestTime, Status) VALUES
(1, 1, 1, '2025-05-06 10:00:00', 'Delivered'),
(1, 3, 2, '2025-05-06 08:00:00', 'Delivered'),
(2, 2, 1, '2025-05-16 11:00:00', 'Delivered'),
(2, 4, 1, '2025-05-17 19:00:00', 'Delivered'),
(4, 6, 1, '2025-05-23 15:00:00', 'Delivered'),
(5, 7, 2, '2025-06-02 09:00:00', 'Delivered'),
(6, 3, 1, '2025-06-06 08:00:00', 'Pending'),
(7, 1, 1, '2025-06-11 10:00:00', 'Pending');

-- Housekeeping
INSERT INTO Housekeeping (RoomID, EmployeeID, ScheduledDate, Status, Notes) VALUES
(1, 4, '2025-05-09', 'Done',      'Post checkout cleaning'),
(3, 5, '2025-05-19', 'Done',      'Deep cleaning after checkout'),
(5, 7, '2025-05-26', 'Done',      'Full suite cleaning'),
(6, 4, '2025-05-27', 'Done',      'Standard cleaning'),
(2, 9, '2025-06-05', 'Done',      'Post checkout cleaning'),
(1, 5, '2025-06-08', 'Pending',   'Pre-checkin prep'),
(8, 4, '2025-06-07', 'InProgress','Maintenance room cleaning'),
(9, 7, '2025-06-07', 'Pending',   'Routine cleaning');

-- ============================================
-- TRANSACTIONS
-- ============================================

-- TRANSACTION 1: Transfer Guest to Another Room
-- Guest in Booking 7 (Room 9) moves to Room 203 -> already Room 9, moving to Room 1 (available after cleanup)
START TRANSACTION;
SET @OldRoomID = 9;
SET @NewRoomID = 1;
SET @BID       = 7;

UPDATE Room    SET Status = 'Available'  WHERE RoomID    = @OldRoomID;
UPDATE Room    SET Status = 'Booked'     WHERE RoomID    = @NewRoomID;
UPDATE Booking SET RoomID = @NewRoomID   WHERE BookingID  = @BID;

COMMIT;


-- TRANSACTION 2: Process Refund on Cancellation (BookingID 3 exists and is Cancelled)
START TRANSACTION;
SET @CancelBookingID = 3;
SET @Refund          = 50000.00;

UPDATE Payment
SET Status = 'Refunded'
WHERE BookingID = @CancelBookingID AND Status = 'Paid';

UPDATE Invoice
SET PaidStatus = 0
WHERE BookingID = @CancelBookingID;

COMMIT;


-- TRANSACTION 3: Bulk Housekeeping Assignment for Available Rooms
START TRANSACTION;
SET @EmpID = 5;
SET @Today = CURDATE();

INSERT INTO Housekeeping (RoomID, EmployeeID, ScheduledDate, Status)
SELECT r.RoomID, @EmpID, @Today, 'Pending'
FROM Room r
WHERE r.Status = 'Available'
  AND NOT EXISTS (
      SELECT 1 FROM Housekeeping h
      WHERE h.RoomID = r.RoomID AND h.ScheduledDate = @Today
  );

COMMIT;