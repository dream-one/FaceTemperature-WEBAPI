using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Web;

namespace FaceServer.Models {
    public class WebSocketViewModel {
        /// <summary>
        /// 设备WebSocket连接
        /// </summary>
        public IWebSocketConnection socket { get; set; }
        /// <summary>
        /// 设备系列号
        /// </summary>
        public string deviceSerial { get; set; }

        public string IP { get; set; }
    }
}