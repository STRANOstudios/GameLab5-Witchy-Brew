using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;

public class ComponentSelectionPopup : EditorWindow
{
    private string[] availableComponents = { "BrightnessController" };
    private int selectedIndex = -1;
    private GraphicsSettings graphicsSettings; // Aggiungi questo riferimento

    [MenuItem("Tools/Add Component to GraphicsSettings")]
    static void Init()
    {
        ComponentSelectionPopup window = (ComponentSelectionPopup)EditorWindow.GetWindow(typeof(ComponentSelectionPopup));
        window.graphicsSettings = GameObject.FindObjectOfType<GraphicsSettings>(); // Trova l'istanza di GraphicsSettings
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Select Component to Add", EditorStyles.boldLabel);
        selectedIndex = EditorGUILayout.Popup("Component", selectedIndex, availableComponents);

        if (GUILayout.Button("Add Component"))
        {
            if (selectedIndex >= 0 && selectedIndex < availableComponents.Length)
            {
                string selectedComponent = availableComponents[selectedIndex];
                AddComponentToGraphicsSettings(selectedComponent); // Chiamare il metodo per aggiungere il componente a GraphicsSettings
            }
            else
            {
                Debug.LogError("No component selected.");
            }
        }
    }

    void AddComponentToGraphicsSettings(string componentName)
    {
        Type componentType = Type.GetType(componentName);
        if (componentType != null && graphicsSettings != null)
        {
            graphicsSettings.gameObject.AddComponent(componentType); // Aggiungi il componente a GraphicsSettings
            Debug.Log("Added " + componentName + " to GraphicsSettings.");
        }
        else
        {
            Debug.LogError("Failed to find component type or GraphicsSettings is null.");
        }
    }
}
