Feature: HomePageLoadingandSearching

  @LoadingHomePageandSearching
  Scenario: Pizza Selection Page Loads Successfully and Searches for Chili Paneer Pizza
    Given the user opens the browser
    When the user enters the URL
    Then the pizza selection page should be displayed
    When the user searches for "Chili Paneer" Pizza
    Then the search results should include "Chili Paneer"

   Scenario: Pizza Selection Page Loads Successfully and Searches for Chilli Paneer Pizza
    Given the user opens the browser
    When the user enters the URL
    Then the pizza selection page should be displayed
    When the user searches for "Chilli Paneer" Pizza
    Then the search results should not include "Chilli Paneer"
