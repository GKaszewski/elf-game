# Project Elf (Klabutong Remake)

A modern Unity recreation of the classic 2003 indie game **Klabutong** by Free Lunch Design. This project aims to replicate the "catch the falling presents" gameplay while strictly adhering to **Clean Architecture** principles and **Clean Code** practices.

The project is built to be modular, testable, and easily expandable (e.g., for future mobile ports) by decoupling business logic from the Unity Engine.

---

## 🏗 Architecture

This project follows a strict **Separation of Concerns** using a layered architecture. The codebase is divided into four distinct layers:


### 1. Core (Domain Layer)
* **Path:** `Scripts/Core`
* **Responsibility:** Pure C# business logic. **No Unity dependencies allowed.**
* **Components:**
    * **Systems:** `ScoreSystem`, `LivesSystem`, and `TimeAttackSystem` manage game rules independently.
    * **GameEvents:** A static Event Bus that acts as the central nervous system, allowing decoupled communication between systems and the UI.
    * **GameMode:** Enum defining Arcade vs. Time Attack modes.

### 2. Abstractions (Interfaces)
* **Path:** `Scripts/Abstractions`
* **Responsibility:** Defines contracts for external dependencies, allowing for easy swapping (e.g., swapping Mouse Input for Touch Input).
* **Key Interfaces:**
    * `IInputService`: Contracts for movement and interaction.
    * `IPersistenceService`: Contracts for saving/loading high scores.

### 3. Infrastructure (Data & Input)
* **Path:** `Scripts/Infrastructure`
* **Responsibility:** Concrete implementations of the Abstractions layer.
* **Components:**
    * `PlayerOneInput`: Implements Unity's **New Input System**.
    * `PlayerPrefsPersistence`: Handles saving data using Unity's `PlayerPrefs`.

### 4. Presentation (View Layer)
* **Path:** `Scripts/Presentation`
* **Responsibility:** Unity `MonoBehaviours` that handle rendering, physics, and UI. They simply "listen" to the Core layer and render the result.
* **Components:**
    * `GameBootstrap`: The entry point. It initializes the Systems and injects dependencies.
    * `ElfController`: Handles the visual movement and cane rotation logic.
    * `PresentSpawner`: Manages object pooling for performance.
    * `GameHud`: Updates the UI based on `GameEvents`.

---

## Features

* **Two Game Modes:**
    * **Arcade:** Classic gameplay with 3 lives.
    * **Time Attack:** Race against the clock with time penalties for missed presents.
* **Local Multiplayer:** Support for 2 players (Co-op).
* **Performance:** Uses an **Object Pool** (`PresentSpawner`) to recycle "Present" objects instead of instantiating/destroying them, ensuring smooth garbage collection performance.
* **Persistence:** Automatically saves and loads high scores based on the selected game mode.

---

## Controls & Mechanics

The core mechanic involves a **physics-based candy cane**:

* **Move Right:** The cane tilts clockwise (`\`), raising the left side to catch presents falling from the left.
* **Move Left:** The cane tilts counter-clockwise (`/`), raising the right side to catch presents falling from the right.
* **Stop:** The cane returns to a neutral horizontal position.

---

## Getting Started

### Prerequisites
* **Unity 6**

### Setup
1.  Open the project in Unity.
2.  Open the main scene.
3.  Ensure the `GameBootstrap` object has the following references assigned in the Inspector:
    * `Elf Prefab`
    * `Spawn P1` / `Spawn P2` (Transforms)
    * `Spawner` (Reference to the PresentSpawner object).
4.  Ensure the `GameHud` object has references to your TextMeshPro UI elements.
5.  Press **Play**.

---

## Configuration

You can tweak the game settings directly on the `GameBootstrap` GameObject:

* **Start Value:** Number of lives (Arcade) or starting seconds (Time Attack).
* **Two Player Mode:** Check this box to spawn a second elf.
* **Mode:** Switch between `Arcade` and `TimeAttack` via the dropdown.

---

## TODO / Known Issues

* **Player 2 Input:** The class `PlayerTwoInput` currently throws `NotImplementedException`. Logic needs to be added to map specific keys (e.g., Arrow Keys) to the `IInputService`.
* **Difficulty Scaling:** The spawner has a placeholder comment for difficulty scaling which needs to be implemented.
* **GFX&SFX**

---

*Remake created for the love of retro gaming.*
