using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill", menuName ="ScriptableObject/Create Skill")]
public class Skill_Info : ScriptableObject
{
	[SerializeField] string m_skillName;
	[SerializeField] int m_cost;
	[SerializeField] int m_attackNum;
	[SerializeField] Sprite m_charaIcon;

	public string GetSkillName()
	{
		return m_skillName;
	}

	public int GetCost()
	{
		return m_cost;
	}

	public int GetAttackNum()
	{
		return m_attackNum;
	}

	public Sprite GetCharaIcon()
	{
		return m_charaIcon;
	}
}
