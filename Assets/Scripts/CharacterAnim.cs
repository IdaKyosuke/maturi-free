using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class CharacterAnim : MonoBehaviour
{
	private Animator m_anim;

	private Vector3 m_startPos;	// �ړ��J�n���̏ꏊ
	private Vector3 m_goalPos;    // �ړ��̖ڕW�n�_
	private bool m_canMove; // �ړ��ł��邩

	private float m_duration = 0;	// �o�ߎ��Ԍv���p
	private float m_moveTime = 0;   // �ړ��ɂ����鎞��

	[SerializeField] float m_diffY; // �����̕␳�i�K�v�Ȃ�t����j
	[SerializeField] int m_attackTypeAmount;	// �U���̎�ސ�

	private void Start()
	{
		m_anim = GetComponent<Animator>();
		m_canMove = false;
	}

	private void Update()
	{
		if(m_canMove)
		{
			m_duration += Time.deltaTime;
			float t = m_duration / m_moveTime;
			if(t >= 1.0f)
			{
				t = 1.0f;
				m_duration = 0;
				m_canMove = false;
				SetRun(false);	
			}

			transform.position = Vector3.Lerp(m_startPos, m_goalPos, t);
		}
	}

	// ----- ���ʂ̏��� -----
	public void SetRun(bool isRun)
	{
		// ����A�j���[�V����
		m_anim.SetBool("run", isRun);
	}

	public void GetHit()
	{
		// �_���[�W���󂯂�A�j���[�V����
		m_anim.SetTrigger("getHit");
	}

	public void Death()
	{
		// ���S�A�j���[�V����
		m_anim.SetTrigger("death");
	}

	// ----- �U���A�j���[�V���� -----
	public void Attack(int attackNum)
	{
		m_anim.SetTrigger("attack" + attackNum);
	}

	// ���݂̍��W���키�ꏊ���ǂ���
	public bool IsBattlePos()
	{
		return transform.position == m_goalPos;
	}

	// �ړ�����
	public void Move(Transform startPos, Transform goalPos, float moveTime)
	{
		// �X�^�[�g�n�_�ƃS�[���n�_�����߂�
		m_startPos = startPos.position;
		m_goalPos = goalPos.position;

		m_startPos.y += m_diffY;
		m_goalPos.y += m_diffY;

		// �ړ����Ԃ�ݒ肷��
		m_moveTime = moveTime;

		// �ړ��t���O�𗧂Ă�
		m_canMove = true;

		SetRun(true);
	}

	// �U���̃p�^�[������Ԃ�
	public int GetAttackTypeAmount()
	{
		return m_attackTypeAmount;
	}
}
