using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro.Examples
{
	// Token: 0x02000446 RID: 1094
	public class TMP_TextSelector_A : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06001878 RID: 6264 RVA: 0x00098950 File Offset: 0x00096B50
		private void Awake()
		{
			this.m_TextMeshPro = base.gameObject.GetComponent<TextMeshPro>();
			this.m_Camera = Camera.main;
			this.m_TextMeshPro.ForceMeshUpdate(false, false);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0009897C File Offset: 0x00096B7C
		private void LateUpdate()
		{
			this.m_isHoveringObject = false;
			if (TMP_TextUtilities.IsIntersectingRectTransform(this.m_TextMeshPro.rectTransform, Input.mousePosition, Camera.main))
			{
				this.m_isHoveringObject = true;
			}
			if (this.m_isHoveringObject)
			{
				int num = TMP_TextUtilities.FindIntersectingCharacter(this.m_TextMeshPro, Input.mousePosition, Camera.main, true);
				if (num != -1 && num != this.m_lastCharIndex && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
				{
					this.m_lastCharIndex = num;
					int materialReferenceIndex = this.m_TextMeshPro.textInfo.characterInfo[num].materialReferenceIndex;
					int vertexIndex = this.m_TextMeshPro.textInfo.characterInfo[num].vertexIndex;
					Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), byte.MaxValue);
					Color32[] colors = this.m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
					colors[vertexIndex] = color;
					colors[vertexIndex + 1] = color;
					colors[vertexIndex + 2] = color;
					colors[vertexIndex + 3] = color;
					this.m_TextMeshPro.textInfo.meshInfo[materialReferenceIndex].mesh.colors32 = colors;
				}
				int num2 = TMP_TextUtilities.FindIntersectingLink(this.m_TextMeshPro, Input.mousePosition, this.m_Camera);
				if ((num2 == -1 && this.m_selectedLink != -1) || num2 != this.m_selectedLink)
				{
					this.m_selectedLink = -1;
				}
				if (num2 != -1 && num2 != this.m_selectedLink)
				{
					this.m_selectedLink = num2;
					TMP_LinkInfo tmp_LinkInfo = this.m_TextMeshPro.textInfo.linkInfo[num2];
					Vector3 vector;
					RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_TextMeshPro.rectTransform, Input.mousePosition, this.m_Camera, out vector);
					string linkID = tmp_LinkInfo.GetLinkID();
					if (!(linkID == "id_01"))
					{
						linkID == "id_02";
					}
				}
				int num3 = TMP_TextUtilities.FindIntersectingWord(this.m_TextMeshPro, Input.mousePosition, Camera.main);
				if (num3 != -1 && num3 != this.m_lastWordIndex)
				{
					this.m_lastWordIndex = num3;
					TMP_WordInfo tmp_WordInfo = this.m_TextMeshPro.textInfo.wordInfo[num3];
					Vector3 position = this.m_TextMeshPro.transform.TransformPoint(this.m_TextMeshPro.textInfo.characterInfo[tmp_WordInfo.firstCharacterIndex].bottomLeft);
					position = Camera.main.WorldToScreenPoint(position);
					Color32[] colors2 = this.m_TextMeshPro.textInfo.meshInfo[0].colors32;
					Color32 color2 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), byte.MaxValue);
					for (int i = 0; i < tmp_WordInfo.characterCount; i++)
					{
						int vertexIndex2 = this.m_TextMeshPro.textInfo.characterInfo[tmp_WordInfo.firstCharacterIndex + i].vertexIndex;
						colors2[vertexIndex2] = color2;
						colors2[vertexIndex2 + 1] = color2;
						colors2[vertexIndex2 + 2] = color2;
						colors2[vertexIndex2 + 3] = color2;
					}
					this.m_TextMeshPro.mesh.colors32 = colors2;
				}
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00098CE1 File Offset: 0x00096EE1
		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log("OnPointerEnter()");
			this.m_isHoveringObject = true;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00098CF4 File Offset: 0x00096EF4
		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log("OnPointerExit()");
			this.m_isHoveringObject = false;
		}

		// Token: 0x040017E4 RID: 6116
		private TextMeshPro m_TextMeshPro;

		// Token: 0x040017E5 RID: 6117
		private Camera m_Camera;

		// Token: 0x040017E6 RID: 6118
		private bool m_isHoveringObject;

		// Token: 0x040017E7 RID: 6119
		private int m_selectedLink = -1;

		// Token: 0x040017E8 RID: 6120
		private int m_lastCharIndex = -1;

		// Token: 0x040017E9 RID: 6121
		private int m_lastWordIndex = -1;
	}
}
