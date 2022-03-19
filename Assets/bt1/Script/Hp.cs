using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace bt11
{
    public class Hp : MonoBehaviour
    {
        [SerializeField] private Image[] m_Images;
        [SerializeField] private Sprite m_LostHp;
        [SerializeField] private Sprite m_Hp;
        private bool tsl = true;
        private int count = 0;
        private int t = 0;

        // Start is called before the first frame update
        private QuestionDataScript[] questionDatas_script;
        private bt1.SSA ssa ;
        void Start()
        {
            // for (int i = 0; i < 3; i++)
            //     {
            //         m_Images[i].sprite = m_LostHp;
            //     }
            ssa = FindObjectOfType<bt1.SSA>();
            questionDatas_script  = ssa.GetQuestionDataScripts();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CheckHp(string noti)
        {

            if (tsl)
            {
                tsl = false;
                if (!questionDatas_script[t].Correct_Answer.ToLower().Equals(noti))
                {
                    m_Images[2 - count].sprite = m_LostHp;
                    count++;
                }
                if (t + 1 == questionDatas_script.Length || count== 3)
                {
                    count = 0;
                    t = -1;
                    Invoke("SetDefault", 1f); 
                }
                Invoke("SetTSL", 1f);
                 Debug.Log("Count= "+count);     
                Debug.Log("T= "+t);
            }
        }
        private void SetTSL()
        {
            tsl = true;
            t++;
        }
        private void SetDefault(){
            for (int i = 0; i < 3; i++)
                {
                    m_Images[i].sprite = m_Hp;
                }
        }
    }
}