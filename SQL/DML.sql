use p4g1;

/* Insert sample data */

-- Insert sample data into Contacts
insert into NBA.[Contract] (ID, [Description], Salary, [Start_Date], End_Date) values
--Lakers players
(10001, 'Lebron James Contract', 39000000, '2021-08-02', '2023-06-30'),
(10002, 'Anthony Davis Contract', 35000000, '2020-07-06', '2025-06-30'),
(10003, 'Russell Westbrook Contract', 44000000, '2021-08-06', '2023-06-30'),
(10004, 'Carmelo Anthony Contract', 2500000, '2021-08-03', '2022-06-30'),
(10005, 'Malik Monk Contract', 2000000, '2021-08-03', '2022-06-30'),
(10006, 'Talen Horton-Tucker Contract', 2000000, '2021-08-03', '2022-06-30'),
(10007, 'Dwight Howard Contract', 2500000, '2021-08-03', '2022-06-30'),
(10008, 'Wayne Ellington Contract', 2500000, '2021-08-03', '2022-06-30'),
(10009, 'DeAndre Jordan Contract', 2500000, '2021-08-03', '2022-06-30'),
(10010, 'Kendrick Nunn Contract', 5000000, '2021-08-03', '2022-06-30'),

--Celtics players
(10011, 'Jayson Tatum Contract', 30000000, '2021-08-06', '2026-06-30'),
(10012, 'Jaylen Brown Contract', 25000000, '2020-10-21', '2024-06-30'),
(10013, 'Al Horford Contract', 27000000, '2021-08-06', '2023-06-30'),
(10014, 'Marcus Smart Contract', 14000000, '2021-08-06', '2022-06-30'),
(10015, 'Josh Richardson Contract', 11000000, '2021-08-06', '2022-06-30'),
(10016, 'Robert Williams III Contract', 2000000, '2021-08-06', '2022-06-30'),
(10017, 'Dennis Schroder Contract', 5000000, '2021-08-06', '2022-06-30'),
(10018, 'Grant Williams Contract', 2000000, '2021-08-06', '2022-06-30'),
(10019, 'Romeo Langford Contract', 2000000, '2021-08-06', '2022-06-30'),
(10020, 'Enes Kanter Contract', 2500000, '2021-08-06', '2022-06-30'),

--Warriors players
(10021, 'Stephen Curry Contract', 45000000, '2021-08-06', '2026-06-30'),
(10022, 'Klay Thompson Contract', 38000000, '2019-07-01', '2024-06-30'),
(10023, 'Draymond Green Contract', 24000000, '2019-08-03', '2024-06-30'),
(10024, 'Andrew Wiggins Contract', 31000000, '2020-07-07', '2023-06-30'),
(10025, 'James Wiseman Contract', 9000000, '2020-11-22', '2024-06-30'),
(10026, 'Kevon Looney Contract', 5000000, '2019-07-01', '2022-06-30'),
(10027, 'Jordan Poole Contract', 2000000, '2021-08-06', '2022-06-30'),
(10028, 'Damion Lee Contract', 2000000, '2021-08-06', '2022-06-30'),
(10029, 'Eric Paschall Contract', 2000000, '2021-08-06', '2022-06-30'),
(10030, 'Juan Toscano-Anderson Contract', 2000000, '2021-08-06', '2022-06-30'),

--Knicks players
(10031, 'Julius Randle Contract', 29000000, '2021-08-06', '2024-06-30'),
(10032, 'RJ Barrett Contract', 9000000, '2019-07-01', '2022-06-30'),
(10033, 'Kemba Walker Contract', 18000000, '2021-08-06', '2024-06-30'),
(10034, 'Evan Fournier Contract', 18000000, '2021-08-06', '2024-06-30'),
(10035, 'Mitchell Robinson Contract', 18000000, '2021-08-06', '2024-06-30'),
(10036, 'Nerlens Noel Contract', 10000000, '2021-08-06', '2024-06-30'),
(10037, 'Derrick Rose Contract', 10000000, '2021-08-06', '2024-06-30'),
(10038, 'Immanuel Quickley Contract', 10000000, '2021-08-06', '2024-06-30'),
(10039, 'Obi Toppin Contract', 5000000, '2021-08-06', '2024-06-30'),
(10040, 'Alec Burks Contract', 2000000, '2021-08-06', '2024-06-30'),

--Bulls players
(10041, 'Contract of Zach LaVine', 19500000, '2022-01-01', '2025-12-31'),
(10042, 'Contract of Nikola Vucevic', 24000000, '2021-01-01', '2024-12-31'),
(10043, 'Contract of DeMar DeRozan', 28000000, '2021-01-01', '2024-12-31'),
(10044, 'Contract of Lonzo Ball', 20000000, '2021-01-01', '2024-12-31'),
(10045, 'Contract of Alex Caruso', 7000000, '2022-01-01', '2024-12-31'),
(10046, 'Contract of Patrick Williams', 7452000, '2021-01-01', '2024-12-31'),
(10047, 'Contract of Troy Brown Jr.', 3702000, '2021-01-01', '2023-12-31'),
(10048, 'Contract of Coby White', 8512000, '2021-01-01', '2023-12-31'),
(10049, 'Contract of Javonte Green', 1700000, '2021-01-01', '2022-12-31'),
(10050, 'Contract of Tony Bradley', 1575000, '2022-01-01', '2023-12-31'),

