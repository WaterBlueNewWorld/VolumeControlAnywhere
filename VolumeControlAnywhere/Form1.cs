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
        Load += Form1_Load;
        
        
        FormClosing += (s, e) =>
        {
            // Unregister hotkey if needed
            GlobalHotkey.UnregisterHotKey(Handle, GlobalHotkey.HotkeyId);
        };
        GlobalHotkey.RegisterHotKey(Handle, GlobalHotkey.HotkeyId, GlobalHotkey.ModControl | GlobalHotkey.ModShift, (int)Keys.B);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        InicializarListaProgramas();
        CalculateWindowPos();
        
        MMDeviceEnumerator enumDisp = new MMDeviceEnumerator();
        MMDevice defaultDevice = enumDisp.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        string defaultDeviceName = defaultDevice.FriendlyName;
        for (int i = 0; i < deviceSelector.Items.Count; i++)
        {
            if (deviceSelector.Items[i] is MMDevice dev && dev.FriendlyName == defaultDeviceName)
            {
                deviceSelector.SelectedIndex = i;
                break;
            }
        }
    }
    
    private void CalculateWindowPos()
    {
        Screen currentScreen = Screen.FromPoint(Cursor.Position);
        Rectangle screenBounds =  currentScreen.WorkingArea;
        Point mousePos = MousePosition;
        
        StartPosition = FormStartPosition.Manual;
        
        int mousePosX = mousePos.X;
        int mousePosY = mousePos.Y;
        
        if(mousePosX + ClientSize.Width > screenBounds.Right)
        {
            mousePosX = mousePos.X - ClientSize.Width;
        }
        if(mousePosY + ClientSize.Height > screenBounds.Bottom)
        {
            mousePosY = mousePos.Y - ClientSize.Height;
        }
        if(mousePosX < screenBounds.Left)
        {
            mousePosX = screenBounds.Left;
        }
        if(mousePosY < screenBounds.Top)
        {
            mousePosY = screenBounds.Top;
        }
        
        Location = new Point(mousePosX, mousePosY);
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
                CalculateWindowPos();
                Show();
                BringToFront();
            }
        }
        base.WndProc(ref m);
    }
    
    private void InicializarListaProgramas()
    {
        Controls.Add(_listaProgramas);
        
        //Get all the audio render devices
        MMDeviceEnumerator enumDisp = new MMDeviceEnumerator();
        MMDeviceCollection deviceCollection = enumDisp.EnumerateAudioEndPoints(DataFlow.Render, (DeviceState)Role.Multimedia);
        Console.WriteLine("HAY "+deviceCollection.Count+" DISPOSITIVOS DE SALIDA");
        
        foreach (MMDevice device in deviceCollection)
        {
            deviceSelector.Items.Add(device);
        }
        deviceSelector.DisplayMember = "FriendlyName";
        
        deviceSelector.SelectedIndexChanged += (s, e) =>
        {
            MMDevice selectedDevice = deviceSelector.SelectedItem as MMDevice;
            int selectedIndex = deviceSelector.SelectedIndex;
            Console.WriteLine($"Selected index: {selectedIndex}");
            Console.WriteLine(selectedDevice.FriendlyName);
            ListOpenApps(selectedDevice);
        };
    }

    private void ListOpenApps(MMDevice selectedDevice)
    {
        if (_listaProgramas == null) return;
        _trackBarVolumen.ForEach(c => c.Dispose());
        _trackBarVolumen.Clear();
        _listaProgramas.Controls.Clear();
        VolumeSlider.AppVolumes.Clear();
        
        SessionCollection sessionsAudio = selectedDevice.AudioSessionManager.Sessions;

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