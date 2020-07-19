using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
    public class ParamField : MonoBehaviour, ISelectHandler
    {
        public GameObject Value;
        public void OnSelect(BaseEventData eventData)//Where text selected it send the text field information to then Master(Keyboard)
        {
            Master cantrolMaster = GameObject.Find("Master").GetComponent<Master>();
            cantrolMaster.SetIt(Value);
        }
    }