--Heat players
(10051, 'Jimmy Butler Contract', 36150000, '2021-07-01', '2025-06-30'),
(10052, 'Bam Adebayo Contract', 28530608, '2020-11-23', '2025-06-30'),
(10053, 'Kyle Lowry Contract', 30000000, '2021-08-06', '2023-06-30'),
(10054, 'Tyler Herro Contract', 3769920, '2019-07-01', '2022-06-30'),
(10055, 'Duncan Robinson Contract', 9000000, '2021-08-06', '2025-06-30'),
(10056, 'PJ Tucker Contract', 2368545, '2021-08-03', '2022-06-30'),
(10057, 'Marquis Teague Contract', 449115, '2021-10-15', '2022-06-30'),
(10058, 'Dewayne Dedmon Contract', 2827982, '2021-07-01', '2022-06-30'),
(10059, 'Max Strus Contract', 449115, '2020-12-20', '2022-06-30'),
(10060, 'Goran Dragic Contract', 19000000, '2020-11-22', '2023-06-30'),

--Mavericks players
(10061, 'Luka Doncic Contract', 40269600, '2021-07-06', '2027-07-06'),
(10062, 'Kristaps Porzingis Contract', 29802321, '2019-07-01', '2024-07-01'),
(10063, 'Tim Hardaway Jr. Contract', 17199999, '2021-07-06', '2024-07-06'),
(10064, 'Dwight Powell Contract', 11000000, '2019-07-07', '2023-07-07'),
(10065, 'Jalen Brunson Contract', 1953846, '2018-07-01', '2022-07-01'),
(10066, 'Josh Green Contract', 2705280, '2020-11-20', '2023-11-20'),
(10067, 'Maxi Kleber Contract', 8000000, '2020-11-22', '2023-11-22'),
(10068, 'Dorian Finney-Smith Contract', 12771039, '2020-11-23', '2023-11-23'),
(10069, 'Trey Burke Contract', 3268519, '2021-08-19', '2022-08-19'),
(10070, 'Willie Cauley-Stein Contract', 4200000, '2021-08-20', '2022-08-20'),

--Spurs players
(10071, 'Dejounte Murray Contract', 16000000, '2021-10-01', '2026-09-30'),
(10072, 'Derrick White Contract', 16000000, '2021-10-01', '2024-09-30'),
(10073, 'Jakob Poeltl Contract', 8750000, '2021-10-01', '2023-09-30'),
(10074, 'Keldon Johnson Contract', 4164000, '2021-10-01', '2023-09-30'),
(10075, 'Lonnie Walker IV Contract', 4389600, '2021-10-01', '2022-09-30'),
(10076, 'Thaddeus Young Contract', 14832000, '2021-10-01', '2022-09-30'),
(10077, 'Joshua Primo Contract', 6100000, '2021-10-01', '2026-09-30'),
(10078, 'Doug McDermott Contract', 14000000, '2021-10-01', '2023-09-30'),
(10079, 'Bryn Forbes Contract', 3500000, '2021-10-01', '2022-09-30'),
(10080, 'Devin Vassell Contract', 4364400, '2021-10-01', '2024-09-30'),

-- Rockets players
(10081, 'Christian Wood', 13750000, '2021-07-01', '2024-06-30'),
(10082, 'Jalen Green', 8941200, '2021-08-01', '2024-06-30'),
(10083, 'Kevin Porter Jr.', 1757160, '2020-12-23', '2021-06-30'),
(10084, 'Jae''Sean Tate', 1749840, '2020-12-18', '2021-06-30'),
(10085, 'Daniel Theis', 9000000, '2021-08-06', '2022-06-30'),
(10086, 'Eric Gordon', 18622500, '2019-07-10', '2022-06-30'),
(10087, 'Alperen Sengun', 3822000, '2021-08-02', '2024-06-30'),
(10088, 'David Nwaba', 3000000, '2020-11-25', '2021-06-30'),
(10089, 'Armoni Brooks', 925258, '2020-12-19', '2021-06-30'),
(10090, 'D.J. Augustin', 7000000, '2021-08-06', '2022-06-30'),

-- Trail Blazers players
(10091, 'Damian Lillard', 43545455, '2019-07-01', '2024-07-01'),
(10092, 'CJ McCollum', 30864198, '2017-07-01', '2024-07-01'),
(10093, 'Norman Powell', 18000000, '2021-08-06', '2024-07-01'),
(10094, 'Robert Covington', 12500000, '2020-11-22', '2023-07-01'),
(10095, 'Jusuf Nurkic', 12000000, '2018-07-06', '2022-07-01'),
(10096, 'Anfernee Simons', 3000000, '2021-08-07', '2022-07-01'),
(10097, 'Larry Nance Jr.', 10000000, '2021-08-07', '2023-07-01'),
(10098, 'Cody Zeller', 7500000, '2021-08-07', '2022-07-01'),
(10099, 'Tony Snell', 5000000, '2021-08-07', '2022-07-01'),
(10100, 'Nassir Little', 3000000, '2021-08-07', '2022-07-01'),

