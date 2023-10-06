Feature: CustomizeAndAddPizzaToCart

  @CustomizingAndAddingPizzaToCart
  Scenario: Customizing and Adding Pizza to Cart
    Given the user navigates to the pizza selection page
    When the user clicks the customize button for the pizza named "Achari Paneer"
    Then the user should see the "Customize your Pizza" page with pizza details
    When the user selects a topping named "Green chili pepper"
    And the user selects a pizza size named "Medium"
    And the user specifies the quantity as "2"
    And the user clicks "Add To Cart"
    Then the "Achari Paneer" pizza with topping "Green chili pepper" size "Medium" and quantity "2" should be added to the cart

  Scenario: Customizing and Adding Pizzas to Cart
    Given the user navigates to the pizza selection page
    When the user clicks the customize button for the pizza named "Curry Paneer"
    Then the user should see the "Customize your Pizza" page with pizza details
    When the user selects a topping named "Capsicum"
    And the user selects a pizza size named "Small"
    And the user specifies the quantity as "4"
    And the user clicks "Add To Cart"
    Then the "Curry Paneer" pizza with topping "Capsicum" size "Small" and quantity "4" should be added to the cart

    When the user clicks the customize button for another pizza named "Shahi Paneer"
    Then the user should see the "Customize your Pizza" page with pizza details
    When the user selects a topping named "Chaat masala"
    And the user selects a pizza size named "Large"
    And the user specifies the quantity as "3"
    And the user clicks "Add To Cart"
    Then the "Shahi Paneer" pizza with topping "Chaat masala" size "Large" and quantity "3" should be added to the cart
    
    
