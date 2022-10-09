using System;
using HarmonyLib;

namespace warmode_cheat.cheats
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch]
	public class stamina
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00003484 File Offset: 0x00001684
		[HarmonyPatch(typeof(FirstPersonPlayer), "SpendSprintCharge")]
		[HarmonyPrefix]
		public static void Prefix(ref bool __runOriginal)
		{
			bool flag = stamina.activated;
			if (flag)
			{
				__runOriginal = false;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000034A0 File Offset: 0x000016A0
		[HarmonyPatch(typeof(FirstPersonPlayer), "GetSprintChargePower")]
		[HarmonyPrefix]
		public static void EndoreTheH(ref bool __runOriginal)
		{
			bool flag = stamina.activated;
			if (flag)
			{
				__runOriginal = false;
			}
		}

		// Token: 0x0400001D RID: 29
		public static bool activated;
	}
}