-- Treinadores
(10101, 'Frank Vogel', 4800000, '2021-08-07', '2024-07-01'),
(10102, 'Steve Kerr', 9000000, '2019-07-01', '2024-07-01'),
(10103, 'Tom Thibodeau', 7500000, '2020-07-30', '2024-07-01'),
(10104, 'Billy Donovan', 6000000, '2020-09-22', '2024-07-01'),
(10105, 'Erik Spoelstra', 7500000, '2019-07-01', '2024-07-01'),
(10106, 'Jason Kidd', 7000000, '2021-06-28', '2024-07-01'),
(10107, 'Gregg Popovich', 10000000, '2019-07-01', '2022-06-30'),
(10108, 'Stephen Silas', 2400000, '2021-05-28', '2023-06-30'),
(10109, 'Chauncey Billups', 2400000, '2021-07-13', '2023-06-30'),
(10110, 'Terry Stotts', 5000000, '2020-11-21', '2022-06-30'),

-- Owners
(10111, 'Jeanie Buss', 500000000, '2021-08-07', '2024-07-01'),
(10112, 'Steve Ballmer', 7000000000, '2014-08-12', '2027-06-30'),
(10113, 'Wyc Grousbeck', 4000000000, '2002-12-31', '2029-03-09'),
(10114, 'Tilman Fertitta', 4000000000, '2017-10-06', '2025-08-02'),
(10115, 'Jody Allen', 3000000000, '2018-10-09', '2026-01-34'),
(10116, 'Mark Cuban', 4000000000, '2000-01-04', '2027-05-20'),
(10117, 'Joe Lacob', 4000000000, '2010-11-12', '2023-08-10'),
(10118, 'Robert Pera', 4000000000, '2012-10-25', '2027-06-30'),
(10119, 'Herb Simon', 4000000000, '1983-06-01', '2028-03-04'),
(10120, 'Glen Taylor', 4000000000, '1994-07-30', '2027-02-18');

-- Insert sample data into Person
insert into NBA.Person (CCNumber, [Name], Age, Contract_ID) values
-- Lakers players
(10000001, 'LeBron James', 36, 10001),
(10000002, 'Anthony Davis', 28, 10002),
(10000003, 'Russell Westbrook', 32, 10003),
(10000004, 'Carmelo Anthony', 37, 10004),
(10000005, 'Malik Monk', 23, 10005),
(10000006, 'Talen Horton-Tucker', 20, 10006),
(10000007, 'Dwight Howard', 35, 10007),
(10000008, 'Wayne Ellington', 33, 10008),
(10000009, 'DeAndre Jordan', 33, 10009),
(10000010, 'Kendrick Nunn', 26, 10010),

-- Celtics players
(10000011, 'Jayson Tatum', 23, 10011),
(10000012, 'Jaylen Brown', 24, 10012),
(10000013, 'Al Horford', 35, 10013),
(10000014, 'Marcus Smart', 27, 10014),
(10000015, 'Josh Richardson', 27, 10015),
(10000016, 'Robert Williams III', 23, 10016),
(10000017, 'Dennis Schroder', 27, 10017),
(10000018, 'Grant Williams', 23, 10018),
(10000019, 'Romeo Langford', 21, 10019),
(10000020, 'Enes Kanter', 29, 10020),

