using System;
using System.Diagnostics;
using UnityEngine;
using warmode_cheat.cheats;

namespace warmode_cheat.gui
{
	// Token: 0x02000003 RID: 3
	public class MainGuiStrut : MonoBehaviour
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		public MainGuiStrut(IntPtr ptr) : base(ptr)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public static void CreateInstance(Plugin loader)
		{
			MainGuiStrut mainGuiStrut = loader.AddComponent<MainGuiStrut>();
			mainGuiStrut.loader = loader;
			Object.DontDestroyOnLoad(mainGuiStrut.gameObject);
			mainGuiStrut.hideFlags |= 61;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		public void AimbotWindow(int windowID)
		{
			bool activated = aimbot.activated;
			if (activated)
			{
				GUI.Label(new Rect(0f, 20f, 200f, 20f), "Target Pos: " + aimbot.TargetVector.ToString());
				GUI.Label(new Rect(0f, 40f, 200f, 20f), "RayHitLayer: " + aimbot.RHLN);
				GUI.Label(new Rect(0f, 60f, 200f, 20f), "Player rot: " + FirstPersonPlayer.Transform.rotation.ToString());
				GUI.Label(new Rect(0f, 80f, 200f, 20f), "TargetVisible: " + aimbot.IsVisable.ToString());
				GUI.Label(new Rect(0f, 100f, 200f, 20f), "TargetActive: " + aimbot.latestTargetedPlayer.Active.ToString());
				GUI.Label(new Rect(0f, 120f, 200f, 20f), "Mode: " + aimbot.MODE);
				GUI.Label(new Rect(0f, 140f, 200f, 20f), "SQRDistance: " + aimbot.dist.ToString());
				Vector3 targetVector = aimbot.TargetVector;
				Vector3 vector;
				vector.x = targetVector.x;
				vector.z = targetVector.z;
				vector.y = targetVector.y - 1f;
				Vector3 vector2;
				vector2.x = targetVector.x;
				vector2.z = targetVector.z;
				vector2.y = targetVector.y + 2f;
				Vector3 vector3 = Camera.main.WorldToScreenPoint(vector);
				Vector3 vector4 = Camera.main.WorldToScreenPoint(vector2);
				MainGuiStrut.DrawLine(vector3, vector4, Color.blue, 3f);
			}
			else
			{
				GUI.Label(new Rect(0f, 20f, 200f, 40f), "Activate aimbot to\nview debug window");
			}
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023CC File Offset: 0x000005CC
		public void OnGUI()
		{
			bool beeg = MainGuiStrut.BEEG;
			if (beeg)
			{
				MainGuiStrut.DrawLine(new Vector2(0f, (float)(Screen.height / 2)), new Vector2((float)Screen.width, (float)(Screen.height / 2)), Color.yellow, 5f);
				MainGuiStrut.DrawLine(new Vector2((float)(Screen.width / 2), 0f), new Vector2((float)(Screen.width / 2), (float)Screen.height), Color.yellow, 5f);
			}
			bool keyDown = Input.GetKeyDown(273);
			if (keyDown)
			{
				FirstPersonPlayer.go.transform.position = FirstPersonPlayer.go.transform.position + new Vector3(0f, 20f, 0f);
			}
			GUI.Label(new Rect(100f, 100f, 300f, 100f), "cheat");
			this.PlayerRect = GUI.Window(0, this.PlayerRect, new Action<int>(this.PlayerWindow), MainGuiStrut.text);
			bool activated = aimbot.activated;
			if (activated)
			{
				this.AimbotRect = GUI.Window(1, this.AimbotRect, new Action<int>(this.AimbotWindow), "Aimbot debug window");
			}
			bool flag = !MainGuiStrut.esp;
			if (!flag)
			{
				foreach (CPlayerData cplayerData in PlayerControll.Player)
				{
					bool flag2 = cplayerData == null;
					if (!flag2)
					{
						Vector3 position = cplayerData.position;
						Vector3 vector;
						vector.x = position.x;
						vector.z = position.z;
						vector.y = position.y - 1f;
						Vector3 vector2;
						vector2.x = position.x;
						vector2.z = position.z;
						vector2.y = position.y + 2f;
						Vector3 vector3 = Camera.main.WorldToScreenPoint(vector);
						Vector3 vector4 = Camera.main.WorldToScreenPoint(vector2);
						bool flag3 = vector3.z > 0f;
						if (flag3)
						{
							GUIStyle guistyle = new GUIStyle
							{
								alignment = 4
							};
							vector3.z = vector3.y + (float)Screen.height;
							GUI.Label(new Rect(vector4.x, (float)Screen.height - vector4.y, 0f, 0f), cplayerData.Name, guistyle);
							bool flag4 = FirstPersonPlayer.Team == cplayerData.Team;
							if (flag4)
							{
								this.DrawBoxESP(vector3, vector4, Color.green, cplayerData.Name);
							}
							else
							{
								this.DrawBoxESP(vector3, vector4, Color.red, cplayerData.Name);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000026C8 File Offset: 0x000008C8
		public void DrawBoxESP(Vector3 footpos, Vector3 headpos, Color color, string name)
		{
			float num = headpos.y - footpos.y;
			float num2 = 2f;
			float num3 = num / num2;
			MainGuiStrut.DrawBox(footpos.x - num3 / 2f, (float)Screen.height - footpos.y - num, num3, num, color, 2f, name);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000271C File Offset: 0x0000091C
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			bool flag = !MainGuiStrut.lineTex;
			if (flag)
			{
				MainGuiStrut.lineTex = new Texture2D(1, 1);
			}
			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);
			bool flag2 = pointA.y > pointB.y;
			if (flag2)
			{
				num = -num;
			}
			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), MainGuiStrut.lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000027FC File Offset: 0x000009FC
		public static void DrawBox(float x, float y, float w, float h, Color color, float thickness, string name)
		{
			MainGuiStrut.DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			MainGuiStrut.DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			MainGuiStrut.DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			MainGuiStrut.DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000287C File Offset: 0x00000A7C
		public void PlayerWindow(int windowID)
		{
			GUIStyle guistyle = new GUIStyle();
			guistyle.richText = true;
			bool flag = GUI.Button(new Rect(5f, 15f, 190f, 30f), this.getToggleText("ESP", MainGuiStrut.esp));
			if (flag)
			{
				MainGuiStrut.esp = !MainGuiStrut.esp;
			}
			bool flag2 = GUI.Button(new Rect(5f, 45f, 190f, 30f), this.getToggleText("No Recoil", noshake.activated));
			if (flag2)
			{
				bool flag3 = !noshake.activated && noshake.ractivated;
				if (flag3)
				{
					noshake.ractivated = false;
				}
				noshake.activated = !noshake.activated;
			}
			bool flag4 = GUI.Button(new Rect(5f, 75f, 190f, 30f), this.getToggleText("No ammo usage", freeammo.activated));
			if (flag4)
			{
				freeammo.activated = !freeammo.activated;
			}
			bool flag5 = GUI.Button(new Rect(5f, 105f, 190f, 30f), this.getToggleText("Infinite stamina", warmode_cheat.cheats.stamina.activated));
			if (flag5)
			{
				warmode_cheat.cheats.stamina.activated = !warmode_cheat.cheats.stamina.activated;
			}
			bool flag6 = GUI.Button(new Rect(5f, 135f, 190f, 30f), this.getToggleText("Weird rapid fire", noshake.ractivated));
			if (flag6)
			{
				bool flag7 = noshake.activated && !noshake.ractivated;
				if (flag7)
				{
					noshake.activated = false;
				}
				noshake.ractivated = !noshake.ractivated;
			}
			bool flag8 = GUI.Button(new Rect(5f, 165f, 190f, 30f), this.getToggleText("AimBot", aimbot.activated));
			if (flag8)
			{
				aimbot.activated = !aimbot.activated;
			}
			bool flag9 = GUI.Button(new Rect(5f, 195f, 190f, 30f), this.getToggleText("BEEG crosshair", MainGuiStrut.BEEG));
			if (flag9)
			{
				MainGuiStrut.BEEG = !MainGuiStrut.BEEG;
			}
			bool flag10 = GUI.Button(new Rect(5f, 225f, 190f, 30f), this.getToggleText("Flight", flight.activated));
			if (flag10)
			{
				bool flag11 = !flight.activated && flight.Moonactivated;
				if (flag11)
				{
					flight.Moonactivated = false;
				}
				flight.activated = !flight.activated;
			}
			bool flag12 = GUI.Button(new Rect(5f, 255f, 190f, 30f), this.getToggleText("Moon gravity", flight.Moonactivated));
			if (flag12)
			{
				bool flag13 = flight.activated && !flight.Moonactivated;
				if (flag13)
				{
					flight.activated = false;
				}
				flight.Moonactivated = !flight.Moonactivated;
			}
			bool flag14 = GUI.Button(new Rect(5f, 285f, 190f, 30f), "Teleport to spawn [RSHIFT]") || Input.GetKeyDown(303);
			if (flag14)
			{
				FirstPersonPlayer.DevSpawn();
			}
			GUI.Label(new Rect(5f, 315f, 190f, 20f), "<color=lime>Made by stikosek#0761</color>");
			bool flag15 = GUI.Button(new Rect(150f, 315f, 40f, 20f), "<color=cyan>DNT</color>");
			if (flag15)
			{
				Process.Start("https://ko-fi.com/stikosek");
			}
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002C18 File Offset: 0x00000E18
		public string getToggleText(string name, bool state)
		{
			string result;
			if (state)
			{
				result = name + " <color=gray>[</color><color=lime>ENABLED</color><color=gray>]</color>";
			}
			else
			{
				result = name + " <color=gray>[</color><color=red>DISABLED</color><color=gray>]</color>";
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002C50 File Offset: 0x00000E50
		public static void DrawColor(Color color, Rect rect)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(1, 1, color);
			texture2D.wrapMode = 0;
			texture2D.Apply();
			GUI.DrawTexture(rect, texture2D);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002C88 File Offset: 0x00000E88
		public static void DrawWindowBackground(Color top, Color rest, Rect rect, int TopBarThickness, string Title)
		{
			MainGuiStrut.DrawColor(rest, new Rect(0f, (float)TopBarThickness, rect.width, rect.height - (float)TopBarThickness));
			MainGuiStrut.DrawColor(top, new Rect(0f, 0f, rect.width, (float)TopBarThickness));
			MainGuiStrut.DrawText(Title, new Rect(0f, 0f, rect.width, (float)TopBarThickness), TopBarThickness - 3, Color.black);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002D04 File Offset: 0x00000F04
		public static void DrawButtonToggle(Rect rect, Color border, Color rest, string NormalText)
		{
			MainGuiStrut.DrawColor(border, rect);
			MainGuiStrut.DrawText(NormalText, new Rect(rect.x + 5f, rect.y + 5f, rect.width - 10f, rect.height - 10f), 15, Color.white);
			MainGuiStrut.DrawColor(border, new Rect(5f, 5f, rect.width - 10f, rect.height - 10f));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002D94 File Offset: 0x00000F94
		public static void DrawText(string text, Rect pos, int fontSize, Color textColor)
		{
			GUIStyle textStyle = MainGuiStrut.GetTextStyle(fontSize, textColor);
			GUI.Label(pos, text, textStyle);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public static GUIStyle GetTextStyle(int fontSize, Color textColor)
		{
			GUIStyle guistyle = new GUIStyle(GUI.skin.label)
			{
				font = Resources.GetBuiltinResource<Font>("Arial.ttf"),
				fontSize = fontSize,
				alignment = 3
			};
			guistyle.normal.textColor = textColor;
			return guistyle;
		}

		// Token: 0x04000005 RID: 5
		private Plugin loader;

		// Token: 0x04000006 RID: 6
		public Rect PlayerRect = new Rect(50f, 20f, 200f, 345f);

		// Token: 0x04000007 RID: 7
		public Rect AimbotRect = new Rect(260f, 20f, 200f, 160f);

		// Token: 0x04000008 RID: 8
		private bool stamina;

		// Token: 0x04000009 RID: 9
		public static string text = "WARMODE cheat (1.1)";

		// Token: 0x0400000A RID: 10
		public static Texture2D lineTex;

		// Token: 0x0400000B RID: 11
		public static bool esp = false;

		// Token: 0x0400000C RID: 12
		private Color ButtonBorder = Color.black;

		// Token: 0x0400000D RID: 13
		private Color ButtonRest = Color.green;

		// Token: 0x0400000E RID: 14
		private Color WindowTop = Color.black;

		// Token: 0x0400000F RID: 15
		private Color WindowRest = Color.gray;

		// Token: 0x04000010 RID: 16
		public static bool BEEG = false;
	}
}
