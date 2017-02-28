using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlineApp
{
  public class CityTest: IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, associated
      int result = City.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange,Act
      City firstCity = new City("Seattle");
      City secondCity = new City("Seattle");

      //Assert
      Assert.Equal(secondCity,firstCity);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      City testCity = new City("Seattle");

      //Act
      testCity.Save();
      List<City> actualResult = City.GetAll();
      List<City> expectedResult = new List<City>{testCity};

      //Assert
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Save_AssignedIdtoCity()
    {
      //Arrange
      City testCity = new City("Seattle");
      testCity.Save();
      City savedCity = City.GetAll()[0];

      //Act
      int savedId = savedCity.GetId();
      int testId = testCity.GetId();

      //Assert
      Assert.Equal(testId,savedId);
    }

    [Fact]
    public void Test_GetAll_ReturnsListofCities()
    {
      //Arrange
      City firstCity = new City("Seattle");
      City secondCity = new City("Portland");

      //Act
      firstCity.Save();
      secondCity.Save();

      //Assert
      List<City> actualResult = City.GetAll();
      List<City> expectedResult = new List<City>{secondCity, firstCity};

      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Test_Find_ReturnCityFromDatabase()
    {
      //Arrange
      City testCity = new City("Seattle");
      testCity.Save();

      //Act
      City foundCity = City.Find(testCity.GetId());

      //Assert
      Assert.Equal(testCity,foundCity);
    }

    public void Dispose()
    {
      City.DeleteAll();
    }

    // [Fact]
    // public void Test_GetFlights_ReturnsListOfFlightsInCity()
    // {
    //   //Arrange
    //   City testCity = new City("Seattle");
    //
    //   DateTime departureTime = new DateTime(2017, 3, 14, 9, 30, 0);
    //   string firstFlightStatus = "On Time";
    //   Flight firstFlight = new Flight("AX5390", departureTime, firstFlightStatus);
    // }
  }
}
