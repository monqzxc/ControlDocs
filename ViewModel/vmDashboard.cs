using DocsControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static DocsControl.Model.Modules;


namespace DocsControl.ViewModel
{
    public partial class vmDashboard : UserControl
    {
        public vmDashboard(string Nickname)
        {
            InitializeComponent();
            lblWelcome.Content = string.Format("WELCOME {0}!", Nickname.Split('|')[1]);
        
            this.DataContext = this;
        }
        dbDocs db = new dbDocs();
        private string ForSign;
        private string Signed;
        private string Received;
        private ObservableCollection<DashboardData> _OutgoingData = new ObservableCollection<DashboardData>();
     
        public ObservableCollection<DashboardData> OutgoingData
        {
            get
            {
                var db = new dbDocs();
                var outGoingList = new ObservableCollection<DashboardData>();
                var status = new string[] { "FOR SIGNATURE", "SIGNED", "RECEIVED" };
                foreach (var item in status)
                {
                    int today = 0;
                    int yesterday = 0;
                    int week = 0;
                    var fsList = new List<string>();
                    var statList = db.DocDatas.Where(x => x.Tag.Equals("O") && x.CurrentStatus.Equals(item));
                    var newDateNow = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        00, 00, 00);
                    if (item.Equals("FOR SIGNATURE"))
                    {
                        for (int i = 0; i < statList.Count(); i++)
                        {
                            fsList.Add(statList.Select(x => x.ForSigned.ToString()).OrderBy(x => x).Skip(i).FirstOrDefault());
                        }
                        foreach (var signeDate in fsList)
                        {
                            var newSignDate = new DateTime(
                                DateTime.Parse(signeDate).Year,
                                DateTime.Parse(signeDate).Month,
                                DateTime.Parse(signeDate).Day,
                                00, 00, 00);

                            var countDate = (newDateNow.Subtract(newSignDate).TotalDays);
                            if (countDate == 0)
                            {
                                today++;
                            }
                            else if (countDate == 1)
                            {
                                yesterday++;
                            }
                            else if (countDate > 1 && countDate <= 7)
                            {
                                week++;
                            }
                        }
                        outGoingList.Add(new DashboardData()
                        {
                            Status = item,
                            Today = today,
                            Yesterday = yesterday,
                            Week = week
                        });

                        ForSign = string.Format("{0},{1},{2},{3}", item, today, yesterday, week);
                    }

                    if (item.Equals("SIGNED"))
                    {
                        for (int i = 0; i < statList.Count(); i++)
                        {
                            fsList.Add(statList.Select(x => x.Signed.ToString()).OrderBy(x => x).Skip(i).FirstOrDefault());
                        }
                        foreach (var signeDate in fsList)
                        {
                            var newSignDate = new DateTime(
                                DateTime.Parse(signeDate).Year,
                                DateTime.Parse(signeDate).Month,
                                DateTime.Parse(signeDate).Day,
                                00, 00, 00);

                            var countDate = (newDateNow.Subtract(newSignDate).TotalDays);
                            if (countDate == 0)
                            {
                                today++;
                            }
                            else if (countDate == 1)
                            {
                                yesterday++;
                            }
                            else if (countDate > 1 && countDate <= 7)
                            {
                                week++;
                            }
                        }
                        outGoingList.Add(new DashboardData()
                        {
                            Status = item,
                            Today = today,
                            Yesterday = yesterday,
                            Week = week
                        });
                        Signed = string.Format("{0},{1},{2},{3}", item, today, yesterday, week);
                    }

                    if (item.Equals("RECEIVED"))
                    {
                        for (int i = 0; i < statList.Count(); i++)
                        {
                            fsList.Add(statList.Select(x => x.ForRelease.ToString()).OrderBy(x => x).Skip(i).FirstOrDefault());
                        }
                        foreach (var signeDate in fsList)
                        {
                            var newSignDate = new DateTime(
                                DateTime.Parse(signeDate).Year,
                                DateTime.Parse(signeDate).Month,
                                DateTime.Parse(signeDate).Day,
                                00, 00, 00);

                            var countDate = (newDateNow.Subtract(newSignDate).TotalDays);
                            if (countDate == 0)
                            {
                                today++;
                            }
                            else if (countDate == 1)
                            {
                                yesterday++;
                            }
                            else if (countDate > 1 && countDate <= 7)
                            {
                                week++;
                            }
                        }
                        outGoingList.Add(new DashboardData()
                        {
                            Status = item,
                            Today = today,
                            Yesterday = yesterday,
                            Week = week
                        });
                        Received = string.Format("{0},{1},{2},{3}", item, today, yesterday, week);
                    }
                }
                return outGoingList;
            }
        }
    }
}
