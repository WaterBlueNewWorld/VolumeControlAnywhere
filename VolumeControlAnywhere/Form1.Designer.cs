namespace VolumeControlAnywhere;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        var currentScreen = Screen.FromPoint(Cursor.Position);
        var screenBounds = currentScreen.WorkingArea;
        var mousePosition = Cursor.Position;
        var formWidth = 800;
        var formHeight = 250;

        int x = mousePosition.X;
        int y = mousePosition.Y;

        if(x + formWidth > screenBounds.Right)
        {
            x = mousePosition.X - formWidth;
        }
        if(y + formHeight > screenBounds.Bottom)
        {
            y = mousePosition.Y - formHeight;
        }
        if(x < screenBounds.Left)
        {
            x = screenBounds.Left;
        }
        if(y < screenBounds.Top)
        {
            y = screenBounds.Top;
        }

        StartPosition = FormStartPosition.Manual;
        SetDesktopLocation(x, y);
        components = new System.ComponentModel.Container();
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(formWidth, formHeight);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "Test volumen";

    }

    #endregion
}