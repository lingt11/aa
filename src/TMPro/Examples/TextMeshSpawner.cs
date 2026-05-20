using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200043D RID: 1085
	public class TextMeshSpawner : MonoBehaviour
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x00002D1D File Offset: 0x00000F1D
		private void Awake()
		{
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00097E44 File Offset: 0x00096044
		private void Start()
		{
			for (int i = 0; i < this.NumberOfNPC; i++)
			{
				if (this.SpawnType == 0)
				{
					GameObject gameObject = new GameObject();
					gameObject.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
					TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
					textMeshPro.fontSize = 96f;
					textMeshPro.text = "!";
					textMeshPro.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					this.floatingText_Script = gameObject.AddComponent<TextMeshProFloatingText>();
					this.floatingText_Script.SpawnType = 0;
				}
				else
				{
					GameObject gameObject2 = new GameObject();
					gameObject2.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));
					TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
					textMesh.GetComponent<Renderer>().sharedMaterial = this.TheFont.material;
					textMesh.font = this.TheFont;
					textMesh.anchor = TextAnchor.LowerCenter;
					textMesh.fontSize = 96;
					textMesh.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					textMesh.text = "!";
					this.floatingText_Script = gameObject2.AddComponent<TextMeshProFloatingText>();
					this.floatingText_Script.SpawnType = 1;
				}
			}
		}

		// Token: 0x040017BC RID: 6076
		public int SpawnType;

		// Token: 0x040017BD RID: 6077
		public int NumberOfNPC = 12;

		// Token: 0x040017BE RID: 6078
		public Font TheFont;

		// Token: 0x040017BF RID: 6079
		private TextMeshProFloatingText floatingText_Script;
	}
}
