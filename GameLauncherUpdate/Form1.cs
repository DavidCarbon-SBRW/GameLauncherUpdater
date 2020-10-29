// Decompiled with JetBrains decompiler
// Type: GameLauncherUpdate.Form1
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using GameLauncherUpdate.Properties;
using SimpleJSON;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace GameLauncherUpdate
{
  public class Form1 : Form
  {
    private string tempNameZip = Path.GetTempFileName();
    private int op = 100;
    private string version;
    private IContainer components;
    private ProgressBar downloadProgress;
    private Label information;

    public Form1()
    {
      this.InitializeComponent();
    }

    public void error(string error)
    {
      this.information.Text = error.ToString();
      Delay.WaitSeconds(2);
      Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
    }

    public void success(string success)
    {
      this.information.Text = success.ToString();
    }

    public void update()
    {
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      if (commandLineArgs.Length == 2)
        Process.GetProcessById(Convert.ToInt32(commandLineArgs[1])).Kill();
      this.version = !System.IO.File.Exists("GameLauncher.exe") ? "0.0.0.0" : FileVersionInfo.GetVersionInfo("GameLauncher.exe").ProductVersion;
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      WebClient webClient1 = new WebClient();
      Uri address = new Uri("http://launcher.worldunited.gg/update.php?version=" + this.version);
      webClient1.CancelAsync();
      webClient1.DownloadStringAsync(address);
      webClient1.DownloadStringCompleted += (DownloadStringCompletedEventHandler) ((sender2, e2) =>
      {
        try
        {
          JSONNode json = JSON.Parse(e2.Result);
          if (json["payload"]["update_exists"] != (object) false)
          {
            new Thread((ThreadStart) (() =>
            {
              WebClient webClient2 = new WebClient();
              webClient2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
              webClient2.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
              webClient2.DownloadFileAsync(new Uri((string) json["payload"][nameof (update)]["download_url"]), this.tempNameZip);
            })).Start();
          }
          else
          {
            Process.Start("GameLauncher.exe");
            this.error("Starting GameLauncher.exe");
          }
        }
        catch (Exception ex)
        {
          this.error("Failed to update. " + ex.Message);
        }
      });
    }

    private string FormatFileSize(long byteCount)
    {
      double[] numArray = new double[4]
      {
        1073741824.0,
        1048576.0,
        1024.0,
        0.0
      };
      string[] strArray = new string[4]
      {
        "GB",
        "MB",
        "KB",
        "Bytes"
      };
      for (int index = 0; index < numArray.Length; ++index)
      {
        if ((double) byteCount >= numArray[index])
          return string.Format("{0:0.00}", (object) ((double) byteCount / numArray[index])) + strArray[index];
      }
      return "0 Bytes";
    }

    private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      this.BeginInvoke((Delegate) (() =>
      {
        long num1 = e.BytesReceived;
        double num2 = double.Parse(num1.ToString());
        num1 = e.TotalBytesToReceive;
        double num3 = double.Parse(num1.ToString());
        double d = num2 / num3 * 100.0;
        this.information.Text = "Downloaded " + this.FormatFileSize(e.BytesReceived) + " of " + this.FormatFileSize(e.TotalBytesToReceive);
        this.downloadProgress.Style = ProgressBarStyle.Blocks;
        this.downloadProgress.Value = int.Parse(Math.Truncate(d).ToString());
      }));
    }

    private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
      this.BeginInvoke((Delegate) (() =>
      {
        this.downloadProgress.Style = ProgressBarStyle.Marquee;
        string path1 = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        using (ZipArchive zipArchive = ZipFile.OpenRead(this.tempNameZip))
        {
          int count = zipArchive.Entries.Count;
          int num = 1;
          this.downloadProgress.Style = ProgressBarStyle.Blocks;
          foreach (ZipArchiveEntry entry in zipArchive.Entries)
          {
            string fullName = entry.FullName;
            if (fullName.Substring(fullName.Length - 1) == "/")
            {
              string path = fullName.Remove(fullName.Length - 1);
              if (Directory.Exists(path))
                Directory.Delete(path, true);
              Directory.CreateDirectory(path);
            }
            else if (fullName != "GameLauncherUpdate.exe")
            {
              if (System.IO.File.Exists(fullName))
                System.IO.File.Delete(fullName);
              this.information.Text = "Extracting: " + fullName;
              try
              {
                entry.ExtractToFile(Path.Combine(path1, fullName));
              }
              catch
              {
              }
              Delay.WaitMSeconds(200);
            }
            this.downloadProgress.Value = (int) (100L * (long) num / (long) count);
            ++num;
          }
        }
        Process.Start("GameLauncher.exe");
        this.error("Update completed. Starting GameLauncher.exe");
      }));
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.BeginInvoke((Delegate) (() => this.update()));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.downloadProgress = new ProgressBar();
      this.information = new Label();
      this.SuspendLayout();
      this.downloadProgress.Location = new Point(12, 31);
      this.downloadProgress.Name = "downloadProgress";
      this.downloadProgress.Size = new Size(331, 10);
      this.downloadProgress.Style = ProgressBarStyle.Marquee;
      this.downloadProgress.TabIndex = 0;
      this.information.AutoSize = true;
      this.information.BackColor = Color.Transparent;
      this.information.ForeColor = Color.Snow;
      this.information.Location = new Point(9, 9);
      this.information.Name = "information";
      this.information.Size = new Size(131, 13);
      this.information.TabIndex = 1;
      this.information.Text = "Checking for latest update";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Black;
      this.BackgroundImage = (Image) Resources.luncher;
      this.ClientSize = new Size(355, 53);
      this.Controls.Add((Control) this.information);
      this.Controls.Add((Control) this.downloadProgress);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Form1);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Update";
      this.TopMost = true;
      this.Load += new EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
