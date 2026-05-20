using System;
using System.Collections.Generic;

// Token: 0x02000059 RID: 89
public static class TimeHelp
{
	// Token: 0x06000198 RID: 408 RVA: 0x00009CAD File Offset: 0x00007EAD
	public static void CreateTimer(this Dictionary<string, Timer> timerDic, string timeName, float time, Action ac)
	{
		Game.TimerManager.CreateTimer(timerDic, timeName, time, 1, ac);
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00009CBE File Offset: 0x00007EBE
	public static void CreateTimer(this Dictionary<string, Timer> timerDic, string timeName, float time, int loopNum, Action ac)
	{
		Game.TimerManager.CreateTimer(timerDic, timeName, time, loopNum, ac);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009CD0 File Offset: 0x00007ED0
	public static void CancelAllTime(this Dictionary<string, Timer> timerDic)
	{
		Game.TimerManager.CancelAllTime(timerDic);
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00009CDD File Offset: 0x00007EDD
	public static void CancelOneTime(this Dictionary<string, Timer> timerDic, string timerName)
	{
		if (timerDic.ContainsKey(timerName))
		{
			Game.TimerManager.CancelTimer(timerDic[timerName]);
		}
	}
}
