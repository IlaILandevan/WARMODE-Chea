using System;
using HarmonyLib;

namespace warmode_cheat.cheats
{
	// Token: 0x02000007 RID: 7
	[HarmonyPatch]
	public class freeammo
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000034C8 File Offset: 0x000016C8
		[HarmonyPatch(typeof(FirstPersonPlayer), "SubtractAmmo")]
		[HarmonyPrefix]
		public static void Prefix(ref bool __runOriginal)
		{
			bool flag = freeammo.activated;
			if (flag)
			{
				__runOriginal = false;
			}
		}

		// Token: 0x0400001E RID: 30
		public static bool activated;
	}
}
