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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        deviceSelector = new System.Windows.Forms.ComboBox();
        SuspendLayout();
        // 
        // deviceSelector
        // 
        deviceSelector.FormattingEnabled = true;
        deviceSelector.Location = new System.Drawing.Point(12, 250);
        deviceSelector.Name = "deviceSelector";
        deviceSelector.Size = new System.Drawing.Size(219, 23);
        deviceSelector.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.ControlDarkDark;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        ClientSize = new System.Drawing.Size(848, 285);
        Controls.Add(deviceSelector);
        DoubleBuffered = true;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Location = new System.Drawing.Point(500, 300);
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        ResumeLayout(false);
    }

    private System.Windows.Forms.ComboBox deviceSelector;

    #endregion
}