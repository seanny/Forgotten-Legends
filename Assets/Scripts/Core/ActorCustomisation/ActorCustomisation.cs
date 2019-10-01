//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using UnityEngine;

namespace Core.ActorCustomisation
{
    public class ActorCustomisation : Singleton<ActorCustomisation>
    {
        public GameObject target;
        public string suffixMax = "Max";
        public string suffixMin = "Min";

        private SkinnedMeshRenderer m_SkinnedMeshRenderer;
        private Mesh m_Mesh;
        private Dictionary<string, BlendShape> m_BlendShapes;

        // Use this for initialization
        private void Start()
        {
            SetupBlendShapes();
            Initialise();
        }

        private void SetupBlendShapes()
        {
            m_BlendShapes = new Dictionary<string, BlendShape>();
        }

        private void Initialise()
        {
            m_SkinnedMeshRenderer = target.GetComponentInChildren<SkinnedMeshRenderer>();
            m_Mesh = m_SkinnedMeshRenderer.sharedMesh;
        }

        private void ParseBlendSHapesToDictionary()
        {
            List<string> blendShapesNames = Enumerable
                .Range(0, m_Mesh.blendShapeCount)
                .Select(x => m_Mesh.GetBlendShapeName(x)).ToList();

            for (int i = 0; blendShapesNames.Count > 0;)
            {
                string altSuffix, noSuffix;
                //Removes the max and min suffixes 
                noSuffix = blendShapesNames[i].TrimEnd(suffixMax.ToCharArray()).TrimEnd(suffixMin.ToCharArray()).Trim();

                string positiveName = string.Empty, negativeName = string.Empty;
                bool exists = false;

                int postiveIndex = -1, negativeIndex = -1;

                //If Suffix is Postive
                if (blendShapesNames[i].EndsWith(suffixMax))
                {
                    altSuffix = noSuffix + " " + suffixMin;

                    positiveName = blendShapesNames[i];
                    negativeName = altSuffix;

                    if (blendShapesNames.Contains(altSuffix)) exists = true;

                    postiveIndex = m_Mesh.GetBlendShapeIndex(positiveName);

                    if (exists)
                        negativeIndex = m_Mesh.GetBlendShapeIndex(altSuffix);
                }

                //If Suffix is Negative
                else if (blendShapesNames[i].EndsWith(suffixMin))
                {
                    altSuffix = noSuffix + " " + suffixMax;

                    negativeName = blendShapesNames[i];
                    positiveName = altSuffix;

                    if (blendShapesNames.Contains(altSuffix)) exists = true;

                    negativeIndex = m_Mesh.GetBlendShapeIndex(negativeName);

                    if (exists)
                        postiveIndex = m_Mesh.GetBlendShapeIndex(altSuffix);
                }

                //Doesn't have a suffix
                else
                {
                    postiveIndex = m_Mesh.GetBlendShapeIndex(blendShapesNames[i]);

                    //This is here so it will remove it (for condition) so its not infinite loop
                    positiveName = noSuffix;
                }


                if (m_BlendShapes.ContainsKey(noSuffix))
                    Logging.LogError(noSuffix + " already exists within the Database!");

                m_BlendShapes.Add(noSuffix, new BlendShape(postiveIndex, negativeIndex));


                //Remove Selected Indexes From the List
                if (positiveName != string.Empty) blendShapesNames.Remove(positiveName);
                if (negativeName != string.Empty) blendShapesNames.Remove(negativeName);
            }
        }

        //Get all registered Blendshapes names without suffixes (The Dictionary Keys)
        public string[] GetBlendShapeNames()
        {
            return m_BlendShapes.Keys.ToArray();
        }

        public int GetNumberOfEntries()
        {
            return m_BlendShapes.Count;
        }

        public BlendShape GetBlendshape(string name)
        {
            return m_BlendShapes[name];
        }

        //Use for editor to check if the Target has been changed so needs to update accordingly
        public bool DoesTargetMatchSkmr()
        {
            return (target == m_SkinnedMeshRenderer) ? true : false;
        }

        public void ClearDatabase()
        {
            m_BlendShapes.Clear();
        }
    }
}
