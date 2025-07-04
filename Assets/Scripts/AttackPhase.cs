using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhase : MonoBehaviour
{
	[SerializeField] GameObject m_targetCursorPrefab;     // �^�[�Q�b�g�J�[�\���̃v���n�u
	private GameObject m_targetCursor;		// ���N��_���Ă��邩
	[SerializeField] List<GameObject> m_enemyList;  // �G�̃��X�g
	private bool m_isChangeScale;	// �X�P�[����ύX����t���O

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(m_enemyList != null)
		{	
			// �G�̐������J��Ԃ�
			for(int i = 0; i < m_enemyList.Count; i++)
			{
				
			}
		}
    }
	
	public void SetCursor()
	{
		// �J�[�\����ݒ肷��
		m_targetCursor = Instantiate(m_targetCursorPrefab, m_enemyList[0].transform.position, Quaternion.identity);
	}

	public void ChangeScale()
	{

	}
}
