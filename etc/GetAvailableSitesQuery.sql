

SELECT * FROM site
    WHERE site.campground_id = 1
    AND site.site_id NOT IN (SELECT site_id	
	FROM reservation			
	WHERE from_date > '2020-06-01' AND to_date < '2020-06-30')
	