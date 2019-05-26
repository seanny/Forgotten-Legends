using System;
using System.Collections.Generic;

// Base Class for all characters, including NPC's and the player character
[Serializable]
public class ActorStats
{
    // Actor Health.
    public int currentHealth;
    public int maxHealth;

    // Actor Level. If maxLevel is 0, then they have no level cap.
    public int currentLevel;
    public int maxLevel;

    // Actor Stats
    public int strength;
    public int perception;
    public int endurance;
    public int speech;
    public int intelligence;
    public int sneak;
    public int luck;

    // Actor factions
    public List<uint> factions;
}
