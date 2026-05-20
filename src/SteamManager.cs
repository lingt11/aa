using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x020003EE RID: 1006
[DisallowMultipleComponent]
public class SteamManager : IApplicationQuit, IUpdate
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06001737 RID: 5943 RVA: 0x00090C66 File Offset: 0x0008EE66
	public bool Initialized
	{
		get
		{
			return this.m_bInitialized;
		}
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00090C6E File Offset: 0x0008EE6E
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00090C76 File Offset: 0x0008EE76
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x00090C84 File Offset: 0x0008EE84
	public static void Reset()
	{
		SteamManager.InitOnPlayMode();
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x00090C8C File Offset: 0x0008EE8C
	public SteamManager()
	{
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.");
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			{
				Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			string str = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
			DllNotFoundException ex2 = ex;
			Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.");
			Debug.LogError("SteamSDK启动失败，可能是未启动steam缘故");
			return;
		}
		SteamManager.s_EverInitialized = true;
		this.OnEnable();
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x00090D64 File Offset: 0x0008EF64
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x00090DA1 File Offset: 0x0008EFA1
	public void OnApplicationQuit()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		Debug.Log("steam结束");
		SteamAPI.Shutdown();
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x00090DCA File Offset: 0x0008EFCA
	public void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x040015CB RID: 5579
	protected static bool s_EverInitialized;

	// Token: 0x040015CC RID: 5580
	protected static SteamManager s_instance;

	// Token: 0x040015CD RID: 5581
	protected bool m_bInitialized;

	// Token: 0x040015CE RID: 5582
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
