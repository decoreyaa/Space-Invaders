# Space Invaders (Horizontal)

An endless, horizontally-oriented take on Space Invaders, built in Unity 6 with C#.

Instead of enemies descending from the top while the player slides along the bottom, waves advance from the right edge of the screen and the player defends from the left, firing horizontally. Same core loop, rotated ninety degrees — which meant every piece of bounds logic, spawn math, and movement direction had to be derived from scratch rather than copied from a reference implementation.

There's no win condition. Waves keep coming, faster and shooting harder each time, until you run out of health. The only question is how far you get.

## About this project

This was built without following tutorials. The goal was to develop independent problem-solving skills in Unity and C# — understanding *why* each decision was made, not just producing working code. Every system here was designed, debugged, and refactored from first principles, using the Unity Scripting API docs and general programming concepts as reference rather than step-by-step guides.

## Features

**Gameplay**
- Full 2D player movement with screen-bound clamping
- Cooldown-gated shooting
- Three-hit health system
- Endless waves with difficulty that scales on an asymptotic curve — enemy speed climbs toward a ceiling and fire rate tightens toward a floor, so every wave is harder without ever becoming impossible
- Enemies spawn off-screen, drift in, and bounce independently off the screen edges and a forward boundary
- Randomized enemy fire timing, so a wave never shoots in lockstep
- Score that scales with wave depth, plus a persistent high score
- Pause menu with volume control

**Enemy spawning**
- Grid-based spawner with configurable rows, columns, and spacing
- Configurable enemy count — spawns *N* enemies into randomly selected grid slots
- Fisher-Yates shuffle for slot selection, so the formation layout varies every wave
- Per-wave difficulty applied to each enemy at spawn time

**Main menu**
- Start / Ships / Quit
- Ship selection panel with multiple player ship sprites
- Selection persists across sessions via `PlayerPrefs`
- Decorative background laser fire to keep the menu feeling alive

**Audio**
- Music and sound effects routed through a per-scene audio manager
- Button click feedback, laser fire, and a game-over sting
- Master volume slider, persisted between sessions

## Architecture

| Script | Responsibility |
|---|---|
| `PlayerMovement.cs` | Input, movement, bounds clamping, shooting |
| `PlayerHealth.cs` | Damage detection, triggering game over |
| `PlayerAppearance.cs` | Applies the ship sprite chosen in the menu |
| `Bullet.cs` | Player bullet movement, off-screen cleanup, collision |
| `EnemyBullet.cs` | Enemy bullet movement, off-screen cleanup, collision |
| `Enemy.cs` | Per-instance movement, bouncing, shooting, death, scoring |
| `EnemyGridSpawner.cs` | Builds each wave, applies difficulty scaling, exposes enemy count |
| `GameManager.cs` | Singleton state machine, wave progression, score, pause, restart |
| `ScreenBounds.cs` | Static utility computing camera-relative screen edges |
| `AudioManager.cs` | Singleton for SFX playback and volume persistence |
| `MainMenu.cs` | Menu navigation, ship selection persistence |
| `MenuBulletSpawner.cs` | Decorative background fire for the menu scene |
| `ButtonClickSound.cs` | Hooks click audio into any button it's attached to |

### Design decisions worth noting

**Independent enemy movement.** The formation started as a single parent object moved by one controller — the classic approach, where the whole grid reverses direction the moment any one enemy hits a wall. That was replaced with per-instance logic on `Enemy.cs`, so each enemy tracks and reacts to its own position. Enemies remain parented to the spawner for organization and counting, but nothing about their movement is shared.

**Enemies configured at spawn, not by lookup.** `enemyPrefab` is typed as `Enemy` rather than `GameObject`, so `Instantiate()` returns a reference the spawner can configure directly. Wave difficulty is written onto each enemy as it's created, keeping the scaling formula in one place instead of having every enemy poll a shared source.

**`ScreenBounds` as a static utility.** Screen edges are computed live from the camera's orthographic size, aspect ratio, and position rather than hardcoded into each script. Changing the camera size doesn't require re-tuning bound values scattered across several files.

**Enum-based state machine.** `GameManager` tracks exactly one of `Playing`, `Pause`, or `GameOver` rather than juggling separate boolean flags — there's no state where two contradictory flags are simultaneously true. Escape-key handling deliberately sits outside the `Playing` guard, since pausing leaves that state and a check inside it could never see the keypress that unpauses.

**Decoupled collision.** Bullets and enemies each detect the other by tag and destroy only themselves. Neither script holds a reference to the other or knows anything about the other's internals beyond a tag string.

**`ButtonClickSound` hooks itself up.** Rather than wiring click audio into every button's On Click list by hand, the script grabs the `Button` component on its own GameObject and registers a listener in code. Attach and forget.

## Controls

| Input | Action |
|---|---|
| `W` / `S` | Move up / down |
| `A` / `D` | Move left / right |
| `Space` | Fire |
| `Esc` | Pause / resume |

## Built with

- Unity 6 (6000.4.6f1), 2D / Universal Render Pipeline
- C#
- TextMeshPro
- Git

## Known limitations

- **Diagonal movement is faster than cardinal movement.** The input vector isn't normalized, so holding two keys yields roughly 1.41× speed. Left in deliberately.
- **No object pooling.** Bullets are instantiated and destroyed on demand. Fine at this scale, but the obvious next optimization.

## Possible next steps

- Object pooling for bullets
- Visual feedback for the currently selected ship in the menu
- Enemy variety — different types with different point values and behaviour

## Credits


	Space Shooter (Remastered, plus fonts and sounds) by Kenney Vleugels (www.kenney.nl)

			------------------------------

			        License (CC0)
	       http://creativecommons.org/publicdomain/zero/1.0/

	You may use these graphics in personal and commercial projects.
	Credit (Kenney or www.kenney.nl) would be nice but is not mandatory.

###############################################################################

Game music loop #9 by BloodPixelHero -- https://freesound.org/s/616049/ -- License: Attribution 4.0
express - [BeatsByZy ](https://www.youtube.com/@BEATS-BY-ZY)
