using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Db4objects.Db4o.Linq;

namespace DemoDB4O
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            //Tao
            var pilot = new Pilot(txtName.Text, Convert.ToInt32(txtPoint.Text));
            //Them
            db.Store(pilot);

            db.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            //Query By Example
            var pilotTemp = new Pilot();
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                pilotTemp.Name = txtName.Text;
            }

            List<Pilot> data = new List<Pilot>();
            IObjectSet result = db.QueryByExample(pilotTemp);
            for (int i = 0; i < result.Count; i++)
            {
                var plObj = (Pilot)result[i];
                data.Add(plObj);
            }
            db.Close();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            //Query By Example
            var pilotTemp = new Pilot();
            pilotTemp.Name = txtName.Text;//dua vao Name (key)

            IObjectSet result = db.QueryByExample(pilotTemp);
            var pilot = (Pilot)result[0];
            pilot.Point += Convert.ToInt32(txtPoint.Text);

            db.Store(pilot);
            db.Close();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Tìm pilot có point >= 100 và tên chứa chữ An
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            IList<Pilot> mylist = db.Query(delegate (Pilot pilot)
            {
                if(pilot.Point >= Convert.ToInt32(txtPoint.Text) && pilot.Name.Contains(txtName.Text))
                {
                    return true;
                }
                return false;
            });
            db.Close();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = mylist.ToList();
        }

        private void btnLINQSearch_Click(object sender, EventArgs e)
        {
            //Tìm pilot có point >= 100 và tên chứa chữ An
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            IEnumerable<Pilot> myList = from Pilot pilot in db
                                        where pilot.Point >= Convert.ToInt32(txtPoint.Text) && pilot.Name.Contains(txtName.Text)
                                        select pilot;
            db.Close();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myList.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IObjectContainer db = Db4oEmbedded.OpenFile("pilotdb.db");

            var myCar = new Car
            {
                Model = "Ferrari",
                Pilot = new Pilot
                {
                    Name = "Jack Ma",
                    Point = 1000
                }
            };
            db.Store(myCar);
            db.Close();
        }
    }
}
