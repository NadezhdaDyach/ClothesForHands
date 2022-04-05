using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClothesForHandsMaterials.Properties;

namespace ClothesForHandsMaterials
{
    public partial class MaterialsList : Form
    {
        DataBase dataBase = new DataBase();
        List<Material> allMaterials = new List<Material>();
        List<Material> searchedMaterials = new List<Material>();
        List<MaterialType> materialTypes = new List<MaterialType>();
        List<Supplier> suppliers = new List<Supplier>();
        List<MaterialSupplier> materialSuppliers = new List<MaterialSupplier>();

        List<Panel> panels = new List<Panel>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Label> labelTitles = new List<Label>();
        List<Label> labelCountInStocks = new List<Label>();
        List<Label> labelMinCounts = new List<Label>();
        List<Label> labelSheetNumbers = new List<Label>();
        List<RichTextBox> richTextBoxeSuppliers = new List<RichTextBox>();

        String sqlExpression = "";
        String query = "";
        String strFilter ="";
        String strSort ="";
        int sheet;
        int materialID;
        int totalCount;
        public MaterialsList()
        {
            InitializeComponent();
            dataBase.openConnection();
        }

        private void MaterialsList_Load(object sender, EventArgs e)
        {
            SelectMaterial();
            totalCount = allMaterials.Count;
            SelectMaterialType();
            SelectMaterialSupplier();
            SelectSupplier();
            sheet = 1;
            InitViews();
            cmbBoxSort.SelectedIndex = 0;
            cmbBoxFilter.SelectedIndex = 0;
        }
        public void InitViews()
        {
            cmbBoxSort.Items.Add("По возрастанию: наименование");
            cmbBoxSort.Items.Add("По убыванию: наименование");
            cmbBoxSort.Items.Add("По возрастанию: остаток на складе");
            cmbBoxSort.Items.Add("По убыванию: остаток на складе");
            cmbBoxSort.Items.Add("По возростанию: стоимость");
            cmbBoxSort.Items.Add("По убыванию: стоимость");

            cmbBoxFilter.Items.Add("Все типы");
            for(int i=0;i<materialTypes.Count;i++)
            {
                cmbBoxFilter.Items.Add(materialTypes[i].getTitle());
            }

            for (int i = 0; i < 15; i++)
            {
                Panel panel = new Panel();
      
                panel.Location = new Point(0, i * 110);
                panel.Width = 860;
                panel.Height = 110;
                panel.BorderStyle=BorderStyle.FixedSingle;
                panel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                panels.Add(panel);
                panelsContainer.Controls.Add(panel);
               
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(15, 10);
                pictureBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left );
                pictureBox.Width = 90;
                pictureBox.Height = 90;
                pictureBox.Image = Resources.picture;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.BackColor = Color.White;
                pictureBoxes.Add(pictureBox);
                panels[i].Controls.Add(pictureBoxes[i]);

                Label labelTitle = new Label();
                labelTitle.Location = new Point(155, 9);
                labelTitle.AutoSize=true;
                labelTitle.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                labelTitle.Text = "Тип материала | Наименование материала";
                labelTitle.Font = new Font("Candara", 14, FontStyle.Bold);
                labelTitles.Add(labelTitle);
                panels[i].Controls.Add(labelTitles[i]);

                Label labelCountInStock = new Label();
                labelCountInStock.Location = new Point(727, 13);
                labelCountInStock.AutoSize = true;
                labelCountInStock.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelCountInStock.Text = "Остаток: 0 шт";
                labelCountInStock.Font = new Font("Candara", 12, FontStyle.Regular);
                labelCountInStocks.Add(labelCountInStock);
                panels[i].Controls.Add(labelCountInStocks[i]);

                Label labelMinCount = new Label();
                labelMinCount.Location = new Point(155, 35);
                labelMinCount.AutoSize = true;
                labelMinCount.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                labelMinCount.Text = "Минимальное количество: 3 шт";
                labelMinCount.Font = new Font("Candara", 12, FontStyle.Regular);
                labelMinCounts.Add(labelMinCount);
                panels[i].Controls.Add(labelMinCounts[i]);

                RichTextBox richTextBoxeSupplier = new RichTextBox();
                richTextBoxeSupplier.Location = new Point(158, 58);
                richTextBoxeSupplier.Width = 672;
                richTextBoxeSupplier.Height = 42;
                richTextBoxeSupplier.Anchor = (AnchorStyles.Top | AnchorStyles.Left| AnchorStyles.Right);
                richTextBoxeSupplier.Text ="Поставщики: Рога и Копыта";
                richTextBoxeSupplier.Font = new Font("Candara", 12, FontStyle.Regular);
                richTextBoxeSupplier.Find("Поставщики:", RichTextBoxFinds.MatchCase);
                richTextBoxeSupplier.SelectionFont = new Font("Candara", 12, FontStyle.Bold);
                richTextBoxeSupplier.BorderStyle = BorderStyle.None;
                richTextBoxeSuppliers.Add(richTextBoxeSupplier);
                panels[i].Controls.Add(richTextBoxeSuppliers[i]);

                panels[i].Name = ((sheet-1)*15+i).ToString();
                pictureBoxes[i].Name = ((sheet - 1) * 15 + i).ToString();
                labelTitles[i].Name = ((sheet - 1) * 15 + i).ToString();
                labelCountInStocks[i].Name = ((sheet - 1) * 15 + i).ToString();
                labelMinCounts[i].Name = ((sheet - 1) * 15 + i).ToString();
                richTextBoxeSuppliers[i].Name = ((sheet - 1) * 15 + i).ToString();
                pictureBoxes[i].Click += new EventHandler(pictureBoxes_Click);
                labelTitles[i].Click += new EventHandler(label_Click);
                labelCountInStocks[i].Click += new EventHandler(label_Click);
                labelMinCounts[i].Click += new EventHandler(label_Click);
                richTextBoxeSuppliers[i].Click += new EventHandler(richTextBoxeSuppliers_Click);
                panels[i].Click+= new EventHandler(panel_Click);
            }
        }
        private void panel_Click(object sender, EventArgs e)
        {
            Panel obj = sender as Panel;
            materialID=searchedMaterials[Convert.ToInt32(obj.Name) + (sheet - 1) * 15].getID();
            EditMaterial f = new EditMaterial(materialID);
            f.Show();
        }

