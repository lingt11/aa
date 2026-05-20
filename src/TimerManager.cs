using System;
using System.Collections.Generic;

// Token: 0x02000058 RID: 88
public class TimerManager : IUpdate, IApplicationQuit, ILateUpdate
{
	// Token: 0x0600018A RID: 394 RVA: 0x00009A9D File Offset: 0x00007C9D
	public TimerManager()
	{
		this.timerList.Clear();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00009AC0 File Offset: 0x00007CC0
	public void Update()
	{
		for (int i = this.timerList.Count - 1; i >= 0; i--)
		{
			this.timerList[i].Update();
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00009AF6 File Offset: 0x00007CF6
	public Timer AddTimer(float duration, Action excuteFunc)
	{
		return this.AddTimer(duration, 1, 0f, excuteFunc, null, null);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00009B08 File Offset: 0x00007D08
	public void AddLateUpdateAction(Action action)
	{
		this.lateUpdateAction = (Action)Delegate.Combine(this.lateUpdateAction, action);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00009B21 File Offset: 0x00007D21
	public Timer AddTimer(float duration, int loopTimes, Action excuteFunc)
	{
		return this.AddTimer(duration, loopTimes, 0f, excuteFunc, null, null);
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00009B34 File Offset: 0x00007D34
	public Timer AddTimer(float duration, int loopTimes, float delayTime, Action excuteFunc, Action completeFunc, Action cancelFunc)
	{
		Timer timer = new Timer(this);
		timer.Create(duration, loopTimes, delayTime, excuteFunc, completeFunc, cancelFunc);
		this.timerList.Add(timer);
		return timer;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00009B64 File Offset: 0x00007D64
	public void ExcuteTimer(Timer timer)
	{
		if (timer == null)
		{
			return;
		}
		this.timerList.Remove(timer);
		if (timer.excuteFunc != null)
		{
			timer.excuteFunc();
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00009B8A File Offset: 0x00007D8A
	public void CompleteTimer(Timer timer)
	{
		if (timer == null)
		{
			return;
		}
		this.timerList.Remove(timer);
		if (timer.completeFunc != null)
		{
			timer.completeFunc();
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00009BB0 File Offset: 0x00007DB0
	public void CancelTimer(Timer timer)
	{
		if (timer == null)
		{
			return;
		}
		this.timerList.Remove(timer);
		if (timer.cancelFunc != null)
		{
			timer.cancelFunc();
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00009BD8 File Offset: 0x00007DD8
	public void CreateTimer(Dictionary<string, Timer> timerDic, string timerName, float time, int loopNum, Action ac)
	{
		bool flag = false;
		if (timerDic.ContainsKey(timerName))
		{
			flag = true;
			this.CancelTimer(timerDic[timerName]);
		}
		Timer value = this.AddTimer(time, loopNum, ac);
		if (!flag)
		{
			timerDic.Add(timerName, value);
			return;
		}
		timerDic[timerName] = value;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00009C1F File Offset: 0x00007E1F
	public void CreateTimer(Dictionary<string, Timer> timerDic, string timerName, float time, Action ac)
	{
		this.CreateTimer(timerDic, timerName, time, 1, ac);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00009C30 File Offset: 0x00007E30
	public void CancelAllTime(Dictionary<string, Timer> timerDic)
	{
		foreach (KeyValuePair<string, Timer> keyValuePair in timerDic)
		{
			this.CancelTimer(keyValuePair.Value);
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00009C84 File Offset: 0x00007E84
	public void OnApplicationQuit()
	{
		this.timerList.Clear();
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00009C91 File Offset: 0x00007E91
	public void LateUpdate()
	{
		if (this.lateUpdateAction != null)
		{
			this.lateUpdateAction();
			this.lateUpdateAction = null;
		}
	}

	// Token: 0x040001EA RID: 490
	private List<Timer> timerList = new List<Timer>(32);

	// Token: 0x040001EB RID: 491
	private Action lateUpdateAction;
}
