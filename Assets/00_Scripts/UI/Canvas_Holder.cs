using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Canvas_Holder : MonoBehaviour
{
   [SerializeField] private GameObject Board;
   [SerializeField] private Image BoardHpFill, BoardWhiteHpFill;
   Coroutine F_Coroutine;
   
   public static Canvas_Holder instance = null;

   private void Awake()
   {
      if (instance == null) instance = this;
        
   }
   private void Start()
   {
      Delegate_Holder.OnInteraction += GetBoard;
      Delegate_Holder.OnInteractionOut += BoardOut;
   }

   public void GetBoard()
   {
      Board.SetActive(true);

   }
   
   public void BoardOut() => Board.GetComponent<UI_Animation_Handler>().AnimationChange("Out");

   public void BoardFill(float hp, float Maxhp)
   {
      BoardHpFill.fillAmount = hp / Maxhp;
      if (F_Coroutine != null)
      {
         StopCoroutine(F_Coroutine);
      }

      F_Coroutine = StartCoroutine((FillCoroutine()));
   }

   IEnumerator FillCoroutine()
   {
      while (BoardWhiteHpFill.fillAmount > BoardHpFill.fillAmount)
      {
         BoardWhiteHpFill.fillAmount = Mathf.Lerp(BoardWhiteHpFill.fillAmount, BoardHpFill.fillAmount, Time.deltaTime * 2.0f);
         
         yield return null;
      }
   }
}
