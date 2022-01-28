using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class AiState
    {
        public List<AiState> myConnections;
        public virtual void OnEnter(TheAi ai) {}
        public virtual void Exicute(TheAi ai) {}
        public virtual void OnExit(TheAi ai) {}
    }
}