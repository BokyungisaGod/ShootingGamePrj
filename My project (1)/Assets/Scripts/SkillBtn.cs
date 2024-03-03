using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SkillBtn : MonoBehaviour
{
    public Image skillFilter; 
    public Text coolTimeCounter; //���� ��Ÿ���� ǥ���� �ؽ�Ʈ     
    public float coolTime;
    private float currentCoolTime; //���� ��Ÿ���� ���� �� ����
    private bool canUseSkill = true; //��ų�� ����� �� �ִ��� Ȯ���ϴ� ����
    public GameObject player;
    public string skillName;
    public bool isMachine = false;
    public Image boom;
    AudioManager audiomanager;
    Animator anim;
    void Start()
    {
        skillFilter.fillAmount = 0; //ó���� ��ų ��ư�� ������ ����
        if(skillName=="A")
            anim= boom.GetComponent<Animator>();
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnSkillA()
    {
        Player playerLogic = player.GetComponent<Player>();
        //isMachine = !isMachine;
        //if (isMachine)
        //{
        //    playerLogic.maxShotDelay = 0.05f;
        //}
        //else
        //{
        //    playerLogic.maxShotDelay = playerLogic.defaultShotDelay;
        //}
        Debug.Log("��");
        playerLogic.Boom();
    }
    public void OnSkillB()
    {
        
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.skillStar();
    }

    public void UseSkillA()
    {
        if (canUseSkill && player.activeSelf)
        {
            anim.SetTrigger("On");
            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1; //��ų ��ư�� ����            
            StartCoroutine("Cooltime");
            currentCoolTime = coolTime;
            //coolTimeCounter.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");
            canUseSkill = false; //��ų�� ����ϸ� ����� �� ���� ���·� �ٲ�
            Player playerLogic = player.GetComponent<Player>();
            Invoke("OnSkillA", 1f);
        }
        else
        {
            Debug.Log("���� ��ų�� ����� �� �����ϴ�.");
        }
    }
    public void UseSkillB()
    {
        Player playerLogic = player.GetComponent<Player>();
        if (canUseSkill && playerLogic.isRespawnTime == false && player.activeSelf)
        {
            anim = player.GetComponent<Animator>();
            anim.SetTrigger("On");
            audiomanager.PlaySFX(audiomanager.star);
            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1; //��ų ��ư�� ����            
            StartCoroutine("Cooltime");
            currentCoolTime = coolTime;
            //coolTimeCounter.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");
            canUseSkill = false; //��ų�� ����ϸ� ����� �� ���� ���·� �ٲ�
            
            playerLogic.skillStar();
            Invoke("OnSkillB", 5);
        }
        else
        {
            Debug.Log("���� ��ų�� ����� �� �����ϴ�.");
        }
    }
    IEnumerator Cooltime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        canUseSkill = true; //��ų ��Ÿ���� ������ ��ų�� ����� �� �ִ� ���·� �ٲ�
        yield break;
    }     //���� ��Ÿ���� ����� �ڸ�ƾ�� ������ݴϴ�.    
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCoolTime -= 1.0f;
            //coolTimeCounter.text = "" + currentCoolTime;        
        }
        yield break;
    }
}
