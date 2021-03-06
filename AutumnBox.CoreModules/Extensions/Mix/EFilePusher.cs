﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("推送文件到手机主目录", "en-us:Push file to device")]
    [ExtIcon("Icons.filepush.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron | DeviceState.Recovery)]
    [ExtText("title","Select a file", "zh-cn:选择一个文件")]
    [ExtText("filter", "Any file", "zh-cn:任意文件(*.*)|*.*")]
    internal class EFilePusher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, IClassTextDictionary text)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Show();
                bool? dialogResult = null;
                string seleFile = null;
                ui.RunOnUIThread(() =>
                {
                    Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                    fileDialog.Reset();
                    fileDialog.Title = text["title"];
                    fileDialog.Filter = text["filter"];
                    fileDialog.Multiselect = false;
                    dialogResult = fileDialog.ShowDialog();
                    seleFile = fileDialog.FileName;
                });
                if (dialogResult != true) ui.EShutdown();
                FileInfo fileInfo = new FileInfo(seleFile);
                CommandExecutor executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteLine(e.Text);
                var result = executor.Adb(device,$"push \"{fileInfo.FullName}\" \"/sdcard/{fileInfo.Name}\"");
                ui.Finish(result.ExitCode);
            }
        }
    }
}
