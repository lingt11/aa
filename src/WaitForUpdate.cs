using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class WaitForUpdate : CustomYieldInstruction
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000330 RID: 816 RVA: 0x0001562E File Offset: 0x0001382E
	public override bool keepWaiting
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00015634 File Offset: 0x00013834
	public WaitForUpdate.MainThreadAwaiter GetAwaiter()
	{
		WaitForUpdate.MainThreadAwaiter mainThreadAwaiter = new WaitForUpdate.MainThreadAwaiter();
		MainThreadUtil.Run(WaitForUpdate.CoroutineWrapper(this, mainThreadAwaiter));
		return mainThreadAwaiter;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00015654 File Offset: 0x00013854
	public static IEnumerator CoroutineWrapper(IEnumerator theWorker, WaitForUpdate.MainThreadAwaiter awaiter)
	{
		yield return theWorker;
		awaiter.Complete();
		yield break;
	}

	// Token: 0x020000A9 RID: 169
	public class MainThreadAwaiter : INotifyCompletion
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00015672 File Offset: 0x00013872
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0001567A File Offset: 0x0001387A
		public bool IsCompleted { get; set; }

		// Token: 0x06000336 RID: 822 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void GetResult()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00015683 File Offset: 0x00013883
		public void Complete()
		{
			this.IsCompleted = true;
			Action action = this.continuation;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001569C File Offset: 0x0001389C
		void INotifyCompletion.OnCompleted(Action continuation)
		{
			this.continuation = continuation;
		}

		// Token: 0x0400032E RID: 814
		private Action continuation;
	}
}
