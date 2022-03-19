using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
namespace bt2
{
    [Serializable]
    public class Btn_message
    {
        public string btn_message;
    }
    public class NewBehaviourScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp_input;
        [SerializeField] private Btn_message btn_mess;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Press_Notifical(string noti)
        {
            if (btn_mess.btn_message == noti)
            {
                Debug.Log("Peocessing...");
            }
            else
            {
                Debug.Log("Completed!!");
            }
        }
    }
}
