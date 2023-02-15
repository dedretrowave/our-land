using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Src.Helpers
{
    public class ExecutionQueue : MonoBehaviour
    {
        private Queue<IEnumerator> _queue = new();
        private Coroutine _queueExecution;
        private IEnumerator _current;

        public IEnumerator Current => _current;

        public void Add(IEnumerator func)
        {
            _queue.Enqueue(func);
            
            _queueExecution ??= StartCoroutine(ExecuteQueue());
        }

        public void Pause()
        {
            StopCoroutine(_queueExecution);
        }

        public void Continue()
        {
            _queueExecution = StartCoroutine(ExecuteQueue());
        }
        
        private IEnumerator ExecuteQueue()
        {
            while (_queue.Count > 0)
            {
                yield return _queue.Dequeue();
            }
            
            StopCoroutine(_queueExecution);
            _queueExecution = null;
        }
    }
}