# SQL Cheatsheet — Basics

FROM → WHERE → GROUP BY → HAVING → SELECT → ORDER BY → LIMIT

- `SELECT` — which columns to return (`*` = all columns)
- `FROM` — which table to read from
- `WHERE` — filters rows BEFORE any grouping; can combine conditions with `AND` / `OR`
- `LIKE` — pattern matching for text: `WHERE name LIKE 'A%'` (starts with A), `'%son'` (ends with), `'%ann%'` (contains)
- `ORDER BY ... DESC` — sorts results; `DESC` = descending (largest first), `ASC` = ascending (default)
- `LIMIT` — caps the number of returned rows (top-N queries)
- `DISTINCT` — removes duplicate rows from the result

- `COUNT(*)` — number of rows; `SUM()`, `AVG()`, `MAX()`, `MIN()`. Without GROUP BY — the whole table is one group → one number
- `GROUP BY col` — split rows into buckets by col, aggregate per bucket. With GROUP BY, SELECT can contain only: the grouping column + aggregates
- `HAVING` — filter for GROUPS (WHERE for rows); only HAVING can use aggregates:

- `(INNER) JOIN` — only rows that have a match in both tables. Non-matching rows are discarded. No NULLs are produced.
- `(OUTER) LEFT JOIN` — all rows from the left table + matches from the right; where no match exists — NULLs in the right table's columns.
- `(OUTER) RIGHT JOIN` — mirror of LEFT: all rows from the right table + matches from the left; where no match exists — NULLs in the left table's columns.
- `(OUTER) FULL JOIN` — all rows from both tables; NULLs on whichever side is missing a pair (not supported in MySQL — emulated via LEFT JOIN + UNION + RIGHT JOIN)
- `CROSS JOIN` — Cartesian product: every row of the first table × every row of the second. No ON by design.
- `NATURAL JOIN` — joins automatically on all columns with identical names. No ON is written. Anti-pattern: adding a same-named column silently changes the result.