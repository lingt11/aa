using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000409 RID: 1033
public class SineUIControllerTopDownEffects : MonoBehaviour
{
	// Token: 0x0600179F RID: 6047 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Start()
	{
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x0009387C File Offset: 0x00091A7C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			this.canvasGroup.alpha = 1f - this.canvasGroup.alpha;
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.ChangeEffect(true);
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.ChangeEffect(false);
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.prefabSpawnerObject.SpawnPrefab();
		}
		this.nameOfThePrafab = this.prefabSpawnerObject.nameOfThePrefab;
		this.nameInUI.text = "Spawn - " + this.nameOfThePrafab;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x00093925 File Offset: 0x00091B25
	public void ChangeEffect(bool bo)
	{
		this.prefabSpawnerObject.ChangePrefabIntex(bo);
		this.nameOfThePrafab = this.prefabSpawnerObject.nameOfThePrefab;
	}

	// Token: 0x04001695 RID: 5781
	public CanvasGroup canvasGroup;

	// Token: 0x04001696 RID: 5782
	public PrefabSpawner prefabSpawnerObject;

	// Token: 0x04001697 RID: 5783
	public Text nameInUI;

	// Token: 0x04001698 RID: 5784
	private string nameOfThePrafab;
}
