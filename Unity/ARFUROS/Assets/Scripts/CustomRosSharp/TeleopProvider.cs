using UnityEngine;
using System;
using rosapi = RosSharp.RosBridgeClient.Services.RosApi;

namespace RosSharp.RosBridgeClient
{
    public class TeleopProvider : MessageProvider
    {
       public override Type MessageType { get { return (typeof(StandardString)); } }
       private StandardString message;

        private void Start()
        {
            InitializeMessage();
        }
        private void FixedUpdate(){
                UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new StandardString();
        }

        private void UpdateMessage()
        {
            message.data = "Patience is a virtue";
            RaiseMessageRelease(new MessageEventArgs(message));
        }

    }
}
