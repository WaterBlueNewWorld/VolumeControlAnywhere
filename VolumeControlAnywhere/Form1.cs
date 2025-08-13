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
            GlobalHotkey.UnregisterHotKey(Handle, GlobalHotkey.HotkeyId);
        };
        GlobalHotkey.RegisterHotKey(Handle, GlobalHotkey.HotkeyId, GlobalHotkey.ModControl | GlobalHotkey.ModShift, (int)Keys.B);
    }
    
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == GlobalHotkey.WmHotkey && m.WParam.ToInt32() == GlobalHotkey.HotkeyId)
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
        MMDevice defaultDevice = enumDisp.GetDefaultAudioEndpoint(
            DataFlow.Render,
            Role.Multimedia
        );
        SessionCollection sessionsAudio = defaultDevice.AudioSessionManager.Sessions;

        for (int i = 0; i < sessionsAudio.Count; i++)
        {
            var session = sessionsAudio[i];
            if (session.State == AudioSessionState.AudioSessionStateActive)
            {
                VolumeSlider.AppVolumes.Add(new AppVolume(session.DisplayName, session.SimpleAudioVolume.Volume, session));
                _trackBarVolumen.AddRange(VolumeSlider.TrackBarUi() ?? throw new InvalidOperationException());
            }
        }
        _listaProgramas.Controls.AddRange(_trackBarVolumen.ToArray());
    }
}