        private void pictureBoxes_Click(object sender, EventArgs e)
        {
            PictureBox obj = sender as PictureBox;
            materialID = searchedMaterials[Convert.ToInt32(obj.Name) + (sheet - 1) * 15].getID();
            EditMaterial f = new EditMaterial(materialID);
            f.Show();
        }
        private void label_Click(object sender, EventArgs e)
        {
            Label obj = sender as Label;
            materialID = searchedMaterials[Convert.ToInt32(obj.Name) + (sheet - 1) * 15].getID();
            EditMaterial f = new EditMaterial(materialID);
            f.Show();
        }
      
        private void richTextBoxeSuppliers_Click(object sender, EventArgs e)
        {
            RichTextBox obj = sender as RichTextBox;
            materialID = searchedMaterials[Convert.ToInt32(obj.Name) + (sheet - 1) * 15].getID();
            EditMaterial f = new EditMaterial(materialID);
            f.Show();
        }
        private void ToSearch()
        {
            searchedMaterials.Clear();
            if (textBoxSearch.Text=="")
            {
                for (int i = 0; i < allMaterials.Count; i++)
                {
                    searchedMaterials.Add(allMaterials[i]);
                }
            }
            else for (int i = 0; i < allMaterials.Count; i++)
                {
                    if (allMaterials[i].getTitle().ToLower().Contains(textBoxSearch.Text.ToLower()))
                    {
                        searchedMaterials.Add(allMaterials[i]);
                    }
                }
        }

