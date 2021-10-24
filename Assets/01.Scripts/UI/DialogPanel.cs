using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogPanel : MonoBehaviour
{
    private List<TextVO> list;
    private RectTransform panel; //��ȭ â �г�

    public TMP_Text dialogText; // �ؽ�Ʈ�Ž� ������ ���̾�α� â
    private WaitForSeconds shortWs = new WaitForSeconds(0.2f); //���ڰ� ������ �ӵ�

    private bool clickToNext = false; // ���� ��ȭ�� �ѱ�� ���� Ŭ���� ��Ÿ���°�?
    private bool isOpen = false; //��ȭâ�� ���ȴ°�?

    public GameObject nextIcon; //�������� �ѱ�� ������
    public GameObject typeEffectParticle; //Ÿ���� ����Ʈ ��ƼŬ
    public Image profileImage; //������
    public AudioClip typeClip; //Ÿ�����ϴ� �Ҹ�

    private int currentIndex; //���� ��ȭ �ε���
    private RectTransform textTransform; //�ؽ�Ʈ â�� ũ��

    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>();

    private System.Action endDialogCallback = null;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        textTransform = dialogText.GetComponent<RectTransform>();
    }

    public void StartDialog(List<TextVO> list, System.Action callback = null)
    {
        endDialogCallback = callback;
        this.list = list;
        ShowDialog();
    }

    public void ShowDialog()
    {
        currentIndex = 0;
        GameManager.TimeScale = 0f;

        panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
        {
            

            TypeIt(list[currentIndex]);
            isOpen = true;
        });
    }

    public void TypeIt(TextVO vo)
    {
        int idx = vo.icon;
        //�̹��� ��ųʸ����� �̹����� ã�ƴٰ� �����ִ� ������ ������ ��.

        if (!imageDictionary.ContainsKey(idx))
        {
            Sprite img = Resources.Load<Sprite>($"profile{idx}");
            imageDictionary.Add(idx, img);
        }

        profileImage.sprite = imageDictionary[idx];

        dialogText.text = vo.msg;
        nextIcon.SetActive(false);
        clickToNext = false;
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        dialogText.ForceMeshUpdate(); //�̰� �ؽ�Ʈ ����
        dialogText.maxVisibleCharacters = 0;
        // 20����
        int totalVisibleChar = dialogText.textInfo.characterCount; //������ �ؽ�Ʈ�� ���� �� ��ü
        for(int i = 1; i <= totalVisibleChar; i++)
        {
            dialogText.maxVisibleCharacters = i;

            //Vector3 pos = dialogText.textInfo.characterInfo[i - 1].bottomRight;
            //Vector3 tPos = textTransform.TransformPoint(pos);

            //������� 

            if (clickToNext)
            {
                dialogText.maxVisibleCharacters = totalVisibleChar;
                break;
            }
            yield return shortWs;
        }
        //������� �Դٸ� �Ѱ��� �ؽ�Ʈ�� ����Ȱ�
        currentIndex++;
        clickToNext = true;
        nextIcon.SetActive(true);
    }

    private void Update()
    {
        if (!isOpen) return;

        //�ؽ�Ʈ �ϳ��� �� ����Ǿ��� �����̽� Ű�� ������쿡 �ش�
        if(Input.GetButtonDown("Jump") && clickToNext)
        {
            if(currentIndex >= list.Count)
            {
                panel.DOScale(new Vector3(0, 0, 1), 0.8f).OnComplete(() =>
                 {
                     // ���ӸŴ����� �ð� ��������� ������ ��
                     GameManager.TimeScale = 1f;
                     isOpen = false;
                     endDialogCallback?.Invoke();
                     endDialogCallback = null;
                 });
            }else
            {
                TypeIt(list[currentIndex]);
            }
        }else if(Input.GetButtonDown("Jump"))
        {
            clickToNext = true;
        }
    }
}
