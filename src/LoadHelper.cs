using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class LoadHelper
{
	// Token: 0x06000233 RID: 563 RVA: 0x0000C2BD File Offset: 0x0000A4BD
	public static AudioClip LoadAudio(string path)
	{
		return Resources.Load<AudioClip>(path);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
	public static Task<byte[]> LoadBytes(string url)
	{
		LoadHelper.<LoadBytes>d__1 <LoadBytes>d__;
		<LoadBytes>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
		<LoadBytes>d__.url = url;
		<LoadBytes>d__.<>1__state = -1;
		<LoadBytes>d__.<>t__builder.Start<LoadHelper.<LoadBytes>d__1>(ref <LoadBytes>d__);
		return <LoadBytes>d__.<>t__builder.Task;
	}
}
