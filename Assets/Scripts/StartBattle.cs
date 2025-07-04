using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class StartBattle : MonoBehaviour
{
	[SerializeField] GameObject[] m_players;    // ����
	[SerializeField] float m_startInTime = 1.0f;    // �퓬�J�n���̓���ɂ����鎞��
	[SerializeField] Transform m_startPos;  // ����J�n���̏ꏊ
	[SerializeField] Transform[] m_battlePos;   // �퓬���̏ꏊ
	[SerializeField] SetCommand m_skillManager;
	[SerializeField] GameObject m_attackPhase;	// �U���t�F�[�Y�̊Ǘ��I�u�W�F�N�g

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
				// �S�����ꏊ�ɂ��܂ő҂�
				if(m_players[i].GetComponent<CharacterAnim>().IsBattlePos())
				{
					count++;
				}
			}

			if(count == m_players.Length)
			{
				// ����������퓬�J�n
				m_isStart = true;
				m_attackPhase.SetActive(false);
				m_skillManager.FirstSet();
			}
		}
    }
}
