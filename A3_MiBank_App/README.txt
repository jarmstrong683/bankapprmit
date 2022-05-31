WDT SP2 2020 RMIT
ASSIGNMENT 2
TEAM:

ANTHONY TARANTO - S3694289
JASON ARMSTRONG - S3163558

github link:
https://github.com/rmit-oua-sp2-wdt2020/A3-mibank-s3163558_s3694289




1. MANUAL - HOW TO USE MiBank App


- pull the project 'master' from github and open csproj. in Visual Studio

- the project comes already with a migration 'initial'

- the database connection string used in app.settings is :

"Server=wdt2020.australiasoutheast.cloudapp.azure.com;Database=s3694289;uid=s3694289;pwd=abc123;MultipleActiveResultSets=true"

database should already be created and seeded
- if the database is empty but present the app should seed the database on launch using data in the Data.SeedData.cs class
- if the database is completely absent (not created ) the app will crash when user tries to log in
- in that case the database may be updated :
from the terminal on mac ->  dotnet ef database update
or directly from the windows version of Visual Studio

Login:
- the loginID is 8 characters long only
- login fails and exits after 3 attempts (wrong password/loginID,
    or 3 times using wrong length LoginID
-
Seeded Logins are:
12345678 (Matthew Bolger) abc123
38074569 (Rodney Cocker) ilovermit2020
17963428 (Shekhar Kalra) youWill_n0tGuess-This!

Navbar:

-MiBank Icon - click to return to Home page
-Statements
-Scheduled BillPays
-Pay Bills
-ATM
-PERSON ICON - click to Edit Customer Details and PAssword
-Logout

Features:

Accounts Page:

Show the Accounts held by the Customer
From here buttons are provided to link to:
'Account Statement' page and
'Scheduled BillPays page'

ATM:
- link provided at the NavBar
- Single page format with drop down selectors for:
Transaction Type - Deposit Withdraw Transfer
Acccount/Destination
Amount


Pay Bills:
- allows delayed scheduled payments
Minutely, quarterly, Annually or Once Off
-BackgroundService checks Billpays every minutes
- due Bills paid automatically on launch or during app running.
- can modify bills
- can't delete bill yet


Change Customer Personal DEtails and Password
- click the PERSON icon at the top right navbar