        public int SelectMaterial()
        {
            allMaterials.Clear();
            query = "SELECT * FROM Material";
            sqlExpression =query+strFilter+strSort;
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Material material = new Material();
                material.setID(Convert.ToInt32(reader[0]));
                material.setTitle(reader[1].ToString());
                material.setCountInPack(Convert.ToInt32(reader[2]));
                material.setUnit(reader[3].ToString());
                material.setCountInStock(Convert.ToSingle(reader[4]));
                material.setMinCount(Convert.ToSingle(reader[5]));
                material.setDescription(reader[6].ToString());
                material.setCost(Convert.ToSingle(reader[7]));
                material.setImage(reader[8].ToString());
                material.setMaterialTypeID(Convert.ToInt32(reader[9]));
                allMaterials.Add(material);
            }
            reader.Close();
            return allMaterials.Count;
        }
        public int SelectSupplier() {
            suppliers.Clear();
            String sqlExpression = "SELECT * FROM Supplier";
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.setID(Convert.ToInt32(reader[0]));
                supplier.setTitle(reader[1].ToString());
                supplier.setINN(reader[2].ToString());
                supplier.setStartDate(Convert.ToDateTime(reader[3]));
                supplier.setQualityRating(Convert.ToInt32(reader[4]));
                supplier.setSupplierType(reader[5].ToString());
                suppliers.Add(supplier);
            }
            reader.Close();
            return suppliers.Count;
        }

        public int SelectMaterialSupplier()
        {
            materialSuppliers.Clear();
            String sqlExpression = "SELECT * FROM MaterialSupplier";
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MaterialSupplier materialSupplier = new MaterialSupplier();
                materialSupplier.setMaterialID(Convert.ToInt32(reader[0]));
                materialSupplier.setSupplierID(Convert.ToInt32(reader[1]));
                materialSuppliers.Add(materialSupplier);
            }
            reader.Close();
            return materialSuppliers.Count;
        }
        public int SelectMaterialType()
        {
            materialTypes.Clear();
            String sqlExpression = "SELECT * FROM MaterialType";
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MaterialType materialType = new MaterialType();
                materialType.setID(Convert.ToInt32(reader[0]));
                materialType.setTitle(reader[1].ToString());
                materialTypes.Add(materialType);
            }
            reader.Close();
            return materialTypes.Count();
        }

        private void MaterialsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataBase.closeConnection();
        }

        private void cmbBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxFilter.SelectedIndex == 0)
                strFilter = "";
            else
                strFilter = " WHERE MaterialTypeID="+ cmbBoxFilter.SelectedIndex.ToString();
            SelectMaterial();
            ToSearch();
            InitItems();
            label2.Text = (searchedMaterials.Count() / 15 + 1).ToString();
            label6.Text = "Количество записей: " + searchedMaterials.Count.ToString() + " из " + totalCount.ToString();
        }

        private void cmbBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxSort.SelectedIndex == 0)
                strSort = " ORDER BY Title ASC";
            if (cmbBoxSort.SelectedIndex == 1)
                strSort = " ORDER BY Title DESC";
            if (cmbBoxSort.SelectedIndex == 2)
                strSort = " ORDER BY CountInStock ASC";
            if (cmbBoxSort.SelectedIndex == 3)
                strSort = " ORDER BY CountInStock DESC";
            if (cmbBoxSort.SelectedIndex == 4)
                strSort = " ORDER BY Cost ASC";
            if (cmbBoxSort.SelectedIndex == 5)
                strSort = " ORDER BY Cost DESC";
            SelectMaterial();
            ToSearch();
            InitItems();
            label2.Text = (searchedMaterials.Count() / 15 + 1).ToString();
            label6.Text = "Количество записей: " + searchedMaterials.Count.ToString() + " из " + totalCount.ToString();

        }



        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ToSearch();
            SelectMaterial();
            InitItems();
            label2.Text = (searchedMaterials.Count() / 15 + 1).ToString();
            label6.Text = "Количество записей: " + searchedMaterials.Count.ToString() + " из " + totalCount.ToString();

        }



        private void InitItems()
        {
            int lastSheet = searchedMaterials.Count / 15 + 1;
            int n;
            if (sheet==lastSheet)
                n = searchedMaterials.Count % 15;
            else 
                n = 15;
            for (int i = 0; i < n; i++)
            {
                int index = (sheet - 1) * 15 + i;
                for (int k = 0; k < materialTypes.Count; k++)
                    if (searchedMaterials[index].getMaterialTypeID() == materialTypes[k].getID())
                        labelTitles[i].Text = materialTypes[k].getTitle() + " | " + searchedMaterials[index].getTitle();
                labelMinCounts[i].Text = "Минимальное количество: " + searchedMaterials[index].getMinCount().ToString() + " шт";
                labelCountInStocks[i].Text = "Остаток: " + searchedMaterials[index].getCountInStock().ToString() + " шт";

                richTextBoxeSuppliers[i].Text = "Поставщики: ";
                List<int> suppliersIDList = new List<int>();
                for (int k = 0; k < materialSuppliers.Count; k++)
                    if (searchedMaterials[index].getID() == materialSuppliers[k].getMaterialID())
                        suppliersIDList.Add(materialSuppliers[k].getSupplierlID());
                for (int k = 0; k < suppliers.Count; k++)
                    for (int l = 0; l < suppliersIDList.Count; l++)
                    {
                        if (suppliers[k].getID() == suppliersIDList[l])
                            richTextBoxeSuppliers[i].Text = richTextBoxeSuppliers[i].Text + suppliers[k].getTitle() + ", ";
                    }
                richTextBoxeSuppliers[i].Find("Поставщики:", RichTextBoxFinds.MatchCase);
                richTextBoxeSuppliers[i].SelectionFont = new Font("Candara", 12, FontStyle.Bold);
                if (searchedMaterials[index].getImage() == "")
                {
                    pictureBoxes[i].Image = Resources.picture;
                }
                else
                {
                    String pathToFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "materials");
                    String imageTitle = searchedMaterials[index].getImage();
                    imageTitle = imageTitle.Remove(0, imageTitle.LastIndexOf(@"\"));
                    pictureBoxes[i].Image = new Bitmap(pathToFile + imageTitle);
                }
                if (searchedMaterials[index].getCountInStock() < searchedMaterials[index].getMinCount())
                {
                    panels[i].BackColor = ColorTranslator.FromHtml("#f19292");
                    pictureBoxes[i].BackColor = ColorTranslator.FromHtml("#f19292");
                    richTextBoxeSuppliers[i].BackColor = ColorTranslator.FromHtml("#f19292");
                }
                if (searchedMaterials[index].getCountInStock() > 3 * searchedMaterials[index].getMinCount() - 1)
                {
                    panels[i].BackColor = ColorTranslator.FromHtml("#ffba01");
                    pictureBoxes[i].BackColor = ColorTranslator.FromHtml("#ffba01");
                    richTextBoxeSuppliers[i].BackColor = ColorTranslator.FromHtml("#ffba01");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sheet = Convert.ToInt32(textBox1.Text);
                if (sheet < 0 )
                    textBox1.Text = "1";
                if (sheet > searchedMaterials.Count / 15 + 1)
                    textBox1.Text = (sheet > searchedMaterials.Count / 15 + 1).ToString();
            }
            catch(Exception ex)
            {
                textBox1.Text = "1";
                sheet = 1;
            }
            ToSearch();
            SelectMaterial();
            InitItems();
        }

     

        private void labelPreviousSheet_Click(object sender, EventArgs e)
        {
            if (sheet > 1)
            {
                sheet--;
                textBox1.Text = sheet.ToString();
            }
        }
        private void labelNextSheet_Click(object sender, EventArgs e)
        {
            if (sheet < searchedMaterials.Count / 15 + 1)
            {
                sheet++;
                textBox1.Text = sheet.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditMaterial f = new EditMaterial(-1);
            f.Show();
        }

    }
}
