--Begin A transaction 
BEGIN Transaction

--Delete the databases
DELETE from reservation
DELETE from site
DELETE From campground
DELETE from park

--Add test data to do integration testing with

-- INSERT park
INSERT into park (name, location, establish_date, area, visitors, description)
VALUES('Zion National Park', 'Utah', '1919-11-19', 146957, 45000000, 'Zion National Park is an American national park located in southwestern Utah near the town of Springdale. A prominent feature of the 229-square-mile (590 km2) park is Zion Canyon, which is 15 miles (24 km) long and up to 2,640 ft (800 m) deep. The canyon walls are reddish and tan-colored Navajo Sandstone eroded by the North Fork of the Virgin River. The lowest point in the park is 3,666 ft (1,117 m) at Coalpits Wash and the highest peak is 8,726 ft (2,660 m) at Horse Ranch Mountain.');

Insert into park (name,location, establish_date, area, visitors, description)
	values ('Bryce Canyon National Park', 'Utah', '1928-2-25', 35835, 2679876, 'Bryce Canyon National Park (/braɪs/) is an American national park located in southwestern Utah. The major feature of the park is Bryce Canyon, which despite its name, is not a canyon, but a collection of giant natural amphitheaters along the eastern side of the Paunsaugunt Plateau. Bryce is distinctive due to geological structures called hoodoos, formed by frost weathering and stream erosion of the river and lake bed sedimentary rocks.');

INSERT park (name, location, establish_date, area, visitors, description)
	VALUES ('Great Smokey Mountains National Park', 'North Carolina', '1983-3-15', 522419, 11421123, 'Great Smoky Mountains National Park is an American national park in the southeastern United States, with parts in Tennessee and North Carolina. The park straddles the ridgeline of the Great Smoky Mountains, part of the Blue Ridge Mountains, which are a division of the larger Appalachian Mountain chain. The park contains some of the highest mountains in eastern North America, including Clingmans Dome, Mount Guyot, and Mount Le Conte.');

--Declare the park ids as variables to match the park to the campground
Declare @ZionId int
	set @ZionId = (Select park_id from park where park.name = 'Zion National Park')

Declare @BryceId int
	set @BryceId = (Select park_id from park where park.name = 'Bryce Canyon National Park')

Declare @SmokeyId int
	set @SmokeyId = (Select park_id from park where park.name = 'Great Smokey Mountains National Park')

--Insert Campgrounds
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@ZionId, 'First Campground', 4, 12, 35.00)
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@ZionId, 'Second Camground', 4, 12, 45.00)
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@BryceId, 'Third Campground', 4, 8, 35.00)
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@BryceId, 'Not Cool Campground', 3, 11, 50.00)
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@SmokeyId, 'Cool Cmapground', 2, 12, 35.00)
Insert campground (park_id, name, open_from_mm, open_to_mm, daily_fee)
	values (@SmokeyId, 'Last Campground', 5, 10, 35.00)

--Varibles to match the campgrounds to the sites
declare @campground1 int
	set @campground1 = (Select campground_id from campground where campground.name = 'First Campground')
declare @campground2 int
	set @campground2 = (Select campground_id from campground where campground.name = 'Second Campground')
declare @campground3 int
	set @campground3 = (Select campground_id from campground where campground.name = 'Third Campground')
declare @campground4 int
	set @campground4 = (Select campground_id from campground where campground.name = 'Not Cool Campground')
declare @campground5 int
	set @campground5 = (Select campground_id from campground where campground.name = 'Cool Campground')
declare @campground6 int
	set @campground6 = (Select campground_id from campground where campground.name = 'Last Campground')
--Insert Sites
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (1, 1, 1, 1, 1);
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (2, 2, 1, 0, 1);
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (3, 3, 1, 1, 0);
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (4, 4, 1, 0, 1);
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (5, 5, 1, 0, 1);
INSERT INTO site (site_id, campground_id, site_number, accessible, utilities) 
	VALUES (6, 6, 1, 1, 0);

--Insert Reservations
declare @site1 int 
	set @site1 = (Select site_number from site where site.site_id = '1')
declare @site2 int
	set @site2 = (Select site_number from site where site.site_id = '2')
declare @site3 int
	set @site3 = (Select site_number from site where site.site_id = '3')
declare @site4 int
	set @site4 = (Select site_number from site where site.site_id = '4')
declare @site5 int
	set @site5 = (Select site_number from site where site.site_id = '5')
declare @site6 int
	set @site6 = (Select site_number from site where site.site_id = '6')

--Select all to see if the delete worked
select * from park
select * from campground
select * from site
select * from reservation

--Rollback the transaction 
ROLLBACK transaction