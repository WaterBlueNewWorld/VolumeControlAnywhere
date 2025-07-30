using NAudio.CoreAudioApi;
using VolumeControlAnywhere.Utils.Models;

namespace VolumeControlAnywhere.UIElements.MainPage.Widgets;

public class VolumeSlider
{
    public static List<AppVolume> AppVolumes { get; } = new();
    public static List<Control>? TrackBarVolumen { get; } = new();

    public static List<Control>? TrackBarUi()
    {
        foreach (var app in AppVolumes)
        {
            TrackBar trackBarVolumenBar = new TrackBar();
            trackBarVolumenBar.Maximum = app.MaxVolume;
            trackBarVolumenBar.Minimum = app.MinVolume;
            trackBarVolumenBar.TickFrequency = 1;
            trackBarVolumenBar.Value = (int) (app.CurrentVolume * 100);
            trackBarVolumenBar.Orientation = Orientation.Vertical;
            trackBarVolumenBar.Width = 300;
            trackBarVolumenBar.Height = 200;
            trackBarVolumenBar.ValueChanged += (s, e) => 
                ChangeVolume(s, e, app.SessionControl);
            trackBarVolumenBar.Name = "trackBarVolumen";

            TrackBarVolumen?.Add(trackBarVolumenBar);
        }

        return TrackBarVolumen;
    }
    
    private static void ChangeVolume(object sender, EventArgs e, AudioSessionControl sessionControl)
    {
        sessionControl.SimpleAudioVolume.Volume = ((TrackBar) sender).Value / 100f;
    }
}