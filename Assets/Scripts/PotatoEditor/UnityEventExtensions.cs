using UnityEditor.Events;
using UnityEngine.Events;

namespace PotatoEditor
{
    public static class UnityEventExtensions
    {
        public static void RemoveAllPersistentListeners(UnityEventBase unityEvent)
        {
            var numEvents = unityEvent.GetPersistentEventCount();
            while (numEvents > 0)
            {
                UnityEventTools.RemovePersistentListener(unityEvent, 0);
                numEvents--;
            }
        }
    }
}
