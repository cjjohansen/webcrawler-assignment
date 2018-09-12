In order to see the code in action
Please Follow these steps

1) Build the solution

2) To step into the Crawler implementation debug the specification tests
   a) in particular "when_calling_start"

3) In order to run the website first do the following
	a) in SQL management Studio or in Visual studio Server explorer, create a new SQL database called 'WebCrawler'
	b) Manually / Explicitly run the CanGenerateDatabaseSchema test. This will add tables to the database. 
	c) set the 'WebCrawler.Web.Mvc' project as start project.
	d) press F5 to run the project

Other points of interest:

4) The 'PageFound event Should be raised' doesn't pass. With more time it could be fixed.

5) if in doubt please contact Christian Johansen on skype:christian.j.johansen or MB : +45 30131514