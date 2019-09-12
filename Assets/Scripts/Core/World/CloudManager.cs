using UnityEngine;

namespace Core.World
{
    public class CloudManager : Singleton<CloudManager>
    {
        public float execTime;
        public GameObject[] cloudPrefabs;
        public Transform parentTransform;

        private float m_NextActionTime = 0.0f;

        private void Update()
        {
            if (execTime > 0)
            {
                m_NextActionTime += Time.deltaTime;
                if (m_NextActionTime > execTime)
                {
                    int index = Random.Range(0, cloudPrefabs.Length - 1);
                    float _scale = Random.Range(1f, 5f);
                    GameObject _cloud = Instantiate(cloudPrefabs[index].gameObject);

                    _cloud.transform.position = new Vector3(Random.Range(-4000f, 4000f), Random.Range(100f, 500f),
                        Random.Range(-4000f, 4000f));
                    _cloud.transform.localScale = new Vector3(_scale, _scale, _scale);

                    m_NextActionTime = m_NextActionTime - execTime;
                }
            }
            else m_NextActionTime = 0;
        }
    }
}