-- Warriors players
(10000021, 'Stephen Curry', 33, 10021),
(10000022, 'Klay Thompson', 31, 10022),
(10000023, 'Draymond Green', 31, 10023),
(10000024, 'Andrew Wiggins', 26, 10024),
(10000025, 'James Wiseman', 20, 10025),
(10000026, 'Kevon Looney', 25, 10026),
(10000027, 'Jordan Poole', 22, 10027),
(10000028, 'Damion Lee', 28, 10028),
(10000029, 'Eric Paschall', 24, 10029),
(10000030, 'Juan Toscano-Anderson', 28, 10030),
-- Knicks players
(10000031, 'Julius Randle', 26, 10031),
(10000032, 'RJ Barrett', 21, 10032),
(10000033, 'Kemba Walker', 31, 10033),
(10000034, 'Evan Fournier', 28, 10034),
(10000035, 'Mitchell Robinson', 23, 10035),
(10000036, 'Nerlens Noel', 27, 10036),
(10000037, 'Derrick Rose', 32, 10037),
(10000038, 'Immanuel Quickley', 22, 10038),
(10000039, 'Obi Toppin', 23, 10039),
(10000040, 'Alec Burks', 30, 10040),
-- Bulls players
(10000041, 'Zach LaVine', 26, 10041),
(10000042, 'Nikola Vucevic', 30, 10042),
(10000043, 'DeMar DeRozan', 32, 10043),
(10000044, 'Lonzo Ball', 23, 10044),
(10000045, 'Alex Caruso', 27, 10045),
(10000046, 'Patrick Williams', 20, 10046),
(10000047, 'Troy Brown Jr.', 22, 10047),
(10000048, 'Coby White', 21, 10048),
(10000049, 'Javonte Green', 28, 10049),
(10000050, 'Tony Bradley', 23, 10050),
-- Heat players
(10000051, 'Jimmy Butler', 32, 10051),
(10000052, 'Bam Adebayo', 24, 10052),
(10000053, 'Kyle Lowry', 35, 10053),
(10000054, 'Tyler Herro', 21, 10054),
(10000055, 'Duncan Robinson', 27, 10055),
(10000056, 'PJ Tucker', 36, 10056),
(10000057, 'Marquis Teague', 28, 10057),
(10000058, 'Dewayne Dedmon', 32, 10058),
(10000059, 'Max Strus', 25, 10059),
(10000060, 'Goran Dragic', 35, 10060),
-- Mavericks players
(10000061, 'Luka Doncic', 22, 10061),
(10000062, 'Kristaps Porzingis', 26, 10062),
(10000063, 'Tim Hardaway Jr.', 29, 10063),
(10000064, 'Dwight Powell', 30, 10064),
(10000065, 'Jalen Brunson', 25, 10065),
(10000066, 'Josh Green', 20, 10066),
(10000067, 'Maxi Kleber', 29, 10067),
(10000068, 'Dorian Finney-Smith', 28, 10068),
(10000069, 'Trey Burke', 28, 10069),
(10000070, 'Willie Cauley-Stein', 28, 10070),
-- Spurs players
(10000071, 'Dejounte Murray', 25, 10071),
(10000072, 'Derrick White', 27, 10072),
(10000073, 'Jakob Poeltl', 26, 10073),
(10000074, 'Keldon Johnson', 22, 10074),
(10000075, 'Lonnie Walker IV', 22, 10075),
(10000076, 'Thaddeus Young', 33, 10076),
(10000077, 'Joshua Primo', 18, 10077),
(10000078, 'Doug McDermott', 29, 10078),
(10000079, 'Bryn Forbes', 28, 10079),
(10000080, 'Devin Vassell', 20, 10080),
-- Rockets players
(10000081, 'Christian Wood', 26, 10081),
(10000082, 'Jalen Green', 19, 10082),
(10000083, 'Kevin Porter Jr.', 21, 10083),
(10000084, 'Jae''Sean Tate', 25, 10084),
(10000085, 'Daniel Theis', 29, 10085),
(10000086, 'Eric Gordon', 32, 10086),
(10000087, 'Alperen Sengun', 19, 10087),
(10000088, 'David Nwaba', 28, 10088),
(10000089, 'Armoni Brooks', 22, 10089),
(10000090, 'D.J. Augustin', 33, 10090),
-- Trail Blazers players
(10000091, 'Damian Lillard', 31, 10091),
(10000092, 'CJ McCollum', 29, 10092),
(10000093, 'Norman Powell', 28, 10093),
(10000094, 'Robert Covington', 30, 10094),
(10000095, 'Jusuf Nurkic', 27, 10095),
(10000096, 'Anfernee Simons', 22, 10096),
(10000097, 'Larry Nance Jr.', 28, 10097),
(10000098, 'Cody Zeller', 29, 10098),
(10000099, 'Tony Snell', 29, 10099),
(10000100, 'Nassir Little', 21, 10100),
-- Treinadores
(10000101, 'Frank Vogel', 48, 10101),
(10000102, 'Steve Kerr', 56, 10102),
(10000103, 'Tom Thibodeau', 63, 10103),
(10000104, 'Billy Donovan', 56, 10104),
(10000105, 'Erik Spoelstra', 51, 10105),
(10000106, 'Jason Kidd', 48, 10106),
(10000107, 'Gregg Popovich', 72, 10107),
(10000108, 'Stephen Silas', 48, 10108),
(10000109, 'Chauncey Billups', 45, 10109),
(10000110, 'Terry Stotts', 64, 10110),
--Owners
(10000111, 'Jeanie Buss', 60, 10111),
(10000112, 'Wyc Grousbeck', 60, 10112),
(10000113, 'Joe Lacob', 65, 10113),
(10000114, 'James Dolan', 65, 10114),
(10000115, 'Jerry Reinsdorf', 63, 10115),
(10000116, 'Micky Arison', 71, 10116),
(10000117, 'Mark Cuban', 43, 10117),
(10000118, 'Peter Holt', 86, 10118),
(10000119, 'Tilman Fertitta', 63, 10119),
(10000120, 'Jody Allen', 65, 10120);

-- Insert sample data into Coach
insert into NBA.Coach (CCNumber) values
(10000101),
(10000102),
(10000103),
(10000104),
(10000105),
(10000106),
(10000107),
(10000108),
(10000109),
(10000110);

/* Insert more sample data into Team */
insert into NBA.Team (ID, [Name], City, Conference, Found_Year, Owner_CCNumber, Coach_CCNumber) values
(1, 'Los Angeles Lakers', 'Los Angeles', 'Western', 1947, 10000111, 10000101),
(2, 'Boston Celtics', 'Boston', 'Eastern', 1946, 10000112, 10000102),
(3, 'Golden State Warriors', 'San Francisco', 'Western', 1946, 10000113, 10000103),
(4, 'New York Knicks', 'New York', 'Eastern', 1946, 10000114, 10000104),
(5, 'Chicago Bulls', 'Chicago', 'Eastern', 1966, 10000115, 10000105),
(6, 'Miami Heat', 'Miami', 'Eastern', 1988, 10000116, 10000106),
(7, 'Dallas Mavericks', 'Dallas', 'Western', 1980, 10000117, 10000107),
(8, 'San Antonio Spurs', 'San Antonio', 'Western', 1967, 10000118, 10000108),
(9, 'Houston Rockets', 'Houston', 'Western', 1967, 10000119, 10000109),
(10, 'Portland Trail Blazers', 'Portland', 'Western', 1970, 10000120, 10000110);

