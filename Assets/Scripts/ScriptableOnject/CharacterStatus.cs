using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status", menuName ="ScriptableObject/Create Status")]
public class CharacterStatus : ScriptableObject
{
	[SerializeField] int m_lv;
	[SerializeField] int m_hp; 
	[SerializeField] int m_atk;
	
	public int GetLv()
	{
		return m_lv;
	}

	public int GetHp()
	{
		return m_hp;
	}
	public int GetAtk()
	{
		return m_atk;
	}
}


