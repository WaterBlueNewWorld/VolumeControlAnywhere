using FuncionExternaDelegate = VolumeControlAnywhere.Utils.Delegates.FuncionExternaDelegate;

namespace VolumeControlAnywhere.UIElements.MainPage.Widgets;

public class BotonStop
{
    public static Button BotonStopUI(FuncionExternaDelegate OnButtonStopClick)
    {
        var botonStop = new Button();
        botonStop.Text = "Detener";
        botonStop.Click += (s,e) => OnButtonStopClick(s, e); ;
        
        return botonStop;
    }
}