-- Insert sample data into Player
insert into NBA.Player (CCNumber, [Number], Height, [Weight], Position, Team_ID) values
-- Lakers players
(1, 23, '6-9', 250, 'Forward', 1),
(2, 3, '6-1', 253, 'Forward-Center', 1),
(3, 0, '6-3', 200, 'Guard', 1),
(4, 7, '6-8', 238, 'Forward', 1),
(5, 11, '6-3', 200, 'Guard', 1),
(6, 5, '6-4', 234, 'Guard-Forward', 1),
(7, 39, '6-1', 265, 'Center', 1),
(8, 2, '6-4', 207, 'Guard', 1),
(9, 6, '6-1', 265, 'Center', 1),
(10, 25, '6-2', 190, 'Guard', 1),
-- Celtics players
(11, 0, '6-8', 210, 'Forward', 2),
(12, 7, '6-6', 223, 'Guard-Forward', 2),
(13, 42, '6-9', 240, 'Forward-Center', 2),
(14, 36, '6-3', 227, 'Guard', 2),
(15, 8, '6-5', 200, 'Guard-Forward', 2),
(16, 44, '6-8', 237, 'Center-Forward', 2),
(17, 17, '6-1', 172, 'Guard', 2),
(18, 12, '6-6', 236, 'Forward', 2),
(19, 45, '6-4', 216, 'Guard', 2),
(20, 11, '6-1', 250, 'Center', 2),
-- Warriors players
(21, 30, '6-3', 185, 'Guard', 3),
(22, 11, '6-6', 215, 'Guard', 3),
(23, 23, '6-6', 230, 'Forward-Center', 3),
(24, 22, '6-7', 197, 'Forward', 3),
(25, 33, '7-0', 240, 'Center', 3),
(26, 3, '6-4', 194, 'Guard', 3),
(27, 30, '6-8', 198, 'Forward', 3),
(28, 8, '6-1', 234, 'Forward', 3),
(29, 1, '6-5', 210, 'Guard', 3),
(30, 95, '6-6', 209, 'Forward', 3),
-- Knicks players
(31, 30, '6-8', 250, 'Forward', 4),
(32, 9, '6-6', 214, 'Guard-Forward', 4),
(33, 23, '7-0', 240, 'Center', 4),
(34, 94, '6-6', 205, 'Guard-Forward', 4),
(35, 4, '6-2', 200, 'Guard', 4),
(36, 1, '6-9', 220, 'Forward', 4),
(37, 5, '6-3', 190, 'Guard', 4),
(38, 20, '6-7', 215, 'Forward', 4),
(39, 10, '6-6', 39, 'Guard-Forward', 4),
(40, 3, '6-1', 27, 'Center-Forward', 4),
-- Bulls players
(41, 8, '6-6', 200, 'Guard', 5),
(42, 11, '6-6', 220, 'Guard-Forward', 5),
(43, 9, '6-6', 190, 'Center', 5),
(44, 2, '6-6', 190, 'Guard', 5),
(45, 6, '6-4', 186, 'Guard', 5),
(46, 5, '6-6', 210, 'Forward', 5),
(47, 4, '6-6', 215, 'Forward', 5),
(48, 0, '6-4', 195, 'Guard', 5),
(49, 43, '6-4', 205, 'Guard-Forward', 5),
(50, 11, '6-1', 248, 'Center', 5),
-- Heat players
(51, 22, '6-7', 230, 'Forward', 6),
(52, 13, '6-9', 255, 'Center-Forward', 6),
(53, 7, '6-0', 196, 'Guard', 6),
(54, 14, '6-5', 245, 'Guard-Forward', 6),
(55, 55, '6-5', 245, 'Guard', 6),
(56, 17, '6-5', 245, 'Forward', 6),
(57, 31, '6-5', 215, 'Guard', 6),
(58, 8, '6-8', 245, 'Forward', 6),
(59, 21, '7-0', 245, 'Center', 6),
(60, 77, '7-0', 275, 'Center', 6),
-- Mavericks players
(61, 77, '6-7', 230, 'Guard-Forward', 7),
(62, 6, '7-3', 240, 'Forward-Center', 7),
(63, 11, '6-5', 205, 'Guard-Forward', 7),
(64, 7, '6-1', 240, 'Forward-Center', 7),
(65, 42, '6-1', 240, 'Forward-Center', 7),
(66, 13, '6-1', 190, 'Guard', 7),
(67, 8, '6-6', 210, 'Guard-Forward', 7),
(68, 25, '6-6', 215, 'Guard-Forward', 7),
(69, 51, '7-4', 290, 'Center', 7),
(70, 11, '6-4', 200, 'Guard', 7),
-- Spurs players
(71, 5, '6-4', 180, 'Guard', 8),
(72, 4, '6-4', 195, 'Guard', 8),
(73, 1, '6-5', 204, 'Guard', 8),
(74, 3, '6-5', 220, 'Forward', 8),
(75, 25, '7-0', 240, 'Center', 8),
(76, 17, '6-7', 225, 'Forward', 8),
(77, 11, '6-2', 195, 'Guard', 8),
(78, 21, '6-8', 235, 'Forward', 8),
(79, 11, '6-5', 190, 'Guard', 8),
(80, 23, '6-1', 250, 'Forward-Center', 8),
-- Rockets players
(81, 35, '6-1', 214, 'Forward-Center', 9),
(82, 0, '6-6', 178, 'Guard', 9),
(83, 4, '6-4', 203, 'Guard', 9),
(84, 9, '6-1', 240, 'Forward-Center', 9),
(85, 10, '6-3', 215, 'Guard', 9),
(86, 8, '6-4', 230, 'Forward', 9),
(87, 4, '6-6', 220, 'Forward', 9),
(88, 27, '6-8', 245, 'Forward-Center', 9),
(89, 2, '6-5', 219, 'Guard-Forward', 9),
(90, 9, '6-8', 229, 'Forward', 9),
-- Trail Blazers players
(91, 0, '6-2', 195, 'Guard', 10),
(92, 3, '6-3', 190, 'Guard', 10),
(93, 24, '6-3', 215, 'Guard-Forward', 10),
(94, 27, '7-0', 290, 'Center', 10),
(95, 23, '6-7', 209, 'Forward', 10),
(96, 40, '7-0', 240, 'Center', 10),
(97, 19, '6-6', 213, 'Guard-Forward', 10),
(98, 0, '6-0', 195, 'Guard', 10),
(99, 22, '6-7', 245, 'Forward', 10),
(100, 1, '6-2', 185, 'Guard', 10);

