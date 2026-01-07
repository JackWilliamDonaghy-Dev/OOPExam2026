using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startData();
        }

        ObservableCollection<Robot> robots = new ObservableCollection<Robot>();

        public void startData()
        {
            tbckRobotInformation.Text = "";
            lbxRobots.ItemsSource = robots; // change to filter using radio buttons

            //They are all out of battery haha (on purpose) ((no AA batteries included in purchase :)))
            HouseholdRobot rb1 = new HouseholdRobot("HouseBot");
            HouseholdRobot rb2 = new HouseholdRobot("GardenBot");
            HouseholdRobot rb3 = new HouseholdRobot("Housemate 3000");
            DeliveryRobot rb4 = new DeliveryRobot("DeliverBot", DeliveryMode.Walking, 50);
            DeliveryRobot rb5 = new DeliveryRobot("FlyBot", DeliveryMode.Flying, 20);
            DeliveryRobot rb6 = new DeliveryRobot("Driver", DeliveryMode.Driving, 250);

            rb2.DownloadSkill(HouseholdSkill.Gardening);
            rb3.DownloadSkill(HouseholdSkill.Cooking);
            rb3.DownloadSkill(HouseholdSkill.Laundry);

            robots.Add(rb1);
            robots.Add(rb2);
            robots.Add(rb3);
            robots.Add(rb4);
            robots.Add(rb5);
            robots.Add(rb6);
        }


        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            
            if (rb.Name == "rbtnHouseRobots")
            {
                var householdRobots = new ObservableCollection<Robot>();
                foreach (var robot in robots)
                {
                    if (robot is HouseholdRobot)
                    {
                        householdRobots.Add(robot);
                    }
                }
                lbxRobots.ItemsSource = householdRobots;
            }
            else if (rb.Name == "rbtnDriverRobots")
            {
                var deliveryRobots = new ObservableCollection<Robot>();
                foreach (var robot in robots)
                {
                    if (robot is DeliveryRobot)
                    {
                        deliveryRobots.Add(robot);
                    }
                }
                lbxRobots.ItemsSource = deliveryRobots;
            }
            else
            {
                lbxRobots.ItemsSource = robots;
            }
        }
           

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxRobots.SelectedItem is Robot selectedRobot)
            {
                tbckRobotInformation.Text = selectedRobot.RobotInfo();
            }
        }

        private void btnCharge_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRobots.SelectedItem is Robot selectedRobot)
            {
                if (selectedRobot.CurrentPowerKWH >= selectedRobot.PowerCapacityKWH)
                {
                    MessageBox.Show("Robot is already fully charged.");
                    return;
                }
                else
                {
                    selectedRobot.CurrentPowerKWH = selectedRobot.PowerCapacityKWH;
                    MessageBox.Show("Robot fully charged. Time taken: 3 hours and 20 minutes.");
                    return;
                }
            }
            
        }
    }
}