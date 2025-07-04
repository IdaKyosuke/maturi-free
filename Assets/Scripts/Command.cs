using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
	// キャラクターの情報
	[SerializeField] Skill_Info m_info;   // 実行する攻撃番号
	[SerializeField] GameObject m_character;    // 攻撃するキャラ

	// 情報を表示する用のUI
	[SerializeField] UnityEngine.UI.Image m_icon;
	[SerializeField] TextMeshProUGUI m_cost; 
	[SerializeField] TextMeshProUGUI m_skillName;

	// 今自分は選択されているかどうか
	private bool m_isSelected = false;

	// スキルを生成時に走る関数
	public void SetInfo(Skill_Info info, GameObject chara, Sprite icon)
	{
		m_character = chara;
		m_info = info;

		// 情報を設定
		m_icon.sprite = icon;
		m_cost.SetText("{0}", m_info.GetCost());
		m_skillName.SetText(m_info.GetSkillName());
	}

	public void OnClick()
	{
		// 攻撃アニメーションを動かす
		m_character.GetComponent<CharacterAnim>().Attack(m_info.GetAttackNum());
		m_isSelected = true;
	}

	public bool IsSelected()
	{
		return m_isSelected;
	}

	// リストを移し替えたら選択されたフラグを折る
	public void IsChanged()
	{
		m_isSelected = false;
	}
}