-- Insert sample data into Stadium
insert into NBA.Stadium (ID, [Name], [Location], Capacity, Team_ID) values
(1, 'Staples Center', 'Los Angeles', 19060, 1),
(2, 'TD Garden', 'Boston', 18624, 2),
(3, 'Chase Center', 'San Francisco', 18064, 3),
(4, 'Madison Square Garden', 'New York', 19812, 4),
(5, 'United Center', 'Chicago', 20917, 5),
(6, 'American Airlines Arena', 'Miami', 19600, 6),
(7, 'American Airlines Center', 'Dallas', 19200, 7),
(8, 'AT&T Center', 'San Antonio', 18418, 8),
(9, 'Toyota Center', 'Houston', 18055, 9),
(10, 'Moda Center', 'Portland', 19441, 10);

-- Insert sample data into Game
insert into NBA.Game (ID, [Time], [Date], Home_Score, Away_Score, Home_Team_ID, Away_Team_ID, Stadium_ID) values
(1, '19:00', '2023-01-01', 110, 105, 1, 2, 1),
(2, '20:00', '2023-01-02', 105, 95, 3, 4, 4),
(3, '21:00', '2023-01-03', 115, 100, 5, 6, 6),
(4, '19:30', '2023-01-04', 102, 98, 7, 8, 7),
(5, '18:00', '2023-01-05', 95, 90, 9, 10, 9),
(6, '19:30', '2023-01-06', 99, 97, 2, 1, 2),
(7, '20:00', '2023-01-07', 92, 85, 4, 3, 3),
(8, '21:00', '2023-01-08', 100, 97, 6, 5, 6),
(9, '19:30', '2023-01-09', 105, 100, 8, 7, 8),
(10, '18:00', '2023-01-10', 110, 105, 10, 9, 10),
(11, '19:00', '2023-01-11', 115, 105, 2, 4, 2),
(12, '20:00', '2023-01-12', 95, 90, 3, 5, 3),
(13, '21:00', '2023-01-13', 97, 92, 6, 8, 6),
(14, '19:30', '2023-01-14', 90, 85, 9, 10, 9),
(15, '18:00', '2023-01-15', 102, 98, 1, 3, 1),
(16, '19:30', '2023-01-16', 105, 100, 4, 6, 4),
(17, '20:00', '2023-01-17', 95, 90, 7, 9, 7),
(18, '19:30', '2023-01-18', 110, 105, 8, 2, 8),
(19, '18:00', '2023-01-19', 97, 92, 3, 1, 3),
(20, '19:00', '2023-01-20', 85, 80, 6, 4, 6),
(21, '20:00', '2023-01-21', 105, 100, 9, 7, 9),
(22, '21:00', '2023-01-22', 92, 87, 5, 3, 5),
(23, '19:30', '2023-01-23', 100, 95, 2, 10, 2),
(24, '18:00', '2023-01-24', 110, 105, 1, 8, 1),
(25, '19:30', '2023-01-25', 98, 92, 4, 6, 4),
(26, '20:00', '2023-01-26', 90, 85, 7, 9, 7),
(27, '21:00', '2023-01-27', 105, 100, 10, 5, 10),
(28, '19:00', '2023-01-28', 97, 92, 3, 1, 3),
(29, '20:00', '2023-01-29', 80, 75, 6, 4, 6),
(30, '21:00', '2023-01-30', 100, 95, 9, 7, 9),
(31, '19:30', '2023-01-31', 92, 87, 5, 3, 5),
(32, '18:00', '2023-02-01', 100, 95, 2, 10, 2),
(33, '19:30', '2023-02-02', 110, 105, 1, 8, 1),
(34, '20:00', '2023-02-03', 95, 90, 4, 6, 4),
(35, '21:00', '2023-02-04', 85, 80, 7, 9, 7),
(36, '19:30', '2023-02-05', 105, 100, 10, 5, 10),
(37, '19:00', '2023-02-06', 92, 93, 3, 1, 3),
(38, '20:00', '2023-02-07', 80, 75, 6, 4, 6),
(39, '21:00', '2023-02-08', 100, 95, 9, 7, 9),
(40, '19:30', '2023-02-09', 92, 87, 5, 3, 5),
(41, '18:00', '2023-02-10', 100, 95, 2, 10, 2),
(42, '19:30', '2023-02-11', 110, 105, 1, 8, 1),
(43, '20:00', '2023-02-12', 95, 90, 4, 6, 4),
(44, '21:00', '2023-02-13', 85, 80, 7, 9, 7),
(45, '19:30', '2023-02-14', 105, 100, 10, 5, 10),
(46, '18:00', '2023-02-15', 97, 92, 3, 1, 3),
(47, '19:00', '2023-02-16', 80, 75, 6, 4, 6),
(48, '20:00', '2023-02-17', 100, 95, 9, 7, 9),
(49, '21:00', '2023-02-18', 92, 87, 5, 3, 5),
(50, '19:30', '2023-02-19', 100, 95, 2, 10, 2),
(51, '18:00', '2023-02-20', 110, 105, 1, 8, 1),
(52, '19:30', '2023-02-21', 95, 90, 4, 6, 4),
(53, '20:00', '2023-02-22', 85, 80, 7, 9, 7),
(54, '21:00', '2023-02-23', 105, 100, 10, 5, 10),
(55, '19:00', '2023-02-24', 97, 92, 3, 1, 3),
(56, '20:00', '2023-02-25', 80, 75, 6, 4, 6),
(57, '21:00', '2023-02-26', 100, 95, 9, 7, 9),
(58, '19:30', '2023-02-27', 92, 87, 5, 3, 5),
(59, '18:00', '2023-02-28', 100, 95, 2, 10, 2),
(60, '19:30', '2023-03-01', 110, 105, 1, 8, 1),
(61, '19:00', '2023-03-02', null, null, 5, 2, 5),
(62, '20:00', '2023-03-03', null, null, 3, 4, 3),
(63, '21:00', '2023-03-04', null, null, 10, 7, 10),
(64, '19:30', '2023-03-05', null, null, 6, 1, 6),
(65, '18:00', '2023-03-06', null, null, 9, 8, 9),
(66, '19:30', '2023-03-07', null, null, 2, 3, 2),
(67, '20:00', '2023-03-08', null, null, 1, 5, 1),
(68, '21:00', '2023-03-09', null, null, 4, 10, 4),
(69, '19:00', '2023-03-10', null, null, 7, 6, 7),
(70, '18:00', '2023-03-11', null, null, 8, 9, 8);

