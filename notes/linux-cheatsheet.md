# Linux / Shell Cheatsheet

Pipeline reads left → right: **producer | filter | filter**

## Navigation & files
- **pwd** — print working directory
- **ls -la** — list all files; `-a` includes hidden (`.env`, `.git`), `-l` long format
- **cd path** — change dir; `cd ..` up, `cd ~` home, `cd -` previous
- **mkdir -p a/b/c** — create nested dirs (`-p` = parents)
- **touch file** — create empty file / update timestamp
- **cp src dst** — copy; `cp -r dir dir2` recursive
- **mv src dst** — move OR rename (same command)
- **rm file** — delete; `-f` no error if missing, `-r` recursive — no trash, no undo
- **cat file** — print whole file
- **less file** — scroll page by page; `q` to quit
- **head -n 20 file** — first 20 lines (default 10)
- **tail -n 20 file** — last 20 lines
- **tail -f file** — follow live (Ctrl+C to exit) — the QA debugging mode

## Permissions (reading `ls -la`)
- **drwxr-xr-x** — type + 3 permission triples
  - `d` = directory (`-` file, `l` symlink)
  - `rwx` owner, `r-x` group, `r-x` others
- **r=4, w=2, x=1** → `755` = `rwxr-xr-x`, `644` = `rw-r--r--`
- **x on a dir** = permission to enter it (`cd`)
- **chmod 755 file** — set mode; `chmod +x script.sh` — make executable
- `777` = everyone everything — anti-pattern, never in prod

## Streams & redirection
- **0 stdin, 1 stdout, 2 stderr**
- **`>`** overwrite, **`>>`** append, **`2>`** stderr, **`2>&1`** merge both, **`/dev/null`** black hole
- **A | B** — pipe: stdout of A → stdin of B
- **A ; B** — B runs always
- **A && B** — B runs only if A succeeded (exit 0)
- **A || B** — B runs only if A failed (exit ≠ 0)
- **$?** — exit code of the last command (0 = success) — what CI checks

## grep — the log block
- **grep ERROR app.log** — lines containing the pattern
- **grep -c** — count matching lines (not occurrences)
- **grep -n** — with line numbers
- **grep -i** — ignore case
- **grep -w 1050** — whole word (won't match 10500)
- **grep -v** — invert: lines NOT matching
- **grep -A/-B/-C 3** — 3 lines after / before / around a match (context for stack traces)
- **grep -r ERROR logs/** — recursive through a directory
- no match → empty output + **exit code 1** (that's how scripts detect "nothing found")

## Processes
- **ps aux** — snapshot of all processes (user, PID, CPU, MEM, command)
- **pgrep name** — PID by name, clean (no grep noise)
- **ps aux | grep name** — finds it, but also lists the grep itself; use `[n]ame` or `pgrep`
- **kill PID** — SIGTERM, graceful (process can clean up)
- **kill -9 PID** — SIGKILL, forced by kernel (last resort, no cleanup)
- **pkill -f "python.*test"** — kill by full command line
- **lsof -i :8080** — who holds the port

## Environment variables
- **env** — list all variables
- **export VAR=val** — visible to current shell and child processes
- **VAR=val command** — only for that one command (`HEADED=1 dotnet test`) — isolated, does not leak
- **echo $VAR** — read value
- **which python3** — where the executable is found (searches `$PATH`)
- **export PATH="$PATH:/new/dir"** — add a directory to the search path

## Remote & containers
- **ssh user@host** — remote shell; same file/grep/tail commands after
- **docker ps** — running containers; `-a` includes stopped (same `-a` spirit as `ls`)
- **docker logs -f <id>** — live logs (exactly like `tail -f`)
- **docker exec -it <id> bash** — shell inside a running container
- **docker run -d -p 8080:80 nginx** — start in background, publish port

## Top 12 for the interview
1. `grep ERROR app.log`
2. `grep -c ERROR app.log`
3. `grep -n ERROR app.log`
4. `grep -i error app.log`
5. `tail app.log`
6. `tail -f app.log`
7. `tail app.log | grep ERROR`
8. `ls -la` + read `drwxr-xr-x` → `755`
9. `ps aux | grep <name>`
10. `kill <PID>` (and `kill -9`)
11. `export VAR=val` and `VAR=val command`
12. `docker ps / logs <id> / exec -it <id> bash`

## Gotchas
- **`tail app.log | grep ERROR`** ≠ **`grep ERROR app.log | tail`** — order matters: last 10 lines filtered vs last 10 matches overall
- **`grep 1050`** matches `10500` too — use `-w` or `request_id=1050\b`
- **`rm ... 2>/dev/null`** hides the error but exit code is still 1 — `rm -f` prevents it properly
- **`HEADED=1 echo $HEADED`** prints empty — shell expands `$HEADED` before the assignment takes effect; use `HEADED=1 bash -c 'echo $HEADED'`
- **2 GB file** — never `cat`; use `tail`, `less`, `grep -n` to jump to a line