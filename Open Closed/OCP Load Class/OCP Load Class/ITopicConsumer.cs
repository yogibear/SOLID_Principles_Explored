using System;
using System.Collections.Generic;
using System.Text;

namespace OCP_Load_Class
{

    interface ITopicConsumer
    {
        string GetName();
        void RunConsumer();
    }
}