-- Insert sample data into Inivididual_Numbers
insert into NBA.Average_Individual_Numbers (Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%], Player_CCNumber) values
(25, 5, 10, 2, 1, 52, 40, 1),
(20, 8, 4, 0, 2, 46, 38, 2),
(30, 7, 6, 1, 3, 55, 45, 3),
(18, 6, 3, 0, 1, 44, 35, 4),
(28, 6, 8, 2, 2, 54, 42, 5),
(22, 9, 5, 0, 3, 48, 39, 6),
(35, 8, 7, 1, 4, 56, 47, 7),
(20, 7, 4, 0, 1, 46, 37, 8),
(32, 10, 8, 1, 3, 54, 44, 9),
(22, 6, 5, 0, 2, 48, 40, 10),
(27, 7, 4, 0, 1, 51, 41, 11),
(12, 8, 10, 3, 2, 55, 47, 12),
(25, 4, 6, 2, 2, 53, 43, 13),
(18, 9, 5, 0, 3, 47, 37, 14),
(16, 4, 3, 0, 1, 42, 35, 15),
(21, 5, 6, 2, 2, 47, 39, 16),
(14, 3, 2, 0, 1, 40, 30, 17),
(19, 7, 4, 1, 2, 49, 40, 18),
(27, 9, 10, 3, 2, 54, 46, 19),
(12, 2, 4, 1, 1, 38, 32, 20),
(24, 6, 8, 2, 1, 51, 41, 21),
(17, 4, 5, 0, 2, 43, 37, 22),
(29, 7, 5, 1, 3, 55, 44, 23),
(13, 2, 3, 0, 1, 39, 31, 24),
(18, 5, 9, 2, 2, 48, 40, 25),
(26, 8, 7, 1, 1, 53, 45, 26),
(20, 6, 4, 0, 2, 46, 38, 27),
(31, 9, 11, 3, 3, 56, 47, 28),
(15, 3, 2, 1, 1, 41, 33, 29),
(22, 5, 7, 1, 2, 49, 41, 30),
(18, 6, 3, 0, 2, 44, 36, 31),
(28, 7, 5, 2, 2, 52, 42, 32),
(19, 5, 4, 0, 1, 47, 39, 33),
(25, 8, 9, 2, 2, 53, 44, 34),
(17, 4, 3, 0, 1, 42, 34, 35),
(30, 10, 7, 1, 3, 55, 46, 36),
(21, 6, 5, 1, 1, 48, 40, 37),
(16, 3, 4, 0, 2, 45, 37, 38),
(27, 7, 8, 2, 2, 52, 43, 39),
(14, 4, 2, 0, 1, 39, 31, 40),
(25, 8, 5, 1, 2, 48, 36, 41),
(20, 6, 4, 0, 1, 45, 37, 42),
(18, 3, 3, 0, 1, 40, 32, 43),
(28, 9, 7, 1, 2, 53, 44, 44),
(16, 4, 3, 0, 1, 42, 35, 45),
(21, 7, 4, 1, 2, 49, 41, 46),
(14, 2, 2, 0, 1, 39, 30, 47),
(24, 7, 6, 2, 1, 51, 42, 48),
(17, 5, 5, 1, 2, 46, 38, 49),
(29, 8, 9, 3, 3, 55, 46, 50),
(13, 3, 3, 0, 1, 38, 31, 51),
(18, 5, 4, 0, 2, 44, 36, 52),
(26, 7, 8, 1, 1, 52, 44, 53),
(20, 6, 5, 0, 2, 46, 38, 54),
(31, 10, 10, 2, 3, 56, 47, 55),
(15, 4, 2, 1, 1, 41, 33, 56),
(22, 6, 6, 1, 2, 48, 40, 57),
(18, 6, 3, 0, 2, 44, 36, 58),
(28, 7, 5, 2, 2, 52, 42, 59),
(19, 5, 4, 0, 1, 47, 39, 60),
(25, 9, 8, 2, 2, 54, 45, 61),
(17, 4, 3, 0, 1, 42, 34, 62),
(30, 10, 8, 1, 3, 55, 46, 63),
(21, 6, 6, 1, 1, 48, 40, 64),
(16, 3, 4, 0, 2, 45, 37, 65),
(27, 7, 9, 2, 2, 52, 43, 66),
(14, 4, 2, 0, 1, 39, 31, 67),
(23, 9, 7, 1, 3, 49, 42, 68),
(20, 6, 5, 1, 2, 47, 39, 69),
(17, 3, 3, 0, 1, 41, 33, 70),
(28, 9, 6, 1, 2, 53, 44, 71),
(13, 2, 2, 0, 1, 38, 30, 72),
(19, 6, 4, 1, 2, 48, 40, 73),
(24, 8, 6, 2, 1, 51, 42, 74),
(15, 4, 3, 0, 1, 40, 32, 75),
(22, 6, 5, 0, 2, 47, 39, 76),
(18, 6, 3, 0, 2, 44, 36, 77),
(28, 7, 5, 2, 2, 52, 42, 78),
(21, 5, 5, 1, 1, 47, 39, 79),
(16, 3, 4, 0, 2, 45, 37, 80),
(27, 7, 8, 1, 1, 52, 44, 81),
(14, 4, 2, 0, 1, 39, 31, 82),
(23, 9, 7, 1, 3, 49, 42, 83),
(19, 5, 5, 0, 1, 46, 38, 84),
(25, 8, 8, 2, 2, 54, 45, 85),
(17, 4, 3, 0, 1, 42, 34, 86),
(30, 9, 8, 1, 3, 55, 46, 87),
(21, 6, 6, 1, 1, 48, 40, 88),
(16, 3, 4, 0, 2, 45, 37, 89),
(27, 7, 9, 2, 2, 52, 43, 90),
(14, 4, 2, 0, 1, 39, 31, 91),
(23, 9, 7, 1, 3, 49, 42, 92),
(19, 5, 5, 0, 1, 46, 38, 93),
(25, 8, 8, 2, 2, 54, 45, 94),
(17, 4, 3, 0, 1, 42, 34, 95),
(30, 9, 8, 1, 3, 55, 46, 96),
(16, 3, 3, 0, 1, 41, 33, 97),
(27, 7, 9, 2, 2, 52, 43, 98),
(14, 4, 2, 0, 1, 39, 31, 99),
(23, 9, 7, 1, 3, 49, 42, 100);


-- Insert sample data into Ticket
insert into NBA.Ticket ([Type], Price, Restantes, Game_ID, Team_ID) values
('Regular', 150, 1000, 61, 1),
('VIP', 500, 100, 61, 1),
('Regular', 150, 1000, 62, 3),
('VIP', 500, 100, 62, 3),
('Regular', 150, 1000, 63, 1),
('VIP', 500, 100, 63, 1),
('Regular', 150, 1000, 64, 2),
('VIP', 500, 100, 64, 2),
('Regular', 150, 1000, 65, 5),
('VIP', 500, 100, 65, 5),
('Regular', 150, 1000, 66, 6),
('VIP', 500, 100, 66, 6),
('Regular', 150, 1000, 67, 7),
('VIP', 500, 100, 67, 7),
('Regular', 150, 1000, 68, 8),
('VIP', 500, 100, 68, 8),
('Regular', 150, 1000, 69, 9),
('VIP', 500, 100, 69, 9),
('Regular', 150, 1000, 70, 10),
('VIP', 500, 100, 70, 10);