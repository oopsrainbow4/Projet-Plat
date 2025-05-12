using System.Collections.Generic;
using Jypeli;

namespace Projet_Plat.Image_Sound_Storage;

/// <summary>
/// Manages sound effects, including loading, playing, and setting volume.
/// </summary>
public static class SoundModule
{
    private static readonly Dictionary<string, (SoundEffect sound, double volume)> soundEffects = new();
    private static readonly Dictionary<string, (string filePath, double volume)> backgroundTracks = new();
    
    /// <summary>
    /// Fade-out duration for background music in seconds.
    /// </summary>
    private static readonly double BackgroundMusicFadeDuration = 1.0;
    
    /// <summary>
    /// Loads all necessary sounds when the game starts.
    /// </summary>
    public static void LoadSounds()
    {
        // Load sound effects
        LoadSound(SoundData.Jump, "SoundEffects/OldRobloxJump.wav", 0.8);
        LoadSound(SoundData.Ouch, "SoundEffects/Ouch.wav", 1.0);
        LoadSound(SoundData.HealingBox, "SoundEffects/TF2_Medkit.wav", 0.75);
        LoadSound(SoundData.JumpPad, "SoundEffects/GravityCoil.wav", 1.0);
        LoadSound(SoundData.SpeedBoost, "SoundEffects/SonicSpinDash.wav", 0.8);
        LoadSound(SoundData.Checkpoint, "SoundEffects/RobloxBassSFX.wav", 1.0);
        
        // Load background music
        LoadBackgroundMusic(SoundData.BackgroundMusic,"Soundtracks/DrawCatMusic", 
            1.0);
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

    public static void LoadBackgroundMusic(string name, string filePath, double volume)
    {
        backgroundTracks[name] = (filePath, volume);
    }
    
    /// <summary>
    /// Plays background music from an external file.
    /// </summary>
    public static void PlayBackgroundMusic(string name)
    {
        if (backgroundTracks.TryGetValue(name, out var musicData))
        {
            Game.Instance.MediaPlayer.Play(musicData.filePath); // Without .wav
            Game.Instance.MediaPlayer.Volume = musicData.volume;
            Game.Instance.MediaPlayer.IsRepeating = true;
        }
    }

    /// <summary>
    /// Stops background music.
    /// </summary>
    public static void StopBackgroundMusic()
    {
        Game.Instance.MediaPlayer.Stop();
    }

    /// <summary>
    /// Stops background music with a fade-out.
    /// </summary>
    public static void StopBackgroundMusicWithFade()
    {
        double duration = BackgroundMusicFadeDuration;
        double startVolume = Game.Instance.MediaPlayer.Volume;
        Timer fadeTimer = new Timer { Interval = 0.05 };
        double elapsed = 0;

        fadeTimer.Timeout += () =>
        {
            elapsed += fadeTimer.Interval;
            double t = elapsed / duration;

            if (t >= 1.0)
            {
                Game.Instance.MediaPlayer.Stop();
                Game.Instance.MediaPlayer.Volume = startVolume; // Reset to original for next time
                fadeTimer.Stop();
            }
            else
            {
                Game.Instance.MediaPlayer.Volume = startVolume * (1.0 - t);
            }
        };
        fadeTimer.Start();
    }
}