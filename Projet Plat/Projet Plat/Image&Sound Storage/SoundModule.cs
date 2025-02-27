using System.Collections.Generic;
using Jypeli;

namespace Projet_Plat.Image_Sound_Storage;

/// <summary>
/// Stores references to all game sound effects.
/// </summary>
public static class SoundModule
{
    private static readonly Dictionary<string, (SoundEffect sound, double volume) > soundEffects = new();

    /// <summary>
    /// Loads all sounds and music into dictionaries.
    /// </summary>
    public static void LoadSounds()
    {
        // Load sound effects
        soundEffects["Jump"] = (Game.LoadSoundEffect("SoundEffects/OldRobloxJump.wav"), 0.8);
        soundEffects["Ouch"] = (Game.LoadSoundEffect("SoundEffects/Ouch.wav"), 1.0);
        soundEffects["HealingBox"] = (Game.LoadSoundEffect("SoundEffects/TF2_Medkit.wav"), 0.75);
    }

    /// <summary>
    /// Plays a sound effect at its set volume.
    /// </summary>
    
    public static void PlaySoundEffect(string name)
    {
        if (soundEffects.TryGetValue(name, out var soundData))
        {
            Sound s = soundData.sound.CreateSound();
            s.Volume = soundData.volume; // Apply volume
            s.Play();
        }
    }
}