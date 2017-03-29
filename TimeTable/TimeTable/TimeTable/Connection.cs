using System;
using System.Collections.Generic;
using System.Text;

using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace TimeTable
{
    class Connection
    {
        public Connection()
        {
            
        }

        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }

        public void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e) // обработка изменения состояния подключения
        {
            CheckConnection();
        }
    }
}
