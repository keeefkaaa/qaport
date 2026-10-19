**Tests** (https://github.com/keeefkaaa/qaport/actions/workflows/tests.yml/badge.svg)

# QA Automation Portfolio
Good morning, and in case I don't see ya, good afternoon, good evening, and good night! I’m Iaroslav, an aspiring QA engineer based in Berlin. 
This repositoryserves as a practical project in test automation, featuring a suite of UI and API testsintegrated into a full CI pipeline. 
The project is structured just as it would be in a professional work environment: it employs the Page Object Model pattern,
includes meaningful commit messages (usually) and pull requests, and utilizes a pipeline that validates every push.

# Tech Stack
- **UI tests:** Playwright for .NET + NUnit (Page Object Model)
- **API tests (code):** Playwright
- **API tests (Postman):** chained collection with auth token, environment variables and test scripts
- **CI/CD:** GitHub Actions — full suite runs on every push, screenshots are uploaded automatically when a test fails
- **Also practiced:** SQL for testers (joins, aggregates, data-vs-UI checks), formal test design (equivalence partitioning, boundary values, pairwise)

# What's Covered
**UI tests** against saucedemo.com (https://www.saucedemo.com):
- Login: positive, locked-out user, wrong password
- Product sorting (A->Z, Z->A, price low→high) and catalog<->cart data consistency
- Cart: add, remove, badge updates
- Checkout: negative form validation (empty fields) and a full end-to-end purchase
**API tests** against restful-booker (https://restful-booker.herokuapp.com): auth token flow, create -> read -> update -> delete, including a 
delete-then-verif (404) check.

# Project Structure
qaport/
|-- tests/                        - the test suite.
|   |-- Pages/                    - Page Object Model: one class per page.
|   |-- LoginTests.cs             - login scenarios.
|   |-- InventoryTests.cs         - catalog: sorting, add-to-cart, logout.
|   |-- CartConsistencyTests.cs   - catalog data matches cart data
|   |-- CheckoutTests.cs          - checkout form validation
|   |-- PurchaseTest.cs           - flagship: full end-to-end purchase flow
|   |-- ApiTests.cs               - API tests via Playwright.
|   |-- TestConfig.cs             - single source of truth.
|   |-- TestHelpers.cs            - shared helpers.
|-- postman/                      - Postman collection (JSON).
|-- notes/                        - study artifacts: test design practice and cheatsheets
|-- .github/workflows/            - CI pipeline definition.

# How to Run
Prerequisites: .NET SDK 10+.
Run a single test: dotnet test --filter "Name=FullPurchase_CompletesSuccessfully"
Run with visible browser: HEADED=1 dotnet test

# What I Learned Building This
- Debugging a flaky test that was green locally but red in CI — turned out to be a race condition; fixed by waiting for the *visible effect* of an action 
and using retrying assertions
- Why role-based locators survive UI redesigns while CSS classes don't (the demo site changed its markup mid-project — my CSS locators broke, the ole-based 
ones didn't)
-
-
-
-