using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using Cysharp.Threading.Tasks;

public class SetCommand : MonoBehaviour
{
	[SerializeField] List<GameObject> m_currentCommands;    // ���ݕ\������Ă���R�}���h
	[SerializeField] GameObject m_commandParent; 
	[SerializeField] int m_firstNum;    // �ŏ��̃R�}���h�̐�
	[SerializeField] int m_maxNum;      // �R�}���h�̍ő吔
	[SerializeField] GameObject m_commandTemp;
	[SerializeField] GameObject m_character;

	private Skill_Info info;

	// �ŏ��ɃR�}���h��z�u����
	public async UniTask FirstSet()
	{
		for(int i = 0; i < m_firstNum; i++)
		{
			// �R�}���h�pUI���쐬
			GameObject command = Instantiate(m_commandTemp, m_commandParent.transform);
			// �X�L���̏����擾
			info = await Addressables.LoadAssetAsync<Skill_Info>("ScriptableObject/Skill_Info/Knight_101.asset");
			command.GetComponent<Command>().SetInfo(info, m_character);
			// ���X�g�ŃR�}���h�����Ǘ�
			m_currentCommands.Add(command);
		}
	}

	// �X�L���ԍ���I������
	private int SkillNum(List<GameObject> party)
	{
		// �p�[�e�B�[���烉���_���ɃL�����N�^�[��I��
		int random = UnityEngine.Random.Range(0, party.Count);
		// �L�����N�^�[�̍U���p�^�[������U����I��
		return UnityEngine.Random.Range(0, party[random].GetComponent<CharacterAnim>().GetAttackTypeAmount());
	}
}
