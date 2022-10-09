using System;
using HarmonyLib;
using UnityEngine;
using warmode_cheat.gui;

namespace warmode_cheat.cheats
{
	// Token: 0x02000005 RID: 5
	[HarmonyPatch]
	public class flight
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00003120 File Offset: 0x00001320
		[HarmonyPatch(typeof(vp_FPController), "FixedUpdate")]
		[HarmonyPrefix]
		public static void Prefix(ref bool __runOriginal, vp_FPController __instance)
		{
			MainGuiStrut.text = __instance.PhysicsGravityModifier.ToString();
			bool flag = flight.activated;
			if (flag)
			{
				__instance.m_Grounded = true;
				__instance.PhysicsGravityModifier = 0.0001f;
				float num = Input.GetKey(306) ? 0.5f : (Input.GetKey(304) ? 1f : 0.5f);
				bool key = Input.GetKey(32);
				if (key)
				{
					FirstPersonPlayer.go.gameObject.transform.position = new Vector3(FirstPersonPlayer.go.gameObject.transform.position.x, FirstPersonPlayer.go.gameObject.transform.position.y + num, FirstPersonPlayer.go.gameObject.transform.position.z);
				}
				Vector3 position = FirstPersonPlayer.go.gameObject.transform.position;
				bool key2 = Input.GetKey(119);
				if (key2)
				{
					FirstPersonPlayer.go.gameObject.transform.position = new Vector3(position.x + Camera.main.transform.forward.x * Camera.main.transform.up.y * num, position.y + Camera.main.transform.forward.y * num, position.z + Camera.main.transform.forward.z * Camera.main.transform.up.y * num);
				}
				bool key3 = Input.GetKey(115);
				if (key3)
				{
					FirstPersonPlayer.go.gameObject.transform.position = new Vector3(position.x - Camera.main.transform.forward.x * Camera.main.transform.up.y * num, position.y - Camera.main.transform.forward.y * num, position.z - Camera.main.transform.forward.z * Camera.main.transform.up.y * num);
				}
				bool key4 = Input.GetKey(100);
				if (key4)
				{
					FirstPersonPlayer.go.gameObject.transform.position = new Vector3(position.x + Camera.main.transform.right.x * num, position.y, position.z + Camera.main.transform.right.z * num);
				}
				bool key5 = Input.GetKey(97);
				if (key5)
				{
					FirstPersonPlayer.go.gameObject.transform.position = new Vector3(position.x - Camera.main.transform.right.x * num, position.y, position.z - Camera.main.transform.right.z * num);
				}
			}
			else
			{
				bool moonactivated = flight.Moonactivated;
				if (moonactivated)
				{
					__instance.m_Grounded = true;
					__instance.PhysicsGravityModifier = 0.04f;
				}
				else
				{
					__instance.PhysicsGravityModifier = 0.2f;
				}
			}
		}

		// Token: 0x0400001B RID: 27
		public static bool activated;

		// Token: 0x0400001C RID: 28
		public static bool Moonactivated;
	}
}
