// Decompiled with JetBrains decompiler
// Type: GameLauncherUpdate.Delay
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System;
using System.Windows.Forms;

namespace GameLauncherUpdate
{
  internal class Delay
  {
    public static void WaitSeconds(int sec)
    {
      if (sec < 1)
        return;
      DateTime dateTime = DateTime.Now.AddSeconds((double) sec);
      while (DateTime.Now < dateTime)
        Application.DoEvents();
    }

    public static void WaitMSeconds(int sec)
    {
      if (sec < 1)
        return;
      DateTime dateTime = DateTime.Now.AddMilliseconds((double) sec);
      while (DateTime.Now < dateTime)
        Application.DoEvents();
    }
  }
}
