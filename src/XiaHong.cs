using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class XiaHong : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x000030A5 File Offset: 0x000012A5
	private void Start()
	{
		Debug.LogError("Start");
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000030B1 File Offset: 0x000012B1
	private void Awake()
	{
		Debug.LogError("Awake");
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000030BD File Offset: 0x000012BD
	private void OnEnable()
	{
		Debug.LogError("OnEnable");
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000030C9 File Offset: 0x000012C9
	private void OnDisable()
	{
		Debug.LogError("OnDisable");
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000030D5 File Offset: 0x000012D5
	private void OnDestroy()
	{
		Debug.LogError("OnDestroy");
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Update()
	{
	}
}
