using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/QuestionData")]
public class QuestionHandler : ScriptableObject
{
    public int checkPoint;
    public List<Questions> questions;
    
}


[Serializable]
public class Questions
{
    public bool isMultipleChoice;
    public string question;
    public int correctOption;
    public List<string> options;
}