using NAudio.CoreAudioApi;

namespace VolumeControlAnywhere.Utils.Models;

public class AppVolume
{
    public string Name { get; set; }
    public float CurrentVolume { get; set; }
    public AudioSessionControl SessionControl { get; set; }
    
    public int MinVolume { get; }
    public int MaxVolume { get; }
    
    public AppVolume(string name, float currentVolume, AudioSessionControl control)
    {
        Name = name;
        CurrentVolume = currentVolume;
        SessionControl = control;
        MinVolume = 0;
        MaxVolume = 100;
    }
}