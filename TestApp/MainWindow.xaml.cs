using System;
using System.Collections.Generic;
using System.Linq;
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
using Order;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Order_cls aCls=new Order_cls();
            using (var ctx=new Entities())
            {
                //get client
                var client = ctx.CLIENTs.First(x => x.CLIENT_ID == 60);//.Last();

                var c1 = client.U_CONTRACT_USER.Last();
                //create contract
                var c = new CONTRACT() { U_CONFIRM_DATE = DateTime.Now};

                //create contract id
             //   c.U_CONTRACT_ID = id;
               
                
                //add contract to client
                client.U_CONTRACT_USER.Add(c);//working

                //create 3 contract data
                int i = 1;
                foreach (var c2 in c1.U_CONTRACT_DATA_USER)
                {
                    
                

                    //create  contract data
                    var ccd = new CONTRACT_DATA() {U_FINAL_PRICE = c2.U_FINAL_PRICE, U_REMARKS =c2.U_REMARKS};

              // create  contract data id.
                    var id1 = ctx.U_CONTRACT_DATA.Max(x => x.U_CONTRACT_DATA_ID) + 1+i++;
                    ccd.U_CONTRACT_DATA_ID = id1;
                  
                    
                    //add contract data to contract.
                    c.U_CONTRACT_DATA_USER.Add(ccd);

                }
                var id = ctx.U_CONTRACT.Max(x => x.U_CONTRACT_ID) + 1;
                c.U_CONTRACT_ID = id;
                
             //   ctx.AddToU_CONTRACT(c); working
                ctx.SaveChanges();
            }
        }
    }
}
