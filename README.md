
---

# 2048 Physics

## Concept

This project demonstrates Unity's **physics engine** through a creative combination of **2048** and **Tetris** mechanics, with added physics-based interactions for unique and dynamic gameplay.

### Inspirations:
- **2048**: Combining numbered tiles to reach a target number.
- **Tetris**: Strategically placing and fitting shapes to maximize space.

### Twist:
Incorporating **real-time physics** to add strategic depth, explosions, and chaos, creating a fresh take on these classic games.

---

## Gameplay

The game is **2D** with a **minimalistic design** to emphasize gameplay mechanics.

- **Objective**: Place figures inside a jar without letting them fall out. Combine figures with the same number to achieve the highest number possible.
- **Physics Mechanics**:
    - Figures have mass, size, and weight that grow with their number.
    - Explosions occur during merges, dynamically pushing nearby figures with force based on weight.
    - The jar temporarily closes during explosions to contain the chaos.
- **Levels**:
    - Each level features different jar sizes, figure ranges, and goals, providing a gradual learning curve:
        1. **Level 1**: Large jar, figures numbered up to 64, goal: **256**.
        2. **Level 2**: Medium jar, figures numbered up to 16, goal: **512**.
        3. **Level 3**: Small jar, figures numbered up to 4, goal: **2048**.
- **Controls**:
    - **A/D**: Move figure left/right.
    - **Q/E**: Rotate figure.
    - **Space**: Drop figure.

---

## Features

- **Unity Physics**:
    - Real-time collision handling.
    - Explosion forces based on figure weight and radius.
    - Physics-driven figure stacking and balancing.
- **Dynamic Level Progression**:
    - Automatic transitions to higher levels upon achieving the goal.
    - Reset to Level 1 if figures are lost.
- **Modular Design**:
    - Figures stored in `Resources/Figures`. Adding figures requires no code changes.
    - Levels are configured using scripts, enabling designers to tweak settings without coding.
- **Strategic Gameplay**:
    - Players must plan figure placement to manage explosions and prevent overflow.

---

## Physics Mechanics: **Explosion**

One of the core mechanics is the **explosion** that occurs when two figures with the same number collide and merge.

### How it Works:
1. When two figures merge, an explosion is triggered at the point of collision.
2. The explosion applies a radial force to all nearby figures, with:
    - **Force proportional to the merged figure's weight.**
    - **Radius defined by the merged figure's explosion radius setting.**
3. The explosion pushes nearby figures outward, creating dynamic interactions that require strategic placement to prevent overflow.

### Implementation:
The explosion mechanic is implemented in the `Figure` script. Here's the link to the exact code handling explosions:  
[Explosion Code in Figure Script](https://github.com/Nikita-Barak/2048/blob/ea15af8a5520ff3a845e0ecd05a48f328550dd46/Assets/Scripts/Figure.cs#L233)

---

## Challenges and Solutions

1. **Physics Stability**:
    - Fine-tuned figure weights and explosion forces to prevent erratic behavior.
2. **Level Transition**:
    - Modular scripting ensures seamless progression across levels.
3. **Dynamic Interactions**:
    - Balanced the explosion mechanic to enhance gameplay without overwhelming the player.

---

## How to Play

1. Play the game on itch.io: [Link](https://shutafimpro.itch.io/2048)
2. Use controls to move, rotate, and drop figures strategically.
3. Reach the target number for each level without losing figures.

---

## Code References

- **Level Control Script**:  
  [View on GitHub](https://github.com/Nikita-Barak/2048/blob/main/Assets/Scripts/LevelControl.cs)
- **Figure Script (Explosion Mechanic)**:  
  [View on GitHub](https://github.com/Nikita-Barak/2048/blob/ea15af8a5520ff3a845e0ecd05a48f328550dd46/Assets/Scripts/Figure.cs#L233)

---
