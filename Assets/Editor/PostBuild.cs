using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
    public static class PostBuild
    {
        [PostProcessBuild]
        public static void OnPostBuild(BuildTarget target, string pathToBuiltProject)
        {
            string pathToBuiltDirectory =
                File.GetAttributes(pathToBuiltProject).HasFlag(FileAttributes.Directory)
                    ? pathToBuiltProject
                    : Directory.GetParent(pathToBuiltProject)!.ToString();
            using StreamWriter writer = new StreamWriter(Path.Combine(pathToBuiltDirectory, "README.md"));
            writer.Write("Build date time: ");
            writer.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Debug.Log($"build location: {pathToBuiltProject}");
        }
    }
}