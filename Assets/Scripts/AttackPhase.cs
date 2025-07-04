using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhase : MonoBehaviour
{
	[SerializeField] GameObject m_targetCursorPrefab;     // ターゲットカーソルのプレハブ
	private GameObject m_targetCursor;		// 今誰を狙っているか
	[SerializeField] List<GameObject> m_enemyList;  // 敵のリスト
	private bool m_isChangeScale;	// スケールを変更するフラグ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(m_enemyList != null)
		{	
			// 敵の数だけ繰り返す
			for(int i = 0; i < m_enemyList.Count; i++)
			{
				
			}
		}
    }
	
	public void SetCursor()
	{
		// カーソルを設定する
		m_targetCursor = Instantiate(m_targetCursorPrefab, m_enemyList[0].transform.position, Quaternion.identity);
	}

	public void ChangeScale()
	{

	}
}
