using System;
using BepInEx;
using BepInEx.IL2CPP;
using CodeStage.AntiCheat.Detectors;
using HarmonyLib;
using warmode_cheat.gui;

namespace warmode_cheat
{
	// Token: 0x02000002 RID: 2
	[BepInPlugin("net.stikosek.warmode_cheat", "warmode_cheat", "0.0.1")]
	public class Plugin : BasePlugin
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override void Load()
		{
			Harmony harmony = new Harmony("net.stikosek.warmode_cheat");
			harmony.PatchAll();
			base.Log.LogInfo("Plugin WARMODE CHEAT is loaded!");
			SpeedHackDetector.StopDetection();
			SpeedHackDetector.Dispose();
			MainGuiStrut.CreateInstance(this);
		}

		// Token: 0x04000001 RID: 1
		public const string MODNAME = "warmode_cheat";

		// Token: 0x04000002 RID: 2
		public const string AUTHOR = "stikosek";

		// Token: 0x04000003 RID: 3
		public const string GUID = "net.stikosek.warmode_cheat";

		// Token: 0x04000004 RID: 4
		public const string VERSION = "0.0.1";
	}
}
