Feature: Login Functionality

    @Login
    Scenario: Successful Login
        Given The user is on the login page
        When user enters the email "testingreg@example.com" and password "Password@123"
        And user clicks the "Log in" button
        Then user should be redirected to the pizza home page
