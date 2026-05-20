using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200043A RID: 1082
	public class TextMeshProFloatingText : MonoBehaviour
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x000974EE File Offset: 0x000956EE
		private void Awake()
		{
			this.m_transform = base.transform;
			this.m_floatingText = new GameObject(base.name + " floating text");
			this.m_cameraTransform = Camera.main.transform;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00097528 File Offset: 0x00095728
		private void Start()
		{
			if (this.SpawnType == 0)
			{
				this.m_textMeshPro = this.m_floatingText.AddComponent<TextMeshPro>();
				this.m_textMeshPro.rectTransform.sizeDelta = new Vector2(3f, 3f);
				this.m_floatingText_Transform = this.m_floatingText.transform;
				this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0f, 15f, 0f);
				this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
				this.m_textMeshPro.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), byte.MaxValue);
				this.m_textMeshPro.fontSize = 24f;
				this.m_textMeshPro.enableKerning = false;
				this.m_textMeshPro.text = string.Empty;
				this.m_textMeshPro.isTextObjectScaleStatic = this.IsTextObjectScaleStatic;
				base.StartCoroutine(this.DisplayTextMeshProFloatingText());
				return;
			}
			if (this.SpawnType == 1)
			{
				this.m_floatingText_Transform = this.m_floatingText.transform;
				this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0f, 15f, 0f);
				this.m_textMesh = this.m_floatingText.AddComponent<TextMesh>();
				this.m_textMesh.font = Resources.Load<Font>("Fonts/ARIAL");
				this.m_textMesh.GetComponent<Renderer>().sharedMaterial = this.m_textMesh.font.material;
				this.m_textMesh.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), byte.MaxValue);
				this.m_textMesh.anchor = TextAnchor.LowerCenter;
				this.m_textMesh.fontSize = 24;
				base.StartCoroutine(this.DisplayTextMeshFloatingText());
				return;
			}
			int spawnType = this.SpawnType;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00097749 File Offset: 0x00095949
		public IEnumerator DisplayTextMeshProFloatingText()
		{
			float CountDuration = 2f;
			float starting_Count = Random.Range(5f, 20f);
			float current_Count = starting_Count;
			Vector3 start_pos = this.m_floatingText_Transform.position;
			Color32 start_color = this.m_textMeshPro.color;
			float alpha = 255f;
			float fadeDuration = 3f / starting_Count * CountDuration;
			while (current_Count > 0f)
			{
				current_Count -= Time.deltaTime / CountDuration * starting_Count;
				if (current_Count <= 3f)
				{
					alpha = Mathf.Clamp(alpha - Time.deltaTime / fadeDuration * 255f, 0f, 255f);
				}
				int num = (int)current_Count;
				this.m_textMeshPro.text = num.ToString();
				this.m_textMeshPro.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);
				this.m_floatingText_Transform.position += new Vector3(0f, starting_Count * Time.deltaTime, 0f);
				if (!this.lastPOS.Compare(this.m_cameraTransform.position, 1000) || !this.lastRotation.Compare(this.m_cameraTransform.rotation, 1000))
				{
					this.lastPOS = this.m_cameraTransform.position;
					this.lastRotation = this.m_cameraTransform.rotation;
					this.m_floatingText_Transform.rotation = this.lastRotation;
					Vector3 vector = this.m_transform.position - this.lastPOS;
					this.m_transform.forward = new Vector3(vector.x, 0f, vector.z);
				}
				yield return TextMeshProFloatingText.k_WaitForEndOfFrame;
			}
			yield return TextMeshProFloatingText.k_WaitForSecondsRandom[Random.Range(0, 19)];
			this.m_floatingText_Transform.position = start_pos;
			base.StartCoroutine(this.DisplayTextMeshProFloatingText());
			yield break;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00097758 File Offset: 0x00095958
		public IEnumerator DisplayTextMeshFloatingText()
		{
			float CountDuration = 2f;
			float starting_Count = Random.Range(5f, 20f);
			float current_Count = starting_Count;
			Vector3 start_pos = this.m_floatingText_Transform.position;
			Color32 start_color = this.m_textMesh.color;
			float alpha = 255f;
			float fadeDuration = 3f / starting_Count * CountDuration;
			while (current_Count > 0f)
			{
				current_Count -= Time.deltaTime / CountDuration * starting_Count;
				if (current_Count <= 3f)
				{
					alpha = Mathf.Clamp(alpha - Time.deltaTime / fadeDuration * 255f, 0f, 255f);
				}
				int num = (int)current_Count;
				this.m_textMesh.text = num.ToString();
				this.m_textMesh.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);
				this.m_floatingText_Transform.position += new Vector3(0f, starting_Count * Time.deltaTime, 0f);
				if (!this.lastPOS.Compare(this.m_cameraTransform.position, 1000) || !this.lastRotation.Compare(this.m_cameraTransform.rotation, 1000))
				{
					this.lastPOS = this.m_cameraTransform.position;
					this.lastRotation = this.m_cameraTransform.rotation;
					this.m_floatingText_Transform.rotation = this.lastRotation;
					Vector3 vector = this.m_transform.position - this.lastPOS;
					this.m_transform.forward = new Vector3(vector.x, 0f, vector.z);
				}
				yield return TextMeshProFloatingText.k_WaitForEndOfFrame;
			}
			yield return TextMeshProFloatingText.k_WaitForSecondsRandom[Random.Range(0, 20)];
			this.m_floatingText_Transform.position = start_pos;
			base.StartCoroutine(this.DisplayTextMeshFloatingText());
			yield break;
		}

		// Token: 0x0400179B RID: 6043
		public Font TheFont;

		// Token: 0x0400179C RID: 6044
		private GameObject m_floatingText;

		// Token: 0x0400179D RID: 6045
		private TextMeshPro m_textMeshPro;

		// Token: 0x0400179E RID: 6046
		private TextMesh m_textMesh;

		// Token: 0x0400179F RID: 6047
		private Transform m_transform;

		// Token: 0x040017A0 RID: 6048
		private Transform m_floatingText_Transform;

		// Token: 0x040017A1 RID: 6049
		private Transform m_cameraTransform;

		// Token: 0x040017A2 RID: 6050
		private Vector3 lastPOS = Vector3.zero;

		// Token: 0x040017A3 RID: 6051
		private Quaternion lastRotation = Quaternion.identity;

		// Token: 0x040017A4 RID: 6052
		public int SpawnType;

		// Token: 0x040017A5 RID: 6053
		public bool IsTextObjectScaleStatic;

		// Token: 0x040017A6 RID: 6054
		private static WaitForEndOfFrame k_WaitForEndOfFrame = new WaitForEndOfFrame();

		// Token: 0x040017A7 RID: 6055
		private static WaitForSeconds[] k_WaitForSecondsRandom = new WaitForSeconds[]
		{
			new WaitForSeconds(0.05f),
			new WaitForSeconds(0.1f),
			new WaitForSeconds(0.15f),
			new WaitForSeconds(0.2f),
			new WaitForSeconds(0.25f),
			new WaitForSeconds(0.3f),
			new WaitForSeconds(0.35f),
			new WaitForSeconds(0.4f),
			new WaitForSeconds(0.45f),
			new WaitForSeconds(0.5f),
			new WaitForSeconds(0.55f),
			new WaitForSeconds(0.6f),
			new WaitForSeconds(0.65f),
			new WaitForSeconds(0.7f),
			new WaitForSeconds(0.75f),
			new WaitForSeconds(0.8f),
			new WaitForSeconds(0.85f),
			new WaitForSeconds(0.9f),
			new WaitForSeconds(0.95f),
			new WaitForSeconds(1f)
		};
	}
}
