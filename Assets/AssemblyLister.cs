using UnityEditor;
using UnityEditor.Compilation;

public static class AssemblyLister
{

    [MenuItem("Tools/List Player Assemblies in Console")]
    public static void PrintAssemblyNames()

    {
        UnityEngine.Debug.Log("== Player Assemblies ==");
        Assembly[] playerAssemblies =
            CompilationPipeline.GetAssemblies(AssembliesType.Player);

        foreach (var assembly in playerAssemblies)
        {
            UnityEngine.Debug.Log(assembly.name);
        }
    }
}