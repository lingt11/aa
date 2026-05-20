using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
[Serializable]
public class JsonSerialization<T>
{
	// Token: 0x0600008A RID: 138 RVA: 0x00004612 File Offset: 0x00002812
	public JsonSerialization(List<T> data)
	{
		this.data = data;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004621 File Offset: 0x00002821
	public List<T> ToList()
	{
		return this.data;
	}

	// Token: 0x0400008C RID: 140
	[SerializeField]
	private List<T> data;
}
