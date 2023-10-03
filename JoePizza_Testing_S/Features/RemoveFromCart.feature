Feature: RemoveFromCart


@RemovingFromCart
Scenario: Removing a pizza from Cart/Checkout Page
	Given the user navigates to the "Checkout" page
	When the user clicks on the trash icon for "Achari Paneer" pizza
	Then the "Achari Paneer" pizza is removed from the cart
Scenario: Removing multiple pizzas from Cart/Checkout Page
	Given the user navigates to the "Checkout" page
	When the user clicks on the trash icon for "Curry Paneer" pizza
	Then the "Curry Paneer" pizza is removed from the cart
	When the user clicks on the trash icon for "Shahi Paneer" pizza
	Then the "Shahi Paneer" pizza is removed from the cart
