using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000074 RID: 116
public static class ExtensionMethods
{
	// Token: 0x06000238 RID: 568 RVA: 0x0000C40C File Offset: 0x0000A60C
	public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
	{
		TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
		asyncOp.completed += delegate(AsyncOperation obj)
		{
			tcs.SetResult(null);
		};
		return tcs.Task.GetAwaiter();
	}
}
