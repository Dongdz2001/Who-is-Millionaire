using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using bt11;

namespace bt1
{
    [Serializable]
    public class Btn_message
    {
        public int Number_Question;
        public string Question_content;
        public string A;
        public string B;
        public string C;
        public string D;
        public string Correct_Answer;
    }

    public enum GameState
    {
        Home,
        Gameplay,
        Gameover,
        Win
    }
    public class SSA : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp_goal;
        [SerializeField] private TextMeshProUGUI tmp_number_Question;
        [SerializeField] private TextMeshProUGUI tmp_question_content;
        [SerializeField] private TextMeshProUGUI tmp_A;
        [SerializeField] private TextMeshProUGUI tmp_B;
        [SerializeField] private TextMeshProUGUI tmp_C;
        [SerializeField] private TextMeshProUGUI tmp_D;
        // [SerializeField] private Btn_message[] btn_mess;
        [SerializeField] private QuestionDataScript[] questionDatas_script;
        [SerializeField] private Image img_A;
        [SerializeField] private Image img_B;
        [SerializeField] private Image img_C;
        [SerializeField] private Image img_D;
        [SerializeField] private GameObject m_HomePanel, m_GamePanel, m_GameOverPanel, m_WinPanel;

        [SerializeField] private AudioSource m_AudioSource;
        [SerializeField] private AudioClip m_MusicCorrect;
        [SerializeField] private AudioClip m_MusicWrong;
        [SerializeField] private AudioClip m_MusicMain;
        [SerializeField] private Sprite Image_Green;
        [SerializeField] private Sprite Image_Yellow;
        [SerializeField] private Sprite Image_Black;
        [SerializeField] private Sprite m_LostHp;
        [SerializeField] private Sprite m_Hp;
        private bool tsl = true;
        private int count;
        private bool block;
        private int point = 0;
        public int m_live = 3;
        // Start is called before the first frame update
        private GameState m_game_state;
        void Start()
        {
            // tmp_number_Question.text = "Question " + btn_mess.Number_Question;
            // tmp_question_content.text = btn_mess.Question_content;
            // tmp_A.text = btn_mess.A;
            // tmp_B.text = btn_mess.B;
            // tmp_C.text = btn_mess.C;
            // tmp_D.text = btn_mess.D;
           
            SetGameState(GameState.Home);
            count = 0;
            tmp_goal.text = "Điểm:" + point;
            InitQuestion(0);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Press_Notifical(string noti)
        {
            if (tsl)
            {
                tsl = false;
                bool check = false;

                if (questionDatas_script[count].Correct_Answer == noti)
                {
                    m_AudioSource.PlayOneShot(m_MusicCorrect);
                    Debug.Log("Correct!!");
                    check = true;
                    point++;
                    tmp_goal.text = "Điểm: " + point;
                }
                else
                {
                    m_live--;
                    m_AudioSource.PlayOneShot(m_MusicWrong);
                    Debug.Log("Wrong!!");
                    if (m_live == 0)
                    {

                        Invoke("setLose", 1f);
                    }

                }
                Change_Color(noti);
                if (count >= questionDatas_script.Length - 1)
                {
                    if (m_live > 0)
                    {
                        Invoke("setWin", 1f);
                        Debug.Log("Win !!");
                    }
                    else
                    {
                        m_AudioSource.PlayOneShot(m_MusicWrong);
                        Debug.Log("Lose !!");
                        Invoke("setLose", 1f);
                    }
                }
                else
                {
                    Invoke("Next", 1f);
                }
            }
        }
        public void Next()
        {
            count++;
            InitQuestion(count);
            tsl = true;
        }
        public void setWin()
        {
            SetGameState(GameState.Win);
        }
        public void setLose()
        {
            SetGameState(GameState.Gameover);
        }
        public void Change_Color(String input)
        {
            bool check = false;
            if (input == questionDatas_script[count].Correct_Answer)
            {
                check = true;
            }
            switch (input)
            {
                case "a":
                    img_A.sprite = check ? Image_Green : Image_Yellow;
                    // img_A.color = check ? Color.green : Color.red;
                    break;
                case "b":
                    img_B.sprite = check ? Image_Green : Image_Yellow;
                    // img_B.color = check ? Color.green : Color.red;
                    break;
                case "c":
                    img_C.sprite = check ? Image_Green : Image_Yellow;
                    // img_C.color = check ? Color.green : Color.red;
                    break;
                case "d":
                    img_D.sprite = check ? Image_Green : Image_Yellow;
                    // img_D.color = check ? Color.green : Color.red;
                    break;
            }
        }
        public void InitQuestion(int pIndex)
        {
            if (pIndex < 0 || pIndex >= questionDatas_script.Length)
            {
                return;
            }

            img_A.sprite = Image_Black;
            img_B.sprite = Image_Black;
            img_C.sprite = Image_Black;
            img_D.sprite = Image_Black;
            tmp_number_Question.text = "Question " + questionDatas_script[pIndex].Number_Question;
            tmp_question_content.text = questionDatas_script[pIndex].Question_content;
            tmp_A.text = "A: " + questionDatas_script[pIndex].A;
            tmp_B.text = "B: " + questionDatas_script[pIndex].B;
            tmp_C.text = "C: " + questionDatas_script[pIndex].C;
            tmp_D.text = "D: " + questionDatas_script[pIndex].D;
        }
        public void SetGameState(GameState state)
        {
            m_game_state = state;
            m_live = 3;
            m_HomePanel.SetActive(m_game_state == GameState.Home);
            m_GamePanel.SetActive(m_game_state == GameState.Gameplay);
            m_GameOverPanel.SetActive(m_game_state == GameState.Gameover);
            m_WinPanel.SetActive(m_game_state == GameState.Win);
        }
        public void Btn_Play_Pressed()
        {
            m_live = 3;
            SetGameState(GameState.Gameplay);
            count = 0;
            point = 0;
            tmp_goal.text = "Điểm: " + point;
            InitQuestion(0);
            m_AudioSource.clip = m_MusicMain;
            m_AudioSource.Play();

        }
        public void Btn_Home_Pressed()
        {
            m_AudioSource.Stop();
            SetGameState(GameState.Home);
            tsl = true;
        }
        public void Btn_Win_Pressed()
        {
            m_AudioSource.Stop();
            SetGameState(GameState.Home);
            tsl = true;
        }
        public QuestionDataScript[] GetQuestionDataScripts(){
            return questionDatas_script;
        }
    }
}