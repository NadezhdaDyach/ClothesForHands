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

namespace ClothesForHandsMaterials.Properties
{
    public partial class EditMaterial : Form
    {
        DataBase dataBase = new DataBase();
        private Bitmap bmp;
        string imageTitle;
        string pathToFile;
        int materialID;
        Material material = new Material();
        List<MaterialType> materialTypes = new List<MaterialType>();

        public EditMaterial(int Data)
        {
            InitializeComponent();
            materialID = Data;
        }

        private void EditMaterial_Load(object sender, EventArgs e)
        {
            dataBase.openConnection();
            SelectMaterialType();
            for (int i = 0; i < materialTypes.Count; i++)
            {
                comboBoxMaterialType.Items.Add(materialTypes[i].getTitle());
            }
            if (materialID == -1)
                InitNewMaterial();
            else
                InitEditMaterial();
        }


        private void InitNewMaterial()
        {
            textBoxMaterialID.Enabled = false;
            pictureBox1.Image = Resources.picture;
            material.setTitle("");
            imageTitle = "";

            material.setMaterialTypeID(materialTypes[0].getID());
            material.setDescription("");
            comboBoxMaterialType.SelectedIndex = 0;
        }
        private void InitEditMaterial()
        {
            textBoxMaterialID.Enabled = true;
            SelectMaterial();
            textBoxMaterialID.Text=material.getID().ToString();
            textBoxTitle.Text=material.getTitle();
            textBoxCountInPack.Text=material.getCountInPack().ToString();
            textBoxUnit.Text=material.getUnit();
            textBoxCountInStock.Text=material.getCountInStock().ToString();
            textBoxMinCount.Text=material.getMinCount().ToString();
            textBoxDescription.Text=material.getDescription();
            textBoxCost.Text=material.getCost().ToString();
            pathToFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"materials");
            if (material.getImage() == "")
            {
                pictureBox1.Image = Resources.picture;
                imageTitle = "";
            }
            else
            {
                imageTitle = material.getImage();
                imageTitle = imageTitle.Remove(0, imageTitle.LastIndexOf("\\")+1);
                imageTitle = imageTitle + ".bmp";
                material.setImage(@"\materials\" + imageTitle);
            }
        }
        private void SelectMaterial()
        {
            String sqlExpression = "SELECT * FROM Material WHERE ID=" + materialID;
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
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
            }
            reader.Close();
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
           // Описываем объект класса OpenFileDialog 
            OpenFileDialog dialog = new OpenFileDialog();
                // Задаем расширения файлов dialog.Filter = "Image files (*.BMP, *.JPG, " + "*.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png"; 
                // Вызываем диалог и проверяем выбран ли файл 
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Загружаем изображение из выбранного файла 
                    Image image = Image.FromFile(dialog.FileName);
                 int width = image.Width; int height = image.Height;

                    // Создаем и загружаем изображение в формате bmp 
                    bmp = new Bitmap(image, width, height);
                    // Записываем изображение в pictureBox1 
                    pictureBox1.Image = bmp;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            try
            {
                imageTitle = dialog.FileName.Substring(dialog.FileName.LastIndexOf(@"\") + 1, dialog.FileName.Length - dialog.FileName.LastIndexOf(@"\") - 1);
                imageTitle = imageTitle.Remove(imageTitle.LastIndexOf("."), imageTitle.Length - imageTitle.LastIndexOf("."));
                imageTitle = imageTitle + ".bmp";
                pathToFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "materials");
                material.setImage(@"\materials\" + imageTitle);
            }
            catch(Exception ex)
            {
                imageTitle = "";
            }
        }


        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            material.setTitle(textBoxTitle.Text);
        }
        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            material.setTitle(textBoxTitle.Text);
        }

        private void textBoxCountInPack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                material.setCountInPack(Convert.ToInt32(textBoxCountInPack.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите целое число");
                textBoxCountInPack.Text = "";
            }
        }

