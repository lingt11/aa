using System;

// Token: 0x02000032 RID: 50
[AttributeUsage(AttributeTargets.Class)]
public class DevPriority : Attribute
{
	// Token: 0x060000C1 RID: 193 RVA: 0x00005C45 File Offset: 0x00003E45
	public DevPriority(int priority)
	{
		this.priority = priority;
	}

	// Token: 0x040000F8 RID: 248
	public int priority;
}
