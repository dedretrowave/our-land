using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Helpers
{
    public class ExecutionQueue : MonoBehaviour
    {
        public UnityEvent OnExecutionFinished = new();
        
        private Queue<IEnumerator> _queue = new();
        private Coroutine _queueExecution;
        private bool _executionIsRunning;

        public void Add(IEnumerator func)
        {
            _queue.Enqueue(func);
            
            if (!_executionIsRunning)
            {
                _queueExecution = StartCoroutine(ExecuteQueue());
            }
        }

        public void Pause()
        {
            StopCoroutine(_queueExecution);
        }

        public void Stop()
        {
            Pause();
            _executionIsRunning = false;
            _queueExecution = null;
        }

        public void Continue()
        {
            _queueExecution = StartCoroutine(ExecuteQueue());
        }
        
        private IEnumerator ExecuteQueue()
        {
            _executionIsRunning = true;
            
            while (_queue.Count > 0)
            {
                yield return _queue.Dequeue();
            }
            
            Stop();
            OnExecutionFinished.Invoke();
        }
    }
}