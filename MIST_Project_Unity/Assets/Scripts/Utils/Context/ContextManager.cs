using System;
using System.Collections.Generic;

namespace MistProject.Utils.Context
{
    public class ContextManager
    {
        public event Action OnContextUpdated; 

        private static ContextManager _instance;
        
        public static ContextManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContextManager();
                }

                return _instance;
            }
        }
        
        private Dictionary<Type, ContextBase> _contextBindings;

        public void BindContext<T>(T context) where T : ContextBase
        {
            if (_contextBindings == null)
            {
                _contextBindings = new Dictionary<Type, ContextBase>();
            }
            _contextBindings[typeof(T)] = context;
            
            OnContextUpdated?.Invoke();
        }

        public bool TryGetContext<T>(out T context) where T : ContextBase
        {
            context = null;
            if (_contextBindings.TryGetValue(typeof(T), out var getContext))
            {
                context = (T) getContext;
                return true;
            }

            return false;
        }

        public bool RemoveContext<T>() where T : ContextBase
        {
            throw new NotImplementedException($"{nameof(RemoveContext)} isn't implemented");
        }
    }
}