using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using Cysharp.Threading.Tasks;

public class SetCommand : MonoBehaviour
{
	[SerializeField] List<GameObject> m_currentCommands;    // 現在表示されているコマンド
	private List<GameObject> m_selectedSkill;   // 行動予定のスキルのリスト
	[SerializeField] GameObject m_commandParent; 
	[SerializeField] int m_firstNum;    // 最初のコマンドの数
	[SerializeField] int m_maxNum;      // コマンドの最大数
	[SerializeField] GameObject m_commandTemp;	// コマンド用のテンプレート
	[SerializeField] PartyManager m_partyManager;	// パーティー管理オブジェクト 
	private Skill_Info m_info;


	private void Start()
	{
		m_currentCommands = new List<GameObject>();
		m_selectedSkill = new List<GameObject>();
	}

	private void Update()
	{
		CheckCurrentCommands();

		CheckSelectedCommands();
	}

	// ----- スキルの選択候補を生成する -----
	// 最初にコマンドを配置する
	public async void FirstSet()
	{
		Skill_Info info;
		Sprite icon;
		// 現在のパーティーメンバーを取得
		List<GameObject> party = m_partyManager.GetParty();

		for(int i = 0; i < m_firstNum; i++)
		{
			// キャラクターを選択
			GameObject m = SelectMember(party);
			// スキル番号を抽選
			int num = SkillNum(m);

			// コマンド用UIを作成
			GameObject command = Instantiate(m_commandTemp, m_commandParent.transform);
			// スキルの情報を取得（戻り値はAsyncOperationHandle）
			var infoHandle = Addressables.LoadAssetAsync<Skill_Info>("Assets/ScriptableObject/Skill_Info/" + m.name + "/" + num.ToString("00") + ".asset");
			// スキルを使うキャラのアイコン情報を取得
			var iconHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Texture/Icon/"+ m.name +".png");
			// ロード
			info = await infoHandle.Task;
			icon = await iconHandle.Task;
			// スキル情報をセットする
			command.GetComponent<Command>().SetInfo(info, m, icon);
			// リストでコマンド数を管理
			m_currentCommands.Add(command);
		}
	}

	// スキル番号を選択する
	private int SkillNum(GameObject member)
	{
		// キャラクターの攻撃パターンから攻撃を選択
		return UnityEngine.Random.Range(0, member.GetComponent<CharacterAnim>().GetAttackTypeAmount()) + 1;
	}

	// 誰のスキルの抽選をするかを指定する
	private GameObject SelectMember(List<GameObject> members)
	{
		int rand = UnityEngine.Random.Range(0, members.Count);
		return members[rand].gameObject;
	}

	// このターンで実行されるスキルのリストを渡す
	public List<GameObject> SelectedList()
	{
		return m_selectedSkill;
	}

	// ----- コマンドの選択状況を確認 -----
	private void CheckCurrentCommands()
	{
		// 1つ以上コマンドの選択候補が用意されている
		if (m_currentCommands != null)
		{
			// いったんコピー
			List<GameObject> commands = m_currentCommands;
			for (int i = 0; i < m_currentCommands.Count; i++)
			{
				// 選択されたコマンドを見つけたら
				if (commands[i].GetComponent<Command>().IsSelected())
				{
					// 選択されたフラグを折る
					commands[i].GetComponent<Command>().IsChanged();

					// リストを移し替える
					m_selectedSkill.Add(commands[i]);
					commands.Remove(commands[i]);
				}
			}
			// リストの本体にコピー
			m_currentCommands = commands;
		}
	}

	private void CheckSelectedCommands()
	{
		// １つ以上実行候補があるとき
		if (m_selectedSkill != null)
		{
			// いったんコピー
			List<GameObject> commands = m_selectedSkill;
			for (int i = 0; i < m_selectedSkill.Count; i++)
			{
				// 選択されたコマンドを見つけたら
				if (commands[i].GetComponent<Command>().IsSelected())
				{
					// 選択されたフラグを折る
					commands[i].GetComponent<Command>().IsChanged();

					// リストを移し替える
					m_currentCommands.Add(commands[i]);
					commands.Remove(commands[i]);
				}
			}
			// リストの本体にコピー
			m_selectedSkill = commands;
		}
	}
}