        private void textBoxUnit_TextChanged(object sender, EventArgs e)
        {
            material.setUnit(textBoxUnit.Text);
        }

        private void textBoxCountInStock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                material.setCountInStock(Convert.ToInt32(textBoxCountInStock.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите целое число");
                textBoxCountInStock.Text = "";
            }
        }

        private void textBoxMinCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                material.setMinCount(Convert.ToInt32(textBoxMinCount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите целое число");
                textBoxMinCount.Text = "";
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            material.setDescription(textBoxDescription.Text);
        }

        private void textBoxCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                material.setCost(Convert.ToSingle(textBoxCost.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите числовое значение");
                textBoxCost.Text = "";
            }
        }

        private void SelectMaterialType()
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
        }

        private void EditMaterial_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataBase.closeConnection();
        }

        private void comboBoxMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            material.setMaterialTypeID(materialTypes[comboBoxMaterialType.SelectedIndex].getID());
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteImage_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.picture;
            material.setImage("");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(material.getTitle()=="")
            {
                MessageBox.Show("Поле \"Наименование\" не может быть пустым!");
                return;
            }
            if (material.getCountInPack() <= 0)
            {
                MessageBox.Show("Количество материала в упаковке должно быть больше 0!");
                return;
            }
            if (material.getUnit() == "")
            {
                MessageBox.Show("Поле \"Единица измерения\" не может быть пустым!");
                return;
            }
            if (material.getMinCount() <= 0)
            {
                MessageBox.Show("Минимальное количество должно быть больше 0 0!");
                return;
            }
            if (material.getCost() <= 0)
            {
                MessageBox.Show("Стоимость материала должно быть больше 00!");
                return;
            }
            if (imageTitle != "")
            {
                MessageBox.Show(imageTitle);
                try
                {
                  //  bmp.Save(pathToFile + "\\" + imageTitle, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                material.setImage("");
            }
            if (materialID == -1)
                InsertIntoMaterial();
            else
                UpdateMaterial();
            this.Close();
        }

        private void InsertIntoMaterial()
        {
            String sqlExpression = "INSERT INTO Material VALUES ('"+material.getTitle()+ "',"+material.getCountInPack()+",'"+
               material.getUnit()+"',"+material.getCountInStock()+","+material.getMinCount()+",'"+material.getDescription()+"',"+
               material.getCost()+",'"+material.getImage()+"',"+material.getMaterialTypeID()+")";
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            command.ExecuteNonQuery();
            MessageBox.Show("Материал добавлен");
        }
        private void UpdateMaterial()
        {
            
            String sqlExpression = "UPDATE Material SET "+"[Title]='"+ material.getTitle()+ "',"+
                "[CountInPack]=" + material.getCountInPack() + "," +
                "[Unit]='"+material.getUnit() + "'," +
                "[CountInStock]="+material.getCountInStock() + ","+
                "[MinCount]=" + material.getMinCount() + ","+
                "[Description]='" + material.getDescription() + "'," +
                "[Cost]="+material.getCost() + "," + 
                "[Image]='"+material.getImage() + "',"+
                "[MaterialTypeID]="+ material.getMaterialTypeID() + " WHERE ID="+material.getID();
            SqlCommand command = new SqlCommand(sqlExpression, dataBase.getConnection());
            command.ExecuteNonQuery();
            MessageBox.Show("Изменения сохранены");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            String sqlExpression = "DELETE FROM Material WHERE ID=" + material.getID();
            SqlCommand command1 = new SqlCommand(sqlExpression, dataBase.getConnection());
            command1.ExecuteNonQuery();
            sqlExpression = "DELETE FROM MaterialSupplier WHERE MaterialID=" + material.getID();
            SqlCommand command2 = new SqlCommand(sqlExpression, dataBase.getConnection());
            command2.ExecuteNonQuery();
            MessageBox.Show("Материал удален");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
