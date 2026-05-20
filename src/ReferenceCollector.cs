using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class ReferenceCollector : MonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x06000305 RID: 773 RVA: 0x00014DA4 File Offset: 0x00012FA4
	public T Get<T>(string key) where T : class
	{
		Object @object;
		if (!this.dict.TryGetValue(key, out @object))
		{
			return default(T);
		}
		return @object as T;
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00014DD8 File Offset: 0x00012FD8
	public Object GetObject(string key)
	{
		Object result;
		if (!this.dict.TryGetValue(key, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00014DF8 File Offset: 0x00012FF8
	public void OnAfterDeserialize()
	{
		this.dict.Clear();
		foreach (ReferenceCollectorData referenceCollectorData in this.data)
		{
			if (!this.dict.ContainsKey(referenceCollectorData.key))
			{
				this.dict.Add(referenceCollectorData.key, referenceCollectorData.gameObject);
			}
		}
	}

	// Token: 0x0400028D RID: 653
	public List<ReferenceCollectorData> data = new List<ReferenceCollectorData>();

	// Token: 0x0400028E RID: 654
	private readonly Dictionary<string, Object> dict = new Dictionary<string, Object>();
}
