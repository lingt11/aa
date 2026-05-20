using System;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class Launch : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x00002BB8 File Offset: 0x00000DB8
	private void Start()
	{
		this.gameMode = GameMode.Release;
		Launch.GameMode = this.gameMode;
		Launch.SteamMode = this.publicMode;
		GameHelperClient.IsPublicMode = this.publicMode;
		GameHelperClient.IsJoyStick = this.JoyStick;
		Application.runInBackground = true;
		AssetManager.Init();
		Main.Init();
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002C08 File Offset: 0x00000E08
	public static void ReloadDll()
	{
		Debug.Log("热重载");
		Launch.hotfixAssembly.GetType("Main").GetMethod("Quit").Invoke(null, null);
		Launch.LoadDll("./Code/Bin");
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002C40 File Offset: 0x00000E40
	private static void LoadDll(string BuildOutputDir)
	{
		string[] files = Directory.GetFiles(BuildOutputDir, "Code*.dll");
		if (files.Length != 1)
		{
			throw new Exception("Logic dll count != 1");
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files[0]);
		byte[] rawAssembly = File.ReadAllBytes(Path.Combine(BuildOutputDir, fileNameWithoutExtension + ".dll"));
		byte[] rawSymbolStore = File.ReadAllBytes(Path.Combine(BuildOutputDir, fileNameWithoutExtension + ".pdb"));
		Launch.hotfixAssembly = Assembly.Load(rawAssembly, rawSymbolStore);
		Launch.hotfixAssembly.GetType("Main").GetMethod("Init").Invoke(null, null);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002CCA File Offset: 0x00000ECA
	private void Update()
	{
		CodeLoader.Instance.Update();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002CDB File Offset: 0x00000EDB
	private void FixedUpdate()
	{
		CodeLoader.Instance.FixedUpdate();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002CEC File Offset: 0x00000EEC
	private void LateUpdate()
	{
		CodeLoader.Instance.LateUpdate();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002CFD File Offset: 0x00000EFD
	private void OnApplicationQuit()
	{
		SaveLoadManager.SaveGameData();
		CodeLoader.Instance.OnApplicationQuit();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002D13 File Offset: 0x00000F13
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			SaveLoadManager.SaveGameData();
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void OnGUI()
	{
	}

	// Token: 0x04000034 RID: 52
	[SerializeField]
	private GameMode gameMode;

	// Token: 0x04000035 RID: 53
	[SerializeField]
	private bool publicMode;

	// Token: 0x04000036 RID: 54
	[SerializeField]
	private bool JoyStick;

	// Token: 0x04000037 RID: 55
	public static GameMode GameMode;

	// Token: 0x04000038 RID: 56
	public static AssetBundle assetBundle;

	// Token: 0x04000039 RID: 57
	private static Assembly hotfixAssembly;

	// Token: 0x0400003A RID: 58
	public static bool SteamMode;

	// Token: 0x0400003B RID: 59
	public static bool quickStart;
}
