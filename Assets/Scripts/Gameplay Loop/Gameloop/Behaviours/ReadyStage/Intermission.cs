using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrojanMouse.GameplayLoop{ 
    public class Intermission : GLNode{
        public float maxDuration, remainingDuration;
        public Image imageUI;
        TextMeshProUGUI label;
        public Intermission(float duration, Image imageUI = null, TextMeshProUGUI label = null){ // CONSTRUCTOR TO PREDEFINE THIS CLASS VARIABLES
            this.maxDuration = duration;
            this.remainingDuration = duration;
            
            this.imageUI = imageUI;
            this.label = label;
        }

        public override NodeState Evaluate(){
            #region DEPENDANCY MANAGEMENT
            if (imageUI){
                imageUI.fillAmount = (remainingDuration / maxDuration); // FILLS THE IMAGE BASED ON THE REMAINING DURATION
            }
            if(label){
                label.text = Mathf.Ceil(remainingDuration).ToString(); // CONVERTS THE REMAINING DURATION VALUE TO A STRING FOR THE TEXT LABEL
            }
            #endregion
            if (remainingDuration <= 0){
                imageUI?.transform.parent.gameObject.SetActive(false); // DISABLES THE TIMER
                return NodeState.SUCCESS;
            }

            imageUI?.transform.parent.gameObject.SetActive(true); // IF IMAGEUI EXISTS, WILL ENABLE IT
            remainingDuration -= Time.deltaTime; // DEDUCTS TIME FROM THE REMAINING DURATION
            return NodeState.FAILURE;
        }
    }
}