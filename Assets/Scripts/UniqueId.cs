// using UnityEngine;
//  using System.Collections.Generic;
//  using System;
//  
//  #if UNITY_EDITOR
//  using UnityEditor;
//  using UnityEditor.SceneManagement;
//  #endif
//  
//  [ExecuteInEditMode]
//  public class UniqueId : MonoBehaviour 
//  {
//      static Dictionary<string, UniqueId> allGuids = new Dictionary<string, UniqueId> ();
//  
//      public string uniqueId;
//      
//      #if UNITY_EDITOR
//      
//      void Update(){
//          if (Application.isPlaying)
//              return;
//          
//          string sceneName = gameObject.scene.name + "_";
//          
//          if  (sceneName == null) return;
//          
//          bool hasSceneNameAtBeginning = (uniqueId != null && 
//              uniqueId.Length > sceneName.Length && 
//              uniqueId.Substring (0, sceneName.Length) == sceneName);
//          
//          bool anotherComponentAlreadyHasThisID = (uniqueId != null && 
//              allGuids.ContainsKey (uniqueId) && 
//              allGuids [uniqueId] != this);
//  
//          if (!hasSceneNameAtBeginning || anotherComponentAlreadyHasThisID){
//              uniqueId =  sceneName + Guid.NewGuid ();
//              EditorUtility.SetDirty (this);
//              EditorSceneManager.MarkSceneDirty (gameObject.scene);
//          }
//
//          if (!allGuids.ContainsKey (uniqueId)) {
//              allGuids.Add(uniqueId, this);
//          }
//      }
//      
//      void OnDestroy(){
//          allGuids.Remove(uniqueId);
//      }
//      #endif
//  }