using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using VolumeControlAnywhere.UIElements.MainPage.Widgets;
using VolumeControlAnywhere.Utils.Models;
using Timer = System.Windows.Forms.Timer;
using VolumeControlAnywhere.Utils;

namespace VolumeControlAnywhere;

public partial class Form1 : Form
{
    private FlowLayoutPanel? _listaProgramas;
    private List<Control> _trackBarVolumen;
    
    private Timer? _timer;
    public Form1()
    {
        _listaProgramas = new FlowLayoutPanel
        {
            Height = 600,
            Width = 800,
            Dock = DockStyle.Fill
        };
        _trackBarVolumen = new List<Control>();
        InitializeComponent();
        InicializarListaProgramas();
        FormClosing += (s, e) =>
        {
            // Unregister hotkey if needed
            GlobalHotkey.UnregisterHotKey(Handle, GlobalHotkey.HOTKEY_ID);
        };
        GlobalHotkey.RegisterHotKey(Handle, GlobalHotkey.HOTKEY_ID, GlobalHotkey.MOD_CONTROL | GlobalHotkey.MOD_SHIFT, (int)Keys.B);
    }
    
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == GlobalHotkey.WM_HOTKEY && m.WParam.ToInt32() == GlobalHotkey.HOTKEY_ID)
        {
            // Handle the hotkey press
            if (Visible)
            {
                Hide();
            }
            else
            {
                InitializeComponent();
                Show();
                BringToFront();
            }
        }
        base.WndProc(ref m);
    }
    
    private void InicializarListaProgramas()
    {
        Controls.Add(_listaProgramas);

        // _timer = new Timer();
        // _timer.Interval = 1000;
        // _timer.Tick += (s, e) => RefrescarListaProgramas();
        // _timer.Start();

        ListOpenApps();
    }

    private void ListOpenApps()
    {
        if (_listaProgramas == null) return;
        _listaProgramas.Controls.Clear();
        
        MMDeviceEnumerator enumDisp = new MMDeviceEnumerator();
        MMDevice dispositivoPredeterminado = enumDisp.GetDefaultAudioEndpoint(
            DataFlow.Render,
            Role.Multimedia
        );
        SessionCollection sesionesAudio = dispositivoPredeterminado.AudioSessionManager.Sessions;

        for (int i = 0; i < sesionesAudio.Count; i++)
        {
            var sesion = sesionesAudio[i];
            if (sesion.State == AudioSessionState.AudioSessionStateActive)
            {
                VolumeSlider.AppVolumes.Add(new AppVolume(sesion.DisplayName, sesion.SimpleAudioVolume.Volume, sesion));
                _trackBarVolumen.AddRange(VolumeSlider.TrackBarUi() ?? throw new InvalidOperationException());
            }
        }
        _listaProgramas.Controls.AddRange(_trackBarVolumen.ToArray());
    }
}