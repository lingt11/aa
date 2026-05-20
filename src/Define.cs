using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class Define
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600001D RID: 29 RVA: 0x00002B8F File Offset: 0x00000D8F
	public static string UIScriptsPath
	{
		get
		{
			return Application.dataPath + "/Scripts/Logic/UI/";
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002BA0 File Offset: 0x00000DA0
	public string GetPath()
	{
		Application.dataPath + "/Scripts/Logic/UI/";
		return "";
	}

	// Token: 0x04000030 RID: 48
	public const string BuildOutputDir = "./Code/Bin";
}
