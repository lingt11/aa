using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class Timer
{
	// Token: 0x06000186 RID: 390 RVA: 0x000099E1 File Offset: 0x00007BE1
	public Timer(TimerManager tm)
	{
		this._timerManager = tm;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000099F0 File Offset: 0x00007BF0
	public void Create(float duration, int loopTimes, float delayTime, Action excuteFunc, Action completeFunc, Action cancelFunc)
	{
		this.duration = duration;
		this.loopTimes = loopTimes;
		this.delayTime = delayTime;
		this.completeFunc = completeFunc;
		this.excuteFunc = excuteFunc;
		this.cancelFunc = cancelFunc;
		float time = this.GetTime();
		this._startTime = time + duration + delayTime;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00009A3C File Offset: 0x00007C3C
	public void Update()
	{
		float time = this.GetTime();
		if (time >= this._startTime)
		{
			this.excuteFunc();
			this.loopTimes--;
			if (this.loopTimes > 0)
			{
				this._startTime = time + this.duration;
				return;
			}
			this._timerManager.CompleteTimer(this);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00009A96 File Offset: 0x00007C96
	private float GetTime()
	{
		return Time.time;
	}

	// Token: 0x040001E2 RID: 482
	private TimerManager _timerManager;

	// Token: 0x040001E3 RID: 483
	public float duration;

	// Token: 0x040001E4 RID: 484
	public int loopTimes;

	// Token: 0x040001E5 RID: 485
	public float delayTime;

	// Token: 0x040001E6 RID: 486
	public Action completeFunc;

	// Token: 0x040001E7 RID: 487
	public Action excuteFunc;

	// Token: 0x040001E8 RID: 488
	public Action cancelFunc;

	// Token: 0x040001E9 RID: 489
	private float _startTime;
}
