using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeometryObjectsData", menuName = "GeometryObjectsData")]
public class GeometryObjectsData : ScriptableObject
{
   [SerializeField]
   private List<ClickColorData> _clicksData = new List<ClickColorData>();

   public ClickColorData GetClickData(string objectType)
   {
      for (int i = 0; i < _clicksData.Count; i++)
      {
         if (string.Compare(_clicksData[i].ObjectType, objectType, StringComparison.Ordinal) == 0)
         {
            return _clicksData[i];
         }
      }

      return null;
   }
}
