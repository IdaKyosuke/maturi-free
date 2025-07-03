using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class CharacterAnim : MonoBehaviour
{
	private Animator m_anim;

	private Vector3 m_startPos;	// 移動開始時の場所
	private Vector3 m_goalPos;    // 移動の目標地点
	private bool m_canMove; // 移動できるか

	private float m_duration = 0;	// 経過時間計測用
	private float m_moveTime = 0;   // 移動にかかる時間

	[SerializeField] float m_diffY; // 高さの補正（必要なら付ける）
	[SerializeField] int m_attackTypeAmount;	// 攻撃の種類数

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

	// ----- 共通の処理 -----
	public void SetRun(bool isRun)
	{
		// 走るアニメーション
		m_anim.SetBool("run", isRun);
	}

	public void GetHit()
	{
		// ダメージを受けるアニメーション
		m_anim.SetTrigger("getHit");
	}

	public void Death()
	{
		// 死亡アニメーション
		m_anim.SetTrigger("death");
	}

	// ----- 攻撃アニメーション -----
	public void Attack(int attackNum)
	{
		m_anim.SetTrigger("attack" + attackNum);
	}

	// 現在の座標が戦う場所かどうか
	public bool IsBattlePos()
	{
		return transform.position == m_goalPos;
	}

	// 移動する
	public void Move(Transform startPos, Transform goalPos, float moveTime)
	{
		// スタート地点とゴール地点を決める
		m_startPos = startPos.position;
		m_goalPos = goalPos.position;

		m_startPos.y += m_diffY;
		m_goalPos.y += m_diffY;

		// 移動時間を設定する
		m_moveTime = moveTime;

		// 移動フラグを立てる
		m_canMove = true;

		SetRun(true);
	}

	// 攻撃のパターン数を返す
	public int GetAttackTypeAmount()
	{
		return m_attackTypeAmount;
	}
}
