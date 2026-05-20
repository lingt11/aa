using System;

// Token: 0x02000031 RID: 49
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DevConsole : Attribute
{
	// Token: 0x060000C0 RID: 192 RVA: 0x00005C36 File Offset: 0x00003E36
	public DevConsole(string str)
	{
		this.name = str;
	}

	// Token: 0x040000F7 RID: 247
	public string name;
}
