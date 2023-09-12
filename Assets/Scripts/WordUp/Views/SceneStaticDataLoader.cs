using System;
using System.Collections.Generic;
using System.Linq;

namespace WordUp.Views
{
    /// <summary>
    /// Класс служит, как статическое хранилище для передачи данных между сценами
    /// </summary>
    internal static class SceneStaticDataLoader
    {
        internal class DataHandler
        {
            internal Action<object> LoadData { get; set; }
            
            internal Func<object> GetData { get; set; }
        }

        private static readonly Stack<DataHandler> StackActiveScenesData = new();

        internal static void CloseScene()
        {
            DataHandler closetSceneHandler = StackActiveScenesData.Pop();
            object data = closetSceneHandler.GetData();
            
            DataHandler lastOpenSceneHandler = StackActiveScenesData.Peek();
            lastOpenSceneHandler.LoadData(data);
        }

        internal static void OpenScene(DataHandler sceneDataHandler)
        {
            if (!StackActiveScenesData.Any())
            {
                StackActiveScenesData.Push(sceneDataHandler);
                
                return;
            }
            
            DataHandler lastOpenSceneHandler = StackActiveScenesData.Peek();
            object data = lastOpenSceneHandler.GetData();
            
            sceneDataHandler.LoadData(data);
            StackActiveScenesData.Push(sceneDataHandler);
        }
    }
}