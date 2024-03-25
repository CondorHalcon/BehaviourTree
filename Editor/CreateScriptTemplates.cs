using UnityEditor;

namespace CondorHalcon.BehaviourTree.Editor
{
    public static class CreateScriptTemplates
    {
        [MenuItem("Assets/Create/BehaviourTree/NodeAction")]
        public static void CreateNodeActionMenuItem()
        {
            string templatePath = $"Assets/ScriptTemplates/03-C# Templates__Class-NewClass.cs";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewNodeAction.cs");
        }
    }
}
