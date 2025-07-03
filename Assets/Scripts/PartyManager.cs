using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour 
{
	[SerializeField] List<GameObject> m_party;  // 現在のパーティーメンバー
	[SerializeField] int m_maxPartyNum; 

	public void Add(GameObject member)
	{
		if (m_party.Count >= m_maxPartyNum) return;
		m_party.Add(member);
	}

	// パーティーメンバーを取得
	public List<GameObject> GetParty()
	{
		return m_party;
	}
}
