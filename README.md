# Space Invaders (Horizontal)

A horizontally-oriented take on Space Invaders, built in Unity 6 with C#.

Instead of enemies descending from the top while the player slides along the bottom, the formation advances from the right edge of the screen and the player defends from the left, firing horizontally. Same core loop, rotated ninety degrees — which meant every piece of bounds logic, spawn math, and movement direction had to be derived from scratch rather than copied from a reference implementation.

## About this project

This was built without following tutorials. The goal was to develop independent problem-solving skills in Unity and C# — understanding *why* each decision was made, not just producing working code. Every system here was designed, debugged, and refactored from first principles, using the Unity Scripting API docs and general programming concepts as reference rather than step-by-step guides.

## Features

**Gameplay**
- Full 2D player movement with screen-bound clamping
- Cooldown-gated shooting
- Three-hit health system
- Enemies that spawn off-screen, drift in, and bounce independently off the screen edges and a forward boundary
- Randomized enemy fire timing so the formation doesn't shoot in lockstep
- Win and loss states with a restart flow

**Enemy spawning**
- Grid-based spawner with configurable rows, columns, and spacing
- Configurable enemy count — spawns *N* enemies into randomly selected grid slots
- Fisher-Yates shuffle for slot selection, so the formation layout varies between runs

**Main menu**
- Start / Ships / Quit
- Ship selection panel with multiple player ship sprites
- Selection persists across sessions via `PlayerPrefs`
- Decorative background laser fire to keep the menu feeling alive

## Architecture

| Script | Responsibility |
|---|---|
| `PlayerMovement.cs` | Input, movement, bounds clamping, shooting |
| `PlayerHealth.cs` | Damage detection, triggering game over |
| `PlayerAppearance.cs` | Applies the ship sprite chosen in the menu |
| `Bullet.cs` | Player bullet movement, off-screen cleanup, collision |
| `EnemyBullet.cs` | Enemy bullet movement, off-screen cleanup, collision |
| `Enemy.cs` | Per-instance movement, bouncing, shooting, death |
| `EnemyGridSpawner.cs` | Builds the formation, exposes enemy count |
| `GameManager.cs` | Singleton state machine, win/lose handling, restart |
| `ScreenBounds.cs` | Static utility computing camera-relative screen edges |
| `MainMenu.cs` | Menu navigation, ship selection persistence |
| `MenuBulletSpawner.cs` | Decorative background fire for the menu scene |

### Design decisions worth noting

**Independent enemy movement.** The formation started as a single parent object moved by one controller — the classic approach, where the whole grid reverses direction the moment any one enemy hits a wall. That was replaced with per-instance logic on `Enemy.cs`, so each enemy tracks and reacts to its own position. Enemies remain parented to the spawner for organization and counting, but nothing about their movement is shared.

**`ScreenBounds` as a static utility.** Screen edges are computed live from the camera's orthographic size, aspect ratio, and position rather than hardcoded into each script. Changing the camera size doesn't require re-tuning bound values scattered across four files.

**Enum-based state machine.** `GameManager` tracks exactly one of `Playing`, `GameOver`, or `Won` rather than juggling separate boolean flags — there's no state where two contradictory flags are simultaneously true.

**Decoupled collision.** Bullets and enemies each detect the other by tag and destroy only themselves. Neither script holds a reference to the other or knows anything about the other's internals beyond a tag string.

## Controls

| Input | Action |
|---|---|
| `W` / `S` | Move up / down |
| `A` / `D` | Move left / right |
| `Space` | Fire |

## Built with

- Unity 6 (6000.4.6f1), 2D / Universal Render Pipeline
- C#
- TextMeshPro
- Git

## Known limitations

- **Diagonal movement is faster than cardinal movement.** The input vector isn't normalized, so holding two keys yields roughly 1.41× speed. Left in deliberately for now.
- **No object pooling.** Bullets are instantiated and destroyed on demand. Fine at this scale, but the obvious next optimization.
- **No score tracking.**

## Planned

- Multiple levels (`enemyCount` on the spawner is already the knob for this)
- Score and high-score persistence
- Visual feedback for the currently selected ship in the menu

## Credits

Ship, enemy, and projectile sprites:
###############################################################################

	Space Shooter (Remastered, plus fonts and sounds) by Kenney Vleugels (www.kenney.nl)

			------------------------------

			        License (CC0)
	       http://creativecommons.org/publicdomain/zero/1.0/

	You may use these graphics in personal and commercial projects.
	Credit (Kenney or www.kenney.nl) would be nice but is not mandatory.

###############################################################################
