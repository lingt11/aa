using System;

// Token: 0x02000076 RID: 118
public struct Body
{
	// Token: 0x0600023B RID: 571 RVA: 0x0000C45A File Offset: 0x0000A65A
	public Body(int id, Delegate handler, MsgRegister register)
	{
		this.id = id;
		this.handler = handler;
		this.register = register;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000C471 File Offset: 0x0000A671
	public void Unregister()
	{
		this.register.Unregister(this.id, this.handler);
	}

	// Token: 0x04000257 RID: 599
	private int id;

	// Token: 0x04000258 RID: 600
	private Delegate handler;

	// Token: 0x04000259 RID: 601
	private MsgRegister register;
}
