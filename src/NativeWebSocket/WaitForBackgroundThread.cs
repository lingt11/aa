using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NativeWebSocket
{
	// Token: 0x020004A0 RID: 1184
	public class WaitForBackgroundThread
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x000A1F7C File Offset: 0x000A017C
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return Task.Run(delegate()
			{
			}).ConfigureAwait(false).GetAwaiter();
		}
	}
}
