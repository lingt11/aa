using System;

// Token: 0x02000079 RID: 121
public class MySystemEvent
{
	// Token: 0x06000257 RID: 599 RVA: 0x0000C988 File Offset: 0x0000AB88
	public MySystemEvent()
	{
		MySystemEvent.Instance = this;
		this.msgRegister = new MsgRegister();
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000C9A1 File Offset: 0x0000ABA1
	public void DispatchMessage(int id)
	{
		this.msgRegister.Dispatcher(id);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000C9AF File Offset: 0x0000ABAF
	public void DispatchMessage<T>(int id, T data)
	{
		this.msgRegister.Dispatcher<T>(id, data);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000C9BE File Offset: 0x0000ABBE
	public void DispatchMessage<T1, T2>(int id, T1 data1, T2 data2)
	{
		this.msgRegister.Dispatcher<T1, T2>(id, data1, data2);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000C9CE File Offset: 0x0000ABCE
	public void DispatchMessage<T1, T2, T3>(int id, T1 data1, T2 data2, T3 data3)
	{
		this.msgRegister.Dispatcher<T1, T2, T3>(id, data1, data2, data3);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
	public void DispatchMessage<T1, T2, T3, T4>(int id, T1 data1, T2 data2, T3 data3, T4 data4)
	{
		this.msgRegister.Dispatcher<T1, T2, T3, T4>(id, data1, data2, data3, data4);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
	public void RegisterMessage(int id, Action<Body> handler)
	{
		this.msgRegister.Register(id, handler);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000CA03 File Offset: 0x0000AC03
	public void RegisterMessage<T>(int id, Action<Body, T> handler)
	{
		this.msgRegister.Register<T>(id, handler);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000CA12 File Offset: 0x0000AC12
	public void RegisterMessage<T1, T2>(int id, Action<Body, T1, T2> handler)
	{
		this.msgRegister.Register<T1, T2>(id, handler);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000CA21 File Offset: 0x0000AC21
	public void RegisterMessage<T1, T2, T3>(int id, Action<Body, T1, T2, T3> handler)
	{
		this.msgRegister.Register<T1, T2, T3>(id, handler);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000CA30 File Offset: 0x0000AC30
	public void RegisterMessage<T1, T2, T3, T4>(int id, Action<Body, T1, T2, T3, T4> handler)
	{
		this.msgRegister.Register<T1, T2, T3, T4>(id, handler);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage(int id, Action<Body> handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage<T>(int id, Action<Body, T> handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage<T1, T2>(int id, Action<Body, T1, T2> handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage<T1, T2, T3>(int id, Action<Body, T1, T2, T3> handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage<T1, T2, T3, T4>(int id, Action<Body, T1, T2, T3, T4> handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000CA3F File Offset: 0x0000AC3F
	public void UnregisterMessage(int id, Delegate handler)
	{
		this.msgRegister.Unregister(id, handler);
	}

	// Token: 0x04000261 RID: 609
	private MsgRegister msgRegister;

	// Token: 0x04000262 RID: 610
	public static MySystemEvent Instance;
}
