﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    /// <summary>
    /// 拓展模块线程
    /// </summary>
    public interface IExtensionTask
    {
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 启动数据
        /// </summary>
        Dictionary<string, object> Args { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        object Result { get; }
        /// <summary>
        /// 开始
        /// </summary>
        void Start();
        /// <summary>
        /// 检查是否在运行中
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// 杀死
        /// </summary>
        void Kill();
        /// <summary>
        /// 结束线程,并设定返回码
        /// </summary>
        /// <param name="exitCode"></param>
        void Shutdown(int exitCode);
        /// <summary>
        /// 无限期等待,直到停止
        /// </summary>
        void WaitForExit();
        /// <summary>
        /// 执行完毕
        /// </summary>
        event EventHandler<TaskFinishedEventArgs> Finished;
        /// <summary>
        /// 线程开始执行了
        /// </summary>
        event EventHandler<TaskStartedEventArgs> Started;
    }
}
