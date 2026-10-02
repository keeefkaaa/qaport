# pom design — saucedemo

idea: test says WHAT, page class knows HOW. locators only in page classes.
assertions stay in tests.

# LoginPage

fields:
- Username - placeholder
- Password - placeholder
- LoginButton - role button "Login"
- Error - [data-test='error']

methods:
- Open()
- LoginAs(user, pass) -> InventoryPage
- GetErrorText()

# InventoryPage

fields:
- SortCombobox - role combobox
- AddToCartButtons - role button "Add to cart" (6 of them)
- Prices - .inventory_item_price
- CartButton - role button, name like "Cart..." (changes to "Cart, 1" after add)
- MenuButton - "Open Menu"
- LogoutLink - [data-test='logout-sidebar-link']

methods:
- SortBy(label)
- AddFirstItemToCart()
- OpenCart() -> CartPage
- Logout() -> LoginPage

# CartPage

check: item got there, name+price same as catalog, remove works

fields:
- CartItems - .cart_item
- ItemNames - [data-test='inventory-item-name']
- ItemPrices - [data-test='inventory-item-price']
- RemoveButtons - role button "Remove"
- ContinueShopping - [data-test='continue-shopping']
- Checkout - [data-test='checkout']

methods:
- GetItemsCount()
- GetFirstItemName() / GetFirstItemPrice()
- RemoveFirstItem()
- ContinueShopping() -> InventoryPage
- Checkout() -> CheckoutInfoPage

# checkout (3 screens, 3 classes)

# CheckoutInfoPage - the form

fields:
- FirstName / LastName / Zip - by placeholder
- Continue - [data-test='continue']
- Cancel - [data-test='cancel']
- Error - [data-test='error'] (same as login, nice)

methods:
- FillForm(first, last, zip)
- Continue() -> CheckoutOverviewPage
- Cancel() -> CartPage

negative tests: empty first name / empty zip -> error text

# CheckoutOverviewPage - summary

fields:
- ItemTotal - [data-test='subtotal-label']
- Tax - [data-test='tax-label']
- Total - [data-test='total-label']
- Finish - [data-test='finish']
- Cancel - [data-test='cancel']

methods:
- GetSubtotal() / GetTax() / GetTotal() -> decimal
- Finish() -> CheckoutCompletePage

good test: subtotal + tax == total

# CheckoutCompletePage

fields:
- ThankYouHeader - [data-test='complete-header']
- BackHome - [data-test='back-to-products']

methods:
- GetConfirmationText()
- BackToProducts() -> InventoryPage