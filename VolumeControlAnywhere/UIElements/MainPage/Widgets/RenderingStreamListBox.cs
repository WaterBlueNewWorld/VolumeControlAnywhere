namespace VolumeControlAnywhere.UIElements.MainPage.Widgets;

public class RenderingStreamListBox
{
    public static ListBox ListBoxUI()
    {
        listaProgramas = new ListBox();
        listaProgramas.Dock = DockStyle.Bottom;
        listaProgramas.Height = 150;
        listaProgramas.Location = new Point(0, 200);

        return listaProgramas;
    }

    public static ListBox listaProgramas { get; set; }
}