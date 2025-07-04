using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class StartBattle : MonoBehaviour
{
	[SerializeField] GameObject[] m_players;    // 味方
	[SerializeField] float m_startInTime = 1.0f;    // 戦闘開始時の入場にかかる時間
	[SerializeField] Transform m_startPos;  // 入場開始時の場所
	[SerializeField] Transform[] m_battlePos;   // 戦闘時の場所
	[SerializeField] SetCommand m_skillManager;
	[SerializeField] GameObject m_attackPhase;	// 攻撃フェーズの管理オブジェクト

	private bool m_isStart = false;

    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < m_players.Length; i++)
		{
			m_players[i].GetComponent<CharacterAnim>().Move(m_startPos, m_battlePos[i], m_startInTime);
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(!m_isStart)
		{
			int count = 0;
			for(int i = 0; i < m_players.Length; i++)
			{
				// 全員が場所につくまで待つ
				if(m_players[i].GetComponent<CharacterAnim>().IsBattlePos())
				{
					count++;
				}
			}

			if(count == m_players.Length)
			{
				// 到着したら戦闘開始
				m_isStart = true;
				m_attackPhase.SetActive(false);
				m_skillManager.FirstSet();
			}
		}
    }
}
