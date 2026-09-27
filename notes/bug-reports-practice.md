## BUG-001: emulated gamepad
- **Environment:** (Hellish Escape/0.2/PC)
- **Steps to reproduce:** 1. Launch the game with emulated gamepad connected and active 2. Press any gamepad button in the main menue 3. Load into gameplay and try to move
- **Expected:** Game registers all gamepad input same as a physical controller.
- **Actual:** No input from the emulated gamepad is registered, no response in menue or gameplay.
- **Severity:** High - Needs to be fixed due to me having emulated gamepad as the only option for the proper playtest.
- **Priority:** Low - Playtest is not soon so it is not part of the current scope.

## BUG-002: gridbox spawn
- **Environment:** (Hellish Escape/0.5/PC)
- **Steps to reproduce:** 1. Reach the eating wall level 2. Die near the end of the level, close to the wall 3. Respawn
- **Expected:** To be spawned at the closest to the Target Point non occupied space
- **Actual:** Spawns player int the wall.
- **Severity:** Medium - Brakes the game but happens rarely due to most of the death on the wall level are located in the very beginning of it.
- **Priority:** High - gridbox spawn System used on multiple levels and bugs there also, so fixing that can fix a lot of gameplay issues at onece.

## BUG-003: credits menue
- **Environment:** (Hellish Escape/0.8/PC)
- **Steps to reproduce:** 1. Launch the game 2. Press the credits button in the main menue
- **Expected:** Open the credits menue.
- **Actual:** Does not open the credits menue, no response at all.
- **Severity:** Low - Mostly visual bug that does not affect the gameplay.
- **Priority:** High - Playtest is soon and we must have that fixed.

## BUG-004: settings menue buttons font
- **Environment:** (Hellish Escape/0.8/PC)
- **Steps to reproduce:** 1. Launch the game 2. Open the settings menue 3. Look at the buttons font
- **Expected:** Settings menue buttons use the standard UI font same as the rest of the game.
- **Actual:** Settings menue buttons use a slightly different font.
- **Severity:** Low - visual bug.
- **Priority:** Low - wouldn't be a problem even of the playtest, people rarely open them and even if they would they are not gonna notice slightly different font.

## BUG-005: lava level finish
- **Environment:** (Hellish Escape/0.9/PC)
- **Steps to reproduce:** 1. Reach the end of the lava level 2. Wait until lava rises to the floor level 3. Insert the keys at that exact moment
- **Expected:** View the outro and load hub level.
- **Actual:** Lava kills while outro is plaing and game softlockes.
- **Severity:** High - cannot progress further and bug is somewhat frequently caused by testers.
- **Priority:** High - it is an early part of the game and frequently caused bug that brakes the game. Playtest also soon so this should be fixed.
