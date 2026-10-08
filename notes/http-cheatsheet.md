# HTTP Cheatsheet

# Methods
- **Method - What it does - Idempotent?**
- GET - read data, changes nothing - yes *cache-control, safe method*
- POST - create a new resource - NO every call creates again
- PUT - replace a resource completely - yes
- PATCH - update part of a resource - yes
- DELETE - delete a resource - yes

# Status codes
- 200 OK — success, here's your data
- 201 Created — POST succeeded, new resource created
- 204 No Content — success, nothing to return (typical DELETE answer)
- 301 Moved Permanently — resource lives at a new URL forever
- 400 Bad Request — request itself is broken (bad JSON, missing field)
- 401 Unauthorized — not logged in / no valid token
- 403 Forbidden — logged in, but no permission for this
- 404 Not Found — no such resource
- 409 Conflict — clashes with current state (e.g. email already taken)
- 500 Internal Server Error — server crashed on a valid request, backend bug

**Groups:** 2xx success, 3xx redirect, 4xx client's fault, 5xx server's fault.

# REST
- **resource** — the URL points to a thing (entity), not a page: /users/42 is user #42, /orders/501 is order #501. The URL is a noun.
- **stateless** — the server remembers nothing between requests. Every request carries its own proof of identity (token in the Authorization header). Logged in once = token, sent with every request.
- **idempotency** — repeating the same request has the same effect as doing it once. GET/PUT/DELETE are idempotent, POST is not: two POSTs "pay" = two payments. Classic QA check: double-click on a payment button must create exactly one order.