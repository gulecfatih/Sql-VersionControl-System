using SqlVersionControlSystem.Dal;
using SqlVersionControlSystem.Models;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SqlVersionControlSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Sadece Okunmasýný Saðlýyor
            richTextBoxSol.ReadOnly = true;
            richTextBoxSað.ReadOnly = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSpname.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEskiSp.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYeniSp.DropDownStyle = ComboBoxStyle.DropDownList;
            spTableLoad();
        }

        public void spTableLoad()
        {
            DbQueryExecute dbQueryExecute = new DbQueryExecute();
            var dt = dbQueryExecute.GetData($"select * from spTable");

            List<SpName> list = new List<SpName>();

            list.Add(new SpName
            {
                Id = 0,
                Name = ""
            });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new SpName
                {
                    Id = Convert.ToInt32(dt.Rows[i]["spID"]),
                    Name = dt.Rows[i]["spName"].ToString()
                });
            }

            cmbSpname.DataSource = list;
            cmbSpname.DisplayMember = "Name"; 
            cmbSpname.ValueMember = "Id";
            cmbSpname.MaxDropDownItems = cmbSpname.Items.Count;

        }

        private void cmbSpname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpname.SelectedIndex == 0)
                return;
            DbQueryExecute dbQueryExecute = new DbQueryExecute();
            var dt = dbQueryExecute.GetData($"select slt.logID,(st.spName+' '+convert(nvarchar,slt.logDate,104)+' '+convert(nvarchar,slt.logDate,108)) as Name from spTable st join spLogTable slt on st.spID=slt.spID where slt.spID ={cmbSpname.SelectedValue.ToString()}");

            List<SpName> list = new List<SpName>();
            list.Add(new SpName
            {
                Id = 0,
                Name = ""
            });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new SpName
                {
                    Id = Convert.ToInt32(dt.Rows[i]["logID"]),
                    Name = dt.Rows[i]["Name"].ToString()
                });
            }

            cmbEskiSp.DataSource = list;
            cmbEskiSp.DisplayMember = "Name"; 
            cmbEskiSp.ValueMember = "Id";
            cmbEskiSp.MaxDropDownItems = cmbEskiSp.Items.Count;
        }

        private void cmbEskiSp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEskiSp.SelectedIndex == 0)
            {
                richTextBoxSol.Text = "";
                return;
            }
               
            DbQueryExecute dbQueryExecute = new DbQueryExecute();
            var dt = dbQueryExecute.GetData($"select slt.logID,(st.spName+' '+convert(nvarchar,slt.logDate,104)+' '+convert(nvarchar,slt.logDate,108)) as Name from spTable st join spLogTable slt on st.spID=slt.spID where slt.logID>{cmbEskiSp.SelectedValue.ToString()} and  slt.spID={cmbSpname.SelectedValue.ToString()}");

            List<SpName> list = new List<SpName>();
            list.Add(new SpName
            {
                Id = 0,
                Name = ""
            });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new SpName
                {
                    Id = Convert.ToInt32(dt.Rows[i]["logID"]),
                    Name = dt.Rows[i]["Name"].ToString()
                });
            }
            
            cmbYeniSp.DataSource = list;
            cmbYeniSp.DisplayMember = "Name";
            cmbYeniSp.ValueMember = "Id"; 
            cmbYeniSp.MaxDropDownItems = cmbYeniSp.Items.Count;

            richTextBoxSol.Text = dbQueryExecute.ExecuteScalar($"select slt.spText from spTable st join spLogTable slt on st.spID=slt.spID where slt.logID={cmbEskiSp.SelectedValue.ToString()}").ToString();

        }

        private void cmbYeniSp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYeniSp.SelectedIndex == 0 || cmbYeniSp.SelectedIndex == -1)
            {
                richTextBoxSað.Text = "";
                return;
            }
            DbQueryExecute dbQueryExecute = new DbQueryExecute();
            richTextBoxSað.Text = dbQueryExecute.ExecuteScalar($"select slt.spText from spTable st join spLogTable slt on st.spID=slt.spID where slt.logID={cmbYeniSp.SelectedValue.ToString()}").ToString();
        }

        private void CompareTexts()
        {
            // richTextBoxSol ve richTextBoxSað metinlerini satýrlara ayýr
            string[] linesLeft = richTextBoxSol.Lines;
            string[] linesRight = richTextBoxSað.Lines;

            // En fazla satýr sayýsýný belirle
            int maxLines = Math.Max(linesLeft.Length, linesRight.Length);

            // Satýr bazýnda karþýlaþtýrma
            for (int i = 0; i < maxLines; i++)
            {
                bool leftExists = i < linesLeft.Length;
                bool rightExists = i < linesRight.Length;

                if (leftExists && rightExists)
                {
                    if (linesLeft[i] != linesRight[i])
                    {
                        HighlightLine(richTextBoxSol, i, Color.Red, Color.White);
                        HighlightLine(richTextBoxSað, i, Color.Green, Color.White);
                    }
                    else
                    {
                        HighlightLine(richTextBoxSol, i, Color.White, Color.Black);
                        HighlightLine(richTextBoxSað, i, Color.White, Color.Black);
                    }
                }
                else if (leftExists)
                {
                    HighlightLine(richTextBoxSol, i, Color.Red, Color.White);
                }
                else if (rightExists)
                {
                    HighlightLine(richTextBoxSað, i, Color.Green, Color.White);
                }
            }
        }

        private void HighlightLine(RichTextBox richTextBox, int lineIndex, Color backColor, Color textColor)
        {
            int startIndex = richTextBox.GetFirstCharIndexFromLine(lineIndex);
            int lineLength = richTextBox.Lines[lineIndex].Length;
            // Satýrý seç ve renklerini ayarla
            richTextBox.Select(startIndex, lineLength);
            richTextBox.SelectionBackColor = backColor;
            richTextBox.SelectionColor = textColor;
        }

        private void btnKarsýlastýrma_Click(object sender, EventArgs e)
        {
            CompareTexts();
        }

    }
}
