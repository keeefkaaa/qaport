# Tables: users(id, email, country), orders(id, user_id, total, status, created_at)
**Idea: UI can lie and the db can't. So after each UI action we check the database directly.**

# Order creation
UI action: user places an order for n amount

```sql
SELECT status, total
FROM orders
WHERE user_id = 42
ORDER BY created_at DESC
LIMIT 1;
```
Expected: status = 'new', total = n

# Order cancellation
UI action: user cancels order 228 on the "My orders" page

```sql
SELECT status
FROM orders
WHERE id = 228;
```
Expected: status = 'cancelled', If it still says 'new' or 'shipped' the cancel button is lying which means it is a bug.

# Email change (updated + no duplicates)
UI action: user #42 changes email from old@mail.com to new@mail.com

```sql
SELECT email
FROM users
WHERE id = 42;

SELECT email, COUNT(*) AS cnt
FROM users
GROUP BY email
HAVING COUNT(*) > 1;
```
Expected: first returns new@mail.com. second returns zero rows — if the new email shows up with cnt = 2, the app let two users share one email, bug (missing unique check).

# User deletion (what happens to their orders?)
UI action: admin deletes user #42

```sql
SELECT orders.id, orders.total, orders.status
FROM orders
LEFT JOIN users ON orders.user_id = users.id
WHERE users.id IS NULL;
```
Expected: zero rows — orders should be deleted or anonymized together with the user. If rows come back, these are orphan orders pointing at a user that no longer exists. The "My orders" page and revenue reports will silently break or miscount.

# "My orders" total matches the db
UI action: user #42 opens "My orders", the page shows "Total spent: €259.50"

```sql
SELECT SUM(total) AS total_spent
FROM orders
WHERE user_id = 42 AND status != 'cancelled';
```
Expected: total_spent = 259.50 — same number as on the screen.