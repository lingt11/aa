using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class Game : EntityStatic
{
	// Token: 0x060000DF RID: 223 RVA: 0x0000666C File Offset: 0x0000486C
	public static void Init()
	{
		EntityStatic.Clear();
		Game.isApplicationQuitting = false;
		EntityStatic.AddComp<InputManager>();
		EntityStatic.AddComp<CameraManager>();
		EntityStatic.AddComp<ExcelManager>();
		EntityStatic.AddComp<UGUIManager>();
		EntityStatic.AddComp<TimerManager>();
		EntityStatic.AddComp<MainLogic>();
		EntityStatic.AddComp<SaveLoadManager>();
		EntityStatic.AddComp<LanguageManager>();
		SaveLoadManager.InitSaveLoadManager();
		EntityStatic.AddComp<AudioManager>();
		EntityStatic.AddComp<EffectManager>();
		EntityStatic.AddComp<SkillManager>();
		EntityStatic.AddComp<EnemyManagerClient>();
		EntityStatic.AddComp<ItemManager>();
		EntityStatic.AddComp<ShopManager>();
		EntityStatic.AddComp<GamePlayItemManager>();
		EntityStatic.AddComp<LobbyManager>();
		EntityStatic.AddComp<SteamManager>();
		EntityStatic.AddComp<LocalWorkshopModSettings>();
		EntityStatic.AddComp<SteamWorkshopService>();
		EntityStatic.AddComp<LocalWorkshopPreloadService>();
		bool isPublicMode = GameHelperClient.IsPublicMode;
		EntityStatic.AddComp<StatisticDataManager>();
		EntityStatic.AddComp<AnalyticsManager>();
		EntityStatic.AddComp<PlayerManagerClient>();
		EntityStatic.AddComp<GameDataManager>();
		EntityStatic.AddComp<CardManager>();
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00006728 File Offset: 0x00004928
	public static void Update()
	{
		for (int i = 0; i < EntityStatic.updateList.Count; i++)
		{
			EntityStatic.updateList[i].Update();
		}
		if (Game.UpdateEvent != null)
		{
			Game.UpdateEvent();
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000676C File Offset: 0x0000496C
	public static void FixedUpdate()
	{
		for (int i = 0; i < EntityStatic.fixedUpdateList.Count; i++)
		{
			EntityStatic.fixedUpdateList[i].FixedUpdate();
		}
		if (Game.FixedUpdateEvent != null)
		{
			Game.FixedUpdateEvent();
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000067B0 File Offset: 0x000049B0
	public static void LateUpdate()
	{
		for (int i = 0; i < EntityStatic.lateUpdateList.Count; i++)
		{
			EntityStatic.lateUpdateList[i].LateUpdate();
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000067E4 File Offset: 0x000049E4
	public static void OnApplicationQuit()
	{
		if (Game.isApplicationQuitting)
		{
			return;
		}
		Game.isApplicationQuitting = true;
		GameHelperClient.StopNet();
		for (int i = 0; i < EntityStatic.applicationList.Count; i++)
		{
			EntityStatic.applicationList[i].OnApplicationQuit();
		}
		EntityStatic.Clear();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000682E File Offset: 0x00004A2E
	public static void QuitApplication()
	{
		Game.OnApplicationQuit();
		Application.Quit();
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000683A File Offset: 0x00004A3A
	public static MainLogic MainLogic
	{
		get
		{
			return EntityStatic.Get<MainLogic>();
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006841 File Offset: 0x00004A41
	public static UGUIManager UI
	{
		get
		{
			return EntityStatic.Get<UGUIManager>();
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006848 File Offset: 0x00004A48
	public static ExcelManager Excel
	{
		get
		{
			return EntityStatic.Get<ExcelManager>();
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000684F File Offset: 0x00004A4F
	public static TimerManager TimerManager
	{
		get
		{
			return EntityStatic.Get<TimerManager>();
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006856 File Offset: 0x00004A56
	public static AudioManager AudioManager
	{
		get
		{
			return EntityStatic.Get<AudioManager>();
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000EA RID: 234 RVA: 0x0000685D File Offset: 0x00004A5D
	public static SaveLoadManager Save
	{
		get
		{
			return EntityStatic.Get<SaveLoadManager>();
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000EB RID: 235 RVA: 0x00006864 File Offset: 0x00004A64
	public static Camera Camera
	{
		get
		{
			return EntityStatic.Get<CameraManager>().camera;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00006870 File Offset: 0x00004A70
	public static CameraManager CameraManager
	{
		get
		{
			return EntityStatic.Get<CameraManager>();
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060000ED RID: 237 RVA: 0x00006877 File Offset: 0x00004A77
	public static LanguageManager Language
	{
		get
		{
			return EntityStatic.Get<LanguageManager>();
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060000EE RID: 238 RVA: 0x0000687E File Offset: 0x00004A7E
	public static EffectManager EffectManager
	{
		get
		{
			return EntityStatic.Get<EffectManager>();
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060000EF RID: 239 RVA: 0x00006885 File Offset: 0x00004A85
	public static AnalyticsManager Analytics
	{
		get
		{
			return EntityStatic.Get<AnalyticsManager>();
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000688C File Offset: 0x00004A8C
	public static GameDataManager GameData
	{
		get
		{
			return EntityStatic.Get<GameDataManager>();
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006893 File Offset: 0x00004A93
	public static SkillManager SkillManager
	{
		get
		{
			return EntityStatic.Get<SkillManager>();
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000689A File Offset: 0x00004A9A
	public static EnemyManagerClient EnemyManagerClient
	{
		get
		{
			return EntityStatic.Get<EnemyManagerClient>();
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x000068A1 File Offset: 0x00004AA1
	public static PlayerManagerClient PlayerManagerClient
	{
		get
		{
			return EntityStatic.Get<PlayerManagerClient>();
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x000068A8 File Offset: 0x00004AA8
	public static ItemManager ItemManager
	{
		get
		{
			return EntityStatic.Get<ItemManager>();
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x000068AF File Offset: 0x00004AAF
	public static GamePlayItemManager GamePlayItemManager
	{
		get
		{
			return EntityStatic.Get<GamePlayItemManager>();
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060000F6 RID: 246 RVA: 0x000068B6 File Offset: 0x00004AB6
	public static LocalWorkshopModSettings LocalWorkshopModSettings
	{
		get
		{
			return EntityStatic.Get<LocalWorkshopModSettings>();
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x000068BD File Offset: 0x00004ABD
	public static SteamWorkshopService SteamWorkshopService
	{
		get
		{
			return EntityStatic.Get<SteamWorkshopService>();
		}
	}

	// Token: 0x04000107 RID: 263
	public static Action UpdateEvent;

	// Token: 0x04000108 RID: 264
	public static Action FixedUpdateEvent;

	// Token: 0x04000109 RID: 265
	private static bool isApplicationQuitting;
}
