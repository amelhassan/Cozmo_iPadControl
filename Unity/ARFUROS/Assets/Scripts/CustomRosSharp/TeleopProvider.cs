using UnityEngine;
using System;
using rosapi = RosSharp.RosBridgeClient.Services.RosApi;

namespace RosSharp.RosBridgeClient
{
    public class TeleopProvider : ServiceProvider<rosapi.GetParamRequest, rosapi.GetParamResponse>
    {
        public Type MessageType { get { return (typeof(GeometryTwist)); } }
        private GeometryTwist rawMessage;

        private void Awake()
        {
            MessageReception += ReceiveMessage;
        }

        protected bool ServiceResponseHandler(rosapi.GetParamRequest request, out rosapi.GetParamResponse response){
        	
        }

        private void UpdateValues()
        {
            rawMessage.linear.x = 2.2;
            rawMessage.linear.y = 0.0;
            rawMessage.linear.z = 0.0;
            rawMessage.angular.x = 0.0;
            rawMessage.angular.y = 0.0;
            rawMessage.angular.z = 0.0;

        }
    }
}
