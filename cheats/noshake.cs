using System;
using System.Runtime.InteropServices;
using HarmonyLib;
using Il2CppSystem;

namespace warmode_cheat.cheats
{
	// Token: 0x02000008 RID: 8
	[HarmonyPatch]
	public class noshake
	{
		// Token: 0x0600001E RID: 30
		[DllImport("GameAssembly", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void il2cpp_raise_exception(IntPtr exc);

		// Token: 0x0600001F RID: 31 RVA: 0x000034F0 File Offset: 0x000016F0
		[HarmonyPatch(typeof(vp_FPCamera), "AddRecoilForce")]
		[HarmonyPrefix]
		public static void Prefix(ref bool __runOriginal)
		{
			bool flag = noshake.ractivated;
			if (flag)
			{
				noshake.il2cpp_raise_exception(new Exception().Pointer);
			}
			bool flag2 = noshake.activated;
			if (flag2)
			{
				__runOriginal = false;
			}
		}

		// Token: 0x0400001F RID: 31
		public static bool activated;

		// Token: 0x04000020 RID: 32
		public static bool ractivated;
	}
}
