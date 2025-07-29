using FuncionExternaDelegate = VolumeControlAnywhere.Utils.Delegates.FuncionExternaDelegate;

namespace VolumeControlAnywhere.UIElements.MainPage.Widgets;

public class BotonPlay
{
    public static Button BotonPlayUI(FuncionExternaDelegate OnButtonPlayClick)
    {
        var botonPlay = new Button();
        botonPlay.Text = "Reproducir";
        botonPlay.Click += (s, e) => OnButtonPlayClick(s, e);
        botonPlay.Width = 200;
        
        return botonPlay;
    }
}