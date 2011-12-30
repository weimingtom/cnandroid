using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidBox.Model
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id;
        /// <summary>
        /// 日志类型 ，如 Android中文API、Android开发者指南、系统通知
        /// </summary>
        public string Type;
        /// <summary>
        /// 日志内容，如TabActivity、
        /// </summary>
        public string Message;
        /// <summary>
        /// 日志内容链接
        /// </summary>
        public string Src;
        /// <summary>
        /// 发布日期
        /// </summary>
        public string PublishDate;
        /// <summary>
        /// 阅读状态 0 未读 1 已读
        /// </summary>
        public string State;

        //public Log()
        //{

        //}

        //public Log(
    }
}


//CREATE TABLE "log" (
//    "id" INTEGER PRIMARY KEY AUTOINCREMENT,
//    "type" TEXT,
//     "message" TEXT,
//     "src" INTEGER,
//    "date" TEXT
//)