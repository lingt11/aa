using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class ClockUtil : SingletonMonobehaviour<ClockUtil>
{
	// Token: 0x060001EC RID: 492 RVA: 0x0000B3B0 File Offset: 0x000095B0
	public Clock AlarmAt(DateTime dateTime, Action callBack)
	{
		if (dateTime < DateTime.Now)
		{
			Debug.LogErrorFormat("不合理的报警时间:dateTime-->{0}", new object[]
			{
				dateTime
			});
			return null;
		}
		Clock clock = new Clock(dateTime, callBack);
		this.clocks.Add(clock);
		return clock;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000B3FC File Offset: 0x000095FC
	public Clock AlarmAfter(float second, Action callBack)
	{
		if (second < 0f)
		{
			Debug.LogErrorFormat("不合理的报警时间:second-->{0}", new object[]
			{
				second
			});
			return null;
		}
		Clock clock = new Clock(Time.time + second, callBack);
		if (second == 0f)
		{
			clock.Invoke();
			return clock;
		}
		this.clocks.Add(clock);
		return clock;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000B458 File Offset: 0x00009658
	public Clock AlarmRepeat(float delay, float repeatInterval, Action callBack)
	{
		if (delay < 0f)
		{
			Debug.LogErrorFormat("不合理的延迟时间 :delay-->{0}", new object[]
			{
				delay
			});
			return null;
		}
		if (repeatInterval <= 0f)
		{
			Debug.LogErrorFormat("不合理的重复间隔 :repeatInterval-->{0}", new object[]
			{
				repeatInterval
			});
			return null;
		}
		Clock clock = new Clock(Time.time + delay, repeatInterval, callBack);
		this.clocks.Add(clock);
		return clock;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000B4C7 File Offset: 0x000096C7
	public void Stop(Clock clock)
	{
		if (clock != null)
		{
			clock.Abandon();
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000B4D4 File Offset: 0x000096D4
	public void Dispose()
	{
		for (int i = this.clocks.Count - 1; i >= 0; i--)
		{
			this.clocks[i].Abandon();
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000B50C File Offset: 0x0000970C
	private void Update()
	{
		for (int i = this.clocks.Count - 1; i >= 0; i--)
		{
			Clock clock = this.clocks[i];
			if (clock.IsStopped)
			{
				this.clocks.RemoveAt(i);
			}
			else
			{
				clock.Invoke();
			}
		}
	}

	// Token: 0x0400022F RID: 559
	private readonly List<Clock> clocks = new List<Clock>();
}
