using System;
using HarmonyLib;
using UnityEngine;

namespace warmode_cheat.cheats
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch]
	public class aimbot
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002E20 File Offset: 0x00001020
		[HarmonyPatch(typeof(vp_FPCamera), "Update")]
		[HarmonyPrefix]
		public static void Prefix(vp_FPCamera __instance)
		{
			bool flag = aimbot.activated;
			if (flag)
			{
				aimbot.TargetVector = aimbot.GetClosestEnemy(PlayerControll.Player, FirstPersonPlayer.Transform, FirstPersonPlayer.Team, true);
				bool flag2 = aimbot.dist > 1700f;
				if (flag2)
				{
					aimbot.MODE = "FARBODY";
				}
				else
				{
					bool flag3 = aimbot.dist > 700f;
					if (flag3)
					{
						aimbot.MODE = "BODY";
					}
					else
					{
						aimbot.MODE = "HEAD";
					}
				}
				bool flag4 = aimbot.TargetVector != Vector3.zero;
				if (flag4)
				{
					Transform transform = new Transform();
					transform = FirstPersonPlayer.Transform;
					bool flag5 = aimbot.MODE == "BODY";
					if (flag5)
					{
						aimbot.TargetVector.y = aimbot.TargetVector.y - 1f;
					}
					else
					{
						bool flag6 = aimbot.MODE == "FARBODY";
						if (flag6)
						{
							aimbot.TargetVector.y = aimbot.TargetVector.y - 0.5f;
						}
					}
					transform.LookAt(aimbot.TargetVector);
					aimbot.IsVisable = aimbot.IsPlayerVisable(aimbot.latestTargetedPlayer.position);
					bool key = Input.GetKey(113);
					if (key)
					{
						__instance.SetRotation(transform.eulerAngles, true, true);
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002F70 File Offset: 0x00001170
		public static Vector3 GetClosestEnemy(CPlayerData[] data, Transform playertrans, int PlTeam, bool onlyVisiblePlayers)
		{
			CPlayerData cplayerData = null;
			float num = float.PositiveInfinity;
			Vector3 position = playertrans.position;
			foreach (CPlayerData cplayerData2 in data)
			{
				bool flag = cplayerData2 == null;
				if (!flag)
				{
					bool flag2 = cplayerData2.Team == PlTeam;
					if (!flag2)
					{
						Vector3 vector = Camera.main.WorldToScreenPoint(cplayerData2.position);
						float sqrMagnitude = (cplayerData2.position - position).sqrMagnitude;
						bool flag3 = sqrMagnitude < num;
						if (flag3)
						{
							num = sqrMagnitude;
							aimbot.dist = num;
							cplayerData = cplayerData2;
						}
					}
				}
			}
			aimbot.latestTargetedPlayer = cplayerData;
			bool flag4 = cplayerData != null;
			Vector3 result;
			if (flag4)
			{
				result = cplayerData.position;
			}
			else
			{
				result = Vector3.zero;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003034 File Offset: 0x00001234
		public static bool IsPlayerVisable(Vector3 toCheck)
		{
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(Camera.main.transform.position, toCheck, ref raycastHit, 1E+12f);
			if (flag)
			{
				aimbot.RHLN = raycastHit.collider.name;
				bool flag2 = raycastHit.collider.name == "LocalPlayer";
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000011 RID: 17
		public static Vector3 TargetVector = Vector3.zero;

		// Token: 0x04000012 RID: 18
		public static string RHLN;

		// Token: 0x04000013 RID: 19
		public static float dist = 0f;

		// Token: 0x04000014 RID: 20
		public static string MODE = "HEAD";

		// Token: 0x04000015 RID: 21
		public static CPlayerData latestTargetedPlayer;

		// Token: 0x04000016 RID: 22
		public static bool IsVisable = false;

		// Token: 0x04000017 RID: 23
		public static bool activated = false;

		// Token: 0x04000018 RID: 24
		public static int smooth = 1;

		// Token: 0x04000019 RID: 25
		public static float minDist = 99999f;

		// Token: 0x0400001A RID: 26
		public static LayerMask mask = LayerMask.GetMask(new string[]
		{
			"Default",
			"Player3rd 21",
			"Map",
			"Player 25"
		});
	}
}
