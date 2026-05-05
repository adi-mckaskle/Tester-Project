using UnityEngine;
public class GroceryEnd : MonoBehaviour
{
       private ItemChecker itemChecker;

   void Start()
    {
        itemChecker = GetComponent<ItemChecker>();
    }
   

    // Update is called once per frame
    void Update()
    {

        if(!itemChecker) return;

       if (itemChecker.score >= 30)
        {
            transform.position = new Vector3(20f, 0f, 0f);
        }
       
        
    }
}
