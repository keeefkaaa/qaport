# SQL Cheatsheet — Basics

- `SELECT` — which columns to return (`*` = all columns)
- `FROM` — which table to read from
- `WHERE` — filters rows BEFORE any grouping; can combine conditions with `AND` / `OR`
- `LIKE` — pattern matching for text: `WHERE name LIKE 'A%'` (starts with A), `'%son'` (ends with), `'%ann%'` (contains)
- `ORDER BY ... DESC` — sorts results; `DESC` = descending (largest first), `ASC` = ascending (default)
- `LIMIT` — caps the number of returned rows (top-N queries)
- `DISTINCT` — removes duplicate rows from the result