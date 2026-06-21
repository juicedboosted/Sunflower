using UnityEngine;
using UnityEngine.UI;

public class ScrollBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setting the scroll bar to start at the top with a script because apparently you can't do this in the editor
        //I can't believe this engine is so widely used
        GetComponent<Scrollbar>().value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
