using UnityEngine;

namespace com
{
    public class KeepOffset : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] bool _startSync = true;
        [SerializeField] bool _updateSync = true;
        Vector3 _offset;

        void Start()
        {
            _offset = transform.position - target.position;
            if (_startSync)
                Sync();
        }

        public void Sync()
        {
            transform.position = target.position + _offset;
        }

        private void Update()
        {
            if (_updateSync) Sync();
        }
    }
}