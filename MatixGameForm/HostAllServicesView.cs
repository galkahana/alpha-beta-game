using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace MatixGameForm
{
    public partial class HostAllServicesView : Form
    {
        private ServiceHost mGameServiceHost;
        private ServiceHost mPingableServiceHost;
        private ServiceHost mAddressesRecieverHost;

        public HostAllServicesView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mGameServiceHost = new ServiceHost(typeof(SharedGameService));
            mPingableServiceHost = new ServiceHost(typeof(PingableService));
            mAddressesRecieverHost = new ServiceHost(typeof(RemoteGameCollectorService));

            mGameServiceHost.Open();
            mPingableServiceHost.Open();
            mAddressesRecieverHost.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mGameServiceHost.Close();
            mPingableServiceHost.Close();
            mAddressesRecieverHost.Close();

            mGameServiceHost = null;
            mPingableServiceHost = null;
            mAddressesRecieverHost = null;
        }
    }
}