using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class Clock
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000B1E2 File Offset: 0x000093E2
	// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000B1EA File Offset: 0x000093EA
	public bool IsStopped { get; private set; }

	// Token: 0x060001E6 RID: 486 RVA: 0x0000B1F3 File Offset: 0x000093F3
	public Clock(float time, Action callBack)
	{
		this.type = ClockType.UnityTimeClock;
		this.alarmTime1 = time;
		this.onAlarm = callBack;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000B228 File Offset: 0x00009428
	public Clock(float time, float interval, Action callBack)
	{
		this.type = ClockType.UnityTimeClock;
		this.alarmTime1 = time;
		this.repeat = true;
		this.interval = interval;
		this.nextAlarmSecond = time + interval;
		this.onAlarm = callBack;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000B27D File Offset: 0x0000947D
	public Clock(DateTime time, Action callBack)
	{
		this.type = ClockType.DateTimeClock;
		this.alarmTime2 = time;
		this.onAlarm = callBack;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000B2B0 File Offset: 0x000094B0
	public void Invoke()
	{
		ClockType clockType = this.type;
		if (clockType != ClockType.DateTimeClock)
		{
			if (clockType != ClockType.UnityTimeClock)
			{
				return;
			}
			if (this.repeat)
			{
				if (Time.time >= this.nextAlarmSecond)
				{
					this.Alarm(false);
					this.nextAlarmSecond += this.interval;
					return;
				}
			}
			else if (Time.time >= this.alarmTime1)
			{
				this.Alarm(true);
				this.IsStopped = true;
			}
		}
		else if (DateTime.Now >= this.alarmTime2)
		{
			this.Alarm(true);
			this.IsStopped = true;
			return;
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000B338 File Offset: 0x00009538
	public void Abandon()
	{
		if (this.IsStopped)
		{
			return;
		}
		this.IsStopped = true;
		this.onAlarm = null;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000B354 File Offset: 0x00009554
	private void Alarm(bool once)
	{
		try
		{
			Action action = this.onAlarm;
			if (action != null)
			{
				action();
			}
		}
		catch (Exception message)
		{
			this.IsStopped = true;
			this.onAlarm = null;
			Debug.LogError(message);
		}
		finally
		{
			if (once)
			{
				this.onAlarm = null;
			}
		}
	}

	// Token: 0x04000227 RID: 551
	private Action onAlarm;

	// Token: 0x04000228 RID: 552
	public readonly ClockType type;

	// Token: 0x04000229 RID: 553
	public readonly float alarmTime1;

	// Token: 0x0400022A RID: 554
	public readonly DateTime alarmTime2 = DateTime.Now;

	// Token: 0x0400022B RID: 555
	private readonly bool repeat;

	// Token: 0x0400022C RID: 556
	private readonly float interval = 10f;

	// Token: 0x0400022D RID: 557
	private float nextAlarmSecond;
}
