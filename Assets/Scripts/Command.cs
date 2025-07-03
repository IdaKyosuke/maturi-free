using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
	// �L�����N�^�[�̏��
	[SerializeField] Skill_Info m_info;   // ���s����U���ԍ�
	[SerializeField] GameObject m_character;    // �U������L����

	// ����\������p��UI
	[SerializeField] UnityEngine.UI.Image m_icon;
	[SerializeField] TextMeshProUGUI m_cost; 
	[SerializeField] TextMeshProUGUI m_skillName; 

	// �X�L���𐶐����ɑ���֐�
	public void SetInfo(Skill_Info info, GameObject chara)
	{
		m_character = chara;
		m_info = info;

		// ����ݒ�
		m_icon.sprite = m_info.GetCharaIcon();
		m_cost.SetText("{0}", m_info.GetCost());
		m_skillName.SetText(m_info.GetSkillName());
	}

	public void OnClick()
	{
		// �U���A�j���[�V�����𓮂���
		m_character.GetComponent<CharacterAnim>().Attack(m_info.GetAttackNum());

		Destroy(this.gameObject);
	}
}
