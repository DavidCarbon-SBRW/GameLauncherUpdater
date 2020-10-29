// Decompiled with JetBrains decompiler
// Type: GameLauncherUpdate.Program
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System;
using System.Windows.Forms;

namespace GameLauncherUpdate
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(true);
      Application.Run((Form) new Form1());
    }
  }
}
