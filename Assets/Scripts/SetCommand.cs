using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using Cysharp.Threading.Tasks;

public class SetCommand : MonoBehaviour
{
	[SerializeField] List<GameObject> m_currentCommands;    // 現在表示されているコマンド
	[SerializeField] GameObject m_commandParent; 
	[SerializeField] int m_firstNum;    // 最初のコマンドの数
	[SerializeField] int m_maxNum;      // コマンドの最大数
	[SerializeField] GameObject m_commandTemp;
	[SerializeField] GameObject m_character;

	private Skill_Info info;

	// 最初にコマンドを配置する
	public async UniTask FirstSet()
	{
		for(int i = 0; i < m_firstNum; i++)
		{
			// コマンド用UIを作成
			GameObject command = Instantiate(m_commandTemp, m_commandParent.transform);
			// スキルの情報を取得
			info = await Addressables.LoadAssetAsync<Skill_Info>("ScriptableObject/Skill_Info/Knight_101.asset");
			command.GetComponent<Command>().SetInfo(info, m_character);
			// リストでコマンド数を管理
			m_currentCommands.Add(command);
		}
	}

	// スキル番号を選択する
	private int SkillNum(List<GameObject> party)
	{
		// パーティーからランダムにキャラクターを選択
		int random = UnityEngine.Random.Range(0, party.Count);
		// キャラクターの攻撃パターンから攻撃を選択
		return UnityEngine.Random.Range(0, party[random].GetComponent<CharacterAnim>().GetAttackTypeAmount());
	}
}
