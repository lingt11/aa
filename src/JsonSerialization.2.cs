using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000020 RID: 32
[Serializable]
public class JsonSerialization<TKey, TValue> : ISerializationCallbackReceiver
{
	// Token: 0x0600008C RID: 140 RVA: 0x00004629 File Offset: 0x00002829
	public JsonSerialization(Dictionary<TKey, TValue> data)
	{
		this.data = data;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004638 File Offset: 0x00002838
	public void OnBeforeSerialize()
	{
		this.keys = new List<TKey>(this.data.Keys);
		this.values = new List<TValue>(this.data.Values);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004668 File Offset: 0x00002868
	public void OnAfterDeserialize()
	{
		if (this.keys == null || this.values == null || this.keys.Count <= 0 || this.values.Count <= 0)
		{
			Debug.LogError("反序列化失败!");
			return;
		}
		if (this.keys.Count == this.values.Count)
		{
			this.data = new Dictionary<TKey, TValue>(this.keys.Count);
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.data.Add(this.keys[i], this.values[i]);
			}
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004713 File Offset: 0x00002913
	public Dictionary<TKey, TValue> ToDictionary()
	{
		return this.data;
	}

	// Token: 0x0400008D RID: 141
	[SerializeField]
	private List<TKey> keys;

	// Token: 0x0400008E RID: 142
	[SerializeField]
	private List<TValue> values;

	// Token: 0x0400008F RID: 143
	private Dictionary<TKey, TValue> data;
}
