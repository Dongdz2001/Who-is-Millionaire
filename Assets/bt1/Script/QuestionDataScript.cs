using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bt11
{

    [CreateAssetMenu(fileName = "QuestionDataScript")]
    public class QuestionDataScript : ScriptableObject
    {
        
        public int Number_Question;
        public string Question_content;
        public string A;
        public string B;
        public string C;
        public string D;
        public string Correct_Answer;
        // Start is called before the first frame update
    }
}

