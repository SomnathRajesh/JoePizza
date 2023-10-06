Feature: User Registration

    @Register
    Scenario: Successful User Registration
        Given user is on the registration page
        When user enters email "test1@example.com" and password "Password@123"
        And user confirms password "Password@123"
        And user clicks on the "Register" button
        Then user should be redirected to the pizza page
