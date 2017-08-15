using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    private Teleport instance = null;
    public Transform target;
    private GameObject player;
    private Image Fillimg;
    private SteamVR_Fade stf;
    

    private float acumTime = 0f;
    private float holdTime = 1f;

    string gui = "";

    [System.Serializable]
    public enum TELEPORT_STATE { IDLE, TRIGGERED, TELEPORTING };

    [SerializeField]
    TELEPORT_STATE state = TELEPORT_STATE.IDLE;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(instance);
        }
        player = GameObject.FindGameObjectWithTag("Player"); //center pivot parent
        Fillimg = GameObject.FindGameObjectWithTag("fill").GetComponent<Image>();
        stf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SteamVR_Fade>();
        //Debug.Log("ME \"" + name + "\" found Fillimg \"" + Fillimg.name + "\"");
        //Fillimg.fillAmount = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            
            //player.transform.position = new Vector3(target.position.x, player.transform.position.y, target.position.z);
            state = TELEPORT_STATE.TRIGGERED;
           
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
           
            //player.transform.position = new Vector3(target.position.x, player.transform.position.y, target.position.z);
            state = TELEPORT_STATE.IDLE;
            acumTime = 0;
        }
    }

    private void Update()
    {

        if (state == TELEPORT_STATE.TRIGGERED)
        {
            stf.OnStartFade(Color.black, .7f, true);
            
            Fillimg.color = new Color(1f, 1f, 1f, 1f);
            //Debug.Log("Color full");
            Fillimg.fillAmount = acumTime;
            acumTime += Time.smoothDeltaTime;
            
            if (Fillimg.fillAmount == 1f)
            {

                player.transform.position = new Vector3(target.position.x, player.transform.position.y, target.position.z);
                stf.OnStartFade(Color.clear, .7f, true);
                state = TELEPORT_STATE.IDLE;
                acumTime = 0;
            }
        }

        if (state == TELEPORT_STATE.IDLE)
        {
            Fillimg.color = new Color(.5f, .5f, .5f, .5f);
            stf.OnStartFade(Color.clear, .001f, true);
        }
        
    }

   
}
