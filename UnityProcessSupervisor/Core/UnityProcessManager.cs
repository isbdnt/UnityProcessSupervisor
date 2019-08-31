using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProcessSupervisor;

namespace UnityProcessSupervisor {
    public class UnityProcessManager {
        static readonly ProcessStartInfo UNITY_PROCESS_START_INFO = new ProcessStartInfo();
        public List<UnityProjectInfo> UnityProjectInfos { get; } = new List<UnityProjectInfo>();
        public List<UnityProcessInfo> UnityProcessInfos { get; } = new List<UnityProcessInfo>();

        public void UpdateUnityProjectInfos() {
            UnityProjectInfos.Clear();

            var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            string[] registryPathsToCheck = new string[] { @"SOFTWARE\Unity Technologies\Unity Editor 5.x", @"SOFTWARE\Unity Technologies\Unity Editor 4.x", @"SOFTWARE\Unity Technologies\Unity Editor 2017.x", @"SOFTWARE\Unity Technologies\Unity Editor 2018.x", @"SOFTWARE\Unity Technologies\Unity Editor 2019.x" };

            // check each version path
            for (int i = 0, len = registryPathsToCheck.Length; i < len; i++) {
                RegistryKey key = hklm.OpenSubKey(registryPathsToCheck[i]);
                if (key == null) {
                    continue;
                } else {
                    //Console.WriteLine("Null registry key at " + registryPathsToCheck[i]);
                }

                // parse recent project path
                foreach (var valueName in key.GetValueNames()) {
                    if (valueName.IndexOf("RecentlyUsedProjectPaths-") == 0) {
                        string projectPath = "";
                        // check if binary or not
                        var valueKind = key.GetValueKind(valueName);
                        if (valueKind == RegistryValueKind.Binary) {
                            byte[] projectPathBytes = (byte[])key.GetValue(valueName);
                            projectPath = Encoding.UTF8.GetString(projectPathBytes, 0, projectPathBytes.Length - 1);
                        } else // should be string then
                          {
                            projectPath = (string)key.GetValue(valueName);
                        }

                        // first check if whole folder exists, if not, skip
                        if (Directory.Exists(projectPath) == false) {
                            //Console.WriteLine("Recent project directory not found, skipping: " + projectPath);
                            continue;
                        }

                        string projectName = "";

                        // get project name from full path
                        if (projectPath.IndexOf(Path.DirectorySeparatorChar) > -1) {
                            projectName = projectPath.Substring(projectPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        } else if (projectPath.IndexOf(Path.AltDirectorySeparatorChar) > -1) {
                            projectName = projectPath.Substring(projectPath.LastIndexOf(Path.AltDirectorySeparatorChar) + 1);
                        } else // no path separator found
                          {
                            projectName = projectPath;
                        }

                        UnityProjectInfos.Add(new UnityProjectInfo() {
                            projectName = projectName,
                            projectPath = projectPath,
                        });
                    }
                }
            }
        }

        public void UpdateUnityProcessInfos() {
            UnityProcessInfos.Clear();
            foreach (var projectInfo in UnityProjectInfos) {
                var lockFile = $"{projectInfo.projectPath}/Temp/UnityLockfile";
                if (File.Exists(lockFile)) {
                    var lockers = FileUtil.WhoIsLocking(lockFile);
                    if (lockers.Count > 0) {
                        foreach (var locker in lockers) {
                            if (locker.ProcessName == "Unity") {
                                var processInfo = new UnityProcessInfo() {
                                    project = projectInfo,
                                    process = lockers[0]
                                };
                                if (processInfo.process.Responding == false) {
                                    processInfo.project.projectName += "(未响应)";
                                }
                                UnityProcessInfos.Add(processInfo);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void RestartUnityProcess(UnityProcessInfo unityProcessInfo) {
            if (UnityProcessInfos.Remove(unityProcessInfo)) {
                unityProcessInfo.process.Kill();
                UNITY_PROCESS_START_INFO.FileName = unityProcessInfo.process.MainModule.FileName;
                UNITY_PROCESS_START_INFO.Arguments = $"-projectPath \"{unityProcessInfo.project.projectPath}\"";
                Process.Start(UNITY_PROCESS_START_INFO);
            }
        }

        public void CloseUnityProcess(UnityProcessInfo unityProcessInfo) {
            if (UnityProcessInfos.Remove(unityProcessInfo)) {
                unityProcessInfo.process.Kill();
            }
        }
    }

}
