namespace VolumeControlAnywhere.UIElements.MainPage.Widgets;

public class VolumeSlider
{
    private static TrackBar trackBarVolumen;

    public static TrackBar TrackBarUI(int maxValue, int minValue, int volumeValue)
    {
        trackBarVolumen = new TrackBar();
        trackBarVolumen.Maximum = maxValue;
        trackBarVolumen.Minimum = minValue;
        trackBarVolumen.TickFrequency = 1;
        trackBarVolumen.Value = volumeValue;

        return trackBarVolumen;
    }
}