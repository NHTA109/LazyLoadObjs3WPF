using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace ConfirmLazyloadDatainS3toWpf
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //connect awsS3
        static AmazonS3Client s3Client = new AmazonS3Client("AKIARB6SPDVBFDRYX4PZ", "4Pp/m6eFu3QEw0pQHlgvmZf1uvGitZyB5Cdwpy9C", Amazon.RegionEndpoint.APSoutheast1);


        public MainWindow()
        {
            InitializeComponent();
        }



        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            // event lazy load 
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                try
                {

                }
                catch { }
            }
        }

        private TreeViewItem CreateTreeItem(object o)
        {
            //model item for treeview
            TreeViewItem item = new TreeViewItem();
            item.Header = o.ToString();
            item.Tag = o;
            item.Items.Add("Loading...");
            return item;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load bucket first you start
            ListBucketsResponse buckets = await s3Client.ListBucketsAsync();
            foreach (var bucket in buckets.Buckets)
            {
                var namebk = bucket.BucketName;
                trvStructure.Items.Add(CreateTreeItem(namebk));
            }
        }
    }
}
