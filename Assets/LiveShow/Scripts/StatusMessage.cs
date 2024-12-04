using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusMessage : MonoBehaviour
{
     TextMeshProUGUI text;
    public int messageDuration = 3;
    public DateTime hideTime = DateTime.Now;
    public bool isUp = false;
    public static bool isMessageShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        SceneActManager.Instance.onMessage.AddListener(UpdateMessage);
        
    }

    private void UpdateMessage(string arg0)
    {
        text.SetText(arg0);
        isUp = true;
        hideTime = DateTime.Now.AddSeconds(messageDuration);
    }
   
    // Update is called once per frame
    void Update()
    {
        //if (isUp) {
        //if(DateTime.Now > hideTime)
        //{
        //    text.SetText("");
        //    isUp = false;
        //}
        //}
    }
}
