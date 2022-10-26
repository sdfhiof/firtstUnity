using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 ������ũ��Ʈ�� �޶������� �ؿ��޶��� �κи��� ��������� �Ŵ��� ��ũ��Ʈ�� �޶������� �̰��� ����
�ϴ� �� ��ư�� ��� ���ڸ� ���������Ƿ� ���ؽ�Ʈ���� �Ŵ����� �ۺ����� ����� ������ �ν�����â����
�־������ ���̶�Űâ���� �ؽ�Ʈ���� �������� �ϴµ�  �̺κ��� �׳ɳ��� ������ ��°�� �ѱ����
���� �������� ���ؼ� ���������� �Ŵ�����ũ��Ʈ�� int�� 6������� 10�� 1�����־������ �׸��� 
shop ��ũ��Ʈ�� �ν�����â���� �� talk data�� 3���� �ø��� ��� �������� �ؽ�Ʈ���־������
�Ŵ�����ũ��Ʈ�� endstage�� ���ڸ��߰��� �����������Ḧ �ν��ؾ��ؼ� �Ŵ������߰��������
 */
public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    // �����������ڰ� �ٲ�� �Ͱ����� �۵������� ���� ������ �ؽ�Ʈ��������
    public Text heartText;
    public Text ammoText;
    public Text grenadeText;
    public Text hammerText;
    public Text handText;
    public Text subText;
    public Text talkText;

    /* �������� ���ؼ� itemobj��6���δø��� �Ŵ�����ũ��Ʈ�� �����ǳ��� ������
     * shop ��ũ��Ʈ������ ���Խ� �� �������� �����س��� ���� 6���δø� itemobj�� �迭�� ���ڷ� �������� �Ǻ��س�*/
    Player enterPlayer;
    public GameManager manager;
    public GameObject[] itemObj;
    public GameObject gamePanel;

    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;

    // ��� 0�̉����� �����ϱ����� ���� 0�̵Ǹ� ��� 0 �ٵ� �� ������ ��� ���ε��� �Ǻ��� ���ؼ� �������ɼ� ����  
    int Inventorymanager = 1;


    public void Enter(Player player)
    {
        endStage();
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }


    // ������������ ���������� ���� �ؽ�Ʈ���ʱ�ȭ���� enter�Լ��� �Լ��� ������
    public void endStage()
    {
        if(manager.endStage == 1)
        {
            manager.heartNumber = 10;
            manager.ammoNumber = 10;
            manager.grenadeNumber = 10;
            manager.hammerNumber = 1;
            manager.handNumber = 1;
            manager.subNumber = 1;
            heartText.text = manager.heartNumber + " / 10";
            ammoText.text = manager.ammoNumber + " / 10";
            grenadeText.text = manager.grenadeNumber + " / 10";
            hammerText.text = manager.hammerNumber + " / 1";
            handText.text = manager.handNumber + " / 1";
            subText.text = manager.subNumber + " / 1";
        }
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        
        

        if (price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        // ������ �Լ�
        Inventory(index);

        // ���� ��� 0���� �����ϸ� buy��ü�� Ż���ϱ����� return�� �Լ� �̰Ծ����� ������ Ŭ���� �� ������
        if (Inventorymanager == 0)
        {
            Inventorymanager = 1;
            return;
        }

        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos[2].position + ranVec, itemPos[2].rotation);
    }


    // shop ��ũ��Ʈ������ ���Խ� �� �������� �����س��� ���� 6���δø� itemobj�� �迭�� ���ڷ� �������� �Ǻ��س�
    //case�� �� �������� �ε����� �޾� �����ϰ� Ŭ������ �ؽ�Ʈ�� �������� 
    // ���� �Ŵ��� ��ũ��Ʈ�� �� ������ ��� ����Ǿ��ִµ� �װ� �ϳ��� �����
    public void Inventory(int index)
    {
        switch (index)
        {
            
            case 0:
                if (manager.heartNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.heartNumber--;
                heartText.text = manager.heartNumber + " / 10";
                break;
            case 1:
                if (manager.ammoNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.ammoNumber--;
                ammoText.text = manager.ammoNumber + " / 10";
                break;
            case 2:
                if (manager.grenadeNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.grenadeNumber--;
                grenadeText.text = manager.grenadeNumber + " / 10";
                break;
            case 3:
                if (manager.hammerNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.hammerNumber--;
                hammerText.text = manager.hammerNumber + " / 1";
                break;
            case 4:
                if (manager.handNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.handNumber--;
                handText.text = manager.handNumber + " / 1";
                break;
            case 5:
                if (manager.subNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.subNumber--;
                subText.text = manager.subNumber + " / 1";
                break;
        }
     }

    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }

    IEnumerator Talk2()
    {
            talkText.text = talkData[2];
            yield return new WaitForSeconds(2f);
            talkText.text = talkData[0];
    }

    }
