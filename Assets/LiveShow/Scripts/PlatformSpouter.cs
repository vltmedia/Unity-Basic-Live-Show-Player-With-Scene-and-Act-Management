#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX

using Klak.Syphon;

#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using Klak.Spout;

#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpouter : MonoBehaviour
{
    public GameObject spout;
    public GameObject syphon;
    public RenderTexture renderTexture;
    public string SpoutName = "BoxesShow";
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
 //SyphonServer serverItem = gameObject.AddComponent<SyphonServer>();
 //       serverItem.sourceTexture = renderTexture;
        Instantiate(syphon);

#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

        Instantiate(spout);
        //SpoutSender spoutSender = gameObject.AddComponent<SpoutSender>();
        //spoutSender.sourceTexture = renderTexture;
        //spoutSender.spoutName = SpoutName;
        //spoutSender.captureMethod = CaptureMethod.Texture;
#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
