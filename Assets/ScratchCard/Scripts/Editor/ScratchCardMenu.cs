using ScratchCardAsset.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
#if !UNITY_2021_3_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEngine;

namespace ScratchCardAsset.Editor
{
    public static class ScratchCardMenu
    {
        private static readonly string ScratchCardPrefabGUID = "bfd21db4576fb4dac871b93fdc37924b";

        private static ScratchCardManager CreateScratchCard()
        {
            var prefabPath = AssetDatabase.GUIDToAssetPath(ScratchCardPrefabGUID);
            var prefab = AssetDatabase.LoadAssetAtPath<ScratchCardManager>(prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"Scratch-card prefab not found at: {prefabPath}");
                return null;
            }

            ScratchCardManager instance;
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                instance = PrefabUtility.InstantiatePrefab(prefab, prefabStage.prefabContentsRoot.transform) as ScratchCardManager;
            }
            else
            {
                instance = PrefabUtility.InstantiatePrefab(prefab) as ScratchCardManager;
            }

            if (instance == null)
                return null;

            Undo.RegisterCreatedObjectUndo(instance.gameObject, "Create Scratch Card");
            return instance;
        }

        private static void MarkAsDirty(Component component)
        {
            Selection.activeObject = component.gameObject;
            EditorUtility.SetDirty(component);
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }

        [MenuItem("GameObject/Scratch Card/MeshRenderer", false, 32)]
        private static void CreateMeshRendererScratchCard()
        {
            var scratchCardManager = CreateScratchCard();
            if (scratchCardManager != null)
            {
                scratchCardManager.RenderType = ScratchCardRenderType.MeshRenderer;
                scratchCardManager.TrySelectCard(scratchCardManager.RenderType);
                MarkAsDirty(scratchCardManager);
            }
        }
        
        [MenuItem("GameObject/Scratch Card/SpriteRenderer", false, 33)]
        private static void CreateSpriteRendererScratchCard()
        {
            var scratchCardManager = CreateScratchCard();
            if (scratchCardManager != null)
            {
                scratchCardManager.RenderType = ScratchCardRenderType.SpriteRenderer;
                scratchCardManager.TrySelectCard(scratchCardManager.RenderType);
                MarkAsDirty(scratchCardManager);
            }
        }
        
        [MenuItem("GameObject/Scratch Card/Image", false, 34)]
        private static void CreateImageRendererScratchCard()
        {
            var scratchCardManager = CreateScratchCard();
            if (scratchCardManager != null)
            {
                scratchCardManager.RenderType = ScratchCardRenderType.CanvasRenderer;
                scratchCardManager.TrySelectCard(scratchCardManager.RenderType);
                MarkAsDirty(scratchCardManager);
            }
        }
    }
}