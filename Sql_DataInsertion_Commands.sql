select * FROM TicketReply;
select * FROM Ticket;
select * FROM TicketType;
select * FROM Employee;
select * FROM SLAs;
select * FROM Departments;

INSERT INTO Departments (DepartmentId, DepartmentName, Description)
VALUES
('D0001', 'IT Support', 'Handles IT related issues'),
('D0002', 'HR', 'Handles employee related queries'),
('D0003', 'Facilities', 'Manages building and infrastructure'),
('D0004', 'Finance', 'Handles billing and payroll');

INSERT INTO SLAs (SlaId, ResponseTime, ResolutionHours, Description)
VALUES
('S0001', 2, 24, 'High Priority'),
('S0002', 6, 72, 'Medium Priority'),
('S0003', 12, 120, 'Low Priority');

INSERT INTO Employee (EmpId, Name, Email, Password, Role, DepartmentId)
VALUES
('E0001', 'Amit Sharma', 'amit@company.com', 'amit123', 'User', 'D0001'),
('E0002', 'Neha Singh', 'neha@company.com', 'neha123', 'User', 'D0004'),
('E0003', 'Ravi Kumar', 'ravi@company.com', 'ravi123', 'User', 'D0002'),
('E0004', 'Priya Mehta', 'priya@company.com', 'priya123', 'User', 'D0003'),
('E0005', 'Abhishek Kumar', 'abhishek@company.com', 'abhi123', 'Admin', NULL);

INSERT INTO TicketType (TicketTypeId, TypeName, Description, DepartmentId, SlaId)
VALUES
('T0001', 'Login Issue', 'User unable to login', 'D0001', 'S0001'),
('T0002', 'Payroll Issue', 'Salary related problem', 'D0004', 'S0002'),
('T0003', 'Leave Request', 'Leave and attendance issue', 'D0002', 'S0003'),
('T0004', 'AC Problem', 'Air conditioning not working', 'D0003', 'S0002');

INSERT INTO Ticket 
(Title, Description, TicketTypeId, CreatedByEmpId, AssignedToEmpId, Status, CreatedAt, DueAt, ResolvedAt)
VALUES
('Cannot login to system',
 'User getting invalid password error',
 'T0001',
 'E0002',
 'E0001',
 'InProgress',
 '2026-01-22 09:30:00',
 '2026-01-23 09:30:00',
 NULL),

('Salary not credited',
 'January salary not credited to bank account',
 'T0002',
 'E0003',
 'E0002',
 'Open',
 '2026-01-22 10:00:00',
 '2026-01-25 10:00:00',
 NULL),

('Office AC not working',
 'AC in meeting room is not cooling',
 'T0004',
 'E0002',
 'E0004',
 'Resolved',
 '2026-01-22 08:45:00',
 '2026-01-24 08:45:00',
 '2026-01-22 12:15:00'),

('Coffee Machine is not working',
 'Coffee machine in pantry not starting',
 'T0004',
 'E0001',
 'E0004',
 'Open',
 '2026-01-23 09:00:00',
 '2026-01-24 21:00:00',
 NULL),

('Server is not working',
 'Internal server is down for all users',
 'T0001',
 'E0002',
 'E0001',
 'InProgress',
 '2026-01-23 09:15:00',
 '2026-01-23 21:15:00',
 NULL),

('Wifi bandwidth is low',
 'Network bandwidth is very slow',
 'T0001',
 'E0003',
 'E0001',
 'Resolved',
 '2026-01-23 08:30:00',
 '2026-01-24 08:30:00',
 '2026-01-23 13:30:00');

INSERT INTO TicketReply
(TicketId, RepliedByCreatorEmpId, RepliedByAssignedEmpId, Message, CreatedAt)
VALUES
(48, 'E0002', NULL, 'I am still facing the issue, please help.', '2026-01-22 10:05:00'),
(48, NULL, 'E0001', 'We are checking the logs, will update you soon.', '2026-01-22 10:20:00'),
(48, 'E0002', NULL, 'Thank you for the quick response.', '2026-01-22 10:35:00'),

(49, 'E0003', NULL, 'Please resolve this urgently.', '2026-01-22 11:00:00'),
(49, NULL, 'E0002', 'We are validating payroll details.', '2026-01-22 11:25:00'),

(50, 'E0002', NULL, 'AC issue seems resolved now.', '2026-01-22 12:10:00'),
(50, NULL, 'E0004', 'Yes, maintenance team fixed it.', '2026-01-22 12:30:00'),

(53, 'E0003', NULL, 'Wifi is still very slow in the morning.', '2026-01-23 09:05:00'),
(53, NULL, 'E0001', 'We are checking the router and ISP connection.', '2026-01-23 09:20:00'),
(53, 'E0003', NULL, 'Please prioritize this, meetings are impacted.', '2026-01-23 09:40:00'),
(53, NULL, 'E0001', 'Bandwidth has been upgraded. Please test now.', '2026-01-23 10:00:00'),

(51, 'E0001', NULL, 'Coffee machine is not powering on.', '2026-01-23 09:05:00'),
(51, NULL, 'E0004', 'Maintenance team has been assigned.', '2026-01-23 09:20:00'),
(51, 'E0001', NULL, 'Thanks, waiting for an update.', '2026-01-23 09:40:00'),
(51, NULL, 'E0004', 'Technician is on the way.', '2026-01-23 10:00:00'),
(51, 'E0001', NULL, 'Please update once fixed.', '2026-01-23 10:15:00'),
(51, NULL, 'E0004', 'Power module replaced. Testing now.', '2026-01-23 10:30:00'),
(51, 'E0001', NULL, 'Confirmed, it’s working now. Thanks!', '2026-01-23 10:45:00'),

(52, 'E0002', NULL, 'Server is down for all users.', '2026-01-23 09:20:00'),
(52, NULL, 'E0001', 'Restarting backend services.', '2026-01-23 09:35:00'),
(52, 'E0002', NULL, 'Still cannot access the portal.', '2026-01-23 09:55:00'),
(52, NULL, 'E0001', 'Database connection restored.', '2026-01-23 10:15:00'),
(52, 'E0002', NULL, 'System is loading now.', '2026-01-23 10:35:00'),
(52, NULL, 'E0001', 'Issue resolved, monitoring system.', '2026-01-23 10:50:00');

Delete FROM TicketReply;
Delete FROM Ticket;
Delete FROM TicketType;
Delete FROM Employee;
Delete FROM SLAs;
Delete FROM Departments;
