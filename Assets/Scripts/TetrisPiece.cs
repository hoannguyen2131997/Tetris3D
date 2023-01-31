using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace Spellcast
{
    public class TetrisPiece : MonoBehaviour
    {
        public Color color;
        public Material material;
        public Vector3Int dimension;
        public List<int> values;
        public bool wireFrameMode = false;

        private Material coloredMaterial;
        public Material ColoredMaterial
        {
            get
            {
                if (coloredMaterial == null)
                {
                    coloredMaterial = new Material(material);
                    coloredMaterial.color = color;
                }

                return coloredMaterial;
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            float blockSize = 1;
            Vector3 blockSize3 = Vector3.one * blockSize;
            
            Vector3 d = new Vector3(dimension.x, dimension.y, dimension.z);
            
            //Bounding box
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + (d * blockSize) / 2, d * blockSize);

            //values
            int i = 0;
            for (int x = 0; x < dimension.x; x++)
            {
                for (int y = 0; y < dimension.y; y++)
                {
                    for (int z = 0; z < dimension.z; z++)
                    {
                        //Catch
                        if (values.Count <= i)
                        {
                            values.Add(0);
                        }
                        
                        //Draw cube 
                        if (values[i] == 1)
                        {
                            Vector3 pos = new Vector3(x, y, z);
                            Gizmos.color = color;

                            if (wireFrameMode)
                            {
                                Gizmos.DrawWireCube(transform.position + pos + blockSize3/2, blockSize3 - new Vector3(0.1f, 0.1f, 0.1f));
                            }
                            else
                            {
                                Gizmos.DrawCube(transform.position + pos + blockSize3/2, blockSize3 - new Vector3(0.1f, 0.1f, 0.1f));
                            }
                            
                        }

                        i++;
                    }
                }
            }
            
            //Remove extra values
            int max = dimension.x * dimension.y * dimension.z;
            if (values.Count > dimension.x * dimension.y * dimension.z)
            {
                values.RemoveRange(max, values.Count - max);
            }
            
        }
#endif
    }
}
