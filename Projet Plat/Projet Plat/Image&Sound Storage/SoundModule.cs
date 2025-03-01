using System.Collections.Generic;
using Jypeli;

namespace Projet_Plat.Image_Sound_Storage;

/// <summary>
/// Manages sound effects, including loading, playing, and setting volume.
/// </summary>
public static class SoundModule
{
    private static readonly Dictionary<string, (SoundEffect sound, double volume)> soundEffects = new();

    /// <summary>
    /// Loads all necessary sounds when the game starts.
    /// </summary>
    public static void LoadSounds()
    {
        LoadSound(SoundData.Jump, "SoundEffects/OldRobloxJump.wav", 0.8);
        LoadSound(SoundData.Ouch, "SoundEffects/Ouch.wav", 1.0);
        LoadSound(SoundData.HealingBox, "SoundEffects/TF2_Medkit.wav", 0.75);
    }

    /// <summary>
    /// Loads a sound effect and stores it in the dictionary with volume.
    /// </summary>
    /// <param name="name">The name of the sound effect</param>
    /// <param name="filePath">The file path of the sound</param>
    /// <param name="volume">The volume level (0.0 to 1.0)</param>
    public static void LoadSound(string name, string filePath, double volume)
    {
        SoundEffect effect = Game.LoadSoundEffect(filePath);
        soundEffects[name] = (effect, volume);
    }

    /// <summary>
    /// Plays a sound effect if it exists in the dictionary.
    /// </summary>
    /// <param name="name">The name of the sound effect</param>
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