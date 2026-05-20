using System;

// Token: 0x02000041 RID: 65
public static class LanguageManagerUtil
{
	// Token: 0x060000FD RID: 253 RVA: 0x00006C30 File Offset: 0x00004E30
	public static string Localization(this string str, string readyStr = "")
	{
		return Game.Language.Get(str, readyStr);
	}
}
