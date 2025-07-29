using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;
using ProcessFilter = VolumeControlAnywhere.Utils.ProcessFilter;
using BotonPlay = VolumeControlAnywhere.UIElements.MainPage.Widgets.BotonPlay;
using BotonStop = VolumeControlAnywhere.UIElements.MainPage.Widgets.BotonStop;
using RenderingStreamListBox = VolumeControlAnywhere.UIElements.MainPage.Widgets.RenderingStreamListBox;

namespace VolumeControlAnywhere;

public partial class Form1 : Form
{
    private WaveOutEvent? _outputDevice;
    private AudioFileReader? _audioFile;
    
    private Timer? _timer;
    public Form1()
    {
        InitializeComponent();
        InicializarListaProgramas();
    }
    
    private void InicializarListaProgramas()
    {
        var flowPanel = new FlowLayoutPanel();
        flowPanel.FlowDirection = FlowDirection.LeftToRight;
        flowPanel.Margin = new Padding(10);

        var botonPlay = BotonPlay.BotonPlayUI(OnButtonPlayClick);
        flowPanel.Controls.Add(botonPlay);
        
        var botonStop = BotonStop.BotonStopUI(OnButtonStopClick);
        flowPanel.Controls.Add(botonStop);

        var listaStreamsAudio = RenderingStreamListBox.ListBoxUI();;
        
        Controls.Add(flowPanel);
        Controls.Add(listaStreamsAudio);
        FormClosing += OnButtonStopClick;
        
        _timer = new Timer();
        _timer.Interval = 1000;
        _timer.Tick += (s, e) => RefrescarListaProgramas();
        _timer.Start();
        
        RefrescarListaProgramas();
    }

    private void OnButtonPlayClick(object sender, EventArgs e)
    {
        if (_outputDevice == null)
        {
            _outputDevice = new WaveOutEvent();
            _outputDevice.PlaybackStopped += DetenerReproduccion;
        }

        if (_audioFile == null)
        {
            _audioFile = new AudioFileReader(@"C:\Users\alejandro\Desktop\thje real glitter xd.mp3");
            _outputDevice.Init(_audioFile);;
        }
        _outputDevice.Play();
    }

    private void OnButtonStopClick(object sender, EventArgs e)
    {
        _outputDevice?.Stop();
    }

    private void DetenerReproduccion(object sender, StoppedEventArgs e)
    {
        _outputDevice?.Stop();
        _outputDevice = null;
        _audioFile?.Dispose();
        _audioFile = null;
    }

    private void RefrescarListaProgramas()
    {
        RenderingStreamListBox.listaProgramas.Items.Clear();
        var enumDisp = new MMDeviceEnumerator();
        var dispositivoPredeterminado = enumDisp.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        var sesionesAudio = dispositivoPredeterminado.AudioSessionManager.Sessions;

        for (int i = 0; i < sesionesAudio.Count; i++) {
            var sesion = sesionesAudio[i];
            if (sesion.State == AudioSessionState.AudioSessionStateActive)
            {
                RenderingStreamListBox.listaProgramas.Items.Add($"{ProcessFilter.NombreProceso(sesion.GetSessionInstanceIdentifier)} - Volumen: {(sesion.SimpleAudioVolume.Volume * 100):F0}%");
            }
        }
    }
}