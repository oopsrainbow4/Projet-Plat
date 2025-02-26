using System.Collections.Generic;
using Jypeli;

namespace TestMovement2.Image_Sound_Storage;

/// <summary>
/// Stores references to all game sound effects.
/// </summary>
public static class SoundModule
{
    private static readonly Dictionary<string, (SoundEffect sound, double volume) > soundEffects = new();
    private static readonly Dictionary<string, (SoundEffect sound, double volume, bool loop)> backgroundMusic = new();
    private static Sound currentMusic; // Keep track of the currently playing music

    /// <summary>
    /// Loads all sounds and music into dictionaries.
    /// </summary>
    public static void LoadSounds()
    {
        // Load sound effects
        soundEffects["Jump"] = (Game.LoadSoundEffect("SoundEffects/OldRobloxJump.wav"), 0.8);
        soundEffects["Ouch"] = (Game.LoadSoundEffect("SoundEffects/Ouch.wav"), 1.0);
        soundEffects["HealingBox"] = (Game.LoadSoundEffect("SoundEffects/TF2_Medkit.wav"), 0.75);

        /*
        // Load background music
        backgroundMusic["TheTixHasReturned"] = (Game.LoadSoundEffect("SoundEffects/CI_ChaosCanyon.wav"), 0.8, true);
        */
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
    
    /// <summary>
    /// Plays background music with volume and looping.
    /// </summary>
    public static void PlayBackgroundMusic(string name)
    {
        if (backgroundMusic.TryGetValue(name, out var musicData))
        {
            StopBackgroundMusic(); // Stop any currently playing music
            
            // Create a Sound object from SoundEffect
            currentMusic = musicData.sound.CreateSound();
            
            // Set volume (Only works for background music, NOT sound effects)
            currentMusic.Volume = musicData.volume;
            
            // Play and loop if needed
            currentMusic.Play();
            if (musicData.loop)
            {
                currentMusic.IsLooped = true;
            }
        }
    }

    /// <summary>
    /// Stops background music.
    /// </summary>
    public static void StopBackgroundMusic()
    {
        currentMusic?.Stop();
        currentMusic = null;
    }
}