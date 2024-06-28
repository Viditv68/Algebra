﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{

    public const string CHECKPOINT = "CheckPoint";
    private const string CORRECTANSWER = "CorrectANswers";

    [SerializeField] private Transform clockTransfrom;


    [SerializeField] private AudioManager audioManager;
    public GameObject scorePanel;

    [Header("Text Questions Info")]
    public Animator questionsPanelAnim;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI textQuestion;
    [SerializeField] private TextMeshProUGUI secondTextQuestion;
    [SerializeField] private List<TextMeshProUGUI> options;



    [Header("Character Info")]
    public RectTransform character;
    public Animator characterAnim;
    public TextMeshProUGUI speechBubbleText;
    public Sprite happySprite;
    public Sprite sadSprite;
    private Vector3 characterStartingPoisition;


    [SerializeField] private ParticleSystem poof;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private VerticalLayoutGroup textLayout;

    public QuestionHandler questionHandler;
    private List<Questions> questions;


    public int correctAnswers = 0;
    int index = 0;
    public bool isTouchEnabled;

    private GameObject clock = null;

    void Start()
    {
        isTouchEnabled = false;
        questions = questionHandler.questions;
        characterStartingPoisition = character.transform.position;
        speechBubbleText.transform.parent.gameObject.SetActive(false);

        index = GetInt(CHECKPOINT);
        correctAnswers = GetInt(CORRECTANSWER);
        GenerateQuestions();
    }

    public void GenerateQuestions()
    {
        Debug.Log(GetInt(CHECKPOINT) + " : " + GetInt(CORRECTANSWER));
        if(index >= questions.Count)
        {
            scorePanel.SetActive(true);
            SetInt(CHECKPOINT, 0);
            SetInt(CORRECTANSWER, 0);
            return;
        }

        questionsPanelAnim.gameObject.SetActive(true);
        questionsPanelAnim.Play("SlideIn");
        if(clock!= null) 
            Destroy(clock.gameObject);
        character.transform.position = characterStartingPoisition;

        Questions ques = questions[index];


        DisplayQuestion(ques);
        DisplayOptions(ques);


    }


    #region[ ============ Not Image Questions ==========]

    private void DisplayQuestion(Questions _ques)
    {
        headerText.text = "Question " + (index + 1);
        textQuestion.text = _ques.question.Substring(0, _ques.question.IndexOf(','));
        secondTextQuestion.text = _ques.question.Substring(_ques.question.IndexOf(',') + 1);

    }


    #endregion




    private void DisplayOptions(Questions _ques)
    {
        if(_ques.isMultipleChoice)
        {
            textLayout.padding.top = 150;
            optionsPanel.SetActive(true);
            inputPanel.SetActive(false);

            for (int i = 0; i < _ques.options.Count; i++)
            {
                options[i].text = _ques.options[i];
            }
        }
        else
        {
            optionsPanel.SetActive(false);
            inputPanel.SetActive(true);
            textLayout.padding.top = 290;
        }



    }


    public void CheckOption(int _index)
    {
        if (!isTouchEnabled)
            return;

        audioManager.PlayAudioClipByKey(SoundKey.ButtonClick);
        isTouchEnabled = false;

        LevelGratificiation(_index);


        //character.DOMoveX(-6, 1f).OnComplete(() =>
        //{
        //    speechBubbleText.transform.parent.gameObject.SetActive(true);

        //    StartCoroutine(MoveCharacterLeft());

        //});

    }

    private void LevelGratificiation(int _index)
    {
        Questions ques = questions[index];
        if (_index == ques.correctOption)
        {
            correctAnswers++;
            speechBubbleText.text = "Great Job!";
            audioManager.PlayAudioClipByKey(SoundKey.LevelWin);
            poof.Play();

        }
        else
        {
            //character.GetComponent<Image>().sprite = sadSprite;
            speechBubbleText.text = "Please try again";
            audioManager.PlayAudioClipByKey(SoundKey.Error);

        }

        if (characterAnim.gameObject.activeInHierarchy)
            characterAnim.Play("CharacterSlideIn");
        else
            characterAnim.gameObject.SetActive(true);

        //speechBubbleText.transform.parent.gameObject.SetActive(true);
        if(ques.isMultipleChoice)
            options[ques.correctOption].GetComponentInParent<Animator>().SetTrigger("PlayBounce");
        characterAnim.Play("CharacterSlideIn");

        StartCoroutine(SlideOut());

        index++;
        if ((index) % questionHandler.checkPoint == 0)
        {
            PlayerPrefs.SetInt(CHECKPOINT, index);
            PlayerPrefs.SetInt(CORRECTANSWER, correctAnswers);
        }
    }

    IEnumerator SlideOut()
    {
        yield return new WaitForSeconds(2f);
        speechBubbleText.transform.parent.gameObject.SetActive(false);
        questionsPanelAnim.Play("SlideOut");
        characterAnim.Play("CharacterSlideOut");
    }


    public void SetInt(string _name, int _val)
    {
        PlayerPrefs.SetInt(_name, _val);
    }

    private int GetInt(string _name)
    {
       return PlayerPrefs.GetInt(_name);
    }

    string answer = "";

    public void GetButtonInput(int value)
    {
        answer = answer + value.ToString();
        Questions ques = questions[index];
        int len = ques.correctOption.ToString().Length;
        secondTextQuestion.text = secondTextQuestion.text + value.ToString();

        if (answer.Length >= len)
        {
            LevelGratificiation(int.Parse(answer));
        }
    }
}
