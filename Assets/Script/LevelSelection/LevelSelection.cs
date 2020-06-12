using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
   //[SerializeField] private bool locked = true;//Default value is false;
   public GameObject lockImage;
   //public GameObject ButtonImage;
   private int LevelId;
   private bool locked;
   private Button btn;
   void Awake()
   {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
   }

   private void Start()
   {

   }

    void update()
   {
      
   }
   public void Init(int id,bool Lock)
   {
        LevelId = id;
        locked = Lock;
        if(Lock)//MARKER if unclock is false means This level is clocked!
        {
            lockImage.SetActive(true);
            btn.interactable = false;
            //ButtonImage.SetActive(true);
            //todo 
        }
        else//if unlock is true means This level can play !
        {
            lockImage.SetActive(false);
            btn.interactable = true;
            //ButtonImage.SetActive(false);

        }
   }

   /*private void Selected(bool locked)
   {
       if(!locked)
       {
          SceneManager.LoadScene(LevelId);
       }
       else{
           return;
       }
   }*/
     private void OnClick()
    {
        //场景加载，进入关卡
        //确保BuildSetting中的场景编号没有问题
        if(!locked)
        {
             SceneManager.LoadScene(LevelId);
        }
        else
        {
            return;
        }
    }


}
