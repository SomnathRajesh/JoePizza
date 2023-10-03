Feature: PlaceOrder

  @PlacingOrder
  Scenario: Placing an Order for single pizza from Cart/Checkout
    Given the user is on the "Checkout" page
    When the user clicks on "Place Order"
    Then the "Order Confirmation" page should be displayed
    And the user should see the "orderid" and "amount"
    And the user should see the following pizza details in the page:
      | Pizza Name     | Topping             | Size   | Quantity |
      | Achari Paneer  | Green chili pepper  | Medium | 2        |

  Scenario: Placing an Order for multiple pizzas from Cart/Checkout
    Given the user is on the "Checkout" page
    When the user clicks on "Place Order"
    Then the "Order Confirmation" page should be displayed
    And the user should see the "orderid" and "amount"
    And the user should see the following pizza details in the page:
      | Pizza Name   | Topping      | Size  | Quantity |
      | Curry Paneer | Capsicum     | Small | 4        |
      | Shahi Paneer | Chaat masala | Large | 3        |