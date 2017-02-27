# Airline Flight Database
#### _Site to view flights_

#### By _**Alexandra Holcombe && Minh Phuong**_

## Description

This website will take a string and a word from a user, then count the number of times that word occurs inside of the string.

***

## Setup/Installation Requirements

#### Create Databases
* In `SQLCMD`:  
        `> CREATE DATABASE airline`  
        `> GO`  
        `> USE airline`  
        `> GO`  
        `> CREATE TABLE cities(id INT IDENTITY(1,1),name VARCHAR(255))`
        `> GO`  
        `> CREATE TABLE flights(id INT IDENTITY(1,1),number VARCHAR(255), departure_time DATETIME,  flight_status VARCHAR(255))`
        `> GO`  
        `> CREATE TABLE flights (id INT IDENTITY(1,1), flight_id INT, arrival_id INT, departure_id INT)`  
        `> GO`  

* Requires DNU, DNX, MSSQL, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

***

## Specifications

### City Class
================  

**The DeleteAll method for the City class will delete all rows from the cities table.**
* Example Input: none
* Example Output: nothing

**The GetAll method for the City class will return an empty list if there are no entries in the City table.**
* Example Input: N/A
* Example Output: empty list

**The Equals method for the City class will return true if the City in local memory matches the City pulled from the database.**
* Example Input:  
        > Local: "Seattle", id is 10  
        > Database: "Seattle", id is 10  
* Example Output: `true`

**The Save method for the City class will save new Cities to the database.**
* Example Input:  
\> New city: "Seattle"
* Example Output: no return value

**The Save method for the City class will assign an id to each new instance of the City class.**
* Example Input:  
\> New stylist: "Seattle", `local id: 0`  
* Example Output:  
\> "Jennifer", `database-assigned id`  

**The GetAll method for the City class will return all city entries in the database in the form of a list.**
* Example Input:  
        > "Seattle", id is 10  
        > "Portland", id is 11  
* Example Output: `{Seattle object}, {Portland object}`

**The Find method for the City class will return the City as defined in the database.**
* Example Input: "Seattle"
* Example Output: "Seattle", `database-assigned id`


### Flight class
================

**The DeleteAll method for the Flight class will delete all rows from the flights table.**
* Example Input: none
* Example Output: nothing

**The GetAll method for the Flight class will return an empty list if there are no entries in the Flight table.**
* Example Input: N/A, automatically loads on home page
* Example Output: empty list

**The Equals method for the Flight class will return true if the Flight in local memory matches the Flight pulled from the database.**
* Example Input:  
        > Local: "AX9087", id is 10  
        > Database: "AX9087", id is 10  
* Example Output: `true`

**The Save method for the Flight class will save new Flights to the database.**
* Example Input:  
\> New flight: "AX9087"
* Example Output: no return value

**The Save method for the Flight class will assign an id to each new instance of the Flight class.**
* Example Input:  
\> New stylist: "AX9087", `local id: 0`  
* Example Output:  
\> "AX9087",`database-assigned id`  

**The GetAll method for the Flight class will return all flight entries in the database in the form of a list.**
* Example Input:  
        > "AX9087", id is 10  
        > "BD695", id is 11  
* Example Output: `{AX9087 object}, {BD695 object}`

**The Find method for the Flight class will return the Flight as defined in the database.**
* Example Input: "AX9087",
* Example Output: "AX9087", `database-assigned id`

**The Update method for the Flight class will return the Flight with the new status.**
* Example Input: "AX9087", id is 10, status: On Time
* Example Output: "AX9087", id is 10, status: Delayed

**The Delete method for the Flight class will return a list of Flights without the deleted Flight.**
* Example Input: "AX9087"
* Example Output: "BD695, JH5689, AI984"

### City && Flight classes
===========================

**The GetFlight method for the City class will return the list of flights associated with that city.**
* Example Input: "Seattle"
* Example Output: Departures: "AX5839" Arrivals: "IM2043"

**The GetCity method for the Flight class will return the list of cities associated with that flight.**
* Example Input: "AX5839"
* Example Output: Departures: "Seattle"  Arrivals: "Portland"

**The Delete method for the City class will also remove all flights associate with that city.**
* Example Input: Delete "Seattle"
* Example Output: List of all flights excluding those connected to Seattle

### User Interface
===================  

**The user can add a new City using the "Add City" form.**
* Example Input: New City: "Seattle"
* Example Output: All Cities: "Seattle, Portland, New York"

**The user can add a new Flight using the "Add flight" form.**
* Example Input: New Flight: "AX5839" Departure City: "Seattle" Arrival City: "Chicago" Departure Time: 4:00 PM
* Example Output: All Flights: "AX5839", "BD3049", etc

**The user can click on any flight in the flights list to view the flight's details**
* Example Input: *click* "AX5839"
* Example Output: "AX5839" Departure City: "Seattle" Arrival City: "Chicago" Departure Time: 4:00 PM

**The user can click on any city to view a list of all flights from and to that city.**
* Example Input: *click* "Seattle"
* Example Output: Departures: "AX5839" Arrivals: "IM2043"

**The user can edit a flight's status on the flight's page.**
* Example Input:  
    \> *click* "AX5389"  
    \> New status: "On Time"  
* Example Output: "AX4389", "On Time"

**The user can delete a flight using a link on the flight's page which will lead to a confirmation page.**
* Example Input:  
    \> *click* "AX5389"  
    \> *delete clicky*  
    \> *confirmation clicky*
* Example Output: Return to All Flights

**The user can delete all flights on the page listing all flights, which will lead to a confirmation page.**
* Example Input: *click* Delete All Flights
* Example Output: No flights.

**The user can delete a city on the city's page and all flights associated with that city will be deleted.**
* Example Input: *click* Delete Portland
* Example Output: all flights to/from portland have been removed, return to All Flights

***

## Support and contact details

Please contact Allie Holcombe at alexandra.holcombe@gmail.com or Minh Phuong mphuong@kent.edu with any questions, concerns, or suggestions.

***

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor
* MSSQL & SSMS

***

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 **_Alexandra Holcombe && Minh Phuong_**
