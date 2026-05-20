using System;

// Token: 0x02000009 RID: 9
public class CodeLoader
{
	// Token: 0x0400002B RID: 43
	public static CodeLoader Instance = new CodeLoader();

	// Token: 0x0400002C RID: 44
	public Action Update;

	// Token: 0x0400002D RID: 45
	public Action FixedUpdate;

	// Token: 0x0400002E RID: 46
	public Action LateUpdate;

	// Token: 0x0400002F RID: 47
	public Action OnApplicationQuit;
}
