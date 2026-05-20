using System;
using System.Collections.Generic;

// Token: 0x02000077 RID: 119
public class MsgRegister : IDisposable
{
	// Token: 0x0600023D RID: 573 RVA: 0x0000C48C File Offset: 0x0000A68C
	public MsgRegister()
	{
		this.isDispose = false;
		this.removes = new List<MsgRegister.Wrapper>();
		this.handlers = new Dictionary<int, List<MsgRegister.Wrapper>>();
		this.clock = SingletonMonobehaviour<ClockUtil>.Instance.AlarmRepeat(0f, 0.1f, new Action(this.OnCheckRemoves));
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
	public void Dispose()
	{
		this.isDispose = true;
		this.removes.Clear();
		this.handlers.Clear();
		this.StopClock();
		this.removes = null;
		this.handlers = null;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register(int id, Action<Body> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register<T>(int id, Action<Body, T> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register<T1, T2>(int id, Action<Body, T1, T2> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register<T1, T2, T3>(int id, Action<Body, T1, T2, T3> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register<T1, T2, T3, T4>(int id, Action<Body, T1, T2, T3, T4> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000C515 File Offset: 0x0000A715
	public void Register<T1, T2, T3, T4, T5>(int id, Action<Body, T1, T2, T3, T4, T5> handler)
	{
		this.RegisterDelegate(id, handler);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000C520 File Offset: 0x0000A720
	public void Unregister(int id, Delegate handler)
	{
		if (handler == null)
		{
			return;
		}
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int num = this.SearchWrapperIndex(list, handler);
			if (num >= 0)
			{
				MsgRegister.Wrapper wrapper = list[num];
				wrapper.isRemove = true;
				list[num] = wrapper;
				this.removes.Add(wrapper);
			}
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000C574 File Offset: 0x0000A774
	public void Dispatcher(int id)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke(this);
			}
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
	public void Dispatcher<T>(int id, T data)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke<T>(this, data);
			}
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
	public void Dispatcher<T1, T2>(int id, T1 data1, T2 data2)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke<T1, T2>(this, data1, data2);
			}
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000C63C File Offset: 0x0000A83C
	public void Dispatcher<T1, T2, T3>(int id, T1 data1, T2 data2, T3 data3)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke<T1, T2, T3>(this, data1, data2, data3);
			}
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000C680 File Offset: 0x0000A880
	public void Dispatcher<T1, T2, T3, T4>(int id, T1 data1, T2 data2, T3 data3, T4 data4)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke<T1, T2, T3, T4>(this, data1, data2, data3, data4);
			}
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
	public void Dispatcher<T1, T2, T3, T4, T5>(int id, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
	{
		List<MsgRegister.Wrapper> list;
		if (this.handlers.TryGetValue(id, out list))
		{
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list[i].Invoke<T1, T2, T3, T4, T5>(this, data1, data2, data3, data4, data5);
			}
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000C710 File Offset: 0x0000A910
	private void RegisterDelegate(int id, Delegate handler)
	{
		if (handler == null)
		{
			return;
		}
		List<MsgRegister.Wrapper> list;
		if (!this.handlers.TryGetValue(id, out list))
		{
			list = new List<MsgRegister.Wrapper>();
			this.handlers.Add(id, list);
		}
		if (this.SearchWrapperIndex(list, handler) == -1)
		{
			list.Add(new MsgRegister.Wrapper(id, handler));
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000C75C File Offset: 0x0000A95C
	private int SearchWrapperIndex(List<MsgRegister.Wrapper> list, Delegate handler)
	{
		int result = -1;
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i].handler == handler)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000C798 File Offset: 0x0000A998
	private void OnCheckRemoves()
	{
		if (this.isDispose)
		{
			this.StopClock();
			return;
		}
		int count = this.removes.Count;
		if (count == 0)
		{
			return;
		}
		for (int i = 0; i < count; i++)
		{
			MsgRegister.Wrapper wrapper = this.removes[i];
			List<MsgRegister.Wrapper> list;
			if (this.handlers.TryGetValue(wrapper.id, out list))
			{
				int num = this.SearchWrapperIndex(list, wrapper.handler);
				if (num >= 0)
				{
					list.RemoveAt(num);
				}
				if (list.Count == 0)
				{
					this.handlers.Remove(wrapper.id);
				}
			}
		}
		this.removes.Clear();
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C833 File Offset: 0x0000AA33
	private void StopClock()
	{
		if (this.clock != null)
		{
			SingletonMonobehaviour<ClockUtil>.Instance.Stop(this.clock);
			this.clock = null;
		}
	}

	// Token: 0x0400025A RID: 602
	private Clock clock;

	// Token: 0x0400025B RID: 603
	private bool isDispose;

	// Token: 0x0400025C RID: 604
	private List<MsgRegister.Wrapper> removes;

	// Token: 0x0400025D RID: 605
	private Dictionary<int, List<MsgRegister.Wrapper>> handlers;

	// Token: 0x02000078 RID: 120
	private struct Wrapper
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000C854 File Offset: 0x0000AA54
		public Wrapper(int id, Delegate handler)
		{
			this.id = id;
			this.isRemove = false;
			this.handler = handler;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C86B File Offset: 0x0000AA6B
		public void Invoke(MsgRegister register)
		{
			if (!this.isRemove)
			{
				((Action<Body>)this.handler)(new Body(this.id, this.handler, register));
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C897 File Offset: 0x0000AA97
		public void Invoke<T>(MsgRegister register, T data)
		{
			if (!this.isRemove)
			{
				((Action<Body, T>)this.handler)(new Body(this.id, this.handler, register), data);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public void Invoke<T1, T2>(MsgRegister register, T1 data1, T2 data2)
		{
			if (!this.isRemove)
			{
				((Action<Body, T1, T2>)this.handler)(new Body(this.id, this.handler, register), data1, data2);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C8F2 File Offset: 0x0000AAF2
		public void Invoke<T1, T2, T3>(MsgRegister register, T1 data1, T2 data2, T3 data3)
		{
			if (!this.isRemove)
			{
				((Action<Body, T1, T2, T3>)this.handler)(new Body(this.id, this.handler, register), data1, data2, data3);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C922 File Offset: 0x0000AB22
		public void Invoke<T1, T2, T3, T4>(MsgRegister register, T1 data1, T2 data2, T3 data3, T4 data4)
		{
			if (!this.isRemove)
			{
				((Action<Body, T1, T2, T3, T4>)this.handler)(new Body(this.id, this.handler, register), data1, data2, data3, data4);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C954 File Offset: 0x0000AB54
		public void Invoke<T1, T2, T3, T4, T5>(MsgRegister register, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
		{
			if (!this.isRemove)
			{
				((Action<Body, T1, T2, T3, T4, T5>)this.handler)(new Body(this.id, this.handler, register), data1, data2, data3, data4, data5);
			}
		}

		// Token: 0x0400025E RID: 606
		public int id;

		// Token: 0x0400025F RID: 607
		public bool isRemove;

		// Token: 0x04000260 RID: 608
		public Delegate handler;
	}
}
