using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using Cysharp.Threading.Tasks;

public class SetCommand : MonoBehaviour
{
	[SerializeField] List<GameObject> m_currentCommands;    // ���ݕ\������Ă���R�}���h
	private List<GameObject> m_selectedSkill;   // �s���\��̃X�L���̃��X�g
	[SerializeField] GameObject m_commandParent; 
	[SerializeField] int m_firstNum;    // �ŏ��̃R�}���h�̐�
	[SerializeField] int m_maxNum;      // �R�}���h�̍ő吔
	[SerializeField] GameObject m_commandTemp;	// �R�}���h�p�̃e���v���[�g
	[SerializeField] PartyManager m_partyManager;	// �p�[�e�B�[�Ǘ��I�u�W�F�N�g 
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

	// ----- �X�L���̑I�����𐶐����� -----
	// �ŏ��ɃR�}���h��z�u����
	public async void FirstSet()
	{
		Skill_Info info;
		Sprite icon;
		// ���݂̃p�[�e�B�[�����o�[���擾
		List<GameObject> party = m_partyManager.GetParty();

		for(int i = 0; i < m_firstNum; i++)
		{
			// �L�����N�^�[��I��
			GameObject m = SelectMember(party);
			// �X�L���ԍ��𒊑I
			int num = SkillNum(m);

			// �R�}���h�pUI���쐬
			GameObject command = Instantiate(m_commandTemp, m_commandParent.transform);
			// �X�L���̏����擾�i�߂�l��AsyncOperationHandle�j
			var infoHandle = Addressables.LoadAssetAsync<Skill_Info>("Assets/ScriptableObject/Skill_Info/" + m.name + "/" + num.ToString("00") + ".asset");
			// �X�L�����g���L�����̃A�C�R�������擾
			var iconHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Texture/Icon/"+ m.name +".png");
			// ���[�h
			info = await infoHandle.Task;
			icon = await iconHandle.Task;
			// �X�L�������Z�b�g����
			command.GetComponent<Command>().SetInfo(info, m, icon);
			// ���X�g�ŃR�}���h�����Ǘ�
			m_currentCommands.Add(command);
		}
	}

	// �X�L���ԍ���I������
	private int SkillNum(GameObject member)
	{
		// �L�����N�^�[�̍U���p�^�[������U����I��
		return UnityEngine.Random.Range(0, member.GetComponent<CharacterAnim>().GetAttackTypeAmount()) + 1;
	}

	// �N�̃X�L���̒��I�����邩���w�肷��
	private GameObject SelectMember(List<GameObject> members)
	{
		int rand = UnityEngine.Random.Range(0, members.Count);
		return members[rand].gameObject;
	}

	// ���̃^�[���Ŏ��s�����X�L���̃��X�g��n��
	public List<GameObject> SelectedList()
	{
		return m_selectedSkill;
	}

	// ----- �R�}���h�̑I���󋵂��m�F -----
	private void CheckCurrentCommands()
	{
		// 1�ȏ�R�}���h�̑I����₪�p�ӂ���Ă���
		if (m_currentCommands != null)
		{
			// ��������R�s�[
			List<GameObject> commands = m_currentCommands;
			for (int i = 0; i < m_currentCommands.Count; i++)
			{
				// �I�����ꂽ�R�}���h����������
				if (commands[i].GetComponent<Command>().IsSelected())
				{
					// �I�����ꂽ�t���O��܂�
					commands[i].GetComponent<Command>().IsChanged();

					// ���X�g���ڂ��ւ���
					m_selectedSkill.Add(commands[i]);
					commands.Remove(commands[i]);
				}
			}
			// ���X�g�̖{�̂ɃR�s�[
			m_currentCommands = commands;
		}
	}

	private void CheckSelectedCommands()
	{
		// �P�ȏ���s��₪����Ƃ�
		if (m_selectedSkill != null)
		{
			// ��������R�s�[
			List<GameObject> commands = m_selectedSkill;
			for (int i = 0; i < m_selectedSkill.Count; i++)
			{
				// �I�����ꂽ�R�}���h����������
				if (commands[i].GetComponent<Command>().IsSelected())
				{
					// �I�����ꂽ�t���O��܂�
					commands[i].GetComponent<Command>().IsChanged();

					// ���X�g���ڂ��ւ���
					m_currentCommands.Add(commands[i]);
					commands.Remove(commands[i]);
				}
			}
			// ���X�g�̖{�̂ɃR�s�[
			m_selectedSkill = commands;
		}
	}
}
